using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Parameter
{
    public string _name;
    public string _shaderParam;
    public float _value;
    public float _min;
    public float _max;

    public Parameter(string name, string shaderParam, float value, float min, float max)
    {
        _name = name;
        _shaderParam = shaderParam;
        _value = value;
        _min = min;
        _max = max;
    }

    public void SetValue(Material m)
    {
        m.SetFloat(_shaderParam, _value);
    }
    public void Render(int y, int w, int tw, int margin)
    {
        GUI.Label(new Rect(margin, y, tw, 30), _name + ":");
        _value = GUI.HorizontalSlider(new Rect(margin + tw + margin, y, w, 30), _value, _min,_max);
    }

}

public class MainScene : MonoBehaviour {

	// Use this for initialization

    private Vector3 _cameraAcc = Vector3.zero;
    private Vector3 _viewAcc = Vector3.zero;
    private Vector3 _view = Vector3.zero;
    private Vector3 _sunRotation = new Vector3(25, 300, 0);
    private float _moveScale = 2.5f;
    private List<Parameter> _parameters = new List<Parameter>();
   // private int _timeToAutoDetail=100;
    private bool _autoDetail = false;
    private bool _displayMenu = true;

    public Parameter GetParameter(string name) {
        foreach (Parameter p in _parameters) {
            if (p._name == name) {
                return p;
            }
        }
        return null;
    }

    private static string _infoText =
        "LemonSpawn VolClouds 1.0 example scene.\n" +
        "Left mouse button to look, WSAD to move.\n" +
        "Currently not renderable from inside the clouds, so height is constrained to stay below the cloud height.\n" +
        "GPU-heavy but sexy - change the \"Detail\" parameter to tune performance."; 

                                        


	void Start () {
        _parameters.Add(new Parameter("Scale", "_CloudScale", 0.3f, 0, 1));
        _parameters.Add(new Parameter("Distance", "_CloudDistance", 0.03f, 0, 0.25f));
        _parameters.Add(new Parameter("Detail", "_MaxDetail", 0.33f, 0, 1f));
        _parameters.Add(new Parameter("Subtract", "_CloudSubtract", 0.6f, 0, 1f));
        _parameters.Add(new Parameter("Scattering", "_CloudScattering", 1.5f, 1f, 3f));
        _parameters.Add(new Parameter("Y Spread", "_CloudHeightScatter", 1.75f, 0.5f, 6f));
        _parameters.Add(new Parameter("Density", "_CloudAlpha", 0.6f, 0f, 1f));
        _parameters.Add(new Parameter("Hardness", "_CloudHardness", 0.9f, 0f, 1f));
        _parameters.Add(new Parameter("Brightness", "_CloudBrightness", 1.4f, 0f, 2f));
        _parameters.Add(new Parameter("Sun Glare", "_SunGlare", 0.6f, 0f, 2f));
       // _parameters.Add(new Parameter("Time", "_CloudTime", 0, 0f, 100f));

        _parameters.Add(new Parameter("XShift", "_XShift", 0f, 0f, 1f));
        _parameters.Add(new Parameter("YShift", "_YShift", 0f, 0f, 1f));
        _parameters.Add(new Parameter("ZShift", "_ZShift", 0f, 0f, 1f));

        //FillTerrain();
    }


    void FillTerrain() {
        Terrain t = GameObject.Find("Terrain").GetComponent<Terrain>();
        TerrainData terrainData = t.terrainData;
        int ewidth = terrainData.heightmapResolution;
        int eheight = terrainData.heightmapResolution;
        float[,] data = terrainData.GetHeights (0, 0, ewidth, eheight);

        for (int i = 0; i < ewidth; i++) {


            for (int j = 0; j < eheight; j++) {
                Vector2 pos = new Vector3(i, j);
                float scale = 0.00034f;
                float h = 0;
                float a = 0;
                for (int k = 1; k < 10; k++) {
                    float f = Mathf.Pow(2, k);
                    float amp = 1.0f / (2 * Mathf.Pow(k, 1f));
                    h += (Mathf.PerlinNoise(pos.x * scale * f, pos.y * scale * f) - 0.5f) * amp;
                    a += amp;
                }
                h /= a;

                data[i, j] = Mathf.Clamp((h * 1.7f) + 0.04f, 0, 1);

            }
        }
        terrainData.SetHeights (0, 0, data);
        t.terrainData = terrainData;
        t.Flush();

    }

    void SetAutoDetail() {
        if (_autoDetail)   {
            float fps = Mathf.Clamp(1.0f/Time.smoothDeltaTime,1,60);
//            Debug.Log(fps);
            float dir = (fps - 25); // Target FPS
            Parameter detail = GetParameter("Detail");
            if (detail == null) {
                return;
            }
            
            detail._value +=dir*0.0005f;
        }
    }

    void OnGUI() {
        if (!_displayMenu) {
            return;
        }
        int w = 150;
        int dy = 27;
        int textWidth = 75;
        int margin = 10;

        GUI.contentColor = Color.black;


        GUI.Label(new Rect(margin, dy, textWidth, dy), "Time of day");
        GUI.Label(new Rect(margin, 2*dy, textWidth, dy), "Sun rotation");
        _sunRotation.x = GUI.HorizontalSlider(new Rect((2*margin) + textWidth, dy, w, dy), _sunRotation.x, 0.0F, 360.0F);
        _sunRotation.y = GUI.HorizontalSlider(new Rect((2* margin) + textWidth, 2*dy, w, dy), _sunRotation.y, 0.0F, 360.0F);
        GUI.Label(new Rect(0, 0, 100, 100), "FPS: "+(int)(1.0f / Time.smoothDeltaTime));

        GUI.Label(new Rect(Screen.width - 380, Screen.height - 100, 360, 100), _infoText);
        GUI.contentColor = Color.white;
        GUI.Label(new Rect(Screen.width - 381, Screen.height - 101, 360, 100), _infoText);
        GUI.contentColor = Color.black;
        string txt = "On";
        if (_autoDetail) {
            txt = "Off";
        }

        if (GUI.Button(new Rect(margin, (int)(3.5f*dy), 200, dy), "Auto Detail " + txt +  "  (performance)")){
            _autoDetail = !_autoDetail;
        }
        int i = 5;
        foreach (Parameter p in _parameters) {
            p.Render(i++ * dy, w, textWidth, margin);
        }



    }
	
    void MoveCamera()
    {
        Camera c = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (c == null) {
            return;
        }
        if (Input.GetKey(KeyCode.W)) {
            _cameraAcc += c.transform.forward * _moveScale;
        }
        if (Input.GetKey(KeyCode.S)) {
            _cameraAcc += c.transform.forward * _moveScale * -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            _cameraAcc += c.transform.right * _moveScale;
        }
        if (Input.GetKey(KeyCode.A)) {
            _cameraAcc += c.transform.right * _moveScale * -1;
        }

        if (Input.GetButton("Fire2")) {
            _viewAcc += ((Vector3.right * Input.GetAxis("Mouse X")) +( Vector3.up * Input.GetAxis("Mouse Y") * -1 * 0.2f));
        }

        c.transform.position = c.transform.position + _cameraAcc;
        Vector3 pos = c.transform.position;
        pos.y = Mathf.Clamp(pos.y, 1f, 100);
        c.transform.position = pos;
        _view += _viewAcc;
        c.transform.eulerAngles = new Vector3(_view.y, _view.x, 0.0f);
        _cameraAcc *= 0.90f;
        _viewAcc *= 0.90f;

    }

    void MoveSun()
    {
        GameObject sun = GameObject.Find("Sun");
        if (sun == null) {
            return;
        }

        sun.transform.rotation = Quaternion.Euler(_sunRotation);
    }

    void UpdateMaterial()
    {
        Material mat = GameObject.Find("Sphere").GetComponent<MeshRenderer>().material;
        foreach (Parameter p in _parameters) {
            p.SetValue(mat);
        }

        mat.SetFloat("_CloudTime", Time.time*0.01f);
    }

    void Update () {
        MoveCamera();
        MoveSun();
        UpdateMaterial();
        SetAutoDetail();
        if (Input.GetKeyUp(KeyCode.Space)) {
            _displayMenu = !_displayMenu;
        }

	}
}

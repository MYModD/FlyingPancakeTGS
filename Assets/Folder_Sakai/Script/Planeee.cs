using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planeee : MonoBehaviour {
    public enum PlaneName { Mig21, Mig29, F4E, F16 };
    public PlaneName planeName;
    public GameObject
        planeWeapons,
        planeTrails;
    public GameObject[]
        planePylons,
        planeArmament,
        planeExhaust,
        planePartsLanding,
        planePartsEject,
        planePartsBreak,
        planePartsDestroy;
    public GameObject explosion;
    public bool
        planeActive,
        planeDamageCritical;

    //Plane specs
    [HideInInspector] public string pName;
    [HideInInspector] public float hitpoints, hp;

    [HideInInspector] public List<Transform> weapons = new List<Transform>();
    [HideInInspector] public List<GameObject> trails = new List<GameObject>();

    private Renderer[] rends;
    private Rigidbody rig;
    //private AudioSource aud;

    //private PlaneControls planeControls;

    //Modifies all animation speed related variables for the jet
    private float spdFac = 1.0f;

    //Function for creation of child axises. Sometimes, additional axis is
    //needed for some jet part in order for that piece to be properly animated
    //(e.g. F-4E slats or MiG-21 wing gear doors). This function will create new
    //axis and make it child of the current axis, and it will make children of
    //the current axis to become children of this new axis. BTW, "axis" is just
    //an empty object with some position and rotation.
    void CreateChildAxis(
        string axisName, GameObject obj, bool identityRotation = false) {
        GameObject newAxis = new GameObject(axisName);
        newAxis.transform.position = obj.transform.parent.position;
        if (identityRotation)
            newAxis.transform.rotation = Quaternion.identity;
        else
            newAxis.transform.rotation = obj.transform.parent.rotation;
        newAxis.transform.parent = obj.transform.parent;

        obj.transform.parent = newAxis.transform;
    }

    void Awake() {
        rends = GetComponentsInChildren<Renderer>();
        rig = GetComponent<Rigidbody>();
        //aud = GetComponent<AudioSource>();

        //planeControls = GetComponent<PlaneControls>();

        //Groups that are parents for all jet's weapons and trails
        planeWeapons = transform.Find("Weapons").gameObject;
        planeTrails = transform.Find("Trails").gameObject;

        //Group of exhausts
        planeExhaust = new GameObject[4];
        if (transform.Find("Hull_LOD0/Nozzle_LOD0") != null)
            planeExhaust[0] = transform.Find("Hull_LOD0/Nozzle_LOD0").gameObject;
        planeExhaust[1] = transform.Find("Hull_LOD0/Flame_LOD0").gameObject;
        planeExhaust[2] = transform.Find("Hull_LOD1/Flame_LOD1").gameObject;
        planeExhaust[3] = transform.Find("Hull_LOD2/Flame_LOD2").gameObject;

        //Each individual weapon and trail stored in the lists from weapon and
        //trail groups
        for (int i = 0; i < planeWeapons.transform.childCount; i++)
            weapons.Add(planeWeapons.transform.GetChild(i));
        //weapons = planeWeapons.GetComponentsInChildren<Transform>();
        for (int i = 0; i < planeTrails.transform.childCount; i++)
            trails.Add(planeTrails.transform.GetChild(i).gameObject);

        //Jet specific attributes
        switch (planeName) {
            case PlaneName.Mig21:
                pName = "MiG-21";
                hitpoints = 9.0f;

                rig.mass = 5400.0f;

                //Groups associated with landing
                planePartsLanding = new GameObject[15];
                //LOD0
                //Struts
                planePartsLanding[0] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                    "GearStrutFront_LOD0").gameObject;
                planePartsLanding[1] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearStrutLeft_LOD0/" +
                    "GearStrutLeft_LOD0").gameObject;
                planePartsLanding[2] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearStrutLeft_LOD0/" +
                    "GearStrutLeft_LOD0/GearStrutLeftPart_LOD0").gameObject;
                planePartsLanding[3] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearStrutRight_LOD0/" +
                    "GearStrutRight_LOD0").gameObject;
                planePartsLanding[4] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearStrutRight_LOD0/" +
                    "GearStrutRight_LOD0/GearStrutRightPart_LOD0").gameObject;
                //Doors
                planePartsLanding[5] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                    "GearDoorFront1_LOD0").gameObject;
                planePartsLanding[6] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                    "GearDoorFront2_LOD0").gameObject;
                planePartsLanding[7] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorLeft1_LOD0/" +
                    "GearDoorLeft1_LOD0").gameObject;
                planePartsLanding[8] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft2_LOD0/" +
                    "GearDoorLeft2_LOD0").gameObject;
                planePartsLanding[9] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorRight1_LOD0/" +
                    "GearDoorRight1_LOD0").gameObject;
                planePartsLanding[10] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight2_LOD0/" +
                    "GearDoorRight2_LOD0").gameObject;
                //LOD1
                //Misc
                planePartsLanding[11] = transform.Find(
                    "Hull_LOD1/MiscGearStruts_LOD1").gameObject;
                planePartsLanding[12] = transform.Find(
                    "Hull_LOD1/MiscGearDoors_LOD1").gameObject;
                planePartsLanding[13] = transform.Find(
                    "Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject;
                //LOD2
                //Misc
                planePartsLanding[14] = transform.Find(
                    "Hull_LOD2/MiscGears_LOD2").gameObject;

                //Groups for pilot ejection
                planePartsEject = new GameObject[6];
                //LOD0
                planePartsEject[0] = transform.Find(
                    "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").
                    gameObject;
                planePartsEject[1] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0").gameObject;
                planePartsEject[2] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0/Pilot_LOD0").gameObject;
                //LOD1
                planePartsEject[3] = transform.Find(
                    "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").
                    gameObject;
                planePartsEject[4] = transform.Find(
                    "Hull_LOD1/PilotSeat_LOD1").gameObject;
                //LOD2    
                planePartsEject[5] = transform.Find(
                    "Hull_LOD2/MiscCanopy_LOD2").gameObject;

                //Groups that break off during destruction
                planePartsBreak = new GameObject[40];
                //LOD0
                planePartsBreak[0] = transform.Find(
                    "Hull_LOD0/Nose_LOD0").gameObject;
                planePartsBreak[1] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                    "StabilatorLeft_LOD0").gameObject;
                planePartsBreak[2] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                    "StabilatorRight_LOD0").gameObject;
                planePartsBreak[3] = transform.Find(
                    "Hull_LOD0/Tail_LOD0").gameObject;
                planePartsBreak[4] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0").gameObject;
                planePartsBreak[5] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0").gameObject;
                planePartsBreak[6] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                        "AileronLeft_LOD0").gameObject;
                planePartsBreak[7] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                        "AileronRight_LOD0").gameObject;
                planePartsBreak[8] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLeft_LOD0/" +
                        "AirbrakeLeft_LOD0").gameObject;
                planePartsBreak[9] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeRear_LOD0/" +
                        "AirbrakeRear_LOD0").gameObject;
                planePartsBreak[10] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeRight_LOD0/" +
                        "AirbrakeRight_LOD0").gameObject;
                planePartsBreak[11] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                        "FlapLeft_LOD0").gameObject;
                planePartsBreak[12] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                        "FlapRight_LOD0").gameObject;
                planePartsBreak[13] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                        "GearDoorFront1_LOD0").gameObject;
                planePartsBreak[14] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                        "GearDoorFront2_LOD0").gameObject;
                planePartsBreak[15] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft1_LOD0/" +
                        "GearDoorLeft1_LOD0").gameObject;
                planePartsBreak[16] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft2_LOD0/" +
                        "GearDoorLeft2_LOD0").gameObject;
                planePartsBreak[17] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight1_LOD0/" +
                        "GearDoorRight1_LOD0").gameObject;
                planePartsBreak[18] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight2_LOD0/" +
                        "GearDoorRight2_LOD0").gameObject;
                planePartsBreak[19] = transform.Find(
                        "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/" +
                        "Rudder_LOD0").gameObject;
                //LOD1
                planePartsBreak[20] =
                    transform.Find("Hull_LOD1/Nose_LOD1").gameObject;
                planePartsBreak[21] =
                    transform.Find("Hull_LOD1/StabilatorLeft_LOD1").gameObject;
                planePartsBreak[22] =
                    transform.Find("Hull_LOD1/StabilatorRight_LOD1").gameObject;
                planePartsBreak[23] =
                    transform.Find("Hull_LOD1/Tail_LOD1").gameObject;
                planePartsBreak[24] =
                    transform.Find("Hull_LOD1/WingLeft_LOD1").gameObject;
                planePartsBreak[25] =
                    transform.Find("Hull_LOD1/WingRight_LOD1").gameObject;

                //Groups that are simply deleted during destruction
                planePartsDestroy = new GameObject[] {
                    //LOD0
                    transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisAileronLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisAileronRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeRear_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisFlapLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisFlapRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisGearDoorLeft2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisGearDoorRight2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0").gameObject,
                    /*transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0/" +
                        "CanopyFrameGlass_LOD0").gameObject,*/
                    /*transform.Find(
                        "Hull_LOD0/CanopyGlass_LOD0").gameObject,*/
                    transform.Find(
                        "Hull_LOD0/Flame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutFront_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisGearStrutLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisGearStrutRight_LOD0").gameObject,
                    //LOD1
                    transform.Find(
                        "Hull_LOD1/_AxisCanopyFrame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Flame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Misc_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearStruts_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoors_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject,    
                    //LOD2
                    transform.Find(
                        "Hull_LOD2/Flame_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/Misc_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/MiscGears_LOD2").gameObject };

                //New axises
                CreateChildAxis(
                    "_AxisGearStrutLeftB_LOD0", planePartsLanding[1]);
                CreateChildAxis(
                    "_AxisGearStrutRightB_LOD0", planePartsLanding[3]);
                CreateChildAxis(
                    "_AxisGearDoorLeft2B_LOD0", planePartsLanding[8]);
                CreateChildAxis(
                    "_AxisGearDoorRight2B_LOD0", planePartsLanding[10]);

                //Rotate landing struts/gears in closed position
                planePartsLanding[0].transform.parent.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[1].transform.parent.parent.Translate(0.3f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[1].transform.parent.Rotate(0.0f, 0.0f, 70.0f, Space.Self);
                planePartsLanding[3].transform.parent.parent.Translate(-0.3f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[3].transform.parent.Rotate(0.0f, 0.0f, -70.0f, Space.Self);

                //Hide landing struts/gears
                //LOD0
                planePartsLanding[0].transform.parent.gameObject.SetActive(false);
                planePartsLanding[1].transform.parent.parent.gameObject.SetActive(false);
                planePartsLanding[2].SetActive(false);
                planePartsLanding[3].transform.parent.parent.gameObject.SetActive(false);
                planePartsLanding[4].SetActive(false);
                //LOD1
                planePartsLanding[11].SetActive(false);
                planePartsLanding[13].SetActive(false);
                //LOD2
                planePartsLanding[14].SetActive(false);

                break;
            case PlaneName.Mig29:
                pName = "MiG-29";
                hitpoints = 12.0f;

                rig.mass = 11000.0f;

                //Groups associated with landing
                planePartsLanding = new GameObject[19];
                //LOD0
                //Struts
                planePartsLanding[0] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                    "GearStrutFront_LOD0").gameObject;
                planePartsLanding[1] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/GearStrutFront_LOD0/" +
                    "GearStrutFrontPart_LOD0").gameObject;
                planePartsLanding[2] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutLeft_LOD0/" +
                    "GearStrutLeft_LOD0").gameObject;
                planePartsLanding[3] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutLeft_LOD0/GearStrutLeft_LOD0/" +
                    "GearStrutLeftPart_LOD0").gameObject;
                planePartsLanding[4] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutRight_LOD0/" +
                    "GearStrutRight_LOD0").gameObject;
                planePartsLanding[5] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutRight_LOD0/GearStrutRight_LOD0/" +
                    "GearStrutRightPart_LOD0").gameObject;
                //Doors
                planePartsLanding[6] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                    "GearDoorFront1_LOD0").gameObject;
                planePartsLanding[7] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                    "GearDoorFront2_LOD0").gameObject;
                planePartsLanding[8] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront3_LOD0/" +
                    "GearDoorFront3_LOD0").gameObject;
                planePartsLanding[9] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorLeft1_LOD0/" +
                    "GearDoorLeft1_LOD0").gameObject;
                planePartsLanding[10] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorLeft2_LOD0/" +
                    "GearDoorLeft2_LOD0").gameObject;
                planePartsLanding[11] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorLeft3_LOD0/" +
                    "GearDoorLeft3_LOD0").gameObject;
                planePartsLanding[12] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorRight1_LOD0/" +
                    "GearDoorRight1_LOD0").gameObject;
                planePartsLanding[13] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorRight2_LOD0/" +
                    "GearDoorRight2_LOD0").gameObject;
                planePartsLanding[14] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorRight3_LOD0/" +
                    "GearDoorRight3_LOD0").gameObject;
                //LOD1
                //Misc
                planePartsLanding[15] =
                    transform.Find("Hull_LOD1/MiscGearStruts_LOD1").gameObject;
                planePartsLanding[16] =
                    transform.Find("Hull_LOD1/MiscGearDoors_LOD1").gameObject;
                planePartsLanding[17] =
                    transform.Find("Hull_LOD1/MiscGearDoorsOpen_LOD1").
                    gameObject;
                //LOD2
                //Misc
                planePartsLanding[18] =
                    transform.Find("Hull_LOD2/MiscGears_LOD2").gameObject;

                //Groups for pilot ejection
                planePartsEject = new GameObject[6];
                //LOD0
                planePartsEject[0] = transform.Find(
                    "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").
                    gameObject;
                planePartsEject[1] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0").gameObject;
                planePartsEject[2] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0/Pilot_LOD0").gameObject;
                //LOD1
                planePartsEject[3] = transform.Find(
                    "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").
                    gameObject;
                planePartsEject[4] = transform.Find(
                    "Hull_LOD1/PilotSeat_LOD1").gameObject;
                //LOD2    
                planePartsEject[5] = transform.Find(
                    "Hull_LOD2/MiscCanopy_LOD2").gameObject;

                //Groups that get torn off during destruction
                planePartsBreak = new GameObject[50];
                //LOD0
                planePartsBreak[0] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                    "StabilatorLeft_LOD0").gameObject;
                planePartsBreak[1] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                    "StabilatorRight_LOD0").gameObject;
                planePartsBreak[2] = transform.Find(
                    "Hull_LOD0/TailLeft_LOD0").gameObject;
                planePartsBreak[3] = transform.Find(
                    "Hull_LOD0/TailRight_LOD0").gameObject;
                planePartsBreak[4] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0").gameObject;
                planePartsBreak[5] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0").gameObject;
                planePartsBreak[6] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                        "AileronLeft_LOD0").gameObject;
                planePartsBreak[7] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                        "AileronRight_LOD0").gameObject;
                planePartsBreak[8] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpper_LOD0/" +
                        "AirbrakeUpper_LOD0").gameObject;
                planePartsBreak[9] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLower_LOD0/" +
                        "AirbrakeLower_LOD0").gameObject;
                planePartsBreak[10] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                        "FlapLeft_LOD0").gameObject;
                planePartsBreak[11] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                        "FlapRight_LOD0").gameObject;
                planePartsBreak[12] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                        "GearDoorFront1_LOD0").gameObject;
                planePartsBreak[13] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                        "GearDoorFront2_LOD0").gameObject;
                planePartsBreak[14] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront3_LOD0/" +
                        "GearDoorFront3_LOD0").gameObject;
                planePartsBreak[15] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft1_LOD0/" +
                        "GearDoorLeft1_LOD0").gameObject;
                planePartsBreak[16] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft2_LOD0/" +
                        "GearDoorLeft2_LOD0").gameObject;
                planePartsBreak[17] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft3_LOD0/" +
                        "GearDoorLeft3_LOD0").gameObject;
                planePartsBreak[18] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight1_LOD0/" +
                        "GearDoorRight1_LOD0").gameObject;
                planePartsBreak[19] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight2_LOD0/" +
                        "GearDoorRight2_LOD0").gameObject;
                planePartsBreak[20] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight3_LOD0/" +
                        "GearDoorRight3_LOD0").gameObject;
                planePartsBreak[21] = transform.Find(
                        "Hull_LOD0/TailLeft_LOD0/_AxisRudderLeft_LOD0/" +
                        "RudderLeft_LOD0").gameObject;
                planePartsBreak[22] = transform.Find(
                        "Hull_LOD0/TailRight_LOD0/_AxisRudderRight_LOD0/" +
                        "RudderRight_LOD0").gameObject;
                planePartsBreak[23] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft_LOD0/" +
                        "SlatLeft_LOD0").gameObject;
                planePartsBreak[24] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisSlatRight_LOD0/" +
                        "SlatRight_LOD0").gameObject;
                //LOD1
                planePartsBreak[25] = transform.Find(
                    "Hull_LOD1/StabilatorLeft_LOD1").gameObject;
                planePartsBreak[26] = transform.Find(
                    "Hull_LOD1/StabilatorRight_LOD1").gameObject;
                planePartsBreak[27] = transform.Find(
                    "Hull_LOD1/TailLeft_LOD1").gameObject;
                planePartsBreak[28] = transform.Find(
                    "Hull_LOD1/TailRight_LOD1").gameObject;
                planePartsBreak[29] = transform.Find(
                    "Hull_LOD1/WingLeft_LOD1").gameObject;
                planePartsBreak[30] = transform.Find(
                    "Hull_LOD1/WingRight_LOD1").gameObject;

                //Groups that are simply deleted during destruction
                planePartsDestroy = new GameObject[] {
                    //LOD0
                    transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisAileronLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisAileronRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLower_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpper_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisFlapLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisFlapRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront3_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft3_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight3_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/TailLeft_LOD0/" +
                        "_AxisRudderLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/TailRight_LOD0/" +
                        "_AxisRudderRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisSlatLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisSlatRight_LOD0").gameObject,
                    /*transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0/" +
                        "CanopyFrameGlass_LOD0").gameObject,*/
                    /*transform.Find(
                        "Hull_LOD0/CanopyGlass_LOD0").gameObject,*/
                    transform.Find(
                        "Hull_LOD0/FanLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/FanRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Flame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutFront_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutRight_LOD0").gameObject,
                    //LOD1
                    transform.Find(
                        "Hull_LOD1/_AxisCanopyFrame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Flame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Misc_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearStruts_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoors_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject,
                    //LOD2
                    transform.Find(
                        "Hull_LOD2/Flame_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/Misc_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/MiscGears_LOD2").gameObject };

                //New axises
                //None needed

                //Rotate landing struts/gears in closed position
                planePartsLanding[0].transform.parent.Rotate(85.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[2].transform.parent.Rotate(-65.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[4].transform.parent.Rotate(-65.0f, 0.0f, 0.0f, Space.Self);

                //Hide landing struts/gears
                //LOD0
                planePartsLanding[0].transform.parent.gameObject.SetActive(false);
                planePartsLanding[1].SetActive(false);
                planePartsLanding[2].transform.parent.gameObject.SetActive(false);
                planePartsLanding[3].SetActive(false);
                planePartsLanding[4].transform.parent.gameObject.SetActive(false);
                planePartsLanding[5].SetActive(false);
                //LOD1
                planePartsLanding[15].SetActive(false);
                planePartsLanding[17].SetActive(false);
                //LOD2
                planePartsLanding[18].SetActive(false);

                break;
            case PlaneName.F4E:
                pName = "F-4E";
                hitpoints = 10.0f;

                rig.mass = 14000.0f;

                //Groups associated with landing
                planePartsLanding = new GameObject[13];
                //LOD0
                //Struts
                planePartsLanding[0] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                    "GearStrutFront_LOD0").gameObject;
                planePartsLanding[1] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearStrutLeft_LOD0/" +
                    "GearStrutLeft_LOD0").gameObject;
                planePartsLanding[2] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearStrutRight_LOD0/" +
                    "GearStrutRight_LOD0").gameObject;
                //Doors
                planePartsLanding[3] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                    "GearDoorFront1_LOD0").gameObject;
                planePartsLanding[4] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                    "GearDoorFront2_LOD0").gameObject;
                planePartsLanding[5] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft1_LOD0/" +
                    "GearDoorLeft1_LOD0").gameObject;
                planePartsLanding[6] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft2_LOD0/" +
                    "GearDoorLeft2_LOD0").gameObject;
                planePartsLanding[7] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight1_LOD0/" +
                    "GearDoorRight1_LOD0").gameObject;
                planePartsLanding[8] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight2_LOD0/" +
                    "GearDoorRight2_LOD0").gameObject;
                //LOD1
                //Misc
                planePartsLanding[9] =
                    transform.Find("Hull_LOD1/MiscGearStruts_LOD1").gameObject;
                planePartsLanding[10] =
                    transform.Find("Hull_LOD1/MiscGearDoors_LOD1").gameObject;
                planePartsLanding[11] =
                    transform.Find("Hull_LOD1/MiscGearDoorsOpen_LOD1").
                    gameObject;
                //LOD2
                //Misc
                planePartsLanding[12] =
                    transform.Find("Hull_LOD2/MiscGears_LOD2").gameObject;

                //Groups for pilot ejection
                planePartsEject = new GameObject[11];
                //Pilot
                //LOD0
                planePartsEject[0] = transform.Find(
                    "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").
                    gameObject;
                planePartsEject[1] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0").gameObject;
                planePartsEject[2] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0/Pilot_LOD0").gameObject;
                //LOD1
                planePartsEject[3] = transform.Find(
                    "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").
                    gameObject;
                planePartsEject[4] = transform.Find(
                    "Hull_LOD1/PilotSeat_LOD1").gameObject;
                //LOD2    
                planePartsEject[5] = transform.Find(
                    "Hull_LOD2/MiscCanopy_LOD2").gameObject;
                //Pilot Rear
                //LOD0
                planePartsEject[6] = transform.Find(
                    "Hull_LOD0/_AxisCanopyFrameRear_LOD0/CanopyFrameRear_LOD0").
                    gameObject;
                planePartsEject[7] = transform.Find(
                    "Hull_LOD0/PilotSeatRear_LOD0").gameObject;
                planePartsEject[8] = transform.Find(
                    "Hull_LOD0/PilotSeatRear_LOD0/PilotRear_LOD0").gameObject;
                //LOD1
                planePartsEject[9] = transform.Find(
                    "Hull_LOD1/_AxisCanopyFrameRear_LOD1/CanopyFrameRear_LOD1").
                    gameObject;
                planePartsEject[10] = transform.Find(
                    "Hull_LOD1/PilotSeatRear_LOD1").gameObject;

                //Groups that get torn off during destruction
                planePartsBreak = new GameObject[44];
                //LOD0
                planePartsBreak[0] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                    "StabilatorLeft_LOD0").gameObject;
                planePartsBreak[1] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                    "StabilatorRight_LOD0").gameObject;
                planePartsBreak[2] = transform.Find(
                    "Hull_LOD0/Tail_LOD0").gameObject;
                planePartsBreak[3] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0").gameObject;
                planePartsBreak[4] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0").gameObject;
                planePartsBreak[5] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                        "AileronLeft_LOD0").gameObject;
                planePartsBreak[6] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                        "AileronRight_LOD0").gameObject;
                planePartsBreak[7] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisAirbrakeLeft_LOD0/" +
                        "AirbrakeLeft_LOD0").gameObject;
                planePartsBreak[8] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisAirbrakeRight_LOD0/" +
                        "AirbrakeRight_LOD0").gameObject;
                planePartsBreak[9] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                        "FlapLeft_LOD0").gameObject;
                planePartsBreak[10] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                        "FlapRight_LOD0").gameObject;
                planePartsBreak[11] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft1_LOD0/" +
                        "SlatLeft1_LOD0").gameObject;
                planePartsBreak[12] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft2_LOD0/" +
                        "SlatLeft2_LOD0").gameObject;
                planePartsBreak[13] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisSlatRight1_LOD0/" +
                        "SlatRight1_LOD0").gameObject;
                planePartsBreak[14] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisSlatRight2_LOD0/" +
                        "SlatRight2_LOD0").gameObject;
                planePartsBreak[15] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0/" +
                        "GearDoorFront1_LOD0").gameObject;
                planePartsBreak[16] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0/" +
                        "GearDoorFront2_LOD0").gameObject;
                planePartsBreak[17] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft1_LOD0/" +
                        "GearDoorLeft1_LOD0").gameObject;
                planePartsBreak[18] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisGearDoorLeft2_LOD0/" +
                        "GearDoorLeft2_LOD0").gameObject;
                planePartsBreak[19] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight1_LOD0/" +
                        "GearDoorRight1_LOD0").gameObject;
                planePartsBreak[20] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisGearDoorRight2_LOD0/" +
                        "GearDoorRight2_LOD0").gameObject;
                planePartsBreak[21] = transform.Find(
                        "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/" +
                        "Rudder_LOD0").gameObject;
                //LOD1
                planePartsBreak[22] = transform.Find(
                    "Hull_LOD1/StabilatorLeft_LOD1").gameObject;
                planePartsBreak[23] = transform.Find(
                    "Hull_LOD1/StabilatorRight_LOD1").gameObject;
                planePartsBreak[24] = transform.Find(
                    "Hull_LOD1/Tail_LOD1").gameObject;
                planePartsBreak[25] = transform.Find(
                    "Hull_LOD1/WingLeft_LOD1").gameObject;
                planePartsBreak[26] = transform.Find(
                    "Hull_LOD1/WingRight_LOD1").gameObject;

                //Groups that are simply deleted during destruction
                planePartsDestroy = new GameObject[] {
                    //LOD0
                    transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisCanopyFrameRear_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisAileronLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisAileronRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisAirbrakeLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisAirbrakeRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisFlapLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisFlapRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisSlatLeft1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisSlatLeft2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisSlatRight1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisSlatRight2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisGearDoorLeft1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisGearDoorLeft2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisGearDoorRight1_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisGearDoorRight2_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Tail_LOD0/" +
                        "_AxisRudder_LOD0").gameObject,
                    /*transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0/" +
                        "CanopyFrameGlass_LOD0").gameObject,*/
                    /*transform.Find(
                        "Hull_LOD0/_AxisCanopyFrameRear_LOD0/" +
                        "CanopyFrameRear_LOD0/CanopyFrameGlassRear_LOD0").gameObject,*/
                    /*transform.Find(
                        "Hull_LOD0/CanopyGlass_LOD0").gameObject,*/
                    transform.Find(
                        "Hull_LOD0/FanLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/FanRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Flame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutFront_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisGearStrutLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisGearStrutRight_LOD0").gameObject,
                    //LOD1
                    transform.Find(
                        "Hull_LOD1/_AxisCanopyFrame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/_AxisCanopyFrameRear_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Flame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Misc_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearStruts_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoors_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject,
                    //LOD2
                    transform.Find(
                        "Hull_LOD2/Flame_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/Misc_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/MiscGears_LOD2").gameObject };

                //New axises
                CreateChildAxis(
                    "_AxisGearDoorLeft2B_LOD0", planePartsLanding[6]);
                CreateChildAxis(
                    "_AxisGearDoorRight2B_LOD0", planePartsLanding[8]);
                CreateChildAxis("_AxisSlatLeft2B_LOD0", planePartsBreak[12]);
                CreateChildAxis("_AxisSlatRight2B_LOD0", planePartsBreak[14]);

                //Rotate landing struts/gears in closed position
                planePartsLanding[0].transform.parent.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[1].transform.parent.Rotate(0.0f, 0.0f, 60.0f, Space.Self);
                planePartsLanding[2].transform.parent.Rotate(0.0f, 0.0f, -60.0f, Space.Self);

                //Hide landing struts/gears
                //LOD0
                planePartsLanding[0].transform.parent.gameObject.SetActive(false);
                planePartsLanding[1].transform.parent.gameObject.SetActive(false);
                planePartsLanding[2].transform.parent.gameObject.SetActive(false);
                //LOD1
                planePartsLanding[9].SetActive(false);
                planePartsLanding[11].SetActive(false);
                //LOD2
                planePartsLanding[12].SetActive(false);

                break;
            case PlaneName.F16:
                pName = "F-16";
                hitpoints = 9.0f;

                rig.mass = 10000.0f;

                //Groups associated with landing
                planePartsLanding = new GameObject[11];
                //LOD0
                //Struts
                planePartsLanding[0] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                    "GearStrutFront_LOD0").gameObject;
                planePartsLanding[1] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutFront_LOD0/GearStrutFront_LOD0/" +
                    "GearStrutFrontPart_LOD0").gameObject;
                planePartsLanding[2] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutLeft_LOD0/" +
                    "GearStrutLeft_LOD0").gameObject;
                planePartsLanding[3] = transform.Find(
                    "Hull_LOD0/_AxisGearStrutRight_LOD0/" +
                    "GearStrutRight_LOD0").gameObject;
                //Doors
                planePartsLanding[4] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorFront_LOD0/" +
                    "GearDoorFront_LOD0").gameObject;
                planePartsLanding[5] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorLeft_LOD0/" +
                    "GearDoorLeft_LOD0").gameObject;
                planePartsLanding[6] = transform.Find(
                    "Hull_LOD0/_AxisGearDoorRight_LOD0/" +
                    "GearDoorRight_LOD0").gameObject;
                //LOD1
                //Misc
                planePartsLanding[7] =
                    transform.Find("Hull_LOD1/MiscGearStruts_LOD1").gameObject;
                planePartsLanding[8] =
                    transform.Find("Hull_LOD1/MiscGearDoors_LOD1").gameObject;
                planePartsLanding[9] =
                    transform.Find("Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject;
                //LOD2
                //Misc
                planePartsLanding[10] =
                    transform.Find("Hull_LOD2/MiscGears_LOD2").gameObject;

                //Groups for pilot ejection
                planePartsEject = new GameObject[6];
                //Pilot
                //LOD0
                planePartsEject[0] = transform.Find(
                    "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").
                    gameObject;
                planePartsEject[1] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0").gameObject;
                planePartsEject[2] = transform.Find(
                    "Hull_LOD0/PilotSeat_LOD0/Pilot_LOD0").gameObject;
                //LOD1
                planePartsEject[3] = transform.Find(
                    "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").
                    gameObject;
                planePartsEject[4] = transform.Find(
                    "Hull_LOD1/PilotSeat_LOD1").gameObject;
                //LOD2    
                planePartsEject[5] = transform.Find(
                    "Hull_LOD2/MiscCanopy_LOD2").gameObject;

                //Groups that get torn off during destruction
                planePartsBreak = new GameObject[38];
                //LOD0
                planePartsBreak[0] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                    "StabilatorLeft_LOD0").gameObject;
                planePartsBreak[1] = transform.Find(
                    "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                    "StabilatorRight_LOD0").gameObject;
                planePartsBreak[2] = transform.Find(
                    "Hull_LOD0/Tail_LOD0").gameObject;
                planePartsBreak[3] = transform.Find(
                    "Hull_LOD0/WingLeft_LOD0").gameObject;
                planePartsBreak[4] = transform.Find(
                    "Hull_LOD0/WingRight_LOD0").gameObject;
                planePartsBreak[5] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpperLeft_LOD0/" +
                        "AirbrakeUpperLeft_LOD0").gameObject;
                planePartsBreak[6] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLowerLeft_LOD0/" +
                        "AirbrakeLowerLeft_LOD0").gameObject;
                planePartsBreak[7] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpperRight_LOD0/" +
                        "AirbrakeUpperRight_LOD0").gameObject;
                planePartsBreak[8] = transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLowerRight_LOD0/" +
                        "AirbrakeLowerRight_LOD0").gameObject;
                planePartsBreak[9] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisFlaperonLeft_LOD0/" +
                        "FlaperonLeft_LOD0").gameObject;
                planePartsBreak[10] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisFlaperonRight_LOD0/" +
                        "FlaperonRight_LOD0").gameObject;
                planePartsBreak[11] = transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft_LOD0/" +
                        "SlatLeft_LOD0").gameObject;
                planePartsBreak[12] = transform.Find(
                        "Hull_LOD0/WingRight_LOD0/_AxisSlatRight_LOD0/" +
                        "SlatRight_LOD0").gameObject;
                planePartsBreak[13] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront_LOD0/" +
                        "GearDoorFront_LOD0").gameObject;
                planePartsBreak[14] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft_LOD0/" +
                        "GearDoorLeft_LOD0").gameObject;
                planePartsBreak[15] = transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight_LOD0/" +
                        "GearDoorRight_LOD0").gameObject;
                planePartsBreak[16] = transform.Find(
                        "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/" +
                        "Rudder_LOD0").gameObject;
                //LOD1
                planePartsBreak[17] = transform.Find(
                    "Hull_LOD1/StabilatorLeft_LOD1").gameObject;
                planePartsBreak[18] = transform.Find(
                    "Hull_LOD1/StabilatorRight_LOD1").gameObject;
                planePartsBreak[19] = transform.Find(
                    "Hull_LOD1/Tail_LOD1").gameObject;
                planePartsBreak[20] = transform.Find(
                    "Hull_LOD1/WingLeft_LOD1").gameObject;
                planePartsBreak[21] = transform.Find(
                    "Hull_LOD1/WingRight_LOD1").gameObject;

                //Groups that are simply deleted during destruction
                planePartsDestroy = new GameObject[] {
                    //LOD0
                    transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisStabilatorRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisFlaperonLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisFlaperonRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingLeft_LOD0/" +
                        "_AxisSlatLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/WingRight_LOD0/" +
                        "_AxisSlatRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpperLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeUpperRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLowerLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisAirbrakeLowerRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorFront_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearDoorRight_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Tail_LOD0/" +
                        "_AxisRudder_LOD0").gameObject,
                    /*transform.Find(
                        "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0/" +
                        "CanopyFrameGlass_LOD0").gameObject,*/
                    /*transform.Find(
                        "Hull_LOD0/CanopyGlass_LOD0").gameObject,*/
                    transform.Find(
                        "Hull_LOD0/Fan_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/Flame_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutFront_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutLeft_LOD0").gameObject,
                    transform.Find(
                        "Hull_LOD0/_AxisGearStrutRight_LOD0").gameObject,
                    //LOD1
                    transform.Find(
                        "Hull_LOD1/_AxisCanopyFrame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Flame_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/Misc_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearStruts_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoors_LOD1").gameObject,
                    transform.Find(
                        "Hull_LOD1/MiscGearDoorsOpen_LOD1").gameObject,
                    //LOD2
                    transform.Find(
                        "Hull_LOD2/Flame_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/Misc_LOD2").gameObject,
                    transform.Find(
                        "Hull_LOD2/MiscGears_LOD2").gameObject };

                //New axises
                //None needed

                //Rotate landing struts/gears in closed position
                planePartsLanding[0].transform.parent.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[2].transform.parent.Rotate(-65.0f, 0.0f, 0.0f, Space.Self);
                planePartsLanding[3].transform.parent.Rotate(-65.0f, 0.0f, 0.0f, Space.Self);

                //Hide landing struts/gears/LOD1 open doors
                //LOD0
                planePartsLanding[0].transform.parent.gameObject.SetActive(false);
                planePartsLanding[1].SetActive(false);
                planePartsLanding[2].transform.parent.gameObject.SetActive(false);
                planePartsLanding[3].transform.parent.gameObject.SetActive(false);
                //LOD1
                planePartsLanding[7].SetActive(false);
                planePartsLanding[9].SetActive(false);
                //LOD2
                planePartsLanding[10].SetActive(false);

                break;
        }

        hp = hitpoints;

        planeActive = false;
        planeDamageCritical = false;

        //Turn off exhaust flames, exhaust trails and weapons
        for (int i = 1; i < planeExhaust.Length; i++)
            planeExhaust[i].SetActive(false);
        trails[0].SetActive(false);
        weapons[0].gameObject.SetActive(false);
    }

    void Start() {
    }

    void Update() {
        //If plane is still in working condition...
        if (!planeDamageCritical) {
            //If hitpoints are depleted...
            if (hp <= 0.0f) {
                //...and number of HP is higher than -100...
                if (hp > -100)
                    //...destroy plane...
                    Terminate(false);
                //...otherwise...
                else
                    //...utterly destroy plane...
                    Terminate(true);

                //...and mark this plane shotdown.
                planeDamageCritical = true;
            }
        }
    }

    //Terminate plane
    public void Terminate(bool criticalHit) {
        planeActive = false;

        //Old code for adding burnt-metal texture over standard texture on
        //destroyed jet
        /*foreach(Renderer rend in rends)
        {
            if(rend != null)
            {
                Texture burnMetalTex =
                    Resources.Load<Texture>("Textures/DetailX2/BurnMetalTex");
                Texture burnMetalNm =
                    Resources.Load<Texture>("Textures/DetailX2/BurnMetalNm");

                rend.material.EnableKeyword ("_DETAIL_MULX2");
                rend.material.EnableKeyword ("_NORMALMAP");

                rend.material.SetTexture("_DetailAlbedoMap", burnMetalTex);
                rend.material.SetTexture("_DetailNormalMap", burnMetalNm);
                //rend.material.mainTexture = burnMetalTex;
            }
        }*/

        planeWeapons.SetActive(false);
        for (int i = 1; i < planeExhaust.Length; i++)
            planeExhaust[i].SetActive(false);
        planeTrails.SetActive(false);

        int d2 = Random.Range(0, 2);
        string type = d2 == 0 ? "light" : "hard";
        if (criticalHit)
            type = "hard";

        Destruction(type);
    }

    //Eject pilot from the jet
    void EjectPilot(int chance, bool secondPilot) {
        int a0 = 0;
        int a1 = 1; //int a2 = 2;
        int a3 = 3;
        int a4 = 4;
        int a5 = 5;

        int percent = Random.Range(0, 100);
        if (percent >= chance)
            return;

        if (secondPilot == true) {
            a0 = 6;
            a1 = 7; //a2 = 8;
            a3 = 9;
            a4 = 10;
        }

        //Canopy Frame
        if (planePartsEject[a0] != null) {
            planePartsEject[a0].transform.parent = null;

            Rigidbody rigPart = planePartsEject[a0].AddComponent<Rigidbody>();
            rigPart.mass = 10.0f;
            rigPart.useGravity = true;

            rigPart.AddTorque(Vector3.one * rigPart.mass * 50.0f);
            rigPart.AddExplosionForce(
                rigPart.mass * 25.0f,
                rig.transform.position,
                10.0f,
                100.0f,
                ForceMode.Impulse);

            Destroy(planePartsEject[a0], 4.0f);
        }
        if (planePartsEject[a3] != null) {
            planePartsEject[a3].transform.parent = null;

            Rigidbody rigPart = planePartsEject[a3].AddComponent<Rigidbody>();
            rigPart.mass = 10.0f;
            rigPart.useGravity = true;

            rigPart.AddTorque(Vector3.one * rigPart.mass * 50.0f);
            rigPart.AddExplosionForce(
                rigPart.mass * 25.0f,
                rig.transform.position,
                10.0f,
                100.0f,
                ForceMode.Impulse);

            Destroy(planePartsEject[a3], 4.0f);
        }
        if (planePartsEject[a5] != null)
            Destroy(planePartsEject[a5]);

        //Pilot Seat
        if (planePartsEject[a1] != null) {
            planePartsEject[a1].transform.parent = null;

            Rigidbody rigPart = planePartsEject[a1].AddComponent<Rigidbody>();
            rigPart.mass = 10.0f;
            rigPart.useGravity = true;

            rigPart.velocity = transform.up * 40.0f;

            Destroy(planePartsEject[a1], 5.0f);
        }
        if (planePartsEject[a4] != null) {
            planePartsEject[a4].transform.parent = null;

            Rigidbody rigPart = planePartsEject[a4].AddComponent<Rigidbody>();
            rigPart.mass = 10.0f;
            rigPart.useGravity = true;

            rigPart.velocity = transform.up * 40.0f;

            Destroy(planePartsEject[a4], 5.0f);
        }

        //Pilot
        //if(planePartsEject[a2] != null)
        //Destroy(planePartsEject[a2]);
    }

    //Controls all jet parts meant to be torn off once jet is terminated
    void PartsBreakOff(int chance) {
        for (int i = 0; i < planePartsBreak.Length / 2; i++) {
            GameObject part0 = planePartsBreak[i];
            GameObject part1 = planePartsBreak[i + planePartsBreak.Length / 2];
            int percent = Random.Range(0, 100);
            float forceFact = Random.Range(0.6f, 1.4f);
            if (part0 != null &&
               part0.transform.parent != null &&
               chance > percent) {
                part0.transform.parent = null;

                Rigidbody rigPart0 = part0.AddComponent<Rigidbody>();
                rigPart0.mass = 10.0f;
                rigPart0.useGravity = true;

                rigPart0.AddTorque(
                    Vector3.one * rigPart0.mass * 100.0f * forceFact);
                rigPart0.AddExplosionForce(
                    rigPart0.mass * 30.0f * forceFact,
                    rig.transform.position,
                    100.0f,
                    1.0f * forceFact,
                    ForceMode.Impulse);

                Destroy(part0, 3.0f * forceFact);
            }
            if (part1 != null &&
               part1.transform.parent != null &&
               chance > percent) {
                part1.transform.parent = null;

                Rigidbody rigPart1 = part1.AddComponent<Rigidbody>();
                rigPart1.mass = 10.0f;
                rigPart1.useGravity = true;

                rigPart1.AddTorque(
                    Vector3.one * rigPart1.mass * 100.0f * forceFact);
                rigPart1.AddExplosionForce(
                    rigPart1.mass * 30.0f * forceFact,
                    rig.transform.position,
                    100.0f,
                    1.0f * forceFact,
                    ForceMode.Impulse);

                Destroy(part1, 3.0f * forceFact);
            }
        }
    }

    //This function simply removes all jet parts that should just vanish upon
    //jet's destruction
    void PartsRemove() {
        foreach (GameObject part in planePartsDestroy) {
            if (part != null)
                Destroy(part);
        }
    }

    //Triggers the coroutine that controls overall jet's destruction 
    public void Destruction(string type) {
        StartCoroutine(DestructionCoo(type));
    }
    IEnumerator DestructionCoo(string type) {
        int d2 = Random.Range(0, 2);
        int dir = d2 == 0 ? -1 : 1;

        EjectPilot(100, false);
        yield return new WaitForSeconds(0.5f);
        if (planeName == PlaneName.F4E)
            EjectPilot(100, true);
        yield return new WaitForSeconds(0.5f);

        if (type == "hard") {
            Instantiate(explosion, transform.position, Quaternion.identity);

            PartsBreakOff(25);

            yield return new WaitForSeconds(0.33f);

            PartsBreakOff(100);

            rig.AddTorque(dir * transform.forward * rig.mass * 1000.0f,
                ForceMode.Impulse);
        } else {
            PartsBreakOff(33);

            rig.AddTorque(transform.right * 5.0f, ForceMode.Acceleration);
            if (d2 == 1)
                rig.AddTorque(dir * transform.forward * rig.mass,
                    ForceMode.Acceleration);
        }

        PartsRemove();

        yield return null;
    }





    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////
    //                         ANIMATION OF THE JET                           //
    ////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////





    //Enable gun fire
    public void FireGunStart() {
        if (!planeDamageCritical)
            weapons[0].gameObject.SetActive(true);
    }
    public void FireGunStop() {
        if (!planeDamageCritical)
            weapons[0].gameObject.SetActive(false);
    }

    //Arm jet with missiles and launch them
    private bool planeArmingInProgress;
    private bool firstArming;
    private List<Transform> pylonHooks = new List<Transform>();
    public void ArmJetMissiles() {
        if (!planeDamageCritical && !planeArmingInProgress)
            StartCoroutine(ArmJetMissilesCoo());
    }
    IEnumerator ArmJetMissilesCoo() {
        planeArmingInProgress = true;

        bool noPylon = false;
        GameObject pylon = null;
        GameObject pylonType = null;
        Vector3 pylonOffset = Vector3.zero;
        List<GameObject> pylons = new List<GameObject>();
        //Spawn and attach various pylons to the jet but only first time
        if (firstArming == false) {
            for (int i = 1; i < weapons.Count; i++) {
                if (!planeDamageCritical) {
                    if (planeName == PlaneName.Mig21) {
                        if (weapons[i].name == "_PylonMiddle0") {
                            pylonOffset = new Vector3(0.0f, -0.122f, 0.0f);
                            pylonType = planePylons[0];
                        } else if (weapons[i].name == "_PylonLeft1" ||
                                  weapons[i].name == "_PylonLeft2" ||
                                  weapons[i].name == "_PylonRight1" ||
                                  weapons[i].name == "_PylonRight2") {
                            pylonOffset = new Vector3(0.0f, -0.130f, 0.0f);
                            pylonType = planePylons[2];
                        } else
                            continue;
                    } else if (planeName == PlaneName.Mig29) {
                        if (weapons[i].name == "_PylonLeft1" ||
                        weapons[i].name == "_PylonRight1") {
                            pylonOffset = new Vector3(0.0f, -0.210f, 0.560f);
                            pylonType = planePylons[1];
                        } else if (weapons[i].name == "_PylonLeft2" ||
                                  weapons[i].name == "_PylonRight2") {
                            pylonOffset = new Vector3(0.0f, -0.210f, 0.432f);
                            pylonType = planePylons[1];
                        } else if (weapons[i].name == "_PylonLeft3" ||
                                  weapons[i].name == "_PylonRight3") {
                            pylonOffset = new Vector3(0.0f, -0.210f, 0.190f);
                            pylonType = planePylons[1];
                        } else
                            continue;
                    } else if (planeName == PlaneName.F4E) {
                        if (weapons[i].name == "_PylonMiddle1") {
                            pylonOffset = new Vector3(0.0f, -0.130f, 0.0f);
                            pylonType = planePylons[0];
                        } else if (weapons[i].name == "_PylonMiddle2" ||
                                  weapons[i].name == "_PylonMiddle3" ||
                                  weapons[i].name == "_PylonMiddle4" ||
                                  weapons[i].name == "_PylonMiddle5") {
                            pylonOffset = new Vector3(0.0f, 0.0f, 0.0f);
                            pylonType = null;
                            noPylon = true;
                        } else if (weapons[i].name == "_PylonLeft1" ||
                                  weapons[i].name == "_PylonRight1") {
                            pylonOffset = new Vector3(0.0f, -0.250f, 0.0f);
                            pylonType = planePylons[3];
                        } else if (weapons[i].name == "_PylonLeft2" ||
                                  weapons[i].name == "_PylonRight2") {
                            pylonOffset = new Vector3(0.0f, -0.210f, 0.0f);
                            pylonType = planePylons[2];
                        } else
                            continue;
                    } else if (planeName == PlaneName.F16) {
                        if (weapons[i].name == "_PylonMiddle0") {
                            pylonOffset = new Vector3(0.0f, -0.115f, 0.0f);
                            pylonType = planePylons[0];
                        } else if (weapons[i].name == "_PylonLeft1" ||
                                  weapons[i].name == "_PylonRight1") {
                            pylonOffset = new Vector3(0.0f, -0.170f, 0.230f);
                            pylonType = planePylons[1];
                        } else if (weapons[i].name == "_PylonLeft2" ||
                                  weapons[i].name == "_PylonLeft3" ||
                                  weapons[i].name == "_PylonRight2" ||
                                  weapons[i].name == "_PylonRight3") {
                            pylonOffset = new Vector3(0.0f, -0.260f, 0.380f);
                            pylonType = planePylons[2];
                        } else if (weapons[i].name == "_PylonLeft4" ||
                                  weapons[i].name == "_PylonRight4") {
                            pylonOffset = new Vector3(0.0f, 0.0f, 0.0f);
                            pylonType = null;
                            noPylon = true;
                        } else
                            continue;
                    } else
                        continue;


                    if (!noPylon) {
                        pylon = Instantiate(
                            pylonType,
                            weapons[i].position,
                            weapons[i].rotation,
                            weapons[i]);
                        pylon.transform.Translate(pylonOffset, Space.Self);
                        pylons.Add(pylon);

                        //Add LOD group to pylon
                        LODGroup pLod = pylon.AddComponent<LODGroup>();
                        LOD[] pLods = new LOD[1];
                        Renderer[] pRends = new Renderer[1];
                        pRends[0] = pylon.GetComponent<MeshRenderer>();
                        pLods[0] = new LOD(1f / 20f, pRends);
                        pLod.SetLODs(pLods);
                        pLod.RecalculateBounds();

                        //Combine all pylon meshes into one mesh,
                        //performance optimization
                        /*Transform[] currentHooks =
                            new Transform[pylon.transform.childCount];*/
                        for (int j = 0; j < pylon.transform.childCount; j++) {
                            pylonHooks.Add(pylon.transform.GetChild(j));

                            //Combine all pylon meshes into one mesh,
                            //performance optimization
                            //currentHooks[j] = pylon.transform.GetChild(j);
                        }

                        //Combine all pylon meshes into one mesh,
                        //performance optimization 
                        /*for(int j = 0; j < currentHooks.Length; j++)
                            currentHooks[j].parent = planeWeapons.transform;*/

                        yield return new WaitForSeconds(0.25f);
                    } else {
                        pylonHooks.Add(weapons[i]);

                        noPylon = false;
                    }
                }
            }

            //Combine all pylon meshes into one mesh, performance optimization.
            //Once all pylons are spawned on the jet, this code will combine
            //them into one mesh, around their "Weapon" parent game object.
            //This will reduce number of batches in use.
            /*MeshFilter pwm = planeWeapons.AddComponent<MeshFilter>();
            MeshRenderer pwr = planeWeapons.AddComponent<MeshRenderer>();
            CombineInstance[] pylonCombine = new CombineInstance[pylons.Count];
            Material pylonMat = pylons[0].GetComponent<MeshRenderer>().material;

            pwm.sharedMesh = new Mesh();

            for(int p = 0; p < pylons.Count; p++)
            {
                pylonCombine[p].mesh =
                    pylons[p].transform.GetComponent<MeshFilter>().sharedMesh;
                Quaternion currentRot = transform.rotation;
                transform.rotation = Quaternion.identity;
                pylonCombine[p].transform =
                    pylons[p].transform.localToWorldMatrix;
                transform.rotation = currentRot;
            }
            pwm.mesh.CombineMeshes(pylonCombine);
            pwr.material = pylonMat;
            foreach(GameObject py in pylons)
                Destroy(py);

            LODGroup pwl = planeWeapons.AddComponent<LODGroup>();
            LOD[] pwLods = new LOD[1];
            Renderer[] pwRends = new Renderer[1];
            pwRends[0] = pwr;
            pwLods[0] = new LOD(1f/10f, pwRends);
            pwl.SetLODs(pwLods);
            pwl.RecalculateBounds();*/

            firstArming = true;
        }

        GameObject missile = null;
        Vector3 missileOffset = Vector3.zero;
        List<GameObject> missiles = new List<GameObject>();

        //Attach air-To-air missiles to air-to-air pylons only
        foreach (Transform pylonHook in pylonHooks) {
            if (!planeDamageCritical) {
                if (planeName == PlaneName.Mig21) {
                    if (pylonHook.name == "_P03Hook")
                        missileOffset = new Vector3(0.0f, -0.010f, 0.650f);
                    else
                        continue;
                } else if (planeName == PlaneName.Mig29) {
                    if (pylonHook.name == "_P02Hook")
                        missileOffset = new Vector3(0.0f, -0.010f, 0.100f);
                    else
                        continue;
                } else if (planeName == PlaneName.F4E) {
                    if (pylonHook.name == "_PylonMiddle2" ||
                       pylonHook.name == "_PylonMiddle3" ||
                       pylonHook.name == "_PylonMiddle4" ||
                       pylonHook.name == "_PylonMiddle5")
                        missileOffset = new Vector3(0.0f, 0.0f, 0.0f);
                    else if (pylonHook.name == "_P04Hook1")
                        missileOffset = new Vector3(0.013f, 0.0f, 0.550f);
                    else if (pylonHook.name == "_P04Hook2")
                        missileOffset = new Vector3(-0.013f, 0.0f, 0.550f);
                    else
                        continue;
                } else if (planeName == PlaneName.F16) {
                    if (pylonHook.name == "_P03Hook")
                        missileOffset = new Vector3(0.0f, -0.010f, 0.250f);
                    else if (pylonHook.name == "_PylonLeft4")
                        missileOffset = new Vector3(-0.010f, 0.0f, 0.100f);
                    else if (pylonHook.name == "_PylonRight4")
                        missileOffset = new Vector3(0.010f, 0.0f, 0.100f);
                    else
                        continue;
                }

                missile = Instantiate(
                    planeArmament[0],
                    pylonHook.position,
                    pylonHook.rotation,
                    pylonHook);
                missile.transform.Translate(
                    missileOffset,
                    Space.Self);
                missiles.Add(missile);

                yield return new WaitForSeconds(0.15f);
            }
        }

        yield return new WaitForSeconds(1.0f);

        //Launch all air-to-air missiles
        foreach (GameObject m in missiles) {
            if (!planeDamageCritical) {
                m.GetComponent<Missile>().
                    MissileLaunch();

                yield return new WaitForSeconds(0.5f);
            }
        }

        planeArmingInProgress = false;

        yield return null;
    }

    //Animate nozzle and flame
    //This is lasso-like selection of required vertices. Two vectors are needed
    //to define closed lasso.
    bool LassoSelection(
        Vector3 vert, Vector3 vecLassoVertsHigh, Vector3 vecLassoVertsLow) {
        if (vert.x > vecLassoVertsLow.x &&
           vert.y > vecLassoVertsLow.y &&
           vert.z > vecLassoVertsLow.z &&
           vert.x < vecLassoVertsHigh.x &&
           vert.y < vecLassoVertsHigh.y &&
           vert.z < vecLassoVertsHigh.z)
            return true;

        return false;
    }
    //Find and return centre of all selected vertices
    Vector3 VertsCentre(
        Mesh mesh, Vector3 vecLassoVertsHigh, Vector3 vecLassoVertsLow) {
        int meshVertsNumSelected = 0;
        int meshVertsNumAll = 0;
        Vector3 meshVertsSum = Vector3.zero;
        Vector3 meshVertsCentre = Vector3.zero;

        for (int i = 0; i < mesh.vertices.Length; i++) {
            meshVertsNumAll++;

            if (LassoSelection(mesh.vertices[i],
               vecLassoVertsHigh, vecLassoVertsLow)) {
                meshVertsNumSelected++;
                meshVertsSum += mesh.vertices[i];
            }
        }

        meshVertsCentre = meshVertsSum / meshVertsNumSelected;

        //Mesh verts info
        /*Debug.Log("All verts " + mesh.name + ": " + meshVertsNumAll);
        Debug.Log("Selected verts " + mesh.name + ": " + meshVertsNumSelected);
        Debug.Log("Centre of selection " + mesh.name + ": " + meshVertsCentre);*/

        return meshVertsCentre;
    }
    IEnumerator ScaleNozzleAndFlameCoo(
        Vector3 vecLassoVertsHigh, Vector3 vecLassoVertsLow,
        Vector3 vecLassoVertsHigh2, Vector3 vecLassoVertsLow2,
        float scaleStart, float scaleEnd, float speed, bool twinengine) {
        Mesh meshNozzle = planeExhaust[0].GetComponent<MeshFilter>().mesh;
        Mesh meshFlame0 = planeExhaust[1].GetComponent<MeshFilter>().mesh;

        Vector3[] nozzleVerts = meshNozzle.vertices;
        Vector3[] nozzleVertsStart = meshNozzle.vertices;
        Vector3[] flameVerts = meshFlame0.vertices;
        Vector3[] flameVertsStart = meshFlame0.vertices;

        Vector3 nozzleVertsCentre =
            VertsCentre(meshNozzle, vecLassoVertsHigh, vecLassoVertsLow);
        Vector3 flameVertsCentre =
            VertsCentre(meshFlame0, vecLassoVertsHigh, vecLassoVertsLow);

        //Second nozzle and flame, if enabled
        Vector3 nozzleVertsCentre2 = Vector3.zero;
        Vector3 flameVertsCentre2 = Vector3.zero;
        if (twinengine) {
            nozzleVertsCentre2 =
                VertsCentre(meshNozzle, vecLassoVertsHigh2, vecLassoVertsLow2);
            flameVertsCentre2 =
                VertsCentre(meshFlame0, vecLassoVertsHigh2, vecLassoVertsLow2);
        }

        float scale = scaleStart;

        //Frame-skip performance optimization variables
        int numFramesToSkip = 5;
        int randOffsetFrame = Random.Range(0, numFramesToSkip);
        float sumDt = 0;

        //Animation of nozzle and flame
        while (!planeDamageCritical &&
              (scale - scaleStart) * (scale - scaleEnd) <= 0.0f) {
            //Frame-skip performance optimization
            //Number of frames to skip. The more frames is skipped, the better
            //perfs but animation is more rough. It may be better for perf that
            //this vertices manipulation is done via shaders instead using this
            //coroutine.
            sumDt += Time.deltaTime;
            if (Time.frameCount % numFramesToSkip != randOffsetFrame) {
                yield return null;
                continue;
            }

            for (int i = 0; i < nozzleVerts.Length; i++) {
                if (LassoSelection(meshNozzle.vertices[i],
                    vecLassoVertsHigh, vecLassoVertsLow))
                    nozzleVerts[i] = nozzleVertsStart[i] +
                        (nozzleVertsStart[i] - nozzleVertsCentre) *
                            (scale - 1.0f);
            }
            for (int i = 0; i < flameVerts.Length; i++) {
                if (LassoSelection(meshFlame0.vertices[i],
                    vecLassoVertsHigh, vecLassoVertsLow))
                    flameVerts[i] = flameVertsStart[i] +
                        (flameVertsStart[i] - flameVertsCentre) *
                            (scale - 1.0f);
            }

            //Second nozzle and flame, if enabled
            if (twinengine) {
                for (int i = 0; i < nozzleVerts.Length; i++) {
                    if (LassoSelection(meshNozzle.vertices[i],
                        vecLassoVertsHigh2, vecLassoVertsLow2))
                        nozzleVerts[i] = nozzleVertsStart[i] +
                            (nozzleVertsStart[i] - nozzleVertsCentre2) *
                                (scale - 1.0f);
                }
                for (int i = 0; i < flameVerts.Length; i++) {
                    if (LassoSelection(meshFlame0.vertices[i],
                        vecLassoVertsHigh2, vecLassoVertsLow2))
                        flameVerts[i] = flameVertsStart[i] +
                            (flameVertsStart[i] - flameVertsCentre2) *
                                (scale - 1.0f);
                }
            }

            meshNozzle.vertices = nozzleVerts;
            //meshNozzle.RecalculateNormals();
            meshNozzle.RecalculateBounds();
            meshNozzle.RecalculateTangents();
            meshFlame0.vertices = flameVerts;
            //meshFlame0.RecalculateNormals();
            meshFlame0.RecalculateBounds();
            meshFlame0.RecalculateTangents();

            scale += Mathf.Sign(scaleEnd - scaleStart) * speed *
                //Time.deltaTime;
                sumDt; //Frame-skip performance optimization (comment line above)

            //Frame-skip performance optimization variable
            sumDt = 0.0f;

            yield return null;
        }

        yield return null;
    }

    private bool planeExhaustInProgress;
    public void ActivateJet() {
        if (!planeDamageCritical && !planeExhaustInProgress) {
            planeActive = planeActive == true ? false : true;
            StartCoroutine(ActivateJetCoo());
        }
    }
    IEnumerator ActivateJetCoo() {
        Material[] flamesRendMat = new Material[planeExhaust.Length - 1];
        for (int i = 1; i < planeExhaust.Length; i++)
            flamesRendMat[i - 1] =
                planeExhaust[i].GetComponent<Renderer>().material;
        float alpha = 0.0f;

        planeExhaustInProgress = true;

        //If plane is active, enable flames in the nozzles
        if (planeActive) {
            alpha = 0.0f;
            for (int i = 1; i < planeExhaust.Length; i++)
                planeExhaust[i].SetActive(true);
            foreach (Material flameRendMat in flamesRendMat)
                flameRendMat.color = new Color(
                    flameRendMat.color.r,
                    flameRendMat.color.g,
                    flameRendMat.color.b,
                    alpha);

            //Activate opening of nozzle(s) for appropriate jet
            if (planeName == PlaneName.Mig29)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(-0.34f, 0.42f, -5.26f),
                    new Vector3(-1.55f, -0.78f, -5.50f),
                    new Vector3(1.56f, 0.42f, -5.26f),
                    new Vector3(0.36f, -0.78f, -5.50f),
                    1.0f, 1.25f, 0.15f, true));
            else if (planeName == PlaneName.F4E)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(-0.05f, 0.32f, -4.43f),
                    new Vector3(-1.15f, -0.77f, -4.65f),
                    new Vector3(1.15f, 0.32f, -4.43f),
                    new Vector3(0.05f, -0.77f, -4.65f),
                    1.0f, 1.25f, 0.15f, true));
            else if (planeName == PlaneName.F16)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(0.6f, 0.58f, -6.0f),
                    new Vector3(-0.6f, -0.62f, -6.24f),
                    Vector3.zero, Vector3.zero,
                    1.0f, 1.3f, 0.15f, false));

            while (!planeDamageCritical && alpha <= 1.0f) {
                alpha += 0.3f * spdFac * Time.deltaTime;
                foreach (Material flameRendMat in flamesRendMat)
                    flameRendMat.color = new Color(
                        flameRendMat.color.r,
                        flameRendMat.color.g,
                        flameRendMat.color.b,
                        alpha);
                foreach (Material flameRendMat in flamesRendMat)
                    flameRendMat.color = new Color(
                        flameRendMat.color.r,
                        flameRendMat.color.g,
                        flameRendMat.color.b,
                        alpha);
                yield return null;
            }
            foreach (Material flameRendMat in flamesRendMat)
                flameRendMat.color = new Color(
                    flameRendMat.color.r,
                    flameRendMat.color.g,
                    flameRendMat.color.b,
                    1.0f);
            trails[0].SetActive(true);
        } else {
            alpha = 1.0f;
            trails[0].SetActive(false);
            foreach (Material flameRendMat in flamesRendMat)
                flameRendMat.color = new Color(
                    flameRendMat.color.r,
                    flameRendMat.color.g,
                    flameRendMat.color.b,
                    alpha);

            //Activate closing of nozzle(s) for appropriate jet
            if (planeName == PlaneName.Mig29)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(-0.36f, 0.42f, -5.26f),
                    new Vector3(-1.56f, -0.78f, -5.50f),
                    new Vector3(1.56f, 0.42f, -5.26f),
                    new Vector3(0.36f, -0.78f, -5.50f),
                    1.0f, 1.0f / 1.25f, 0.15f, true));
            else if (planeName == PlaneName.F4E)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(-0.05f, 0.32f, -4.43f),
                    new Vector3(-1.15f, -0.77f, -4.65f),
                    new Vector3(1.15f, 0.32f, -4.43f),
                    new Vector3(0.05f, -0.77f, -4.65f),
                    1.0f, 1.0f / 1.25f, 0.15f, true));
            else if (planeName == PlaneName.F16)
                StartCoroutine(ScaleNozzleAndFlameCoo(
                    new Vector3(0.6f, 0.58f, -6.0f),
                    new Vector3(-0.6f, -0.62f, -6.24f),
                    Vector3.zero, Vector3.zero,
                    1.0f, 1.0f / 1.3f, 0.15f, false));

            while (!planeDamageCritical && alpha > 0.0f) {
                alpha -= 0.3f * spdFac * Time.deltaTime;
                foreach (Material flameRendMat in flamesRendMat)
                    flameRendMat.color = new Color(
                        flameRendMat.color.r,
                        flameRendMat.color.g,
                        flameRendMat.color.b,
                        alpha);
                yield return null;
            }
            foreach (Material flameRendMat in flamesRendMat)
                flameRendMat.color = new Color(
                    flameRendMat.color.r,
                    flameRendMat.color.g,
                    flameRendMat.color.b,
                    0.0f);
            for (int i = 1; i < planeExhaust.Length; i++)
                planeExhaust[i].SetActive(false);
        }

        planeExhaustInProgress = false;

        yield return null;
    }

    //Landing part animation type 1. Rotate jet piece involved in jet's
    //landing system.
    IEnumerator AnimatePartLandingType1(
        GameObject obj, GameObject[] specialObjs,
        Vector3 axis, float angle, float animTime,
        bool hide) {
        GameObject objParent = obj.transform.parent.gameObject;

        if (hide && specialObjs != null)
            foreach (GameObject specialObj in specialObjs)
                specialObj.SetActive(false);

        objParent.SetActive(true);

        float time = 0.0f;
        while (!planeDamageCritical && time < animTime) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime > animTime)
                deltaTime = animTime - time;
            float speed = angle / animTime;
            float delta = speed * spdFac * deltaTime;

            objParent.transform.Rotate(
                axis,
                delta,
                Space.Self);
            time += deltaTime;

            yield return null;
        }

        if (!planeDamageCritical) {
            if (hide)
                objParent.SetActive(false);
            else if (specialObjs != null)
                foreach (GameObject specialObj in specialObjs)
                    specialObj.SetActive(true);
        }

        yield return null;
    }
    //Landing part animation type 2. Complex animation. Double rotation of the
    //piece. Used for GearDoorLeft2/Right2 of F-4E jet.
    IEnumerator AnimatePartLandingType2(
        GameObject obj, GameObject[] specialObjs,
        Vector3 axisParentRot, float angleParentRot,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime, bool hide) {
        GameObject objParent = obj.transform.parent.gameObject;
        GameObject objParent2 = obj.transform.parent.parent.gameObject;

        if (hide && specialObjs != null)
            foreach (GameObject specialObj in specialObjs)
                specialObj.SetActive(false);

        objParent2.SetActive(true);

        float time = 0.0f;
        while (!planeDamageCritical && time < animTime) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime > animTime)
                deltaTime = animTime - time;

            float speedParentRot = angleParentRot / animTime;
            float speedLocalRot = angleLocalRot / animTime;

            float deltaParentRot = speedParentRot * spdFac * deltaTime;
            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent2.transform.Rotate(
                axisParentRot,
                deltaParentRot,
                Space.Self);

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            time += deltaTime;

            yield return null;
        }

        if (!planeDamageCritical) {
            if (hide)
                objParent2.SetActive(false);
            else if (specialObjs != null)
                foreach (GameObject specialObj in specialObjs)
                    specialObj.SetActive(true);
        }

        yield return null;
    }
    //Landing part animation type 3. Complex animation. Translation and
    //rotation of the piece. Used for GearDoorLeft2/Right2 and
    //GearStrutLeft/Right of MiG-21 jet.
    IEnumerator AnimatePartLandingType3(
        GameObject obj, GameObject[] specialObjs,
        Vector3 axisParentTrans, float lengthParentTrans,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime, bool hide) {
        GameObject objParent = obj.transform.parent.gameObject;
        GameObject objParent2 = obj.transform.parent.parent.gameObject;

        if (hide && specialObjs != null)
            foreach (GameObject specialObj in specialObjs)
                specialObj.SetActive(false);

        objParent2.SetActive(true);

        float time = 0.0f;
        while (!planeDamageCritical && time < animTime) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime > animTime)
                deltaTime = animTime - time;

            float speedParentTrans = lengthParentTrans / animTime;
            float speedLocalRot = angleLocalRot / animTime;

            float deltaParentTrans = speedParentTrans * spdFac * deltaTime;
            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent2.transform.Translate(
                axisParentTrans * deltaParentTrans,
                Space.Self);

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            time += deltaTime;

            yield return null;
        }

        if (!planeDamageCritical) {
            if (hide)
                objParent2.SetActive(false);
            else if (specialObjs != null)
                foreach (GameObject specialObj in specialObjs)
                    specialObj.SetActive(true);
        }

        yield return null;
    }

    //Animate landing gears
    private bool planeLanding, planeLandingInProgress;
    public void AnimateJetLanding() {
        if (!planeDamageCritical && !planeLandingInProgress)
            StartCoroutine(AnimateJetLandingCoo());
    }
    IEnumerator AnimateJetLandingCoo() {
        planeLandingInProgress = true;

        planeLanding = planeLanding == false ? true : false;

        if (planeName == PlaneName.Mig21) {
            if (planeLanding) {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, 110f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[9], null,
                    Vector3.forward, -110f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5], null,
                    Vector3.forward, -80f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6], null,
                    Vector3.forward, 80f, 2.5f,
                    false));

                yield return new WaitForSeconds(1.4f);

                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[8], null,
                    Vector3.right, -0.4f,
                    Vector3.forward, -95f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[10], null,
                    Vector3.right, 0.4f,
                    Vector3.forward, 95f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.6f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0], null,
                    Vector3.right, 90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[1],
                    new GameObject[] { planePartsLanding[2] },
                    Vector3.right, -0.3f,
                    Vector3.forward, -70f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[3],
                    new GameObject[] { planePartsLanding[4] },
                    Vector3.right, 0.3f,
                    Vector3.forward, 70f, 2.5f,
                    false));

                planePartsLanding[11].SetActive(true);
                planePartsLanding[12].SetActive(false);
                planePartsLanding[13].SetActive(true);
                planePartsLanding[14].SetActive(true);
            } else {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0],
                    null, Vector3.right, -90f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[1],
                    new GameObject[] { planePartsLanding[2] },
                    Vector3.right, 0.3f,
                    Vector3.forward, 70f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[3],
                    new GameObject[] { planePartsLanding[4] },
                    Vector3.right, -0.3f,
                    Vector3.forward, -70f, 2.5f,
                    true));

                yield return new WaitForSeconds(0.6f);

                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[8], null,
                    Vector3.right, 0.4f,
                    Vector3.forward, 95f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType3(
                    planePartsLanding[10], null,
                    Vector3.right, -0.4f,
                    Vector3.forward, -95f, 2.5f,
                    false));

                yield return new WaitForSeconds(1.4f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5], null,
                    Vector3.forward, 80f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6], null,
                    Vector3.forward, -80f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.5f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, -110f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[9], null,
                    Vector3.forward, 110f, 2.5f,
                    false));

                planePartsLanding[11].SetActive(false);
                planePartsLanding[12].SetActive(true);
                planePartsLanding[13].SetActive(false);
                planePartsLanding[14].SetActive(false);
            }
        } else if (planeName == PlaneName.Mig29) {
            if (planeLanding) {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[9], null,
                    Vector3.forward, 155f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[10], null,
                    Vector3.forward, -80f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[12], null,
                    Vector3.forward, -155f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[13], null,
                    Vector3.forward, 80f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.8f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6], null,
                    Vector3.forward, -70f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, 70f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[8], null,
                    Vector3.right, -26f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.6f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[11], null,
                    Vector3.right, 15f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[14], null,
                    Vector3.right, 15f, 2.5f,
                    false));

                yield return new WaitForSeconds(1.1f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0],
                    new GameObject[] { planePartsLanding[1] },
                    Vector3.right, -85f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2],
                    new GameObject[] { planePartsLanding[3] },
                    Vector3.right, 65f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4],
                    new GameObject[] { planePartsLanding[5] },
                    Vector3.right, 65f, 2.5f,
                    false));

                planePartsLanding[15].SetActive(true);
                planePartsLanding[16].SetActive(false);
                planePartsLanding[17].SetActive(true);
                planePartsLanding[18].SetActive(true);
            } else {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0],
                    new GameObject[] { planePartsLanding[1] },
                    Vector3.right, 85f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2],
                    new GameObject[] { planePartsLanding[3] },
                    Vector3.right, -65f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4],
                    new GameObject[] { planePartsLanding[5] },
                    Vector3.right, -65f, 2.5f,
                    true));

                yield return new WaitForSeconds(1.1f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[11], null,
                    Vector3.right, -15f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[14], null,
                    Vector3.right, -15f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.6f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6], null,
                    Vector3.forward, 70f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, -70f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[8], null,
                    Vector3.right, 26f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.8f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[9], null,
                    Vector3.forward, -155f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[10], null,
                    Vector3.forward, 80f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[12], null,
                    Vector3.forward, 155f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[13], null,
                    Vector3.forward, -80f, 2.5f,
                    false));

                planePartsLanding[15].SetActive(false);
                planePartsLanding[16].SetActive(true);
                planePartsLanding[17].SetActive(false);
                planePartsLanding[18].SetActive(false);
            }
        } else if (planeName == PlaneName.F4E) {
            if (planeLanding) {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4], null,
                    Vector3.forward, 90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5], null,
                    Vector3.forward, 95f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, -95f, 2.5f,
                    false));

                yield return new WaitForSeconds(1.2f); //1.1

                StartCoroutine(AnimatePartLandingType2(
                    planePartsLanding[6], null,
                    Vector3.forward, -90f,
                    Vector3.right, -28f,
                    2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType2(
                    planePartsLanding[8], null,
                    Vector3.forward, 90f,
                    Vector3.right, -28f,
                    2.5f,
                    false));

                yield return new WaitForSeconds(0.8f); //0.9

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0], null,
                    Vector3.right, -90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[1], null,
                    Vector3.forward, -60f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2], null,
                    Vector3.forward, 60f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[3], null,
                    Vector3.right, -120f, 2.5f,
                    false));

                planePartsLanding[9].SetActive(true);
                planePartsLanding[10].SetActive(false);
                planePartsLanding[11].SetActive(true);
                planePartsLanding[12].SetActive(true);
            } else {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0], null,
                    Vector3.right, 90f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[1], null,
                    Vector3.forward, 60f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2], null,
                    Vector3.forward, -60f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[3], null,
                    Vector3.right, 120f, 2.5f,
                    false));

                yield return new WaitForSeconds(0.8f);

                StartCoroutine(AnimatePartLandingType2(
                    planePartsLanding[6], null,
                    Vector3.forward, 90f,
                    Vector3.right, 28f,
                    2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType2(
                    planePartsLanding[8], null,
                    Vector3.forward, -90f,
                    Vector3.right, 28f,
                    2.5f,
                    false));

                yield return new WaitForSeconds(1.2f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4], null,
                    Vector3.forward, -90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5], null,
                    Vector3.forward, -95f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[7], null,
                    Vector3.forward, 95f, 2.5f,
                    false));

                planePartsLanding[9].SetActive(false);
                planePartsLanding[10].SetActive(true);
                planePartsLanding[11].SetActive(false);
                planePartsLanding[12].SetActive(false);
            }
        } else if (planeName == PlaneName.F16) {
            if (planeLanding) {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4], null,
                    Vector3.forward, 90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5], null,
                    Vector3.forward, -100f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6], null,
                    Vector3.forward, 100f, 2.5f,
                    false));

                yield return new WaitForSeconds(2.5f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0],
                    new GameObject[] { planePartsLanding[1] },
                    Vector3.right, -90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2], null,
                    Vector3.right, 65f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[3], null,
                    Vector3.right, 65f, 2.5f,
                    false));

                planePartsLanding[7].SetActive(true);
                planePartsLanding[8].SetActive(false);
                planePartsLanding[9].SetActive(true);
                planePartsLanding[10].SetActive(true);
            } else {
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[0],
                    new GameObject[] { planePartsLanding[1] },
                    Vector3.right, 90f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[2],
                    null, Vector3.right, -65f, 2.5f,
                    true));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[3],
                    null, Vector3.right, -65f, 2.5f,
                    true));

                yield return new WaitForSeconds(2.5f);

                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[4],
                    null, Vector3.forward, -90f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[5],
                    null, Vector3.forward, 100f, 2.5f,
                    false));
                StartCoroutine(AnimatePartLandingType1(
                    planePartsLanding[6],
                    null, Vector3.forward, -100f, 2.5f,
                    false));

                planePartsLanding[7].SetActive(false);
                planePartsLanding[8].SetActive(true);
                planePartsLanding[9].SetActive(false);
                planePartsLanding[10].SetActive(false);
            }
        }

        yield return new WaitForSeconds(4.0f);

        planeLandingInProgress = false;

        yield return null;
    }

    //Animation type 1. Suitable for jet pieces like fans or wheels.
    IEnumerator AnimatePartType1(
        GameObject obj, Vector3 axis, float speed) {
        Quaternion baseAngle = obj.transform.localRotation;

        while (!planeDamageCritical && planeAnim) {
            obj.transform.Rotate(axis, speed * Time.deltaTime);

            yield return null;
        }

        if (!planeDamageCritical)
            obj.transform.localRotation = baseAngle;

        yield return null;
    }
    //Animation type 2. Suitable for jet pieces like airbrakes, slats, flaps or
    //canopy.
    IEnumerator AnimatePartType2(
        GameObject obj,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime) {
        GameObject objParent = obj.transform.parent.gameObject;

        Quaternion baseRot = objParent.transform.localRotation;

        float time = 0.0f;
        float speedLocalRot = angleLocalRot / animTime;

        while (!planeDamageCritical && planeAnim) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime < animTime)
                time += deltaTime;
            else {
                deltaTime = animTime - time;
                time = 0.0f;
                speedLocalRot *= -1;
            }

            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            yield return null;
        }

        if (!planeDamageCritical)
            objParent.transform.localRotation = baseRot;

        yield return null;
    }
    //Animation type 3. Suitable for jet pieces like ailerons or rudders.
    IEnumerator AnimatePartType3(
        GameObject obj,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime) {
        GameObject objParent = obj.transform.parent.gameObject;

        Quaternion baseRot = objParent.transform.localRotation;

        float time = 0.0f;
        float speedLocalRot = angleLocalRot / animTime;
        float switchTime = animTime;

        while (!planeDamageCritical && planeAnim) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime < switchTime)
                time += deltaTime;
            else {
                deltaTime = switchTime - time;
                switchTime = 2 * animTime;
                time = 0.0f;
                speedLocalRot *= -1;
            }

            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            yield return null;
        }

        if (!planeDamageCritical)
            objParent.transform.localRotation = baseRot;

        yield return null;
    }
    //Animation type 4. Suitable for complex jet pieces like F-4E inner slats
    IEnumerator AnimatePartType4(
        GameObject obj,
        Vector3 axisParentTrans, float lengthParentTrans,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime) {
        GameObject objParent = obj.transform.parent.gameObject;
        GameObject objParent2 = obj.transform.parent.parent.gameObject;

        Vector3 basePos = objParent.transform.localPosition;
        Quaternion baseRot = objParent.transform.localRotation;

        float time = 0.0f;
        float speedParentTrans = lengthParentTrans / animTime;
        float speedLocalRot = angleLocalRot / animTime;

        while (!planeDamageCritical && planeAnim) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime < animTime)
                time += deltaTime;
            else {
                deltaTime = animTime - time;
                time = 0.0f;
                speedParentTrans *= -1;
                speedLocalRot *= -1;
            }

            float deltaParentTrans = speedParentTrans * spdFac * deltaTime;
            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent.transform.Translate(
                axisParentTrans * deltaParentTrans,
                objParent2.transform);

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            yield return null;
        }

        if (!planeDamageCritical) {
            objParent.transform.localPosition = basePos;
            objParent.transform.localRotation = baseRot;
        }

        yield return null;
    }
    //Animation type 5. Suitable for even more complex jet pieces like F-4E
    //outer slats
    IEnumerator AnimatePartType5(
        GameObject obj,
        Vector3 axisParentTrans, float lengthParentTrans,
        Vector3 axisParentRot, float angleParentRot,
        Vector3 axisLocalRot, float angleLocalRot,
        float animTime) {
        GameObject objParent = obj.transform.parent.gameObject;
        GameObject objParent2 = obj.transform.parent.parent.gameObject;
        GameObject objParent3 = obj.transform.parent.parent.parent.gameObject;

        Vector3 basePos = objParent.transform.localPosition;
        Vector3 basePos2 = objParent2.transform.localPosition;
        Quaternion baseRot = objParent.transform.localRotation;
        Quaternion baseRot2 = objParent2.transform.localRotation;

        float time = 0.0f;
        float speedParentTrans = lengthParentTrans / animTime;
        float speedParentRot = angleParentRot / animTime;
        float speedLocalRot = angleLocalRot / animTime;

        while (!planeDamageCritical && planeAnim) {
            float deltaTime = Time.deltaTime;
            if (time + deltaTime < animTime)
                time += deltaTime;
            else {
                deltaTime = animTime - time;
                time = 0.0f;
                speedParentTrans *= -1;
                speedParentRot *= -1;
                speedLocalRot *= -1;
            }

            float deltaParentTrans = speedParentTrans * spdFac * deltaTime;
            float deltaParentRot = speedParentRot * spdFac * deltaTime;
            float deltaLocalRot = speedLocalRot * spdFac * deltaTime;

            objParent2.transform.Translate(
                axisParentTrans * deltaParentTrans,
                objParent3.transform);

            objParent2.transform.Rotate(
                axisParentRot,
                deltaParentRot,
                Space.Self);

            objParent.transform.Rotate(
                axisLocalRot,
                deltaLocalRot,
                Space.Self);

            yield return null;
        }

        if (!planeDamageCritical) {
            objParent.transform.localPosition = basePos;
            objParent2.transform.localPosition = basePos2;
            objParent.transform.localRotation = baseRot;
            objParent2.transform.localRotation = baseRot2;
        }

        yield return null;
    }

    //Trigger animation of various pieces of the jet
    private bool planeAnim, planeAnimInProgress;
    public void AnimateJet() {
        if (!planeDamageCritical) {
            if (!planeAnim && !planeAnimInProgress) {
                planeAnim = true;
                StartCoroutine(AnimateJetCoo());
            } else
                planeAnim = false;
        }
    }
    IEnumerator AnimateJetCoo() {
        planeAnimInProgress = true;

        if (planeName == PlaneName.Mig21) {
            //Canopy
            GameObject canopyFrame = transform.Find(
                "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame, Vector3.forward, 90f, 2.5f));
            GameObject canopyFrame_1 = transform.Find(
                "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame_1, Vector3.forward, 90f, 2.5f));

            //Nose
            /*GameObject Nose = transform.Find(
                "Hull_LOD0/Nose_LOD0").gameObject;
            StartCoroutine(
                AnimatePart(Nose, Vector3.forward, 90f, 0f, 40f));
            GameObject Nose1 = transform.Find(
                "Hull_LOD1/Nose_LOD1").gameObject;
            StartCoroutine(
                AnimatePart(Nose1, Vector3.forward, 90f, 0f, 40f));
            GameObject Nose2 = transform.Find(
                "Hull_LOD2/Nose_LOD2").gameObject;
            StartCoroutine(
                AnimatePart(Nose2, Vector3.forward, 90f, 0f, 40f));*/

            //Gears
            GameObject gearFront = transform.Find(
                "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                "GearStrutFront_LOD0/GearFront_LOD0").gameObject;
            GameObject gearLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisGearStrutLeft_LOD0/" +
                "_AxisGearStrutLeftB_LOD0/GearStrutLeft_LOD0/" +
                "GearLeft_LOD0").gameObject;
            GameObject gearRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisGearStrutRight_LOD0/" +
                "_AxisGearStrutRightB_LOD0/GearStrutRight_LOD0/" +
                "GearRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(gearFront, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearLeft, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearRight, Vector3.left, -180f));

            //Airbrakes
            GameObject airbrakeLeft = transform.Find(
                "Hull_LOD0/_AxisAirbrakeLeft_LOD0/" +
                "AirbrakeLeft_LOD0").gameObject;
            GameObject airbrakeRear = transform.Find(
                "Hull_LOD0/_AxisAirbrakeRear_LOD0/" +
                "AirbrakeRear_LOD0").gameObject;
            GameObject airbrakeRight = transform.Find(
                "Hull_LOD0/_AxisAirbrakeRight_LOD0/" +
                "AirbrakeRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(airbrakeLeft, Vector3.right, -60f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeRear, Vector3.right, -60f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeRight, Vector3.right, -60f, 1.5f));

            //Flaps
            GameObject flapLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                "FlapLeft_LOD0").gameObject;
            GameObject flapRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                "FlapRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(flapLeft, Vector3.right, -30f, 2.5f));
            StartCoroutine(
                AnimatePartType2(flapRight, Vector3.right, -30f, 2.5f));

            yield return new WaitForSeconds(0.5f);

            //Ailerons
            GameObject aileronLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                "AileronLeft_LOD0").gameObject;
            GameObject aileronRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                "AileronRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(aileronLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(aileronRight, Vector3.right, 15f, 1.0f));

            yield return new WaitForSeconds(0.5f);

            //Rudder
            GameObject rudder = transform.Find(
                "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/Rudder_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(rudder, Vector3.up, 15f, 1.0f));

            //Stabilators
            GameObject stabilatorLeft = transform.Find(
                "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                "StabilatorLeft_LOD0").gameObject;
            GameObject stabilatorRight = transform.Find(
                "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                "StabilatorRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(stabilatorLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(stabilatorRight, Vector3.right, 15f, 1.0f));
        } else if (planeName == PlaneName.Mig29) {
            //Canopy
            GameObject canopyFrame = transform.Find(
                "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame, Vector3.right, -35f, 2.5f));
            GameObject canopyFrame_1 = transform.Find(
                "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame_1, Vector3.right, -35f, 2.5f));

            //Fans
            GameObject fanLeft = transform.Find(
                "Hull_LOD0/FanLeft_LOD0").gameObject;
            GameObject fanRight = transform.Find(
                "Hull_LOD0/FanRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(fanLeft, Vector3.forward, 360f));
            StartCoroutine(
                AnimatePartType1(fanRight, Vector3.forward, -360f));

            //Gears
            GameObject gearFront = transform.Find(
                "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                "GearStrutFront_LOD0/GearFront_LOD0").gameObject;
            GameObject gearLeft = transform.Find(
                "Hull_LOD0/_AxisGearStrutLeft_LOD0/" +
                "GearStrutLeft_LOD0/GearLeft_LOD0").gameObject;
            GameObject gearRight = transform.Find(
                "Hull_LOD0/_AxisGearStrutRight_LOD0/" +
                "GearStrutRight_LOD0/GearRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(gearFront, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearLeft, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearRight, Vector3.left, -180f));

            //Airbrakes
            GameObject airbrakeUpper = transform.Find(
                "Hull_LOD0/_AxisAirbrakeUpper_LOD0/" +
                "AirbrakeUpper_LOD0").gameObject;
            GameObject airbrakeLower = transform.Find(
                "Hull_LOD0/_AxisAirbrakeLower_LOD0/" +
                "AirbrakeLower_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(airbrakeUpper, Vector3.right, 55f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeLower, Vector3.right, -60f, 1.5f));

            //Flaps
            GameObject flapLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                "FlapLeft_LOD0").gameObject;
            GameObject flapRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                "FlapRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(flapLeft, Vector3.right, -30f, 2.5f));
            StartCoroutine(
                AnimatePartType2(flapRight, Vector3.right, -30f, 2.5f));

            yield return new WaitForSeconds(0.5f);

            //Ailerons
            GameObject aileronLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                "AileronLeft_LOD0").gameObject;
            GameObject aileronRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                "AileronRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(aileronLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(aileronRight, Vector3.right, 15f, 1.0f));

            yield return new WaitForSeconds(0.5f);

            //Slats
            GameObject slatLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft_LOD0/" +
                "SlatLeft_LOD0").gameObject;
            GameObject slatRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisSlatRight_LOD0/" +
                "SlatRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(slatLeft, Vector3.right, 20f, 1.0f));
            StartCoroutine(
                AnimatePartType2(slatRight, Vector3.right, 20f, 1.0f));

            //Rudders
            GameObject rudderLeft = transform.Find(
                "Hull_LOD0/TailLeft_LOD0/_AxisRudderLeft_LOD0/" +
                "RudderLeft_LOD0").gameObject;
            GameObject rudderRight = transform.Find(
                "Hull_LOD0/TailRight_LOD0/_AxisRudderRight_LOD0/" +
                "RudderRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(rudderLeft, Vector3.up, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(rudderRight, Vector3.up, 15f, 1.0f));

            //Stabilators
            GameObject stabilatorLeft = transform.Find(
                "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                "StabilatorLeft_LOD0").gameObject;
            GameObject stabilatorRight = transform.Find(
                "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                "StabilatorRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(stabilatorLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(stabilatorRight, Vector3.right, 15f, 1.0f));
        } else if (planeName == PlaneName.F4E) {
            //Canopy
            //Front
            GameObject canopyFrame = transform.Find(
                "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame, Vector3.right, -45f, 2.5f));
            GameObject canopyFrame_1 = transform.Find(
                "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame_1, Vector3.right, -45f, 2.5f));
            //Rear
            GameObject canopyFrameRear = transform.Find(
                "Hull_LOD0/_AxisCanopyFrameRear_LOD0/" +
                "CanopyFrameRear_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrameRear, Vector3.right, -45f, 2.5f));
            GameObject canopyFrameRear_1 = transform.Find(
                "Hull_LOD1/_AxisCanopyFrameRear_LOD1/" +
                "CanopyFrameRear_LOD1").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrameRear_1, Vector3.right, -45f, 2.5f));

            //Fans
            GameObject fanLeft = transform.Find(
                "Hull_LOD0/FanLeft_LOD0").gameObject;
            GameObject fanRight = transform.Find(
                "Hull_LOD0/FanRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(fanLeft, Vector3.forward, 360f));
            StartCoroutine(
                AnimatePartType1(fanRight, Vector3.forward, -360f));

            //Gears
            GameObject gearFront = transform.Find(
                "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                "GearStrutFront_LOD0/GearFront_LOD0").gameObject;
            GameObject gearLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisGearStrutLeft_LOD0/" +
                "GearStrutLeft_LOD0/GearLeft_LOD0").gameObject;
            GameObject gearRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisGearStrutRight_LOD0/" +
                "GearStrutRight_LOD0/GearRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(gearFront, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearLeft, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearRight, Vector3.left, -180f));

            //Airbrakes
            GameObject airbrakeLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisAirbrakeLeft_LOD0/" +
                "AirbrakeLeft_LOD0").gameObject;
            GameObject airbrakeRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisAirbrakeRight_LOD0/" +
                "AirbrakeRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(airbrakeLeft, Vector3.right, -60f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeRight, Vector3.right, -60f, 1.5f));

            //Flaps
            GameObject flapLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisFlapLeft_LOD0/" +
                "FlapLeft_LOD0").gameObject;
            GameObject flapRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisFlapRight_LOD0/" +
                "FlapRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(flapLeft, Vector3.right, -30f, 2.5f));
            StartCoroutine(
                AnimatePartType2(flapRight, Vector3.right, -30f, 2.5f));

            yield return new WaitForSeconds(0.5f);

            //Ailerons
            GameObject aileronLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisAileronLeft_LOD0/" +
                "AileronLeft_LOD0").gameObject;
            GameObject aileronRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisAileronRight_LOD0/" +
                "AileronRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(aileronLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(aileronRight, Vector3.right, 15f, 1.0f));

            yield return new WaitForSeconds(0.5f);

            //Slats
            GameObject slatLeft1 = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft1_LOD0/" +
                "SlatLeft1_LOD0").gameObject;
            GameObject slatLeft2 = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft2_LOD0/" +
                "_AxisSlatLeft2B_LOD0/SlatLeft2_LOD0").gameObject;
            GameObject slatRight1 = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisSlatRight1_LOD0/" +
                "SlatRight1_LOD0").gameObject;
            GameObject slatRight2 = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisSlatRight2_LOD0/" +
                "_AxisSlatRight2B_LOD0/SlatRight2_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType4(
                    slatLeft1,
                    Vector3.forward, 0.26f,
                    Vector3.right, 15f,
                    1.0f));
            StartCoroutine(
                AnimatePartType5(
                    slatLeft2,
                    Vector3.forward, 0.34f,
                    Vector3.right, 15f,
                    Vector3.up, -2.7f,
                    1.0f));
            StartCoroutine(
                AnimatePartType4(
                    slatRight1,
                    Vector3.forward, 0.26f,
                    Vector3.right, 15f,
                    1.0f));
            StartCoroutine(
                AnimatePartType5(
                    slatRight2,
                    Vector3.forward, 0.34f,
                    Vector3.right, 15f,
                    Vector3.up, 2.7f,
                    1.0f));

            //Rudder
            GameObject rudder = transform.Find(
                "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/Rudder_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(rudder, Vector3.up, 15f, 1.0f));

            //Stabilators
            GameObject stabilatorLeft = transform.Find(
                "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                "StabilatorLeft_LOD0").gameObject;
            GameObject stabilatorRight = transform.Find(
                "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                "StabilatorRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(stabilatorLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(stabilatorRight, Vector3.right, 15f, 1.0f));
        } else if (planeName == PlaneName.F16) {
            //Canopy
            GameObject canopyFrame = transform.Find(
                "Hull_LOD0/_AxisCanopyFrame_LOD0/CanopyFrame_LOD0/").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame, Vector3.right, -30f, 2.5f));
            GameObject canopyFrame_1 = transform.Find(
                "Hull_LOD1/_AxisCanopyFrame_LOD1/CanopyFrame_LOD1/").gameObject;
            StartCoroutine(
                AnimatePartType2(canopyFrame_1, Vector3.right, -30f, 2.5f));

            //Fan
            GameObject fan = transform.Find(
                "Hull_LOD0/Fan_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(fan, Vector3.forward, 360f));

            //Gears
            GameObject gearFront = transform.Find(
                "Hull_LOD0/_AxisGearStrutFront_LOD0/" +
                "GearStrutFront_LOD0/GearFront_LOD0").gameObject;
            GameObject gearLeft = transform.Find(
                "Hull_LOD0/_AxisGearStrutLeft_LOD0/" +
                "GearStrutLeft_LOD0/GearLeft_LOD0").gameObject;
            GameObject gearRight = transform.Find(
                "Hull_LOD0/_AxisGearStrutRight_LOD0/" +
                "GearStrutRight_LOD0/GearRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType1(gearFront, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearLeft, Vector3.left, -180f));
            StartCoroutine(
                AnimatePartType1(gearRight, Vector3.left, -180f));

            //Airbrakes
            GameObject airbrakeUpperLeft = transform.Find(
                "Hull_LOD0/_AxisAirbrakeUpperLeft_LOD0/" +
                "AirbrakeUpperLeft_LOD0").gameObject;
            GameObject airbrakeLowerLeft = transform.Find(
                "Hull_LOD0/_AxisAirbrakeLowerLeft_LOD0/" +
                "AirbrakeLowerLeft_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(airbrakeUpperLeft, Vector3.right, 60f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeLowerLeft, Vector3.right, -60f, 1.5f));
            GameObject airbrakeUpperRight = transform.Find(
                "Hull_LOD0/_AxisAirbrakeUpperRight_LOD0/" +
                "AirbrakeUpperRight_LOD0").gameObject;
            GameObject airbrakeLowerRight = transform.Find(
                "Hull_LOD0/_AxisAirbrakeLowerRight_LOD0/" +
                "AirbrakeLowerRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(airbrakeUpperRight, Vector3.right, 60f, 1.5f));
            StartCoroutine(
                AnimatePartType2(airbrakeLowerRight, Vector3.right, -60f, 1.5f));

            //Flaperons
            GameObject flaperonLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisFlaperonLeft_LOD0/" +
                "FlaperonLeft_LOD0").gameObject;
            GameObject flaperonRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisFlaperonRight_LOD0/" +
                "FlaperonRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(flaperonLeft, Vector3.right, -30f, 1.0f));
            StartCoroutine(
                AnimatePartType2(flaperonRight, Vector3.right, -30f, 1.0f));

            yield return new WaitForSeconds(0.5f);

            //Slats
            GameObject slatLeft = transform.Find(
                "Hull_LOD0/WingLeft_LOD0/_AxisSlatLeft_LOD0/" +
                "SlatLeft_LOD0").gameObject;
            GameObject slatRight = transform.Find(
                "Hull_LOD0/WingRight_LOD0/_AxisSlatRight_LOD0/" +
                "SlatRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType2(slatLeft, Vector3.right, 20f, 1.0f));
            StartCoroutine(
                AnimatePartType2(slatRight, Vector3.right, 20f, 1.0f));

            //Rudder
            GameObject rudder = transform.Find(
                "Hull_LOD0/Tail_LOD0/_AxisRudder_LOD0/Rudder_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(rudder, Vector3.up, 15f, 1.0f));

            //Stabilators
            GameObject stabilatorLeft = transform.Find(
                "Hull_LOD0/_AxisStabilatorLeft_LOD0/" +
                "StabilatorLeft_LOD0").gameObject;
            GameObject stabilatorRight = transform.Find(
                "Hull_LOD0/_AxisStabilatorRight_LOD0/" +
                "StabilatorRight_LOD0").gameObject;
            StartCoroutine(
                AnimatePartType3(stabilatorLeft, Vector3.right, 15f, 1.0f));
            StartCoroutine(
                AnimatePartType3(stabilatorRight, Vector3.right, 15f, 1.0f));
        }

        yield return new WaitForSeconds(1.0f);

        planeAnimInProgress = false;

        yield return null;
    }
}

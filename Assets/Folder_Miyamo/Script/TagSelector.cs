using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class TagSelector : MonoBehaviour
{
    private FlyingPancakeTGS _flyingPancakeTGS;

    [SerializeField, Tag]
    private string _testTag;
    private void Start()
    {
        _flyingPancakeTGS = new FlyingPancakeTGS();
        _flyingPancakeTGS.Enable();
    }
    private void Update()
    {
        if (_flyingPancakeTGS.Player.Fire.triggered)
        {
            print("hoge");
            Gamepad.current.SetMotorSpeeds(12f, 12f);
        }
        if (_flyingPancakeTGS.Player.Move.triggered)
        {
            print("hoge");
            Gamepad.current.SetMotorSpeeds(0, 0f);
        }
        Debug.Log(_flyingPancakeTGS.Player.Move.ReadValue<Vector2>());
        Debug.Log(_flyingPancakeTGS.Player.Speed.ReadValue<Vector2>().y);

    }
}

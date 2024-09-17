using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PizzaManParam")]
public class PizzaGameParameterScript : ScriptableObject
{

    [Header("ピザにタグが現れる値")]
    public float _pizzaCountmin;

    [Header("あたったときに減少する間隔")]
    public float _decreaseInterval;

    [Header("一度に減少する量")]
    public float _decreaseAmount;



    [Header("あたったときに減少する間隔"), Foldout("左腕")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange;







}

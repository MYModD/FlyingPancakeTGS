using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PizzaManParam")]
public class PizzaGameParameterScript : ScriptableObject
{

    [Header("�s�U�Ƀ^�O�������l")]
    public float _pizzaCountmin;

    [Header("���������Ƃ��Ɍ�������Ԋu")]
    public float _decreaseInterval;

    [Header("��x�Ɍ��������")]
    public float _decreaseAmount;



    [Header("���������Ƃ��Ɍ�������Ԋu"), Foldout("���r")]
    [MinMaxSlider(0, 5f)]
    public Vector2 _durationRange;







}

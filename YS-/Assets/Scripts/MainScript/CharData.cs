using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor.Animations;
using UnityEngine;

public enum Character
{
    farmer1, farmer2
}

[System.Serializable]
public class CharData : MonoBehaviour
{
    public Sprite info_img;
    public AnimatorController info_anim;

    public Character character;
    public float hp;
    public float attack;
    public int speed;


}
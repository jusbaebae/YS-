using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[Serializable]
public class Data
{
    [SerializeField] private Sprite portrait;
    [SerializeField] private Sprite info_img;

    public String name;
    public float hp;
    public float attack;
    public int speed;

    public Sprite getPortraitSprite()
    {
        return portrait;
    }
    
    public Sprite getInfo_imgSprite()
    {
        return info_img;
    }
}



public class CharData : MonoBehaviour
{
    [SerializeField] private Data[] data;
   
    public Data[] GetCharDatas() { return data; }
    
}




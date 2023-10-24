using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vanilla;

public class Observer : MonoBehaviour
{
    public int kill;
    public List<ItemData> data;
    public static Observer instance;


    private void Awake()
    {
        data = new List<ItemData>();

        if (instance != this && instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

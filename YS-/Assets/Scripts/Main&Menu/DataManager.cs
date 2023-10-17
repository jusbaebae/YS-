using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public CharData currentCharData;

    private void Awake()
    {
        currentCharData = null;
        instance = this;
    }
}

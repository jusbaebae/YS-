using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{

    public static FadeController instance { get; private set;}

    private void Awake()
    {
        instance = this;
    }





}

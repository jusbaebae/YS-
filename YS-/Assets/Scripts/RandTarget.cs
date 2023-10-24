using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vanilla;

public class RandTarget : MonoBehaviour
{
    public Transform[] targetPoint;
    void Awake()
    {
        targetPoint = GetComponentsInChildren<Transform>();
    }
    public Vector3 RandDir()
    {
        Vector3 randdir = targetPoint[Random.Range(1, targetPoint.Length)].position;
        return randdir;
    }
}

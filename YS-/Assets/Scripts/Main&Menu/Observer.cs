using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vanilla;

public class Observer : MonoBehaviour
{
    public int kill;
    public static Observer instance;

    private void Awake()
    {
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
    public int GetKill()
    {
        return kill;
    }

    public void SetKill(int kill)
    {
        this.kill = 0;
        this.kill = kill;
    }
}

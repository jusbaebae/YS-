using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vanilla;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] items;
    enum Achive
    {
       k100,
       k300,
       k500
    }

    Achive[] achives;
    // Start is called before the first frame update
    private void Awake()
    { 
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        if (!PlayerPrefs.HasKey("MyData")){
            Init();
        }

        foreach (Achive achive in achives)
        {
            CheckAchive(achive);
        }
    }

    private void Start()
    {
        UnlockItem();

    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach(Achive achive in achives) {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
       
    }

    void UnlockItem()
    {
       for(int index= 0; index<items.Length; index++) { 
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            if (isUnlock)
            {
                items[index].transform.GetComponent<Image>().color = new Color(255, 255, 255);
            }
        }
    }


    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch(achive)
        {
            case Achive.k100:
                isAchive = Observer.instance.GetKill() >= 100;
                break;
            case Achive.k300:
                isAchive = Observer.instance.GetKill() >= 300;
                break;
            case Achive.k500:
                isAchive = Observer.instance.GetKill() >= 500;
                break;
        }

        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        }
    }


}

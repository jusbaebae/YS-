using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeletChar : MonoBehaviour
{
    private CharData charData;
    public GameObject info_anim;
    public TextMeshProUGUI info_hp;
    public TextMeshProUGUI info_attack;
    public TextMeshProUGUI info_speed;
    void Start()
    {
        charData = null;

        info_hp.SetText("");
        info_attack.SetText("");
        info_speed.SetText("");

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Seletion()
    {
        charData = EventSystem.current.currentSelectedGameObject.GetComponent<CharData>();
        DataManger.instance.currentCharData = charData;
        


        info_anim.GetComponent<SpriteRenderer>().sprite = charData.info_img;
        info_anim.GetComponent<Animator>().runtimeAnimatorController = new AnimatorOverrideController(charData.info_anim);
        SetState(charData);
    }
    
    public void SetState(CharData chardata)
    {
        info_hp.SetText("" + charData.hp);
        info_attack.SetText("" + charData.attack);
        info_speed.SetText("" + charData.speed);
    }
}

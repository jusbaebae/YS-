using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectChar : MonoBehaviour
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
    public void Seletion()
    {
        charData = EventSystem.current.currentSelectedGameObject.GetComponent<CharData>();
        DataManager.instance.currentCharData = charData;
        
        info_anim.GetComponent<Animator>().runtimeAnimatorController = new AnimatorOverrideController(charData.info_anim);
        SetState(charData);
    }
    
    public void SetState(CharData chardata)
    {
        info_hp.SetText("" + charData.hp);
        info_attack.SetText("" + charData.attack);
        info_speed.SetText("" + charData.speed);
    }

    public void GameStart()
    {
        if (charData != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

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

    public TextMeshProUGUI[] info_state;
    public GameObject[] info_stateImage;

    void Start()
    {
        charData = null;

        for(int i = 0; i < info_state.Length; i++)
        {
            info_state[i].SetText("");
            info_stateImage[i].SetActive(false);
        }

    }
    public void Seletion()
    {
        charData = EventSystem.current.currentSelectedGameObject.GetComponent<CharData>();
        DataManager.instance.currentCharData = charData;
        
        info_anim.GetComponent<Animator>().runtimeAnimatorController = new AnimatorOverrideController(charData.info_anim);
        print(DataManager.instance.currentCharData.play_anim);
        SetState(charData);
    }
    
    public void SetState(CharData chardata)
    {
        string[] state = { 
            chardata.hp.ToString(), 
            chardata.attack.ToString(), 
            chardata.speed.ToString() ,
            chardata.defense.ToString(),
            chardata.luck.ToString()
        };
        for(int index = 0; index < info_state.Length; index++)
        {
            info_state[index].SetText(state[index].ToString());
            info_stateImage[index].SetActive(true);
        }
    }

    public void GameStart()
    {
        if (charData != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}

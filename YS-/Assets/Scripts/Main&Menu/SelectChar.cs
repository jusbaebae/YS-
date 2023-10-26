using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        info_anim.GetComponent<SpriteRenderer>().sprite = charData.info_sprite;
        

        print(DataManager.instance.currentCharData.play_anim);
        SetState(charData);
    }
    
    public void SetState(CharData chardata)
    {
        string[] state = { 
            chardata.hp.ToString(), 
            (10 * chardata.attack).ToString(), 
            (5 * chardata.speed).ToString() ,
            (20 - chardata.defense).ToString(),
            (2 * chardata.luck).ToString()
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

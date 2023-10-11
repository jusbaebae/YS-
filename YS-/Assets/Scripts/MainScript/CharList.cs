using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharList : MonoBehaviour
{
    //ĳ���� �ڽ� ������
    [SerializeField] private GameObject charBoxPrefab;

    //��ũ�Ѻ� ������Ʈ
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject info_img;
    
    public CharData charData;
    private Data[] data;


    private void Start()
    {
        charData = GameObject.Find("Data").GetComponent<CharData>();
        data = charData.GetCharDatas();
        init();
    }


    void init()
    {
        for (int i = 0; i < data.Length; i++)
        {
            GameObject charBox = Instantiate(charBoxPrefab);
            charBox.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = data[i].getPortraitSprite();
            charBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(data[i].name);

            charBox.transform.SetParent(content.transform, false);
        }

    }

    void selection()
    {
        GameObject character = EventSystem.current.currentSelectedGameObject;
        Sprite charImage = character.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;

        info_img.GetComponent<Image>().sprite = charImage;
    }

}

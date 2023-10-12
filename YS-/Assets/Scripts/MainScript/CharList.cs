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

    public static CharList Instance;
    //캐릭터 박스 프리팹
    [SerializeField] private GameObject charBoxPrefab;

    //스크롤뷰 오브젝트
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject info_img;
    
    public CharData charData;
    private Data[] data;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        charData = GameObject.Find("Data").GetComponent<CharData>();
        data = charData.GetCharDatas();
        Init();
    }


    void Init()
    {
        for (int i = 0; i < data.Length; i++)
        {
            GameObject charBox = Instantiate(charBoxPrefab);
            charBox.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = data[i].getPortraitSprite();
            charBox.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText(data[i].name);

            charBox.transform.GetComponent<Button>().onClick.AddListener(delegate { Selection(); });
            charBox.transform.SetParent(content.transform, false);
        }

    }

    public void Selection()
    {
        print("클릭");
        GameObject character = EventSystem.current.currentSelectedGameObject;
        Sprite charImage = character.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;

        if (character != null)
        {
            info_img.transform.GetComponent<SpriteRenderer>().sprite=charImage;
            print(charImage.name);
        }
        else
        {
            
        }
    }

}

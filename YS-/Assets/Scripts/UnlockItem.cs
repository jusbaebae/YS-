using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vanilla;

public class UnlockItem : MonoBehaviour
{
    //해금된 아이템
    public List<ItemData> unlockData = new List<ItemData>();
    public GameObject unlockItemPrefab;
    LevelUp LevelUp;

    void Awake()
    { 
        LevelUp = GameObject.Find("LevelUp").gameObject.GetComponent<LevelUp>();
        //unlockData = Observer.instance.data; 테스트를 위해서 잠시 주석처리
        SetUnlockItem();
    }

    public void SetUnlockItem()
    {     
        for (int i = 0; i < unlockData.Count; i++)
        {
            unlockItemPrefab.GetComponent<Item>().data = unlockData[i];

            GameObject unlockItem = Instantiate(unlockItemPrefab, gameObject.transform);
            unlockItem.GetComponent<Button>().onClick.AddListener(LevelUp.Hide);
            unlockItem.GetComponent<Button>().onClick.AddListener(unlockItemPrefab.GetComponent<Item>().onClick);

            unlockItem.SetActive(false);
        }  
    }
}

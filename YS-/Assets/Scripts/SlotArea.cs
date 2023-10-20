using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using vanilla;

public class SlotArea : MonoBehaviour
{
    public bool[] wfullcheck;
    public bool[] gfullcheck;
    public ItemData data; 
    public GameObject[] wslots;
    public GameObject[] gslots;

    void Awake()
    {   // wfullcheck 배열 초기화
        wfullcheck = new bool[wslots.Length];
        // gfullcheck 배열 초기화
        gfullcheck = new bool[gslots.Length];
    }

    // 아이템추가(무기)
    public bool AddItemToSlotWeapon(ItemData itemData)
    {
        // 무기 슬롯 검사
        for (int i = 0; i < wslots.Length; i++)
        {
            if (!wfullcheck[i])
            {
                // 무기 슬롯이 비어 있는 경우
                wslots[i].GetComponent<Slot>().AddSlotWeapon(itemData);
                wfullcheck[i] = true; // 슬롯을 차지함을 표시
                return true; // 아이템을 성공적으로 추가함
            }
        }
        // 모든 슬롯이 이미 차있음
        return false; // 아이템을 추가할 수 없음
    }
    // 아이템추가(장신구 위코드랑 동일)
    public bool AddItemToSlotGear(ItemData itemData) 
    { 
        for (int i = 0; i < gslots.Length; i++)
        {
            if (!gfullcheck[i])
            {
                gslots[i].GetComponent<Slot>().AddSlotGear(itemData);
                gfullcheck[i] = true;
                return true; 
            }
        }
        return false;
    }

    //각 아이템의 레벨 할당
    public void levelToSlot(ItemData itemData, int level)
    {
        // 무기 슬롯 검사
        for (int i = 0; i < wslots.Length; i++)
        {
            //아이템이 일치하는경우 레벨 할당
            if (itemData == wslots[i].GetComponent<Slot>().data)
            {
                // 슬롯 아래에 텍스트를 생성하고 레벨 할당
                Text levelText = wslots[i].GetComponentInChildren<Text>();
                levelText.text = "Lv." + level.ToString();
            }
        }

        // 장신구 슬롯 검사
        for (int i = 0; i < gslots.Length; i++)
        {
            if (itemData == gslots[i].GetComponent<Slot>().data)
            {
                Text levelText = gslots[i].GetComponentInChildren<Text>();
                levelText.text = "Lv." + level.ToString();
            }
        }

    }
}


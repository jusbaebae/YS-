using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // 아이템추가
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
    public bool AddItemToSlotGear(ItemData itemData) 
    { 
        // 장신구 슬롯 검사
        for (int i = 0; i < gslots.Length; i++)
        {
            if (!gfullcheck[i])
            {
                // 장신구 슬롯이 비어 있는 경우
                gslots[i].GetComponent<Slot>().AddSlotGear(itemData);
                gfullcheck[i] = true; // 슬롯을 차지함을 표시
                return true; // 아이템을 성공적으로 추가함
            }
        }

        // 모든 슬롯이 이미 차있음
        return false; // 아이템을 추가할 수 없음
    }
}


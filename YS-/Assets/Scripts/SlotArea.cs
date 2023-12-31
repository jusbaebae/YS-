using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using vanilla;
using static Cinemachine.DocumentationSortingAttribute;
using System.Net.NetworkInformation;

public class SlotArea : MonoBehaviour
{
    public Slot[] wslots;
    public Slot[] gslots;

    // 아이템추가
    private void Awake()
    {
        Slot[] slots = GetComponentsInChildren<Slot>();
        int i = 0, j = 0;
        foreach(Slot slot in slots)
        {
            switch (slot.slotType)
            {
                case Slot.SlotType.Weapon:
                    wslots[i++] = slot;
                    break;
                case Slot.SlotType.Gear:
                    gslots[j++] = slot;
                    break;
            }
        }
    }
    public bool AddItemToSlot(ItemData itemData)
    {
        if (itemData.itemCategory == ItemData.ItemCategory.Weapon)
        {
            // 무기 슬롯 검사
            foreach (Slot wslot in wslots)
            {
                if (wslot.data == null)
                {
                    // 무기 슬롯이 비어 있는 경우
                    wslot.AddSlot(itemData);
                    return true; // 아이템을 성공적으로 추가함
                }
            }
        }
        else if (itemData.itemCategory == ItemData.ItemCategory.Brooch)
        {
            // 장신구 슬롯 검사
            foreach (Slot gslot in gslots)
            {
                if (gslot.data == null)
                {
                    // 장신구 슬롯이 비어 있는 경우
                    gslot.AddSlot(itemData);
                    return true; // 아이템을 성공적으로 추가함
                }
            }
        }
        return false;// 아이템을 추가할 수 없음
    }
    public void levelToSlot(ItemData itemData, int level)
    {
        if (itemData.itemCategory == ItemData.ItemCategory.Weapon)
        {
            // 무기 슬롯 검사
            foreach (Slot slot in wslots)
            {
                //아이템이 일치하는경우 레벨 할당
                if (itemData == slot.data)
                {
                    // 슬롯 아래에 텍스트를 컴포넌트를 찾고 레벨 할당
                    Text levelText = slot.GetComponentInChildren<Text>();
                    levelText.text = "Lv." + level.ToString();
                }
            }
        }
        else if (itemData.itemCategory == ItemData.ItemCategory.Brooch)
        {
            // 장신구 슬롯 검사
            foreach (Slot slot in gslots)
            {
                if (itemData == slot.data)
                {
                    // 슬롯 아래에 텍스트를 컴포넌트를 찾고 레벨 할당
                    Text levelText = slot.GetComponentInChildren<Text>();
                    levelText.text = "Lv." + level.ToString();
                }
            }
        }
    }
    public void UpgradeWeapon(ItemData itemData, ItemData upgradeData)
    {
        foreach (Slot slot in wslots)
        {
            //아이템이 일치하는경우 레벨 할당
            if (itemData == slot.data)
            {
                // 아이템 추가
                slot.AddSlot(upgradeData);
                // 슬롯 아래에 텍스트를 컴포넌트를 찾고 레벨 제거
                Text levelText = slot.GetComponentInChildren<Text>();
                levelText.text = "";
            }
        }
    }
}


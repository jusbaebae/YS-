using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vanilla;

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
    public bool AddItemToSlotWeapon(ItemData itemData)
    {
        // 무기 슬롯 검사
        foreach (Slot wslot in wslots)
        {
            if(wslot.data == null)
            {
                // 무기 슬롯이 비어 있는 경우
                wslot.AddSlot(itemData);
                return true; // 아이템을 성공적으로 추가함
            }
        }
        return false;// 아이템을 추가할 수 없음
    }
    public bool AddItemToSlotGear(ItemData itemData) 
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
        // 모든 슬롯이 이미 차있음
        return false; // 아이템을 추가할 수 없음
    }
}


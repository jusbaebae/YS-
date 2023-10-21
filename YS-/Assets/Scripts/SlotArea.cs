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

    // �������߰�
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
        // ���� ���� �˻�
        foreach (Slot wslot in wslots)
        {
            if(wslot.data == null)
            {
                // ���� ������ ��� �ִ� ���
                wslot.AddSlot(itemData);
                return true; // �������� ���������� �߰���
            }
        }
        return false;// �������� �߰��� �� ����
    }
    public bool AddItemToSlotGear(ItemData itemData) 
    {
        // ��ű� ���� �˻�
        foreach (Slot gslot in gslots)
        {
            if (gslot.data == null)
            {
                // ��ű� ������ ��� �ִ� ���
                gslot.AddSlot(itemData);
                return true; // �������� ���������� �߰���
            }
        }
        // ��� ������ �̹� ������
        return false; // �������� �߰��� �� ����
    }
    public void levelToSlot(ItemData itemData, int level)
    {
        // ���� ���� �˻�
        foreach (Slot slot in wslots)
        {
            //�������� ��ġ�ϴ°�� ���� �Ҵ�
            if (itemData == slot.data)
            {
                // ���� �Ʒ��� �ؽ�Ʈ�� �����ϰ� ���� �Ҵ�
                Text levelText = slot.GetComponentInChildren<Text>();
                levelText.text = "Lv." + level.ToString();
            }
        }

        // ��ű� ���� �˻�
        foreach (Slot slot in gslots)
        {
            if (itemData == slot.data)
            {
                Text levelText = slot.GetComponentInChildren<Text>();
                levelText.text = "Lv." + level.ToString();
            }
        }

    }
}


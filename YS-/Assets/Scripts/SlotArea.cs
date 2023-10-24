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
    public bool AddItemToSlot(ItemData itemData)
    {
        if (itemData.itemCategory == ItemData.ItemCategory.Weapon)
        {
            // ���� ���� �˻�
            foreach (Slot wslot in wslots)
            {
                if (wslot.data == null)
                {
                    // ���� ������ ��� �ִ� ���
                    wslot.AddSlot(itemData);
                    return true; // �������� ���������� �߰���
                }
            }
        }
        else if (itemData.itemCategory == ItemData.ItemCategory.Brooch)
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
        }
        return false;// �������� �߰��� �� ����
    }
    public void levelToSlot(ItemData itemData, int level)
    {
        if (itemData.itemCategory == ItemData.ItemCategory.Weapon)
        {
            // ���� ���� �˻�
            foreach (Slot slot in wslots)
            {
                //�������� ��ġ�ϴ°�� ���� �Ҵ�
                if (itemData == slot.data)
                {
                    // ���� �Ʒ��� �ؽ�Ʈ�� ������Ʈ�� ã�� ���� �Ҵ�
                    Text levelText = slot.GetComponentInChildren<Text>();
                    levelText.text = "Lv." + level.ToString();
                }
            }
        }
        else if (itemData.itemCategory == ItemData.ItemCategory.Brooch)
        {
            // ��ű� ���� �˻�
            foreach (Slot slot in gslots)
            {
                if (itemData == slot.data)
                {
                    // ���� �Ʒ��� �ؽ�Ʈ�� ������Ʈ�� ã�� ���� �Ҵ�
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
            //�������� ��ġ�ϴ°�� ���� �Ҵ�
            if (itemData == slot.data)
            {
                // ������ �߰�
                slot.AddSlot(upgradeData);
                // ���� �Ʒ��� �ؽ�Ʈ�� ������Ʈ�� ã�� ���� ����
                Text levelText = slot.GetComponentInChildren<Text>();
                levelText.text = "";
            }
        }
    }
}


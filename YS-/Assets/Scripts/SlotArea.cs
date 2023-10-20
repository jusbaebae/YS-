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
    {   // wfullcheck �迭 �ʱ�ȭ
        wfullcheck = new bool[wslots.Length];
        // gfullcheck �迭 �ʱ�ȭ
        gfullcheck = new bool[gslots.Length];
    }

    // �������߰�(����)
    public bool AddItemToSlotWeapon(ItemData itemData)
    {
        // ���� ���� �˻�
        for (int i = 0; i < wslots.Length; i++)
        {
            if (!wfullcheck[i])
            {
                // ���� ������ ��� �ִ� ���
                wslots[i].GetComponent<Slot>().AddSlotWeapon(itemData);
                wfullcheck[i] = true; // ������ �������� ǥ��
                return true; // �������� ���������� �߰���
            }
        }
        // ��� ������ �̹� ������
        return false; // �������� �߰��� �� ����
    }
    // �������߰�(��ű� ���ڵ�� ����)
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

    //�� �������� ���� �Ҵ�
    public void levelToSlot(ItemData itemData, int level)
    {
        // ���� ���� �˻�
        for (int i = 0; i < wslots.Length; i++)
        {
            //�������� ��ġ�ϴ°�� ���� �Ҵ�
            if (itemData == wslots[i].GetComponent<Slot>().data)
            {
                // ���� �Ʒ��� �ؽ�Ʈ�� �����ϰ� ���� �Ҵ�
                Text levelText = wslots[i].GetComponentInChildren<Text>();
                levelText.text = "Lv." + level.ToString();
            }
        }

        // ��ű� ���� �˻�
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


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
    {   // wfullcheck �迭 �ʱ�ȭ
        wfullcheck = new bool[wslots.Length];
        // gfullcheck �迭 �ʱ�ȭ
        gfullcheck = new bool[gslots.Length];
    }

    // �������߰�
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
    public bool AddItemToSlotGear(ItemData itemData) 
    { 
        // ��ű� ���� �˻�
        for (int i = 0; i < gslots.Length; i++)
        {
            if (!gfullcheck[i])
            {
                // ��ű� ������ ��� �ִ� ���
                gslots[i].GetComponent<Slot>().AddSlotGear(itemData);
                gfullcheck[i] = true; // ������ �������� ǥ��
                return true; // �������� ���������� �߰���
            }
        }

        // ��� ������ �̹� ������
        return false; // �������� �߰��� �� ����
    }
}


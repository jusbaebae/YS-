using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vanilla;

public class Slot : MonoBehaviour
{
    public Image slotimage;
    public ItemData data; //�����۵�����

    private void Awake()
    {
        data = null;
    }
    public void SetColor(float _alpha)
    {
        Color color = slotimage.color;
        color.a = _alpha;
        slotimage.color = color;
    } //���� ����
    public void AddSlotWeapon(ItemData wdata)
    {
        data = wdata;
        slotimage.sprite = data.itemIcon;
        SetColor(0.5f);
    } //�����߰�

    public void AddSlotGear(ItemData gdata)
    {
        data = gdata;
        slotimage.sprite = data.itemIcon;
        SetColor(0.5f);
    } //��ű� �߰�
}

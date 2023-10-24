using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vanilla;

public class Slot : MonoBehaviour
{
    public enum SlotType { Weapon, Gear }

    public Image slotimage;
    public ItemData data; //�����۵�����
    public SlotType slotType;
    public void SetColor(float _alpha)
    {
        Color color = slotimage.color;
        color.a = _alpha;
        slotimage.color = color;
    } //���� ����
    public void AddSlot(ItemData data)
    {
        this.data = data;
        slotimage.sprite = data.itemIcon;
        SetColor(0.5f);
    } //�����߰�
}

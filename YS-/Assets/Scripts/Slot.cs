using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vanilla;

public class Slot : MonoBehaviour
{
    public enum SlotType { Weapon, Gear }

    public Image slotimage;
    public ItemData data; //아이템데이터
    public SlotType slotType;
    public void SetColor(float _alpha)
    {
        Color color = slotimage.color;
        color.a = _alpha;
        slotimage.color = color;
    } //투명도 조절
    public void AddSlot(ItemData wdata)
    {
        data = wdata;
        slotimage.sprite = data.itemIcon;
        SetColor(0.5f);
    } //무기추가
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType; //아이템 종류
    public int itemId; //아이템 코드값
    public string itemName; //이름
    public string itemDesc; //?
    public Sprite itemIcon; //이미지

    [Header("# Level Data")]
    public float baseDamage; //기본데미지
    public int baseCount; //기본수량
    public float[] damages; //레벨에따른 데미지
    public int[] counts; //레벨에따른 수량
     

    [Header("# Weapon")]
    public GameObject projectile;
}

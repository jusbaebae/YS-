using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum ItemCategory { Weapon, Brooch }
        public enum ItemType { Melee, Range, Throw, Bounce, Bomb, Boomerang, Clear, Bash, Glove, Shoe, Heal, Magnet, Spinach, Armor, HollowHeart, Pummarola, Clover, Clown }
        [Header("# Main Info")]
        public ItemCategory itemCategory;
        public ItemType itemType;
        public int itemId;
        public string itemName;

        [TextArea]
        public string itemDesc;
        [TextArea]
        public string newitemDesc;
        public Sprite itemIcon;

        [Header("# Level Data")]
        public float baseDamage;
        public int baseCount;
        public float baseSpeed;
        public float baseDurabiliy;
        public float[] damages;
        public int[] counts;
        public float[] durabiliys;

        [Header("# Weapon")]
        public GameObject projecttile;
        public Sprite hand;

    }
}

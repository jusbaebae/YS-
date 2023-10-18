using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum ItemType { Melee, Range, Throw, Bounce, Bomb, Glove, Shoe, Heal, Boomerang, Test}

        [Header("# Main Info")]
        public ItemType itemType;
        public int itemId;
        public string itemName;

        [TextArea]
        public string itemDesc;
        public Sprite itemIcon;

        [Header("# Level Data")]
        public float baseDamage;
        public int baseCount;
        public float baseSpeed;
        public float[] damages;
        public int[] counts;

        [Header("# Weapon")]
        public GameObject projecttile;
        public Sprite hand;

    }
}

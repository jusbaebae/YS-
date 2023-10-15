using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
    public class ItemData : ScriptableObject
    {
<<<<<<< HEAD
        public enum ItemType { Melee, Range, Throw, Bounce, Bomb, Glove, Shoe, Heal, Boomerang}
=======
        public enum ItemType { Melee, Range, Throw, Bounce, Glove, Shoe, Heal }
>>>>>>> main

        [Header("# Main Info")]
        public ItemType itemType;
        public int itemId;
        public string itemName;
<<<<<<< HEAD

=======
>>>>>>> main
        [TextArea]
        public string itemDesc;
        public Sprite itemIcon;

        [Header("# Level Data")]
        public float baseDamage;
        public int baseCount;
<<<<<<< HEAD
        public float baseSpeed;
=======
>>>>>>> main
        public float[] damages;
        public int[] counts;

        [Header("# Weapon")]
        public GameObject projecttile;
        public Sprite hand;

    }
}

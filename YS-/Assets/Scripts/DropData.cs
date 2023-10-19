using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    [CreateAssetMenu(fileName = "Drop", menuName = "Scriptable Object/DropData")]
    public class DropData : ScriptableObject
    {
        public enum ItemType {Heal, Magnet, Rosary, Clover, Chest}

        [Header("# Main Info")]
        public ItemType itemType;
        public int itemId;
        public Sprite itemIcon;
    }
}
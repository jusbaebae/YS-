using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace vanilla
{
    public class Item : MonoBehaviour
    {
        public ItemData data;
        public int level;
        public Weapon weapon;
        public Gear gear;

        Image icon;
        Text textlevel;
        Text textName, textDesc;
        SlotArea slot;
        private void Awake()
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;
            Text[] texts = GetComponentsInChildren<Text>();
            textlevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];
            textName.text = data.itemName;
            slot = FindAnyObjectByType<SlotArea>();
        }

        private void OnEnable()
        {
            textlevel.text = "Lv." + (level + 1);
            int counts = data.baseCount;
            float dura = data.baseDurabiliy;
            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                    for (int i = 0; i < level + 1; i++)
                    {
                        counts += data.counts[i];
                        dura += data.durabiliys[i];
                    }
                    if (level == 0)
                        textDesc.text = string.Format(data.newitemDesc, data.baseDamage, data.baseCount, data.baseDurabiliy, data.baseSpeed);
                    else
                        textDesc.text = string.Format(data.itemDesc, data.baseDamage * data.damages[level - 1], data.baseDamage * data.damages[level], counts - data.counts[level], counts, dura - data.durabiliys[level], dura);
                    break;
                case ItemData.ItemType.Range:
                case ItemData.ItemType.Bounce:
                case ItemData.ItemType.Throw:
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.Boomerang:
                    for (int i = 0; i < level + 1; i++)
                        counts += data.counts[i];
                    if (level == 0)
                        textDesc.text = string.Format(data.newitemDesc, data.baseDamage, data.baseCount, data.baseSpeed);
                    else
                        textDesc.text = string.Format(data.itemDesc, data.baseDamage * data.damages[level - 1], data.baseDamage * data.damages[level], counts - data.counts[level], counts);
                    break;
                case ItemData.ItemType.Clear:
                    for (int i = 0; i < level + 1; i++)
                        dura += data.durabiliys[i];
                    if (level == 0)
                        textDesc.text = string.Format(data.newitemDesc, data.baseSpeed);
                    else
                        textDesc.text = string.Format(data.itemDesc, data.baseSpeed - (data.baseSpeed * (dura - data.durabiliys[level])), data.baseSpeed - (data.baseSpeed * dura));
                    break;
                case ItemData.ItemType.Bash:
                    for (int i = 0; i < level + 1; i++)
                        dura += data.durabiliys[i];
                    if (level == 0)
                        textDesc.text = string.Format(data.newitemDesc, data.baseDamage, data.baseDurabiliy);
                    else
                        textDesc.text = string.Format(data.itemDesc, data.baseDamage * data.damages[level - 1], data.baseDamage * data.damages[level],
                            data.baseDurabiliy + (data.baseDurabiliy * (dura - data.durabiliys[level])), data.baseDurabiliy + (data.baseDurabiliy * dura));
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                case ItemData.ItemType.Magnet:
                case ItemData.ItemType.Armor:
                case ItemData.ItemType.Spinach:
                case ItemData.ItemType.Pummarola:
                case ItemData.ItemType.HollowHeart:
                case ItemData.ItemType.Clover:
                case ItemData.ItemType.Clown:
                    textDesc.text = string.Format(data.itemDesc, data.damages[level]);
                    break;
                default:
                    textDesc.text = string.Format(data.itemDesc);
                    break;
            }
        }

        public void onClick()
        {
            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                    if (level == 0)
                    {
                        GameObject newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                        if (GameManager.inst.weaponCount < 6)
                        {
                            slot.AddItemToSlotWeapon(data);
                            slot.levelToSlot(data, level + 1);
                            GameManager.inst.weaponCount++;
                        }
                    }
                    else
                    {
                        float nextDamage = data.baseDamage;
                        int nextCount = 0;
                        float nextDura = 0;
                        nextDamage *= data.damages[level];
                        nextCount += data.counts[level];
                        nextDura += data.durabiliys[level];
                        slot.levelToSlot(data, level + 1);
                        weapon.LevelUp(nextDamage, nextCount, nextDura);
                    }
                    level++;
                    break;
                case ItemData.ItemType.Range:
                case ItemData.ItemType.Throw:
                case ItemData.ItemType.Bounce:
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.Boomerang:
                    if (level == 0)
                    {
                        GameObject newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                        if (GameManager.inst.weaponCount < 6)
                        {
                            slot.AddItemToSlotWeapon(data);
                            slot.levelToSlot(data, level + 1);
                            GameManager.inst.weaponCount++;
                        }
                    }
                    else
                    {
                        float nextDamage = data.baseDamage;
                        int nextCount = 0;
                        nextDamage += data.baseDamage * data.damages[level];
                        nextCount += data.counts[level];
                        slot.levelToSlot(data, level + 1);
                        weapon.LevelUp(nextDamage, nextCount, 1);
                    }
                    level++;
                    break;
                case ItemData.ItemType.Clear:
                case ItemData.ItemType.Bash:
                    if (level == 0)
                    {
                        GameObject newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                        if (GameManager.inst.weaponCount < 6)
                        {
                            slot.AddItemToSlotWeapon(data);
                            slot.levelToSlot(data, level + 1);
                            GameManager.inst.weaponCount++;
                        }
                    }
                    else
                    {
                        float nextDamage = data.baseDamage;
                        float nextDura = 0;
                        nextDamage *= data.damages[level];
                        nextDura += data.durabiliys[level];
                        slot.levelToSlot(data, level + 1);
                        weapon.LevelUp(nextDamage, 0, nextDura);
                    }
                    level++;
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                case ItemData.ItemType.Magnet:
                case ItemData.ItemType.Armor:
                case ItemData.ItemType.Spinach:
                case ItemData.ItemType.Pummarola:
                case ItemData.ItemType.HollowHeart:
                case ItemData.ItemType.Clover:
                case ItemData.ItemType.Clown:
                    if (level == 0)
                    {
                        GameObject newGear = new GameObject();
                        gear = newGear.AddComponent<Gear>();
                        gear.Init(data);
                        if (GameManager.inst.gearCount < 6)
                        {
                            slot.AddItemToSlotGear(data);
                            slot.levelToSlot(data, level + 1);
                            GameManager.inst.gearCount++;
                        }
                    }
                    else
                    {
                        float nextrate = data.damages[level];
                        slot.levelToSlot(data, level + 1);
                        gear.Levelup(nextrate);
                    }
                    level++;
                    break;
                case ItemData.ItemType.Heal:
                    GameManager.inst.health = GameManager.inst.maxHealth;
                    break;
                default:
                    break;
            }
            if (level == data.damages.Length)
                GetComponent<Button>().interactable = false;
        }
    }
}

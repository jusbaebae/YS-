using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace vanilla
{
    public class Item : MonoBehaviour
    {
        public ItemData data;
        public int level;
        public Weapon weapon;
        public Gear gear;
        public int weaponchoice;
        public int gearchoice;

        Image icon;
        Text textlevel;
        Text textName, textDesc;
        LevelUp lev;
        public SlotArea slot;
        private void Awake()
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;
            Text[] texts = GetComponentsInChildren<Text>();
            textlevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];
            textName.text = data.itemName;
            lev = GetComponentInParent<LevelUp>();
            igiya();
            Init();
        }

        private void OnEnable()
        {
            textlevel.text = "Lv." + (level + 1);
            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.Range:
                case ItemData.ItemType.Bounce:
                case ItemData.ItemType.Throw:
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.Boomerang:
                    textDesc.text = string.Format(data.itemDesc, data.damages[level], data.counts[level]);
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                case ItemData.ItemType.Magnet:
                case ItemData.ItemType.Test:
                case ItemData.ItemType.Test2:
                case ItemData.ItemType.Test3:
                case ItemData.ItemType.Test4:
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
                case ItemData.ItemType.Range:
                case ItemData.ItemType.Throw:
                case ItemData.ItemType.Bounce:
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.Boomerang:
                case ItemData.ItemType.Test:
                case ItemData.ItemType.Test2:
                case ItemData.ItemType.Test3:
                case ItemData.ItemType.Test4:
                    if (level == 0)
                    {
                        GameObject newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                        if (lev.weaponmax < 3)
                        {
                            slot = FindAnyObjectByType<SlotArea>();
                            slot.AddItemToSlotWeapon(data);
                            lev.weaponmax++;
                            weaponchoice++;
                        }
                    }
                    else
                    {
                        float nextDamage = data.baseDamage;
                        int nextCount = 0;
                        nextDamage += data.baseDamage * data.damages[level];
                        nextCount += data.counts[level];
                        weapon.LevelUp(nextDamage, nextCount);
                    }
                    level++;
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                case ItemData.ItemType.Magnet:
                    if (level == 0)
                    {
                        GameObject newGear = new GameObject();
                        gear = newGear.AddComponent<Gear>();
                        gear.Init(data);
                        if (lev.gearmax < 3)
                        {
                            slot = FindAnyObjectByType<SlotArea>();
                            slot.AddItemToSlotGear(data);
                            lev.gearmax++;
                            gearchoice++;
                        }
                    }
                    else
                    {
                        float nextrate = data.damages[level];
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
        void igiya()//무기랑 장신구 구분하는 코드
        {
            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.Range:
                case ItemData.ItemType.Bounce:
                case ItemData.ItemType.Throw:
                case ItemData.ItemType.Bomb:
                case ItemData.ItemType.Boomerang:
                    lev.weaponcount++;
                    weaponchoice++;
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                case ItemData.ItemType.Magnet:
                    lev.gearcount++;
                    gearchoice++;
                    break;
                default:
                    break;
            }
        }

        public void Init()
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;
            Text[] texts = GetComponentsInChildren<Text>();
            textlevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];
            textName.text = data.itemName;
        }
    }
}

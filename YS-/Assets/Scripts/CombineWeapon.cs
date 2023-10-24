using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class CombineWeapon : MonoBehaviour
    {
        public List<CombineData> combineDatas;
        SlotArea slots;
        public void IsAbleToCombine(ItemData data)
        {
            slots = FindAnyObjectByType<SlotArea>();
            if (data.itemCategory == ItemData.ItemCategory.Weapon)
            {
                foreach (CombineData combineData in combineDatas)
                {
                    if (combineData.weapon.data == data)
                    {
                        if (combineData.weapon.level >= 5 && combineData.gear.level >= 1)
                        {
                            slots.UpgradeWeapon(data, combineData.resultWeapon);
                            //강화 무기 반환
                            combineData.weapon.UpgradeWeapon(combineData.resultWeapon);
                        }

                        break;
                    }
                }
            }
            else if (data.itemCategory == ItemData.ItemCategory.Brooch)
            {
                foreach (CombineData combineData in combineDatas)
                {
                    if (combineData.gear.data == data)
                    {
                        if (combineData.weapon.level >= 5 && combineData.gear.level >= 1)
                        {
                            slots.UpgradeWeapon(combineData.weapon.data, combineData.resultWeapon);
                            //강화 무기 반환
                            combineData.weapon.UpgradeWeapon(combineData.resultWeapon);
                        }

                        break;
                    }
                }
            }
        }
    }
    [System.Serializable]
    public class CombineData
    {
        public Item weapon;
        public Item gear;
        public ItemData resultWeapon;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace vanilla
{
    public class Gear : MonoBehaviour
    {
        public ItemData.ItemType type;
        public float rate;
        public bool regen;
        float sec;

        private void Update()
        {
            if (regen)
            {
                sec += Time.deltaTime;
                if (sec >= 1f)
                {
                    GameManager.inst.health += rate;
                    sec = 0;
                }
            }
        }
        public void Init(ItemData data)
        {
            //Basic Set
            name = "Gear" + (data.itemId);
            transform.parent = GameManager.inst.player.transform;
            transform.localPosition = Vector3.zero;
            //Property Set
            type = data.itemType;
            rate = data.damages[0];
            LevelUpGear();
        }
        public void Levelup(float rate)
        {
            this.rate = rate;
            LevelUpGear();
        }
        void LevelUpGear()
        {
            switch (type)
            {
                case ItemData.ItemType.Glove:
                    RateUp();
                    break;
                case ItemData.ItemType.Shoe:
                    SpeedUp();
                    break;
                case ItemData.ItemType.Heal:
                    break;
                case ItemData.ItemType.Magnet:
                    MagnetUp();
                    break;
                case ItemData.ItemType.Armor:
                    ArmorUp();
                    break;
                case ItemData.ItemType.Spinach:
                    AttackUp();
                    break;
                case ItemData.ItemType.HollowHeart:
                    HealthUp();
                    break;
                case ItemData.ItemType.Pummarola:
                    RegenUp();
                    break;
                case ItemData.ItemType.Clover:
                    LuckUp();
                    break;
                case ItemData.ItemType.Clown:
                    ExpBonusUp();
                    break;
            }
        }
        void ApplyGear()
        {
            switch (type)
            {
                case ItemData.ItemType.Glove:
                    RateUp();
                    break;
                case ItemData.ItemType.Shoe:
                    SpeedUp();
                    break;
                case ItemData.ItemType.Heal:
                    break;
                case ItemData.ItemType.Magnet:
                    MagnetUp();
                    break;
            }
        }
        void ExpBonusUp()
        {
            GameManager.inst.player.expBonus = rate;
        }
        void LuckUp()
        {
            GameManager.inst.player.luck += rate;
            GameManager.inst.player.critical += rate;
        }
        void RegenUp()
        {
            regen = true;
        }
        void HealthUp()
        {
            GameManager.inst.SetMaxHP( GameManager.inst.originHealth * rate);
        }
        void ArmorUp()
        {
            GameManager.inst.player.defend = GameManager.inst.player.baseDefend - (GameManager.inst.player.baseDefend * rate);
        }
        void AttackUp()
        {
            GameManager.inst.player.attack = GameManager.inst.player.baseAttack + rate;
        }
        void RateUp()
        {
            Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in weapons)
            {
                switch (weapon.id)
                {
                    case 0:
                    case 200:
                        weapon.speed = weapon.baseSpeed + (weapon.baseSpeed * rate);
                        break;
                    case 1:
                        weapon.speed = 0.5f * (1f - rate);
                        break;
                    case 2:
                        weapon.speed = weapon.baseSpeed - rate * 3;
                        break;
                    case 6:
                        break;
                    default:
                        weapon.speed = weapon.baseSpeed - (weapon.baseSpeed * rate)/2;
                        break;
                }
            }
        }
        void SpeedUp()
        {
            float speed = 5f;
            GameManager.inst.player.speed = speed + speed * rate;
        }

        void MagnetUp()
        {
            DropItem[] dropItems = GameManager.inst.pool.gameObject.GetComponentsInChildren<DropItem>();
            foreach (DropItem dropitem in dropItems)
                if (dropitem.type == ItemType.Exp)
                    dropitem.magnetDistance = 2 + 2 * rate;
        }
    }
}

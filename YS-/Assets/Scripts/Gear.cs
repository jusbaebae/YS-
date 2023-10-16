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

        public void Init(ItemData data)
        {
            //Basic Set
            name = "Gear" + (data.itemId - 2);
            transform.parent = GameManager.inst.player.transform;
            transform.localPosition = Vector3.zero;
            //Property Set
            type = data.itemType;
            rate = data.damages[0];
            ApplyGear();
        }
        public void Levelup(float rate)
        {
            this.rate = rate;
            ApplyGear();
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
        void RateUp()
        {
            Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
            foreach (Weapon weapon in weapons)
            {
                switch (weapon.id)
                {
                    case 0:
                        weapon.speed = weapon.baseSpeed + (weapon.baseSpeed * rate);
                        break;
                    case 1:
                        weapon.speed = 0.5f * (1f - rate);
                        break;
                    case 5:
                        weapon.speed = weapon.baseSpeed - rate * 3;
                        break;
                    default:
                        weapon.speed = weapon.baseSpeed - (weapon.baseSpeed * rate);
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
            Magnet[] magnets = GameManager.inst.pool.gameObject.GetComponentsInChildren<Magnet>();
            foreach (Magnet magnet in magnets)
            {
                magnet.magnetDistance += magnet.magnetDistance * rate;
            }
        }
    }
}

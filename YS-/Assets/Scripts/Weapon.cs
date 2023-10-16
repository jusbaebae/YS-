using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace vanilla
{
    public class Weapon : MonoBehaviour
    {
        public int id;
        public int prefabId;
        public float damage;
        public int count;
        public float speed;

        float timer;
        int[,] thr = new int[5, 5]
        {
        {0,0,0,0,0},
        {-2,2,0,0,0},
        {-2,0,2,0,0},
        {-2,-1,1,2,0},
        {-2,-1,0,1,2}
        };

        Player player;
        private void Awake()
        {
            player = GameManager.inst.player;
        }
        void Update()
        {
            if (!GameManager.inst.isLive)
                return;
            switch (id)
            {
                case 0:
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);
                    break;
                case 1:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Fire();
                    }
                    break;
                case 5:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        if (count >= 6)
                            count = 5;
                        for (int i = 0; i < count; i++)
                        {
                            Throw(thr[Mathf.Abs(count) - 1, i]);
                        }
                    }
                    break;
                case 6:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        if (count >= 6)
                            count = 5;
                        BFire();
                    }
                    break;
                default:
                    break;
            }
        }
        public void Init(ItemData data)
        {
            // Basic Set
            name = "Weapon" + data.itemId;
            transform.parent = player.transform;
            transform.localPosition = Vector3.zero;

            // Property Set
            id = data.itemId;
            damage = data.baseDamage;
            count = data.baseCount;

            for (int i = 0; i < GameManager.inst.pool.prefabs.Length; i++)
            {
                if (data.projecttile == GameManager.inst.pool.prefabs[i])
                {
                    prefabId = i;
                    break;
                }
            }

            switch (id)
            {
                case 0:
                    speed = 150;
                    Batch();
                    break;
                case 1:
                    speed = 0.3f;
                    break;
                case 5:
                    speed = 6f;
                    break;
                case 6:
                    speed = 3f;
                    break;
                default:
                    break;
            }
            if (data.itemType == ItemData.ItemType.Melee || data.itemType == ItemData.ItemType.Range)
            {
                Hands hands = player.hands;
                hands.gameObject.SetActive(true);
                hands.sprites[(int)data.itemType].sprite = data.hand;
            }
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        }
        public void LevelUp(float damage, int count)
        {
            this.damage += damage;
            this.count += count;
            if (id == 0)
                Batch();
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        }
        void Batch()
        {
            for (int i = 0; i < count; i++)
            {
                Transform bullet;
                if (i < transform.childCount)
                    bullet = transform.GetChild(i);
                else
                {
                    bullet = GameManager.inst.pool.Get(prefabId).transform;
                    bullet.parent = transform;
                }

                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                Vector3 rotVec = Vector3.forward * 360 * i / count;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, false); // -1 은 무제한
            }
        }
        void Throw(int i)
        {
            Vector3 dir = transform.position + Vector3.up * 30000000;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.GetComponent<Bullet>().Init(damage, -1, dir, false);
            bullet.GetComponent<Bullet>().Throwing(i);
            Debug.Log("Throwing");
        }
        void Fire()
        {
            if (!player.scanner.nearestTarget)
                return;

            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir, false);
            Debug.Log("Fire");
        }
        void BFire()
        {
            if (!player.scanner.nearestTarget)
                return;

            Vector3 targetPos = player.scanner.nearestTarget.position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir, true);
            Debug.Log("Bullet");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace vanilla
{
    public class Weapon : MonoBehaviour
    {
        public int id;
        public int prefabId;
        public float damage;
        public int count;
        public float speed;
        public float baseSpeed;
        Vector3[] dir;

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
            dir = new Vector3[10];
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
                case 7:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Bomb();
                    }
                    break;
                case 8:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Boomerang();
                        
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
            baseSpeed = data.baseSpeed;
            speed = baseSpeed;
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
                    Batch();
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
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   //만든 무기에 장갑속도 적용
        }
        void Targeting()
        {
            int i = 0;
            foreach(Transform pos in player.scanner.nearestTarget)
            {
                if(pos != null)
                {
                    Vector3 targetPos = pos.position;
                    dir[i] = targetPos - transform.position;
                    dir[i] = dir[i].normalized;
                    i++;
                }
            }
        }
        void ShotEnemy(int cnt, bool bounce, bool bomb, bool boomerang, bool rotate)
        {
            Transform[] bullets = new Transform[count];
            for (int index = 0; index < count; index++)
                bullets[index] = GameManager.inst.pool.Get(prefabId).transform;

            int i = 0;

            foreach (Transform bullet in bullets)
            {
                bullet.position = transform.position;
                if (dir[i] == null || dir[i] == Vector3.zero)
                {
                    Vector3 randvec = transform.position + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                    Vector3 randdir = randvec - transform.position;
                    randdir = randdir.normalized;
                    bullet.rotation = Quaternion.FromToRotation(Vector3.up, randdir);
                    bullet.GetComponent<Bullet>().Init(damage, cnt, randdir, bounce, bomb, boomerang, rotate);
                }
                else
                {
                    bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir[i]);
                    bullet.GetComponent<Bullet>().Init(damage, cnt, dir[i], bounce, bomb, boomerang, rotate);
                    i++;
                }
            }
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
                bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, false, false, false, false); // -1 은 무제한
            }
        }

        void Throw(int i)
        {
            Vector3 dir = transform.position + Vector3.up * 30000000;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.GetComponent<Bullet>().Init(damage, -1, dir, false, false, false, true);
            bullet.GetComponent<Bullet>().Throwing(i);
        }
        void Fire()
        {
            if (!player.scanner.nearestTarget[0])
                return;

            Vector3 targetPos = player.scanner.nearestTarget[0].position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, dir, false, false, false, false);
        }
        void BFire()
        {
            if (!player.scanner.nearestTarget[0])
                return;

            Targeting();
            ShotEnemy(count, true, false, false, false);
        }
        void Bomb()
        {
            if (!player.scanner.nearestTarget[0])
                return;
            Vector3 targetPos = player.scanner.nearestTarget[0].position;
            Vector3 dir = targetPos - transform.position;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.GetComponent<Bullet>().Init(damage, count, Vector3.zero, false, true, false, true);
            StartCoroutine(MoveToDestination(bullet, targetPos));
        }
        void Boomerang()
        {
            if (!player.scanner.nearestTarget[0])
                return;

            Targeting();
            ShotEnemy(9999, false, false, true, true);
        }


        IEnumerator MoveToDestination(Transform bullet, Vector3 targetPos)
        {
            bool isMoving = true;
            float journeyLength = Vector3.Distance(bullet.transform.position, targetPos);
            float startTime = Time.time;
            float moveSpeed = 0.2f;
            //float rotationSpeed = 90.0f;

            while (isMoving)
            {
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float journeyFraction = distanceCovered / journeyLength;
                bullet.transform.position = Vector3.Lerp(bullet.transform.position, targetPos, journeyFraction);
                if (journeyFraction >= 0.05f)
                {
                    isMoving = false;
                    Transform bomb = GameManager.inst.pool.Get(prefabId + 1).transform;
                    bomb.position = bullet.transform.position;
                    bomb.localScale = Vector3.one;
                    bomb.GetComponent<Bullet>().Init(damage, count, Vector3.zero, false, true, false, false);
                    bullet.gameObject.SetActive(false);
                }

                yield return null;
            }
        }
    }
}

using System.Xml.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
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
        public float dura;
        bool isOn;
        Vector3[] dir;
        Vector3[] targetDir;
        RandTarget randTarget;
        Fade fade;
        new CameraController camera;

        float timer;
        float shottimer;
        int shotindex;
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
            targetDir = new Vector3[10];
            randTarget = GameManager.inst.player.GetComponentInChildren<RandTarget>();
            camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        }
        void Update()
        {
            if (!GameManager.inst.isLive)
                return;
            switch (id)
            {
                case 0:
                    timer += Time.deltaTime;
                    if (timer > 6)
                    {
                        timer = 0f;
                        Batch();
                    }
                    break;
                case 1:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Fire();
                    }
                    break;
                case 2:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        shottimer += Time.deltaTime;
                        if (shottimer >= 0.1f)
                        {
                            Throw(thr[Mathf.Abs(count) - 1, shotindex]);
                            shotindex++;
                            shottimer = 0f;
                            if (shotindex == count)
                            {
                                timer = 0;
                                shotindex = 0;
                            }
                        }
                    }
                    break;
                case 3:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        shottimer += Time.deltaTime;
                        if (shottimer >= 0.1f)
                        {
                            BFire();
                            shotindex++;
                            shottimer = 0f;
                            if (shotindex == count)
                            {
                                timer = 0;
                                shotindex = 0;
                            }
                        }
                    }
                    break;
                case 4:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        shottimer += Time.deltaTime;
                        if (shottimer >= 0.1f)
                        {
                            Bomb();
                            shotindex++;
                            shottimer = 0f;
                            if (shotindex == count)
                            {
                                timer = 0;
                                shotindex = 0;
                            }
                        }
                    }
                    break;
                case 5:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        shottimer += Time.deltaTime;
                        if (shottimer >= 0.1f)
                        {
                            Boomerang();
                            shotindex++;
                            shottimer = 0f;
                            if (shotindex == count)
                            {
                                timer = 0;
                                shotindex = 0;
                            }
                        }
                    }
                    break;
                case 6:
                    timer += Time.deltaTime;
                    if (timer > speed - (speed * dura))
                    {
                        timer = 0f;
                        Rosary();
                    }
                    break;
                case 7:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        timer = 0f;
                        Bash();
                    }
                    break;
                case 200:
                    transform.Rotate(Vector3.back * speed * Time.deltaTime);
                    timer += Time.deltaTime;
                    if (timer > 3)
                    {
                        timer = 0f;
                        Batch();
                    }
                    break;
                case 205:
                    timer += Time.deltaTime;
                    if (timer > speed)
                    {
                        shottimer += Time.deltaTime;
                        if (shottimer >= 0.1f)
                        {
                            Boomerang();
                            shotindex++;
                            shottimer = 0f;
                            if (shotindex == count)
                            {
                                timer = 0;
                                shotindex = 0;
                            }
                        }
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
            dura = data.baseDurabiliy;
            for (int i = 0; i < GameManager.inst.pool.prefabs.Length; i++)
            {
                if (data.projecttile == GameManager.inst.pool.prefabs[i])
                {
                    prefabId = i;
                    break;
                }
            }
            if (data.itemType == ItemData.ItemType.Melee || data.itemType == ItemData.ItemType.Range)
            {
                Hands hands = player.hands;
                hands.gameObject.SetActive(true);
                hands.sprites[(int)data.itemType].sprite = data.hand;
            }
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   //만든 무기에 장갑속도 적용
        }
        public void Upgrade(ItemData data)
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
            dura = data.baseDurabiliy;
            for (int i = 0; i < GameManager.inst.pool.prefabs.Length; i++)
            {
                if (data.projecttile == GameManager.inst.pool.prefabs[i])
                {
                    prefabId = i;
                    break;
                }
            }
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);   //만든 무기에 장갑속도 적용
        }
        void Targeting()
        {
            int i = 0;
            foreach (Transform pos in player.scanner.nearestTarget)
            {
                targetDir[i] = Vector3.zero;
                dir[i] = Vector3.zero;
                if (pos != null)
                {
                    Vector3 targetPos = pos.position;
                    targetDir[i] = targetPos;
                    dir[i] = targetPos - transform.position;
                    dir[i] = dir[i].normalized;
                    i++;
                }
            }
        }
        void Shot(Transform bullet, float cnt)
        {
            int i = shotindex;
            bullet.position = transform.position;
            if (dir[i] == null || dir[i] == Vector3.zero)
            {
                Vector3 randvec = randTarget.RandDir();
                Vector3 randdir = randvec - transform.position;
                randdir = randdir.normalized;
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, randdir);
                bullet.GetComponent<Bullet>().Init(damage, cnt, randdir, id);
            }
            else
            {
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir[i]);
                bullet.GetComponent<Bullet>().Init(damage, cnt, dir[i], id);
            }
        }
        void DestShot(float cnt, float moveSpeed)
        {
            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            int i = shotindex;
            bullet.position = transform.position;
            if (dir[i] == null || dir[i] == Vector3.zero)
            {
                Vector3 randvec = randTarget.RandDir();
                Vector3 randdir = randvec - transform.position;
                randdir = randdir.normalized;
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, randdir);
                bullet.GetComponent<Bullet>().Init(damage, cnt, Vector3.zero, id);
                StartCoroutine(MoveToDestination(bullet, randvec, moveSpeed));
            }
            else
            {
                bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir[i]);
                bullet.GetComponent<Bullet>().Init(damage, cnt, Vector3.zero, id);
                switch (id)
                {
                    case 4:
                        StartCoroutine(MoveToDestination(bullet, targetDir[i], moveSpeed));
                        break;
                    case 5:
                    case 205:
                        StartCoroutine(MoveToDestination(bullet, targetDir[i], moveSpeed));
                        break;
                }
            }
        }

        public void LevelUp(float damage, int count, float dura)
        {
            this.damage = damage;
            this.count += count;
            this.dura += dura;
            player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        }
        void Batch()
        {
            Transform[] bullet = new Transform[5];
            for (int i = 0; i < count; i++)
            {
                bullet[i] = GameManager.inst.pool.Get(prefabId).transform;
                bullet[i].parent = transform;
                bullet[i].localPosition = Vector3.zero;
                bullet[i].localRotation = Quaternion.identity;
                bullet[i].localScale = Vector3.zero;
                Bullet bul = bullet[i].GetComponent<Bullet>();
                bul.Init(damage, -1, Vector3.zero, id); // -1 은 무제한
                if (id == 0)
                    bul.RotateSet(count, i, true, speed, 2.5f);
                else
                    bul.RotateSet(count, i, true, speed, 3.5f);
                StartCoroutine(SetOff(bul));
            }
        }

        void Throw(int i)
        {
            Vector3 dir = transform.position + Vector3.up * 30000000;
            dir = dir.normalized;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.GetComponent<Bullet>().Init(damage, -1, dir, id);
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
            bullet.GetComponent<Bullet>().Init(damage, count, dir, id);
        }
        void BFire()
        {
            Targeting();
            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            Shot(bullet, dura);
        }
        void Bomb()
        {
            Targeting();
            DestShot(dura, 0.2f);
        }
        void Boomerang()
        {
            Targeting();
            switch (id)
            {
                case 5:
                    DestShot(9999, 0.2f);
                    break;
                case 205:
                    DestShot(9999, 0.2f);
                    break;
            }
        }
        void Rosary()
        {
            float randn = Random.value;
            if (randn >= GameManager.inst.player.luck * 0.01111111112)
            {
                fade = FindAnyObjectByType<Fade>();
                StartCoroutine(fade.FadeImage(fade.redImage, 0.0f));
                camera.VibrateForTime(0.3f);
                DropItem[] drops = GameManager.inst.pool.GetComponentsInChildren<DropItem>();
                foreach (DropItem d in drops)
                {
                    d.gameObject.SetActive(false);
                }
                GameManager.inst.ClearField(true);
                return;
            }
            else
            {
                fade = FindAnyObjectByType<Fade>();
                StartCoroutine(fade.FadeImage(fade.blueImage, 0.0f));
                camera.VibrateForTime(0.3f);
                GameManager.inst.ClearField(false);
            }
        }
        void Bash()
        {
            if (!player.scanner.nearestTarget[0])
                return;

            Transform bullet = GameManager.inst.pool.Get(prefabId).transform;
            bullet.localScale = Vector3.one * dura;
            bullet.position = player.scanner.nearestTarget[0].position;
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, id);
        }

        IEnumerator MoveToDestination(Transform bullet, Vector3 targetPos, float moveSpeed)
        {
            float journeyLength = Vector3.Distance(bullet.transform.position, targetPos);
            float startTime = Time.time;
            while (true)
            {
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float journeyFraction = distanceCovered / journeyLength;

                bullet.transform.position = Vector3.Lerp(bullet.transform.position, targetPos, journeyFraction);
                if (journeyFraction >= 0.05f && id == 4)
                {
                    Transform bomb = GameManager.inst.pool.Get(prefabId + 1).transform;
                    bomb.position = bullet.transform.position;
                    bomb.localScale = Vector3.one;
                    bomb.GetComponent<Bullet>().Init(damage, dura, Vector3.zero, -1);
                    bullet.gameObject.SetActive(false);
                    break;
                }
                else if (journeyFraction >= 0.05f && id == 5)
                {
                    Vector3 dir = transform.position - bullet.position;
                    dir = dir.normalized;
                    bullet.GetComponent<Bullet>().Init(damage, 9999, dir, id);
                    break;
                }
                else if (journeyFraction >= 0.04f && id == 205)
                {
                    Vector3 dir = transform.position - bullet.position;
                    dir = dir.normalized;
                    Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, dir);

                    float elapsedTime = 0f;
                    while (elapsedTime < 0.5f)
                    {
                        elapsedTime += Time.deltaTime * 1.5f; // 회전 속도 적용
                        bullet.rotation = Quaternion.Slerp(bullet.rotation, targetRotation, elapsedTime * 2);
                        yield return null;
                    }
                    bullet.GetComponent<Bullet>().Init(damage, 9999, dir, id);
                    break;
                }
                yield return null;
            }
        }
        IEnumerator SetOff(Bullet bullet)
        {
            yield return new WaitForSeconds(dura);
            bullet.GetComponent<Bullet>().setOn = false;
        }
    }
}

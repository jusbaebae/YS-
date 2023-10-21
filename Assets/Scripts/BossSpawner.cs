using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class BossSpawner : MonoBehaviour
    {
        public Transform[] spawnPoint;
        int level;
        float timer;
        public BossSpawnData[] bossSpawnData;
        void Awake()
        {
            spawnPoint = GetComponentsInChildren<Transform>();
        }
        void Update()
        {
            if (!GameManager.inst.isLive)
                return;
            timer += Time.deltaTime;
            level = Mathf.Min(Mathf.FloorToInt(GameManager.inst.gameTime / 10f), bossSpawnData.Length - 1);
            if (timer > bossSpawnData[level].spawnTime)
            {
                Spawn();
                timer = 0f;
            }
        }

        void Spawn()
        {
            GameObject enemy = GameManager.inst.pool.Get(GameManager.inst.pool.prefabs.Length-2);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<BossEnemy>().Init(bossSpawnData[level]);
        }
    }
    [System.Serializable]
    public class BossSpawnData
    {
        public int spriteType;
        public float spawnTime;
        public float health;
        public float speed;
    }
}


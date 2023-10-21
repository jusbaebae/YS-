using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] spawnPoint;
        int level;
        float timer;
        public SpawnData[] spawnData;
        void Awake()
        {
            spawnPoint = GetComponentsInChildren<Transform>();
        }
        void Update()
        {
            if (!GameManager.inst.isLive || GameManager.inst.noExp)
                return;
            timer += Time.deltaTime;
            level = Mathf.Min(Mathf.FloorToInt(GameManager.inst.gameTime / 10f), spawnData.Length -1);

            if (timer > spawnData[level].spawnTime)
            {
                Spawn();
                timer = 0f;
            }
        }

        void Spawn()
        {
            GameObject enemy = GameManager.inst.pool.Get(0);
            enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnData[level]);
        }
    }
    [System.Serializable]
    public class SpawnData
    {
        public int spriteType;
        public float spawnTime;
        public float health;
        public float speed;
    }
}

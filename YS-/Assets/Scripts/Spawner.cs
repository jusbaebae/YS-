using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    float timer;
    int level;

    void Awake()
    {
        //스폰포인트 초기화
        spawnPoint = GetComponentsInChildren<Transform>(); //자식의 컴포넌트를 가져옴(Point)
    }
    void Update()
    {
        //타이머설정
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.inst.gameTime / 10f), spawnData.Length - 1); //소환 레벨 설정

        if(timer > spawnData[level].spawnTime) {
            Spawn(); //몬스터 소환
            timer = 0; //몬스터를 소환했으면 타이머초기화
        }
    }

    // 스폰함수 작성
    void Spawn()
    {
        GameObject enemy = GameManager.inst.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    
}

//소환데이터 작성
[System.Serializable] //직렬화
public class SpawnData
{
    public float spawnTime; //소환시간
    public int spriteType; //몬스터이미지
    public int health; //체력
    public float speed; //스피드
}

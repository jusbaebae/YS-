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
        //��������Ʈ �ʱ�ȭ
        spawnPoint = GetComponentsInChildren<Transform>(); //�ڽ��� ������Ʈ�� ������(Point)
    }
    void Update()
    {
        //Ÿ�̸Ӽ���
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.inst.gameTime / 10f), spawnData.Length - 1); //��ȯ ���� ����

        if(timer > spawnData[level].spawnTime) {
            Spawn(); //���� ��ȯ
            timer = 0; //���͸� ��ȯ������ Ÿ�̸��ʱ�ȭ
        }
    }

    // �����Լ� �ۼ�
    void Spawn()
    {
        GameObject enemy = GameManager.inst.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }

    
}

//��ȯ������ �ۼ�
[System.Serializable] //����ȭ
public class SpawnData
{
    public float spawnTime; //��ȯ�ð�
    public int spriteType; //�����̹���
    public int health; //ü��
    public float speed; //���ǵ�
}

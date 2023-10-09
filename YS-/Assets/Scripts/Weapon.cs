using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //���� �����ͼ���
    public int id; //��������
    public int prefabId; //�����̹���
    public float damage; 
    public int count;
    public float speed;
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    float timer;
    void Start()
    {
        Init(); //�������
    }

    void Update()
    {
        switch (id) {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        //�׽�Ʈ �ڵ�
        if(Input.GetButtonDown("Jump")) {
            LevelUp(10, 1);
        }
    }

    //���� ������ ����
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
        {
            //Batch();
        }
    }
    public void Init() 
    {
        switch(id) {
            case 0:
                speed = 150;
                //Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }

    void Batch() //�����ġ
    {
        for(int i = 0; i < count; i++) {
            Transform bullet; 
            if(i < transform.childCount) {
                bullet = transform.GetChild(i);
            } else {
                bullet = GameManager.inst.pool.Get(prefabId).transform;
                bullet.parent = transform; //�θ�Ӽ�����
            } //����������Ʈ Ȱ���� �����ϸ� Ǯ������ ��������
           
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; //���⸦ ��ġ�ϸ鼭 ��ġ�ʱ�ȭ

            Vector3 rotVec = Vector3.forward * 360 * i / count; 
            bullet.Rotate(rotVec); //���� ȸ��
            bullet.Translate(bullet.up * 1.5f, Space.World); //������ y�� ����
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //������ �������� ����¼���(-1�� ���Ѱ���)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; 
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; //�Ѿ��� ������

        Transform bullet = GameManager.inst.pool.Get(prefabId).transform; //�Ѿ� �̹��� ��������
        bullet.position = transform.position; //�Ѿ� ��ġ ����
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}

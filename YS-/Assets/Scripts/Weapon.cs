using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기 데이터설정
    public int id; //무기종류
    public int prefabId; //무기이미지
    public float damage; 
    public int count;
    public float speed;

    float timer;
    Player player;

    void Awake()
    {
        player = GameManager.inst.player;
    }

    void Update()
    {
        if (!GameManager.inst.isLive)
            return;

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

        //테스트 코드
        if(Input.GetButtonDown("Jump")) {
            LevelUp(10, 1);
        }
    }

    //무기 레벨업 설정
    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data) 
    {
        //기본 세팅
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // 프로퍼티 세팅
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;

        for(int i = 0; i < GameManager.inst.pool.prefabs.Length; i++){
            if (data.projectile == GameManager.inst.pool.prefabs[i]) {
                prefabId = i;
                break;
            }
        } //무기 이미지설정

        switch(id) {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    void Batch() //무기배치
    {
        for(int i = 0; i < count; i++) {
            Transform bullet; 
            if(i < transform.childCount) {
                bullet = transform.GetChild(i);
            } else {
                bullet = GameManager.inst.pool.Get(prefabId).transform;
                bullet.parent = transform; //부모속성변경
            } //기존오브젝트 활용후 부족하면 풀링에서 가져오기
           
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity; //무기를 배치하면서 위치초기화

            Vector3 rotVec = Vector3.forward * 360 * i / count; 
            bullet.Rotate(rotVec); //무기 회전
            bullet.Translate(bullet.up * 1.5f, Space.World); //무기의 y축 조절
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); //무기의 데미지와 관통력설정(-1은 무한관통)
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position; 
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized; //총알의 방향계산

        Transform bullet = GameManager.inst.pool.Get(prefabId).transform; //총알 이미지 가져오기
        bullet.position = transform.position; //총알 위치 설정
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}

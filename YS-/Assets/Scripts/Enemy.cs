using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //이동속도
    public float health; //체력
    public float maxHealth; //최대체력
    public RuntimeAnimatorController[] animCon; //애니메이터 컨트롤러(이미지)
    public Rigidbody2D target; //목표설정

    bool isLive; //생존여부
    
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        //초기화
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //몬스터 뒤지면 의미없으니 탈출
        if (!isLive)
            return;
        //위치차이 = 타겟 위치 - 나의 위치
        Vector2 dirVec = target.position - rigid.position;
        //다음 위치구하기
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //물리속도가 이동에 영향을 주지않도록 속도 제거
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        //몬스터가 플레이어를 바라보며 따라오게 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.inst.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    //몬스터 데이터 설정
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}

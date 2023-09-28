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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        //초기화
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.inst.isLive)
            return;

        //몬스터 뒤지면 의미없으니 탈출
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.inst.isLive)
            return;

        if (!isLive)
            return;
        //몬스터가 플레이어를 바라보며 따라오게 설정
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.inst.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
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

    //충돌여부 체크
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if(health > 0) {
            // 맞는 액션
            anim.SetTrigger("Hit");
        } else {
            // 뒤짐
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
            GameManager.inst.kill++;
            GameManager.inst.GetExp();
        }

    }

    IEnumerator KnockBack()
    {
        yield return wait; // 하나의 물리 프레임을 딜레이
        Vector3 playerPos = GameManager.inst.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false); //몬스터뒤지면 안보이게설정
    }
}

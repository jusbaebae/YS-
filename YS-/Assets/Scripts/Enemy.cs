using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //�̵��ӵ�
    public float health; //ü��
    public float maxHealth; //�ִ�ü��
    public RuntimeAnimatorController[] animCon; //�ִϸ����� ��Ʈ�ѷ�(�̹���)
    public Rigidbody2D target; //��ǥ����

    bool isLive; //��������
    
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        //�ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //���� ������ �ǹ̾����� Ż��
        if (!isLive)
            return;
        //��ġ���� = Ÿ�� ��ġ - ���� ��ġ
        Vector2 dirVec = target.position - rigid.position;
        //���� ��ġ���ϱ�
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero; //�����ӵ��� �̵��� ������ �����ʵ��� �ӵ� ����
    }

    void LateUpdate()
    {
        if (!isLive)
            return;
        //���Ͱ� �÷��̾ �ٶ󺸸� ������� ����
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.inst.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    //���� ������ ����
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}

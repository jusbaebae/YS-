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
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    void Awake()
    {
        //�ʱ�ȭ
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

        //���� ������ �ǹ̾����� Ż��
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        if (!GameManager.inst.isLive)
            return;

        if (!isLive)
            return;
        //���Ͱ� �÷��̾ �ٶ󺸸� ������� ����
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

    //���� ������ ����
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    //�浹���� üũ
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());
        if(health > 0) {
            // �´� �׼�
            anim.SetTrigger("Hit");
        } else {
            // ����
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
        yield return wait; // �ϳ��� ���� �������� ������
        Vector3 playerPos = GameManager.inst.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false); //���͵����� �Ⱥ��̰Լ���
    }
}

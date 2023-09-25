using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed; //�̵��ӵ�
    public Rigidbody2D target; //��ǥ����

    bool isLive = true; //��������
    
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        //�ʱ�ȭ
        rigid = GetComponent<Rigidbody2D>();
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
}

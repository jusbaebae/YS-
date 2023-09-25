using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //근접무기 설정
    public float damage;
    public int per; //관통력

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per > -1) {
            rigid.velocity = dir * 15f; //총알 속도조절
        }
    }

    //총알 오브젝트 충돌감지
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if(per == -1) {
            rigid.velocity = Vector2.zero; //물리속도 초기화
            gameObject.SetActive(false);
        }
    }
}

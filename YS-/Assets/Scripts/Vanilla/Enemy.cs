using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Enemy : MonoBehaviour
    {
        float speed;
        float health;
        float maxhealth;
        bool isLive;

        Rigidbody2D target;
        Animator anim;
        Rigidbody2D rigid;
        SpriteRenderer spriter;
        WaitForFixedUpdate wait;
        CapsuleCollider2D coll;
        BoxCollider2D bcol;

        public RuntimeAnimatorController[] animCon;

        void Awake()
        {
            anim = GetComponent<Animator>();
            coll = GetComponent<CapsuleCollider2D>();
            bcol = GetComponent<BoxCollider2D>();
            rigid = GetComponent<Rigidbody2D>();
            spriter = GetComponent<SpriteRenderer>();
            wait = new WaitForFixedUpdate();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!GameManager.inst.isLive)
                return;
            if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
                return;

            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (!GameManager.inst.isLive || !isLive)
                return;
            spriter.flipX = target.position.x < rigid.position.x;
        }

        private void OnEnable()
        {
            target = GameManager.inst.player.GetComponent<Rigidbody2D>();
            isLive = true;
            health = maxhealth;
            coll.enabled = true;
            rigid.simulated = true;
            spriter.sortingOrder = 2;
            anim.SetBool("Dead", false);

        }

        public void Init(SpawnData data)
        {
            anim.runtimeAnimatorController = animCon[data.spriteType];
            speed = data.speed;
            maxhealth = data.health;
            health = data.health;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Bullet") || !isLive)
                return;

            health -= collision.GetComponent<Bullet>().damage;

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                StartCoroutine(KnockBack());
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                GameManager.inst.kill++;
                GameManager.inst.GetExp();
            }
        }

        IEnumerator KnockBack()
        {
            yield return wait;
            Vector3 playerPos = GameManager.inst.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}

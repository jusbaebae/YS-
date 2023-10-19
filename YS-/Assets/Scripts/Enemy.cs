using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
namespace vanilla
{
    public class Enemy : MonoBehaviour
    {
        float speed;
        float health;
        float maxhealth;
        bool isLive;
        float hittime;
        float timer;

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
            hittime = 0.5f;
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
            if (!collision.CompareTag("Bullet") || !isLive || collision.GetComponent<Bullet>().bomb)
                return;

            health -= collision.GetComponent<Bullet>().damage;

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                if (!collision.GetComponent<Bullet>().bomb)
                    StartCoroutine(KnockBack(collision.transform));
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                //GameManager.inst.GetExp();
                GameManager.inst.kill++;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.CompareTag("Bullet") || !isLive || !collision.GetComponent<Bullet>().bomb)
                return;
            timer += Time.deltaTime;
            if (!(timer > hittime))
                return;

            timer = 0f;
            health -= collision.GetComponent<Bullet>().damage;
            if (health > 0)
            {
                anim.SetTrigger("Hit");
                StartCoroutine(Slow());
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                GameManager.inst.kill++;
            }
        }
        IEnumerator Slow()
        {
            speed *= 0.5f;
            yield return new WaitForSeconds(0.5f);
            speed *= 2.0f;

        }
        IEnumerator KnockBack(Transform bullet)
        {
            yield return wait;
            Vector3 bulletPos = bullet.transform.position;
            Vector3 dirVec = transform.position - bulletPos;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        void Dead()
        {
            gameObject.SetActive(false);
            //¸÷Á×À¸¸é °æÇèÄ¡¶³±¸±â
            if (GameManager.inst.noExp)
                return;
            int i = GameManager.inst.pool.prefabs.Length;
            GameObject dropItem = GameManager.inst.pool.Get(i-1);
            dropItem.transform.position = transform.position;
            GameManager.inst.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

        }
    }
}

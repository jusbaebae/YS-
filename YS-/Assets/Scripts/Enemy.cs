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
        Player player;

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
            player = FindAnyObjectByType<Player>();
        }

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
            if (!collision.CompareTag("Bullet") || !isLive || (collision.GetComponent<Bullet>().id == -1))
                return;

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            CritCheck(player.critical);
            if (CritCheck(player.critical) && !GameManager.inst.noExp)
            {
                float attack = collision.GetComponent<Bullet>().damage * 1.5f * GameManager.inst.player.attack;
                DamageController.instance.CreateDamageText(pos, Mathf.FloorToInt(attack), true);
                health -= attack;
            }
            else
            {
                float attack = collision.GetComponent<Bullet>().damage * GameManager.inst.player.attack;
                DamageController.instance.CreateDamageText(pos, Mathf.FloorToInt(attack), false);
                health -= attack;
            }

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                if (!(collision.GetComponent<Bullet>().id == -1))
                    StartCoroutine(KnockBack(collision.transform));
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                GameManager.inst.kill++;
                if (GameManager.inst.isLive && !GameManager.inst.noExp)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.CompareTag("Bullet") || !isLive || !(collision.GetComponent<Bullet>().id == -1))
                return;
            timer += Time.deltaTime;
            if (!(timer > hittime))
                return;
            timer = 0f;

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            CritCheck(player.critical);
            if (CritCheck(player.critical) && !GameManager.inst.noExp)
            {
                float attack = collision.GetComponent<Bullet>().damage * 1.5f * GameManager.inst.player.attack;
                DamageController.instance.CreateDamageText(pos, Mathf.FloorToInt(attack), true);
                health -= attack;
            }
            else
            {
                float attack = collision.GetComponent<Bullet>().damage * GameManager.inst.player.attack;
                DamageController.instance.CreateDamageText(pos, Mathf.FloorToInt(attack), false);
                health -= attack;
            }

            if (health > 0)
            {
                anim.SetTrigger("Hit");
                StartCoroutine(Slow());
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
            }
            else
            {
                isLive = false;
                coll.enabled = false;
                rigid.simulated = false;
                spriter.sortingOrder = 1;
                anim.SetBool("Dead", true);
                GameManager.inst.kill++;
                if(GameManager.inst.isLive && !GameManager.inst.noExp)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
                }
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
            if (GameManager.inst.noExp)
                return;
            int i = GameManager.inst.pool.prefabs.Length;
            GameObject dropItem = GameManager.inst.pool.Get(i-1);
            dropItem.transform.position = transform.position;
            GameManager.inst.player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
        }
        bool CritCheck(float critical)
        {
            int crit = UnityEngine.Random.Range(1, 101); ;
            if (crit <= 20 + (GameManager.inst.player.critical * 100))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

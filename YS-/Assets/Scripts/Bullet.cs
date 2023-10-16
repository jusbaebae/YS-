using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public int per;
        public bool bounce;
        public bool bomb;
        public bool boomerang;
        bool isRotate;

        Vector3 dir;
        Animator anim;
        Rigidbody2D rb;

        private void Awake()
        {
            if (GetComponent<Animator>() != null)
            {
                anim = GetComponent<Animator>();
            }

            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (gameObject.activeSelf && isRotate)
            {
                // 회전 속도 360f = 초당 한바퀴
                float rotationSpeed = 360f;
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
        }
        public void Init(float damage, int per, Vector3 dir, bool bounce, bool bomb, bool boomerang, bool isRotate)
        {
            this.damage = damage;
            this.per = per;
            this.bounce = bounce;
            this.bomb = bomb;
            this.boomerang = boomerang;
            this.isRotate = isRotate;
            this.dir = dir;

            if (per > -1)
            {
                rb.velocity = dir * 15f;
            }
            if (bomb || bounce)
            {
                StartCoroutine(DisableBullet());
            }
        }
        public void Throwing(int i)
        {
            rb.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);
            rb.AddForce(Vector3.right * i, ForceMode2D.Impulse);
            if(GameManager.inst.player.inputVec.x != 0)
                rb.AddForce(Vector3.right * 3f * GameManager.inst.player.inputVec.x, ForceMode2D.Impulse);
            if(GameManager.inst.player.inputVec.y > 0)
                rb.AddForce(Vector3.up * 3f * GameManager.inst.player.inputVec.y, ForceMode2D.Impulse);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (bounce)
            {
                if (collision.CompareTag("XArea"))
                {
                    rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
                }
                else if (collision.CompareTag("YArea"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
                }
            }
            if (!collision.CompareTag("Enemy") || per == -1 || bomb || bounce)
                return;
            if (!boomerang)
            {
                per--;
                if (per == -1)
                {
                    rb.velocity = Vector2.zero;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                boomerang = false;
                StartCoroutine(BackToPlayer(dir));
            }
        }
        IEnumerator BackToPlayer(Vector3 dir)
        {
            for (int i = 0; i<15; i++)
            {
                yield return new WaitForSeconds(0.03f);
                rb.velocity = dir * (15f - i);
            }
            yield return new WaitForSeconds(0.5f);
            Vector3 targetPos = GameManager.inst.player.transform.position;
            Vector3 dirs = targetPos - transform.position;
            dirs = dirs.normalized;

            rb.velocity = dirs * 15f; 
        }
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(3f + 3f * per);
            if (bomb)
            {
                anim.SetTrigger("GasEnd");
                yield return new WaitForSeconds(0.6f);
            }
            gameObject.SetActive(false);
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag("Area"))
                return;
            this.gameObject.SetActive(false);
        }
    }
}

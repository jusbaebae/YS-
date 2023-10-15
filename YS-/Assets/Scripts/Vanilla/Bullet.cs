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

        Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Init(float damage, int per, Vector3 dir, bool bounce)
        {
            this.damage = damage;
            this.per = per;
            this.bounce = bounce;
            if (per > -1)
            {
                rb.velocity = dir * 15f;
            }
            if (bounce)
                StartCoroutine(DisableBullet());

        }
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(3f);
            Debug.Log("È÷È÷");
            gameObject.SetActive(false);
        }
        public void Throwing(int i)
        {
            rb.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);
            rb.AddForce(Vector3.right * i, ForceMode2D.Impulse);
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
            if (!collision.CompareTag("Enemy") || per == -1)
                return;

            per--;

            if (per == -1)
            {
                rb.velocity = Vector2.zero;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag("Area"))
                return;
            this.gameObject.SetActive(false);
        }
    }
}

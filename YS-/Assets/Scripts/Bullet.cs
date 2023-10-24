using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
namespace vanilla
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public float per;
        public bool setOn;
        public int index;
        public int count;
        public int id;
        public float speed;
        public float distance;
        float startTime;
        Vector3 dir;
        Animator anim;
        Rigidbody2D rb;
        Vector3 getScale;

        private void Awake()
        {
            if (GetComponent<Animator>() != null)
            {
                anim = GetComponent<Animator>();
            }
            rb = GetComponent<Rigidbody2D>();
            getScale = gameObject.transform.localScale;
            setOn = true;
        }
        private void OnEnable()
        {
            startTime = Time.time;
        }
        private void Update()
        {
            if (gameObject.activeSelf && (id == 4 || id == 5 || id == 2))
            {
                // ȸ�� �ӵ� 360f = �ʴ� �ѹ���
                float rotationSpeed = 360f;
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            if (id == 0 || id == 200)        // ũ�Ⱑ �۾����� Ŀ�����ϰԲ� �ϴ� �ڵ�
            {
                Rotate();
            }
        }
        public void Init(float damage, float per, Vector3 dir, int id)
        {
            this.damage = damage;
            this.per = per;
            this.dir = dir;
            this.id = id;
            if (per > -1)
                rb.velocity = dir * 15f;
            if (id == -1 || id == 3)
                StartCoroutine(DisableBullet());
        }
        public void Throwing(int i)
        {
            rb.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);
            rb.AddForce(Vector3.right * i, ForceMode2D.Impulse);
            if (GameManager.inst.player.inputVec.x != 0)
                rb.AddForce(Vector3.right * 3f * GameManager.inst.player.inputVec.x, ForceMode2D.Impulse);
            if (GameManager.inst.player.inputVec.y > 0)
                rb.AddForce(Vector3.up * 3f * GameManager.inst.player.inputVec.y, ForceMode2D.Impulse);
        }
        void SetDisable()
        {
            gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (id==3)
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
            if (!collision.CompareTag("Enemy") || per == -1 || id == 3 || id == -1)
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
            gameObject.SetActive(false);
        }
        IEnumerator DisableBullet()
        {
            yield return new WaitForSeconds(per);
            if (id==-1)
            {
                anim.SetTrigger("GasEnd");
                yield return new WaitForSeconds(0.6f);
            }
            gameObject.SetActive(false);
        }
        public void RotateSet(int count, int index, bool setOn, float speed, float distance)
        {
            this.count = count;
            this.index = index;
            this.setOn = setOn;
            this.speed = speed;
            this.distance = distance;
        }
        void Rotate()
        {
            float angle = index * (360.0f / count) + startTime; // �� ������Ʈ �� �����
            angle += Time.time * speed; //�ð��� ������ ���� �ӵ��� ����� �̵��ϴ� �Ÿ� ���
            float radians = angle * Mathf.Deg2Rad;  //Degrees to Radians ������ �������� ġȯ���ش�. 90����� �׿� �´� �Ÿ� ���
            float x = transform.parent.position.x + Mathf.Cos(radians) * distance;  // ���� �������� �ڻ��� ���, �Ʒ� Sin�� ���ΰ��
            float y = transform.parent.position.y + Mathf.Sin(radians) * distance;  // distance�� �÷��̾�κ����� �Ÿ�
            transform.position = new Vector3(x, y, transform.position.z);   // ��ġ ����
            transform.LookAt(transform.parent.transform, Vector3.forward);  //�̰� ���� ����
            transform.eulerAngles = Vector3.zero;                           // rotation�� z�� ����
            if (!setOn && !(gameObject.transform.localScale == Vector3.zero))
            {
                gameObject.transform.localScale -= getScale * 0.02f;
                if (gameObject.transform.localScale == Vector3.zero)
                    SetDisable();
            }
            else if (setOn && !(gameObject.transform.localScale == getScale))
            {
                gameObject.transform.localScale += getScale * 0.02f;
            }

        }
    }
}

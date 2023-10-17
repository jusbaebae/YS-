using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vanilla
{
    public class Magnet : MonoBehaviour
    {
        
        public float moveSpeed; //�ӵ�
        public float magnetDistance; // ����

        public Transform player;

        void Start()
        {
            //�÷��̾� ������Ʈ ã��
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            //�÷��̾� ������ �Ÿ����
            float distance = Vector2.Distance(transform.position, player.position);

            //���� ���� �̳��ϰ�� ������� ����
            if (distance <= magnetDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        //�÷��̾�� �浹�� ����ġ���� �� ������Ʈ ����
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                GameManager.inst.GetExp();
                gameObject.SetActive(false);
            }
        }
    }
}

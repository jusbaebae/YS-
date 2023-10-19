using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vanilla
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] public ItemType type;
        public float moveSpeed; //�ӵ�
        public float magnetDistance; //����

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
                if (type == ItemType.Exp)
                    GameManager.inst.GetExp(1);
                else if (type == ItemType.Item)
                    gameObject.GetComponent<DropItem>().ApplyItem();
                gameObject.SetActive(false);
            }
        }
        public enum ItemType
        {
            Exp,
            Item
        }
    }
}

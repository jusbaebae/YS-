using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vanilla
{
    public class Magnet : MonoBehaviour
    {
        [SerializeField] public ItemType type;
        public float moveSpeed; //속도
        public float magnetDistance; //범위

        public Transform player;

        void Start()
        {
            //플레이어 오브젝트 찾기
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            //플레이어 사이의 거리계산
            float distance = Vector2.Distance(transform.position, player.position);

            //일정 범위 이내일경우 끌어당기게 설정
            if (distance <= magnetDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        //플레이어와 충돌시 경험치증가 및 오브젝트 삭제
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

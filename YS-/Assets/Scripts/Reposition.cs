using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
namespace vanilla
{
    public class Reposition : MonoBehaviour
    {
        Collider2D coll;
        public int num;
        private void Awake()
        {
            coll = GetComponent<Collider2D>();
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag("Area"))
                return;
            // 플레이어의 위치
            Vector3 playerPos = GameManager.inst.player.transform.position;
            // 현 타일맵의 위치
            Vector3 myPos = transform.position;
            // 차이 구하기
            float diffX = Mathf.Abs(playerPos.x - myPos.x);
            float diffY = Mathf.Abs(playerPos.y - myPos.y);

            // 입력 값 확인
            Vector3 playerDir = GameManager.inst.player.inputVec;
            // 플레이어와 맵 위치 비교 후 이동
            float dirX = myPos.x > playerPos.x ? -1 : 1;
            float dirY = myPos.y > playerPos.y ? -1 : 1;
            // 맵 이동로직
            switch (transform.tag)
            {
                case "Ground":
                    // 대각선
                    if ((int)(diffX - diffY) >= -5 && (int)(diffX - diffY) <= 5 && diffX >= 45 && diffY >= 45)
                    {
                        transform.Translate(Vector3.right * dirX * 96);
                        transform.Translate(Vector3.up * dirY * 96);
                    }
                    else if (diffX > diffY)
                    {
                        transform.Translate(Vector3.right * dirX * 96);
                    }
                    else if (diffX < diffY)
                    {
                        transform.Translate(Vector3.up * dirY * 96);
                        /*
                        Debug.Log(num + " 2");
                        Debug.Log(diffX + " " + diffY);
                        Debug.Log("dirX : " + dirX + "dirY : " + dirY);*/
                    }
                    break;
                case "Enemy":
                    if (coll.enabled)
                    {
                        transform.Translate(playerDir * 60 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f));
                    }
                    break;
            }
        }
    }
}

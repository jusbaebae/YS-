using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
namespace vanilla
{
    public class SetPosition : MonoBehaviour
    {
        Map map;
        void Start()
        {
            map = GameManager.inst.map;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.CompareTag("Ground"))
                return;
            float halfWidth = gameObject.GetComponent<BoxCollider2D>().bounds.size.x * 0.4f;
            float halfHeight = gameObject.GetComponent<BoxCollider2D>().bounds.size.y * 0.4f;

            Vector2 currentPosition = gameObject.transform.position;

            // 置社 X, 置社 Y
            if (currentPosition.x <= map.minX - halfWidth && currentPosition.y <= map.minY - halfHeight)
            {
                gameObject.transform.position = new Vector2(map.minX - halfWidth, map.minY - halfHeight);
            }
            // 置社 X, 置企 Y
            else if (currentPosition.x <= map.minX - halfWidth && currentPosition.y >= map.maxY + halfHeight)
            {
                gameObject.transform.position = new Vector2(map.minX - halfWidth, map.maxY + halfHeight);
            }
            // 置企 X, 置社 Y
            else if (currentPosition.x >= map.maxX + halfWidth && currentPosition.y <= map.minY - halfHeight)
            {
                gameObject.transform.position = new Vector2(map.maxX + halfWidth, map.minY - halfHeight);
            }
            // 置企 X, 置企 Y
            else if (currentPosition.x >= map.maxX + halfWidth && currentPosition.y >= map.maxY + halfHeight)
            {
                gameObject.transform.position = new Vector2(map.maxX + halfWidth, map.maxY + halfHeight);
            }
            else if (map.minX - halfWidth >= currentPosition.x)
            {
                gameObject.transform.position = new Vector2(map.minX - halfWidth * 1.25f, currentPosition.y) ;
            }
            else if (map.maxX + halfWidth <= currentPosition.x)
            {
                gameObject.transform.position = new Vector2(map.maxX + halfWidth, currentPosition.y);
            }
            else if (map.minY - halfHeight >= currentPosition.y)
            {
                gameObject.transform.position = new Vector2(currentPosition.x, map.minY - halfHeight);
            }
            else if (map.maxY + halfHeight <= currentPosition.y)
            {
                gameObject.transform.position = new Vector2(currentPosition.x, map.maxY + halfHeight * 1.25f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
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
            Debug.Log("¼½½º");
            if (map.minX > gameObject.transform.position.x)
            {
                gameObject.transform.position = new Vector2(map.minX, gameObject.transform.position.y);
            }
            if (map.maxX < gameObject.transform.position.x)
            {
                gameObject.transform.position = new Vector2(map.maxX, gameObject.transform.position.y);
            }
            if (map.minY > gameObject.transform.position.y)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, map.minY);
            }
            if (map.maxY < gameObject.transform.position.y)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, map.maxY);
            }
        }
    }
}

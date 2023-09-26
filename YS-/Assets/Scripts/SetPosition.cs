using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    Map map;
    void Start()
    {
        map = GameManager.inst.map;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (map.minX > GameManager.inst.player.transform.position.x)
        {
            GameManager.inst.player.transform.position = new Vector2(map.minX, GameManager.inst.player.transform.position.y);
        }
        if (map.maxX < GameManager.inst.player.transform.position.x)
        {
            GameManager.inst.player.transform.position = new Vector2(map.maxX, GameManager.inst.player.transform.position.y);
        }
        if (map.minY > GameManager.inst.player.transform.position.y)
        {
            GameManager.inst.player.transform.position = new Vector2(GameManager.inst.player.transform.position.x, map.minY);
        }
        if (map.maxY < GameManager.inst.player.transform.position.y)
        {
            GameManager.inst.player.transform.position = new Vector2(GameManager.inst.player.transform.position.x, map.maxY);
        }
    }
}

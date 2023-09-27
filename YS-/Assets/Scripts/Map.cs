using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace vanilla
{
    public class Map : MonoBehaviour
    {
        SpriteRenderer[] sprites;
        public float maxX, maxY, minX, minY;
        private void Start()
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
            CompositeCollider2D comp = GetComponent<CompositeCollider2D>();
            int i = 0;
            float size = 0;
            float offset = 0;
            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprites.Length == 1) // 0인경우 제외
                    break;
                if (i == 1) // 1인경우 -2 못해서 따로
                {
                    float scale = sprites[0].bounds.size.y / sprite.bounds.size.y;
                    sprite.transform.localScale = new Vector2(scale, scale);
                    sprite.transform.position = new Vector2(sprites[0].transform.position.x - (sprites[0].bounds.size.x / 2f) - (sprite.bounds.size.x / 2f), sprite.transform.position.y);
                    offset += sprite.bounds.size.x;
                }
                else if (!(i == 0))
                {
                    float scale = sprites[0].bounds.size.y / sprite.bounds.size.y;
                    sprite.transform.localScale = new Vector2(scale, scale);
                    if (i % 2 == 1)//좌
                    {
                        sprite.transform.position = new Vector2(sprites[i - 2].transform.position.x - (sprites[i - 2].bounds.size.x / 2f) - (sprite.bounds.size.x / 2f), sprite.transform.position.y);
                        offset += sprite.bounds.size.x;
                    }
                    else //우
                        sprite.transform.position = new Vector2(sprites[i - 2].transform.position.x + (sprites[i - 2].bounds.size.x / 2f) + (sprite.bounds.size.x / 2f), sprite.transform.position.y);
                }
                else
                    offset += sprite.bounds.size.x / 2f;
                size += sprite.bounds.size.x;
                i++;
            }
            BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
            col.size = new Vector2(size - GameManager.inst.player.col.bounds.size.x, sprites[0].bounds.size.y - GameManager.inst.player.col.bounds.size.y);
            col.offset = new Vector2((size - offset * 2) / 2f, 0 + GameManager.inst.player.col.offset.y);  //중앙값을 바꾼다.
            Bounds bound = col.bounds;
            col.usedByComposite = true;
            //전달용 크기들
            minX = bound.center.x - (bound.size.x / 2f);
            maxX = bound.center.x + (bound.size.x / 2f);
            minY = bound.center.y - (bound.size.y / 2f);
            maxY = bound.center.y + (bound.size.y / 2f);
        }
    }
}


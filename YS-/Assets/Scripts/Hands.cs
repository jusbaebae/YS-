using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Hands : MonoBehaviour
    {
        public SpriteRenderer[] sprites;

        SpriteRenderer player;

        Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
        Vector3 rightPosReverse = new Vector3(0, -0.15f, 0);
        Quaternion leftRot = Quaternion.Euler(0, 0, -35);
        Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

        private void Awake()
        {
            player = GetComponentInParent<SpriteRenderer>();
            sprites = GetComponentsInChildren<SpriteRenderer>(true);
        }

        private void LateUpdate()
        {
            bool isReverse = player.flipX;
            sprites[0].transform.localRotation = isReverse ? leftRotReverse : leftRot;
            sprites[0].flipY = isReverse;
            sprites[0].sortingOrder = isReverse ? 4 : 6;
            sprites[1].transform.localPosition = isReverse ? rightPosReverse : rightPos;
            sprites[1].flipX = isReverse;
            sprites[1].sortingOrder = isReverse ? 6 : 4;
        }
    }
}

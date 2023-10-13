using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vanilla
{
    public class Follow : MonoBehaviour
    {
        RectTransform rect;
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
        private void FixedUpdate()
        {
            rect.position = Camera.main.WorldToScreenPoint(GameManager.inst.player.transform.position);
        }
    }
}


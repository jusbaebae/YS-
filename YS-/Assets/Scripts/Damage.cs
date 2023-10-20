using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using vanilla;

public class Damage : MonoBehaviour
{
    //폰트 효과
    public float moveSpeed = 0.8f;
    public float alphaSpeed = 0.8f;
    public float destroyTime = 1.2f;


    public Text txt;
    Color alpha;
    public string damage;
    public GameManager gm;

    void Start()
    {
        gm = GetComponent<GameManager>();
        txt = GetComponent<Text>();
        txt.text = damage;
        alpha = txt.color;

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed)); //데미지 점점 위로 이동

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 데미지 투명도
        txt.color = alpha;
    }
}

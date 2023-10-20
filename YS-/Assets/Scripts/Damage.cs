using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using vanilla;

public class Damage : MonoBehaviour
{
    //��Ʈ ȿ��
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
        transform.Translate(new Vector2(0, moveSpeed)); //������ ���� ���� �̵�

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // ������ ����
        txt.color = alpha;
    }
}

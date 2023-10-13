using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject CharUI;

    [SerializeField] private GameObject arrow;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private Image screen;

    private bool state;
    private bool isEffect;
    void Start()
    {
        StartUI.SetActive(true);
        CharUI.SetActive(false);

        arrow.transform.localPosition = new Vector3(-200, -200, 0);
        state = true;
        isEffect = false;

        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            arrow.transform.localPosition = new Vector3(-200, -200, 0);

        }
        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            arrow.transform.localPosition = new Vector3(-200, -350, 0);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Finish();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    private void Finish()
    {
        throw new NotImplementedException();
    }

    private void StartGame()
    {
        StartUI.SetActive(false);
        CharUI.SetActive(true);
    }

    IEnumerator ArrowEffect()
    {
        while(state)
        {
            isEffect = !isEffect;
            arrow.SetActive(isEffect);
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator FadeOut()
    {
        Color color = screen.color;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / 1f;
            screen.color = color;
            yield return new WaitForSeconds(0.001f);
        }
        StartCoroutine(ArrowEffect());
    }

}

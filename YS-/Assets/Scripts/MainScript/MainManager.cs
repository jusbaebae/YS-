using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public MainManager instance;
    public static bool isStart;

    [SerializeField] public Image screen;

    private void Awake()
    {
        instance = this;
        isStart = false;
    }


    private void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(FadeIn());

            }
        }
        
    }

    IEnumerator FadeIn()
    {
        Color color = screen.color;
        color.a = 0f;
        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime / 1f;
            screen.color = color;
            yield return new WaitForSeconds(0.001f);

        }
        SceneManager.LoadScene("MainMenuScene");
    }



}

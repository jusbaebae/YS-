using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] public GameObject fade;

    public static FadeEffect instance;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator FadeIn()
    {
        Color fadeColor = fade.transform.GetComponent<Image>().color;
        Color color = fadeColor;
        color.a = 0f;
        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime / 1f;
            fade.transform.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.001f);
        }
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator FadeOut()
    {
        Color fadeColor = fade.transform.GetComponent<Image>().color;
        Color color = fadeColor;
        color.a = 1f;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / 1f;
            fade.transform.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.001f);
        }
        fade.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    public bool isStart;

    [SerializeField] private TextMeshProUGUI story;

    private void Awake()
    {
        isStart = false;
        story.SetText("");
        
    }

    private void Start()
    {
        StartCoroutine(StoryChat());
    }


    private void Update()
    {
        if (isStart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(FadeEffect.instance.FadeIn());
            }
        }
    }

    IEnumerator StoryChat()
    {
        string content = "대충 연성대가 바이러스에게 점령당했다는 스토리.";
        for (int i = 0; i < content.Length; i++)
        {
            story.SetText(story.text + content[i]);
            yield return new WaitForSeconds(0.1f);
        }
        isStart = true;
    }
}

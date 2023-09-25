using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI chat;
    public static Chat instance;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        chat.text = "";
        StartCoroutine(StoryChat());
    }


    IEnumerator StoryChat()
    {
        string story = "���� �����밡 ���̷������� ���ɴ��ߴٴ� ���丮.";

        for (int i = 0; i < story.Length; i++)
        {
            chat.text += story[i];
            yield return new WaitForSeconds(0.1f);
        }
        MainManager.isStart = true;
    }
}

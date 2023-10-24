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
    [SerializeField] private GameObject UnlockUI;


    void Awake()
    {
        StartUI.SetActive(true);
        CharUI.SetActive(false);
        UnlockUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UnlockUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnlockUI.SetActive(false);
            }
        }

        if (CharUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CharUI.SetActive(false);
                StartUI.SetActive(true);
            }
        }
    }


    public void StartGame()
    {
        StartUI.SetActive(false);
        CharUI.SetActive(true);
    }

    public void OnUnlock()
    {
        UnlockUI.SetActive(true);
    }

    public void Finish()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quiot();
#endif
    }



}

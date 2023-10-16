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


    void Start()
    {
        StartUI.SetActive(true);
        CharUI.SetActive(false);
        UnlockUI.SetActive(false);

        StartCoroutine(FadeEffect.instance.FadeOut());
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
        throw new NotImplementedException();
    }



}

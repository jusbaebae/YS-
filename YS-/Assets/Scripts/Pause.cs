using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using vanilla;

public class Pause : MonoBehaviour
{
    public bool state;
    public GameObject PauseUI;
    private void Awake()
    {
        state = false;
        PauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelUp.state)
        {
            PauseUI.SetActive(true);
            GameManager.inst.Stop();
        }

    }

    public void GamePlay()
    {
        PauseUI.SetActive(false);
        GameManager.inst.Resume();
    }

    public void GameExit()
    {
        SceneManager.LoadScene("Menu");
    }
}

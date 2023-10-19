using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using vanilla;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    public bool state = false;

    private void Start()
    {
        //pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state)
            {
                pauseUI.SetActive(false);
                GameManager.inst.Resume();
                state = !state;
            }
            else
            {
                pauseUI.SetActive(true);
                GameManager.inst.Stop();
                state = !state;
            }

        }
    }

    public void GameExit()
    {
        SceneManager.LoadScene("Menu");
    }
}

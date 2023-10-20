using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using vanilla;

public class Pause : MonoBehaviour
{    public bool state = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (state)
            {
                gameObject.SetActive(false);
                GameManager.inst.Resume();
                state = !state;
            }

            if(!state)
            {
                gameObject.SetActive(true);
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

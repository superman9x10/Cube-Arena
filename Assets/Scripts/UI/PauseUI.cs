using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;
    private void Start()
    {
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseUI.activeInHierarchy)
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            } else
            {
                Resume();
            }
            
        } 
    }

    public void Resume()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}

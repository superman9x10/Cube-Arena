using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingPauseUI;
    private void Start()
    {
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!pauseUI.activeInHierarchy && !settingPauseUI.activeInHierarchy)
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
            else if (settingPauseUI.activeInHierarchy)
            {
                pauseUI.SetActive(true);
                settingPauseUI.SetActive(false);
            }
            else if(pauseUI.activeInHierarchy)
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

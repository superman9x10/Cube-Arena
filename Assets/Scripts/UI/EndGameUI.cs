using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public GameObject loseGameImage;
    public GameObject winGameImage;

    
    private void Start()
    {
        loseGameImage.SetActive(false);
        winGameImage.SetActive(false);
    }

    private void Update()
    {
        if(Spawner.instance.isEndGame)
        {
            showEndGameUI();
        }
    }
    void showEndGameUI()
    {
        if(!PlayerController.instance.isAlive)
        {
            showLoseGameUI();
        } 
        else
        {
            showWinGameUI();
        }
    }
    void showWinGameUI()
    {
        winGameImage.SetActive(true);
    }

    void showLoseGameUI()
    {
        loseGameImage.SetActive(true);
    }
    public void BackToMenu()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }

        SceneManager.LoadScene(1);
    }
}

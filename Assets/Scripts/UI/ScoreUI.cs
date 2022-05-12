using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;
    public Text scoreText;
    public GameObject inGameScoreUI;

    public GameObject endGameScoreUI;
    public Text highScore;
    public Text yourScore;

    float score;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        scoreText.text = Mathf.Round(score).ToString();
        inGameScoreUI.SetActive(true);
    }

    private void Update()
    {
        if(Spawner.instance.isEndGame)
        {
            inGameScoreUI.SetActive(false);
            endGameScoreUI.SetActive(true);
            setHighScore();
            setYourScore();
        }
        
    }
    public void setScore(float score)
    {
        this.score += score;
        float curScore = Mathf.Round(this.score);
        this.scoreText.text = curScore.ToString();
        
    }

    void setHighScore()
    {
        float roundScore = Mathf.Round(this.score);
        
        if ( roundScore > PlayerPrefs.GetFloat("highScore"))
        {
            PlayerPrefs.SetFloat("highScore", roundScore);
            
        }
        highScore.text = PlayerPrefs.GetFloat("highScore").ToString();

    }
    void setYourScore()
    {
        float roundScore = Mathf.Round(this.score);
        this.yourScore.text = roundScore.ToString();
    }


}

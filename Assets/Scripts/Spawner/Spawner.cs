using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public int value;
    public List<enemy> enemiesList = new List<enemy>();
}

[System.Serializable]
public class enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    
    public GameObject groundScale;
    
    //Create waves and enemies List
    public List<Wave> wavesList = new List<Wave>();
    public List<GameObject> enemies = new List<GameObject>();

    //Time bettween 2 wave
    public float timeToNextWave;
    private float timer;

    //Countdown UI
    public GameObject timeToNextWaveManager;
    public Text coutingText;
    private bool isStartCout = true;

    public bool isNextWave()
    {
        return isStartCout;
    }

    //the wave is playing
    private int curWave = 0;


    //EndGame 

    
    public bool isEndGame;

    //Time finish wave
    public float timeFinishWave;

    public int getCurWave()
    {
        return this.curWave;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        curWave = 0;
        timer = timeToNextWave;
    }
    private void Start()
    {
        
        
    }
    private void Update()
    {
        spawnEnemy();
    }
    void spawnEnemy()
    {

        checkEndGame();

        if(!isEndGame)
        {
            if (timer <= 0)
            {
                //Playing time of this wave
                timeFinishWave += Time.deltaTime;

                if (wavesList[curWave].value != 0)
                {
                    generateEnemy(wavesList[curWave]);
                }
                else
                {
                    if (GameObject.FindWithTag("Enemy") == null)
                    {
                        curWave++;
                        timer = timeToNextWave;
                        enemies.Clear();
                        isStartCout = true;

                        timeFinishWave = 0;
                    }
                }
            }
            else
            {
                StartCoroutine(startCouting());
                coutingText.text = timer.ToString("0.00");
                timer -= Time.deltaTime;
            }
        }
        
    }

    void generateEnemy(Wave m_wave)
    {
        int randEnemy = Random.Range(0, m_wave.enemiesList.Count);

        if(m_wave.enemiesList[randEnemy].cost <= m_wave.value)
        {
            GameObject enemy = m_wave.enemiesList[randEnemy].enemyPrefab;

            enemies.Add(enemy);
            Instantiate(enemy, randPos(), Quaternion.identity);

            m_wave.value -= m_wave.enemiesList[randEnemy].cost;
        }
    }

    Vector3 randPos()
    {
        float scaleX = groundScale.transform.localScale.x;
        float scaleZ = groundScale.transform.localScale.z;

        float randX = Random.Range((-scaleX + 20) / 2, (scaleX - 20) / 2);
        float randZ = Random.Range((-scaleZ + 70) / 2, (scaleZ + 20) / 2);

        return new Vector3(randX, 0, randZ);
    }

    void checkEndGame()
    {
        if (curWave == wavesList.Count || !PlayerController.instance.isAlive)
        {
            isEndGame = true;
        }  
    }

    IEnumerator startCouting()
    {
        if (isStartCout)
        {
            isStartCout = false;
            timeToNextWaveManager.SetActive(true);
            yield return new WaitForSeconds(timeToNextWave);
            timeToNextWaveManager.SetActive(false);
        }
    }
}







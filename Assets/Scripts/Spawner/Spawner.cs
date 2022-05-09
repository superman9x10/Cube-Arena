using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Wave
{
    public int totalEnemy;
    public List<enemy> enemiesList = new List<enemy>();
}

[System.Serializable]
public class enemy
{
    public GameObject enemyPrefab;
    public int amount;
}

public class Spawner : MonoBehaviour
{
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

    //the wave is playing
    private int curWave = 0;


    private void Start()
    {
        timer = timeToNextWave;
    }
    private void Update()
    {
        spawnEnemy();
    }
    void spawnEnemy()
    {

        checkEndGame();

        if(timer <= 0)
        {
            if (enemies.Count != wavesList[curWave].totalEnemy)
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
                }
            }
        }
        else
        {
            StartCoroutine(startCouting());
            coutingText.text = timer.ToString("0.000");
            timer -= Time.deltaTime;
        }
        
    }

    void generateEnemy(Wave m_wave)
    {
        int randEnemy = Random.Range(0, m_wave.enemiesList.Count);
        if (m_wave.enemiesList[randEnemy].amount > 0)
        {
            GameObject enemy = m_wave.enemiesList[randEnemy].enemyPrefab;

            enemies.Add(enemy);
            Instantiate(enemy, randPos(), Quaternion.identity);

            m_wave.enemiesList[randEnemy].amount--;
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
        if (curWave == wavesList.Count)
        {
            Debug.Log("END GAME");
            
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







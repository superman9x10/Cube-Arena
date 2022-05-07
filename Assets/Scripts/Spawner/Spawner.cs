using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<Wave> wavesList = new List<Wave>();
    public GameObject groundScale;
    
    public float timeToNextWave;

    public List<GameObject> enemies = new List<GameObject>();

    private int curWave = 0;
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

        if(timeToNextWave <= 0)
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
                    timeToNextWave = 3f;
                    enemies.Clear();
                }
            }
        }
        else
        {
            timeToNextWave -= Time.deltaTime;
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
}







using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject groundScale;
    public GameObject healthItem;

    public float timerToSpawn;
    private float timer;
    private void Start()
    {
        
    }
    private void Update()
    {
        HealthSpawner();
    }

    void HealthSpawner()
    {
        if (timer <= 0)
        {
            int rate = spawnRate();

            if (rate < 1)
            {
                Vector3 spawnPos = randPos();
                GameObject item = Instantiate(healthItem, spawnPos, Quaternion.identity);
                Destroy(item, 5f);
            }

            timer = timerToSpawn;
        }
        else
        {
            timer -= Time.deltaTime;
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

    int spawnRate()
    {
        int randNum = 1;
        int playerCurHP = PlayerController.instance.getPlayerCurHP();
        int playerHP = PlayerController.instance.getPlayerHP();

        if (playerCurHP <= playerHP * 90 / 100 && playerCurHP > playerHP * 80 / 100)
        {
            randNum = Random.Range(0, 10);
        }
        else if (playerCurHP <= playerHP * 80 / 100 && playerCurHP > playerHP * 50 / 100)
        {
            randNum = Random.Range(0, 8);
        }
        else if (playerCurHP <= playerHP * 50 / 100)
        {
            randNum = Random.Range(0, 1);
        }
        return randNum;
    }
}

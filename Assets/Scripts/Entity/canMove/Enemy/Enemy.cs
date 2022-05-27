using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity
{
    private Transform playerPos;
    public float timeToRandomMoving;
    protected override void Start()
    {
        playerPos = PlayerController.instance.transform;
        base.Start();
    }

    protected override void Update()
    {
        lookAtPlayer();
    }
    protected override void movement()
    {
        movingState();
        rb.velocity = moveDir * moveSpeed;
        limitMoving();
    }
    void lookAtPlayer()
    {
        if (PlayerController.instance.isAlive)
        {
            transform.LookAt(playerPos);
        }

    }

    void movingState()
    {
        if (PlayerController.instance.isAlive)
        {
            float disToPlayer = Vector3.Distance(transform.position, playerPos.position);
            if (disToPlayer < 150f)
            {
                moveDir = (playerPos.position - transform.position).normalized;
            }
            else
            {
                randomMoving();
            }
        }
        else
        {
            randomMoving();
        }
    }

    void randomMoving()
    {
        timeToRandomMoving -= Time.deltaTime;
        if (timeToRandomMoving <= 0)
        {
            float randX = Random.Range(-1f, 1f);
            float randZ = Random.Range(-1f, 1f);
            moveDir = new Vector3(randX, 0, randZ).normalized;
            timeToRandomMoving = 3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);

            Damageable(PlayerController.instance.getDamage());
            if (hp <= 0)
            {
                ScoreUI.instance.setScore(1 / Spawner.instance.timeFinishWave * 1000);

                GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(explo, 2f);
                Destroy(gameObject);
            }
        }
    }
}

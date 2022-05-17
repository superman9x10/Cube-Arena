using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Controller : MovingEntity, IDamageable
{
    private Transform playerPos;
    private Rigidbody rb;
    private Vector3 moveDir;

    [SerializeField] int hp;
    [SerializeField] float moveSpeed;
    public float timer;
    public GameObject explosionFx;

    private void Start()
    {
        playerPos = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody>();

    }
    private void Update()
    {
        lookAtPlayer();
        movement();
    }

    void lookAtPlayer()
    {
        if (PlayerController.instance.isAlive)
        {
            transform.LookAt(playerPos);
        }

    }

    void movement()
    {
        movingState();
        rb.velocity = moveDir * moveSpeed;
        limitMoving(this.transform);
    }

    void movingState()
    {
        if (PlayerController.instance.isAlive)
        {
            float disToPlayer = Vector3.Distance(transform.position, playerPos.position);
            if (disToPlayer < 10f)
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
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float randX = Random.Range(-1f, 1f);
            float randZ = Random.Range(-1f, 1f);
            moveDir = new Vector3(randX, 0, randZ).normalized;
            timer = 3f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);

            Damageable(PlayerController.instance.getPlayerDamage());
            if (hp <= 0)
            {
                ScoreUI.instance.setScore(1 / Spawner.instance.timeFinishWave * 1000);

                GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(explo, 2f);
                Destroy(gameObject);
            }
        }
    }

    public void Damageable(int damage)
    {
        this.hp -= damage;
    }
}

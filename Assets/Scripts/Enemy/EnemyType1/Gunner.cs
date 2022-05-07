using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    private Transform playerPos;
    private GameObject groundScale;
    private Rigidbody rb;
    private Vector3 moveDir;
    private PlayerController playerController;

    public int hp;
    public float moveSpeed;
    public float timer;
    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        groundScale = GameObject.FindWithTag("Ground");
        playerController = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(playerController.isAlive)
        {
            lookAtPlayer();
            
        }
        movement();

    }

    void lookAtPlayer()
    {
        transform.LookAt(playerPos);
    }

    void movement()
    {
        movingState();
        rb.velocity = moveDir * moveSpeed;
        limitMoving();
    }

    void limitMoving()
    {
        float scaleX = groundScale.transform.localScale.x;
        float scaleZ = groundScale.transform.localScale.z;

        Vector3 temp = transform.position;

        if (temp.x > (scaleX - 20) / 2)
        {
            temp.x = (scaleX - 20) / 2;
        }
        else if (temp.x < (-scaleX + 20) / 2)
        {
            temp.x = (-scaleX + 20) / 2;
        }

        if (temp.z > (scaleZ + 20) / 2)
        {
            temp.z = (scaleZ + 20) / 2;
        }
        else if (temp.z < (-scaleZ + 70) / 2)
        {
            temp.z = (-scaleZ + 70) / 2;
        }

        transform.position = temp;
    }

    void movingState()
    {
        if(playerController.isAlive)
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
            moveDir = Vector3.zero;
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
        if(other.tag == "PlayerBullet")
        {
            hp -= 10;
            if(hp <= 0)
            {
                Destroy(gameObject);
            }
            Debug.Log("Enemy Hit");
            Destroy(other.gameObject);
        }
    }
}

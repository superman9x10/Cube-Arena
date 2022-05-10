using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    private Transform playerPos;
    private GameObject groundScale;
    private Rigidbody rb;
    private Vector3 moveDir;

    [SerializeField] int hp;
    [SerializeField] float moveSpeed;
    public float timer;

    public GameObject explosionFx;

    public int getHP()
    {
        return this.hp;
    }
    //public AudioSource audioSource;
    private void Start()
    {
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        groundScale = GameObject.FindWithTag("Ground");
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
        } else
        {
            
            transform.LookAt(-moveDir);
        }
            
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
        if(PlayerController.instance.isAlive)
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
            //moveDir = Vector3.zero;
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
            
            hp -= 10;
            if (hp <= 0)
            {
                GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(explo, 2f);
                Destroy(gameObject);
            }
        }
    }
}

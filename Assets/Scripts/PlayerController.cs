using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Movement
    [SerializeField] float moveSpeed;
    [SerializeField] int hp;

    private GameObject groundScale;
    private float h, v;
    private Vector3 dirMovement;
    private Rigidbody m_rb;
    public bool isAlive;

    public GameObject explosionFx;
    
    public int getPlayerHP()
    {
        return this.hp;
    }
    public void damagePlayer(int damage)
    {
        this.hp -= damage;
    }

    public HealthBar healthBar;

    private void Awake()
    {
        isAlive = true;
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        healthBar.setMaxHealth(hp);
        groundScale = GameObject.FindWithTag("Ground");
        m_rb = GetComponent<Rigidbody>();   
    }
    private void Update()
    {
        getInput();
        lookProcess();
        isPlayerDead();
        healthBar.setHealth(hp);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement ()
    {
        m_rb.velocity = dirMovement * moveSpeed;
        limitMoving();
    }

    void getInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirMovement = new Vector3(h, 0, v);
    }

    void lookProcess()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void limitMoving()
    {
        float scaleX = groundScale.transform.localScale.x;
        float scaleZ = groundScale.transform.localScale.z;

        Vector3 temp = transform.position;

        if(temp.x > (scaleX - 20) / 2)
        {
            temp.x = (scaleX - 20) / 2;
        } else if (temp.x < (-scaleX + 20) / 2)
        {
            temp.x = (-scaleX + 20) / 2;
        }

        if(temp.z > (scaleZ + 20) / 2)
        {
            temp.z = (scaleZ + 20) / 2;
        } else if(temp.z < (-scaleZ + 70) / 2)
        {
            temp.z = (-scaleZ + 70) / 2;
        }

        transform.position = temp;
    }

    void isPlayerDead()
    {
        if (this.hp <= 0 && isAlive)
        {
            isAlive = false;
            GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(explo, 2f);
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            damagePlayer(10);
            //healthBar.setHealth(hp);
            //playerDead();
            Destroy(other.gameObject);
        }
    }

}

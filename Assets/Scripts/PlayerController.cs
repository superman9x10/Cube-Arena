using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    [SerializeField] float moveSpeed;
    [SerializeField] int hp;

    public bool isAlive;
    public int getPlayerHP()
    {
        return this.hp;
    }
    public void damagePlayer(int damage)
    {
        this.hp -= damage;
    }
    
    
    private GameObject groundScale;
    private float h, v;
    private Vector3 dirMovement;
    private Rigidbody m_rb;

    private void Awake()
    {
        isAlive = true;
    }

    private void Start()
    {
        
        groundScale = GameObject.FindWithTag("Ground");
        m_rb = GetComponent<Rigidbody>();   
    }
    private void Update()
    {
        getInput();
        lookProcess();
        playerDead();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement ()
    {
        //transform.position += dirMovement * moveSpeed * Time.deltaTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            hp -= 10;
            Destroy(other.gameObject);
        }
    }

    void playerDead()
    {
        if(this.hp <= 0)
        {
            //isAlive = false;
            //Destroy(this.gameObject);
            Debug.Log("Player Dead");
        }
    }
}

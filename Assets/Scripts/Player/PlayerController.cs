using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingEntity, IDamageable, IHealingable
{
    public static PlayerController instance;

    //Movement
    [SerializeField] float moveSpeed;
    [SerializeField] int hp;
    [SerializeField] int playerDamage;

    private int curHP;
    private float h, v;
    private Vector3 dirMovement;
    private Rigidbody m_rb;
    public bool isAlive;

    public GameObject explosionFx;


    
    public int getPlayerHP()
    {
        return this.hp;
    }
    
    public int getPlayerCurHP()
    {
        return this.curHP;
    }

    public int getPlayerDamage()
    {
        return this.playerDamage;
    }
   

    public HealthBar healthBar;
    public GameObject playerShield;
    private float timer;

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
        m_rb = GetComponent<Rigidbody>();
        curHP = this.hp;
    }
    private void Update()
    {
        if (isAlive && !Spawner.instance.isEndGame)
        {
            getInput();
            lookProcess();
            healthBar.setHealth(curHP);
        }
        PlayerDead();
        
    }
    private void FixedUpdate()
    {
        Movement();
    }

    void Movement ()
    {
        m_rb.velocity = dirMovement * moveSpeed;
        limitMoving(this.transform);
    }

    void getInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirMovement = new Vector3(h, 0, v);
        useShield();
    }

    void useShield()
    {
        if (timer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameObject shield = Instantiate(playerShield);
                Destroy(shield, 3f);

                timer = SkillCoolDown.instance.coolDownTime;
            }

        }
        else
        {
            timer -= Time.deltaTime;
        }
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

    void PlayerDead()
    {
        if (curHP <= 0 && isAlive)
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
            Damageable(10);
            //healthBar.setHealth(hp);
            //playerDead();
            other.gameObject.SetActive(false);
        }
    }

    //Interface
    public void Damageable(int damage)
    {
        curHP -= damage;
    }

    public void healing(int HP)
    {
        curHP += HP;
    }
}

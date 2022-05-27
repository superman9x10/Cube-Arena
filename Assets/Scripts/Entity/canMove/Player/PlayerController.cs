using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovingEntity, IHealingable
{
    public static PlayerController instance;
    private float timer;

    private int curHP;

    public bool isAlive;

    
    public int getPlayerCurHP()
    {
        return this.curHP;
    }



    public HealthBar healthBar;
    public GameObject playerShield;
    

    private void Awake()
    {
        isAlive = true;
        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void Start()
    {
        base.Start();
        healthBar.setMaxHealth(hp);
        curHP = this.hp;
    }
    protected override void Update()
    {
        if (isAlive && !Spawner.instance.isEndGame)
        {
            getInput();
            lookProcess();
            healthBar.setHealth(curHP);
        }
        checkPlayer();
        
    }


    protected override void movement ()
    {
        rb.velocity = moveDir;
        limitMoving();
    }

    void getInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(h * moveSpeed, rb.velocity.y, v * moveSpeed);
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

    void checkPlayer()
    {
        if (curHP <= 0 && isAlive)
        {
            isAlive = false;
            GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
            Destroy(explo, 2f);
            Destroy(this.gameObject);
        }
    }

    //Interface
    public override void Damageable(int damage)
    {
        curHP -= damage;
    }

    public void healing(int HP)
    {
        curHP += HP;
    }
}

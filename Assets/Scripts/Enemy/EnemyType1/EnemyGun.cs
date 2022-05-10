using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bullet;

    public float fireRate;
    public float canShootAfter;
    public float bulletSpeed;
    public float timer;

    private int swapGun;
    private float randNum = 3f;

    private void Update()
    {
        timer -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (timer < randNum && timer > 0)
        {
            Shoot();
            
        } else if (timer < 0)
        {
            timer = 4;
            randNum = Random.Range(1, 5);
        }
    }

    void Shoot()
    {
        if(PlayerController.instance.isAlive)
        {
            if (Time.time > canShootAfter)
            {
                if (swapGun == 0)
                {
                    swapGun = 1;
                    CreateBul(firePoint1);
                }
                else
                {
                    swapGun = 0;
                    CreateBul(firePoint2);
                }

                canShootAfter = Time.time + 1 / fireRate;
            }
        }
    }

    void CreateBul(Transform firePoint)
    {
        GameObject bull = (GameObject)Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody bul_rb = bull.GetComponent<Rigidbody>();
        bul_rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(bull, 1f);
    }
}

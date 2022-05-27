using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerGun : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bullet;

    public float fireRate;
    public float timer;

    private float canShootAfter;
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
        GameObject bul = PoolingObject.instance.GetPoolingObject("EnemyBullet");
        if (bul != null)
        {
            bul.transform.position = firePoint.position;
            bul.transform.rotation = Quaternion.identity;
            
            bul.GetComponent<GunnerBul>().dirToFire = firePoint.forward;
            bul.GetComponent<GunnerBul>().setDamage(GetComponentInParent<Gunner>().getDamage());
            bul.SetActive(true);
        }
    }
}

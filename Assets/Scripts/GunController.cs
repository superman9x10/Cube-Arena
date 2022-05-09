using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject bullet;
    public Vector3 fireDirection { set; get; }
    public float fireRate;
    public float canShootAfter;
    private int swapGun;

    public AudioSource shootingSound;

    private void Update()
    {
        shoot();
    }

    void shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > canShootAfter)
            {
                shootingSound.Play();
                if (swapGun == 0)
                {
                    swapGun = 1;
                    GameObject bull = (GameObject)Instantiate(bullet, firePoint1.position, Quaternion.identity);
                    fireDirection = firePoint1.forward;
                }
                else
                {
                    swapGun = 0;
                    GameObject bull = (GameObject)Instantiate(bullet, firePoint2.position, Quaternion.identity);
                    fireDirection = firePoint2.forward;
                }

                canShootAfter = Time.time + 1 / fireRate;
            }

        }
    }
}

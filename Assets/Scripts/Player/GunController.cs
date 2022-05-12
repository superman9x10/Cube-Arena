using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;

    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject bullet;
    public Vector3 fireDirection { set; get; }
    public float fireRate;
    public float canShootAfter;
    private int swapGun;

    public AudioSource shootingSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

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
                    GameObject bull = (GameObject)Instantiate(bullet, firePointLeft.position, Quaternion.identity);
                    fireDirection = firePointLeft.forward;
                }
                else
                {
                    swapGun = 0;
                    GameObject bull = (GameObject)Instantiate(bullet, firePointRight.position, Quaternion.identity);
                    fireDirection = firePointRight.forward;
                }

                canShootAfter = Time.time + 1 / fireRate;
            }

        }
    }
}

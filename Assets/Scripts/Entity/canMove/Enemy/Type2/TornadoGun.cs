using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoGun : MonoBehaviour
{
    public Transform[] guns;
    public GameObject bullet;
    public float fireRate;
    public float timeBetweenShootWave;
    
    private float timer;
    private bool isShooting;
    private float timeBetweenBul;
    private void Start()
    {
        timer = timeBetweenShootWave;
    }

    private void Update()
    {
        if(timer < 0)
        {
            gunRotate();
            fire();

            if(!isShooting)
            {
                isShooting = true;
                StartCoroutine(startFire());
            }
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    void fire()
    {
        if(PlayerController.instance.isAlive)
        {
            if(Time.time > timeBetweenBul)
            {
                createAndShoot();
                timeBetweenBul = Time.time + 1 / fireRate;
            }
        }
    }

    void createAndShoot()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            GameObject bul = PoolingObject.instance.GetPoolingObject("TornadoBul");
            if (bul != null)
            {
                bul.transform.position = guns[i].transform.position;
                bul.transform.rotation = Quaternion.identity;

                bul.GetComponent<TornadoBul>().fireDir = guns[i].forward;
                bul.GetComponent<TornadoBul>().setDamage(GetComponentInParent<Tornado>().getDamage());
                bul.SetActive(true);
            }
        }
    }
    void gunRotate()
    {
        this.transform.Rotate(Vector3.up, 180f * Time.deltaTime);
    }
    IEnumerator startFire()
    {
        yield return new WaitForSeconds(timeBetweenShootWave);
        timer = timeBetweenShootWave;
        isShooting = false;
    }
}

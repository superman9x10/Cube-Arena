using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerBul : MonoBehaviour
{
    public Vector3 dirToFire { set; get; }
    public float bulletSpeed;
    private void Update()
    {
        this.transform.position += dirToFire * bulletSpeed * Time.deltaTime;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            this.gameObject.GetComponent<TrailRenderer>().Clear();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "PlayerShield")
        {
            this.gameObject.GetComponent<TrailRenderer>().Clear();
            this.gameObject.SetActive(false);
        }
    }
}

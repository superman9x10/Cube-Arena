using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBul : MonoBehaviour
{
    public float speed;
    public Vector3 fireDir;
    private int damage;
    public void setDamage(int dmg)
    {
        damage = dmg;
    }
    private void Update()
    {
        this.transform.position += fireDir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerShield")
        {
            if(other.tag == "Player")
            {
                PlayerController.instance.Damageable(damage);
            }
            this.gameObject.GetComponent<TrailRenderer>().Clear();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            Debug.Log("Dead Zone");
            this.gameObject.GetComponent<TrailRenderer>().Clear();
            this.gameObject.SetActive(false);
        }
    }
}

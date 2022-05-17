using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2Bul : MonoBehaviour
{
    public float speed;
    public Vector3 fireDir;

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
                PlayerController.instance.Damageable(10);
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

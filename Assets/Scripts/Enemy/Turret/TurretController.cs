using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] int hp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {

            hp -= 10;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            Destroy(other.gameObject);
        }
    }
}

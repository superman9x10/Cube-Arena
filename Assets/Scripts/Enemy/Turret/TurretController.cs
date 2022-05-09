using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] int hp;

    public int getHP()
    {
        return this.hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            hp -= 10;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

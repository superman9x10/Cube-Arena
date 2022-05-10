using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] int hp;
    public GameObject explosionFx;
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
                GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(explo, 2f);
                Destroy(gameObject);
            }
            
        }
    }
}

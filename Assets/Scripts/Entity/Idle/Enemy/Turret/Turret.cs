using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IDamageable
{
    [SerializeField] int hp;
    [SerializeField] int damage;

    public int getDamage()
    {
        return damage;
    }

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

            Damageable(PlayerController.instance.getDamage());

            if (hp <= 0)
            {
                ScoreUI.instance.setScore(1 / Spawner.instance.timeFinishWave * 1000);

                GameObject explo = Instantiate(explosionFx, transform.position, Quaternion.identity);
                Destroy(explo, 2f);

                Destroy(gameObject);
            }
            
        }
    }

    //interface
    public void Damageable(int damage)
    {
        this.hp -= damage;  
    }
}

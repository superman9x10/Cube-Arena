using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject explosionFx;

    public int getHP()
    {
        return hp;
    }
    public int getDamage()
    {
        return damage;
    }

    public virtual void Damageable(int dmg)
    {
        this.hp -= dmg;
    }
}

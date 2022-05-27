using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    GunController gunController;
    public Rigidbody m_rb;
    public float speed;
    private void Start()
    {
        gunController = GunController.instance.GetComponent<GunController>();
        m_rb.AddForce(gunController.fireDirection * speed, ForceMode.Impulse);
        Destroy(gameObject, 1f);
    }
}

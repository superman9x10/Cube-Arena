using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void Update()
    {
        ItemRotate();
    }

    void ItemRotate()
    {
        this.transform.Rotate(Vector3.up, Time.deltaTime * 100f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}

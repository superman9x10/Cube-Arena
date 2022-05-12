using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform playerPos;
    public float distance = 100f;
    private void Start()
    {
        playerPos = PlayerController.instance.transform;
        this.transform.position = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z) + playerPos.forward * distance;
    }

    private void Update()
    {
        if (PlayerController.instance.isAlive)
        {
            playerPos = PlayerController.instance.transform; 
            //this.transform.position = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private Transform playerPos;

    private void Start()
    {
        playerPos = PlayerController.instance.transform;
        this.transform.position = new Vector3(playerPos.position.x, playerPos.position.y - 40f, playerPos.position.z + 10f);
    }
    private void Update()
    {
        if(PlayerController.instance.isAlive)
        {
            playerPos = PlayerController.instance.transform;
            this.transform.position = new Vector3(playerPos.position.x, playerPos.position.y - 40f, playerPos.position.z + 10f);
        } else
        {
            Destroy(gameObject);
        }  
    }
}

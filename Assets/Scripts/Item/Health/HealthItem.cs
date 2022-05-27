using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int _healing;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (PlayerController.instance.getPlayerCurHP() + _healing >= PlayerController.instance.getHP())
            {
                PlayerController.instance.healing(PlayerController.instance.getHP() - PlayerController.instance.getPlayerCurHP());
            }
            else
            {
                PlayerController.instance.healing(_healing);
            }

        }
    }
}

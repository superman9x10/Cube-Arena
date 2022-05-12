using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : MonoBehaviour
{
    protected void limitMoving(Transform entityTrans)
    {
        float scaleX = Ground.Instance.transform.localScale.x;
        float scaleZ = Ground.Instance.transform.localScale.z;
        Vector3 temp = entityTrans.position;

        if (temp.x > (scaleX - 20) / 2)
        {
            temp.x = (scaleX - 20) / 2;
        }
        else if (temp.x < (-scaleX + 20) / 2)
        {
            temp.x = (-scaleX + 20) / 2;
        }

        if (temp.z > (scaleZ + 20) / 2)
        {
            temp.z = (scaleZ + 20) / 2;
        }
        else if (temp.z < (-scaleZ + 70) / 2)
        {
            temp.z = (-scaleZ + 70) / 2;
        }

        entityTrans.position = temp;
    }
}

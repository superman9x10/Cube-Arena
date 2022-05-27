using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEntity : Entity
{
    protected Rigidbody rb;
    protected Vector3 moveDir;
    [SerializeField] protected float moveSpeed;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected virtual void Update()
    {
    }
    protected virtual void FixedUpdate()
    {
        movement();
    }
    protected virtual void movement()
    {
    }

    protected virtual void limitMoving()
    {
        float scaleX = Ground.Instance.transform.localScale.x;
        float scaleZ = Ground.Instance.transform.localScale.z;
        Vector3 temp = this.transform.position;

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

        this.transform.position = temp;
    }


}

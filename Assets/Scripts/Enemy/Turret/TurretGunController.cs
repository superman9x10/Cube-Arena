using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGunController : MonoBehaviour
{
    public Transform[] shootingPoints;
    public GameObject[] guns;
    [SerializeField] float timeToShoot = 2f;

    private LineRenderer[] lines;
    private bool isHitPlayer;
    private float timer = 0.5f;

    private bool canReverse = true;
    private float rotateSpeed = 20f;

    private void Start()
    {
        lines = new LineRenderer[guns.Length];
        for(int i = 0; i < guns.Length; i++)
        {
            lines[i] = guns[i].GetComponent<LineRenderer>();
        }
    }
    private void Update()
    {
        shooting();
    }

    void shooting()
    {
        reverseLaser();
        readyToShoot();
        
    }
    void reverseLaser()
    {
        if (Spawner.instance.getCurWave() > 3)
        {
            if (canReverse)
            {
                canReverse = false;
                StartCoroutine(startReverse());
            }
        }

        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
    }
    void readyToShoot()
    {
        if (timeToShoot > 0)
        {
            timeToShoot -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                shootLaser(lines[i], shootingPoints[i].position, shootingPoints[i].forward);
            }
        }
    }
    IEnumerator startReverse()
    {
        int randNum = Random.Range(-2, 3);
        if (randNum < 0)
        {
            rotateSpeed = -20f;
        }
        else if (randNum >= 0)
        {
            rotateSpeed = 20f;
        }
        yield return new WaitForSeconds(3f);
        canReverse = true;
    }

    void shootLaser(LineRenderer line, Vector3 originPos, Vector3 direction)
    {
        checkHitPlayer(originPos, direction);
        hitDelay();

        if(PlayerController.instance.isAlive)
        {
            drawLaser(line, originPos, direction * 1000f);
        } else
        {
            for (int i = 0; i < guns.Length; i++)
            {
                lines[i].enabled = false;
            }
        }

    }

    void checkHitPlayer(Vector3 originPos, Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(originPos, direction, out hit) && !isHitPlayer)
        {
            if (hit.collider.tag == "Player")
            {
                isHitPlayer = true;
                PlayerController.instance.damagePlayer(10);
            }
        }
    }

    void hitDelay()
    {
        if (isHitPlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isHitPlayer = false;
                timer = 0.2f;
            }
        }
    }
    void drawLaser(LineRenderer line, Vector3 startPos, Vector3 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

}

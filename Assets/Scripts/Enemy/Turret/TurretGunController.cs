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
    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        lines = new LineRenderer[guns.Length];
        for(int i = 0; i < guns.Length; i++)
        {
            lines[i] = guns[i].GetComponent<LineRenderer>();
        }
    }
    private void Update()
    {

        transform.Rotate(Vector3.up * (20f * Time.deltaTime));
        
        if (timeToShoot > 0)
        {
            timeToShoot -= Time.deltaTime;
        }
        
        if (timeToShoot <= 0)
        {
            
            for (int i = 0; i < shootingPoints.Length; i++)
            {
                shootLaser(lines[i], shootingPoints[i].position, shootingPoints[i].forward);
            }
        }
        
    }

    void shootLaser(LineRenderer line, Vector3 originPos, Vector3 direction)
    {
        RaycastHit hit;
        if(Physics.Raycast(originPos, direction, out hit) && !isHitPlayer)
        {
            if(hit.collider.tag == "Player")
            {
                isHitPlayer = true;
                playerController.damagePlayer(10);
                //Debug.Log(playerController.getPlayerHP());
            } 
        }
        
        if(isHitPlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isHitPlayer = false;
                timer = 0.5f;
            }
        }
        
        drawLaser(line, originPos, direction * 1000f);
    }
    void drawLaser(LineRenderer line, Vector3 startPos, Vector3 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{
    public static SkillCoolDown instance;
    public Image skillImage;
    public float coolDownTime;
    bool isCoolDown = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        skillImage.fillAmount = 1;
    }

    private void Update()
    {
        CoolDownHandler();
    }

    void CoolDownHandler()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isCoolDown && PlayerController.instance.isAlive)
        {
            isCoolDown = true;
            skillImage.fillAmount = 0;
        }
        if(isCoolDown)
        {
            skillImage.fillAmount += 1 / coolDownTime * Time.deltaTime;
            if(skillImage.fillAmount >= 1)
            {
                skillImage.fillAmount = 1f;
                isCoolDown = false;
            }
        }
    }
}

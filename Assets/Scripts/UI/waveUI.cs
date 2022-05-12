using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveUI : MonoBehaviour
{
    public Text wave;

    private void Start()
    {
        wave.text = (Spawner.instance.getCurWave() + 1).ToString();
    }

    private void Update()
    {
        if(Spawner.instance.isNextWave() && !Spawner.instance.isEndGame)
        {
            wave.text = (Spawner.instance.getCurWave() + 1).ToString();
        }
    }
}

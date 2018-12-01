using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveIndicator : MonoBehaviour {

    public Text waveText;
    private int waveNum;

    public void WaveUpdate(int i)
    {
        waveNum = i;
        StartCoroutine(UpdateWave());
    }

    
    IEnumerator UpdateWave()
    {
        float timer = 3f;

        while(timer >= 0)
        {
            timer -= Time.deltaTime;

            if(timer > 1)
            {
                waveText.text = "WAVE " + waveNum.ToString();
            }
            else if (timer < 1 && timer > 0)
            {
                waveText.text = "GO!";
            }
            else
            {
                waveText.text = "";
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

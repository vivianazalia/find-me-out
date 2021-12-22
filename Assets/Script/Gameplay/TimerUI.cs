using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private float minutes;
    private float seconds;

    private void Update()
    {
        //minutes = GameManager.instance.minutes;
        //seconds = GameManager.instance.seconds;
        if(seconds < 10)
        {
            timerText.text = ((int)minutes).ToString() + ":" + "0" + ((int)seconds).ToString();
        } 
        else
        {
            timerText.text = ((int)minutes).ToString() + ":" + ((int)seconds).ToString();
        }
    }
}

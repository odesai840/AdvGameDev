using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool playerAlive = true;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (playerAlive)
        {
            float timeAlive = Time.time - startTime;
            int totalSeconds = Mathf.FloorToInt(timeAlive);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            string timeText;

            if (minutes == 0)
            {
                timeText = seconds.ToString();
            }
            else
            {
                timeText = minutes.ToString("0") + ":" + seconds.ToString("00");
            }

            if (timerText != null)
                timerText.text = timeText;
        }
    }
}

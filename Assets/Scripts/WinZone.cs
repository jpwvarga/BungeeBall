using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour
{
    public GameController gc;

    public float timeBeforeWin = 3f; // Time in seconds the player must be in the zone to win
    private float countdownToWin = 0f; // Variable used to track time in the WinZone
    private bool inZone = false;
    
    void Update()
    {
        if (inZone)
        {
            countdownToWin -= Time.deltaTime;
            DoCountdown(countdownToWin);
        }
        else if (!gc.HasWon() && gc.winText.enabled)
        {
            gc.winText.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = true;
            countdownToWin = timeBeforeWin; // Resets countdown timer
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && inZone)
        {
            inZone = false;
        }
    }

    // Does a dramatic countdown to the grand finale of the level
    void DoCountdown(float timeRemaining)
    {
        if (timeRemaining < 0f)
        {
            gc.Win();
        }
        else if (timeRemaining < 1f)
        {
            // Change text to 1
            if (!gc.winText.text.Contains("1"))
            {
                gc.winText.text = "1";
            }
        }
        else if (timeRemaining < 2f)
        {
            // Change text to 2
            if (!gc.winText.text.Contains("2"))
            {
                gc.winText.text = "2";
            }
        }
        else if (timeRemaining < 3f)
        {
            // Change text to 3
            if (!gc.winText.text.Contains("3"))
            {
                gc.winText.enabled = true;
                gc.winText.text = "3";
            }
        }
    }    
}

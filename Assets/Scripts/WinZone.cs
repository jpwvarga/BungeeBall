using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour
{
    public GameController gc;

    public float timeBeforeWin = 1.5f; // Time in seconds the player must be in the zone to win
    private float countdownToWin = 0f; // Variable used to track time in the WinZone
    private bool inZone = false;
    
    void Update()
    {
        if (inZone)
        {
            countdownToWin -= Time.deltaTime;
            if (countdownToWin < 0f && !gc.HasWon())
            {
                gc.Win();
            }
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinZone : MonoBehaviour
{
    [SerializeField] GameController gc;

    [SerializeField] float timeBeforeWin = 1.5f; // Time in seconds the player must be in the zone to win
    private float countdownToWin = 0f; // Variable used to track time in the WinZone
    private bool inZone = false;
    
    void Update()
    {
        if (inZone)
        {
            countdownToWin -= Time.deltaTime;
            if (countdownToWin < 0f && !gc.HasWon())
            {
                gc.GameOver(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inZone = true;
            countdownToWin = timeBeforeWin; // Resets countdown timer
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && inZone)
        {
            inZone = false;
        }
    }
}

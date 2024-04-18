using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public GameController gc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") gc.Win();
    }
}

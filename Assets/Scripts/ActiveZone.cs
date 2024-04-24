using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveZone : MonoBehaviour
{
    [SerializeField] GameController gc;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.GameOver();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectible : MonoBehaviour
{
    public GameController gc;

    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.AddCollectible(value);
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }
}

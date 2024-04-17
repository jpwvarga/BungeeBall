using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text collectibleText;
    public string collectibleSpriteName;
    [SerializeField] private int nCollectibles = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCollectibleText();
    }

    public void AddCollectible(int amount)
    {
        nCollectibles += amount;
        UpdateCollectibleText();
    }

    void UpdateCollectibleText()
    {
        collectibleText.text = "<sprite name=\"" + collectibleSpriteName + "\">: " + nCollectibles.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TMP_Text collectibleText;
    public Image crosshair;
    public string collectibleSpriteName;
    [SerializeField] private int nCollectibles = 0;
    private int maxCollectibleNumber;
    public TMP_Text winText; // Text displayed on level completion
    public TMP_Text continueText; // Text displayed when asking to continue
    private bool hasWon = false;

    // Start is called before the first frame update
    void Start()
    {
        hasWon = false;
        winText.enabled = false;
        continueText.enabled = false;
        crosshair.enabled = true;
        collectibleText.enabled = true;
        UpdateCollectibleText();
        maxCollectibleNumber = FindObjectsByType<Collectible>(0).Length;
    }

    void Update()
    {
        if (hasWon && Input.GetButtonDown("Fire1")) // Might add a quirky little timer here eventually :P
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // TODO: This should go to the next level instead
        }
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

    public void Win()
    {
        collectibleText.enabled = false;
        crosshair.enabled = false;
        winText.text = "CONGRATULATIONS\nScore: " + nCollectibles.ToString() + "/" + maxCollectibleNumber.ToString();
        continueText.text = "Press [Fire1] to continue..."; // TODO: Make this show an icon of the button
        winText.enabled = true;
        continueText.enabled = true;
        hasWon = true;
    }
}

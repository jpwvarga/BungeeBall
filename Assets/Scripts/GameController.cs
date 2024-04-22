using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Main GUI")]
    public Image crosshair;

    [Header("Collectibles")]
    public TMP_Text collectibleText;
    public Sprite collectibleSpriteName;
    [SerializeField] private int nCollectibles = 0;
    private int maxCollectibleNumber;

    [Header("Timer")]
    public TMP_Text goalLvlTimeText;
    public TMP_Text currLvlTimeText;
    public Sprite timerSprite;
    public float lvlGoalTime = 15f;
    private float currLvlTime = 0f;

    [Header("Winning")]
    public TMP_Text winText; // Text displayed on level completion
    public TMP_Text continueText; // Text displayed when asking to continue
    private bool hasWon = false;
    [SerializeField] GameObject winScreen;

    [Header("Win Saves")]
    private WinSave currentSave;
    private int currentLevel;

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
        goalLvlTimeText.enabled = true;
        currLvlTimeText.enabled = true;
        UpdateGoalTimeText();
        UpdateLevelTimeText();

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentSave = WinSave.ReadFromFile(currentLevel);
    }

    void Update()
    {
        if (hasWon)
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // TODO: This should go to the next level instead
            }
        }
        else
        {
            currLvlTime += Time.deltaTime;
            UpdateLevelTimeText();
        }
    }

    public void AddCollectible(int amount)
    {
        nCollectibles += amount;
        UpdateCollectibleText();
    }

    void UpdateCollectibleText()
    {
        collectibleText.text = "<sprite name=\"" + collectibleSpriteName.name + "\">: " + nCollectibles.ToString();
    }

    public void Win()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
        collectibleText.enabled = false;
        crosshair.enabled = false;
        winText.text = "LEVEL COMPLETE\nScore: " + nCollectibles.ToString() + "/" + maxCollectibleNumber.ToString();
        continueText.text = "Press [Jump] to continue..."; // TODO: Make this show an icon of the button
        winText.enabled = true;
        continueText.enabled = true;
        hasWon = true;

        WinSave thisW = new WinSave();
        thisW.level = currentLevel;
        thisW.hasCompleted = true;
        thisW.hasAllCollectibles = currentSave.hasAllCollectibles || nCollectibles == maxCollectibleNumber;
        thisW.hasBeatTime = currentSave.hasBeatTime || currLvlTime <= lvlGoalTime;
        thisW.highscoreTime = currentSave.highscoreTime < 0 || currLvlTime < currentSave.highscoreTime ? currLvlTime : currentSave.highscoreTime;
        WinSave.WriteToFile(thisW);
    }

    public bool HasWon()
    {
        return hasWon;
    }

    void UpdateGoalTimeText()
    {
        goalLvlTimeText.text = string.Format("<sprite name=\"{0}\">: {1:00}:{2:00.00}", timerSprite.name, lvlGoalTime/60f, lvlGoalTime, lvlGoalTime*100f%100f);
    }

    void UpdateLevelTimeText()
    {
        currLvlTimeText.text = string.Format("<sprite name=\"{0}\">: {1:00}:{2:00.00}", timerSprite.name, currLvlTime/60f, currLvlTime, currLvlTime * 100f % 100f);
    }
}

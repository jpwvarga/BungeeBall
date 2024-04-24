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
    private int nCollectibles = 0;
    private int maxCollectibleNumber;

    [Header("Timer")]
    public TMP_Text goalLvlTimeText;
    public TMP_Text currLvlTimeText;
    public Sprite goalSprite;
    public Sprite bestTimeSprite;
    public Sprite timerSprite;
    public float lvlGoalTime = 15f;
    private float currLvlTime = 0f;

    [Header("Winning")]
    public TMP_Text winText; // Text displayed on level completion
    private bool hasWon = false;
    [SerializeField] GameObject winScreen;

    [Header("Win Saves")]
    private WinSave currentSave;
    private int currentLevel;

    [Header("Losing")]
    public GameObject loseScreen;
    private bool gameOver = false;

    [Header("Pausing")]
    [SerializeField] GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        hasWon = false;

        //winText.enabled = false;
        crosshair.enabled = true;
        collectibleText.enabled = true;
        UpdateCollectibleText();
        maxCollectibleNumber = FindObjectsByType<Collectible>(0).Length;
        
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        currentSave = WinSave.ReadFromFile(currentLevel);

        goalLvlTimeText.enabled = true;
        currLvlTimeText.enabled = true;
        UpdateGoalTimeText();
        UpdateLevelTimeText();
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetButtonUp("Cancel"))
            {
                pauseScreen.SetActive(true);
            }
            else
            {
                currLvlTime += Time.deltaTime;
                UpdateLevelTimeText();
            }
        }
    }

    void UpdateCollectibleText()
    {
        collectibleText.text = "<sprite name=\"" + collectibleSpriteName.name + "\">: " + nCollectibles.ToString();
    }

    void UpdateGoalTimeText()
    {
        if (currentSave.highscoreTime > 0 && currentSave.highscoreTime < lvlGoalTime)
        {
            goalLvlTimeText.text = string.Format("<sprite name=\"{0}\">: {1}", bestTimeSprite.name, formatForTime(currentSave.highscoreTime));
        }
        else
        {
            goalLvlTimeText.text = string.Format("<sprite name=\"{0}\">: {1}", goalSprite.name, formatForTime(lvlGoalTime));
        }
    }
    void UpdateLevelTimeText()
    {
        currLvlTimeText.text = string.Format("<sprite name=\"{0}\">: {1}", timerSprite.name, formatForTime(currLvlTime));
    }

    public static string formatForTime(float time_s)
    {
        return string.Format("{0:##00}:{1:00}.{2:00}", time_s / 60f, time_s % 60f, time_s * 100f % 100f);
    }

    public void GameOver(bool isWin = false)
    {
        CursorController.Unlock();
        crosshair.enabled = false;
        //winText.text = "LEVEL COMPLETE";
        //winText.enabled = true;
        gameOver = true;
        hasWon = isWin;

        if (hasWon && !loseScreen.activeInHierarchy)
        {
            winScreen.SetActive(true);

            WinSave thisW = new WinSave();
            thisW.level = currentLevel;
            thisW.hasCompleted = true;
            thisW.hasAllCollectibles = currentSave.hasAllCollectibles || nCollectibles == maxCollectibleNumber;
            thisW.hasBeatTime = currentSave.hasBeatTime || currLvlTime <= lvlGoalTime;

            if (currentSave.highscoreTime < 0 || currLvlTime < currentSave.highscoreTime)
            {
                thisW.highscoreTime = currLvlTime;
                winText.text = string.Format("New Best Time: {0}!", formatForTime(thisW.highscoreTime));
                //lvlGoalTime = currentSave.highscoreTime;
            }
            else
            {
                thisW.highscoreTime = currentSave.highscoreTime;
                winText.text = "LEVEL COMPLETE";
            }
            thisW.highscoreTime = currentSave.highscoreTime < 0 || currLvlTime < currentSave.highscoreTime ? currLvlTime : currentSave.highscoreTime;
            WinSave.WriteToFile(thisW);
        }
        else if (!winScreen.activeInHierarchy)
        {
            loseScreen.SetActive(true);
        }
        else
        {
            Debug.LogError("Somehow won and lost at the same time!");
        }
    }

    public bool HasWon()
    {
        return hasWon;
    }

    public bool IsOver()
    {
        return gameOver;
    }

    public void AddCollectible(int amount)
    {
        nCollectibles += amount;
        UpdateCollectibleText();
    }
}

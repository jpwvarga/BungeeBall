using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject controlsMenu;
    [SerializeField] Image levelIndicator;
    [SerializeField] Sprite[] levelIndicators;
    [SerializeField] Button[] levelButtons;
    private int numLevels;
    private int selectedLvl = 1;

    private List<WinSave> saves;

    void Start()
    {
        CursorController.Unlock();
        levelSelect.SetActive(false);
        controlsMenu.SetActive(false);
        numLevels = SceneManager.sceneCountInBuildSettings-1;
        UpdateLevelSelect();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(selectedLvl);
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void LevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void LSelectPlay(int level)
    {
        selectedLvl = level;
        levelIndicator.sprite = levelIndicators[level - 1];
        ExitLevelSelect();
    }

    public void ExitLevelSelect()
    {
        levelSelect.SetActive(false);
    }

    public void ControlsMenu()
    {
        controlsMenu.SetActive(true);
    }

    public void ExitControlsMenu()
    {
        controlsMenu.SetActive(false);
    }

    public void ResetLevels()
    {
        //resetLevels = true;
        StartCoroutine(IResetLevels());
        UpdateLevelSelect();
    }

    private IEnumerator IResetLevels()
    {
        for (int i = 1; i <= numLevels; i++)
        {
            WinSave.ClearSave(i);
            yield return null;
        }
    }

    private void UpdateLevelSelect()
    {
        StartCoroutine(IUpdateLLvlSelect());
    }

    private IEnumerator IUpdateLLvlSelect()
    {
        saves = new List<WinSave>(numLevels);
        for (int i = 1; i <= numLevels; i++)
        {
            // Get the save
            saves.Insert(i-1, WinSave.ReadFromFile(i));

            // Update the stars
            levelButtons[i - 1].GetComponent<ChangeStars>().ChangeTo(saves[i-1]);
            yield return null;
        }
    }
}

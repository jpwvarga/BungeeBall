using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject controlsMenu;
    private int numLevels;

    void Start()
    {
        Cursor.visible = true;
        levelSelect.SetActive(false);
        controlsMenu.SetActive(false);
        numLevels = SceneManager.sceneCountInBuildSettings-1;
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LevelSelect()
    {
        levelSelect.SetActive(true);
    }

    public void LSelectPlay(int level)
    {
        SceneManager.LoadSceneAsync(level);
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
    }

    private IEnumerator IResetLevels()
    {
        for (int i = 1; i <= numLevels; i++)
        {
            WinSave.ClearSave(i);
            yield return null;
        }
    }
}

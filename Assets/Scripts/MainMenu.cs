using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject controlsMenu;
    void Start()
    {
        Cursor.visible = true;
        levelSelect.SetActive(false);
        controlsMenu.SetActive(false);
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
}

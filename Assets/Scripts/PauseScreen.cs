using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        CursorController.Unlock();
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Restart()
    {
        //Time.timeScale = 1;
        OverlayScreen.Restart();
    }

    public void MainMenu()
    {
        //Time.timeScale = 1;
        OverlayScreen.MainMenu();
    }

    public void Continue()
    {
        CursorController.Lock();
        this.gameObject.SetActive(false);
    }
}

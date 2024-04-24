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

    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Continue();
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        CursorController.Unlock();
    }

    private void OnDisable()
    {
        CursorController.Lock();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        OverlayScreen.Restart();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        OverlayScreen.MainMenu();
    }

    public void Continue()
    {
        this.gameObject.SetActive(false);
    }
}

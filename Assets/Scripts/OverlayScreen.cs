using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayScreen : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public static void MainMenu()
    {
        //this is run when the 'home' or 'main menu' button is pressed
        //you can put the stuff in here for saving the stars
        SceneManager.LoadSceneAsync(0);
    }

    public static void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void Continue()
    {
        CursorController.Lock();
        this.gameObject.SetActive(false);
    }
}

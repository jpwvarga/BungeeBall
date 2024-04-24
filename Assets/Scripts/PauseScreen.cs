using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;

    void Start()
    {
        pauseScreen.SetActive(false);
    }
    public void PauseScreenRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseScreenGotoMainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void PauseScreenContinueButton()
    {
        pauseScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}

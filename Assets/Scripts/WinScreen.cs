using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        //this is run when the 'home' or 'main menu' button is pressed
        //you can put the stuff in here for saving the stars
        SceneManager.LoadSceneAsync(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}

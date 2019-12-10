using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    private bool optionState;

    private void Start()
    {
        optionState = false;
    }

    private void Update()
    {
        if (optionState)
        {
            options.SetActive(true);
            mainMenu.SetActive(false);
        }
        else
        {
            options.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu() {
        optionState = false;
    }

    public void GameOptions() {
        optionState = true;
    }

    public void GameQuit() {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    

    private bool _optionState;

    private void Start()
    {
        _optionState = false;
    }

    private void Update()
    {
        if (_optionState)
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
        _optionState = false;
    }

    public void GameOptions() {
        _optionState = true;
    }

    public void GameQuit() {
        Application.Quit();
    }

}

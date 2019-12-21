using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Language{
    english,
    russian
}

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
    private Language m_language;

    public string languagePath;
    public bool reloadLanguage;

    public Language language {
        get { return m_language; }

        set
        {
            switch (value) {
                case Language.english:
                    languagePath = "Data/English";

                    break;
                case Language.russian:
                    
                    languagePath = "Data/Russian";
                    break;
              
            }
            reloadLanguage = true;
            m_language = value;
        }
    }


    private void Awake()
    {
        language = Language.english;
    }
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu() {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GameOptions() {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ChooseEnglish() {
        language = Language.english;
        Debug.Log("allo");
    }

    public void ChooseRussian() {
        language = Language.russian;
        Debug.Log("chose");
        Debug.Log(languagePath);
    }

    public void GameQuit() {
        Application.Quit();
    }

}

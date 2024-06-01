using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenuSreen;
    public GameObject settingsScreen;
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        mainMenuSreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void OpenMainMenu()
    {
        mainMenuSreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetEnemySpeed(float speed)
    {
        GameController.EnamySpeed = speed;
    }

    public void OpenMainMenuFromDeadState()
    {
        SceneManager.LoadScene("Menu");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mainMenuSreen;
    public GameObject settingsScreen;
    private AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Invoke(nameof(LoadGame), 0.5f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettings()
    {
        audioSource.PlayOneShot(buttonClickSound);
        mainMenuSreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void OpenMainMenu()
    {
        audioSource.PlayOneShot(buttonClickSound);
        mainMenuSreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Application.Quit();
    }

    public void SetEnemySpeed(float speed)
    {
        GameController.EnamySpeed = speed;
    }

    public void OpenMainMenuFromDeadState()
    {
        audioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("Menu");
    }
}
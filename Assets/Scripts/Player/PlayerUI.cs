using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    private int itemCount;
    public GameObject[] images;
    public GameObject trapDisplay;
    public GameObject gun;
    public PlayerInteract player;

    public GameObject gameOverScreen;
    public GameObject escMenu;

    public Slider sprintSlider;
    public GameObject winScreen;

    public AudioSource audioSource;
    public AudioClip itemPickupSound;
    public AudioClip finalGunPartPickupSound;
    public AudioClip buttonClickSound;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        sprintSlider.maxValue = gameObject.GetComponent<PlayerMotor>().maxSprintCapacity;
        UpdateSprintDisplay();
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void GetItem()
    {

        itemCount += 1;
        images[itemCount - 1].SetActive(true);
        if (itemCount == 3)
        {
            gun.SetActive(true);
            player.hasGun = true;
            audioSource.PlayOneShot(finalGunPartPickupSound);
        }
        else
        {
            audioSource.PlayOneShot(itemPickupSound);
        }
        Debug.Log(itemCount);
    }

    public void GetArmItem()
    {
        audioSource.PlayOneShot(itemPickupSound);
        player.hasArm = true;
        images[itemCount].SetActive(true);
        Debug.Log("has arm");
    }

    public void GetTrapItem()
    {
        audioSource.PlayOneShot(itemPickupSound);
        GameController.TrapCount += 1;
        UpdateTrapDisplay();

    }

    public void UpdateTrapDisplay()
    {
        if (GameController.TrapCount == 0)
        {
            trapDisplay.SetActive(false);
        }
        else
        {
            trapDisplay.SetActive(true);
            trapDisplay.GetComponentInChildren<TextMeshProUGUI>().text = GameController.TrapCount.ToString();
        }
    }

    public void DisplayGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
        StopGameProcessing();
    }

    public void UpdateSprintDisplay()
    {
        sprintSlider.value = gameObject.GetComponent<PlayerMotor>().sprintCapacity;
    }

    public void WinGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winScreen.SetActive(true);
        StopGameProcessing();
    }

    private void StopGameProcessing()
    {
        Time.timeScale = 0;
        GameObject.Find("Player").GetComponent<PlayerInteract>().isDead = true;
        GameObject.Find("Player").GetComponent<PlayerMotor>().speed = 0;
        GameObject.Find("Player").GetComponent<PlayerMotor>().sprintCapacity = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                EscMenuClose();
            }
            else
            {
                EscMenuOpen();
            }
        }
    } 
    private void EscMenuOpen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        escMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    private void EscMenuClose()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
    }

    public void EscToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        audioSource.PlayOneShot(buttonClickSound);
        Application.Quit();
    }
}



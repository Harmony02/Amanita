using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    private int itemCount;
    public GameObject[] images;
    public GameObject gun;
    public PlayerInteract player;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        itemCount = 0;
        Cursor.lockState = CursorLockMode.Locked;
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
        }
        Debug.Log(itemCount);
    }

    public void GetArmItem()
    {
        player.hasArm = true;
        images[itemCount].SetActive(true);
        Debug.Log("has arm");
    }

    public void DisplayGameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverScreen.SetActive(true);
    }
}

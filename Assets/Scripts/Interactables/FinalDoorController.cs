using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorController : Interactable
{
    [SerializeField]
    private GameObject finalDoor;

    void Start()
    {
        finalDoor = GameObject.FindGameObjectWithTag("FinalDoor");
    }
    protected override void Interact()
    {
        if (GameObject.Find("Player").GetComponent<PlayerInteract>().hasArm)
        {
            finalDoor.GetComponent<Animator>().SetBool("IsOpen", true);
            GameObject.Find("Player").GetComponent<PlayerUI>().WinGame();
        }
        else Debug.Log("You need the arm to open this door");
    }
}

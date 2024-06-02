using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorControl : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    public AudioSource audioSource;
    public AudioClip doorOpenSound;
    public AudioClip terminalInteractSound;

    private GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        Debug.Log(doors.Length);
    }
    protected override void Interact()
    {
        audioSource.PlayOneShot(terminalInteractSound);
        audioSource.PlayOneShot(doorOpenSound);

        foreach (GameObject door in doors)
        {
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 1)
                doorOpen = true;
            else
                doorOpen = false;

            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }

}

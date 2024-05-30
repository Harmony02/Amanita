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

    private GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        doors = GameObject.FindGameObjectsWithTag("Door");
        Debug.Log(doors.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        foreach(GameObject door in doors)
        {
        int random = UnityEngine.Random.Range(0 , 2);
        if(random==1)
        doorOpen = true;
        else 
        doorOpen = false;
        
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        Debug.Log(doorOpen);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : Interactable
{
    // public Transform transform;

    public PlayerUI playerUI;

    void Start()
    {
        
        // transform = gameObject.AddComponent<Transform>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
    }

    protected override void Interact()
    {
        Destroy(gameObject);
        if(gameObject.name == "Arm")
        {
            playerUI.GetArmItem();
        }
        else{
            playerUI.GetItem();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : Interactable
{
    // public Transform transform;

    private PlayerUI playerUI;

    void Start()
    {
        playerUI = GameObject.Find("Player").GetComponent<PlayerUI>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime, Space.Self);
    }

    protected override void Interact()
    {
        Destroy(gameObject);
        if (gameObject.name == "Arm(Clone)")
        {
            playerUI.GetArmItem();

        }
        else if (CompareTag("Trap"))
        {
            playerUI.GetTrapItem();
        }
        else
        {
            playerUI.GetItem();
        }
    }
}

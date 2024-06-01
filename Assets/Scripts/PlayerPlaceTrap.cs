using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceTrap : MonoBehaviour
{
    private InputManager inputManager;
    public GameObject trap;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }
    void Update()
    {
        if (inputManager.onFoot.Trap.triggered)
        {
            Debug.Log("Trap");
            Instantiate(trap, transform.position, Quaternion.identity);
        }
    }
}

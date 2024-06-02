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
            if (GameController.TrapCount == 0)
            {
                return;
            }

            GameController.TrapCount -= 1;

            Instantiate(trap, transform.position + transform.forward, Quaternion.identity);
            gameObject.GetComponent<PlayerUI>().UpdateTrapDisplay();
        }
    }
}

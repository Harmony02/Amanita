using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;
    private InputManager inputManager;
    public bool hasGun;
    public float gunDistance;
    public LayerMask gunMask;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo,distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null) 
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if(inputManager.onFoot.interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
        if(hasGun)
        {
            if(inputManager.onFoot.Shoot.triggered)
                {
                    Debug.Log("shoot");
                    if (Physics.Raycast(ray,out hitInfo,gunDistance, gunMask))
                    {
                        Debug.Log("Shot enemy!!!");
                        Destroy(hitInfo.transform.gameObject);
                    }
                }

        }

    }
}
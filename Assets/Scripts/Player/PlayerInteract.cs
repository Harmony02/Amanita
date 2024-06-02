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
    public bool hasArm;
    public float gunDistance;
    public LayerMask gunMask;
    public GameObject itemDrop;
    public AudioSource audioSource;
    public AudioClip shootSound1;
    public AudioClip shootSound2;
    public AudioClip enemyDeathSound;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                if (inputManager.onFoot.interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
        if (hasGun)
        {
            if (inputManager.onFoot.Shoot.triggered)
            {
                Debug.Log("shoot");
                PlayRandomShootSound();
                if (Physics.Raycast(ray, out hitInfo, gunDistance, gunMask))
                {
                    Debug.Log("Shot enemy!!!");

                    audioSource.PlayOneShot(enemyDeathSound);
                    Destroy(hitInfo.transform.gameObject);
                    Instantiate(itemDrop, hitInfo.transform.position, transform.rotation);

                }
            }
        }
    }
    public void PlayRandomShootSound()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            audioSource.clip = shootSound1;
        }
        else
        {
            audioSource.clip = shootSound2;
        }
        audioSource.Play();
    }
}

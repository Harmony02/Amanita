using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    public AudioSource walkAudioSource;
    public AudioSource runAudioSource;

    public PlayerInput PlayrInput { get => playerInput; set => playerInput = value; }

    // Start is called before the first frame update
    void Awake()
    {
        PlayrInput = new PlayerInput();
        onFoot = PlayrInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        if (onFoot.Movement.ReadValue<Vector2>() != Vector2.zero)
        {
            if (motor.sprinting)
            {
                if (!runAudioSource.isPlaying)
                {
                    walkAudioSource.Stop();
                    runAudioSource.Play();
                }
            }
            else
            {
                if (!walkAudioSource.isPlaying)
                {
                    runAudioSource.Stop();
                    walkAudioSource.Play();
                }
            }
        }
        else
        {
            walkAudioSource.Stop();
            runAudioSource.Stop();
        }
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}

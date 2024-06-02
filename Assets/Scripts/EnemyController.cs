using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Interactable
{
    // Start is called before the first frame update
    public Transform player;
    public float attackDistance = 2;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool canAttack = true;
    public float timeInTrap = 2;
    public float pathfindDistance = 20f;
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip trapSound;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GameController.EnamySpeed;
        agent.autoBraking = false;
        agent.destination = points[destPoint].position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (agent.speed == 0)
        {
            return;
        }
        if (distanceToPlayer < attackDistance && canAttack)
        {
            AttackPlayer();
        }
        else if (Physics.Raycast(transform.position, directionToPlayer, out hit, pathfindDistance) && hit.transform == player)
        {
            if (!runningAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
                runningAudioSource.Play();
            }
            agent.destination = player.position;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                destPoint = (destPoint + 1) % points.Length;
                Debug.Log(destPoint);
            }

            if (!walkingAudioSource.isPlaying)
            {
                runningAudioSource.Stop();
                walkingAudioSource.Play();
            }

            agent.destination = points[destPoint].position;
        }
    }

    public void AttackPlayer()
    {

        runningAudioSource.Stop();
        audioSource.PlayOneShot(attackSound);
        player.GetComponent<PlayerUI>().DisplayGameOver();
        player.GetComponent<PlayerMotor>().speed = 0;
        canAttack = false;
        agent.speed = 0;
    }

    void OnDestroy()
    {
        audioSource.PlayOneShot(deathSound);
        Debug.Log("i am ded");
    }

    public void EnterTrap()
    {
        runningAudioSource.Stop();
        walkingAudioSource.Stop();
        audioSource.PlayOneShot(trapSound);
        agent.speed = 0;
        Invoke(nameof(ExitTrap), timeInTrap);
    }

    public void ExitTrap()
    {
        agent.speed = GameController.EnamySpeed;
    }
}
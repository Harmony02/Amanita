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
    public float timeInTrap = 8;
    [SerializeField]
    private GameObject zombie;
    private bool seePlayer;
    private bool isStunned;
    private bool Attack = false;
    private bool dead = false;


    public float pathfindDistance = 20f;
    public AudioSource walkingAudioSource;
    public AudioSource runningAudioSource;
    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip trapSound;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GameController.EnamySpeed;
        agent.autoBraking = false;
        agent.destination = points[destPoint].position;
        zombie = GameObject.FindGameObjectWithTag("zombie");
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
            zombie.GetComponent<Animator>().SetBool("attack", Attack);
            AttackPlayer();
        }
        else if (Physics.Raycast(transform.position, directionToPlayer, out hit, pathfindDistance) && hit.transform == player)
        {
            zombie.GetComponent<Animator>().SetBool("seesPlayer", seePlayer);
            if (!runningAudioSource.isPlaying)
            {
                walkingAudioSource.Stop();
                runningAudioSource.Play();
            }
            agent.destination = player.position;
            seePlayer = true;
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
            seePlayer = false;
            zombie.GetComponent<Animator>().SetBool("seesPlayer", seePlayer);
        }
        UpdateCollider();
    }

    public void UpdateCollider()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        boxCollider.center = skinnedMeshRenderer.bounds.center - transform.position;
        boxCollider.size = skinnedMeshRenderer.bounds.size;
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

    public void Die()
    {
        runningAudioSource.Stop();
        walkingAudioSource.Stop();
        dead = true;
        zombie.GetComponent<Animator>().SetBool("isDead", dead);
        agent.speed = 0;
    }

    public void EnterTrap()
    {
        isStunned = true;
        zombie.GetComponent<Animator>().SetBool("stunned", isStunned);
        runningAudioSource.Stop();
        walkingAudioSource.Stop();
        audioSource.PlayOneShot(trapSound);
        Invoke(nameof(ExitTrap), timeInTrap);
        agent.speed = 0;
    }

    public void ExitTrap()
    {
        isStunned = false;
        zombie.GetComponent<Animator>().SetBool("stunned", isStunned);
        agent.speed = GameController.EnamySpeed;
    }
}
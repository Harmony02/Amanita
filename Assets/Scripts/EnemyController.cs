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
    public float pathfindDistance = 10f;
    [SerializeField]
    private GameObject zombie;
    private bool seePlayer;
    
    

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

        if (distanceToPlayer < attackDistance && canAttack)
        {
            AttackPlayer();
        }
        else if (Physics.Raycast(transform.position, directionToPlayer, out hit, pathfindDistance) && hit.transform == player)
        {
            zombie.GetComponent<Animator>().SetBool("seesPlayer", seePlayer);
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
            agent.destination = points[destPoint].position;
            seePlayer = false;
            zombie.GetComponent<Animator>().SetBool("seesPlayer", seePlayer);
        }
    }

    public void AttackPlayer()
    {
        player.GetComponent<PlayerUI>().DisplayGameOver();
        player.GetComponent<PlayerMotor>().speed = 0;
        canAttack = false;
        agent.speed = 0;
    }

    void OnDestroy()
    {
        Debug.Log("i am ded");
    }

    public void EnterTrap()
    {
        agent.speed = 0;
        Invoke(nameof(ExitTrap), timeInTrap);
    }

    public void ExitTrap()
    {
        agent.speed = GameController.EnamySpeed;
    }
}
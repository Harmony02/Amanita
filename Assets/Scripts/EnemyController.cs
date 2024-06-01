using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Interactable
{
    // Start is called before the first frame update
    public Transform player;
    public float attackDistance = 2;
    private NavMeshAgent agent;
    private bool canAttack = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GameController.EnamySpeed;
        Debug.Log("Enemy speed: " + agent.speed);
    }

    void Update()
    {
        agent.destination = player.position;
        if (Vector3.Distance(transform.position, player.position) < attackDistance && canAttack)
        {
            AttackPlayer();
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
}
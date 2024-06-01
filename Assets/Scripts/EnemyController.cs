using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Interactable
{
    // Start is called before the first frame update
    public Transform player;
    private NavMeshAgent agent;

    public GameObject mainMenu;
    public GameObject settingsMenu;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GameController.EnamySpeed;
        Debug.Log("Enemy speed: " + agent.speed);
    }

    void Update()
    {
        agent.destination = player.position;
    }

    void OnDestroy()
    {
        Debug.Log("i am ded");
    }
}
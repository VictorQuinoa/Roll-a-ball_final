using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public Transform player;
    private NavMeshAgent NavMeshAgent;
    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            NavMeshAgent.SetDestination(player.position);
        }
        
    }
    
}

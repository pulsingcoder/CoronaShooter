using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointPatrol : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int currentWayPointIndex;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWayPointIndex].position);
        }
     /*   float distance = Vector3.Distance(transform.position, player.position);
        if (distance < 3)
        {
            navMeshAgent.SetDestination(player.position);
        }*/
    }
}

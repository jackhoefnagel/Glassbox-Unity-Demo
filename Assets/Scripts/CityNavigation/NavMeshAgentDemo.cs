using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentDemo : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public CityNavigation cityNavigation;
    public float newDestinationInterval = 3f;

    private Vector3 newDestination;


    void Start()
    {
        GetComponent<Animator>().SetFloat("offset", Random.Range(0f, 1f));

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (navMeshAgent.isActiveAndEnabled && navMeshAgent.isOnNavMesh)
        {
            if (navMeshAgent.remainingDistance < 1f)
            {
                newDestination = cityNavigation.GetRandomNavigationPoint().position;
                navMeshAgent.SetDestination(newDestination);
            }
        }
    }
}

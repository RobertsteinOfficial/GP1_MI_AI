using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntitiesController : MonoBehaviour
{
    List<NavMeshAgent> agents = new List<NavMeshAgent>();
    public Transform player;

    private void Start()
    {
        NavMeshAgent[] a = FindObjectsOfType<NavMeshAgent>();

        agents.AddRange(a);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;

        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        //    {
        //        foreach (NavMeshAgent agent in agents)
        //        {
        //            agent.SetDestination(hit.point);
        //        }
        //    }
        //}

        foreach (NavMeshAgent agent in agents)
        {
            agent.SetDestination(player.position);
        }
    }
}

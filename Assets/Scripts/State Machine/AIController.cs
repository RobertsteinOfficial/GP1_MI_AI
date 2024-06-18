using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{


    NavMeshAgent agent;
    Animator anim;
    State currentState;

    public Transform player;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, anim, player);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState = currentState.Process();
            Debug.Log(currentState.name);
        }

    }




}

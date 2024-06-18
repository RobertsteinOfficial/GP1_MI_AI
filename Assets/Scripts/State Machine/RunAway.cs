using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : State
{
    GameObject safeSpot;

    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.RUNAWAY;

        safeSpot = GameObject.FindGameObjectWithTag("Safe");
    }


    public override void Enter()
    {
        anim.SetBool("Running", true);
        agent.isStopped = false;
        agent.speed = 6;
        agent.SetDestination(safeSpot.transform.position);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("Running", false);
        base.Exit();
    }
}

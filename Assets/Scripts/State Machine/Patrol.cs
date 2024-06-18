using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    int currentCheckpoint = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PATROL;
        agent.isStopped = false;
        agent.speed = 2;
    }

    public override void Enter()
    {
        float minorDist = Mathf.Infinity;
        for (int i = 0; i < AreaManager.Instance.checkpoints.Length; i++)
        {
            Transform checkpoint = AreaManager.Instance.checkpoints[i];
            float dist = Vector3.Distance(npc.transform.position, checkpoint.position);

            if (dist < minorDist)
            {
                currentCheckpoint = i;
                minorDist = dist;
            }
        }


        anim.SetBool("isWalking", true);
        agent.SetDestination(AreaManager.Instance.checkpoints[currentCheckpoint].position);
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Hide(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        //else if (IsPlayerBehind())
        //{
        //    nextState = new RunAway(npc, agent, anim, player);
        //    stage = EVENT.EXIT;
        //}

        if (Vector3.Distance(npc.transform.position, agent.destination) < 2)
        {
            currentCheckpoint++;


            if (currentCheckpoint >= AreaManager.Instance.checkpoints.Length)
                currentCheckpoint = 0;

            agent.SetDestination(AreaManager.Instance.checkpoints[currentCheckpoint].position);

        }

    }

    public override void Exit()
    {
        anim.SetBool("isWalking", false);

        base.Exit();
    }
}

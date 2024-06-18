using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    float lookAheadMultiplier = 1f;

    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.PURSUE;
        agent.speed = 15;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetBool("isRunning", true);
        base.Enter();
    }

    public override void Update()
    {
        Intercept();

        if (!agent.hasPath) return;

        if (CanAttackPlayer())
        {
            //Attacca Player
            nextState = new Attack(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (!CanSeePlayer())
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.SetBool("isRunning", false);
        base.Exit();
    }

    public void Intercept()
    {
        Vector3 targetDir = player.transform.position - npc.transform.position;

        float angle = Vector3.Angle(npc.transform.forward, npc.transform.TransformVector(player.transform.forward));
        float angleToTarget = Vector3.Angle(npc.transform.forward, npc.transform.TransformVector(targetDir));

        if (angle < 20 && angleToTarget < 20 || player.GetComponent<Player>().moveVector.magnitude < 0.01f)
        {
            Debug.Log("Stop");
            Seek(player.transform.position);
        }


        float lookAhead = targetDir.magnitude / (agent.speed + player.transform.GetComponent<Player>().speed);
        Seek(player.transform.position + player.transform.forward * lookAhead * lookAheadMultiplier);
    }

   
}

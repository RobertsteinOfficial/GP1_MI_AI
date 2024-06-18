using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Evade : State
{
    public Evade(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.EVADE;
        agent.speed = 6;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        EvadePlayer();

        if (Vector3.Distance(npc.transform.position, player.position) > 10)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    void EvadePlayer()
    {
        Vector3 targetDir = player.position - npc.transform.position;
        float lookAhead = targetDir.magnitude / (agent.speed + player.GetComponent<Player>().speed);

        Flee(player.position + player.forward * lookAhead);
    }

    void Flee(Vector3 point)
    {
        Vector3 destination = point - npc.transform.position;
        agent.SetDestination(npc.transform.position - destination);
    }
}

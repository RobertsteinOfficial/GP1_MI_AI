using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : State
{
    Vector3 wanderDestination = Vector3.zero;

    public Wander(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.WANDER;
        agent.isStopped = false;
        agent.speed = 2;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        AIWander();


    }

    public override void Exit()
    {
        base.Exit();
    }

    void AIWander()
    {
        float wanderRadius = 10f;
        float wanderDistance = 20f;
        float wanderJitter = 1;

        wanderDestination += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0f, Random.Range(-1.0f, 1.0f) * wanderJitter);
        wanderDestination.Normalize();
        wanderDestination *= wanderRadius;

        Vector3 targetLocal = wanderDestination + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = npc.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }
}

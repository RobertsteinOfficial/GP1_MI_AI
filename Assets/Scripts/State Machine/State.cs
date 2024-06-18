using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class State
{
    public enum STATE
    {
        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        SLEEP,
        RUNAWAY,
        EVADE,
        WANDER,
        HIDE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    float visionDistance = 15.0f;
    float visionAngle = 90.0f;
    float attackDistance = 2.0f;
    float scareDistance = 2.0f;


    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        anim = _anim;
        player = _player;
        agent = _agent;

        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();

            return nextState;
        }
        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visionDistance && angle < visionAngle)
        {
            return true;
        }

        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;

        if (direction.magnitude < attackDistance)
            return true;
        else
            return false;
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < scareDistance && angle < 30.0f)
        {
            return true;
        }

        return false;

    }

    public void Seek(Vector3 point)
    {
        agent.SetDestination(point);
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    float rotationSpeed = 2.0f;
    AudioSource source;

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ATTACK;
        source = npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        anim.SetTrigger("Attack");
        agent.isStopped = true;
        source.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation =
            Quaternion.Slerp(
                npc.transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * rotationSpeed);

        if (!CanAttackPlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Attack");
        source.Stop();
        base.Exit();
    }
}

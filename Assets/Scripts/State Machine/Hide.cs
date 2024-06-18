using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hide : State
{
    public Hide(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.HIDE;
        agent.isStopped = false;
        agent.speed = 8;
    }

    public override void Enter()
    {
        HideMySelf2();
        base.Enter();
    }

    public override void Update()
    {

        if (Vector3.Distance(npc.transform.position, agent.destination) <= 2)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }


    private void HideMySelf()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        for (int i = 0; i < AreaManager.Instance.HidingSpots.Length; i++)
        {
            Vector3 hideSpotPos = AreaManager.Instance.HidingSpots[i].transform.position;
            Vector3 hideDir = hideSpotPos - player.transform.position;
            Vector3 hidePos = hideSpotPos + hideDir.normalized * 5;

            float hideDist = Vector3.Distance(npc.transform.position, hidePos);

            if (hideDist < dist)
            {
                chosenSpot = hidePos;
                dist = hideDist;
            }
        }

        Seek(chosenSpot);
    }

    private void HideMySelf2()
    {
        float dist = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenHideSpot = AreaManager.Instance.HidingSpots[0];

        for (int i = 0; i < AreaManager.Instance.HidingSpots.Length; i++)
        {
            Vector3 hideSpotPos = AreaManager.Instance.HidingSpots[i].transform.position;
            Vector3 hideDir = hideSpotPos - player.transform.position;
            Vector3 hidePos = hideSpotPos + hideDir.normalized * 10;

            float hideDist = Vector3.Distance(npc.transform.position, hidePos);

            if (hideDist < dist)
            {
                chosenSpot = hidePos;
                chosenDir = hideDir;
                dist = hideDist;
                chosenHideSpot = AreaManager.Instance.HidingSpots[i];
            }
        }

        Collider hideCol = chosenHideSpot.GetComponent<Collider>();
        Ray backRay = new Ray(chosenSpot, -chosenDir.normalized);

        hideCol.Raycast(backRay, out RaycastHit hit, 50);



        Seek(hit.point + chosenDir.normalized * 2);

    }
}

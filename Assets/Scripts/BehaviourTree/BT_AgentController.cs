using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace BehaviourTree
{

    public class BT_AgentController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform wayOut;
        [SerializeField] private float stopDistance = 3f;
        [SerializeField] private Transform frontDoor;
        [SerializeField] private Transform backDoor;

        BehaviourTree tree;
        NavMeshAgent agent;
        BTNode.Status treeStatus = BTNode.Status.Running;

        public enum ActionState { Idle, Active };
        ActionState state = ActionState.Idle;


        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();

            tree = new BehaviourTree();
            BTSequence getItem = new BTSequence("Get the item");
            BTLeaf goToItem = new BTLeaf("Go to item", GoToItem);
            BTLeaf goToBackDoor = new BTLeaf("Go To Back Door", GoToBackDoor);
            BTLeaf goToFrontDoor = new BTLeaf("Go To Front Door", GoToFrontDoor);
            BTLeaf escape = new BTLeaf("Escape", Escape);
            BTSelector selectDoor = new BTSelector("Select Door");

            tree.AddChild(getItem);
            getItem.AddChild(selectDoor);
            getItem.AddChild(goToItem);
            getItem.AddChild(escape);

            selectDoor.AddChild(goToFrontDoor);
            selectDoor.AddChild(goToBackDoor);

            tree.PrintTree();
        }


        private void Update()
        {
            if (!(treeStatus == BTNode.Status.Running)) return;
            treeStatus = tree.Process();
        }

        BTNode.Status GoToLocation(Vector3 pos)
        {
            float distanceToTarget = Vector3.Distance(pos, transform.position);


            if (state == ActionState.Idle)
            {
                agent.SetDestination(pos);
                state = ActionState.Active;
            }
            else if (Vector3.Distance(agent.pathEndPosition, pos) >= stopDistance)
            {
                state = ActionState.Idle;
                return BTNode.Status.Failure;
            }
            else if (distanceToTarget <= stopDistance)
            {
                state = ActionState.Idle;
                return BTNode.Status.Success;
            }

            return BTNode.Status.Running;
        }

        public BTNode.Status GoToItem()
        {
            return GoToLocation(target.position);
        }

        public BTNode.Status Escape()
        {
            return GoToLocation(wayOut.position);
        }

        private BTNode.Status GoToDoor(Transform door)
        {
            BTNode.Status status = GoToLocation(door.position);

            if (status == BTNode.Status.Success)
            {
                if (door.GetComponent<Door>().isLocked) return BTNode.Status.Failure;

                door.parent.gameObject.SetActive(false);
                return BTNode.Status.Success;
            }
            else
                return status;
        }

        private BTNode.Status GoToFrontDoor()
        {
            return GoToDoor(frontDoor);
        }

        private BTNode.Status GoToBackDoor()
        {
            return GoToDoor(backDoor);
        }

        
    }

}
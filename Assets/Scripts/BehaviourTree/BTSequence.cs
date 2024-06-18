using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class BTSequence : BTNode
    {
        public BTSequence(string n)
        {
            name = n;
        }

        public override Status Process()
        {
            Debug.Log(children[currentChild].name);
            Status childStatus = children[currentChild].Process();

            if (childStatus == Status.Running ||
                childStatus == Status.Failure) return childStatus;

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return Status.Success;
            }

            return Status.Running;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTree
{
    public class BTSelector : BTNode
    {
        public BTSelector(string n)
        {
            name = n;
        }

        public override Status Process()
        {
            Status childStatus = children[currentChild].Process();
            if (childStatus == Status.Running) return Status.Running;
            if (childStatus == Status.Success)
            {
                currentChild = 0;
                return Status.Success;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return Status.Failure;
            }

            return Status.Running;
        }
    }
}

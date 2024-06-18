using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class BTLeaf : BTNode
    {
        public delegate Status Tick();
        public Tick ProcessMethod;

        public BTLeaf() { }
        public BTLeaf (string _name, Tick _method)
        {
            name = _name;
            ProcessMethod = _method;
        }

        public override Status Process()
        {
            if (ProcessMethod == null) return Status.Failure;

            return ProcessMethod();
        }
    }
}
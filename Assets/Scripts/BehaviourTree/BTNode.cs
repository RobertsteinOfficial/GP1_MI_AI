using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    struct NodeLevel
    {
        public int level;
        public BTNode node;
    }

    public class BTNode
    {
        public enum Status { Success, Running, Failure };
        public Status status;

        public List<BTNode> children = new List<BTNode>();
        public int currentChild = 0;
        public string name;

        public BTNode() { }

        public BTNode(string _name)
        {
            name = _name;
        }

        public void AddChild(BTNode node)
        {
            children.Add(node);
        }

        public virtual Status Process()
        {
            return children[currentChild].Process();
        }
    }

}
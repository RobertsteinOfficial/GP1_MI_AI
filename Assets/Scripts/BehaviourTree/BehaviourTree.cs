using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class BehaviourTree : BTNode
    {
        public BehaviourTree()
        {
            name = "Tree";
        }

        public void PrintTree()
        {
            string treePrintOut = "";
            Stack<NodeLevel> stack = new Stack<NodeLevel>();
            BTNode currentNode = this;

            stack.Push(new NodeLevel { level = 0, node = currentNode });

            while (stack.Count != 0)
            {
                NodeLevel nextNode = stack.Pop();
                treePrintOut += new string('-', nextNode.level) + nextNode.node.name + "\n";

                for (int i = nextNode.node.children.Count - 1; i >= 0; i--)
                {
                    stack.Push(new NodeLevel { level = nextNode.level + 1, node = nextNode.node.children[i] });
                }
            }

            Debug.Log(treePrintOut);
        }

        public override Status Process()
        {
            return children[currentChild].Process();
        }

    }
}

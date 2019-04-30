using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoffmanAlgorithm
{
    //--------------------------------NODE-----------------------------
    public class Node : IComparable
    {
        int value;
        char ch;
        public Node left = null;
        public Node right = null;

        public Node(char ch, int value)
        {
            this.value = value;
            this.ch = ch;
        }

        public Node(Node nodeR, Node nodeL)
        {
            this.left = nodeL;
            this.right = nodeR;
            this.value = nodeR.value + nodeL.value;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Node otherNode = obj as Node;
            if (otherNode != null)
                return this.value.CompareTo(otherNode.value);
            else
                throw new ArgumentException("Object is not a Node");
        }

        public char Ch
        {
            get { return this.ch; }
        }

    }
    //--------------------------------------------------------------------------------- ~NODE
}

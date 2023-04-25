using System.Collections.Generic;

namespace LinkedList
{
    partial class Program
    {
        public class Node
        {
            public int val;
            public Node next;
            public Node random;

            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
            }

            public Node InsertAtTail(int value, Node head)
            {
                var last = head;
                Node node = new Node(value);

                while (last.next != null)
                {
                    last = last.next;
                }

                node.next = null;
                last.next = node;

                return head;
            }

              

            public Node CopyRandomList(Node head)
            {
                if (head == null) return null;

                var copy = new Dictionary<Node, Node>();
                Node current = head;

                //copy the nodes without random and next
                while(current != null)
                {
                    copy.Add(current, new Node(current.val));
                    current = current.next;
                }

                current = head;

                //connect the random and next points
                while(current != null)
                {
                    copy[current].next = current.next != null ? copy[current.next] : null;
                    copy[current].random = current.random != null ? copy[current.random] : null;
                    current = current.next;
                }

                return copy[head];
            }

            

        }
    }
    }


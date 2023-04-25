using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    using System.Collections.Generic;

    public class LRUCache
    {
        public class DLinkedNode
        {
            public int Key;
            public int Value;
            public DLinkedNode Prev;
            public DLinkedNode Next;
        }

        private void AddNode(DLinkedNode node)
        {
            node.Prev = head;
            node.Next = head.Next;
            head.Next.Prev = node;
            head.Next = node;
        }

        private void RemoveNode(DLinkedNode node)
        {
            //node reps the LRU
            DLinkedNode prev = node.Prev;
            DLinkedNode next = node.Next;

            prev.Next = next;
            next.Prev = prev;
        }

        private void MoveToHead(DLinkedNode node)
        {
            //Move certain node in between to the head
            RemoveNode(node);
            AddNode(node);
        }

        private DLinkedNode PopTail()
        {
            //Remove the last node
            DLinkedNode res = tail.Prev; //gets the element just before the tail. This is the LRU.
            RemoveNode(res);
            return res;
        }

        private Dictionary<int, DLinkedNode> cache = new Dictionary<int, DLinkedNode>();
        private int size; //helps to keep track of the capacity
        private int capacity;
        private DLinkedNode head, tail;

        public LRUCache(int capacity)
        {
            this.size = 0;
            this.capacity = capacity;
            head = new DLinkedNode();
            tail = new DLinkedNode();
            //set the pointers
            head.Next = tail;
            tail.Prev = head;
        }

        public int Get(int key)
        {
            //if the key doesn't exist
            if(!cache.TryGetValue(key, out DLinkedNode node))
            {
                return -1;
            }

            //Move the accessed key to the head
            MoveToHead(node);
            return node.Value;

        }

        public void Put(int key, int value)
        {
            //if the key doesn't exist, we need to add it to the node
            if(!cache.TryGetValue(key, out DLinkedNode node))
            {
                DLinkedNode newNode = new DLinkedNode();
                newNode.Key = key;
                newNode.Value = value;

                //add to the dictionary
                cache.Add(key, newNode);
                //add to the linkedlist
                AddNode(newNode);

                ++size; //keeps track of the capacity
                if(size > capacity)
                {
                    //pop the tail of the list
                    DLinkedNode tail = PopTail();
                    //Remove it from the cache(dict)
                    cache.Remove(tail.Key);
                    --size;//decrease the size
                }
            }
            else //if the size is not greater than capacity, just add to the begining
            {
                node.Value = value;
                MoveToHead(node);
            }

        }
    }


}

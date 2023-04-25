using System;

namespace LinkedList_Difficult
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode list = new ListNode(1);
            ListNode list2 = new ListNode(1);
            ListNode list3 = new ListNode(2);
            ListNode list4 = new ListNode(5);
            ListNode list5 = new ListNode(10);
            ListNode list6 = new ListNode();
            

            list.InsertAtTail(4, list);
            list.InsertAtTail(5, list);
          
            list2.InsertAtTail(3, list2);
            list2.InsertAtTail(4, list2);

        //    list3.InsertAtTail(1, list3);
            list3.InsertAtTail(6, list3);
           // list3.InsertAtTail(9, list3);

           // list4.InsertAtTail(5, list4);
            list4.InsertAtTail(7, list4);
            list4.InsertAtTail(9, list4);

            list5.InsertAtTail(11, list5);
            list5.InsertAtTail(12, list5);

            var arr = new ListNode[0];
            var result = list.MergeKLists(arr);

            Console.WriteLine("Hello World!");
        }

        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }

            public ListNode ReverseKGroup(ListNode head, int k)
            {
                ListNode newHead = null; //the head of the reversed list
                ListNode tail = null; //the tail of the unreversed portion
                ListNode pointer = head; //to transverse the list

                while (pointer != null)
                {
                    //verify that there are knodes to reverse
                    int count = 0;
                    while (count < k && pointer != null)
                    {
                        pointer = pointer.next;
                        count++;
                    }

                    //if we have knodes, reverse them 
                    if (count == k)
                    {
                        var revHead = ReverseList(head, k);

                        //set the newHead if it's still null
                        if (newHead == null)
                            newHead = revHead;

                        //chain the new set of rev nodes to the existing set of rev nodes
                        if (tail != null)
                        {
                            tail.next = revHead;
                        }

                        //update the tail of the unreversed portion
                        tail = head;

                        //Update the head of the unreversed portion
                        head = pointer;

                    }
                }

                //if there are unreversed nodes, chain them to the reversed list via the head;
                if (tail != null)
                {
                    tail.next = head;
                }

                return newHead == null ? head : newHead;

            }
            public ListNode ReverseList(ListNode head, int k)
            {
                var current = head;
                ListNode previous = null;

                while (k > 0)
                {
                    var temp = current.next;
                    current.next = previous;
                    previous = current;
                    current = temp;
                    k--;
                }

                return previous;
            }
            public ListNode InsertAtTail(int value, ListNode head)
            {
                var last = head;
                ListNode node = new ListNode(value);

                while (last.next != null)
                {
                    last = last.next;
                }

                node.next = null;
                last.next = node;

                head = head.next;
                return head;
            }

            /// <summary>
            /// Time: O(NLogK), 
            ///       N = Number of nodes
            ///       K = Length of the lists
            /// Space:0(1): the dummy list      
            /// </summary>
            /// <param name="lists"></param>
            /// <returns></returns>
            public ListNode MergeKLists(ListNode[] lists)
            {
                if (lists.Length < 1) return null;
                int interval = 1;
                int length = lists.Length;

                while(interval < length)
                {
                    for(int i = 0; i < (length-interval); i += (interval * 2))
                    {
                        lists[i] = Merge2Lists(lists[i], lists[i + interval]);
                    }
                    interval *= 2;
                }

                return lists.Length < 1 ? null : lists[0];
            }

            //public ListNode Merge2Lists(ListNode l1, ListNode l2)
            //{
            //    var dummyList = new ListNode(0);
            //    var pointer = dummyList;

            //    while(l1 != null && l2 != null)
            //    {
            //        if(l1.val <= l2.val)
            //        {
            //            pointer.next = l1;
            //            l1 = l1.next;
            //        }
            //        else
            //        {
            //            pointer.next = l2;
            //            l2 = l2.next;
            //        }
            //        pointer = pointer.next;
            //    }

            //    pointer.next = l1 != null ? l1 : l2;
            //    return dummyList.next;

            //}

            public ListNode Merge2Lists(ListNode l1, ListNode l2)
            {
                ListNode head = new ListNode(0);
                ListNode point = head;
                while (l1 != null && l2 != null)
                {
                    Console.WriteLine($"L1: {l1.val}");
                    Console.WriteLine($"L2: {l2.val}");
                    Console.WriteLine();
                    if (l1.val <= l2.val)
                    {
                        point.next = l1;
                        l1 = l1.next;
                    }
                    else
                    {
                        point.next = l2;
                        l2 = l1;
                        l1 = point.next.next;
                    }
                    point = point.next;
                }
                if (l1 == null)
                {
                    point.next = l2;
                }
                else
                {
                    point.next = l1;
                }
                return head.next;
            }
        }
    }
}

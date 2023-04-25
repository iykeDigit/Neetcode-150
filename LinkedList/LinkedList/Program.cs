using System;
using System.Collections.Generic;

namespace LinkedList
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ListNode list = new ListNode(1);
            ListNode list2 = new ListNode();
            ListNode list3 = new ListNode();
            ListNode list4 = new ListNode();
           // list.InsertAtTail(1,list);
            list.InsertAtTail(2,list);
            list.InsertAtTail(3, list);
            list.InsertAtTail(4, list);
            list.InsertAtTail(5, list);
            list.InsertAtTail(6, list);
            list.InsertAtTail(7, list);


            list2.InsertAtTail(2, list2);
            list2.InsertAtTail(5, list2);
            list2.InsertAtTail(7, list2);

            list3.InsertAtTail(1, list3);
            list3.InsertAtTail(8, list3);
            list3.InsertAtTail(9, list3);

            list4.InsertAtTail(5, list4);
            list4.InsertAtTail(7, list4);
            list4.InsertAtTail(9, list4);

            var arr = new ListNode[] { list, list2, list3, list4 };
            
            var m = list.ReverseKGroup(list, 3);

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

            #region Reverse K Group
            public ListNode ReverseKGroup(ListNode head, int k)
            {
                ListNode ptr = head;
                ListNode kTail = null;

                // Head of the final, modified linked list
                ListNode newHead = null;

                // Keep going until there are nodes in the list
                while (ptr != null)
                {
                    int count = 0;

                    // Start counting nodes from the head
                    ptr = head;

                    // Find the head of the next k nodes
                    while (count < k && ptr != null)
                    {
                        ptr = ptr.next;
                        count += 1;
                    }

                    // If we counted k nodes, reverse them        
                    if (count == k)
                    {
                        // Reverse k nodes and get the new head
                        ListNode revHead = ReverseLinkedList(head, k);

                        // newHead is the head of the final linked list
                        if (newHead == null)
                            newHead = revHead;

                        // kTail is the tail of the previous block of 
                        // reversed k nodes
                        if (kTail != null)
                            kTail.next = revHead;

                        kTail = head;
                        head = ptr;
                    }
                }

                // attach the final, possibly un-reversed portion
                if (kTail != null)
                    kTail.next = head;

                return newHead == null ? head : newHead;
            }

            public ListNode ReverseLinkedList(ListNode head, int k)
            {
                // Reverse k nodes of the given linked list.
                // This function assumes that the list contains 
                // atleast k nodes.
                ListNode newHead = null;
                ListNode ptr = head;

                while (k > 0)
                {
                    // Keep track of the next node to process in the
                    // original list
                    ListNode nextNode = ptr.next;

                    // Insert the node pointed to by "ptr"
                    // at the beginning of the reversed list
                    ptr.next = newHead;
                    newHead = ptr;

                    // Move on to the next node
                    ptr = nextNode;

                    // Decrement the count of nodes to be reversed by 1
                    k--;
                }

                // Return the head of the reversed list
                return newHead;
            }

            public ListNode ReverseKGroupBruteForce(ListNode head, int k)
            {
                ListNode result = new ListNode();
                var pointer = result;
                ListNode finalResult = result;
                var current = head;

                while (current != null)
                {
                    var list = new List<int>();
                    for (int i = 0; i < k; i++)
                    {
                        if (current != null)
                        {
                            list.Add(current.val);
                            current = current.next;
                        }
                        else break;
                    }

                    //add from the back
                    ListNode reverse = new ListNode();
                    var reversedData = reverse;
                    var reversedList = reverse;
                    
                    if(list.Count == k)
                    {
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            reversedData.next = new ListNode(list[i]);
                            reversedData = reversedData.next;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            reversedData.next = new ListNode(list[i]);
                            reversedData = reversedData.next;
                        }

                    }

                    pointer.next = reversedList.next;
                    pointer = pointer.next;
                    while(pointer.next != null)
                    {
                        pointer = pointer.next;
                    }
                }

                return result.next;
            }
            #endregion

            #region Merge K Lists

            public ListNode MergeKLists(ListNode[] lists)
            {
                int length = lists.Length;
                int interval = 1;

                while (interval < length)
                {
                    for (int i = 0; i < (length - interval); i += (interval * 2)) //skip 2 lists at each iteration
                    {
                        lists[i] = Merge2Lists(lists[i], lists[interval + i]);
                    }

                    interval *= 2;// amount to skip for a new set of iterations
                }

                return length > 0 ? lists[0] : null;

            }
            public ListNode Merge2Lists(ListNode a, ListNode b)
            {
                ListNode head = new ListNode(0);
                var point = head;
                ListNode l1 = a;
                ListNode l2 = b;
                while (l1 != null && l2 != null)
                {
                    if (l1.val <= l2.val)
                    {
                        point.next = l1;
                        l1 = l1.next;
                    }
                    else
                    {
                        point.next = l2;
                        l2 = l2.next;
                    }

                    point = point.next;
                }

                if (l1 == null)
                {
                    point.next = l2;
                }

                if (l2 == null)
                {
                    point.next = l1;
                }

                return head.next;
            }

            #endregion

            public ListNode ReverseList(ListNode head)
            {
                ListNode current = head;
                ListNode prev = null;
                ListNode temp = null;

                while(current != null)
                {
                    temp = current.next;
                    current.next = prev;
                    prev = current;
                    current = temp;
                }
                head = prev;
                return head;
            }

            /// <summary>
            /// Initialize a dummy node, initialize a tail var and set equal to the dummy node
            /// Iterate through list1 and list2 and compare the values
            /// if list1 is greater, tail.next = list1, update list1 to list1.next
            /// Vice versa if list 2 is greater. Do this while neither of them is null
            /// If either one gets empty, point tail.next to the non-empty list
            /// return dummy.next
            /// </summary>
            /// <param name="list1"></param>
            /// <param name="list2"></param>
            /// <returns></returns>
            public ListNode MergeTwoLists(ListNode list1, ListNode list2)
            {
                var dummyList = new ListNode();
                var tail = dummyList;

                while(list1 != null && list1 != null)
                {
                    if(list1.val < list2.val)
                    {
                        tail.next = list1;
                        list1 = list1.next;
                    }
                    else
                    {
                        tail.next = list2;
                        list2 = list2.next;
                    }
                    tail = tail.next;
                }

                if(list2 != null)
                {
                    tail.next = list2;
                }
                if (list1 != null)
                {
                    tail.next = list1;
                }
                return dummyList.next;
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

            public void ReorderList(ListNode head)
            {
                //find the middle of the list
                var slow = head;
                var fast = head.next;

                while(fast != null && fast.next != null)
                {
                    slow = slow.next;
                    fast = fast.next.next;
                }

                //reverse the second half of the list
                var second = slow.next;
                ListNode previous = null;
                slow.next = null;
                while(second != null)
                {
                    var temp = second.next;
                    second.next = previous;
                    previous = second;
                    second = temp;
                }
                var first = head;
                second = previous;

                while(second != null)
                {
                    var temp1 = first.next;
                    var temp2 = second.next;
                    first.next = second;
                    second.next = temp1;
                    first = temp1;
                    second = temp2;
                }
                
            }

            public ListNode RemoveNthFromEnd(ListNode head, int n)
            {
                var dummyNode = new ListNode(0, head);
                var left = dummyNode;
                var right = head;

                while(n > 0)
                {
                    right = right.next;
                    n--;
                }

                while (right != null)
                {
                    left = left.next;
                    right = right.next;
                }

                left.next = left.next.next;
                return dummyNode.next;
            }

            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                ListNode head = new ListNode(0);
                var list = head;
                int sum = 0, carry = 0;

                while(l1 != null || l2 != null)
                {
                    sum = (l1 != null ? l1.val : 0) + (l2 != null ? l2.val : 0) + carry;
                    list.next = new ListNode(sum % 10);
                    carry = sum / 10;
                    list = list.next;
                    l1 = l1 != null ? l1.next : null;
                    l2 = l2 != null ? l2.next : null;
                }

                if(carry != 0)
                {
                    list.next = new ListNode(carry);
                }

                return head.next;
            }

            public bool HasCycle(ListNode head)
            {
                var slow = head;
                var fast = head;

                while(fast != null && fast.next != null)
                {
                    slow = slow.next;
                    fast = fast.next.next;

                    if(fast == slow)
                    {
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Find the intersection
            /// 
            /// </summary>
            /// <param name="nums"></param>
            /// <returns></returns>
            public int FindDuplicate(int[] nums)
            {
                int slow = 0, fast = 0;

                while (true)
                {
                    slow = nums[slow];
                    fast = nums[nums[fast]];
                    if (slow == fast)
                        break;
                }

                int slow2 = 0;
                while (true)
                {
                    slow = nums[slow];
                    slow2 = nums[slow2];
                    if (slow == slow2) return slow;
                }

            }

            public int FindDuplicateNegative(int[] nums)
            {
                int duplicate = 1;
                for(int i = 0; i < nums.Length; i++)
                {
                    int current = Math.Abs(nums[i]);
                    if(nums[current] < 0)
                    {
                        duplicate = current;
                        break;
                    }
                    nums[current] *= -1;
                }

                for(int i=0; i < nums.Length; i++)
                {
                    nums[i] = Math.Abs(nums[i]);
                }
                return duplicate;

            }

            public int FindDuplicateBinary(int[] nums)
            {
                // 'low' and 'high' represent the range of values of the target        
                int low = 1;
                int high = nums.Length - 1;
                int duplicate = -1;

                while (low <= high)
                {
                    int cur = (low + high) / 2;

                    // Count how many numbers in 'nums' are less than or equal to 'cur'
                    int count = 0;
                    foreach (int num in nums)
                    {
                        if (num <= cur)
                            count++;
                    }

                    if (count > cur)
                    {
                        duplicate = cur;
                        high = cur - 1;
                    }
                    else
                    {
                        low = cur + 1;
                    }
                }

                return duplicate;
            }

            //ListNode head = new ListNode(0)
            public ListNode MergeKListsBruteForce(ListNode[] lists)
            {
                var nodes = new List<int>();
                ListNode head = new ListNode(0);
                ListNode point = head;

                foreach(var list in lists)
                {
                    var current = list;
                    while(list != null)
                    {
                        nodes.Add(list.val);
                        current = current.next;
                    }
                }

                nodes.Sort(); //NLogN
                foreach(int x in nodes)
                {
                    point.next = new ListNode(x);
                    point = point.next;
                }

                return head.next;
            }

            

        }
    }
    }


// Program Description: Doubly linked list to store animals.

using System.Xml.Linq;

namespace GenericLinkedList
{
    public class Node<T> where T : IComparable<T>
    {
        public Node<T> next = null;
        public Node<T> previous = null;
        public T data;

        public Node() { }
        public Node(T data)
        {
            this.data = data;
        }
    }
    public class DoublyLinkedList<T> where T : IComparable<T>
    {
        private Node<T> front;
        private Node<T> back;
        int count;

        public DoublyLinkedList()
        {
            front = null;
            back = null;
            count = 0;
        }

        public void AddFirst(T toAdd)
        {
            Node<T> newNode = new Node<T>(toAdd);

            if (front == null)
            {
                front = newNode;
                back = newNode;
                count++;
            }
            else
            {
                newNode.next = front;
                front.previous = newNode;
                front = newNode;
                count++;
            }
        }
        public void AddLast(T toAdd)
        {
            Node<T> lastNode = new Node<T>(toAdd);
            if (front == null)
            {
                front = lastNode;
                back = lastNode;
                count++;
            }
            else
            {
                back.next = lastNode;
                lastNode.previous = back;
                back = lastNode;
                count++;
            }
        }

        public int GetCount()
        {
            return count;
        }

        public void InsertAtRandomLocation(T toAdd)
        {
            Random r;
            int index;

            if (front == null)
                AddFirst(toAdd);

            else if (front.next == null)
            {
                r = new Random();
                index = r.Next(0, 2);

                if (index == 0)
                    AddFirst(toAdd);
                else
                    AddLast(toAdd);
            }

            else
            {
                Node<T> newNode = new Node<T>(toAdd);
                r = new Random();
                index = r.Next(0, count);
                int pointer = 0;

                Node<T> forward = front;
                while (pointer < index)
                {
                    forward = forward.next;
                    pointer++;
                }
                newNode.previous = forward.previous;
                forward.previous.next = newNode;
                newNode.next = forward;
                forward.previous = newNode;
            }
            
        }

        public DoublyLinkedList<T> Merge(DoublyLinkedList<T> list)
        {
            DoublyLinkedList<T> newList = new DoublyLinkedList<T>();
            DoublyLinkedList<T> list1 = HardCopy();
            DoublyLinkedList<T> list2 = list.HardCopy();

            if (list1.front == null && list2.front == null)
            {
                return newList;
            }
            else if (list1.front == null && list2.front != null)
            {
                newList.front = list2.front;
                newList.back = list2.back;
                newList.count = list2.count;
                return newList;
            }
            else if (list1.front != null && list2.front == null)
            {
                newList.front = list1.front;
                newList.back = list1.back;
                newList.count = list1.count;
                return newList;
            }
            else
            {

                newList.front = list1.front;
                newList.back = list1.back;

                newList.back.next = list2.front;
                newList.back.next.previous = newList.back;
                newList.back = list2.back;

                newList.count = list2.count + list1.count;

                return newList;
            }
        }

        public Node<T> Find(T toFind)
        {
            if (front == null)
                return null;
            if (front.next == null)
            {
                if (front.data.Equals(toFind))
                    return front;
                return null;
            }
            Node<T> forward = front;
            Node<T> behind = back;
            while (forward != behind) 
            {
                if (forward.data.Equals(toFind))
                    return forward;
                if (behind.data.Equals(toFind))
                    return behind;

                forward = forward.next;
                behind = behind.previous;
            }
            if(forward != null && forward.data.Equals(toFind))
                return forward;
            return null;
        }

        public T Find(int index)
        {
            int i = 0;

            if (front == null)
                return default(T);

            Node<T> forward = front;
            Node<T> behind = back;
            while (index != i && index != count && forward != null)
            {
                forward = forward.next;
                i++;
            }
            if (index != i)
                return default(T);
            return forward.data;
        }

        public void AddBefore(T before, T toAdd)
        {
            Node<T> newNode = new Node<T>(toAdd);
            Node<T> node = Find(before);

            if (node == null)
            {
                Console.WriteLine("The node you are trying to add before does not exist");
                return;
            }

            if (node == front)
                AddFirst(toAdd);
            else
            {
                node.previous.next = newNode;
                newNode.previous = node.previous;
                newNode.next = node;
                node.previous = newNode;
                count++;
            }
        }
        public void AddAfter(T after, T toAdd)
        {
            Node<T> newNode = new Node<T>(toAdd);
            Node<T> node = Find(after);

            if (node == null)
            {
                Console.WriteLine("The node you are trying to add after does not exist");
                return;
            }

            if (node == back)
                AddLast(toAdd);
            else
            {
                node.next.previous = newNode;
                newNode.next = node.next;
                newNode.previous = node;
                node.next = newNode;
                count++;
            }
        }

        public void Swap(T ele1, T ele2)
        {
            if(count == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }
            if (ele1 == null || ele2 == null)
            {
                Console.WriteLine("The element(s) does not exist");
                return;
            }
            Node<T> node1 = Find(ele1);
            Node<T> node2 = Find(ele2);
            if (node1 == null || node2 == null)
            {
                Console.WriteLine("The element(s) do not exist in the list");
                return;
            }

            T d = node1.data;
            node1.data = node2.data;
            node2.data = d;
        }

        public T DeleteFirst()
        {
            if (front == null)
                return default(T);
            else if (front.next == null)
            {
                Node<T> temp = front;
                front = null;
                back = null;
                count--;
                return temp.data;
            }
            else
            {
                Node<T> temp = front;
                front = temp.next;
                front.previous.next = null;
                front.previous = null;
                count--;
                return temp.data;
            }
        }

        public T DeleteLast()
        {
            if (front == null)
                return default(T);
            else if (front.next == null)
            {
                Node<T> temp = front;
                front = null;
                back = null;
                count--;
                return temp.data;
            }
            else
            {
                Node<T> temp = back;
                back = temp.previous;
                back.next.previous = null;
                back.next = null;
                count--;
                return temp.data;
            }
        }

        public void Sort()
        {
            if (front == null || front.next == null)
                return;

            Node<T> i = front.next;
            Node<T> j = front;

            bool flag = false;

            while (i != null)
            {
                j = i.previous;
                flag = false;
                while (j != null && !flag)
                {
                    if (j.data.CompareTo(j.next.data) > 0)
                    {
                        Swap(j.data, j.next.data);
                        j = j.previous;
                    }
                    else
                        flag = true;
                }
                i = i.next;
            }

        }
        public void RotateLeft()
        {
            if (count == 0)
                Console.WriteLine("There are no elements to rotate");
            else if (count == 2)
                Swap(front.data, back.data);
            else
            {
                T data = front.data;
                DeleteFirst();
                AddLast(data);
            }
        }
        public void RotateRight()
        {
            if (count == 0)
                Console.WriteLine("There are no elements to rotate");
            else if (count == 2)
                Swap(front.data, back.data);
            else
            {
                T data = back.data;
                DeleteLast();
                AddFirst(data);
            }
        }

        public string PrintAllForward()
        {
            Node<T> forward = front;
            if (front == null)
                return "";

            string temp = " " + front.data.ToString();
            while (forward.next != null)
            {
                forward = forward.next;
                temp += " " + forward.data.ToString();
            }
            return temp;
        }

        public string PrintAllReverse()
        {
            Node<T> behind = back;
            if (front == null)
                return "";

            string temp = " " + behind.data.ToString();
            while (behind.previous != null)
            {
                behind = behind.previous;
                temp += " " + behind.data.ToString();
            }
            return temp;
        }
        public DoublyLinkedList<T> HardCopy()
        {
            DoublyLinkedList<T> list = new DoublyLinkedList<T>();

            Node<T> j = front;
            while (j != null)
            {
                list.AddLast(j.data);
                j = j.next;
            }
            return list;
        }
    }
}
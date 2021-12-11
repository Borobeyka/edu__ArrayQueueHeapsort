using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayQueueHeapsort
{
  class Queue
  {
    public List<int> queue;
    public ulong operations;

    public Queue() // TOTAL: 1
    {
      operations = 1;
      queue = new List<int>(); // 1
    }

    ~Queue()
    {
      queue.Clear();
      queue.TrimExcess(); // Удаление ссылок на элементы (Capacity)
      operations = 0;
    }

    public int count() // TOTAL: 1
    {
      operations += 1;
      return queue.Count; // 1
    }

    public void push(int newValue) // TOTAL: 1
    {
      operations += 1;
      queue.Add(newValue); // 1
    }

    public int pop() // TOTAL: 3
    {
      operations += 3;
      int result = queue[0]; // 1
      queue.RemoveAt(0); // 1
      return result; // 1
    }

    public int show() // TOTAL: 1
    {
      operations += 1;
      return queue[0]; //1
    }

    public int get(int pos) // TOTAL: 2 + E0,n(4) + 2 + 2 + En,m(4) + 1 = 4n + 7
    {
      operations += 5;
      for (int i = 0; i < pos; ++i) // 2 | 2 + E0,n(4)
        push(pop()); // + 1 + 3

      int result = show(); // 1 + 1

      for (int i = pos; i < count(); ++i) // 2 | 2 + En,m(4)
        push(pop()); // + 1 + 3
      return result; // + 1
    }

    public void set(int pos, int newValue) // TOTAL: 2 + E0,n(4) + 1 + 2 + E0,n(4) = 4n + 5
    {
      operations += 5;
      for (int i = 0; i < pos; ++i) // 2 | 2 + E0,n(4)
        push(pop()); // + 1 + 3
      queue[0] = newValue; // 1
      for (int i = pos; i < count(); ++i) // 2 | 2 + E0,n(4)
        push(pop()); // + 1 + 3
    }

    public void print()
    {
      foreach (int elem in queue)
        Console.Write("{0} ", elem);
      Console.WriteLine();
      Console.WriteLine("────────────────────────────────────────");
    }

    public void heapify(Queue tempQueue, int s, int i) // TOTAL: 7 + 8 + (4n + 7) * 2 + 1 + 1 + 4n + 7 + 4n + 5 + 4n + 7 + 2 + 4n + 5 + 2
      // 45 + 8n + 14 + 4n + 4n + 4n + 4n = 59 + 24n
    {
      int largest = i; // + 1
      int left = 2 * i + 1; // + 3
      int right = 2 * i + 2; // 3
      // + 7
      operations += 7;

      if (left < s && tempQueue.get(left) > tempQueue.get(largest)) // + 1 + 2 + 4n + 7
        largest = left; // + 1

      if (right < s && tempQueue.get(right) > tempQueue.get(largest)) // + 1 + 2 + 4n + 7
        largest = right; // + 1

      operations += 8;

      if (largest != i) // + 1
      {
        int temp = tempQueue.get(i); // + 1 + 4n + 7
        tempQueue.set(i, tempQueue.get(largest)); // + 4n + 5 + 4n + 7 + 2
        tempQueue.set(largest, temp); // + 4n + 5 + 2
        heapify(tempQueue, s, largest);
      }
    }

    public void sort() // TOTAL: 1 + 4 + 4 + En,0(59 + 24n) + 3 + En,0(40n + 94) = 12 + En,0(59 + 24n) + En,0(40n + 94)=
      // 12 + 59n + 24n^2 + 40n^2 + 94n = 134n^2 + 153n + 12
    {
      int s = count(); // + 1
      for (int i = s / 2 - 1; i >= 0; i--) // + 4 + En,0(59 + 24n)
        heapify(this, s, i); // 62 + 24n
      for (int i = s - 1; i >= 0; i--) // + 3 + En,0(40n + 94)
      {
        int temp = get(0); // 4n + 7 + 1 + 1
        set(0, get(i)); // + 4n + 5 + 2 + 4n + 7 + 1
        set(i, temp); // + 4n + 5 + 2 + 1
        heapify(this, i, 0); // 62 + 24n
      }
    }
  }
}

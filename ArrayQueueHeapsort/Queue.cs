using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayQueueHeapsort
{
  class Queue
  {
    public List<int> queue;
    public int operations;

    public Queue()
    {
      operations += 1;
      queue = new List<int>(); // 1
    }

    ~Queue()
    {
      queue.Clear();
      operations = 0;
    }

    public int count()
    {
      operations += 1;
      return queue.Count; // 1
    }

    public void push(int newValue)
    {
      operations += 1;
      queue.Add(newValue); // 1
    }

    public int pop()
    {
      operations += 3;
      int result = queue[0]; // 1
      queue.RemoveAt(0); // 1
      return result; // 1
    }

    public int show()
    {
      operations += 1;
      return queue[0]; //1
    }

    public int get(int pos)
    {
      operations += 5;
      for (int i = 0; i < pos; ++i) // 2
        push(pop());
      int result = show();
      for (int i = pos; i < count(); ++i) // 2
        push(pop());
      return result;
    }

    public void set(int pos, int newValue)
    {
      operations += 5;
      for (int i = 0; i < pos; ++i) // 2
        push(pop());
      queue[0] = newValue; // 1
      for (int i = pos; i < count(); ++i) // 2
        push(pop());
    }

    public void print()
    {
      foreach (int elem in queue)
        Console.WriteLine(elem);
      Console.WriteLine("──────────");
    }

    //Преобразование поддерева с корнем i в двоичную кучу.
    public void heapify(Queue tempQueue, int s, int i)
    {
      int largest = i; //Делаем наибольший элемент корнем дерева.
      int left = 2 * i + 1; //Левый "сын"
      int right = 2 * i + 2; //Правый "сын"

      //Если левый сын больше корня.
      if (left < s && tempQueue.get(left) > tempQueue.get(largest))
        largest = left;

      //Если правый сын больше корня.
      if (right < s && tempQueue.get(right) > tempQueue.get(largest))
        largest = right;

      //Если наибольший элемент - не корень.
      if (largest != i)
      {
        int temp = tempQueue.get(i);
        tempQueue.set(i, tempQueue.get(largest));
        tempQueue.set(largest, temp);
        heapify(tempQueue, s, largest); //Преобразуем поддерево в двоичную кучу, используя рекурсию.
      }
    }

    public void sort()
    {
      int s = count();
      //Построение кучи путём перегруппировки дека.
      for (int i = s / 2 - 1; i >= 0; i--)
        heapify(this, s, i);
      //Поочерёдно извлекаем элементы дека.
      for (int i = s - 1; i >= 0; i--)
      {
        //Перемещаем корень в конец дека.
        int temp = get(0);
        set(0, get(i));
        set(i, temp);
        //Используем heapify на сокращённой куче.
        heapify(this, i, 0);
      }
    }
  }

  /*class Queue
  {
    int[] queue;

    public int getLength() { // TOTAL: 2
      if (queue != null) // 1
        return queue.Length; // 1
      else return 0;
    }

    public void set(int index, int elem) { // TOTAL: 61 + 114n + 24n^2
      for(int i = 0; i < index; i++) // 3 + E0,n(51 + 12n) = 3 + 51n + 12n^2
        push(pop()); // 51 + 12n
      pop(); // 26 + 6n
      push(elem); // 25 + 6n
      for (int i = 0; i < getLength() - index - 1; i++) // 3 + 2 + 2 + E0,n(51 + 12n) = 7 + 51n + 12n^2
        push(pop()); // 51 + 12n
    }

    public void push(int elem) { // TOTAL: 25 + 6n
      if (queue == null) { // 1
        queue = new int[1] { elem }; // 3
        return;
      }
      int length = getLength(); // 1 + 2
      int[] tmp = new int[length + 1]; // 1 + 3
      for (int i = 0; i < length; i++) // 3 + E0,n(3) = 3 + 3n
        tmp[i] = queue[i]; // 3
      tmp[length++] = elem; // 2 + 1
      rewrite(tmp); // 8 + 3n
    }

    public int pop() // TOTAL: 26 + 6n
    {
      int n = queue[0]; // 2
      int[] tmp = new int[getLength() - 1]; // 2 + 1 + 3
      for (int i = 1; i < getLength(); i++) //3 + 2 + E1,n(3) = 5 + 3n
        tmp[i - 1] = queue[i]; // 3
      rewrite(tmp); // 8 + 3n
      return n;
    }

    public int get(int index) // TOTAL: 6 + 110n + 75n^2 + 12n^3
    {
      int r = 99999; // 1
      for(int i = 0; i < getLength(); i++) // 3 + 2 + E0,n(110 + 75n + 12n^2)
      {
        if (i == index) // 1
        {
          r = pop(); // 1 + 26 + 6n
          push(r); // 25 + 6n
          for (int j = 0; j < getLength() - 1; j++) // 3 + 2 + 1 + E0,n(51 + 12n) = 6 + 51n + 12n^2
            push(pop()); // 51 + 12n
        }
        push(pop()); // 51 + 12n
      }
      return r;
    }

    public void rewrite(int[] arr) { // TOTAL:  8 + 3n
      queue = new int[arr.Length]; // 3
      for (int i = 0; i < getLength(); i++) // 3 + 2 + E0,n(3) = 5 + 3n
        queue[i] = arr[i]; // 3
    }

    public void print() {
      foreach (int elem in queue)
        Console.WriteLine(elem);
      Console.WriteLine("──────────");
    }

    public int add2pyramid(int i, int N)
    {
      int imax, buf; // 2
      if ((2 * i + 2) < N) // 1 + 2
      {
        if (get(2 * i + 1) < get(2 * i + 2)) // 1 + (2 + 6 + 110n + 75n^2 + 12n^3) * 2 = 17 + 220n + 150n^2 + 24n^3
          imax = 2 * i + 2; // 3
        else
          imax = 2 * i + 1;
      }
      else imax = 2 * i + 1;
      if (imax >= N) return i; // 1
      if (get(i) < get(imax)) // (6 + 110n + 75n^2 + 12n^3) * 2 = 12 + 220n + 150n^2 + 24n^3
      {
        buf = get(i);
        set(i, get(imax));
        set(imax, buf);
        if (imax < N / 2)
          i = imax;
      }
      return i;
    }

    public void sort()
    {
      int len = getLength();
      for (int i = len / 2 - 1; i >= 0; --i)
      {
        long prev_i = i;
        i = add2pyramid(i, len);
        if (prev_i != i) ++i;
      }
      int buf;
      for (int k = len - 1; k > 0; --k)
      {
        buf = get(0);
        set(0, get(k));
        set(k, buf);
        int i = 0, prev_i = -1;
        while (i != prev_i)
        {
          prev_i = i;
          i = add2pyramid(i, k);
        }
      }
    }*/
}

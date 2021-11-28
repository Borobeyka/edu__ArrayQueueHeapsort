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
      queue.TrimExcess(); // Удаление ссылок на элементы (Capacity)
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
}

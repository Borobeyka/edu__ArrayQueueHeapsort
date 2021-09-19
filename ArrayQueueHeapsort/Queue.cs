using System;
using System.Collections.Generic;
using System.Text;

namespace ArrayQueueHeapsort
{
  class Queue
  {
    int[] queue;

    public int getLength() {
      if (queue != null) return queue.Length;
      else return 0;
    }

    public void set(int elem, int index) {
      index -= 1;
      for(int i = 0; i < index; i++) push(pop());
      pop();
      push(elem);
      for (int i = 0; i < index - 1; i++) push(pop());
    }

    public void push(int elem) {
      if (queue == null) {
        queue = new int[1] { elem };
        return;
      }
      int length = getLength();
      int[] tmp = new int[length + 1];
      for (int i = 0; i < length; i++)
        tmp[i] = queue[i];
      tmp[length++] = elem;
      rewrite(tmp);
    }

    public int pop()
    {
      int n = queue[0];
      int[] tmp = new int[getLength() - 1];
      for (int i = 1; i < getLength(); i++)
        tmp[i - 1] = queue[i];
      rewrite(tmp);
      return n;
    }

    public int get(int index)
    {
      for(int i = 0; i < index; i++)
      {
        if (index - i == 1) return pop();
        push(pop());
      }
      return 99999;
    }

    public void rewrite(int[] arr) {
      queue = new int[arr.Length];
      for (int i = 0; i < getLength(); i++)
        queue[i] = arr[i];
    }

    public void print() {
      foreach (int elem in queue)
        Console.WriteLine(elem);
      Console.WriteLine("-----------");
    }

  }
}

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

    public void set(int index, int elem) {
      //index = index > getLength() - 1 ? index % getLength() : index;
      for(int i = 0; i < index; i++) push(pop());
      pop();
      push(elem);
      for (int i = 0; i < getLength() - index - 1; i++) push(pop());
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
      int r = 99999;
      for(int i = 0; i < getLength(); i++)
      {
        if (i == index)
        {
          r = pop();
          push(r);
          for (int j = 0; j < getLength() - 1; j++) push(pop());
        }
        push(pop());
      }
      return r;
    }

    public void rewrite(int[] arr) {
      queue = new int[arr.Length];
      for (int i = 0; i < getLength(); i++)
        queue[i] = arr[i];
    }

    public void print() {
      foreach (int elem in queue)
        Console.WriteLine(elem);
      Console.WriteLine("──────────");
    }

    public int add2pyramid(int i, int N)
    {
      int imax;
      int buf;
      if ((2 * i + 2) < N)
      {
        if (get(2 * i + 1) < get(2 * i + 2)) imax = 2 * i + 2;
        else imax = 2 * i + 1;
      }
      else imax = 2 * i + 1;
      if (imax >= N) return i;
      if (get(i) < get(imax))
      {
        buf = get(i);
        set(i, get(imax));
        set(imax, buf);
        if (imax < N / 2) i = imax;
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
    }

  }
}

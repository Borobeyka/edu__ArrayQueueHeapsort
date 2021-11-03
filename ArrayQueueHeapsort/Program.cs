using System;

namespace ArrayQueueHeapsort
{
  class Program
  {
    static void Main(string[] args)
    {
      Queue queue = new Queue();
      queue.push(10);
      queue.push(-98);
      queue.push(6);
      queue.push(7);
      queue.push(-3);
      queue.push(2);
      queue.push(1);
      queue.push(5);
      queue.push(25);
      queue.push(9);

      queue.print();

      queue.sort();
      queue.print();

    }
  }
}

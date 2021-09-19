using System;

namespace ArrayQueueHeapsort
{
  class Program
  {
    static void Main(string[] args)
    {
      Queue queue = new Queue();
      queue.push(1);
      queue.push(3);
      queue.push(5);
      queue.push(6);
      queue.print();
      queue.set(2, 3);
      queue.print();
      //Console.WriteLine(queue.get(7));
      //queue.sort();

    }
  }
}

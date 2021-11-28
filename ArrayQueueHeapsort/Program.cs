using System;
using System.Diagnostics;

namespace ArrayQueueHeapsort
{
  class Program
  {
    public static long numberOfOperations = 0;

    static void Main(string[] args)
    {
      Random rnd = new Random();
      for (int i = 300, iteration = 1; i <= 3000; i += 300, iteration++)
      {
        Stopwatch time = new Stopwatch();
        Console.WriteLine("[*] Итерация {0}, кол-во элементов - {1}", iteration, i);
        
        Queue queue = new Queue();
        for (int j = 0; j < i; j++)
          queue.push(rnd.Next(-70, 70 + 1));

        time.Start();
        queue.sort();
        time.Stop();

        long elapsedMilliseconds = time.ElapsedMilliseconds;
        Console.WriteLine("\tЗатрачено времени - {0}мс ({1} сек.)",
          elapsedMilliseconds, Math.Round((double)elapsedMilliseconds / 1000));
        Console.WriteLine("\tКоличество операций - {0}", queue.operations);
        Console.WriteLine();
      }
    }
  }
}

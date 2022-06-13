/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

namespace MultiThreading.Task4.Threads.Join;

internal static class Program
{
    private static readonly Semaphore Semaphore = new (1, 1);

    private static void Main(string[] args)
    {
        ProcessThread(10);
        ProcessThreadPool(10);

        Console.ReadLine();
    }

    private static void ProcessThread(int value)
    {
        if (value <= 0)
        {
            return;
        }

        var thread = new Thread(() => ProcessThread(value - 1));

        Console.WriteLine($"Option A: Thread #{value} created");
        thread.Start();
        thread.Join();
        Console.WriteLine($"Option A: Thread #{value} completed");
    }

    private static void ProcessThreadPool(object? state)
    {
        var value = (int?)state;
        if (value <= 0)
        {
            return;
        }

        ThreadPool.QueueUserWorkItem(ProcessThreadPool, value - 1);

        Console.WriteLine($"Option A: Thread #{value} created");
        Semaphore.WaitOne();
        Console.WriteLine($"Option A: Thread #{value} completed");
        Semaphore.Release();
    }
}

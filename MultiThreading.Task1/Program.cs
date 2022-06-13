/*
 * 1.	Write a program, which creates an array of 100 Tasks, runs them and waits all of them are not finished.
 * Each Task should iterate from 1 to 1000 and print into the console the following string:
 * “Task #0 – {iteration number}”.
 */

namespace MultiThreading.Task1;

internal static class Program
{
    private const int TaskAmount = 100;
    private const int MaxIterationsCount = 1000;
    private const int FromInclusive = 0;

    private static void Main(string[] args)
    {
        HundredTasks();

        Console.ReadLine();
    }

    private static void HundredTasks()
    {
        Parallel.For(
            FromInclusive,
            TaskAmount,
            i =>
            {
                Task.Run(
                    () =>
                    {
                        for (var j = 0; j < MaxIterationsCount; j++)
                        {
                            Output(i, j);
                        }
                    });
            });
    }

    private static void Output(int taskNumber, int iterationNumber)
    {
        Console.WriteLine($"Task #{taskNumber} – {iterationNumber}");
    }
}

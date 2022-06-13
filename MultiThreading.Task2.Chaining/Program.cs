/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */

namespace MultiThreading.Task2.Chaining;

internal static class Program
{
    private static readonly Random Random = new ();

    private static void Main(string[] args)
    {
        var task1 = Task.Run(
            () =>
            {
                var collection = new List<int>();
                for (var i = 0; i < 10; i++)
                {
                    collection.Add(GetRandomNumber(0, 10000));
                }

                Console.WriteLine("Task1:");
                collection.ForEach(Console.WriteLine);

                return collection;
            });

        var task2 = task1.ContinueWith(
            task =>
            {
                var collection = task.Result.Select(item => item * GetRandomNumber(0, 1000)).ToList();

                Console.WriteLine("Task2:");
                collection.ForEach(Console.WriteLine);

                return collection;
            });

        var task3 = task2.ContinueWith(
            task =>
            {
                task.Result.Sort();

                Console.WriteLine("Task3:");
                task.Result.ForEach(Console.WriteLine);

                return task.Result;
            });

        task3.ContinueWith(
            task =>
            {
                var average = task.Result.Average();

                Console.WriteLine("Task4:");
                Console.WriteLine(average);

                return average;
            });

        Task.WaitAll(task1, task2, task3);
    }

    private static int GetRandomNumber(int minValue, int maxValue)
    {
        return Random.Next(minValue, maxValue);
    }
}

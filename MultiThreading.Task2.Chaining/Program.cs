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
    private static void Main(string[] args)
    {
        var random = new Random();

        var task1 = Task.Run(
            () =>
            {
                var collection = new List<int>();
                for (var i = 0; i < 10; i++)
                {
                    collection.Add(random.Next(0, 10000));
                }

                Console.WriteLine("Task1:");
                collection.ForEach(Console.WriteLine);

                return collection;
            });

        var task2 = task1.ContinueWith(
            task =>
            {
                var collection = task.Result.Select(item => item * random.Next(0, 1000)).ToList();

                Console.WriteLine("Task2:");
                collection.ForEach(Console.WriteLine);

                return collection;
            });

        var task3 = task2.ContinueWith(
            task =>
            {
                var sortedCollection = task.Result.OrderBy(i => i).ToList();

                Console.WriteLine("Task3:");
                sortedCollection.ForEach(Console.WriteLine);

                return sortedCollection;
            });

        task3.ContinueWith(
            task =>
            {
                var average = task.Result.Average();

                Console.WriteLine("Task4:");
                Console.WriteLine(average);

                return average;
            });

        Console.ReadLine();
    }
}

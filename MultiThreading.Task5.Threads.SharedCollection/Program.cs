/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */

namespace MultiThreading.Task5.Threads.SharedCollection;

internal static class Program
{
    private static void Main(string[] args)
    {
        var shared = new Shared();
        var task = Task.Factory.StartNew(
            () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    shared.Add(i);
                }

                Task.Factory.StartNew(
                    () =>
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            shared.Print(i);
                        }
                    },
                    TaskCreationOptions.AttachedToParent);
            });

        task.Wait();
    }
}

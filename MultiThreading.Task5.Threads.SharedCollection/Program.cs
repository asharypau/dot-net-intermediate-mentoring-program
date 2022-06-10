/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */

namespace MultiThreading.Task5.Threads.SharedCollection;

internal static class Program
{
    private const int MaxIterationsCount = 1000;

    private static readonly IDictionary<int, string> Persons = new Dictionary<int, string>();
    private static readonly ReaderWriterLockSlim ReaderWriterLockSlim = new ReaderWriterLockSlim();

    private static void Main(string[] args)
    {
        var task = Task.Factory.StartNew(
            () =>
            {
                for (var i = 0; i < MaxIterationsCount; i++)
                {
                    Write(i, "name" + i);
                }

                Task.Factory.StartNew(
                    () =>
                    {
                        for (var i = 0; i < 10; i++)
                        {
                            Read(i);
                        }
                    },
                    TaskCreationOptions.AttachedToParent);
            });

        task.Wait();
    }

    private static void Write(int id, string value)
    {
        ReaderWriterLockSlim.EnterWriteLock();
        Persons.Add(id, value);
        ReaderWriterLockSlim.ExitWriteLock();
    }

    private static void Read(int value)
    {
        ReaderWriterLockSlim.EnterReadLock();
        Console.WriteLine(Persons[value]);
        ReaderWriterLockSlim.ExitReadLock();
    }
}

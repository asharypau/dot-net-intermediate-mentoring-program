namespace MultiThreading.Task5.Threads.SharedCollection;

public class Shared
{
    private readonly Mutex _mutex;
    private readonly IList<int> _collection;

    public Shared()
    {
        _mutex = new Mutex();
        _collection = new List<int>();
    }

    public void Add(int value)
    {
        _mutex.WaitOne();

        _collection.Add(value);

        _mutex.ReleaseMutex();
    }

    public void Print(int value)
    {
        _mutex.WaitOne();

        Console.WriteLine(_collection[value]);

        _mutex.ReleaseMutex();
    }
}

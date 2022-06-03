/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/

namespace MultiThreading.Task6.Continuation;

internal static class Program
{
    private static void Main(string[] args)
    {
        var task = Task.Run(
            () =>
            {
                Console.WriteLine($"The task is running on a thread with id: {Environment.CurrentManagedThreadId}");

                return DateTime.Today.DayOfWeek;
            });

        task.ContinueWith(
            antecedent =>
            {
                Console.WriteLine($"The continuation is running on a thread with id: {Environment.CurrentManagedThreadId}");

                if (antecedent.IsCanceled)
                {
                    Console.WriteLine("The task was canceled");
                }
                else if (antecedent.IsFaulted)
                {
                    Console.Write("The task was failed: ");
                    Console.WriteLine(antecedent.Exception?.Message);
                }
                else
                {
                    Console.WriteLine($"Today is {antecedent.Result}");
                }
            },
            TaskContinuationOptions.ExecuteSynchronously);

        Console.ReadLine();
    }
}

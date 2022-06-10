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
        var tokenSource = new CancellationTokenSource();

        var task = Task.Run(
                () =>
                {
                    Console.WriteLine($"The task is running on a thread with id: {Environment.CurrentManagedThreadId}");

                    return DateTime.Today.DayOfWeek;
                })
            .ContinueWith(
                antecedent =>
                {
                    Console.WriteLine(
                        "a. Continuation task executed regardless of the result of the parent task" +
                        $"The continuation is running on a thread with id: {Environment.CurrentManagedThreadId}");

                    throw null;
                })
            .ContinueWith(
                antecedent =>
                {
                    Console.WriteLine(
                        "b. Continuation task executed when the parent task finished without success" +
                        $"The continuation is running on a thread with id: {Environment.CurrentManagedThreadId}");

                    throw null;
                },
                TaskContinuationOptions.OnlyOnFaulted)
            .ContinueWith(
                antecedent =>
                {
                    Console.WriteLine(
                        "c. Continuation task executed when the parent task would be finished with fail and parent task thread should be reused for continuation" +
                        $"The continuation is running on a thread with id: {Environment.CurrentManagedThreadId}");

                    tokenSource.Cancel();
                    tokenSource.Token.ThrowIfCancellationRequested();
                },
                tokenSource.Token,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Current)
            .ContinueWith(
                antecedent =>
                {
                    Console.WriteLine(
                        "d. Continuation task executed outside of the thread pool when the parent task would be cancelled" +
                        $"The continuation is running on a thread with id: {Environment.CurrentManagedThreadId}");
                },
                TaskContinuationOptions.OnlyOnCanceled);

        task.Wait();
    }
}

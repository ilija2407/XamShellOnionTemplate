using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MvvmHelpers.Commands;

namespace XamShell.ViewModels.GoodExamples
{
    public class GoodTaskInvokeViewModel : ViewModelBase
    {
        //.Result change it to await 
        private AsyncCommand _goodTaskInvokeCommand;
        public AsyncCommand GoodTaskInvokeCommand => _goodTaskInvokeCommand ?? (_goodTaskInvokeCommand = new AsyncCommand(async () =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("This allows the task to run on a background thread without locking the UI.");

            var taskResult = await TaskService.GetStringWithTaskRunAsync("Using await");

            PrintStatus("Command ending");
        }));

        // Example: Multiple task execution - The intent is to await multiple tasks. The right way!
        private AsyncCommand _goodMultipleTaskExecutionCommand;
        public AsyncCommand GoodMultipleTaskExecutionCommand => _goodMultipleTaskExecutionCommand ?? new AsyncCommand(async () =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("Awaiting Task.WhenAll lets all the tasks run asynchronously.");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var taskCount = 5;
            var tasks = new Task<string>[taskCount];
            for (int i = 0; i < taskCount; i++)
            {
                tasks[i] = TaskService.GetStringWithTaskRunAsync($"Task {i}");
            }

            await Task.WhenAll(tasks);

            stopwatch.Stop();

            PrintStatus($"Command ending after: {stopwatch.Elapsed.TotalSeconds}s");
        });
        
        //using configure await
        private AsyncCommand _goodConsecutiveOperations;
        public AsyncCommand GoodConsecutiveOperations => _goodConsecutiveOperations ?? new AsyncCommand(async () =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("Not using ConfigureAwait(false) allows execution to continue on a background thread.");

            PrintThreadCheck();
            await TaskService.GetStringWithTaskRunAsync().ConfigureAwait(false);
            PrintThreadCheck();

            //Simulate CPU intensive operation. Eg: Deserializing a lot of JSON
            Thread.Sleep((int)TimeSpan.FromSeconds(5).TotalMilliseconds);

            PrintStatus("Command ending");
        });
    }
}
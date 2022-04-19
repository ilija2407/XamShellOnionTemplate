using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using XamShell.Services;
using Command = Xamarin.Forms.Command;

namespace XamShell.ViewModels.BadExapmles
{
    public class BadTaskInvokeViewModel : ViewModelBase
    {
        //Don't use .Result , you are making the main thread non-responsive 
        private Command _badTaskInvokeCommand;
        public Command BadTaskInvokeCommand => _badTaskInvokeCommand ?? (_badTaskInvokeCommand = new Command(() =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("This locks up the UI. Never use .Result");

            var taskResult = TaskService.GetStringWithTaskRunAsync("Using Task.Result").Result;
            PrintStatus("Task Completed with result: {taskResult}");

            PrintStatus("Command ending");
        }));
        
       // Example: Multiple task execution - The intent is to await multiple tasks. 
        private AsyncCommand _badMultipleTaskExecutionCommand;
        public AsyncCommand BadMultipleTaskExecutionCommand => _badMultipleTaskExecutionCommand ?? new AsyncCommand(async () =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("Awaiting tasks in a for loop causes the tasks to be awaited synchronously (one after another).");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var taskCount = 5;
            for (int i = 0; i < taskCount; i++)
            {
                await TaskService.GetStringWithTaskRunAsync($"Task {i}");
            }

            stopwatch.Stop();

            PrintStatus($"Command ending after: {stopwatch.Elapsed.TotalSeconds}s");
        });
        
        //Not using configure await
        private AsyncCommand _badConsecutiveOperations;
        public AsyncCommand BadConsecutiveOperations => _badConsecutiveOperations ?? new AsyncCommand(async () =>
        {
            ClearStatus();
            PrintStatus("Command starting");
            PrintStatus("Not using ConfigureAwait(false) causes execution to continue on the UI thread.");

            PrintThreadCheck();
            await TaskService.GetStringWithTaskRunAsync();
            PrintThreadCheck();

            //Simulate CPU intensive operation
            Thread.Sleep((int)TimeSpan.FromSeconds(5).TotalMilliseconds);
          
            PrintStatus("Command ending");
        });

    }
}
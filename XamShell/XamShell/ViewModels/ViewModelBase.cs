using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamShell.Services;

namespace XamShell.ViewModels
{
    public class ViewModelBase : BaseViewModel
    {
       private TaskService _taskService;
        public TaskService TaskService => _taskService ?? (_taskService = InitializeTaskService());

        private string _status = "Status: \n";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        private TaskService InitializeTaskService()
        {
            var taskService = new TaskService();
            taskService.TaskCreated += HandleTaskStatusUpdated;
            taskService.TaskStarting += HandleTaskStatusUpdated;
            taskService.TaskCompleted += HandleTaskStatusUpdated;
            taskService.TaskFaulted += HandleTaskStatusUpdated;
            return taskService;
        }

        private void HandleTaskStatusUpdated(object sender, TaskStatusChangedEventArgs status)
        {
            Console.WriteLine(status.Status);
            PrintStatus($"{status.Status}");
        }

        public void ClearStatus() => Status = string.Empty;

        public void PrintStatus(string input) => Device.BeginInvokeOnMainThread(() => Status += $"{input}\n");

        public void PrintDot() => Status += ".";

        public void PrintThreadCheck(bool addSpacing = true) => Status += $"{(addSpacing ? "\n" : "")}" +
            $"==={(MainThread.IsMainThread ? "Is" : "Not")} on MainThread===\n" +
            $"{(addSpacing ? "\n" : "")}";
        
    }
}
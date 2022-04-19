using System.Threading.Tasks;

namespace XamShell.Services
{
    public class TaskStatusChangedEventArgs
    {
        public Task Task { get; set; }
        public string Status { get; set; }

        public TaskStatusChangedEventArgs(Task task, string status)
        {
            Task = task;
            Status = status;
        }
        
    }
}
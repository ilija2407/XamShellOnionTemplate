using Xamarin.Essentials;

namespace XamShell.Helpers
{
    public static class StatusExtensions
    {
        public static string AppendMainThreadAlert(this string status)
        {
            if (MainThread.IsMainThread)
            {
                status += " *on MainThread!*\n";
            }
            return status;
        }
    }
}
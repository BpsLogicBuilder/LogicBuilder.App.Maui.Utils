using LogicBuilder.App.Maui.Utils.Interfaces;

namespace LogicBuilder.App.Maui.Utils.Tests
{
    public class AppLogger : IAppLogger
    {
        public void LogMessage(string group, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{group}: {message}");
        }
    }
}

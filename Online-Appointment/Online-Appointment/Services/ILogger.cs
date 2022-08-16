namespace Online_Appointment.Services
{
    public interface ILogger
    {
        void Info(string message, string arg = null);
        void Error(string message, string arg = null);
        void Warning(string message, string arg = null);
    }

}
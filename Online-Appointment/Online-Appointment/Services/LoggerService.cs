using NLog;
using System;
using System.IO;
namespace Online_Appointment.Services
{
    public class LoggerService : ILogger
    {
        //private static object _locker = new object();
        //private Logger logger;
        //public LoggerService()
        //{
        //    logger = LogManager.GetLogger("OnlineAppointment");
        //    Task.Run(DeleteFiles);
        //}

        private static LoggerService instance;
        private static Logger logger;

        private static object _locker = new object();
        public LoggerService()
        {

        }

        public static LoggerService GetInstance()
        {

            if (instance == null)
            {
                instance = new LoggerService();
            }
            return instance;
        }

        private static Logger GetLogger(string theLogger)
        {
            if (LoggerService.logger == null)
            {
                LoggerService.logger = LogManager.GetLogger(theLogger);
            }
            return LoggerService.logger;

        }


        //public void Error(string email, string ip, string ev, string source)
        //{
        //    lock (_locker)
        //        logger.Error($"{ev} for: {email}; IP: {ip}; Source: {source}");
        //}

        //public void Info(string email, string ip, string ev, string source)
        //{
        //    lock (_locker)
        //        logger.Info($"{ev} for: {email}; IP: {ip}; Source: {source}");
        //}

        public void Info(string message, string arg = null)
        {
            lock (_locker)
            {
                if (arg == null)
                {
                    GetLogger("OnlineAppointment").Info(message);
                }
                else
                {
                    GetLogger("OnlineAppointment").Info(message, arg);
                }
            }
        }

        public void Warning(string message, string arg = null)
        {
            lock (_locker)
            {
                if (arg == null)
                {
                    GetLogger("OnlineAppointment").Warn(message);
                }
                else
                {
                    GetLogger("OnlineAppointment").Warn(message, arg);
                }
            }
        }

        public void Error(string message, string arg = null)
        {
            lock (_locker)
            {
                if (arg == null)
                {
                    GetLogger("OnlineAppointment").Error(message);
                }
                else
                {
                    GetLogger("OnlineAppointment").Error(message, arg);
                }
            }
        }

        //public void Warning(string email, string ip, string ev, string source)
        //{
        //    lock (_locker)
        //        logger.Warn($"{ev} for: {email}; IP: {ip}; Source: {source}");
        //}

        public void DeleteFiles()
        {
            while (true)
            {
                try
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory + "/logs";
                    if (Directory.Exists(path))
                    {
                        var files = Directory.GetFiles(path);
                        foreach (var file in files)
                        {
                            try
                            {
                                var fileDateFixed = file.Substring(file.Length - 10);
                                var dtFile = DateTime.Parse(fileDateFixed);
                                if ((DateTime.Now - dtFile).TotalDays >= 7)
                                {
                                    File.Delete(file);
                                }
                            }
                            catch { }
                        }
                    }
                }
                catch { }
                System.Threading.Thread.Sleep(60 * 60 * 1000);
            }
        }

        //public void Info(string email)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Info(string email)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
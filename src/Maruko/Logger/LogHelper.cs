using System;
using System.IO;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace Maruko.Logger
{
    /// <summary>
    /// Log4net日志帮助
    /// </summary>
    public class LogHelper
    {

        public string LogReposName = @"MarukoNetCoreRepository";

        public string Log4ConfigName = @"log4net.config";

        public static LogHelper Log4NetInstance = new LogHelper();
        
        private ILoggerRepository LoggerRepository { get; set; }
        private LogHelper() { }

        public void Instance(string @log4ConfigName,Type type)
        {
            ILoggerRepository repository = LogManager.CreateRepository(LogReposName);
            XmlConfigurator.Configure(repository, new FileInfo(@log4ConfigName));

            LoggerRepository = repository;
        }

        public ILog LogFactory(Type type)
        {
           
            if (LoggerRepository == null)
            {
                Instance(Log4ConfigName, type);

                return LogManager.GetLogger(LoggerRepository.Name, type);
            }

            return LogManager.GetLogger(LoggerRepository.Name, type);
        }
    }
}

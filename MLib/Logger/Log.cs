using System;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Appender;
using log4net.Core;
using System.IO;

namespace MLib.Logger
{
    public class Log
    {
        private static ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// log4net 클래스 생성
        /// </summary>
        public Log()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Configured = true;

            RollingFileAppender rfa = new RollingFileAppender();
            rfa.File = Path.Combine("log", "logger.log");
            rfa.AppendToFile = true;
            rfa.RollingStyle = RollingFileAppender.RollingMode.Date;
            rfa.MaxSizeRollBackups = 10;
            rfa.DatePattern = "_yyyyMMdd\".log\"";
            rfa.Layout = new PatternLayout("%date %-5level %message%newline");

            hierarchy.Root.AddAppender(rfa);
            rfa.ActivateOptions();

            hierarchy.Root.Level = Level.All;
        }

        /// <summary>
        /// log4net Error
        /// </summary>
        /// <param name="msg">문자열</param>
        public void Error(string msg)
        {
            _log.Error(msg);
        }

        /// <summary>
        /// log4net ErrorFormat
        /// </summary>
        /// <param name="format">문자열</param>
        /// <param name="arg">문자열 배열</param>
        public void ErrorFormat(string format, params object[] arg)
        {
            _log.ErrorFormat(format, arg);
        }

        /// <summary>
        /// log4net Info
        /// </summary>
        /// <param name="msg">문자열</param>
        public void Info(string msg)
        {
            _log.Info(msg);
        }

        /// <summary>
        /// log4net InfoFormat
        /// </summary>
        /// <param name="format">문자열</param>
        /// <param name="arg">문자열 배열</param>
        public void InfoFormat(string format, params object[] arg)
        {
            _log.InfoFormat(format, arg);
        }

        /// <summary>
        /// log4net Debug
        /// </summary>
        /// <param name="msg">문자열</param>
        public void Debug(string msg)
        {
            _log.Debug(msg);
        }

        /// <summary>
        /// log4net DebugFormat
        /// </summary>
        /// <param name="format">문자열</param>
        /// <param name="arg">문자열 배열param>
        public void DebugFormat(string format, params object[] arg)
        {
            _log.DebugFormat(format, arg);
        }

        /// <summary>
        /// log4net Fatal
        /// </summary>
        /// <param name="msg">문자열</param>
        public void Fatal(string msg)
        {
            _log.Fatal(msg);
        }

        /// <summary>
        /// log4net FatalFormat
        /// </summary>
        /// <param name="format">문자열</param>
        /// <param name="arg">문자열 배열</param>
        public void FatalFormat(string format, params object[] arg)
        {
            _log.FatalFormat(format, arg);
        }

        /// <summary>
        /// log4net Warn
        /// </summary>
        /// <param name="msg">문자열</param>
        public void Warn(string msg)
        {
            _log.Warn(msg);
        }

        /// <summary>
        /// log4net WarnFormat
        /// </summary>
        /// <param name="format">문자열</param>
        /// <param name="arg">문자열 배열</param>
        public void WarnFormat(string format, params object[] arg)
        {
            _log.WarnFormat(format, arg);
        }
    }
}

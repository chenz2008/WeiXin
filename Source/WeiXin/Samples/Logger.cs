using System;
using System.IO;
using WeiXin.Core;

namespace Samples
{
    public class Logger : ILogger
    {
        readonly string debugFileName = @"debug.txt";
        readonly string infoFileName = @"info.txt";
        readonly string warningFileName = @"warning.txt";
        readonly string errorFileName = @"error.txt";

        public Logger()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var logPath = Path.Combine(baseDir, "Log");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            debugFileName = Path.Combine(logPath, debugFileName);
            infoFileName = Path.Combine(logPath, infoFileName);
            warningFileName = Path.Combine(logPath, warningFileName);
            errorFileName = Path.Combine(logPath, errorFileName);
        }

        public void Debug(string format, params object[] objs)
        {
            LogWrite("Debug", debugFileName, format, objs);
        }

        public void Info(string format, params object[] objs)
        {
            LogWrite("Info", infoFileName, format, objs);
        }

        public void Warning(string format, params object[] objs)
        {
            LogWrite("Warning", warningFileName, format, objs);
        }

        public void Error(string format, params object[] objs)
        {
            LogWrite("Error", errorFileName, format, objs);
        }

        private void LogWrite(string type, string fileName, string format, params object[] objs)
        {
            string msg = string.Format(format, objs);
            File.AppendAllText(fileName, string.Format("时间：{0}\r\n类型：{1}\r\n消息：{2}\r\n\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), type, msg));
        }
    }
}
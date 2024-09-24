using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ProtocoloConsultasAPI
{
    public static class Logger
    {
        private static readonly string logFilePath = "C:\\Users\\administrador\\Desktop";

        public static void Log(string message)
        {
            var logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;

namespace YaskawaNet
{
    static class DiagnosticException
    {
        private static readonly object _lockFile = new object();
        public static Stopwatch globalStopWatch = new Stopwatch();

        public static void ExceptionHandler(Exception exception)
        {
#if DEBUG

            Debug.WriteLine("DX200::" + DateTime.Now.ToShortTimeString() + "::" + "Error" + "::" + exception.Source + "::" + exception.StackTrace + "::" + exception.Message);
            LogToFile("DX200::" + DateTime.Now.ToShortTimeString() + "::" + exception.Source + "::" + exception.StackTrace + "::" + exception.Message);

#endif
        }
        public static void LogToFile(string message)
        {
            try
            {
                lock (_lockFile)
                {
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "YaskawaNet\\YaskawaNetLogs\\" + "Log.txt", message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

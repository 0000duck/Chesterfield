using System.Diagnostics;

namespace YaskawaNet
{
    static class DiagnosticException
    {
        public static Stopwatch globalStopWatch = new Stopwatch();

        public static void ExceptionHandler(string exception)
        {
#if DEBUG

            Debug.WriteLine(exception);

#endif
        }
    }
}

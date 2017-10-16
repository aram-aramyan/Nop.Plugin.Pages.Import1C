using System;
using System.IO;
using System.Text;

namespace Nop.Plugin.Pages.Import1C.Services
{
    internal static class LogToFile
    {
        internal static void Log(this string fileName, string line) 
            => LogText(fileName, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}\t{line}\r\n");

        internal static void LogText(this string fileName, string text)
        {
            File.AppendAllText(fileName, text, Encoding.UTF8);
        }
    }
}

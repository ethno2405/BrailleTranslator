//using NLog;
using System;
using System.IO;

namespace BGAssist.Braille.Client.Logic
{
    public enum LogType { Trace, Debug, Warn, Fatal, Error }

    //public static class Log
    //{
    //    public static void Write(string message, LogType type, Exception ex = null)
    //    {
    //        try
    //        {
    //            if (Directory.Exists("C:/BGAssist/Braille Editor/logs") == false)
    //            {
    //                try
    //                {
    //                    Directory.CreateDirectory("C:/BGAssist/Braille Editor/logs");
    //                }
    //                catch
    //                {
    //                }
    //            }

    //            //Logger logger = LogManager.GetCurrentClassLogger();

    //            switch (type)
    //            {
    //                case LogType.Error:
    //                    logger.Error(message, ex);
    //                    break;

    //                case LogType.Debug:
    //                    logger.Debug(message, ex);
    //                    break;

    //                case LogType.Warn:
    //                    logger.Warn(message, ex);
    //                    break;

    //                case LogType.Fatal:
    //                    logger.Fatal(message, ex);
    //                    break;

    //                case LogType.Trace:
    //                default:
    //                    logger.Trace(message, ex);
    //                    break;
    //            }
    //        }
    //        catch
    //        {
    //        }
    //    }
    //}
}
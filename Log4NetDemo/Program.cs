using System;

namespace Log4NetDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LogData();
        }

        public static void LogData()
        {
            //记录一般信息
            LogHelper.Info("info");
            //记录调试信息
            LogHelper.Debug("debug");
            //记录警告信息
            LogHelper.Warn("warn");

            //记录错误日志
            LogHelper.Error("error", new Exception("发生了一个异常"));
            //记录严重错误
            LogHelper.Fatal("fatal", new Exception("发生了一个致命错误"));

            Console.WriteLine("日志记录完毕。");
            Console.Read();
        }
    }
}
using System;
using Quartz;

namespace Quertz.net.demo
{
    public class DemoJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("作业执行!"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}

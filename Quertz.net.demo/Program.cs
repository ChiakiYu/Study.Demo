using Quartz;
using Quartz.Impl;

namespace Quertz.net.demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start(); //开启调度器

            var job = JobBuilder.Create<DemoJob>()
                .WithIdentity("myJob", "group1")
                .UsingJobData("jobSays", "Hello World!")
                .Build();


            var trigger = TriggerBuilder.Create()
                .WithIdentity("mytrigger", "group1")
                .StartNow()
                .WithCronSchedule("/5 * * ? * *") //时间表达式，5秒一次     
                .Build();


            scheduler.ScheduleJob(job, trigger);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;
using System.Workflow.Runtime;

namespace WorkflowDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var workflowRuntime = new WorkflowRuntime())
            {
                var waitHandle = new AutoResetEvent(false);
                workflowRuntime.WorkflowCompleted += delegate { waitHandle.Set(); };
                workflowRuntime.WorkflowTerminated += delegate(object sender, WorkflowTerminatedEventArgs e)
                {
                    Console.WriteLine(e.Exception.Message);
                    waitHandle.Set();
                };
                var str = Console.ReadLine();
                var dic = new Dictionary<string, object> { { "Str", str } };

                var instance = workflowRuntime.CreateWorkflow(typeof(Workflow1), dic);
                instance.Start();

                waitHandle.WaitOne();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Workflow.Runtime;
using System.Workflow.Runtime.Hosting;

namespace WorkflowDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WorkflowRuntime workflowRuntime = new WorkflowRuntime())
            {
                AutoResetEvent waitHandle = new AutoResetEvent(false);
                workflowRuntime.WorkflowCompleted += delegate(object sender, WorkflowCompletedEventArgs e) { waitHandle.Set(); };
                workflowRuntime.WorkflowTerminated += delegate(object sender, WorkflowTerminatedEventArgs e)
                {
                    Console.WriteLine(e.Exception.Message);
                    waitHandle.Set();
                };
                string str = Console.ReadLine();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Str", str);

                WorkflowInstance instance = workflowRuntime.CreateWorkflow(typeof(WorkflowDemo.Workflow1), dic);
                instance.Start();

                waitHandle.WaitOne();
            }
        }
    }
}

using System;
using System.Workflow.Activities;

namespace WorkflowDemo
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        public Workflow1()
        {
            InitializeComponent();
        }

        public string Str { get; set; }

        private void EvaluatePostalCode(object sender, ConditionalEventArgs e)
        {
            e.Result = Str.Equals("1");
        }

        private void PostalCodeVaild(object sender, EventArgs e)
        {
            Console.Write("{0}：验证通过", Str);
            Console.ReadKey();
        }

        private void PostalCodeInVaild(object sender, EventArgs e)
        {
            Console.Write("{0}：没通过验证", Str);
            Console.ReadKey();
        }
    }
}
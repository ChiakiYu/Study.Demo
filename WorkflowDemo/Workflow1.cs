using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace WorkflowDemo
{
    public sealed partial class Workflow1 : SequentialWorkflowActivity
    {
        private string _str;

        public string Str
        {
            get { return _str; }
            set { _str = value; }
        }


        public Workflow1()
        {
            InitializeComponent();
        }

        private void EvaluatePostalCode(object sender, ConditionalEventArgs e)
        {
            e.Result = _str.Equals("1");
        }

        private void PostalCodeVaild(object sender, EventArgs e)
        {
            Console.Write("{0}：验证通过", _str);
            Console.ReadKey();
        }

        private void PostalCodeInVaild(object sender, EventArgs e)
        {
            Console.Write("{0}：没通过验证", _str);
            Console.ReadKey();
        }
    }

}

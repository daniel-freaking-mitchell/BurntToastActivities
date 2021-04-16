using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Management.Automation;
using System.Threading;
using System.IO;

namespace BurntToastActivities
{
    public class Create_Toast : CodeActivity
    {
        [Category("Input"),DefaultValue("")]
        public InArgument<String> IconPath { get; set; }

        [Category("Input"),DefaultValue("")]
        public InArgument<String> Header { get; set; }

        [Category("Input"),DefaultValue("")]
        public InArgument<String> LineOne { get; set; }

        [Category("Input"),DefaultValue("")]
        public InArgument<String> LineTwo { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddCommand("New-BurntToastNotification");

                //Sets the icon of the notification to the picture at the specified path
                if (!String.IsNullOrEmpty(IconPath.Get(context))) ps.AddParameter("AppLogo", Path.GetFullPath(IconPath.Get(context)));

                //
                if (!String.IsNullOrEmpty(Header.Get(context)))
                {
                    ps.AddParameter("Text");
                }
                IAsyncResult result = ps.BeginInvoke();
                while (result.IsCompleted == false) Thread.Sleep(100);
            }
        }
    }
}

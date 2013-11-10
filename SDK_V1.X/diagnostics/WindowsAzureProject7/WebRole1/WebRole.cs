using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WebRole1
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            DiagnosticMonitorConfiguration dmc = DiagnosticMonitor.GetDefaultInitialConfiguration();
            dmc.Logs.ScheduledTransferPeriod = TimeSpan.FromSeconds(1);
            dmc.Logs.ScheduledTransferLogLevelFilter = LogLevel.Verbose;

            DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", dmc);

            return  base.OnStart();
            //// For information on handling configuration changes
            //// see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //System.Diagnostics.Trace.WriteLine("Entering OnStart in WebRole...");

            //var traceResource = RoleEnvironment.GetLocalResource("TraceFiles");
            //var config = DiagnosticMonitor.GetDefaultInitialConfiguration();
            //config.Directories.DataSources.Add(
            //    new DirectoryConfiguration
            //    {
            //        Path = traceResource.RootPath,
            //        Container = "traces",
            //        DirectoryQuotaInMB = 100
            //    });
            //config.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1);

            //DiagnosticMonitor.Start("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString", config);

            //return true;

            //return base.OnStart();
        }
    }
}

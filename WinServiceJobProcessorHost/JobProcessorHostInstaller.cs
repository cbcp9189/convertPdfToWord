using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WinServiceHost
{
	[RunInstaller(true)]
	public partial class JobProcessorHostInstaller : System.Configuration.Install.Installer
	{
		public JobProcessorHostInstaller()
		{
			ServiceProcessInstaller process = new ServiceProcessInstaller();
			// We want LocalSystem
			process.Account = ServiceAccount.LocalSystem;         
			
			ServiceInstaller service = new ServiceInstaller(); service.ServiceName = "JobProcessorWindowService";
			service.DisplayName = "JobProcessorWindowService";
			service.Description = "Solid Documents Job Processor Service Hosted Successfully.";
			service.StartType = ServiceStartMode.Automatic;
						
			Installers.Add(process);
			Installers.Add(service);

			// After we are installed, go ahead and start the service.
			service.AfterInstall += service_AfterInstall;
		}

		void service_AfterInstall(object sender, InstallEventArgs e)
		{
			using (ServiceController sc = new ServiceController((sender as ServiceInstaller).ServiceName))
			{
				sc.Start();
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinServiceProcessorHost
{
	public partial class ProcessorHost : ServiceBase
    {
        ServiceHost jobProcessorServiceHost = null;

        public ProcessorHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                // Base Address for JobProcessService
                Uri httpBaseAddress = new Uri("http://localhost:4321/JobProcessorService");
				jobProcessorServiceHost = new ServiceHost(typeof(SolidFrameworkService.JobProcessorService), httpBaseAddress);
                //Add Endpoint to Host
                BasicHttpBinding binding = new BasicHttpBinding();
				binding.MaxReceivedMessageSize = 4294967296;
                binding.MaxBufferSize = 65536;
                binding.TransferMode = TransferMode.Streamed;

				jobProcessorServiceHost.AddServiceEndpoint(typeof(SolidFrameworkService.IjobProcessorService), binding, "");
                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
				jobProcessorServiceHost.Description.Behaviors.Add(serviceBehavior);
                //Open
				jobProcessorServiceHost.Open();
            }
            catch (Exception)
            {
				jobProcessorServiceHost = null;
            }
        }

        protected override void OnStop()
        {
			if (jobProcessorServiceHost != null)
            {
				jobProcessorServiceHost.Close();
				jobProcessorServiceHost = null;
            }
        }
    }
}

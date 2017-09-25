using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ServiceConsole
{
    class Program
    {
        static ServiceHost jobProcessorHost = null;
        
        static void Main(string[] args)
        {
            try
            {
                //Base Address for JobProcessorService
                Uri httpBaseAddress = new Uri("http://localhost:4321/JobProcessorService");
				jobProcessorHost = new ServiceHost(typeof(SolidFrameworkService.JobProcessorService), httpBaseAddress);
                //Add Endpoint to Host
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 4294967296;
                binding.MaxBufferSize = 65536;
                binding.TransferMode = TransferMode.Streamed;

				jobProcessorHost.AddServiceEndpoint(typeof(SolidFrameworkService.IjobProcessorService), binding, "");
                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
				jobProcessorHost.Description.Behaviors.Add(serviceBehavior);
                //Open
				jobProcessorHost.Open();
            }
            catch (Exception ex)
            {
				jobProcessorHost = null;
                Console.WriteLine("Service starting failed: " + ex.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Service started. Press any key to exit.");
            Console.ReadKey();
			jobProcessorHost.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestServiceClient.JobProcessorServiceReference;
using System.Threading;

namespace TestServiceClient
{
	class Program
	{
		static void Main(string[] args)
		{
            IjobProcessorServiceClient client = new IjobProcessorServiceClient();
			client.Open();

			Console.Write("Input a file path to upload: ");
			string uploadPath = Console.ReadLine();
				
			if (!File.Exists(uploadPath))
			{
				Console.WriteLine(uploadPath + " was not found.");
				Console.WriteLine("Press any key to exit.");
				Console.ReadKey();
				client.Close();
				return;
			}

			int jobId = 0;
			using (System.IO.FileStream stream = new System.IO.FileStream(uploadPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
			{
				Console.WriteLine("Uploading file, please wait.");
				PdfToWordJobEnvelope envelope = new PdfToWordJobEnvelope();
				envelope.Mode = ReconstructionMode.Flowing;
				envelope.DetectTables = true;
				envelope.DocType = WordDocumentType.Rtf;
				envelope.HeadersAndFooters = HeaderAndFooterMode.Detect;
				envelope.ImageAnchor = ImageAnchoringMode.Automatic;
				envelope.TextRecovery = TextRecovery.Automatic;
				jobId = client.UploadWordJob(Path.GetFileName(uploadPath), stream.Length, envelope, stream);
				Console.WriteLine("File upload completed. Job ID = " + jobId.ToString());
			}
			
			if (jobId != 0)
			{
				Console.WriteLine();
				Console.Write("Processing");
				JobEnvelopeStatus status;
				do
				{
					Thread.Sleep(500);
					Console.Write(".");
					status = client.GetJobStatus(jobId);
				} while(status.Status == JobStatus.Created || status.Status == JobStatus.Started);

				Console.WriteLine(); 
				Console.WriteLine(string.Format("Finished Job {0} with Status = {1} and Message = {2}.", jobId, status.Status, status.Message));
				Console.WriteLine(); 
				Console.WriteLine("Downloading converted file.");
				
				Stream downloadStream;
				long fileLength;
				string strConvertedName = client.DownloadFile(jobId, out fileLength, out downloadStream);

				string downloadPath = @"d:\download\";
				downloadPath = Path.Combine(downloadPath, strConvertedName);

				FileStream localStream = new FileStream(downloadPath, FileMode.Create);
				downloadStream.CopyTo(localStream);					
				localStream.Flush();
				localStream.Close();
				downloadStream.Close();
				
				// Tell the server to remove the job now that we have got the converted file.
				
				client.Close();

				Console.WriteLine(String.Format("{0} downloaded, length returned was {1} bytes.", downloadPath, fileLength));
				Console.WriteLine(); 
				Console.WriteLine("Press any key to exit.");
				Console.ReadKey();
			}
		}
	}
}

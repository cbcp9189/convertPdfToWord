using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobProcessorService.JobEnvelopes;

namespace SolidFrameworkService
{
	public partial class JobProcessorService
	{
		public JobId UploadWordJob(RemoteWordJob request)
		{
			FileStream targetStream = null;
			Stream sourceStream = request.FileByteStream;

			string uploadFolder = ServiceUploadDirectory;

			Guid guid = Guid.NewGuid();
			string extension = Path.GetExtension(request.FileName);
			string sourcePath = Path.Combine(uploadFolder, string.Format("{0}{1}", guid.ToString(), extension));
			string originalName = Path.GetFileNameWithoutExtension(request.FileName);

			using (targetStream = new FileStream(sourcePath, FileMode.Create,
								  FileAccess.Write, FileShare.None))
			{
				//read from the input stream in 65000 byte chunks

				const int bufferLen = 65000;
				byte[] buffer = new byte[bufferLen];
				int count = 0;
				while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
				{
					// save to output stream
					targetStream.Write(buffer, 0, count);
				}
				targetStream.Close();
				sourceStream.Close();
			}

			SolidFramework.Services.PdfToWordJobEnvelope job = new SolidFramework.Services.PdfToWordJobEnvelope();
			job.ReconstructionMode = request.WordEnvelope.Mode;
			job.ImageAnchoringMode = request.WordEnvelope.ImageAnchor;
			job.DetectTables = request.WordEnvelope.DetectTables;
			job.OcrMode = request.WordEnvelope.TextRecovery;
			job.OutputType = request.WordEnvelope.DocType;

			if (!string.IsNullOrEmpty(request.WordEnvelope.Password))
			{
				job.Password = request.WordEnvelope.Password;
			}

			job.SourcePath = sourcePath;

			job.CustomData = (Object)Path.GetFileName(originalName);
			// Submit the job
			sfProcessor.SubmitJob(job);

			JobId jobid = new JobId();
			jobid.ID = job.ID;
			return jobid;
		}

        public JobId convertWordJob(String sourcePath)
        {
            SolidFramework.Services.PdfToWordJobEnvelope job = new SolidFramework.Services.PdfToWordJobEnvelope();
            //job.ReconstructionMode = request.WordEnvelope.Mode;
            //job.ImageAnchoringMode = request.WordEnvelope.ImageAnchor;
            //job.DetectTables = request.WordEnvelope.DetectTables;
            //job.OcrMode = request.WordEnvelope.TextRecovery;
            //job.OutputType = request.WordEnvelope.DocType;

            job.SourcePath = sourcePath;
            String originalName = Path.GetFileName(sourcePath);
            
            job.CustomData = (Object)Path.GetFileName(originalName);
            // Submit the job
            sfProcessor.SubmitJob(job);

            JobId jobid = new JobId();
            jobid.ID = job.ID;
            return jobid;
        }
	}
}

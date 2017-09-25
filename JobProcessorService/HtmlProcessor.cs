using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobProcessorService.JobEnvelopes;
using System.IO;

namespace SolidFrameworkService
{
	public partial class JobProcessorService
	{
		public JobId UploadHtmlJob(RemoteHtmlJob request)
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

			SolidFramework.Services.PdfToHtmlJobEnvelope job = new SolidFramework.Services.PdfToHtmlJobEnvelope();
			job.HtmlNavigation = request.HtmlEnvelope.Navigation;
			job.Images = request.HtmlEnvelope.Images;
			job.ImageType = request.HtmlEnvelope.ImageType;
			job.HtmlNavigation = request.HtmlEnvelope.Navigation;
			if (request.HtmlEnvelope.Navigation == SolidFramework.Converters.HtmlNavigation.SingleWithEmbeddedNavigation ||
				request.HtmlEnvelope.Navigation == SolidFramework.Converters.HtmlNavigation.SplitIntoMultipleFiles)
			{
				job.HtmlSplittingUsing = request.HtmlEnvelope.SplitUsing;
			}

			job.WidthLimit = request.HtmlEnvelope.WidthLimit;

			if (!string.IsNullOrEmpty(request.HtmlEnvelope.Password))
			{
				job.Password = request.HtmlEnvelope.Password;
			}

			job.SourcePath = sourcePath;

			job.CustomData = (Object)Path.GetFileName(originalName);
			// Submit the job
			sfProcessor.SubmitJob(job);

			JobId jobid = new JobId();
			jobid.ID = job.ID;
			return jobid;
		}
	}
}

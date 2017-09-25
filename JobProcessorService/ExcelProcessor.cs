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
		public JobId UploadExcelJob(RemoteExcelJob request)
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

			SolidFramework.Services.PdfToExcelJobEnvelope job = new SolidFramework.Services.PdfToExcelJobEnvelope();
			job.TablesFromContent = request.ExcelEnvelope.NonTableContent;
			if (request.ExcelEnvelope.CombineTables)
			{
				job.SingleTable = SolidFramework.Converters.Plumbing.ExcelTablesOnSheet.PlaceAllTablesOnSingleSheet;
			}
			else
			{
				job.SingleTable = SolidFramework.Converters.Plumbing.ExcelTablesOnSheet.PlaceEachTableOnOwnSheet;
			}

			job.DecimalSeparator = request.ExcelEnvelope.DecimalSeperator;
			job.ThousandsSeparator = request.ExcelEnvelope.ThousandsSeperator;
			job.AutoDetectSeparators = request.ExcelEnvelope.AutoDetectSeparators;
			job.OcrMode = request.ExcelEnvelope.TextRecovery;

			if (!string.IsNullOrEmpty(request.ExcelEnvelope.Password))
			{
				job.Password = request.ExcelEnvelope.Password;
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

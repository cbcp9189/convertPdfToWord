using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JobProcessorService.JobEnvelopes;
using System.IO.Compression;

namespace SolidFrameworkService
{
	// NOTE: We use a singleton context here so that we only have ONE Job Processor for each
	// service call.

	public class JobData
	{
		public int JobId;
		public DateTime Date;
	}

	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public partial class JobProcessorService : IjobProcessorService, IDisposable
    {		
		// Location to put uploaded pdf files for conversion. LOCAL_SYSTEM should have read/write access to folder.
        const string ServiceUploadDirectory = @"D:\solid\upload\";
		// Path and name of log file.
        const string LogFile = @"D:\solid\download\processor.log";

		const double JOB_EXPIRE_TIME = 30;						// Time that a completed job expires (30 minutes).
		const double TIMER_ELAPSE_TIME = 1000 * 60 * 15;		// Interval to check for stale jobs (15 minutes).

		static SolidFramework.Services.JobProcessor sfProcessor = null;
		static List<SolidFramework.Services.Plumbing.JobEnvelope> CompletedJobs = new List<SolidFramework.Services.Plumbing.JobEnvelope>();
		static List<JobData> JobDates = new List<JobData>();
		static System.Timers.Timer timer = new System.Timers.Timer();

		public JobProcessorService()
		{
            // Aways unpack. This way switching bitness will update SF files correctly.
			SolidFramework.Configuration.Installer.ForceUnpack = true;

			/*****************************************************************************************
			 ****************************** Set your license info here. ******************************
			 *****************************************************************************************/
            SolidFramework.License.Import(@"chenbo", @"chenbo@hoboom.com", string.Empty, @"8e441-a9831-0d291-0137b");
            //SolidFramework.License.Import(@"D:\User\license.xml");
            
            
			// Turn on SolidFramework logging.
			SolidFramework.Plumbing.Logging.Instance.Path = LogFile;
            
			// Add an entry to the log on where Solid Framework is getting extracted.
			SolidFramework.Plumbing.Logging.Instance.WriteLine("SF Extraction = " + SolidFramework.Configuration.Installer.NativePlatformDirectory);
            
			// Start Solid Framework Job Processor.
			sfProcessor = new SolidFramework.Services.JobProcessor();

			// Connect to Job Processor Job Completed event.
			sfProcessor.JobCompletedEvent += sfProcessor_JobCompletedEvent;

			// Cleanup timer, event fires every half hour.
			timer.AutoReset = true;
			timer.Interval = TIMER_ELAPSE_TIME;
			timer.Elapsed += timer_Elapsed;
			timer.Enabled = true;
		}

		void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			lock (JobDates)
			{
				List<JobData> elapsedJobs = new List<JobData>();
				DateTime currentDate = DateTime.Now;
				foreach (JobData data in JobDates)
				{
					TimeSpan diff = currentDate - data.Date;
					if (diff.Minutes >= JOB_EXPIRE_TIME)
					{
						elapsedJobs.Add(data);
					}
				}

				if (elapsedJobs.Count > 0)
				{
					lock (CompletedJobs)
					{
						foreach (JobData data in elapsedJobs)
						{
							SolidFramework.Services.Plumbing.JobEnvelope doneEnvelope = null;
							foreach (SolidFramework.Services.Plumbing.JobEnvelope envelope in CompletedJobs)
							{
								if (envelope.ID == data.JobId)
								{
									doneEnvelope = envelope;
									break;
								}
							}

							if (doneEnvelope != null)
							{
								if (File.Exists(doneEnvelope.SourcePath))
								{
									File.Delete(doneEnvelope.SourcePath);
								}

								if (File.Exists(doneEnvelope.OutputPaths[0]))
								{
									File.Delete(doneEnvelope.OutputPaths[0]);
								}

								CompletedJobs.Remove(doneEnvelope);
							}

							JobDates.Remove(data);
						}
					}
				}
			}
		}
	
		public void Dispose()
		{
			// Stop the cleanup timer.
			timer.Enabled = false;
			timer.Dispose();
			
			// Stop the Job Processor when we get closed.
			sfProcessor.Close();
			sfProcessor.Dispose();

			// Remove any converted files that haven't been processed.
			foreach (SolidFramework.Services.Plumbing.JobEnvelope envelope in CompletedJobs)
			{
				if (File.Exists(envelope.SourcePath))
				{
					File.Delete(envelope.SourcePath);
				}

				if (File.Exists(envelope.OutputPaths[0]))
				{
					File.Delete(envelope.OutputPaths[0]);
				}
			}
		}

		void sfProcessor_JobCompletedEvent(object sender, SolidFramework.Services.JobCompletedEventArgs e)
		{
            lock (CompletedJobs)
            {
				SolidFramework.Plumbing.Logging.Instance.WriteLine(string.Format("Service Job Completed for JobID = {0}, status = {1}", e.JobEnvelope.ID, e.JobEnvelope.Status.ToString())); 
				CompletedJobs.Add(e.JobEnvelope);

				lock (JobDates)
				{
					// Hold the datetime of the start of the job.
					JobData jobDate = new JobData();
					jobDate.JobId = e.JobEnvelope.ID;
					jobDate.Date = DateTime.Now;
					JobDates.Add(jobDate);
				}
            }
		}

		public JobEnvelopeStatus GetJobStatus(int jobid)
        {
			JobEnvelopeStatus jobStatus = new JobEnvelopeStatus();

            lock (CompletedJobs)
            {
				foreach (SolidFramework.Services.Plumbing.JobEnvelope envelope in CompletedJobs)
                {
                    if (envelope.ID == jobid)
                    {
                        jobStatus.Status = envelope.Status;
                        jobStatus.Message = envelope.Message;
                        return jobStatus;
                    }
                }
            }
            
            // If we get here. assume the job is still processing. TODO: Check with M on this.
            jobStatus.Status = SolidFramework.Services.Plumbing.JobStatus.Started;
            jobStatus.Message = @"Started";
            return jobStatus;
        }

        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();

            lock (CompletedJobs)
            {
                foreach (SolidFramework.Services.Plumbing.JobEnvelope envelope in CompletedJobs)
                {
                    if (envelope.ID == request.JobId)
                    {
						if (envelope.Name == "PdfToHtmlJobEnvelope")
						{
							string fullFilePath = envelope.OutputPaths[0];
							string path = Path.GetDirectoryName(fullFilePath);
							string fileName = Path.GetFileName(fullFilePath);
							string fileExtension = Path.GetExtension(fileName);
							string folderName = Path.GetFileNameWithoutExtension(fileName);
							string orgName = Path.GetFileNameWithoutExtension((string)envelope.CustomData);
							string zipFilename = orgName + ".zip";
							string zipPath = Path.Combine(path, zipFilename);
							
							if (Directory.Exists(Path.Combine(path, folderName)))
							{
								using (ZipArchive zipFile = ZipFile.Open(zipPath, ZipArchiveMode.Create))
								{
									zipFile.CreateEntryFromFile(fullFilePath, fileName);
	
									string[] files = Directory.GetFiles(Path.Combine(path, folderName), "*.*");
									foreach (string file in files)
									{
										string myName = Path.GetFileName(file);
										zipFile.CreateEntryFromFile(file, folderName + Path.DirectorySeparatorChar + myName);
									}
								}

								// open stream
								System.IO.FileStream stream = new System.IO.FileStream(zipPath,
											System.IO.FileMode.Open, System.IO.FileAccess.Read);

								result.FileName = zipFilename;
								result.Length = stream.Length;
								result.FileByteStream = stream;
							}
							else
							{
								// open stream
								System.IO.FileStream stream = new System.IO.FileStream(fullFilePath,
											System.IO.FileMode.Open, System.IO.FileAccess.Read);

								result.FileName = (string)envelope.CustomData + fileExtension;
								result.Length = stream.Length;
								result.FileByteStream = stream;
							}
						}
						else if (envelope.Name == "PdfToImagesJobEnvelope")
						{
							string fullFilePath = envelope.OutputPaths[0];
							string path = Path.GetDirectoryName(fullFilePath); 
							string orgName = Path.GetFileNameWithoutExtension((string)envelope.CustomData);
							string zipFilename = orgName + ".zip";
							string zipPath = Path.Combine(path, zipFilename);

							using (ZipArchive zipFile = ZipFile.Open(zipPath, ZipArchiveMode.Create))
							{
								foreach (string file in envelope.OutputPaths)
								{
									string filename = Path.GetFileName(file);
									zipFile.CreateEntryFromFile(file, filename);
								}
							}
							
							// open stream
							System.IO.FileStream stream = new System.IO.FileStream(zipPath,
										System.IO.FileMode.Open, System.IO.FileAccess.Read);

							result.FileName = zipFilename;
							result.Length = stream.Length;
							result.FileByteStream = stream;
						}
						else
						{
							string filePath = envelope.OutputPaths[0];
							System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
							string fileExtension = Path.GetExtension(envelope.OutputPaths[0]);

							// check if exists
							if (!fileInfo.Exists)
							{
								throw new System.IO.FileNotFoundException("File not found", envelope.OutputPaths[0]);
							}

							// open stream
							System.IO.FileStream stream = new System.IO.FileStream(filePath,
										System.IO.FileMode.Open, System.IO.FileAccess.Read);

							result.FileName = (string)envelope.CustomData + fileExtension;
							result.Length = stream.Length;
							result.FileByteStream = stream;
						}
                        break;
                    }
                }
            }

            return result;
        }
    }
}

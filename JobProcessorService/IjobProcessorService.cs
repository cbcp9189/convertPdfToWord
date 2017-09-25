using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JobProcessorService.JobEnvelopes;

namespace SolidFrameworkService
{
    [ServiceContract]
    public interface IjobProcessorService
    {
        [OperationContract]
		JobEnvelopeStatus GetJobStatus(int jobid);

        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        [OperationContract]
		JobId UploadWordJob(RemoteWordJob request);

		[OperationContract]
		JobId UploadExcelJob(RemoteExcelJob request);

		[OperationContract]
		JobId UploadPowerPointJob(RemotePowerPointJob request);

		[OperationContract]
		JobId UploadPdfAJob(RemotePdfAJob request);

		[OperationContract]
		JobId UploadTextJob(RemoteTextJob request);

		[OperationContract]
		JobId UploadDataJob(RemoteDataJob request);

		[OperationContract]
		JobId UploadHtmlJob(RemoteHtmlJob request);

		[OperationContract]
		JobId UploadImageJob(RemoteImageJob request);
	}

	public class JobEnvelopeStatus
	{
		public SolidFramework.Services.Plumbing.JobStatus Status;
		public string Message;
	}

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public int JobId;
    }

	[MessageContract]
	public class JobId
	{
		[MessageBodyMember]
		public int ID;
	}

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}

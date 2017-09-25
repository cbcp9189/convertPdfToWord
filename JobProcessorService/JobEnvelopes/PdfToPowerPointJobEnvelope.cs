using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToPowerPointJobEnvelope
	{
		public bool DetectLists { get; set; }
		public SolidFramework.Converters.Plumbing.TextRecovery TextRecovery { get; set; }
		public string Password { get; set; }

		PdfToPowerPointJobEnvelope()
		{
			DetectLists = true;
			TextRecovery = SolidFramework.Converters.Plumbing.TextRecovery.Automatic;
			Password = null;
		}
	}

	[MessageContract]
	public class RemotePowerPointJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToPowerPointJobEnvelope PowerPointEnvelope;

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

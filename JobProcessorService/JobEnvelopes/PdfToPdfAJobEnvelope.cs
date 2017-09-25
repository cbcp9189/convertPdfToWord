using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToPdfAJobEnvelope
	{
		public bool Searchable { get; set; }
		public SolidFramework.Plumbing.ValidationMode Mode { get; set; }
		public string Password { get; set; }

		PdfToPdfAJobEnvelope()
		{
			Password = null;
			Searchable = false;
			Mode = SolidFramework.Plumbing.ValidationMode.PdfA1B;
		}
	}

	[MessageContract]
	public class RemotePdfAJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToPdfAJobEnvelope PdfAEnvelope;

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

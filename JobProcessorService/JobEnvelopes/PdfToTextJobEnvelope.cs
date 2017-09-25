using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToTextJobEnvelope
	{
		public int LineLength { get; set; }
		public SolidFramework.Converters.Plumbing.LineTerminator Terminator { get; set; }
		public int CharacterEncoding { get; set; }
		public String Password { get; set; }

		PdfToTextJobEnvelope()
		{
			Password = null;
			LineLength = 100;
			Terminator = SolidFramework.Converters.Plumbing.LineTerminator.Windows;
			CharacterEncoding = 1252; 
		}
	}

	[MessageContract]
	public class RemoteTextJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToTextJobEnvelope TextEnvelope;

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

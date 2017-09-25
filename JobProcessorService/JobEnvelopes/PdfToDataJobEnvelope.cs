using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToDataJobEnvelope
	{
		public SolidFramework.Converters.Plumbing.DelimiterOptions Delimiter { get; set; }
		public SolidFramework.Converters.Plumbing.LineTerminator Terminator { get; set; }
		public int CharacterEncoding { get; set; }
		public string Password { get; set; }

		PdfToDataJobEnvelope()
		{
			Delimiter = SolidFramework.Converters.Plumbing.DelimiterOptions.Comma;
			Terminator = SolidFramework.Converters.Plumbing.LineTerminator.Windows;
			CharacterEncoding = 1252;
			Password = null;
		}
	}

	[MessageContract]
	public class RemoteDataJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToDataJobEnvelope DataEnvelope;

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

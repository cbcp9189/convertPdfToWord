using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToWordJobEnvelope
	{
		public SolidFramework.Converters.Plumbing.ReconstructionMode Mode { get; set; }
		public SolidFramework.Converters.Plumbing.ImageAnchoringMode ImageAnchor { get; set; }
		public bool DetectTables { get; set; }
		public SolidFramework.Converters.Plumbing.HeaderAndFooterMode HeadersAndFooters { get; set; }
		public SolidFramework.Converters.Plumbing.TextRecovery TextRecovery { get; set; }
		public SolidFramework.Converters.Plumbing.WordDocumentType DocType { get; set; }
		public string Password { get; set; }

		PdfToWordJobEnvelope()
		{
			Mode = SolidFramework.Converters.Plumbing.ReconstructionMode.Flowing;
			ImageAnchor = SolidFramework.Converters.Plumbing.ImageAnchoringMode.Automatic;
			DetectTables = true;
			HeadersAndFooters = SolidFramework.Converters.Plumbing.HeaderAndFooterMode.Detect;
			TextRecovery = SolidFramework.Converters.Plumbing.TextRecovery.Automatic;
			DocType = SolidFramework.Converters.Plumbing.WordDocumentType.DocX;
			Password = null;
		}
	}

	[MessageContract]
	public class RemoteWordJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToWordJobEnvelope WordEnvelope;

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

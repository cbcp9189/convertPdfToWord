using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToImagesJobEnvelope
	{
		public SolidFramework.Converters.Plumbing.ImageDocumentType ImageType { get; set; }
		public SolidFramework.Converters.Plumbing.ImageConversionType ConversionType { get; set; }
		public int PageDPI { get; set; }
		public string Password { get; set; }

		PdfToImagesJobEnvelope()
		{
			ImageType = SolidFramework.Converters.Plumbing.ImageDocumentType.Default;
			ConversionType = SolidFramework.Converters.Plumbing.ImageConversionType.ExtractImages;
			PageDPI = 96;
			Password = null;
		}
	}

	[MessageContract]
	public class RemoteImageJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToImagesJobEnvelope ImageEnvelope;

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
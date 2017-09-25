using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToHtmlJobEnvelope
	{
		public SolidFramework.Converters.Plumbing.ImageDocumentType ImageType { get; set; }
		public SolidFramework.Converters.Plumbing.HtmlImages Images { get; set; }
		public SolidFramework.Converters.HtmlNavigation Navigation { get; set; }
		public SolidFramework.Converters.HtmlSplittingUsing SplitUsing { get; set; }
		public int WidthLimit { get; set; }
		public string Password { get; set; }

		PdfToHtmlJobEnvelope()
		{
			Images = SolidFramework.Converters.Plumbing.HtmlImages.Default;
			ImageType = SolidFramework.Converters.Plumbing.ImageDocumentType.Default;
			Navigation = SolidFramework.Converters.HtmlNavigation.SingleWithoutNavigation;
			SplitUsing = SolidFramework.Converters.HtmlSplittingUsing.Pages;
			Password = null;
		}
	}

	[MessageContract]
	public class RemoteHtmlJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToHtmlJobEnvelope HtmlEnvelope;

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
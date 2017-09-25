using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace JobProcessorService.JobEnvelopes
{
	public class PdfToExcelJobEnvelope
	{
		public bool NonTableContent { get; set; }
		public bool CombineTables { get; set; }
		public SolidFramework.Converters.Plumbing.DecimalSeparator DecimalSeperator { get; set; }
		public SolidFramework.Converters.Plumbing.ThousandsSeparator ThousandsSeperator { get; set; }
		public bool AutoDetectSeparators { get; set; }
		public SolidFramework.Converters.Plumbing.TextRecovery TextRecovery { get; set; }
		public string Password { get; set; }

		PdfToExcelJobEnvelope()
		{
			NonTableContent = true;
			CombineTables = true;
			AutoDetectSeparators = true;
			TextRecovery = SolidFramework.Converters.Plumbing.TextRecovery.Automatic;
			DecimalSeperator = SolidFramework.Converters.Plumbing.DecimalSeparator.Period;
			ThousandsSeperator = SolidFramework.Converters.Plumbing.ThousandsSeparator.Comma;
			Password = null;
		}
	}

	[MessageContract]
	public class RemoteExcelJob : IDisposable
	{
		[MessageHeader(MustUnderstand = true)]
		public string FileName;

		[MessageHeader(MustUnderstand = true)]
		public long Length;

		[MessageHeader(MustUnderstand = true)]
		public PdfToExcelJobEnvelope ExcelEnvelope;

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

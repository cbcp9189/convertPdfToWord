using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Runtime.Remoting.Messaging;

namespace wait
{
	/// <summary>
	/// Summary description for checkStatus.
	/// </summary>
	public partial class checkStatus : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Response.Clear();
			if (!string.IsNullOrEmpty(Request.QueryString["JobId"]))
			{
				int jobId = int.Parse(Request.QueryString["JobId"]);
				PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
				PdfToWord.JobProcessorServiceReference.JobEnvelopeStatus status = serviceClient.GetJobStatus(jobId);

				if (status.Status != PdfToWord.JobProcessorServiceReference.JobStatus.Created && status.Status != PdfToWord.JobProcessorServiceReference.JobStatus.Started)
				{
					Response.Write("1");
				}
				else
				{
					Response.Write("0");
				}
			}

			Response.End();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}

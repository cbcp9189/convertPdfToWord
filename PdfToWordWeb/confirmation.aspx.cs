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
using System.IO;

namespace wait
{
	/// <summary>
	/// Summary description for confirmation.
	/// </summary>
	public partial class confirmation : System.Web.UI.Page
	{
		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				int myJobId = -1;
				if (!string.IsNullOrEmpty(Request.QueryString["JobId"]))
				{
					myJobId = int.Parse(Request.QueryString["JobId"]);
				}

				HiddenField jobIdField = this.FindControl("JobID") as HiddenField;
				jobIdField.Value = myJobId.ToString();

				if (myJobId != -1)
				{
					Label myLabel = this.FindControl("Label1") as Label;
					Button myButton = this.FindControl("Button1") as Button;

					PdfToWord.JobProcessorServiceReference.JobEnvelopeStatus jobStatus = serviceClient.GetJobStatus(myJobId);

					if (jobStatus.Status != PdfToWord.JobProcessorServiceReference.JobStatus.Success)
					{
						myLabel.Text = string.Format("Conversion failed. Error = {0}", jobStatus.Status);
						myButton.Visible = false;
					}
					else
					{
						myLabel.Text = "Conversion completed successfully.";
						myButton.Visible = true;
					}
				}
			}
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

        protected void Button1_Click(object sender, EventArgs e)
        {
			// Put user code to initialize the page here
			long fileLength;
			Stream stream;

			HiddenField jobIdField = this.FindControl("JobID") as HiddenField;
			int jobId = int.Parse(jobIdField.Value);

			try
			{
				string filename = serviceClient.DownloadFile(jobId, out fileLength, out stream);
				string fileExtension = Path.GetExtension(filename);

				if (fileExtension.ToLower().Contains("doc") || fileExtension.ToLower().Contains("rtf"))
				{
					Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
				}
				else if (fileExtension.ToLower().Contains("xls"))
				{
					Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				}
				else if (fileExtension.ToLower().Contains("pptx"))
				{
					Response.ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
				}
				else if (fileExtension.ToLower().Contains("pdf"))
				{
					Response.ContentType = "application/pdf";
				}
				else
				{
					Response.ContentType = "text/plain";
				}

				string fname = Path.GetFileName(filename);
				Response.AppendHeader("Content-Disposition", "attachment; filename=" + fname);
				byte[] buffer = new byte[0x1000];
				int read;
				while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					Response.OutputStream.Write(buffer, 0, read);

				stream.Close();
			}
			catch (System.Exception ex)
			{
				Response.Clear();
				Response.Write("Converted file download failed: " + ex.Message);
			}
			Response.End();
        }

		protected void Button2_Click(object sender, EventArgs e)
		{
			Response.Redirect("Default.aspx");
		}
	}
}

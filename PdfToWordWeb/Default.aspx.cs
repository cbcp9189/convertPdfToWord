using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

using System.Configuration;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Data;

public partial class _Default : System.Web.UI.Page
{    
    [STAThread]
    protected void ConvertWordButton_Click(object sender, EventArgs e)
    {
        bool convertTheFile = true;

        if (FileUpLoad1.HasFile)
        {
            // all is well
        }
        else
        {
            convertTheFile = false;
        }
 
        if (!convertTheFile)
        {
            return;
        }

		PdfToWord.JobProcessorServiceReference.PdfToWordJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToWordJobEnvelope();
        
        if (flowing.Checked)
        {
			job.Mode = PdfToWord.JobProcessorServiceReference.ReconstructionMode.Flowing;
        }
        else if (exact.Checked)
        {
			job.Mode = PdfToWord.JobProcessorServiceReference.ReconstructionMode.Exact;
        }
        else
        {
			job.Mode = PdfToWord.JobProcessorServiceReference.ReconstructionMode.Continuous;
        }

		if (irauto.Checked)
		{
			job.ImageAnchor = PdfToWord.JobProcessorServiceReference.ImageAnchoringMode.Automatic;	
		}
		else if (irpara.Checked)
		{
			job.ImageAnchor = PdfToWord.JobProcessorServiceReference.ImageAnchoringMode.Paragraph;
		}
		else if (irpage.Checked)
		{
			job.ImageAnchor = PdfToWord.JobProcessorServiceReference.ImageAnchoringMode.Page;
		}
		else
		{
			job.ImageAnchor = PdfToWord.JobProcessorServiceReference.ImageAnchoringMode.RemoveImages;
		}

		if (tdon.Checked)
		{
			job.DetectTables = true;
		}
		else
		{
			job.DetectTables = false;	
		}

		if (hfrecover.Checked)
		{
			job.HeadersAndFooters = PdfToWord.JobProcessorServiceReference.HeaderAndFooterMode.Detect;
		}
		else if (hfplace.Checked)
		{
			job.HeadersAndFooters = PdfToWord.JobProcessorServiceReference.HeaderAndFooterMode.Ignore;
		}
		else
		{
			job.HeadersAndFooters = PdfToWord.JobProcessorServiceReference.HeaderAndFooterMode.Remove;
		}

		int sel = ddlOCR.SelectedIndex;
		if (sel == 0)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Automatic;
		}
		else if (sel == 1)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Always;
		}
		else
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Never;
		}

		sel = ddlType.SelectedIndex;
		if (sel == 0)
		{
			job.DocType = PdfToWord.JobProcessorServiceReference.WordDocumentType.DocX;
		}
		else
		{
			job.DocType = PdfToWord.JobProcessorServiceReference.WordDocumentType.Rtf;
		}

		if (!string.IsNullOrEmpty(TextBoxPassword.Text))
		{
			job.Password = TextBoxPassword.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadWordJob(FileUpLoad1.FileName, FileUpLoad1.PostedFile.ContentLength, job, FileUpLoad1.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);  
    }

	protected void ConvertExcelButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad2.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToExcelJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToExcelJobEnvelope();

		job.NonTableContent = cbContent.Checked;
		job.CombineTables = cbCombine.Checked;

		if (ddlThousands.SelectedIndex == 0)
		{
			job.ThousandsSeperator = PdfToWord.JobProcessorServiceReference.ThousandsSeparator.Comma;
		}
		else if (ddlThousands.SelectedIndex == 1)
		{
			job.ThousandsSeperator = PdfToWord.JobProcessorServiceReference.ThousandsSeparator.Period;
		}
		else
		{
			job.ThousandsSeperator = PdfToWord.JobProcessorServiceReference.ThousandsSeparator.Space;
		}

		if (ddlDecimal.SelectedIndex == 0)
		{
			job.DecimalSeperator = PdfToWord.JobProcessorServiceReference.DecimalSeparator.Comma;
		}
		else
		{
			job.DecimalSeperator = PdfToWord.JobProcessorServiceReference.DecimalSeparator.Period;
		}

		job.AutoDetectSeparators = cbAuto.Checked;

		int sel = ddlExcelOCR.SelectedIndex;
		if (sel == 0)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Automatic;
		}
		else if (sel == 1)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Automatic;
		}
		else
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Never;
		}

		if (!string.IsNullOrEmpty(TextBoxPwdExcel.Text))
		{
			job.Password = TextBoxPwdExcel.Text;
		}
        
		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadExcelJob(job, FileUpLoad2.FileName, FileUpLoad2.PostedFile.ContentLength, FileUpLoad2.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);       
	}

	protected void ConvertPPTXButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad3.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToPowerPointJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToPowerPointJobEnvelope();

		job.DetectLists = cbList.Checked;

		int sel = ddlPPTXOCR.SelectedIndex;
		if (sel == 0)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Automatic;
		}
		else if (sel == 1)
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Automatic;
		}
		else
		{
			job.TextRecovery = PdfToWord.JobProcessorServiceReference.TextRecovery.Never;
		}

		if (!string.IsNullOrEmpty(TextBoxPwdExcel.Text))
		{
			job.Password = TextBoxPwdPowerPoint.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadPowerPointJob(FileUpLoad3.FileName, FileUpLoad3.PostedFile.ContentLength, job, FileUpLoad3.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);  
	}

	protected void ConvertPdfAButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad4.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToPdfAJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToPdfAJobEnvelope();

		job.Searchable = cbSearchable.Checked;

		int sel = ddlPDFAMode.SelectedIndex;
		switch(sel)
		{
			case 0:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA1A;
				break;
			default:
			case 1:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA1B;
				break;
			case 2:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA2A;
				break;
			case 3:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA2B;
				break;
			case 4:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA2U;
				break;
			case 5:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA3A;
				break;
			case 6:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA3B;
				break;
			case 7:
				job.Mode = PdfToWord.JobProcessorServiceReference.ValidationMode.PdfA3U;
				break;

		}

		if (!string.IsNullOrEmpty(TextBoxPwdPdfA.Text))
		{
			job.Password = TextBoxPwdPdfA.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadPdfAJob(FileUpLoad4.FileName, FileUpLoad4.PostedFile.ContentLength, job, FileUpLoad4.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);
	}

	protected void ConvertTextButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad5.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToTextJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToTextJobEnvelope();

		string strLineLength = tbLength.Text;
		int iLineLenght;
		if (int.TryParse(strLineLength, out iLineLenght))
		{
			job.LineLength = iLineLenght;
		}
		else
		{
			job.LineLength = 100;
		}

		if (rbWindows.Checked)
		{
			job.Terminator = PdfToWord.JobProcessorServiceReference.LineTerminator.Windows;
		}
		else
		{
			job.Terminator = PdfToWord.JobProcessorServiceReference.LineTerminator.OSX;
		}

		System.Web.UI.WebControls.ListItem item = ddlEncoding.SelectedItem;
		int iEncoding;
		if (int.TryParse(item.Value, out iEncoding))
		{
			job.CharacterEncoding = iEncoding;
		}
		else
		{
			job.CharacterEncoding = 1252;
		}

		if (!string.IsNullOrEmpty(TextBoxPwdText.Text))
		{
			job.Password = TextBoxPwdText.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadTextJob(FileUpLoad5.FileName, FileUpLoad5.PostedFile.ContentLength, job, FileUpLoad5.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);
	}

	protected void ConvertDataButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad6.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToDataJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToDataJobEnvelope();

		int sel = ddlDataDelimiter.SelectedIndex;
		switch (sel)
		{
			default:
			case 0:
				job.Delimiter = PdfToWord.JobProcessorServiceReference.DelimiterOptions.Comma;
				break;
			case 1:
				job.Delimiter = PdfToWord.JobProcessorServiceReference.DelimiterOptions.Semicolon;
				break;
			case 2:
				job.Delimiter = PdfToWord.JobProcessorServiceReference.DelimiterOptions.Tab;
				break;
		}

		if (rbDataTermWin.Checked)
		{
			job.Terminator = PdfToWord.JobProcessorServiceReference.LineTerminator.Windows;
		}
		else
		{
			job.Terminator = PdfToWord.JobProcessorServiceReference.LineTerminator.OSX;
		}

		System.Web.UI.WebControls.ListItem item = ddlDataEncoding.SelectedItem;
		int iEncoding;
		if (int.TryParse(item.Value, out iEncoding))
		{
			job.CharacterEncoding = iEncoding;
		}
		else
		{
			job.CharacterEncoding = 1252;
		}

		if (!string.IsNullOrEmpty(TextBoxPwdText.Text))
		{
			job.Password = TextBoxPwdData.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadDataJob(job, FileUpLoad6.FileName, FileUpLoad6.PostedFile.ContentLength, FileUpLoad6.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);
	}

	protected void ConvertHtmlButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad7.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToHtmlJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToHtmlJobEnvelope();
		if (rbHtmlIncludeImages.Checked)
		{
			job.Images = PdfToWord.JobProcessorServiceReference.HtmlImages.Embed;
		}
		else
		{
			job.Images = PdfToWord.JobProcessorServiceReference.HtmlImages.Ignore;
		}

		if (rbHtmlSingleNoNav.Checked)
		{
			job.Navigation = PdfToWord.JobProcessorServiceReference.HtmlNavigation.SingleWithoutNavigation;
		}
		else if (rbHtmlSingleNav.Checked)
		{
			job.Navigation = PdfToWord.JobProcessorServiceReference.HtmlNavigation.SingleWithEmbeddedNavigation;
		}
		else
		{
			job.Navigation = PdfToWord.JobProcessorServiceReference.HtmlNavigation.SplitIntoMultipleFiles;
		}

		if (rbHtmlSingleNav.Checked)
		{
			int selList = rbHtmlSplitList.SelectedIndex;
			switch(selList)
			{
				default:
				case 0:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Pages;
					break;
				case 1:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Bookmarks;
					break;
				case 2:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Headings;
					break;
			}
		} 
		else if (rbHtmlMultiFile.Checked)
		{
			int selList = rbHtmlSplitMultiList.SelectedIndex;
			switch (selList)
			{
				default:
				case 0:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Pages;
					break;
				case 1:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Bookmarks;
					break;
				case 2:
					job.SplitUsing = PdfToWord.JobProcessorServiceReference.HtmlSplittingUsing.Headings;
					break;
			}
		}

		int typeSel = ddlHtmlImageFormat.SelectedIndex;
		switch (typeSel)
		{
			default:
			case 0:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Default;
				break;
			case 1:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Gif;
				break;
			case 2:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Jpeg;
				break;
			case 3:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Png;
				break;
		}

		if (!cbHtmlImageWidth.Checked)
		{
			job.WidthLimit = 0;
		}
		else
		{
			int customWidth;
			if (int.TryParse(tbHtmlImageWidth.Text, out customWidth))
			{
				job.WidthLimit = customWidth;
			}
			else
			{
				job.WidthLimit = 320;
			}
		}

		if (!string.IsNullOrEmpty(TextBoxPwdText.Text))
		{
			job.Password = TextBoxPwdHtml.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadHtmlJob(FileUpLoad7.FileName, job, FileUpLoad7.PostedFile.ContentLength, FileUpLoad7.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);
	}

	protected void ConvertImagesButton_Click(object sender, EventArgs e)
	{
		bool convertTheFile = true;

		if (FileUpLoad8.HasFile)
		{
			// all is well
		}
		else
		{
			convertTheFile = false;
		}

		if (!convertTheFile)
		{
			return;
		}

		PdfToWord.JobProcessorServiceReference.PdfToImagesJobEnvelope job = new PdfToWord.JobProcessorServiceReference.PdfToImagesJobEnvelope();
		if (rbImagesExtract.Checked)
		{
			job.ConversionType = PdfToWord.JobProcessorServiceReference.ImageConversionType.ExtractImages;
		}
		else
		{
			job.ConversionType = PdfToWord.JobProcessorServiceReference.ImageConversionType.ExtractPages;
			int iSel = ddlImageDPI.SelectedIndex;
			switch (iSel)
			{
				default:
				case 0:
					job.PageDPI = 300;
					break;
				case 1:
					job.PageDPI = 150;
					break;
				case 2:
					job.PageDPI = 75;
					break;
				case 3:
					job.PageDPI = 50;
					break;
				case 4:
					job.PageDPI = 25;
					break;
			}
		}

		int imgSel = ddlImageFormat.SelectedIndex;
		switch(imgSel)
		{
			default:
			case 0:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Default;
				break;
			case 1:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Bmp;
				break;
			case 2:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Jpeg;
				break;
			case 3:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Png;
				break;
			case 4:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Tiff;
				break;
			case 5:
				job.ImageType = PdfToWord.JobProcessorServiceReference.ImageDocumentType.Gif;
				break;
		}

		if (!string.IsNullOrEmpty(TextBoxPwdText.Text))
		{
			job.Password = TextBoxPwdImages.Text;
		}

		PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient serviceClient = new PdfToWord.JobProcessorServiceReference.IjobProcessorServiceClient();
		int jobID = serviceClient.UploadImageJob(FileUpLoad8.FileName, job, FileUpLoad8.PostedFile.ContentLength, FileUpLoad8.PostedFile.InputStream);

		Response.Redirect("wait.aspx?JobId=" + jobID);
	}

	private DataSet LanguageList()
	{
		DataSet objDS = new DataSet();
		DataTable objTable = objDS.Tables.Add();

		objTable.Columns.Add("Language_ID", typeof(int));
		objTable.Columns.Add("Language_Name", typeof(string));

		foreach (EncodingInfo ei in Encoding.GetEncodings())
		{
			Encoding encoding = ei.GetEncoding();
			if (encoding.IsBrowserSave && encoding.IsBrowserDisplay && encoding.IsMailNewsDisplay && encoding.IsMailNewsSave)
			{
				objTable.Rows.Add(ei.CodePage, ei.DisplayName);
			}
		}

		return objDS;
	}

	protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			DataSet oDs = LanguageList(); 
			if (oDs != null && oDs.Tables != null) 
			{ 
				ddlEncoding.DataValueField = "Language_ID";
				ddlEncoding.DataTextField = "Language_Name"; 
				ddlEncoding.DataSource = oDs;
				ddlEncoding.DataBind();
				ddlEncoding.Items.FindByValue("1252").Selected = true;

				ddlDataEncoding.DataValueField = "Language_ID";
				ddlDataEncoding.DataTextField = "Language_Name"; 
				ddlDataEncoding.DataSource = oDs;
				ddlDataEncoding.DataBind();
				ddlDataEncoding.Items.FindByValue("1252").Selected = true;
			} 
		}

		// Remember what we had in the fileuploaders.
		if (rbHtmlSingleNoNav.Checked)
		{
			rbHtmlSplitList.Enabled = false;
			rbHtmlSplitMultiList.Enabled = false;
		}
		else if (rbHtmlSingleNav.Checked)
		{
			rbHtmlSplitList.Enabled = true;
			rbHtmlSplitMultiList.Enabled = false;
		}
		else
		{
			rbHtmlSplitList.Enabled = false;
			rbHtmlSplitMultiList.Enabled = true;
		}

		if (cbHtmlImageWidth.Checked)
		{
			tbHtmlImageWidth.Enabled = true;
		}
		else
		{
			tbHtmlImageWidth.Enabled = false;
		}

		if (rbImagesExtract.Checked)
		{
			ddlImageDPI.Enabled = false;
		}
		else
		{
			ddlImageDPI.Enabled = true;
		}
	}
}
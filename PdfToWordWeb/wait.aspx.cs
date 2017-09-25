using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PdfToWord
{
    public partial class WebForm1 : System.Web.UI.Page
    {
		protected int MyJobId { get; set; }
		
		protected void Page_Load(object sender, EventArgs e)
        {
			if (!string.IsNullOrEmpty(Request.QueryString["JobId"]))
			{
				MyJobId = int.Parse(Request.QueryString["JobId"]);
			}
			else
			{
				MyJobId = -1;
			}
        }
    }
}
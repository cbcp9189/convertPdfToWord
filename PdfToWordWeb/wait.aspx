<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wait.aspx.cs" Inherits="PdfToWord.WebForm1"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Converting file, please wait<span id="dots" style="color:red;font-weight:bold;"></span>
    </form>
</body>
	
    <script language="javascript">
	<!--
	    var pollInterval = 1000;
	    var nextPageUrl = "confirmation.aspx";
	    var checkStatusUrl = "checkStatus.aspx";
	    var req;

	    var myValue = "<%= MyJobId %>";

	    // this tells the wait page to check the status every so often
	    window.setInterval("checkStatus()", pollInterval);

	    // this is just for entertainment, not necessary for the code
	    window.setInterval("drawDots()", pollInterval);
	    function drawDots() {
	        var element = document.getElementById("dots");
	        dots.innerText = dots.innerText + ".";
	    }

	    function checkStatus() {
	        createRequester();

	        if (req != null) {
	            req.onreadystatechange = process;
	            req.open("GET", checkStatusUrl + "?JobId=" + myValue + '&_=' + new Date().getTime(), true);
	            req.send(null);
	        }
	    }

	    function process() {
	        if (req.readyState == 4) {
	            // only if "OK"
	            if (req.status == 200) {
	                if (req.responseText == "1") {
	                    // a "1" means it is done, so here is where you redirect
	                    // to the confirmation page
	                	document.location.replace(nextPageUrl + "?JobId=" + myValue);
	                }
	                // NOTE: any status other than 200 or any response other than
	                // "1" require no action
	            }
	        }
	    }

	    /*
	    Note that this tries several methods of creating the XmlHttpRequest object,
	    depending on the browser in use. Also note that as of this writing, the
	    Opera browser does not support the XmlHttpRequest.
	    */
	    function createRequester() {
	        try {
	            req = new ActiveXObject("Msxml2.XMLHTTP");
	        }
	        catch (e) {
	            try {
	                req = new ActiveXObject("Microsoft.XMLHTTP");
	            }
	            catch (oc) {
	                req = null;
	            }
	        }

	        if (!req && typeof XMLHttpRequest != "undefined") {
	            req = new XMLHttpRequest();
	        }

	        return req;
	    }
	//-->
	</script>
	
</html>

<%@ Page language="c#" Codebehind="confirmation.aspx.cs" AutoEventWireup="True" Inherits="wait.confirmation"%>
<!doctype html public "-//w3c//dtd html 4.0 transitional//en" >
<html>
	<head>
		<title>confirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
			<asp:HiddenField ID="JobID" runat="server"></asp:HiddenField>
            <p>
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Download Converted File" />
            </p>
			<p>
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Do Another Conversion" />
            </p>
		</form>
	</body>
</html>

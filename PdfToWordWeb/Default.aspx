<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="_Default"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "DTD/xhtml1-transitional.dtd">
<html><head><title>Convert PDF using Solid Framework Sample</title>

<style type="text/css">
	body {
		margin:10px 10px 0px 10px;
		padding:0px;
		font-family:Arial;
		font-size:10px
		}
	
	#leftcontent {
		position: absolute;
		left:10px;
		top:50px;
		width:500px;
		height:299px;
		background:#fff;
		border:1px solid #000;
		}

	#centercontent {
		background:#fff;
		position: absolute;
		left:62px;
   		margin-left: 199px;
   		margin-right:199px;
   		height:149px;
   		width:249px;
		border:1px solid #000;
		}


	#rightcontent {
		position: absolute;
		left:261px;
		top:200px;
		width:249px;
		height:100px;
		background:#fff;
		border:1px solid #000;
		}
	
	#banner {
		background:#fff;
		height:40px;
		width:500px;
		border-top:1px solid #000;
		border-right:1px solid #000;
		border-left:1px solid #000;
		}
	html>body #banner {
		height:39px;
		}
		
	p,h1,pre {
		margin:0px 10px 10px 10px;
		}
		
	h1 {
		font-size:14px;
		padding-top:10px;
		}
		
	#banner h1 {
		font-size:14px;
		padding:10px 10px 0px 10px;
		margin:0px;
		}
	
	#rightcontent p {
		
		}
	
</style>
    <link type="text/css" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.9/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.min.js"></script>
    <script type="text/javascript">
    	$(function() {
    		$("#myTabs").tabs({
    			show: function() {
    				var sel = $('#myTabs').tabs('option', 'selected');
    				$("#<%= hidLastTab.ClientID %>").val(sel);
    			},
    			selected: <%= hidLastTab.Value %>
    			});
    	});
    </script>
</head>
<body>
  <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager" runat="server" />
	<asp:HiddenField runat="server" ID="hidLastTab" Value="0" />
	        <div id="myTabs" style="width:500px;">
            <ul>
                <li><a href="#tab-1">Word</a></li>
                <li><a href="#tab-2">Excel</a></li>
                <li><a href="#tab-3">PowerPoint</a></li>
                <li><a href="#tab-4">PDF/A</a></li>
                <li><a href="#tab-5">Text</a></li>
                <li><a href="#tab-6">Data</a></li>
                <li><a href="#tab-7">Html</a></li>
				<li><a href="#tab-8">Images</a></li>
            </ul>
            <div id="tab-1">
					 <b>Select PDF to Convert:</b>
					 <br />
					 <asp:FileUpLoad id="FileUpLoad1" runat="server" />
					 <br />
					 <br />
					 <b>Select Reconstruction Mode:</b>
					 <br />
					 <asp:RadioButton id="flowing" Text="Flowing - recover page layout, columns, formatting, graphics and preserve text flow." Checked="True" GroupName="ReconstructionMode" runat="server"/>
					 <br />
					 <asp:RadioButton id="continous" Text="Continuous - detect layout and columns but only recover formatting, graphics and text flow." GroupName="ReconstructionMode" runat="server"/>
					 <br />
					 <asp:RadioButton id="exact" Text="Exact - recover exact page presentation using text boxes in Microsoft Word." GroupName="ReconstructionMode" runat="server"/>
					 <br />
					 <br />
					 <b>Image Recovery:</b>
					 <br />
					 <asp:RadioButton id="irauto" Text="Automatic Anchoring" Checked="True" GroupName="ImageRecovery" runat="server"/>
					 <br />
					 <asp:RadioButton id="irpara" Text="Anchor to Paragraph" GroupName="ImageRecovery" runat="server"/>
					 <br />
					 <asp:RadioButton id="irpage" Text="Anchor to Page" GroupName="ImageRecovery" runat="server"/>
					 <br />
					 <asp:RadioButton id="irremove" Text="Remove Images" GroupName="ImageRecovery" runat="server"/>
					 <br />
					 <br />
					<b>Table Detection:</b>
					 <br />
					 <asp:RadioButton id="tdon" Text="Detect Tables" Checked="True" GroupName="TableDetection" runat="server"/>
					 <br />
					 <asp:RadioButton id="tdoff" Text="Do Not Detect Tables" GroupName="TableDetection" runat="server"/>
					 <br />
					<br />
					<b>Headers and Footers:</b>
					 <br />
					 <asp:RadioButton id="hfrecover" Text="Recover as headers and/or footers" Checked="True" GroupName="HeadersAndFooters" runat="server"/>
					 <br />
					 <asp:RadioButton id="hfplace" Text="Place in the body of the document" GroupName="HeadersAndFooters" runat="server"/>
					 <br />
					 <asp:RadioButton id="hfremove" Text="Remove" GroupName="HeadersAndFooters" runat="server"/>
					 <br />
				<br />
				<table style="width:320px">
					<tr>
						<td><b>Optical Text Recovery:</b></td>
						<td>
							<asp:DropDownList ID="ddlOCR" runat="server" Width="75px">
								<asp:ListItem Text="Auto" Value="0"></asp:ListItem>
								<asp:ListItem Text="Always" Value="1"></asp:ListItem>
								<asp:ListItem Text="Never" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td><b>Output Document Type:</b></td>
						<td>
							<asp:DropDownList ID="ddlType" runat="server" Width="150px">
								<asp:ListItem Text="Word Document (*.docx)" Value="0"></asp:ListItem>
								<asp:ListItem Text="Rich Text (*.rtf)" Value="1"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td><b>Optional PDF Password:</b></td>
						<td>
							<asp:TextBox id="TextBoxPassword" columns="30" runat="server" />
						</td>
					</tr>
				</table>
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertWordButton" Text="Start Conversion"  OnClick="ConvertWordButton_Click" Runat="server" width="150"/>
				</center>
            </div>
            <div id="tab-2">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad2" runat="server" />
				<br />
				<br />
				<b>Convert Content:</b><br />
				<asp:CheckBox ID="cbContent" Checked="true" runat="server" Text="Convert and include non-table content" />
				<br />
				<br />
				<b>Combine Tables:</b><br />
				<asp:CheckBox ID="cbCombine" Checked="true" runat="server" Text="Combine tables that appear on more than one sheet" />
				<br />
				<br />
				<b>Numeric Reconstruction:</b><br />
				<table style="width:200px">
					<tr>
						<td align="right">Thousands seperator:</td>
						<td>
							<asp:DropDownList ID="ddlThousands" runat="server" Width="74px">
								<asp:ListItem Text="Comma" Value="0"></asp:ListItem>
								<asp:ListItem Text="Period" Value="1"></asp:ListItem>
								<asp:ListItem Text="Space" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td align="right">Decimal symbol:</td>
						<td>
							<asp:DropDownList ID="ddlDecimal" runat="server" Width="75px">
								<asp:ListItem Text="Comma" Value="0"></asp:ListItem>
								<asp:ListItem Text="Period" Value="1" Selected="True"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
				</table>
				<asp:CheckBox ID="cbAuto" Checked="true" runat="server" Text="Attempt automatic detection of numeric delimiters first" />
				<br />
				<br />
				<table style="width:320px">
					<tr>
						<td><b>Optical Text Recovery:</b></td>
						<td>
							<asp:DropDownList ID="ddlExcelOCR" runat="server" Width="75px">
								<asp:ListItem Text="Auto" Value="0"></asp:ListItem>
								<asp:ListItem Text="Always" Value="1"></asp:ListItem>
								<asp:ListItem Text="Never" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td><b>Optional PDF Password:</b></td>
						<td>
							<asp:TextBox id="TextBoxPwdExcel" columns="30" runat="server" />
						</td>
					</tr>
				</table>
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertExcelButton" Text="Start Conversion"  OnClick="ConvertExcelButton_Click" Runat="server" width="150"/>
				</center>
            </div>
            <div id="tab-3">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad3" runat="server" />
				<br />
				<br />
				<b>Convert Content:</b><br />
				<asp:CheckBox ID="cbList" Checked="true" runat="server" Text="Detect lists" />
				<br />
				<br />
				<table style="width:320px">
					<tr>
						<td><b>Optical Text Recovery:</b></td>
						<td>
							<asp:DropDownList ID="ddlPPTXOCR" runat="server" Width="75px">
								<asp:ListItem Text="Auto" Value="0"></asp:ListItem>
								<asp:ListItem Text="Always" Value="1"></asp:ListItem>
								<asp:ListItem Text="Never" Value="2"></asp:ListItem>
							</asp:DropDownList>
						</td>
					</tr>
					<tr>
						<td><b>Optional PDF Password:</b></td>
						<td>
							<asp:TextBox id="TextBoxPwdPowerPoint" columns="30" runat="server" />
						</td>
					</tr>
				</table>
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertPPTXButton" Text="Start Conversion"  OnClick="ConvertPPTXButton_Click" Runat="server" width="150"/>
				</center>
            </div>
			<div id="tab-4">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad4" runat="server" />
				<br />
				<br />
				<b>PDF Compliance:</b><br />
				PDF Standard:
				<asp:DropDownList ID="ddlPDFAMode" runat="server" Width="75px">
					<asp:ListItem Text="PDF/A-1a" Value="0"></asp:ListItem>
					<asp:ListItem Text="PDF/A-1b" Value="1" Selected="True"></asp:ListItem>
					<asp:ListItem Text="PDF/A-2a" Value="2"></asp:ListItem>
					<asp:ListItem Text="PDF/A-2b" Value="3"></asp:ListItem>
					<asp:ListItem Text="PDF/A-2u" Value="4"></asp:ListItem>
					<asp:ListItem Text="PDF/A-3a" Value="5"></asp:ListItem>
					<asp:ListItem Text="PDF/A-3b" Value="6"></asp:ListItem>
					<asp:ListItem Text="PDF/A-3u" Value="7"></asp:ListItem>
				</asp:DropDownList>
				<br />
				<br />
				<b>Searchable PDF:</b><br />
				<asp:CheckBox ID="cbSearchable" Checked="false" runat="server" Text="Add searchable text layer" />
				<br />
				<br />
				<b>Optional PDF Password:</b>
				<asp:TextBox id="TextBoxPwdPdfA" columns="30" runat="server" />
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertPdfAButton" Text="Start Conversion"  OnClick="ConvertPdfAButton_Click" Runat="server" width="150"/>
				</center>
            </div>
			<div id="tab-5">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad5" runat="server" />
				<br />
				<br />
				<b>Line Length:</b><br />
				<asp:TextBox ID="tbLength" Columns="6" runat="server" Text="100" TextMode="SingleLine" style="text-align: right" />
				characters
				<br />
				<br />
				<b>Line Terminator:</b><br />
				<asp:RadioButton id="rbWindows" Text="Windows (CR+LF)" Checked="True" GroupName="Terminator" runat="server"/>
				<br />
				<asp:RadioButton id="rbOSX" Text="Mac OSX (LF)" GroupName="Terminator" runat="server"/>
				<br />
				<br />
				<b>Encoding:</b><br />
				Character set:
				<asp:DropDownList ID="ddlEncoding" runat="server" Width="200px">
				</asp:DropDownList>
				<br />
				<br />
				<b>Optional PDF Password:</b>
				<asp:TextBox id="TextBoxPwdText" columns="30" runat="server" />
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertTextButton" Text="Start Conversion"  OnClick="ConvertTextButton_Click" Runat="server" width="150"/>
				</center>
            </div>
			<div id="tab-6">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad6" runat="server" />
				<br />
				<br />
				<b>Line Length:</b><br />
				<asp:DropDownList ID="ddlDataDelimiter" runat="server" Width="75px">
					<asp:ListItem Text="Comma" Value="0" Selected="True"></asp:ListItem>
					<asp:ListItem Text="Semicolon" Value="1"></asp:ListItem>
					<asp:ListItem Text="Tab" Value="2"></asp:ListItem>
				</asp:DropDownList>
				<br />
				<br />
				<b>Line Terminator:</b><br />
				<asp:RadioButton id="rbDataTermWin" Text="Windows (CR+LF)" Checked="True" GroupName="HtmlTerminator" runat="server"/>
				<br />
				<asp:RadioButton id="rbDataTermMac" Text="Mac OSX (LF)" GroupName="HtmlTerminator" runat="server"/>
				<br />
				<br />
				<b>Encoding:</b><br />
				Character set:
				<asp:DropDownList ID="ddlDataEncoding" runat="server" Width="200px">
				</asp:DropDownList>
				<br />
				<br />
				<b>Optional PDF Password:</b>
				<asp:TextBox id="TextBoxPwdData" columns="30" runat="server" />
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertDataButton" Text="Start Conversion"  OnClick="ConvertDataButton_Click" Runat="server" width="150"/>
				</center>
            </div>
			<div id="tab-7">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad7" runat="server" />
				<br />
				<br />
				<b>Image Handling:</b><br />
				<asp:RadioButton id="rbHtmlIncludeImages" Text="Include Images" Checked="True" GroupName="HtmlImages" runat="server"/>
				<br />
				<asp:RadioButton id="rbHtmlRemoveImages" Text="Remove images" GroupName="HtmlImages" runat="server"/>
				<br />
				<br />
				<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
				<ContentTemplate>
				<b>Navigation and Output:</b><br />
				<asp:RadioButton id="rbHtmlSingleNoNav" Text="Single file with no navigation" Checked="True" GroupName="HtmlStruct" runat="server" AutoPostBack="true"/>
				<br />
				<asp:RadioButton id="rbHtmlSingleNav" Text="Single file with navigation frame" GroupName="HtmlStruct" runat="server" AutoPostBack="true"/>
				<br />
				<asp:RadioButtonList ID="rbHtmlSplitList" runat="server" RepeatDirection="Vertical">
					<asp:ListItem Selected="True" style="margin:10px">Using pages</asp:ListItem>
					<asp:ListItem style="margin:10px">Using bookmarks from original document</asp:ListItem>
					<asp:ListItem style="margin:10px">Using detected headings</asp:ListItem>
				</asp:RadioButtonList>
				<asp:RadioButton id="rbHtmlMultiFile" Text="Split into multiple files" GroupName="HtmlStruct" runat="server" AutoPostBack="true"/>
				<br />
				<asp:RadioButtonList ID="rbHtmlSplitMultiList" runat="server" RepeatDirection="Vertical">
					<asp:ListItem Selected="True" style="margin:10px">Using pages</asp:ListItem>
					<asp:ListItem style="margin:10px">Using bookmarks from original document</asp:ListItem>
					<asp:ListItem style="margin:10px">Using detected headings</asp:ListItem>
				</asp:RadioButtonList>
				<br />
				<br />
				<b>Image Format:</b><br />
				Format:
				<asp:DropDownList ID="ddlHtmlImageFormat" runat="server" Width="100px">
					<asp:ListItem Text="Optimal" Value="0" Selected="True"></asp:ListItem>
					<asp:ListItem Text="GIF (*.gif)" Value="1"></asp:ListItem>
					<asp:ListItem Text="JPG (*.jpg)" Value="2"></asp:ListItem>
					<asp:ListItem Text="PNG (*.png)" Value="3"></asp:ListItem>
				</asp:DropDownList>
				<br />
				<br />
				<b>Image Width:</b><br />
				<asp:CheckBox ID="cbHtmlImageWidth" Checked="false" runat="server" Text="Set a limit on width" AutoPostBack="true" /><br />
				Width in pixels:
				<asp:TextBox id="tbHtmlImageWidth" columns="8" Text="320" runat="server" />
				</ContentTemplate>
				</asp:UpdatePanel>
				<br />
				<br />
				<b>Optional PDF Password:</b>
				<asp:TextBox id="TextBoxPwdHtml" columns="30" runat="server" />
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertHtmlButton" Text="Start Conversion"  OnClick="ConvertHtmlButton_Click" Runat="server" width="150"/>
				</center>
            </div>
			<div id="tab-8">
				<b>Select PDF to Convert:</b>
				<br />
				<asp:FileUpLoad id="FileUpLoad8" runat="server" />
				<br />
				<br />
				<b>Image Handling:</b><br />
				<asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
				<ContentTemplate>
				<asp:RadioButton id="rbImagesExtract" Text="Extract images from PDF pages" Checked="True" GroupName="ImageHandling" runat="server" AutoPostBack="true"/>
				<br />
				<asp:RadioButton id="rbImagesFromPDF" Text="Convert PDF pages to images" GroupName="ImageHandling" runat="server" AutoPostBack="true"/>
				<br />
				DPI of page image:
				<asp:DropDownList ID="ddlImageDPI" runat="server" Width="75px">
					<asp:ListItem Text="300" Value="0" Selected="True"></asp:ListItem>
					<asp:ListItem Text="150" Value="1"></asp:ListItem>
					<asp:ListItem Text="75" Value="2"></asp:ListItem>
					<asp:ListItem Text="50" Value="3"></asp:ListItem>
					<asp:ListItem Text="25" Value="4"></asp:ListItem>
				</asp:DropDownList>
				</ContentTemplate>
				</asp:UpdatePanel>
				<br />
				<br />
				<b>Image Format:</b><br />
				Format:
				<asp:DropDownList ID="ddlImageFormat" runat="server" Width="100px">
					<asp:ListItem Text="Optimal" Value="0" Selected="True"></asp:ListItem>
					<asp:ListItem Text="BMP (*.bmp)" Value="1"></asp:ListItem>
					<asp:ListItem Text="JPG (*.jpg)" Value="2"></asp:ListItem>
					<asp:ListItem Text="PNG (*.png)" Value="3"></asp:ListItem>
					<asp:ListItem Text="TIF (*.tif)" Value="4"></asp:ListItem>
					<asp:ListItem Text="GIF (*.gif)" Value="5"></asp:ListItem>
				</asp:DropDownList>
				<br />
				<br />
				<b>Optional PDF Password:</b>
				<asp:TextBox id="TextBoxPwdImages" columns="30" runat="server" />
				<p></p>
				<br />
				<center>
				<asp:Button ID="ConvertImagesButton" Text="Start Conversion"  OnClick="ConvertImagesButton_Click" Runat="server" width="150"/>
				</center>
            </div>
        </div>
	  </form>
</body>
</html>

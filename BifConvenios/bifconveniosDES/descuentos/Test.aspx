<%--<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>--%>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Test" CodeFile="Test.aspx.vb" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Test</title>
		<meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
		<%--<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
			<rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" Width="100%" Height="700px" ShowDocumentMapButton="False" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
              <%--  <LocalReport ReportPath="descuentos\repCartaCobranzaLight.rdlc">
                </LocalReport>--%>
				
			</rsweb:ReportViewer>
		<%--	MIGRAR INNOVA
			<CR:CrystalReportViewer id="CrystalReportViewer1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server" Width="350px" Height="50px" DisplayGroupTree="False" HasToggleGroupTreeButton="False"></CR:CrystalReportViewer>--%>
		</form>
	</body>
</html>

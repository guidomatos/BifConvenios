<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContainerDocumentoCobranza.aspx.vb" Inherits="BIFConvenios.consultas_ContainerDocumentoCobranza" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
   <title>BIFConvenios - Documentos de Cobranza</title>
		<meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
		<%--<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">
		<BASE TARGET="_self">
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />--%>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" ProcessingMode="Local" Width="100%" Height="700px" ShowDocumentMapButton="false" DocumentMapCollapsed="true">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContainerDocumentoCobranza.aspx.vb" Inherits="descuentos_ContainerDocumentoCobranza" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
    <title>BIFConvenios - Documentos de Cobranza</title>
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<BASE TARGET="_self">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" Width="100%" Height="700px" ShowDocumentMapButton="false" DocumentMapCollapsed="true">
        </rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>

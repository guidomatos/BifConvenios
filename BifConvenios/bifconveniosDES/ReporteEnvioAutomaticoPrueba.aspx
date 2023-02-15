<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteEnvioAutomaticoPrueba.aspx.vb" Inherits="ReporteEnvioAutomaticoPrueba" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
		<title>BIFConvenios - Listado Envio Automatico</title>
		<meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>

    <form id="form1" runat="server">

    <div>
        <%--<CR:CrystalReportViewer ID="crReporteAutomatico" runat="server" AutoDataBind="true" />--%>    
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
		<rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" Width="100%" Height="700px" ShowDocumentMapButton="False" DocumentMapCollapsed="True" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226">
            <LocalReport ReportPath="RepListadoEnvioAutomatico.rdlc">
            </LocalReport>
        </rsweb:ReportViewer>
    </div>
    
    </form>
</body>
</html>
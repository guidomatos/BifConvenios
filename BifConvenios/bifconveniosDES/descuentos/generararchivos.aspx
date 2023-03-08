<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.generarArchivos" CodeFile="generarArchivos.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Archivo Generado</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td colSpan="2"><uc1:banner id="Banner1" title="Detalle del proceso del archivo de Cuotas - Archivo Generado" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td width="3">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td align="middle" colSpan="2" class="Caption">&nbsp;
						<asp:Literal id="ltrlEnlace" runat="server"></asp:Literal>&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td align="left">
						<a href="JavaScript:window.history.back();">Regresar </a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

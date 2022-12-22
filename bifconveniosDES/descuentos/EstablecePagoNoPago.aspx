<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstablecePagoNoPago.aspx.vb" Inherits="BIFConvenios.EstablecePagoNoPago" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src ="~/controls/banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Elegir Créditos para pagar</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Elegir Créditos para pagar" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">&nbsp;</TD>
							</TR>
							<tr>
							<TD width="30">&nbsp;</TD>
								<td>
									<asp:DataGrid ID="dgPagoNoPago"  runat ="server" AutoGenerateColumns="False" CellPadding=5 DataKeyField="DLNP">
										<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" ></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField ="DLNP" HeaderText ="# Pagaré"></asp:BoundColumn>
											<asp:BoundColumn DataField="DLNE" HeaderText="Empleado"></asp:BoundColumn>
											<asp:TemplateColumn HeaderText="Pagar?">
											<ItemTemplate>
												<asp:LinkButton Runat="server"  CommandName="Select" Text='<%#"<img src=../images/" + GetImage(DataBinder.Eval(Container.DataItem, "Pago")) + " border=0/>" %>'></asp:LinkButton> 
 											</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
									</asp:DataGrid>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
				<tr>
					<td><br>
						<br>
					</td>
				</tr>
				<tr>
					<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:linkbutton id="lnkRegresar" runat="server" ><IMG id="btnRetornar" title="Cancelar" alt="Cancelar" src="../images/regresar.jpg" border="0" name="Image12"></asp:linkbutton></TD>
				</tr>
			</table>
		</form>
	</body>
</html>

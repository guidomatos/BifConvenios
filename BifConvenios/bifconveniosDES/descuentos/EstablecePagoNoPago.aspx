<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EstablecePagoNoPago.aspx.vb" Inherits="BIFConvenios.EstablecePagoNoPago" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src ="~/controls/banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Elegir Créditos para pagar</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:Banner id="Banner1" title="Elegir Créditos para pagar" runat="server"></uc1:Banner></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="750" border="0">
							<tr>
								<td width="30"></td>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td width="30">&nbsp;</td>
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
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
						<br>
					</td>
				</tr>
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:LinkButton id="lnkRegresar" runat="server">
                            <img src="<%= ResolveUrl("~/images/regresar.jpg") %>" name='Image1' border="0" alt='Regresar' />
                        </asp:LinkButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteProcesoDescuentosDetalle" CodeFile="reporteProcesoDescuentosDetalle.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Detalle del proceso del archivo de Cuotas</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
	</head>
	<body leftmargin="0" topmargin="0" onload="MM_preloadImages('<%=ResolveUrl("~/images/regresar_on.jpg") %>')">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td>
						<uc1:Banner id="Banner1" runat="server" Title="Detalle del proceso del archivo de Cuotas"></uc1:Banner></td>
				</tr>
				<tr>
					<td>
						<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="30">&nbsp;</td>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<table cellSpacing="0" class="InputField" cellPadding="0" width="750" border="0">
										<tr>
											<td width="30">&nbsp;</td>
											<td colSpan="2">
												<table cellSpacing="4" cellPadding="0" width="100%" border="0">
													<tr>
														<td width="120" class="SubHead">Empresa</td>
														<td class="Normal"><asp:Literal id="ltrlCliente" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td width="120" class="SubHead">Documento</td>
														<td class="Normal"><asp:Literal id="ltrlDocumento" runat="server"></asp:Literal></td>
													</tr>
													<!--<TR>
														<TD width="120" class="SubHead">Fecha de Proceso</TD>
														<TD class="Normal"><asp:literal id="ltrlFechaProceso" runat="server"></asp:literal></TD>
													</TR>-->
													<tr>
														<td width="120" class="SubHead">Estado</td>
														<td class="Normal"><asp:Literal id="ltrlEstado" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td width="120" class="SubHead">Periodo</td>
														<td class="Normal"><asp:Literal id="ltrlPeriodo" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td width="120" class="SubHead">Fecha de Carga</td>
														<td class="Normal"><asp:Literal id="ltrlProcesoAS400" runat="server"></asp:Literal></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<div id="dvDia" class="DRCuerpoNormal" style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 250px" width="100%">
										<div class="tabla">
											<asp:DataGrid id="dgProcesoResult" runat="server" Visible="False" AutoGenerateColumns="False" Width="1200" BorderWidth="1px" CellPadding="3" CellSpacing="3" AllowSorting="True" AllowPaging="True" PageSize="500">
												<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
												<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
												<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
												<Columns>
													<asp:BoundColumn DataField="Convenio" HeaderText="Código Convenio">
														<ItemStyle Width="90px"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="DLNP" HeaderText="Código Pagaré/Prestamo"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLNE" HeaderText="Nombre del Trabajador" ItemStyle-Width="200"></asp:BoundColumn>
													<asp:BoundColumn DataField="NombreStatusTrabajador" HeaderText="Estado&#160;Trabajador">
														<HeaderStyle Width="15px"></HeaderStyle>
														<ItemStyle Width="15px"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="DLMO" HeaderText="Moneda"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLIC" HeaderText="Importe Cuota" DataFormatString="{0:#.00}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="DLID" HeaderText="Importe Descontado" DataFormatString="{0:#.00}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="DeudaPeriodo" HeaderText="Deuda Periodo" DataFormatString="{0:#.00}">
														<ItemStyle HorizontalAlign="Right"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Estado" HeaderText="Estado&#160;Registro" ItemStyle-Width="220"></asp:BoundColumn>
												</Columns>
												<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
											</asp:DataGrid>
										</div>
									</div>
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="SubHead">Total de Registros&nbsp;&nbsp;
												<asp:Label CssClass="Text" id="lblTotalReg" Runat="server"></asp:Label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="30">&nbsp;</td>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<asp:LinkButton id="lnkBack" Runat="server"><img src="<%= ResolveUrl("~/images/regresar.jpg") %>" name='Image1' border="0" alt='Regresar' /></asp:LinkButton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="ConsultaDetallePagosIBS" CodeFile="ConsultaDetallePagosIBS.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Detalle de Pagos Procesados en IBS</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<BASE TARGET="_self">
		<script language="javascript">
			function Cerrar(){
				window.close();
			}
		</script>
	</HEAD>
	<body topmargin="5" leftmargin="15">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellspacing="0" cellpadding="0" width="100%">
				<tr>
					<td class="SubHead" align="center">
						Pagos en IBS desde&nbsp;<asp:Label Runat="server" ID="lblFechaDesde" CssClass="Normal"></asp:Label>&nbsp;hasta&nbsp;<asp:Label Runat="server" ID="lblFechaHasta" CssClass="Normal"></asp:Label>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td class="SubHead" valign="bottom" width="20">
									Pagaré&nbsp;
								</td>
								<td align="left">
									<asp:Label Runat="server" ID="lblNumeroPagare" CssClass="Normal"></asp:Label>
								</td>
								<td>&nbsp;</td>
								<td class="SubHead" valign="bottom" width="20">
									Trabajador&nbsp;&nbsp;
								</td>
								<td align="left" colspan="3">
									<asp:Label Runat="server" CssClass="Normal" ID="lblNombreTrabajador"></asp:Label>
								</td>
							</tr>
							<tr>
								<td class="Subhead" width="80">Importe BIF Informado</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lblCuotaMes" Runat="server"></asp:Label></td>
								<td>&nbsp;</td>
								<td class="Subhead" width="150">Importe Descontado</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lblimporteDescontado" Runat="server"></asp:Label></td>
								<td class="Subhead" width="150">Deuda del periodo</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lbldeudaPeriodo" Runat="server"></asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td align="center" class="CommandButton">
						<a href="javascript:Cerrar();">Cerrar</a>
					</td>
				</tr>
				<tr>
					<td class="Subhead">Detalle Proceso IBS
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:DataGrid id="dgData" runat="server" CellPadding="4" BorderWidth="1px" Width="740" AutoGenerateColumns="False" Visible="False">
							<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Fecha">
									<ItemTemplate>
										<%#BIFConvenios.Utils.GetFechaCanonica(DataBinder.Eval ( Container.DataItem, "Fecha"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="DLDSC" HeaderText="Descripción"></asp:BoundColumn>
								<asp:BoundColumn DataField="Motivo" HeaderText="Motivo"></asp:BoundColumn>
								<asp:BoundColumn DataField="DLCCY" HeaderText="Mon" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#.00}"></asp:BoundColumn>
								<asp:BoundColumn DataField="DLCIC" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#.00}"></asp:BoundColumn>
								<asp:BoundColumn DataField="TipoCuota" HeaderText="Tipo de Importe"></asp:BoundColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td align="right" class="SubHead">
									Total Procesado IBS
								</td>
								<td align="left" Class="Normal">&nbsp;
									<%=Request.Params("mon")%>
									&nbsp;<%=Request.Params("amount")%>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td align="center" class="CommandButton">
						<a href="javascript:Cerrar();">Cerrar</a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

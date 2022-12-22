<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.DetalleConsultaEnvioPorTrabajador" CodeFile="DetalleConsultaEnvioPorTrabajador.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Reporte de pagos por trabajador</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Detalle de pagos realizados" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
							<TBODY>
								<TR>
									<TD width="30">&nbsp;</TD>
									<TD colSpan="2">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td class="SubHead">Cliente</td>
												<td width="40">&nbsp;</td>
												<td class="Normal"><asp:Label Runat="server" ID="lblNombreCliente" cssclass="Normal"></asp:Label></td>
											</tr>
										</table>
									</TD>
								</TR>
								<tr>
									<td colspan="3">&nbsp;</td>
								</tr>
								<TR>
									<TD width="30">&nbsp;</TD>
									<TD colSpan="2">
										<div class="tabla">
											<asp:DataGrid runat="server" ID="dgPagosRealizados" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3" CellSpacing="3" Width="600">
												<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
												<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
												<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
												<Columns>
													<asp:BoundColumn DataField="DLAP" HeaderText="A&#241;o" HeaderStyle-Width="30"></asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Mes" HeaderStyle-Width="80">
														<ItemTemplate>
															<asp:Label runat="server" Text='<%# BIFConvenios.Periodo.GetMonthByNumber(DataBinder.Eval(Container, "DataItem.DLMP")) %>'>
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="DLMo" HeaderText="Moneda" HeaderStyle-Width="40"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLIC" HeaderText="Importe BIF Informado" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLID" HeaderText="importe de Institución" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="80"></asp:BoundColumn>
													<asp:BoundColumn DataField="DeudaPeriodo" HeaderText="Deuda Periodo" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="85"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLESD" HeaderText="Estado Documento"></asp:BoundColumn>
													<asp:BoundColumn DataField="EnvioAS" HeaderText="Estado pago"></asp:BoundColumn>
												</Columns>
											</asp:DataGrid>
										</div>
									</TD>
								</TR>
								<tr>
									<td colspan="2" class="Subhead" align="right">&nbsp;
									</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
									<td colspan="2" class="Subhead" align="left">
										<a href="#" onclick="Javascript:history.back()">Regresar</a>
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

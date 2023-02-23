<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="DetalleConsultaEnvio" CodeFile="DetalleConsultaEnvio.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Detalle del proceso del archivo de importes</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="/css/global.css" type="text/css" rel="Stylesheet">
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
	</HEAD> 
	<body leftmargin="0" topmargin="0" onload="MM_preloadImages('/BIFConvenios/images/regresar_on.jpg')">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td>
						<uc1:Banner id="Banner1" runat="server" Title="Detalle del proceso del archivo de importes"></uc1:Banner></td>
				</tr>
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="30">&nbsp;</TD>
								<TD colSpan="2">&nbsp;</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<TABLE cellSpacing="0" class="InputField" cellPadding="0" width="750" border="0">
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2">
												<table cellSpacing="4" cellPadding="0" width="100%" border="0">
													<TR>
														<TD width="120" class="SubHead">Empresa</TD>
														<TD class="Normal"><asp:literal id="ltrlCliente" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD width="120" class="SubHead">Documento</TD>
														<TD class="Normal"><asp:literal id="ltrlDocumento" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD width="120" class="SubHead">Fecha de Proceso</TD>
														<TD class="Normal"><asp:literal id="ltrlFechaProceso" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD width="120" class="SubHead">Estado</TD>
														<TD class="Normal"><asp:literal id="ltrlEstado" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD width="120" class="SubHead">Periodo</TD>
														<TD class="Normal"><asp:literal id="ltrlPeriodo" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD width="120" class="SubHead">Fecha de Carga</TD>
														<TD class="Normal"><asp:literal id="ltrlProcesoAS400" runat="server"></asp:literal></TD>
													</TR>
												</table>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">&nbsp;</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<DIV id="dvDia" class="DRCuerpoNormal" style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 250px" width="100%">
										<div class="tabla">
											<asp:datagrid id="dgProcesoResult" runat="server" Visible="False" AutoGenerateColumns="False" Width="1200" BorderWidth="1px" CellPadding="3" CellSpacing="3" AllowPaging="True" PageSize="500">
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
											</asp:datagrid>
										</div>
									</DIV>
								</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">&nbsp;&nbsp;
								</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="SubHead">Total de Registros&nbsp;&nbsp;
												<asp:label CssClass="Text" id="lblTotalReg" Runat="server"></asp:label></td>
										</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD width="30">&nbsp;</TD>
								<TD colSpan="2">&nbsp;</TD>
							</TR>
							<tr>
								<TD width="30"></TD>
								<TD colSpan="2"><asp:linkbutton id="lnkBack" Runat="server"><img src="../images/regresar.jpg" name='Image1' border="0" alt='Regresar' /></asp:linkbutton></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

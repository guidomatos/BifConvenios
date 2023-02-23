<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="ConsultaEstadoEnvios" CodeFile="ConsultaEstadoEnvios.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Consultar Envíos por Empresa/Periodo</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/global.css" type="text/css" rel="Stylesheet">
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
	</HEAD>
	<body topmargin="0" leftmargin="0" bottommargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Consultar Estado de Envíos por Periodo" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<TABLE class="InputField" height="100" cellSpacing="0" cellPadding="0" border="0" width="750">
										<TR>
											<TD width="70">&nbsp;</TD>
											<TD width="80">&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD class="SubHead" width="50">Año</TD>
											<TD align="left" width="650">
												<asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" Width="200px" DataValueField="Anio_Periodo" DataTextField="Anio_Periodo"></asp:DropDownList></TD>
										</TR>
										<TR>
											<TD>&nbsp;</TD>
											<TD class="SubHead">Mes</TD>
											<TD>
												<asp:DropDownList id="ddlMes" runat="server" AutoPostBack="True" Width="200px" DataValueField="MonthOrder" DataTextField="MonthName"></asp:DropDownList></TD>
										</TR>
										<TR>
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD colSpan="3">&nbsp;</TD>
							</TR>
							<TR>
								<TD align="middle" width="30">&nbsp;</TD>
								<TD align="left" colSpan="2"><!--<asp:BoundColumn DataField="Nombre_Cliente" HeaderText="Nombre del Cliente" ></asp:BoundColumn> -->
									<DIV id="dvDia" class="DRCuerpoNormal" style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 250px">
										<div class="tabla">
											<asp:DataGrid id="dgProcesos" runat="server" Width="750px" CellPadding="3" CellSpacing="3" BorderWidth="1px" AutoGenerateColumns="False">
												<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="middle" HorizontalAlign="Center"></ItemStyle>
												<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="middle" HorizontalAlign="Center"></HeaderStyle>
												<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
												<Columns>
													<asp:BoundColumn DataField="Codigo_clienteIBS" HeaderText="C&#243;digo IBS" ItemStyle-HorizontalAlign="Center">
														<HeaderStyle Width="20px"></HeaderStyle>
														<ItemStyle Width="20px"></ItemStyle>
													</asp:BoundColumn>													
													<asp:BoundColumn DataField="Nombre_empresa" HeaderText="Nombre Empresa">
														<HeaderStyle Width="450px"></HeaderStyle>
														<ItemStyle Width="450px"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="Anio_periodo" HeaderText="A&#241;o" ItemStyle-HorizontalAlign="Center">
														<HeaderStyle Width="25px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Mes" ItemStyle-HorizontalAlign="Center">
														<ItemTemplate>
															<asp:Label runat="server" Text='<%# BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval(Container, "DataItem.Mes_periodo")) %>' ID="Label1">
															</asp:Label>															
														</ItemTemplate>
														<HeaderStyle Width="25px"></HeaderStyle >
													</asp:TemplateColumn>	
													 											 
													<asp:BoundColumn DataField="Fecha_envio_recepcion" HeaderText="Fecha de Retorno" ItemStyle-HorizontalAlign="Center">
													    <HeaderStyle Width="50px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="procesados" HeaderText="Total Procesados" ItemStyle-HorizontalAlign="Center">
													    <HeaderStyle Width="25px"></HeaderStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="rechazados" HeaderText="Total Rechazados" ItemStyle-HorizontalAlign="Center">
													    <HeaderStyle Width="25px"></HeaderStyle>														
													</asp:BoundColumn>
													<asp:BoundColumn DataField="recibidos" HeaderText="Total Recibidos" ItemStyle-HorizontalAlign="Center">
													    <HeaderStyle Width="25px"></HeaderStyle>														 
													</asp:BoundColumn>													 													 
													
													<asp:HyperLinkColumn DataNavigateUrlField="Codigo_proceso"   Text="Detalle" DataNavigateUrlFormatString="ConsultaEstadoEnviosDetalle.aspx?id={0}&accion=detalle"   HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
													    <HeaderStyle Width="25px"></HeaderStyle>
																									
													</asp:HyperLinkColumn>
												</Columns>
											</asp:DataGrid>
										</div>
									</DIV>
								</TD>
							</TR>
							<TR>
								<TD colSpan="3">&nbsp;</TD>
							</TR>
							<TR>
								<TD>&nbsp;</TD>
								<TD colSpan="2">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD class="SubHead" width="150">Número de Registros
											</TD>
											<TD class="Text">
												<asp:Label id="lblNumReg" Runat="server"></asp:Label></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
			</TD></TR></TABLE>
		</form>
	</body>
</HTML>

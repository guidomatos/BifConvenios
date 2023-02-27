<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ConsultaEnvioPorTrabajador" CodeFile="ConsultaEnvioPorTrabajador.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Consulta de Envio por Empleado</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../css/global.css" type="text/css" rel="Stylesheet">
		<script language=javascript  type=text/javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td><uc1:banner id="Banner1" title="Consulta de envió por Empleado" runat="server"></uc1:banner></td>
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
											<TABLE class="InputField" height="100" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD width="70">&nbsp;</TD>
													<TD width="80">&nbsp;</TD>
													<TD>&nbsp;</TD>
												</TR>
												<TR>
													<TD width="30">&nbsp;</TD>
													<td colspan="2">
														<table border="0" cellpadding="2" cellspacing="0" width="90%">
															<tr>
																<TD class="SubHead" width="50">Empresa</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="650" colspan="5">
																	<asp:DropDownList id="ddlEmpresa" runat="server" DataTextField="Nombre_Cliente" DataValueField="Codigo_Cliente" Width="200px" AutoPostBack="True"></asp:DropDownList>
																</TD>
															</tr>
															<TR>
																<TD class="SubHead" width="50">Año</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="170">
																	<asp:DropDownList id="ddlAnio" runat="server" DataTextField="Anio_Periodo" DataValueField="Anio_Periodo" Width="200px" AutoPostBack="True"></asp:DropDownList></TD>
																<TD width="30">&nbsp;</TD>
																<TD class="SubHead">Mes</TD>
																<TD width="30">&nbsp;</TD>
																<TD>
																	<asp:DropDownList id="ddlMes" runat="server" DataTextField="MonthName" DataValueField="MonthOrder" Width="200px" AutoPostBack="True"></asp:DropDownList></TD>
															</TR>
															<tr>
																<TD class="SubHead" width="50">Nombre del Empleado</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="650" colspan="5">
																	<asp:TextBox Runat="server" MaxLength="75" ID="txtTrabajador" Width="495px"></asp:TextBox>
																</TD>
															</tr>
															<tr>
																<td colspan="7" align="right" height="30" valign="center" class="SubHead">
																	<asp:LinkButton Runat="server" ID="lnkBuscar">Buscar</asp:LinkButton>
																</td>
															</tr>
														</table>
													</td>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD colSpan="3">&nbsp;</TD>
									</TR>
									<TR>
										<TD align="middle" width="30">&nbsp;</TD>
										<TD align="left" colSpan="2">
											<div class="tabla">
												<asp:DataGrid id="dgProcesos" runat="server" Width="720px" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="3" CellSpacing="3">
													<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
													<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
													<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
													<Columns>
														<asp:HyperLinkColumn DataNavigateUrlFormatString="DetalleConsultaEnvioPorTrabajador.aspx?DLNE={0}" DataNavigateUrlField="DLNE" DataTextField="DLNE" HeaderText="Nombre del Empleado"></asp:HyperLinkColumn>
													</Columns>
												</asp:DataGrid>
											</div>
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
														<asp:Label id="lblNumReg" Runat="server">0</asp:Label></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
								</TBODY>
							</TABLE>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>

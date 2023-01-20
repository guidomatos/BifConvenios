<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteProcesoDescuentosResumen" CodeFile="reporteProcesoDescuentosResumen.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Resumen de resultados del proceso</title>
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
	<!--onload="MM_preloadImages('/BIFConvenios/images/regresar_on.jpg');"-->
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Resumen de resultados del proceso del Archivo de Cuotas" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td>
						<table height="515" cellSpacing="0" cellPadding="20" width="650" border="0">
							<tr>
								<td vAlign="top" valign="center"><!-- background="/BIFConvenios/images/hoja1.jpg"-->
									<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2">&nbsp;</TD>
										</TR>
										<tr>
											<td width="30">&nbsp;</td>
											<td colSpan="2">
												<table cellSpacing="2" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="SubHead" width="120">Cliente</TD>
														<TD class="Normal"><asp:literal id="ltrlCliente" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD class="SubHead" width="120">Documento</TD>
														<TD class="Normal"><asp:literal id="ltrlDocumento" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD class="SubHead" width="120">Proceso de Archivo</TD>
														<TD class="Normal"><asp:literal id="ltrlFechaProceso" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD class="SubHead" width="120">Estado</TD>
														<TD class="Normal"><asp:literal id="ltrlEstado" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD class="SubHead" width="120">Periodo</TD>
														<TD class="Normal"><asp:literal id="ltrlPeriodo" runat="server"></asp:literal></TD>
													</TR>
													<TR>
														<TD class="SubHead" width="200">Fecha de Carga</TD>
														<TD class="Normal"><asp:literal id="ltrlProcesoAS400" runat="server"></asp:literal></TD>
													</TR>
												</table>
											</td>
										</tr>
										<tr>
											<td colspan="3">&nbsp;</td>
										</tr>
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD width="80%">
												<hr>
											</TD>
											<td>&nbsp;</td>
										</TR>
										<tr>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2">
												<table cellSpacing="2" cellPadding="0" border="0" runat="server" id="tblnfo" visible="true">
													<tr>
														<td class="SubHead" style="WIDTH: 300px" width="213" colSpan="2">Total de Registros 
															enviados a la empresa
														</td>
														<td class="Normal">
															<asp:hyperlink id="hpTotal" Runat="server" NavigateUrl="reporteProcesoDescuentosDetalle.aspx"></asp:hyperlink>
														</td>
													</tr>
													<tr>
														<td class="Normal">
															&nbsp;&nbsp;&nbsp;Con error</td>
														<td width="20">&nbsp;</td>
														<td class="Normal"><asp:hyperlink id="hlErrorenvio" Runat="server"></asp:hyperlink></td>
													</tr>
													<tr>
														<td class="Normal">&nbsp;&nbsp;&nbsp;Válidos</td>
														<td width="20">&nbsp;</td>
														<td class="Normal"><asp:hyperlink id="hpValidos" Runat="server" NavigateUrl="reporteProcesoDescuentosCat.aspx"></asp:hyperlink></td>
													</tr>
													<tr>
														<td colspan="3">&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="3">
															<hr>
														</td>
													</tr>
													<tr>
														<td class="SubHead" width="213" colSpan="2" style="WIDTH: 300px" >Total Registros Recibidos de la empresa
														</td>
														<td class="normal"><asp:hyperlink id="hlTotal" Runat="server"></asp:hyperlink></td>
													</tr>
													<tr>
														<td class="Normal">&nbsp;&nbsp;&nbsp;Con error&nbsp;de 
															proceso&nbsp;&nbsp;(información completa procesada con problemas)</td>
														<td width="20">&nbsp;</td>
														<td class="Normal"><asp:hyperlink id="hpErrores" Runat="server"></asp:hyperlink></td>
													</tr>
													<tr>
														<td class="Normal">&nbsp;&nbsp;&nbsp;Con error en 
															archivo&nbsp;&nbsp;&nbsp;(información incompleta o no encontrada)</td>
														<td width="20">&nbsp;</td>
														<td class="Normal" valign="top"><asp:hyperlink id="hlErrorArchivo" NavigateUrl="reporteArchivo.aspx" Runat="server"></asp:hyperlink></td>
													</tr>
													<tr>
														<td class="Normal">&nbsp;&nbsp;&nbsp;Válidos</td>
														<td width="20">&nbsp;</td>
														<td class="Normal"><asp:hyperlink id="hlValidos" Runat="server" NavigateUrl="reporteProcesoDescuentosCat.aspx"></asp:hyperlink></td>
													</tr>
													<tr>
														<td colSpan="3">
															<hr>
														</td>
													</tr>
												</table>
											</TD>
										</tr>
										<tr>
											<td colspan="3">&nbsp;</td>
										</tr>
										<TR>
											<TD width="30">&nbsp;</TD>
											<!--<IMG id="btnRetornar" title="Cancelar" alt="Cancelar" src="/BIFConvenios/images/regresar.jpg" border="0" name="Image12"></a>-->
											<TD colSpan="2">
												<table border="0" cellpadding="2" cellspacing="0" class="box">
													<thead>
														<tr>
															<th>
																<img src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17"></th>
															<th>
																<asp:linkbutton id="lnkRegresar" runat="server">Regresar</asp:linkbutton></th>
															<th>
																<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18"></th>
															<th>
																<a href="reporteSeguimiento.aspx">Ir a seguimiento</a></th>
															<th>
																<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18"></th>
															<th>
																<a href="processfile.aspx?id=<%=Request.params("id")%>">Cargar archivo de cuotas</a></th>
															<th>
																<img src="<%=Request.ApplicationPath%>/images/bar_end.gif" width="17" height="18"></th>
														</tr>
													</thead>
												</table>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		</FORM>
	</body>
</HTML>

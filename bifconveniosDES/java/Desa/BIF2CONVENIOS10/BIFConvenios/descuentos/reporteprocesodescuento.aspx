<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteProcesoDescuento" CodeFile="reporteProcesoDescuento.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Enviar Cobranza a IBS</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
		<script language="javascript">
	<!--
		function Procesar ( id , nombre, anio, mes, fechaProcesoAS400) {
			if ( confirm ( '¿Desea enviar la informacion de pagos a IBS de la Empresa ' + nombre + '\npara el periodo ' + mes + '-' + anio + ', procesado el ' + fechaProcesoAS400 +  ' en IBS?') ) {
				document.all('hdIdEnvio').value = id;
				__doPostBack('lnkEnviar', '');			
			}
		}
		
		function AnularProceso( id , nombre, anio, mes, fechaProcesoAS400) {
			if ( confirm ( '¿Desea anular el proceso del archivo de cuotas de la Empresa ' + nombre + '\npara el periodo ' + mes + '-' + anio + ', procesado el ' + fechaProcesoAS400 +  ' en IBS?\nEsto eliminará tambien la información que ha sido enviada a IBS.') ) {
				document.all('hdIdAnulacionProceso').value = id;
				__doPostBack('lnkAnularProceso', '');			
			}
		}
	
	//-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Enviar Cobranza a IBS" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td><asp:panel id="pnlControl" Visible="true" Runat="server">
							<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
								<TR>
									<TD width="30"></TD>
									<TD colSpan="2">&nbsp;</TD>
								</TR>
								<TR>
									<TD>&nbsp;</TD>
									<TD colSpan="2">
										<TABLE class="InputField" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD width="70">&nbsp;</TD>
												<TD width="80">&nbsp;</TD>
												<TD>&nbsp;</TD>
											</TR>
											<TR>
												<TD width="30"></TD>
												<TD class="SubHead">Año</TD>
												<TD width="650">
													<asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" DataTextField="Anio_periodo" DataValueField="Anio_periodo" Width="200px"></asp:DropDownList></TD>
											</TR>
											<TR>
												<TD>&nbsp;</TD>
												<TD class="SubHead">Mes</TD>
												<TD width="650">
													<asp:DropDownList id="ddlMes" runat="server" AutoPostBack="True" DataTextField="MonthName" DataValueField="MonthOrder" Width="200px"></asp:DropDownList></TD>
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
									<TD></TD>
									<TD colSpan="2">
										<div class="tabla">
											<asp:DataGrid id="dgDatos" runat="server" Width="100%" BorderWidth="1" AutoGenerateColumns="False" CellPadding="3" CellSpacing="3">
												<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
												<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
												<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
												<Columns>
													<asp:TemplateColumn HeaderText="C&#243;digo Empresa" HeaderStyle-Width="60" ItemStyle-Width="60">
														<ItemTemplate>
															<a href='EstablecePagoNoPago.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Codigo_proceso")%>' >
																<%#DataBinder.Eval(Container.DataItem, "Codigo_Cliente")%>
															</a>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Nombre Empresa" HeaderStyle-Width="162">
														<ItemTemplate>
															<a href='reporteProcesoDescuentosResumen.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Codigo_proceso")%>' >
																<%#DataBinder.Eval(Container.DataItem, "Nombre_Cliente")%>
															</a>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Periodo" ItemStyle-Width="62" HeaderStyle-Width="62">
														<ItemTemplate>
															<asp:Label runat="server" Text='<%#  BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval(Container, "DataItem.Mes_Periodo")) + "/" +  DataBinder.Eval(Container, "DataItem.Anio_periodo")  %>' ID="Label1">
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:BoundColumn DataField="Numreg" HeaderText="#Reg" ItemStyle-Width="35" HeaderStyle-Width="35"></asp:BoundColumn>
													<asp:BoundColumn DataField="CodigoNombre" HeaderText="Estado" ItemStyle-Width="60" HeaderStyle-Width="60"></asp:BoundColumn>
													<asp:BoundColumn DataField="Fecha_CargaAS400" HeaderText="Fecha Proceso Archivo" ItemStyle-Width="69" HeaderStyle-Width="69"></asp:BoundColumn>
													<asp:TemplateColumn HeaderText="Proceso" ItemStyle-Width="100">
														<ItemTemplate>
															<asp:Panel Runat=server ID=pnlInfo Visible='<%#DataBinder.Eval(Container.DataItem,"CanProcess")%>'>
																<a href='ResumenProceso.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Codigo_proceso")%>'>
																	<%#DataBinder.Eval(Container.DataItem, "MensajeEnvio")%>
																</a>
															</asp:Panel>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn ItemStyle-Width="100" Visible="False">
														<ItemTemplate>
															<%#MostrarEnlace ( DataBinder.Eval(Container.DataItem, "Codigo_proceso"), DataBinder.Eval(Container.DataItem, "CanProcess") , DataBinder.Eval(Container.DataItem, "Nombre_Cliente"), DataBinder.Eval ( Container.DataItem, "Anio_periodo"), BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval(Container, "DataItem.Mes_Periodo")), DataBinder.Eval(Container.DataItem, "MensajeEnvio") , DataBinder.Eval(Container.DataItem, "Fecha_ProcesoAS400") )%>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn ItemStyle-Width="100">
														<ItemTemplate>
															<%#MostrarEnlaceAnulacionProceso ( DataBinder.Eval(Container.DataItem, "Codigo_proceso"), True , DataBinder.Eval(Container.DataItem, "Nombre_Cliente"), DataBinder.Eval ( Container.DataItem, "Anio_periodo"), BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval(Container, "DataItem.Mes_Periodo")), DataBinder.Eval(Container.DataItem, "Fecha_ProcesoAS400") )%>
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:DataGrid>
											<!--'DataBinder.Eval(Container.DataItem, "CanProcess")-->
										</div>
									</TD>
								</TR>
								<TR>
									<TD colSpan="3">&nbsp;</TD>
								</TR>
								<TR>
									<TD>&nbsp;</TD>
									<TD colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD colspan="2">
													<span class="SubHead">Número de Registros&nbsp;:&nbsp;</span>
													<asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label></TD>
											</TR>
											<tr>
												<td colspan="2" class="NormalLite">Click en el código de la Empresa para establecer 
													los pagos y no pagos</td>
											</tr>
											<tr>
												<td colspan="2" class="NormalLite">Click en el nombre de la Empresa para ver los 
													detalles del proceso</td>
											</tr>
										</TABLE>
										<asp:LinkButton id="lnkEnviar" Runat="server"></asp:LinkButton>
										<INPUT id="hdIdEnvio" type="hidden" runat="server">
										<asp:LinkButton id="lnkAnularProceso" Runat="server"></asp:LinkButton>
										<INPUT id="hdIdAnulacionProceso" type="hidden" name="hdIdAnulacionProceso" runat="server">
									</TD>
								</TR>
								<TR>
									<TD>&nbsp;</TD>
									<TD colSpan="2">
										<asp:Label id="lblMensajeError" Runat="server" ForeColor="red"></asp:Label></TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="pnlMensaje" Visible="False" Runat="server"><BR>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:Label id="lblMensaje" Runat="server" CssClass="SubHead"></asp:Label>
      <SCRIPT language="javascript">
							<!--
							//procedimiento para mostrar la ventana de espera del proceso
								openPage('EsperaFinalEnvioAS400.aspx?id=<%=idProcess%>', 300, 390);							
							-->
							</SCRIPT>
      </asp:panel>
						<asp:Panel ID="pnlAnulacionProcesoDescuentos" Runat="server" Visible="False"><BR>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:Label id="lblMensajeAnulacion" Runat="server" CssClass="SubHead"></asp:Label>
      <SCRIPT language="javascript">
								<!--
									//procedimiento para mostrar la ventana de espera del proceso
									openPage('EsperaFinalAnulacion.aspx?id=<%=idProcess%>', 300, 390);							
								-->
							</SCRIPT>
						</asp:Panel></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

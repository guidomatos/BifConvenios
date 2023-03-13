<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteProcesoDescuento" CodeFile="reporteProcesoDescuento.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Enviar Cobranza a IBS</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language="javascript" type="text/javascript"></script>
		<script type="text/javascript" language="javascript">
			function Procesar(id, nombre, anio, mes, fechaProcesoAS400) {
				if (confirm('¿Desea enviar la informacion de pagos a IBS de la Empresa ' + nombre + '\npara el periodo ' + mes + '-' + anio + ', procesado el ' + fechaProcesoAS400 + ' en IBS?')) {
					document.all('hdIdEnvio').value = id;
					__doPostBack('lnkEnviar', '');
				}
			}
			function AnularProceso(id, nombre, anio, mes, fechaProcesoAS400) {
				if (confirm('¿Desea anular el proceso del archivo de cuotas de la Empresa ' + nombre + '\npara el periodo ' + mes + '-' + anio + ', procesado el ' + fechaProcesoAS400 + ' en IBS?\nEsto eliminará tambien la información que ha sido enviada a IBS.')) {
					document.all('hdIdAnulacionProceso').value = id;
					__doPostBack('lnkAnularProceso', '');
				}
			}
		</script>
	</head>
	<body leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:Banner id="Banner1" title="Enviar Cobranza a IBS" runat="server"></uc1:Banner></td>
				</tr>
				<tr>
					<td><asp:Panel id="pnlControl" Visible="true" Runat="server">
							<table cellSpacing="0" cellPadding="0" width="750" border="0">
								<tr>
									<td width="30"></td>
									<td colSpan="2">&nbsp;</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
									<td colSpan="2">
										<table class="InputField" height="100" cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td width="70">&nbsp;</td>
												<td width="80">&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
											<tr>
												<td width="30"></td>
												<td class="SubHead">Año</td>
												<td width="650">
													<asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" DataTextField="Anio_periodo" DataValueField="Anio_periodo" Width="200px"></asp:DropDownList></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td class="SubHead">Mes</td>
												<td width="650">
													<asp:DropDownList id="ddlMes" runat="server" AutoPostBack="True" DataTextField="MonthName" DataValueField="MonthOrder" Width="200px"></asp:DropDownList></td>
											</tr>
											<tr>
												<td>&nbsp;</td>
												<td>&nbsp;</td>
												<td>&nbsp;</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td colSpan="3">&nbsp;</td>
								</tr>
								<tr>
									<td></td>
									<td colSpan="2">
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
									</td>
								</tr>
								<tr>
									<td colSpan="3">&nbsp;</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
									<td colSpan="2">
										<table cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td colspan="2">
													<span class="SubHead">Número de Registros&nbsp;:&nbsp;</span>
													<asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label>
												</td>
											</tr>
											<tr>
												<td colspan="2" class="NormalLite">Click en el código de la Empresa para establecer 
													los pagos y no pagos</td>
											</tr>
											<tr>
												<td colspan="2" class="NormalLite">Click en el nombre de la Empresa para ver los 
													detalles del proceso</td>
											</tr>
										</table>
										<asp:LinkButton id="lnkEnviar" Runat="server"></asp:LinkButton>
										<input id="hdIdEnvio" type="hidden" runat="server" />
										<asp:LinkButton id="lnkAnularProceso" Runat="server"></asp:LinkButton>
										<input id="hdIdAnulacionProceso" type="hidden" name="hdIdAnulacionProceso" runat="server" />
									</td>
								</tr>
								<tr>
									<td>&nbsp;</td>
									<td colSpan="2">
										<asp:Label id="lblMensajeError" Runat="server" ForeColor="red"></asp:Label>
									</td>
								</tr>
							</table>
						</asp:Panel>
						<asp:Panel id="pnlMensaje" Visible="False" Runat="server"><br>&nbsp;&nbsp;&nbsp;&nbsp; 
							<asp:Label id="lblMensaje" runat="server" CssClass="SubHead"></asp:Label>
							<script type="text/javascript" language="javascript">
							<!--
							//procedimiento para mostrar la ventana de espera del proceso
								openPage('EsperaFinalEnvioAS400.aspx?id=<%=idProcess%>', 300, 390);							
							-->
							</script>
						</asp:Panel>
						<asp:Panel ID="pnlAnulacionProcesoDescuentos" Runat="server" Visible="False"><br>&nbsp;&nbsp;&nbsp;&nbsp; 
							<asp:Label id="lblMensajeAnulacion" Runat="server" CssClass="SubHead"></asp:Label>
							<script type="text/javascript" language="javascript">
								<!--
									//procedimiento para mostrar la ventana de espera del proceso
									openPage('EsperaFinalAnulacion.aspx?id=<%=idProcess%>', 300, 390);							
								-->
							</script>
						</asp:Panel>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

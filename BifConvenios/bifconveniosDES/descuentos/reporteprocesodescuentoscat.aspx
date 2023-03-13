<%@ Reference Control="~/controls/banner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteProcesoDescuentosCat" CodeFile="reporteProcesoDescuentosCat.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Detalle del proceso del archivo de cuotas</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
		<script type="text/javascript" language="javascript">

			function generarArchivo() {
				location.href = "generarArchivos.aspx?id=<%=Request.Params("id")%>&type=<%=Request.Params("type")%>";
			}
			function Send() {
				var controls = '<%=BIFConvenios.Utils.GetControlNames(dgProcesoResultErr, "chk")%>';
				if (controls == '') {
					return;
				}
				var a = controls.split(',');
				var i = 0;
				var anyChecked = false;
				for (i = 0; i <= a.length - 1; i++) {
					if (document.all(a[i]).checked) {
						anyChecked = true;
					}
				}

				if (!anyChecked) {
					alert('Debe seleccionar por lo menos un prestamo.');
				}
				else {
					if (confirm('¿Desea establecer el monto del importe descontado en 0 en los prestamos seleccionados?')) {
						__doPostBack('lnkEnviaPrestamos', '');
					}
				}
			}
	
			/**
			* Función para checkar todos los controles
			**/
			function checkall(bln) {
				var controls = '<%=BIFConvenios.Utils.GetControlNames(dgProcesoResultErr, "chk")%>';
				var a = controls.split(',');
				var i = 0;
				for (i = 0; i <= a.length - 1; i++) {
					document.all(a[i]).checked = bln;
				}
			}

		</script>
	</head>
	<body leftMargin="0" topMargin="0" onload="MM_preloadImages('<%=ResolveUrl("~/images/regresar_on.jpg")%>')" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Detalle del proceso del archivo de Cuotas" runat="server"></uc1:banner></td>
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
									<table class="InputField" cellSpacing="0" cellPadding="0" width="750" border="0">
										<tr>
											<td width="30">&nbsp;</td>
											<td colSpan="2">
												<table cellSpacing="4" cellPadding="0" width="100%" border="0">
													<tr>
														<td class="SubHead" width="120">Empresa</td>
														<td class="Normal"><asp:Literal id="ltrlCliente" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td class="SubHead" width="120">Documento</td>
														<td class="Normal"><asp:Literal id="ltrlDocumento" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td class="SubHead" width="120">Estado</td>
														<td class="Normal"><asp:Literal id="ltrlEstado" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td class="SubHead" width="120">Periodo</td>
														<td class="Normal"><asp:Literal id="ltrlPeriodo" runat="server"></asp:Literal></td>
													</tr>
													<tr>
														<td class="SubHead" width="120">Fecha de Carga</td>
														<td class="Normal"><asp:Literal id="ltrlFechaProceso" runat="server"></asp:Literal></td>
													</tr>
													<!--<TR>
														<TD class="SubHead" width="120">Fecha de Proceso IBS</TD>
														<TD class="Normal"><asp:literal id="ltrlProcesoAS400" runat="server"></asp:literal></TD>
													</TR>-->
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<asp:Panel id="pnlCuota" CssClass="Normal" runat="server" Visible="False"><BR>
										<a href="JavaScript:Send();">Establecer cuota a Cero</a>&nbsp;|&nbsp;<a href="JavaScript:generarArchivo();">Generar 
											Archivo</a>
										<br>
										<asp:LinkButton id="lnkEnviaPrestamos" Runat="server"></asp:LinkButton>
									</asp:Panel>
									&nbsp;
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">
									<!--<DIV class="DRCuerpoNormal" id="dvDia" style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 250px" width="100%">-->
									<div class="tabla">
										<asp:DataGrid id="dgProcesoResultVA" runat="server" AutoGenerateColumns="False" Width="1000" BorderWidth="1px" CellPadding="3" CellSpacing="3" Visible="False" AllowSorting="True" AllowPaging="True" PageSize="500">
											<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
											<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Código Convenio">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Convenio") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>' >
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Código Pagaré/Prestamo">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DLNP") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Nombre del Trabajador" ItemStyle-Width="200">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DLNE") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Estado&nbsp;Trabajador">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NombreStatusTrabajador") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Moneda">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DLMO") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Importe Cuota">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DLIC", "{0:#.00}") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Importe Descontado">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DLID", "{0:#.00}") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Deuda Periodo">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DeudaPeriodo", "{0:#.00}") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="Estado&#160;Registro">
													<ItemTemplate>
														<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Estado") %>' ForeColor= '<%#GetColor ( DataBinder.Eval(Container.DataItem, "Alert"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
											<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
										<asp:DataGrid id="dgProcesoResultErr" runat="server" AutoGenerateColumns="False" Width="780" BorderWidth="1px" CellPadding="3" CellSpacing="3" Visible="False" AllowSorting="True" AllowPaging="True" PageSize="500">
											<ItemStyle Height="27px" CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
											<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="<input type=checkbox id=chkall name=chkall onclick='javascript:checkall(this.checked);'>">
													<ItemTemplate>
														<asp:CheckBox ID="chk" Runat="server"></asp:CheckBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Convenio" HeaderText="C&#243;digo Convenio">
													<ItemStyle Width="90px"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DLNP" HeaderText="C&#243;digo Pagaré/Prestamo" ItemStyle-Width="100"></asp:BoundColumn>
												<asp:TemplateColumn HeaderText="Nombre Trabajador" ItemStyle-Width="300px" HeaderStyle-Width="300px">
													<ItemTemplate>
														<a href='EdicionNoEncontrados.aspx?id=<%#DataBinder.Eval(Container.DataItem, "DLNP")%>&pid=<%=Request.Params("id")%>'>
															<%#DataBinder.Eval(Container.DataItem, "DLNE")%>
														</a>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="NombreStatusTrabajador" HeaderText="Estado&#160;<br>Trabajador"></asp:BoundColumn>
												<asp:BoundColumn DataField="DLMO" HeaderText="Mon"></asp:BoundColumn>
												<asp:BoundColumn DataField="DLIC" HeaderText="Importe Cuota" DataFormatString="{0:#.00}">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DLID" HeaderText="Importe Descontado" DataFormatString="{0:#.00}" Visible="false">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="DeudaPeriodo" HeaderText="Deuda Periodo" DataFormatString="{0:#.00}" Visible="False">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Estado" HeaderText="Estado&#160;Registro">
													<HeaderStyle Width="130px"></HeaderStyle>
													<ItemStyle Width="130px"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
										</asp:DataGrid>
										<asp:DataGrid id="dgProcesoResultErrTabla" runat="server" AutoGenerateColumns="False" Width="1000" BorderWidth="1px" CellPadding="3" CellSpacing="3" Visible="False">
											<ItemStyle Height="27px" CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
											<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
											<Columns>
												<asp:BoundColumn DataField="DLNP" HeaderText="N&#250;mero de Pagaré"></asp:BoundColumn>
												<asp:BoundColumn DataField="CodigoModular" HeaderText="Código Modular"></asp:BoundColumn>
												<asp:HyperLinkColumn DataNavigateUrlField="codigo" DataNavigateUrlFormatString="EdicionErroresProceso.aspx?id={0}" DataTextField="DLNE" HeaderText="Nombre Empleado" ItemStyle-Width="300"></asp:HyperLinkColumn>
												<asp:BoundColumn DataField="Estado" HeaderText="Estado Registro" ItemStyle-Width="250"></asp:BoundColumn>
												<asp:BoundColumn DataField="NombreStatus" HeaderText="Estado Trabajador"></asp:BoundColumn>
												<asp:BoundColumn DataField="Moneda" ReadOnly="True" HeaderText="Moneda"></asp:BoundColumn>
												<asp:BoundColumn DataField="Cuota" HeaderText="Cuota" DataFormatString="{0:#.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
												<asp:BoundColumn DataField="MontoDescuento" HeaderText="Monto Descuento" DataFormatString="{0:#.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
											</Columns>
										</asp:DataGrid>
									</div>
									<!--</DIV>-->
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
												<asp:label id="lblTotalReg" CssClass="Text" runat="server"></asp:label></td>
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
								<td colSpan="2"><asp:LinkButton id="lnkBack" runat="server"><img src='<%=ResolveUrl("~/images/regresar.jpg") %>' name='Image1' border="0" alt='Procesar archivo' /></asp:LinkButton></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

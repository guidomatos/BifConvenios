<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteArchivo" CodeFile="reporteArchivo.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Errores en archivo</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<link href="<%=ResolveUrl("~/css/tabs.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language="javascript" type="text/javascript"></script>

		<script type="text/javascript" language="javascript">
	
			function ShowHide(obj) {
				if (document.all(obj).className == 'show') {
					document.all(obj).className = 'hide';
				}
				else {
					document.all(obj).className = 'show';
				}
			}
			function show(obj) {
				document.all(obj).className = 'show';
			}
			function hide(obj) {
				document.all(obj).className = 'hide';
			}
			function showObject(id) {
				if (id == 'dvNoProcesada') {
					show(id);
					hide('dvNoProcesadaArchivo');
					document.all("uno").className = 'on';
					document.all("dos").className = '';
					document.all("uno").title = "seleccionado";
					document.all("dos").title = "";
					document.all("uno").innerHTML = "<a href=\"javascript:showObject('dvNoProcesada');\"><STRONG><EM>Vista Legible</EM></strong></A>";
					document.all("dos").innerHTML = "<A href= \"javascript:showObject('dvNoProcesadaArchivo');\"><EM>Vista de Archivo</EM></A> ";

				}
				if (id == 'dvNoProcesadaArchivo') {
					show(id);
					hide('dvNoProcesada');
					document.all("uno").className = '';
					document.all("dos").className = 'on';
					document.all("uno").title = "";
					document.all("dos").title = "seleccionado";
					document.all("uno").innerHTML = "<a href=\"javascript:showObject('dvNoProcesada');\"><EM>Vista Legible</EM></A>";
					document.all("dos").innerHTML = "<A href= \"javascript:showObject('dvNoProcesadaArchivo');\"><STRONG><EM>Vista de Archivo</EM></strong></A> ";

				}
			}
		</script>
	</head>
	<body onload="MM_preloadImages('<%=ResolveUrl("~/images/regresar_on.jpg")%>')" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Registros no procesados de archivos" runat="server"></uc1:banner></td>
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
														<td class="SubHead" width="120">Proceso de Archivo</td>
														<td class="Normal"><asp:Literal id="ltrlFechaProceso" runat="server"></asp:Literal></td>
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
														<td class="SubHead" width="120">Fecha de Proceso IBS</td>
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
								<td colSpan="2">&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colspan="2" align="left">
									<asp:LinkButton id="lnkDownload" Runat="server">Descargar registros no procesados</asp:LinkButton>
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td width="30">&nbsp;</td>
								<td colspan="2">
									<!----Tabs visuales------>
									<div class="navset" id="nav">
										<div class="hd">
											<ul>
												<li id="uno" name="uno" class="on">
													<a href="javascript:showObject('dvNoProcesada');"><strong><em>Vista Legible</em></strong></a>
												</li>
												<li id="dos" name="dos">
													<a href="javascript:showObject('dvNoProcesadaArchivo');"><em>Vista de Archivo</em></a>
												</li>
											</ul>
										</div>
										<div class="show" id="dvNoProcesada" name="dvNoProcesada">
											<div class="bd">
												<UL>
													<asp:DataGrid Runat="server" ID="dgInformacionNoProcesada" CellPadding="3" CellSpacing="3">
														<ItemStyle VerticalAlign="Top"></ItemStyle>
														<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
														<HeaderStyle Height="30px" CssClass="head" VerticalAlign="Top"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<ItemTemplate>
																	<a href="IngresaDatosArchivoCarga.aspx?id=<%=Request.Params("id")%>&pro=<%#DataBinder.Eval(Container.DataItem, "Id Proceso")%>&lin=<%#DataBinder.Eval(Container.DataItem, "Linea")%>">Agregar</a>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:DataGrid>
												</UL>
											</div>
										</div>
										<div class="hide" id="dvNoProcesadaArchivo" name="dvNoProcesadaArchivo">
											<div class="bd">
												<ul>
													<br/>
													<asp:datagrid id="dgData" Runat="server" AutoGenerateColumns="False">
														<ItemStyle Font-Name="Courier" VerticalAlign="Top"></ItemStyle>
														<HeaderStyle Height="30px" CssClass="head" VerticalAlign="Top"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderText="Registros no Procesados">
																<ItemTemplate>
																	<pre><%#DataBinder.Eval(Container.DataItem, "lineainformacion")%></pre>
																</ItemTemplate>
															</asp:TemplateColumn>
														</Columns>
													</asp:datagrid>
												</ul>
											</div>
										</div>
									</div>
									<!------fin de tabs ----->
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2">&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colSpan="2" align="left">
									<asp:LinkButton id="lnkBack" Runat="server">
										<img src="<%= ResolveUrl("~/images/regresar.jpg") %>" name='Image1' border="0" alt='Regresar' /></asp:LinkButton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

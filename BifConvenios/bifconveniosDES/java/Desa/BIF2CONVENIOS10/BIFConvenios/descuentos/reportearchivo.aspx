<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.reporteArchivo" CodeFile="reporteArchivo.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Errores en archivo</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<LINK href="<%=Request.ApplicationPath%>/css/tabs.css" 
type=text/css rel=stylesheet>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
		<script language="javascript">
	<!--
		function ShowHide(obj){
			if(document.all(obj).className=='show'){
				document.all(obj).className='hide';
			}
			else{
				document.all(obj).className='show';
			}
		}
		
		function show (obj){
				document.all(obj).className='show';
		} 
		function hide (obj){
				document.all(obj).className='hide';
		} 
		
		function showObject(id){
			if ( id =='dvNoProcesada'){
				show(id);
				hide('dvNoProcesadaArchivo');
				document.all("uno").className = 'on';
				document.all("dos").className = '';
				document.all("uno").title="seleccionado";
				document.all("dos").title="";
				document.all("uno").innerHTML = "<a href=\"javascript:showObject('dvNoProcesada');\"><STRONG><EM>Vista Legible</EM></strong></A>";
				document.all("dos").innerHTML = "<A href= \"javascript:showObject('dvNoProcesadaArchivo');\"><EM>Vista de Archivo</EM></A> ";
				
			}
			if ( id =='dvNoProcesadaArchivo'){
				show(id);
				hide('dvNoProcesada');
				document.all("uno").className = '';
				document.all("dos").className = 'on';
				document.all("uno").title="";
				document.all("dos").title="seleccionado";
				document.all("uno").innerHTML = "<a href=\"javascript:showObject('dvNoProcesada');\"><EM>Vista Legible</EM></A>";
				document.all("dos").innerHTML = "<A href= \"javascript:showObject('dvNoProcesadaArchivo');\"><STRONG><EM>Vista de Archivo</EM></strong></A> ";
				
			}
		}
		
	-->
		</script>
	</HEAD>
	<body onload="MM_preloadImages('/BIFConvenios/images/regresar_on.jpg')" topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Registros no procesados de archivos" runat="server"></uc1:banner></td>
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
									<TABLE class="InputField" cellSpacing="0" cellPadding="0" width="750" border="0">
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2">
												<table cellSpacing="4" cellPadding="0" width="100%" border="0">
													<TR>
														<TD class="SubHead" width="120">Empresa</TD>
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
														<TD class="SubHead" width="120">Fecha de Proceso IBS</TD>
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
								<TD colSpan="2">&nbsp;&nbsp;
								</TD>
							</TR>
							<tr>
								<TD width="30"></TD>
								<td colspan="2" align="left">
									<asp:linkbutton id="lnkDownload" Runat="server">Descargar registros no procesados</asp:linkbutton>
								</td>
							</tr>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">&nbsp;&nbsp;
								</TD>
							</TR>
							<tr>
								<TD width="30">&nbsp;</TD>
								<td colspan="2">
									<!----Tabs visuales------>
									<DIV class="navset" id="nav">
										<DIV class="hd">
											<UL>
												<LI id="uno" name="uno" class="on">
													<a href="javascript:showObject('dvNoProcesada');"><STRONG><EM>Vista Legible</EM></STRONG></a>
												</LI>
												<LI id="dos" name="dos">
													<A href="javascript:showObject('dvNoProcesadaArchivo');"><EM>Vista de Archivo</EM></A>
												</LI>
											</UL>
										</DIV>
										<DIV class="show" id="dvNoProcesada" name="dvNoProcesada">
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
										</DIV>
										<DIV class="hide" id="dvNoProcesadaArchivo" name="dvNoProcesadaArchivo">
											<div class="bd">
												<UL>
													<br>
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
												</UL>
											</div>
										</DIV>
									</DIV>
									<!------fin de tabs ----->
								</td>
							</tr>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">&nbsp;&nbsp;
								</TD>
							</TR>
							<tr>
								<TD width="30"></TD>
								<TD colSpan="2" align="left">
									<asp:linkbutton id="lnkBack" Runat="server">
										<img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Procesar archivo' /></asp:linkbutton>
								</TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

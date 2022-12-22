<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="ConsultaEstadoEnviosDetalle" CodeFile="ConsultaEstadoEnviosDetalle.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Detalle del proceso del archivo de importes</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
<script language=javascript >
function printDiv(divID) {
        var divToPrint = document.getElementById(divID);
    newWin= window.open();
    newWin.document.write(divToPrint.innerHTML);
    newWin.location.reload();
    newWin.focus();
    newWin.print();
    newWin.close();
        
    
    }
</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="MM_preloadImages('/BIFConvenios/images/regresar_on.jpg')">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
				
					<td>
						<uc1:Banner id="Banner1" runat="server" Title="Detalle del proceso de envíos de archivos"></uc1:Banner></td>
				</tr>
				<tr>
				    
					<td>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="750" border="0">
							<TR>								 
								<TD width="30">&nbsp;</TD>
								<TD colSpan="2">&nbsp;</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<TABLE cellSpacing="0" class="InputField" cellPadding="0" width="720" border="0">
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2" width="720">
												<table cellSpacing="4" cellPadding="0" width="750" border="0">
													<TR>
														<TD width="120" class="SubHead">Empresa</TD>
														<TD class="Normal"><asp:literal id="ltrlCliente" runat="server"></asp:literal></TD>
													</TR>
													 
													<TR>
														<TD width="120" class="SubHead">Periodo</TD>
														<TD class="Normal"><asp:literal id="ltrlperiodo" runat="server"></asp:literal></TD>
													</TR>
													 
													<TR>
														<TD width="120" class="SubHead">Fecha de Retorno</TD>
														<TD class="Normal"><asp:literal id="ltrlfechaRetorno" runat="server"></asp:literal></TD>
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
							<tr >
								<TD width="30">&nbsp;</TD>
								<TD colSpan="2" align="right" width="750" >									
									<table border="0" cellpadding="2" cellspacing="0" class="box" >
										<thead>
											<tr >												
												<th>
													<img alt ="Exportar Excel" src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17"></th>
												<th>
													<a href='ConsultaEstadoEnviosDetalle.aspx?accion=exportar'>Exportar Excel</a>
												</th>
												<th>
													<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18">
												</th>
												<th>
													<a alt ="Imprimir" href="javascript:printDiv('tablaDetalle')">Imprimir</a>
												</th>													 										 											 												 												
												<th>
													<img src="<%=Request.ApplicationPath%>/images/bar_end.gif" width="17" height="18">
												</th>
											</tr>
										</thead>
									</table>																		 
									 
								</TD>
							</tr>
							 
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<DIV id="dvDia" class="DRCuerpoNormal" style="OVERFLOW: auto; WIDTH: 770px; HEIGHT: 250px" width="100%">
										<div class="tabla" id="tablaDetalle">
											<asp:datagrid id="dgProcesoResult" runat="server" Visible="False" AutoGenerateColumns="False" Width="750" BorderWidth="1px" CellPadding="3" CellSpacing="3" AllowPaging="True" PageSize="500">
												<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
												<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
												<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
												<Columns>
													<asp:BoundColumn DataField="CodigoBanco" HeaderText="Código IBS">
														<ItemStyle Width="50px"></ItemStyle>
													</asp:BoundColumn>								
													<asp:BoundColumn DataField="NombreTrabajador" HeaderText="Nombre del cliente">
														<HeaderStyle Width="150px"></HeaderStyle>
														<ItemStyle Width="150px"></ItemStyle>
													</asp:BoundColumn>
													<asp:BoundColumn DataField="NumeroPagare" HeaderText="Nro. De Pagaré" ItemStyle-Width="100"></asp:BoundColumn>													
													<asp:BoundColumn DataField="MontoDescuento" HeaderText="Descuento"></asp:BoundColumn>
													<asp:BoundColumn DataField="CodigoNombre" HeaderText="Comentarios" DataFormatString="{0:#.00}">
														<ItemStyle HorizontalAlign="left"></ItemStyle>
													</asp:BoundColumn>
												  </Columns>
												<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
											</asp:datagrid>
										</div>
									</DIV>
								</TD>
							</TR>
							<TR>
								<TD width="30"></TD>
								<TD colSpan="2">
									<TABLE cellSpacing="0" class="InputField" cellPadding="0" width="750" border="0">
										<TR>
											<TD width="30">&nbsp;</TD>
											<TD colSpan="2">
												<table cellSpacing="4" cellPadding="0" width="750" border="0">
													<TR>
													    <TD width="510" class="SubHead"></TD>
														<TD width="120" class="SubHead"></TD>
														<TD class="Normal"></TD>
													</TR>
													<TR>
													    <TD width="510" class="SubHead"></TD>
														<TD width="120" class="SubHead" align="right">Total Recibidos:</TD>
														<TD class="Normal" align="center"><asp:literal id="TotalRecibidos" runat="server"></asp:literal></TD>
													</TR>
													 
													<TR>
													    <TD width="510" class="SubHead"></TD>
														<TD width="120" class="SubHead" align="right">Total Procesados:</TD>
														<TD class="Normal" align="center"><asp:literal id="TotalProcesados" runat="server"></asp:literal></TD>
													</TR>
													<TR>
													    <TD width="510" class="SubHead"></TD>
														<TD width="120" class="SubHead" align="right">Total Rechazadas:</TD>
														<TD class="Normal" align="center"><asp:literal id="TotalRechazados" runat="server"></asp:literal></TD>
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
								<TD colSpan="2"><asp:linkbutton id="lnkBack" Runat="server"><img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Regresar' /></asp:linkbutton></TD>
							</tr>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="ConsultaDetallePagosIBSFechas" CodeFile="ConsultaDetallePagosIBSFechas.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Detalle de Pagos Procesados en IBS</title>
		<META HTTP-EQUIV="PRAGMA" CONTENT="NO-CACHE">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<BASE TARGET="_self">
		<LINK media="all" href="../css/calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../js/calendar.js" type="text/javascript"></script>
		<script language="javascript" src="../js/calendar-es.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
				<!--
					//Código para mostrar el calendario
					var oldLink = null;

					function selected(cal, date) 
					{	cal.sel.value = date;
						cal.callCloseHandler();
					}

					function closeHandler(cal) 
					{	cal.hide();
					}

					function showCalendar(id, format) 
					{	var el = document.getElementById(id);
						if (calendar != null) 
						{	calendar.hide();
						} else 
						{	var cal = new Calendar(false, null, selected, closeHandler);
						
							calendar = cal;
							cal.setRange(1900, 2070);
							cal.create();
						}
					
						calendar.setDateFormat(format);
						calendar.parseDate(el.value);
						calendar.sel = el;
						calendar.showAtElement(el);

						return false;
					}

			function Cerrar(){
				window.close();
			}
			
			function procesa(){
				__doPostBack ('lnkEnviar', '');
				document.all('divData').className='hide';
				 
				document.all ( 'divProcess').innerHTML = '<br><br>'+waitMessage ('<%=Request.ApplicationPath%>/images/sqsWait.gif');//'<center class="Subhead">Procesando...</center>';
					//				<div id="divProcess" name="divProcess"/>
					///<div id="divData" name="divData">

			}

		-->
		</script>
	</HEAD>
	<body topmargin="0" leftmargin="5"  onload="MM_preloadImages( '<%=Request.ApplicationPath%>/images/sqsWait.gif');">
		<form id="Form1" method="post" runat="server">
			<asp:LinkButton Runat="server" ID="lnkEnviar"></asp:LinkButton>
			<table border="0" cellspacing="0" cellpadding="0" width="100%">
				<tr>
					<td class="SubHead" align="middle">
						Pagos en IBS
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td class="SubHead" valign="bottom" width="20">
									Pagaré&nbsp;
								</td>
								<td align="left">
									<asp:Label Runat="server" ID="lblNumeroPagare" CssClass="Normal"></asp:Label>
								</td>
								<td>&nbsp;</td>
								<td class="SubHead" valign="bottom" width="20">
									Trabajador&nbsp;&nbsp;
								</td>
								<td align="left" colspan="3">
									<asp:Label Runat="server" CssClass="Normal" ID="lblNombreTrabajador"></asp:Label>
								</td>
							</tr>
							<tr>
								<td class="Subhead" width="80">Importe BIF Informado</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lblCuotaMes" Runat="server"></asp:Label></td>
								<td>&nbsp;</td>
								<td class="Subhead" width="150">Importe Descontado</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lblimporteDescontado" Runat="server"></asp:Label></td>
								<td class="Subhead" width="150">Deuda del periodo</td>
								<td align="left"><%=Request.Params("mon")%>
									<asp:Label ID="lbldeudaPeriodo" Runat="server"></asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td align="middle" class="CommandButton">
						<a href="javascript:Cerrar();">Cerrar</a>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td class="Subhead">Detalle Proceso IBS
								</td>
								<td class="Subhead">Desde
								</td>
								<td><asp:textbox id="txtFechaDesde" Runat="server" MaxLength="30" Columns="10" ReadOnly="true"></asp:textbox>&nbsp;<%=PintarBotonCalendario(ClientID, "txtFechaDesde")%>
								</td>
								<td class="Subhead">Hasta
								</td>
								<td><asp:textbox id="txtFechaHasta" Runat="server" MaxLength="30" Columns="10" ReadOnly="true"></asp:textbox>&nbsp;<%=PintarBotonCalendario(ClientID, "txtFechaHasta")%>
								</td>
								<td>
									<table border="0" cellpadding="2" cellspacing="0" class="box">
										<thead>
											<tr>
												<th>
													<img src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17"></th>
												<th align="center">
													<a href="javascript:procesa();">Buscar</a></th>
												<th>
													<img src="<%=Request.ApplicationPath%>/images/bar_end.gif" width="17" height="18"></th>
											</tr>
										</thead>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td><br>
					</td>
				</tr>
				<tr>
					<td>
						<div id="divProcess" name="divProcess" />
						<div id="divData" name="divData">
							<table border="0" cellspacing="0" cellpadding="0" width="100%">
								<tr>
									<td align="middle">
										<asp:DataGrid id="dgData" runat="server" CellPadding="2" BorderWidth="1px" Width="760" AutoGenerateColumns="False" Visible="False">
											<ItemStyle CssClass="TablaNormalBIFLittle" VerticalAlign="Top"></ItemStyle>
											<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="Fecha">
													<ItemTemplate>
														<%#BIFConvenios.Utils.GetFechaCanonica(DataBinder.Eval ( Container.DataItem, "Fecha"))%>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="DLDSC" HeaderText="Descripción"></asp:BoundColumn>
												<asp:BoundColumn DataField="Motivo" HeaderText="Motivo"></asp:BoundColumn>
												<asp:BoundColumn DataField="DLCCY" HeaderText="Mon" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#.00}"></asp:BoundColumn>
												<asp:BoundColumn DataField="DLCIC" HeaderText="Monto" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#.00}"></asp:BoundColumn>
												<asp:BoundColumn DataField="TipoCuota" HeaderText="Tipo de Importe"></asp:BoundColumn>
												<asp:BoundColumn DataField="PROCEDENCIA" HeaderText="Procedencia"></asp:BoundColumn>
											</Columns>
										</asp:DataGrid>
									</td>
								</tr>
								<tr>
									<td><br>
									</td>
								</tr>
								<tr>
									<td>
										<table border="0" cellspacing="0" cellpadding="0">
											<tr>
												<td align="right" class="SubHead">
													Total Procesado IBS
												</td>
												<td align="left" Class="Normal">&nbsp;
													<%=Request.Params("mon")%>
													&nbsp;<%=Request.Params("amount")%>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td><br>
									</td>
								</tr>
								<tr>
									<td align="middle" class="CommandButton">
										<a href="javascript:Cerrar();">Cerrar</a>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
			</table>
		</form>
		<script language="javascript">
		<!--
			window.status='BIF Convenios';
		-->
		</script>
	</body>
</HTML>

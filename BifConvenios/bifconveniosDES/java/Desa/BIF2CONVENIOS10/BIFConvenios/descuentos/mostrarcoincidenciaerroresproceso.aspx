<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.MostrarCoincidenciaErroresProceso" CodeFile="MostrarCoincidenciaErroresProceso.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Mostrar coincidencias con archivos cargados</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<BASE TARGET="_self">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type="text/javascript"></script>
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel=stylesheet>
		<script language="javascript">
<!--
	function Seleccionar(){
	if ( Form1.document.all ("rData") != null ) {
		if ( getSelectedRadio ( Form1.document.all ("rData") ) !=-1){
			top.returnValue = getSelectedRadioValue( Form1.document.all ("rData") ); 
			window.close();
		}
		else{
			alert ( 'Seleccione un trabajador');
		}
		
	}
	}
	
	function Cancelar(){
		window.close();
	}
-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" width="100%">
				<tr>
					<td colspan="2">
						<table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
							<tr>
								<td width="30">&nbsp;</td>
								<td class="PageTitle"><DIV id="hdShort">
										<H1 align="center">Coincidencias con carga no procesada
										</H1>
									</DIV>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="middle">
						&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="2" align="middle">
						<table border="0" cellpadding="2" cellspacing="0" class="box">
							<thead>
								<tr>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17"></th>
									<td class="CommandButton">
										<a href="javascript:Seleccionar();">Seleccionar</a>
									</td>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18"></th>
									<th>
										<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a>
									</th>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_end.gif" width="17" height="18"></th>
								</tr>
							</thead>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2" align="middle">
						&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="2" align="middle">
						<DIV class="tabla">
							<asp:DataGrid Runat="server" ID="dgData" AutoGenerateColumns="False" CellPadding="4" BorderWidth="1" CellSpacing="3" Width="100%">
								<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
								<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<input type=radio id="rData" name="rData"  value='<%#DataBinder.Eval(Container.DataItem, "Id Proceso").ToString() +"|" + DataBinder.Eval(Container.DataItem, "Linea").ToString() +"|" + DataBinder.Eval(Container.DataItem, "Monto Descuento").ToString() %>' >
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:BoundColumn DataField="Nombre" HeaderText="Nombre"></asp:BoundColumn>
									<asp:BoundColumn DataField="Monto Descuento" HeaderText="Monto Descuento" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
								</Columns>
							</asp:DataGrid>
						</DIV>
					</td>
				</tr>
				<TR>
					<TD colspan="2">&nbsp;
					</TD>
				</TR>
				<TR>
					<TD colspan="2">
						<span class="SubHead">&nbsp;&nbsp;Número de Registros&nbsp;:&nbsp;</span>
						<asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label></TD>
				</TR>
				<tr>
					<td colspan="2" align="middle">
						<table border="0" cellpadding="2" cellspacing="0" class="box">
							<thead>
								<tr>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17"></th>
									<td class="CommandButton">
										<a href="javascript:Seleccionar();">Seleccionar</a>
									</td>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18"></th>
									<th>
										<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a>
									</th>
									<th>
										<img src="<%=Request.ApplicationPath%>/images/bar_end.gif" width="17" height="18"></th>
								</tr>
							</thead>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

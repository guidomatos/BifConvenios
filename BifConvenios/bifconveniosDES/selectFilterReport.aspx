<%@ Page Language="VB" AutoEventWireup="false" CodeFile="selectFilterReport.aspx.vb" Inherits="selectFilterReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
        <title>Seleccione los filtros del reporte</title>
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">    
		<script language="javascript">
		<!--
		function GenerarReporte(id){
		    //alert('enviomail.aspx?idp='+ id +  '&formatoArchivo=' + formatoArchivo +'&tipoCliente='+tipoCliente)
			//openPage( 'enviomail.aspx?idp='+ id +  '&formatoArchivo=' + formatoArchivo +'&tipoCliente='+tipoCliente, 430,500 );
			//top.returnValue	= id + "|" + formatoArchivo + "|" + situacionTrabajador;			
			top.returnValue	= id + "|" + document.all('lstModalidad').value + "|" + document.all('ddlEstadoTrabajador').value;			
			this.close();			
		}		
		
		function Cerrar2() {
			top.returnValue	= '';
			this.close();
		}		
		-->
		</script>
</head>
<body leftmargin="20" topmargin="10">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height="60" class="TableSmooth">
				<tr>
					<td class="SubHead" colspan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td class="Text" colspan="2">&nbsp; Selecci&oacute;n de Filtro para Listado de Cuotas
					</td>
				</tr>
				<tr>
					<td class="Text" colspan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<table border="0" width="430">
							<tr>
								<td class="SubHead" width="140"><font class="Normal">&nbsp;Empresa&nbsp;: </font>
								</td>
								<td class="Normal"><%=nombre%></td>
							</tr>
							<tr>
								<td class="SubHead"><font class="Normal">&nbsp;Periodo&nbsp;:</font>
								</td>
								<td class="Normal"><%=mes%>
									-
									<%=anio%>
								</td>
							</tr>
							<tr>
								<td class="SubHead"><font class="Normal">&nbsp;Fecha de Proceso IBS&nbsp;: </font>
								</td>
								<td class="Normal"><%=fechaProcesoAS400%></td>
							</tr>
							<tr>
								<td class="SubHead">
									<font class="Normal">&nbsp;Obtener trabajadores&nbsp;: </font>
								</td>
								<td>
									<asp:DropDownList Runat="server" id="ddlEstadoTrabajador" DataTextField="nombreCortoFlag" DataValueField="dlst"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td class="SubHead" colspan="2">&nbsp;
								</td>
							</tr>
							<tr>
								<td class="SubHead" style="height: 24px">
									<font class="Normal">&nbsp; Modalidad&nbsp;:&nbsp; </font>
								</td>
								<td style="height: 24px">
									<asp:DropDownList Runat="server" ID="lstModalidad" DataTextField="textoModalidad" DataValueField="codigoModalidad">									   
									</asp:DropDownList>

								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="SubHead" colspan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td align="middle" colspan="2">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td class="SubHead" align="middle">
									<a href="#" onclick="JavaScript:GenerarReporte('<%=idP%>')"  >
									Generar Listado</a> &nbsp;&nbsp;&nbsp;&nbsp;
									<a href="#" onclick="JavaScript:Cerrar2();">Cancelar</a>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="SubHead" colspan="2">&nbsp;
					</td>
				</tr>
			</table>
		</form>
</body>
</html>

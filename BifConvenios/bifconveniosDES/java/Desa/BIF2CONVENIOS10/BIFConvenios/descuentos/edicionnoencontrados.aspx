<%@ Reference Control="~/controls/statusmsg.ascx" %>
<%@ Register TagPrefix="uc1" TagName="StatusMsg" Src="../controls/StatusMsg.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EdicionNoEncontrados" CodeFile="EdicionNoEncontrados.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Edición de registros no encontrados en el proceso de carga de 
			Cuotas</title>
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
			 function Valida ( obj, args ) {
				
				if( fctTrim ( document.all( 'txtMontoDescuento').value )  == '') {
					alert('Ingrese el monto del descuento');
					document.all( 'txtMontoDescuento').focus();
					args.IsValid = false
					return;
				}

				if( isNaN(fctTrim ( document.all( 'txtMontoDescuento').value ) ) == true) {
					alert('Ingrese un monto de descuento válido');
					document.all( 'txtMontoDescuento').value = '';
					document.all( 'txtMontoDescuento').focus();
					args.IsValid = false
					return;
				}			 
				args.IsValid = confirm ( '¿Desea actualizar el monto de descuento?' ) ;
			 }
			 
			 //Eliminar
			 function Eliminar() {
				if (confirm ( '¿Desea eliminar el registro?') ){
					__doPostBack('lnkEliminar', '');
				}
			 }

			 function VerCoincidenciasProceso(){
				var returnValue = openDialog('MostrarCoincidenciaErroresProceso.aspx?id=<%=Request.Params("id")%>&pid=<%=Request.Params("pid")%>', 400, 430);

				if ( typeof returnValue != "undefined" ) {
					document.all( 'txtMontoDescuento').value = getvalue ( returnValue , 3, "|");
					document.all ( 'hdInformacionDescuentoRegistro').value = returnValue;
				} 
			 }
		-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="MM_preloadImages('/BIFConvenios/images/aceptar_on.jpg', '/BIFConvenios/images/eliminar_on.jpg', '/BIFConvenios/images/cancelar_on.jpg')">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Detalle del proceso del archivo de cuotas" runat="server"></uc1:banner></td>
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
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td class="NormalRed" colSpan="3">Información Personal</td>
										</tr>
										<tr>
											<td colSpan="3">&nbsp;</td>
										</tr>
										<tr>
											<td class="SubHead">Convenio</td>
											<td class="Normal" width="40">&nbsp;</td>
											<td><asp:label id="lblConvenio" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Nombre del trabajador</td>
											<td class="Normal" width="40">&nbsp;</td>
											<td><asp:label id="lblNombreTrabajador" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Código Modular
											</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblCodigoModular" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Código de referencia</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblCodigoReferencia" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Situación laboral</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblSituacionLaboral" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td colSpan="3">&nbsp;</td>
										</tr>
										<tr>
											<td class="NormalRed" colSpan="3">Información del Pago</td>
										</tr>
										<tr>
											<td colSpan="3">&nbsp;</td>
										</tr>
										<tr>
											<td class="SubHead">Número de Pagaré
											</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblNumeroPagare" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Año</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblAnio" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Mes</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblMes" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Moneda</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblMoneda" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Cuota</td>
											<td>&nbsp;</td>
											<td><asp:label id="lblCuota" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Monto de descuento</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtMontoDescuento" CssClass="flatTextBox" MaxLength="15" Runat="server" ReadOnly="true" ></asp:textbox>
												<a href="javascript:VerCoincidenciasProceso();"><img src="../images/hojaylupa.gif" border="0" alt="Ver Trabajadores con coincidencia en apellido paterno con esta persona"></a>
												<input type="hidden" id="hdInformacionDescuentoRegistro" name="hdInformacionDescuentoRegistro" runat="server">
											</td>
										</tr>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD></TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="SubHead" colSpan="3"><uc1:statusmsg id="StatusMsg1" runat="server" Visible="False"></uc1:statusmsg><asp:customvalidator id="cvValida" runat="server" ClientValidationFunction="Valida" Display="None" ErrorMessage="CustomValidator"></asp:customvalidator></TD>
										</TR>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD></TD>
											<TD>&nbsp;</TD>
										</TR>
										<tr>
											<td>&nbsp;</td>
											<td colspan="2">
												<asp:LinkButton id="lnkModificar" runat="server">
													<img src='/BIFConvenios/images/aceptar.jpg' name='Image1' border="0" alt='Modificar registro' /></asp:LinkButton>&nbsp;
												<a href="JavaScript:Eliminar();" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image2','','/BIFConvenios/images/eliminar_on.jpg',1)">
													<img src='/BIFConvenios/images/eliminar.jpg' name='Image2' border="0" alt='Eliminar registro'></a>
												<asp:LinkButton id="lnkEliminar" CausesValidation="False" runat="server"></asp:LinkButton>&nbsp;
												<asp:LinkButton id="lnkCancelar" runat="server" CausesValidation="False">
													<img src='/BIFConvenios/images/cancelar.jpg' name='Image3' border="0" alt='Regresar' /></asp:LinkButton>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

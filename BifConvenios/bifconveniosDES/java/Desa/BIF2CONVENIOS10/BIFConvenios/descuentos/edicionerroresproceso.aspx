<%@ Reference Control="~/controls/statusmsg.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EdicionErroresProceso" CodeFile="EdicionErroresProceso.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="StatusMsg" Src="../controls/StatusMsg.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Edición de errores del proceso de carga de Archivo de 
			descuentos</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<style type="text/css">
		.hide { DISPLAY: none }
		</style>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
		<script language="javascript">
		<!--
			function Valida ( obj, args ) {
			
				/*if ( fctTrim ( document.all('txtCodigoModular').value )== '') {
					document.all('txtCodigoModular').focus();
					alert ('Debe ingresar el Código Modular');
					args.IsValid = false;
					return;
				}*/
				
				/*if ( fctTrim ( document.all('txtCodigoReferencia').value )== '') {
					document.all('txtCodigoReferencia').focus();
					alert ('Debe ingresar el Código de referencia');
					args.IsValid = false;
					return;
				}*/
				
				/*if ( fctTrim ( document.all('ddlSituacionLaboral').value )== '') {
					document.all('ddlSituacionLaboral').focus();
					alert ('Seleccione la situación Laboral');
					args.IsValid = false;
					return;
				}*/
				
				if ( fctTrim ( document.all('txtNumeroPagare').value )== '') {
					document.all('txtNumeroPagare').focus();
					alert ('Debe ingresar el número de pagaré');
					args.IsValid = false;
					return;
				}				
				
				if ( isNaN ( fctTrim (document.all('txtNumeroPagare').value ))) {
					document.all('txtNumeroPagare').focus();
					alert ('Utilicé dígitos del [0-9] para el número de pagaré');
					args.IsValid = false;
					return;
				}	
				
				
				if ( fctTrim ( document.all('ddlAnio').value )== '') {
					document.all('ddlAnio').focus();
					alert ('Seleccione el año');
					args.IsValid = false;
					return;
				}
				
				if ( fctTrim ( document.all('ddlMes').value )== '') {
					document.all('ddlMes').focus();
					alert ('Seleccione el mes');
					args.IsValid = false;
					return;
				}
				
				if ( fctTrim ( document.all('ddlMoneda').value )== '') {
					document.all('ddlMoneda').focus();
					alert ('Seleccione la moneda');
					args.IsValid = false;
					return;
				}
				
				if ( fctTrim ( document.all('txtCuota').value )== '' ) {
					document.all('txtCuota').focus();
					alert ('Debe ingresar el monto de la cuota');
					args.IsValid = false;
					return;
				}
				

				if ( isNaN(fctTrim ( document.all('txtCuota').value ))== true ) {
					document.all('txtCuota').value = '';
					document.all('txtCuota').focus();
					alert ('Ingrese un valor de cuota válido');
					args.IsValid = false;
					return;
				}


				if ( fctTrim ( document.all('txtMontoDescuento').value )== '' ) {
					document.all('txtMontoDescuento').focus();
					alert ('Debe ingresar el monto de descuento');
					args.IsValid = false;
					return;
				}
				
				if ( isNaN(fctTrim ( document.all('txtMontoDescuento').value ))== true ) {
					document.all('txtMontoDescuento').value = '';
					document.all('txtMontoDescuento').focus();
					alert ('Ingrese un monto de descuento válido');
					args.IsValid = false;
					return;
				}

				
				args.IsValid = confirm ( '¿Desea actualizar la información?');
			}
			
			function Eliminar(){
				if (confirm( '¿Desea eliminar el registro?')) 
				{
					__doPostBack ('lnkEliminar', '');
				}
			}
			function Insertar ( ) {
				if (confirm( '¿Desea adicionar el registro a los datos existentes en la tabla de envió?\n(Adicionar a la Información que fue extraída de IBS)')) 
				{
					__doPostBack ('lnkInsertar', '');
				}
			}
			
			function changeObjs ( ) {
				aIns.className='hide';
				alert('Al modificar el registro no podrá insertarlo a en la tabla de envió.');
			}
		-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onLoad="MM_preloadImages('/BIFConvenios/images/cancelar_on.jpg','/BIFConvenios/images/aceptar_on.jpg', '/BIFConvenios/images/eliminar_on.jpg')">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Edición de errores del proceso de carga de Archivo de cuotas" runat="server"></uc1:banner></td>
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
											<td class="SubHead">Nombre del trabajador</td>
											<td class="Normal" width="40">&nbsp;</td>
											<td><asp:label id="lblNombreTrabajador" runat="server" CssClass="Normal"></asp:label></td>
										</tr>
										<tr>
											<td class="SubHead">Código Modular
											</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtCodigoModular" Runat="server" MaxLength="20" CssClass="flatTextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Código de referencia</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtCodigoReferencia" Runat="server" MaxLength="20" CssClass="flatTextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Situación laboral</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList ID="ddlSituacionLaboral" Runat="server" CssClass="flatTextBox">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="A">Activo
											</asp:ListItem>
													<asp:ListItem Value="C">Cesante
											</asp:ListItem>
													<asp:ListItem Value="P">Pensionista
											</asp:ListItem>
													<asp:ListItem Value="S">Sobreviviente
											</asp:ListItem>
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td colspan="3">&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="3" class="NormalRed">
												Información del Pago</td>
										</tr>
										<tr>
											<td colspan="3">&nbsp;</td>
										</tr>
										<tr>
											<td class="SubHead">Número de Pagaré
											</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtNumeroPagare" Runat="server" MaxLength="12" CssClass="flatTextBox"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Año</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList Runat="server" ID="ddlAnio"></asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td class="SubHead">Mes</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList Runat="server" ID="ddlMes"></asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td class="SubHead">Moneda</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList Runat="server" ID="ddlMoneda">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="SOL">SOL</asp:ListItem>
													<asp:ListItem Value="USD">USD</asp:ListItem>
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td class="SubHead">Cuota</td>
											<td>&nbsp;</td>
											<td>
												<asp:textbox id="txtCuota" Runat="server" MaxLength="15" CssClass="flatTextBox"></asp:textbox>
											</td>
										</tr>
										<tr>
											<td class="SubHead">Monto de descuento</td>
											<td>&nbsp;</td>
											<td>
												<asp:textbox id="txtMontoDescuento" Runat="server" MaxLength="15" CssClass="flatTextBox"></asp:textbox></td>
										</tr>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD></TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="SubHead" colSpan="3">
												<uc1:StatusMsg id="StatusMsg1" runat="server" Visible="False"></uc1:StatusMsg>
												<asp:CustomValidator id="cvValida" runat="server" ErrorMessage="CustomValidator" Display="None" ClientValidationFunction="Valida"></asp:CustomValidator></TD>
										</TR>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="SubHead">&nbsp;
											</TD>
											<TD></TD>
											<TD>
												<asp:LinkButton id="lnkModificar" runat="server">
													<img src="/BIFConvenios/images/aceptar.jpg" name="Image11" width="88" height="21" border="0"></asp:LinkButton>&nbsp;
												<a href="Javascript:Eliminar();" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image12','','/BIFConvenios/images/eliminar_on.jpg',1)">
													<img src="/BIFConvenios/images/eliminar.jpg" name="Image12" width="88" height="21" border="0"></a>
												<a href="JavaScript:Insertar();" onMouseOut="MM_swapImgRestore()" onMouseOver="MM_swapImage('Image13','','/BIFConvenios/images/insertar_on.jpg',1)" id="aIns" name="aIns" runat="server" visible="false">
													<img src="/BIFConvenios/images/insertar.jpg" name="Image13" width="88" height="21" border="0"></a>
												<asp:LinkButton id="lnkInsertar" CausesValidation="False" runat="server"></asp:LinkButton>
												<asp:LinkButton id="lnkEliminar" runat="server" CausesValidation="False"></asp:LinkButton>
												<asp:LinkButton id="lnkCancelar" runat="server" CausesValidation="False">
													<img src="/BIFConvenios/images/cancelar.jpg" name="Image10" width="88" height="21" border="0"></asp:LinkButton>&nbsp;</TD>
										</TR>
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

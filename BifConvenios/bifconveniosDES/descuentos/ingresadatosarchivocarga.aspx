<%@ Register TagPrefix="uc1" TagName="StatusMsg" Src="../controls/StatusMsg.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.IngresaDatosArchivoCarga" CodeFile="IngresaDatosArchivoCarga.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IngresaDatosArchivoCarga</title>
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
		/*function Valida(obj, args){
			
		}*/
		function Insertar(){
				if ( fctTrim ( document.all('txtCodigoModular').value )== '') {
					document.all('txtCodigoModular').focus();
					alert ('Debe ingresar el Código Modular');
					return;
				}				
		
				if ( fctTrim ( document.all('txtNumeroPagare').value )== '') {
					document.all('txtNumeroPagare').focus();
					alert ('Debe ingresar el número de pagaré');
					return;
				}				
				
				if ( isNaN ( fctTrim (document.all('txtNumeroPagare').value ))) {
					document.all('txtNumeroPagare').focus();
					alert ('Utilicé dígitos del [0-9] para el número de pagaré');
					return;
				}	
				
				
				if ( fctTrim ( document.all('ddlAnio').value )== '') {
					document.all('ddlAnio').focus();
					alert ('Seleccione el año');
					return;
				}
				
				if ( fctTrim ( document.all('ddlMes').value )== '') {
					document.all('ddlMes').focus();
					alert ('Seleccione el mes');
					return;
				}
				
				if ( fctTrim ( document.all('ddlMoneda').value )== '') {
					document.all('ddlMoneda').focus();
					alert ('Seleccione la moneda');
					return;
				}
				
				if ( fctTrim ( document.all('txtCuota').value )== '' ) {
					document.all('txtCuota').focus();
					alert ('Debe ingresar el monto de la cuota');
					return;
				}
				

				if ( isNaN(fctTrim ( document.all('txtCuota').value ))== true ) {
					document.all('txtCuota').value = '';
					document.all('txtCuota').focus();
					alert ('Ingrese un valor de cuota válido');
					return;
				}


				if ( fctTrim ( document.all('txtMontoDescuento').value )== '' ) {
					document.all('txtMontoDescuento').focus();
					alert ('Debe ingresar el monto de descuento');
					return;
				}
				
				if ( isNaN(fctTrim ( document.all('txtMontoDescuento').value ))== true ) {
					document.all('txtMontoDescuento').value = '';
					document.all('txtMontoDescuento').focus();
					alert ('Ingrese un monto de descuento válido');
					return;
				}
		
			__doPostBack ( 'lnkInsertar', '');
		}
	-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="MM_preloadImages('/BIFConvenios/images/cancelar_on.jpg','/BIFConvenios/images/aceptar_on.jpg')" rightMargin="0">
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
											<td><asp:label id="lblNombreTrabajador" runat="server" CssClass="Normal"></asp:label><asp:textbox id="txtNombreTrabajador" CssClass="flatTextBox" Runat="server"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Código Modular
											</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtCodigoModular" CssClass="flatTextBox" MaxLength="20" Runat="server"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Código de referencia</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtCodigoReferencia" CssClass="flatTextBox" MaxLength="20" Runat="server"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Situación laboral</td>
											<td>&nbsp;</td>
											<td><asp:dropdownlist id="ddlSituacionLaboral" CssClass="flatTextBox" Runat="server">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="A">Activo
											</asp:ListItem>
													<asp:ListItem Value="C">Cesante
											</asp:ListItem>
													<asp:ListItem Value="P">Pensionista
											</asp:ListItem>
													<asp:ListItem Value="S">Sobreviviente
											</asp:ListItem>
												</asp:dropdownlist></td>
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
											<td><asp:textbox id="txtNumeroPagare" CssClass="flatTextBox" MaxLength="12" Runat="server"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Año</td>
											<td>&nbsp;</td>
											<td><asp:dropdownlist id="ddlAnio" Runat="server"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td class="SubHead">Mes</td>
											<td>&nbsp;</td>
											<td><asp:dropdownlist id="ddlMes" Runat="server"></asp:dropdownlist></td>
										</tr>
										<tr>
											<td class="SubHead">Moneda</td>
											<td>&nbsp;</td>
											<td><asp:dropdownlist id="ddlMoneda" Runat="server">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="SOL">SOL</asp:ListItem>
													<asp:ListItem Value="USD">USD</asp:ListItem>
												</asp:dropdownlist></td>
										</tr>
										<tr>
											<td class="SubHead">Cuota</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtCuota" CssClass="flatTextBox" MaxLength="15" Runat="server"></asp:textbox></td>
										</tr>
										<tr>
											<td class="SubHead">Monto de descuento</td>
											<td>&nbsp;</td>
											<td><asp:textbox id="txtMontoDescuento" CssClass="flatTextBox" MaxLength="15" Runat="server"></asp:textbox></td>
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
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="SubHead">&nbsp;
											</TD>
											<TD></TD>
											<TD><A id="aIns" onmouseover="MM_swapImage('Image13','','/BIFConvenios/images/aceptar_on.jpg',1)" onmouseout="MM_swapImgRestore()" href="JavaScript:Insertar();" name="aIns" runat="server"><IMG height="21" src="/BIFConvenios/images/aceptar.jpg" width="88" border="0" name="Image13"></A>
												<asp:linkbutton id="lnkInsertar" runat="server" CausesValidation="False"></asp:linkbutton><asp:linkbutton id="lnkCancelar" runat="server" CausesValidation="False">
													<img src="/BIFConvenios/images/cancelar.jpg" name="Image10" width="88" height="21" border="0"></asp:linkbutton>&nbsp;</TD>
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

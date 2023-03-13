<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.IngresaDatosArchivoCarga" CodeFile="IngresaDatosArchivoCarga.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="StatusMsg" Src="../controls/StatusMsg.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>IngresaDatosArchivoCarga</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language="javascript" type="text/javascript"></script>
		<script type="text/javascript" language="javascript">
	
			function Insertar() {
				if (fctTrim(document.all('txtCodigoModular').value) == '') {
					document.all('txtCodigoModular').focus();
					alert('Debe ingresar el Código Modular');
					return;
				}

				if (fctTrim(document.all('txtNumeroPagare').value) == '') {
					document.all('txtNumeroPagare').focus();
					alert('Debe ingresar el número de pagaré');
					return;
				}

				if (isNaN(fctTrim(document.all('txtNumeroPagare').value))) {
					document.all('txtNumeroPagare').focus();
					alert('Utilicé dígitos del [0-9] para el número de pagaré');
					return;
				}

				if (fctTrim(document.all('ddlAnio').value) == '') {
					document.all('ddlAnio').focus();
					alert('Seleccione el año');
					return;
				}

				if (fctTrim(document.all('ddlMes').value) == '') {
					document.all('ddlMes').focus();
					alert('Seleccione el mes');
					return;
				}

				if (fctTrim(document.all('ddlMoneda').value) == '') {
					document.all('ddlMoneda').focus();
					alert('Seleccione la moneda');
					return;
				}

				if (fctTrim(document.all('txtCuota').value) == '') {
					document.all('txtCuota').focus();
					alert('Debe ingresar el monto de la cuota');
					return;
				}

				if (isNaN(fctTrim(document.all('txtCuota').value)) == true) {
					document.all('txtCuota').value = '';
					document.all('txtCuota').focus();
					alert('Ingrese un valor de cuota válido');
					return;
				}

				if (fctTrim(document.all('txtMontoDescuento').value) == '') {
					document.all('txtMontoDescuento').focus();
					alert('Debe ingresar el monto de descuento');
					return;
				}

				if (isNaN(fctTrim(document.all('txtMontoDescuento').value)) == true) {
					document.all('txtMontoDescuento').value = '';
					document.all('txtMontoDescuento').focus();
					alert('Ingrese un monto de descuento válido');
					return;
				}

				__doPostBack('lnkInsertar', '');
			}
	
		</script>
	</head>
	<body leftMargin="0" topMargin="0" onload="MM_preloadImages('<%=ResolveUrl("~/images/cancelar_on.jpg")%>','<%=ResolveUrl("~/images/aceptar_on.jpg")%>')" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:Banner id="Banner1" title="Edición de errores del proceso de carga de Archivo de cuotas" runat="server"></uc1:Banner></td>
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
											<td><asp:Label id="lblNombreTrabajador" runat="server" CssClass="Normal"></asp:Label><asp:TextBox id="txtNombreTrabajador" CssClass="flatTextBox" Runat="server"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="SubHead">Código Modular
											</td>
											<td>&nbsp;</td>
											<td><asp:TextBox id="txtCodigoModular" CssClass="flatTextBox" MaxLength="20" Runat="server"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="SubHead">Código de referencia</td>
											<td>&nbsp;</td>
											<td><asp:TextBox id="txtCodigoReferencia" CssClass="flatTextBox" MaxLength="20" Runat="server"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="SubHead">Situación laboral</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList id="ddlSituacionLaboral" CssClass="flatTextBox" Runat="server">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="A">Activo</asp:ListItem>
													<asp:ListItem Value="C">Cesante</asp:ListItem>
													<asp:ListItem Value="P">Pensionista</asp:ListItem>
													<asp:ListItem Value="S">Sobreviviente</asp:ListItem>
												</asp:DropDownList>
											</td>
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
											<td class="SubHead">Número de Pagaré</td>
											<td>&nbsp;</td>
											<td><asp:TextBox id="txtNumeroPagare" CssClass="flatTextBox" MaxLength="12" Runat="server"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="SubHead">Año</td>
											<td>&nbsp;</td>
											<td><asp:DropDownList id="ddlAnio" Runat="server"></asp:DropDownList></td>
										</tr>
										<tr>
											<td class="SubHead">Mes</td>
											<td>&nbsp;</td>
											<td><asp:DropDownList id="ddlMes" Runat="server"></asp:DropDownList></td>
										</tr>
										<tr>
											<td class="SubHead">Moneda</td>
											<td>&nbsp;</td>
											<td>
												<asp:DropDownList id="ddlMoneda" Runat="server">
													<asp:ListItem Value="">-Sin Seleccionar-</asp:ListItem>
													<asp:ListItem Value="SOL">SOL</asp:ListItem>
													<asp:ListItem Value="USD">USD</asp:ListItem>
												</asp:DropDownList>
											</td>
										</tr>
										<tr>
											<td class="SubHead">Cuota</td>
											<td>&nbsp;</td>
											<td><asp:TextBox id="txtCuota" CssClass="flatTextBox" MaxLength="15" Runat="server"></asp:TextBox></td>
										</tr>
										<tr>
											<td class="SubHead">Monto de descuento</td>
											<td>&nbsp;</td>
											<td><asp:TextBox id="txtMontoDescuento" CssClass="flatTextBox" MaxLength="15" Runat="server"></asp:TextBox></td>
										</tr>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD></TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD class="SubHead" colSpan="3"><uc1:StatusMsg id="StatusMsg1" runat="server" Visible="False"></uc1:StatusMsg><asp:CustomValidator id="cvValida" runat="server" ClientValidationFunction="Valida" Display="None" ErrorMessage="CustomValidator"></asp:CustomValidator></TD>
										</TR>
										<TR>
											<TD class="SubHead">&nbsp;</TD>
											<TD>&nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
										<tr>
											<td class="SubHead">&nbsp;
											</td>
											<td></td>
											<td><a id="aIns" onmouseover="MM_swapImage('Image13','','../images/aceptar_on.jpg',1)" onmouseout="MM_swapImgRestore()" href="JavaScript:Insertar();" name="aIns" runat="server"><img alt="" height="21" src="../images/aceptar.jpg" width="88" border="0" name="Image13"></a>
												<asp:LinkButton id="lnkInsertar" runat="server" CausesValidation="false"></asp:LinkButton>
												<asp:LinkButton id="lnkCancelar" runat="server" CausesValidation="false">
													<img alt="" src="../images/cancelar.jpg" name="Image10" width="88" height="21" border="0"></asp:LinkButton>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

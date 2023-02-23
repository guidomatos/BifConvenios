<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EditarCliente" CodeFile="EditarCliente.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="./controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Edición de Datos de la Empresa</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">--%>
		<LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet">
		<%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
		<script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		<!--
			function Valida (obj, args ){
				args.IsValid = true;

				if ( fctTrim (document.all("txtNombreCliente").value ) == '' ) {
					alert ('Ingrese el nombre de la empresa');
					document.all("txtNombreCliente").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("ddlTipoDocumento").value ) == '' ) {
					alert ('Seleccione el tipo de documento');
					document.all("ddlTipoDocumento").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtNumeroDocumento").value ) == '' ) {
					alert ('Ingrese el número de documento');
					document.all("txtNumeroDocumento").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtDiaEnvioPlanilla").value ) == '' ) {
					alert ('Ingrese el día de envio de planilla');
					document.all("txtDiaEnvioPlanilla").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtDiaCierrePlanilla").value ) == '' ) {
					alert ('Ingrese el día de cierre de planilla');
					document.all("txtDiaCierrePlanilla").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtMesesAnticipEnvioListado").value ) == '' ) {
					alert ('Ingrese el número de meses de anticipación para el envío de listado');
					document.all("txtMesesAnticipEnvioListado").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtDiaCorte").value ) == '' ) {
					alert ('Ingrese el día de corte');
					document.all("txtDiaCorte").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("ddlFuncionarioConvenios").value ) == '' ) {
					alert ('Seleccione el funcionario del convenio');
					document.all("ddlFuncionarioConvenios").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtCodigoIBS").value ) == '' ) {
					alert ('Ingrese el código IBS de la empresa');
					document.all("txtCodigoIBS").focus();
					args.IsValid = false;
					return;
				}
                if ( fctTrim (document.all("txtCodigoInstitucion").value ) == '' ) {
					alert ('Ingrese el código de institución del banco');
					document.all("txtCodigoInstitucion").focus();
					args.IsValid = false;
					return;
				}				
				if ( fctTrim (document.all("ddlEnvioAutListadoDescuentos").value ) == '' ) {
					alert ('Seleccione el indicador de envío automático de listado de descuentos');
					document.all("ddlEnvioAutListadoDescuentos").focus();
					args.IsValid = false;
					return;
				}
				if ( fctTrim (document.all("txtTelefono1").value ) == '' && fctTrim (document.all("txtTelefono2").value ) == '' && fctTrim (document.all("txtTelefono3").value ) == '') {
					alert ('Ingrese al menos un teléfono de la empresa');
					document.all("txtTelefono1").focus();
					args.IsValid = false;
					return;
				}
								
				if ( args.IsValid ) {
					args.IsValid = confirm ("¿Desea grabar la información de la empresa?");
				}
			}			

		-->
		</script>
	</HEAD>
	<body leftmargin="0" topmargin="0" onload="MM_preloadImages('/images/aceptar_on.jpg','/images/cancelar_on.jpg');">
		<form id="Form1" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td>
						<uc1:Banner id="Banner1" runat="server" Title="Edición de Datos de la Empresa"></uc1:Banner>
					</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="0" cellPadding="35" width="650" border="0">
				<TR>
					<TD width="30%" colspan="2" background="<%= ResolveUrl("~/images/hoja1.jpg") %>" height="550" valign="top">
						<table border="0" cellpadding="8" cellspacing="0" width="100%">
							<TR>
								<TD width="30">&nbsp;</TD>
								<TD class="Normal" style="width: 464px">
									Nombre de la Empresa
								</TD>
								<TD style="width: 417px"><asp:textbox id="txtNombreCliente" runat="server" BorderWidth="1pt" BorderColor="Silver" Width="280px" MaxLength="100"></asp:textbox></TD>
							</TR>
							<TR>
								<TD width="30">&nbsp;</TD>
								<TD class="Normal" style="width: 464px">
									Tipo de Documento
								</TD>
								<TD style="width: 417px"><asp:dropdownlist id="ddlTipoDocumento" runat="server" DataTextField="Descripcion" DataValueField="Codigo"></asp:dropdownlist></TD>
							</TR>
							<TR>
								<TD width="30">&nbsp;</TD>
								<TD class="Normal" style="width: 464px">
									Número de Documento
								</TD>
								<TD style="width: 417px"><asp:textbox id="txtNumeroDocumento" runat="server" MaxLength="12" BorderWidth="1pt" BorderColor="Silver" Width="166px"></asp:textbox></TD>
							</TR>
                            <tr>
                                <td style="width: 20px; height: 38px">
                                </td>
                                <td class="Normal" style="width: 464px; height: 38px;">
                                    Día de envío planilla</td>
                                <td valign="top" style="height: 38px; width: 417px;">
                                    <asp:TextBox ID="txtDiaEnvioPlanilla" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="2" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px; height: 38px">
                                </td>
                                <td class="Normal" style="width: 464px; height: 38px;">
                                    Día de cierre planilla</td>
                                <td valign="top" style="height: 38px; width: 417px;">
                                    <asp:TextBox ID="txtDiaCierrePlanilla" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="2" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Meses anticip. envío listado</td>
                                <td valign="top" style="width: 417px">
                                    <asp:TextBox ID="txtMesesAnticipEnvioListado" runat="server" BorderColor="Silver"
                                        BorderWidth="1pt" MaxLength="2" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Día de corte</td>
                                <td valign="top" style="width: 417px">
                                    <asp:TextBox ID="txtDiaCorte" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="2" Width="40px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Funcionario Convenios</td>
                                <td valign="top" style="width: 417px">
                                    <asp:dropdownlist id="ddlFuncionarioConvenios" runat="server" DataTextField="Descripcion" DataValueField="Codigo">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Código IBS</td>
                                <td valign="top" style="width: 417px">
                                    <asp:TextBox ID="txtCodigoIBS" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="12" Width="115px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Código de institución</td>
                                <td valign="top" style="width: 417px">
                                    <asp:TextBox ID="txtCodigoInstitucion" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="100" Width="116px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Envío aut. listado descuentos</td>
                                <td valign="top" style="width: 417px">
                                    <asp:dropdownlist id="ddlEnvioAutListadoDescuentos" runat="server" DataTextField="Descripcion" DataValueField="Codigo">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width: 20px">
                                </td>
                                <td class="Normal" style="width: 464px">
                                    Teléfonos de contacto</td>
                                <td valign="top" style="width: 417px">
                                    <asp:TextBox ID="txtTelefono1" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="11" Width="95px"></asp:TextBox>
                                    &nbsp;
                                    <asp:TextBox ID="txtTelefono2" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="11" Width="95px"></asp:TextBox>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtTelefono3" runat="server" BorderColor="Silver" BorderWidth="1pt"
                                        MaxLength="11" Width="95px"></asp:TextBox>
                                    &nbsp;</td>
                            </tr>
							<tr>
								<td colspan="3" align="right">
                                    &nbsp;
									<asp:Label id="lblMensaje" runat="server" ForeColor="Red"></asp:Label></td>
							</tr>
							<TR>
								<TD width="30" style="height: 28px">&nbsp;</TD>
								<TD style="width: 464px; height: 28px">
									<asp:customvalidator id="cvValida" runat="server" ErrorMessage="CustomValidator" Display="None" ClientValidationFunction="Valida"></asp:customvalidator>
								</TD>
								<TD style="width: 417px; height: 28px;">
                                    &nbsp;
                                    &nbsp;
                                    <asp:LinkButton CausesValidation="False" id="lnkCancelar" runat="server">
										<IMG id="btnRetornar" title="Cancelar" alt="Cancelar" src="<%= ResolveUrl("~/images/cancelar.jpg") %>" border="0" name="Image12"></asp:LinkButton>
									&nbsp;
									<asp:LinkButton id="lnkAceptar" runat="server">
										<img src="<%= ResolveUrl("~/images/aceptar.jpg") %>" name="Image10" width="88" height="21" border="0"></asp:LinkButton></TD>
							</TR>
						</table>																									
						</td>										
				</TR>
			</TABLE>			
		</form>
	</body>
</HTML>

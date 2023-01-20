<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ActualizarTablas" CodeFile="ActualizarTablas.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Actualizar Nuevos Convenios</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>
		<script language="javascript">
		<!--
			var clicked = 0;

	
			function Valida ( obj, args ) {
				args.IsValid = confirm('¿Desea realizar la Actualización Nuevos Convenios?');
			}		
			
			//Grabar los datos de la orden de compra
			function Grabar(){
				if (notAllowConsecutiveButtonClicks()){
					__doPostBack('lnkEnviarPropuestas','');
				}
			}
		
			function notAllowConsecutiveButtonClicks() {
				if (clicked == 0) {
					clicked = 1;
				return true;
				} 
				else {
					alert("Se estan procesando los datos. Por favor, haz click en OK para continuar.");
					return false;
				}
			}
		-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" runat="server" Title="Actualizar Nuevos Convenios"></uc1:banner></td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="775">
							<tr>
								<td width="30">&nbsp;</td>
								<td width="700">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD colSpan="2"><br>
											</TD>
										</TR>
										<TR>
											<TD colSpan="2"><br>
											</TD>
										</TR>
										<TR>
											<TD colSpan="2"><asp:label id="lblMensaje" ForeColor="red" Runat="server"></asp:label><br>
											</TD>
										</TR>
										<TR>
											<TD colSpan="2"><br>
												<asp:customvalidator id="cvvalida" runat="server" ClientValidationFunction="Valida"></asp:customvalidator></TD>
										</TR>
										<TR>
											<TD colspan="2" vAlign="center" align="middle">
												<asp:Panel ID="lblActualizacionPropuestas" Runat="server" HorizontalAlign="Left"> <!--<a href="JavaScript:Grabar();" class="Caption" >Actualizar Información de Tarjetas de Crédito</a>-->
													<INPUT class="sbttn3" id="btnGrabar2" onclick="JavaScript:Grabar();" type="button" value="Actualizar Nuevos Convenios" name="btnRetornar">
													<asp:LinkButton id="lnkEnviarPropuestas" Runat="server" CssClass="Caption"></asp:LinkButton>
												</asp:Panel>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

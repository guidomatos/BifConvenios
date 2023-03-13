<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EnvioMail" CodeFile="EnvioMail.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Seleccione los correos electronicos</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript">
		
			function Cerrar() {
				window.close();
			}
			function SendMail(controls) {
				var a = controls.split(',');
				var i = 0;
				var anyChecked = false;
				for (i = 0; i <= a.length - 1; i++) {
					if (document.all(a[i]).checked) {
						anyChecked = true;
					}
				}

				if (!anyChecked) {
					alert('Debe seleccionar por lo menos un correo electronico.');
				}
				else {
					if (confirm('¿Desea enviar el correo electronico con el archivo de cuotas?')) {
						__doPostBack('lnkEnviarEmail', '');
					}
				}
			}
		
		</script>
	</HEAD>
	<body leftMargin="20" topMargin="10">
		<form id="Form1" method="post" runat="server">				
<%--			<asp:Panel id="pnlSwf" Runat="server" Visible="False">
				<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<SCRIPT language="VBScript">
							<!--
								Private i, x, MM_FlashControlVersion
								On Error Resume Next
								x = null
								MM_FlashControlVersion = 0
								var Flashmode
								FlashMode = False
								For i = 6 To 1 Step -1
									Set x = CreateObject("ShockwaveFlash.ShockwaveFlash." & i)
									
									MM_FlashControlInstalled = IsObject(x)
									
									If MM_FlashControlInstalled Then
										MM_FlashControlVersion = CStr(i)
										Exit For
									End If
								Next
								FlashMode = (MM_FlashControlVersion >= 6)
								If FlashMode = True Then
								document.write "<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"" width=""350"" height=""100""><param name=""movie"" value=""/BIFConvenios/swf/load.swf""><param name=quality value=high><embed src=""/BIFConvenios/swf/load.swf"" width=""350"" height=""100"" quality=high pluginspage=""http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"" type=""application/x-shockwave-flash""></embed></object>"
								Else
								document.write "Procesando..."
								End If
								-->
							</SCRIPT>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>		
			<asp:Panel Runat="server" ID="pnlMensaje" visible="False">
				<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD align="middle">
							<asp:Label id="lblMensaje" Runat="server" CssClass="Subhead"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:Panel>		
			<asp:Panel ID="pnlClose" Runat="server" Visible="False">
				<SCRIPT language="javascript">	
				    alert("Correo enviado exitosamente")
	        	    window.close ();	
				</SCRIPT>
			</asp:Panel>	--%>			
			
		   <%-- <asp:Panel Runat="server" ID="pnlMail" visible="False">--%>
			<table class="TableSmooth" height="300" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td class="SubHead" colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td class="Text" colSpan="2">&nbsp; Generación de archivo de descuentos - Envió de 
						archivo vía e-mail</td>
				</tr>
				<tr>
					<td class="Text" colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<table width="100%" border="0">
							<tr>
								<td class="SubHead">&nbsp;&nbsp;&nbsp;Empresa
								</td>
								<td class="Normal"><asp:label id="lblCliente" Runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2" class="SubHead"><br>
						&nbsp;&nbsp;&nbsp;&nbsp;Cuerpo del correo<br>
					</td>
				</tr>
				<TR>
					<TD align="middle" colSpan="2"><asp:textbox id="txtComentario" runat="server" Width="400px" TextMode="MultiLine" Height="61px"></asp:textbox></TD>
				<tr>
					<td class="Text" colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top" align="middle" colSpan="2" height="160"><asp:datagrid id="dgGen" runat="server" BorderColor="Silver" BackColor="White" BorderWidth="1px" CellPadding="4" Width="401px" AutoGenerateColumns="False">
							<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:CheckBox id="chk" runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Mail" ReadOnly="True" HeaderText="Correo Electr&#243;nico"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<td class="SubHead" align="middle">
									&nbsp;
									<asp:LinkButton Runat="server" ID="lnkEnviarEmail"></asp:LinkButton>
									<a href="#" onclick="Javascript:SendMail('<%=GetControlNames()%>')">Enviar email</a>
									&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="JavaScript:Cerrar();">Cerrar</a>
								</td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;
						<asp:Panel ID="pnlClose" Runat="server" Visible="False">
							<SCRIPT language="javascript">
				<!--
					window.close ();
				-->
							</SCRIPT>
						</asp:Panel>
					</td>
				</tr>
			</table>
			<%--</asp:Panel> --%>

		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EsperaFinalAD" CodeFile="EsperaFinalAD.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Procesando archivo de cuotas... por favor espere unos instantes mientras se 
			procesa en el servidor</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
	</HEAD>
	<body leftmargin="0" topmargin="0" bottommargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="pnlSwf" Runat="server" Visible="False">
				<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD>
							<script type="text/vbscript" language="VBScript">
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
							</script>
						</TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:Panel Runat="server" ID="pnlMensaje" Visible="False" HorizontalAlign="Center">
				<asp:Label ID="lblMensaje" Runat="server" CssClass="Normal"></asp:Label>
			</asp:Panel>
			<asp:Panel ID="pnlFinal" Visible="False" Runat="server">
				<script type="text/javascript" language="javascript">
            <!--
            //Mostramos los reportes de la carga
            	if ( window.opener != null ) {
            		window.opener.location.href = "reporteProcesoDescuentosResumen.aspx?id=<%=Pid%>";  
            		window.close();
            	}
            -->
				</script>
			</asp:Panel>
		</form>
	</body>
</HTML>

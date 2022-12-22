<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EsperaFinalEnvioAS400" CodeFile="EsperaFinalEnvioAS400.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Espere un momento, estamos enviando la información...</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<META http-equiv="Refresh" content="5">
		<META http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="-1">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="pnlSwf" Visible="False" Runat="server" BorderWidth=0 >
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
					<TR>
						<TD align=center>
							<asp:Label id="lblAvance" Runat="server"  Font-Names=Verdana Font-Size=8   Font-Bold=True ForeColor=Silver ></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:panel><asp:panel id="pnlMensaje" Visible="False" Runat="server">
				<asp:label id="lblMensaje" Runat="server"></asp:label>
			</asp:panel><asp:panel id="pnlFinal" Visible="False" Runat="server">
				<SCRIPT language="javascript">
            <!--
            //Mostramos los reportes de la carga
            	if ( window.opener != null ) {
            		window.opener.location.href = "ReporteProcesoDescuento.aspx";  
            		window.close();
            	}
            -->
				</SCRIPT>
			</asp:panel></form>
	</body>
</HTML>

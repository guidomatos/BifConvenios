<%@ Page Language="vb" AutoEventWireup="false" Inherits="EsperaFinalAnulacion" CodeFile="EsperaFinalAnulacion.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Espere un momento...</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<META HTTP-EQUIV="Refresh" CONTENT="5">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body leftmargin="0" topmargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:Panel id="pnlSwf" Runat="server" Visible="False">
<TABLE height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TR>
    <TD>
      <SCRIPT language="vbscript">
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
    </TD></TR></TABLE>
			</asp:Panel>
			<asp:Panel ID="pnlFinal" Visible="False" Runat="server">
            <SCRIPT language="javascript">
            
            //Mostramos los reportes de la carga
            	if ( window.opener != null ) {
            		window.opener.location.href = "ProcesarArchivoDescuento.aspx";  
            		window.close();
            	}
            
				</SCRIPT>
			</asp:Panel>
		</form>
	</body>
</HTML>

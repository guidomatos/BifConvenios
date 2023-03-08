<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EsperaFinalProceso.aspx.vb" Inherits="BIFConvenios.EsperaFinalProceso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
		<title>Procesando archivo... por favor espere unos instantes mientras se procesa en 
			el servidor</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
	</head>
	<body leftmargin="0" topmargin="0" bottommargin="0" rightmargin="0">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
				<tr>
					<td>
						<asp:Literal id="ltrlScript" runat="server" Visible="False"></asp:Literal>
						<asp:Label Runat="server" ID="lblMensaje"></asp:Label>
						<asp:Literal ID="lrtlSwf" Runat="server" Visible="False">
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
						</asp:Literal>
					</td>
				</tr>
			</table>
		</form>
	</body>
</html>

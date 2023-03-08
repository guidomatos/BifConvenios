<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ProcesarBloqueosTestWait" CodeFile="ProcesarBloqueosTestWait.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProcesarBloqueosTestWait</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script type="text/javascript" language="javascript">
			function send() {
				document.forms["frmWait"].submit();
			}
			
			function close() {
				top.document.all("divFrame").className = "hide1";
				top.document.all('dvData').className = 'show';
				top.document.all('fraProccess').src = '';
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Panel ID="pnlError" Runat="server" visible="False">
				<table cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td width="20" rowSpan="2">&nbsp;</td>
						<td class="PageTitle" colSpan="2">Bloqueo</td>
					</tr>
					<tr>
						<td>
							<asp:Label id="lblMensaje" Runat="server"></asp:Label></td>
					</tr>
				</table>
			</asp:Panel>
			<asp:Literal ID="lrtlSwf" Runat="server" Visible="False">
				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
					<tr>
						<td width="80" rowSpan="2">&nbsp;</td>
						<td><br>
							<br>
							<br>
							<br>
							<br>
						</td>
					</tr>
					<tr>
						<td vAlign="center">
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
								document.write "<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"" width=""350"" height=""100""><param name=""movie"" value=""../swf/load.swf""><param name=quality value=high><embed src=""../swf/load.swf"" width=""350"" height=""100"" quality=high pluginspage=""http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"" type=""application/x-shockwave-flash""></embed></object>"
								Else
								document.write "Procesando..."
								End If
								-->
							</script>
						</td>
					</tr>
				</table>
			</asp:Literal>
		</form>
		<form action="ProcesarBloqueos.aspx" method="post" id="frmWait" name="frmWait">
			<input type="hidden" name="pagares" runat="server" id="pagares" value="<%=pagares%>"> <input type="hidden" runat="server" name="proceso" id="proceso" value="<%=proceso%>" >
		</form>
		<asp:Literal id="ltrlScript" runat="server" Visible="False">
			<script type="text/javascript" language="javascript">
				<!--
					send ();
				-->
			</script>
		</asp:Literal>
	</body>
</HTML>

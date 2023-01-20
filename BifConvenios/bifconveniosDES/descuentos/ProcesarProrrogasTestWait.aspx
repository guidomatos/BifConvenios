<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ProcesarProrrogasTestWait" CodeFile="ProcesarProrrogasTestWait.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProcesarBloqueosTestWait</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language="javascript">
		<!--
			function send(){
				document.forms["frmWait"].submit();
			}
			function close(){
					top.document.all ( "divFrame").className = "hide1";
					top.document.all('dvData').className='show';	
					top.document.all('fraProccess').src='';			
			}			
		-->
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<asp:Panel ID="pnlError" Runat="server" visible="False">
				<TABLE cellSpacing="0" cellPadding="0" border="0">
					<TR>
						<TD width="20" rowSpan="2">&nbsp;</TD>
						<TD class="PageTitle" colSpan="2">Prorroga</TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="lblMensaje" Runat="server"></asp:Label></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:literal ID="lrtlSwf" Runat="server" Visible="False">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="80" rowSpan="2">&nbsp;</TD>
						<TD><BR>
							<BR>
							<BR>
							<BR>
							<BR>
						</TD>
					</TR>
					<TR>
						<TD vAlign="center">
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
								document.write "<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"" width=""350"" height=""100""><param name=""movie"" value=""../swf/load.swf""><param name=quality value=high><embed src=""../swf/load.swf"" width=""350"" height=""100"" quality=high pluginspage=""http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"" type=""application/x-shockwave-flash""></embed></object>"
								Else
								document.write "Procesando..."
								End If
								-->
							</SCRIPT>
						</TD>
					</TR>
				</TABLE>
			</asp:literal>
		</form>
		<form action="ProcesarProrrogas.aspx" method="post" id="frmWait" name="frmWait">
			<input type="hidden" runat ="server"  name="pagares" id="pagares" value="<%=pagares%>"> <input type="hidden" runat ="server" name="proceso" id="proceso" value="<%=proceso%>" >
		</form>
		<asp:Literal id="ltrlScript" runat="server" Visible="False">
			<SCRIPT language="javascript">
				<!--
					send ();
				-->
			</SCRIPT>
		</asp:Literal>
	</body>
</HTML>

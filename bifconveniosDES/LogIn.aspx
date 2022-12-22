<%@ Page Language="vb" AutoEventWireup="false" CodeFile="LogIn.aspx.vb" Inherits="BIFConvenios.LogIn"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LogIn</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td><FONT face="Arial" color="navy" size="2"><STRONG>Nombre de Usuario</STRONG></FONT></td>
					<td>
						<asp:TextBox id="txtUserName" runat="server"></asp:TextBox></td>
				</tr>
				<tr>
					<td><FONT face="Arial" color="navy" size="2"><STRONG>Contraseña</STRONG></FONT></td>
					<td>
						<asp:TextBox id="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
				</tr>
				<TR>
					<TD><FONT color="navy"><STRONG><FONT face="Arial"><FONT size="2"></FONT></FONT></STRONG></FONT></TD>
					<TD>
						<asp:ImageButton id="ibtnIngresar" runat="server" AlternateText="Ingresar"></asp:ImageButton></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>

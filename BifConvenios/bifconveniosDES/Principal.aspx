

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Principal.aspx.vb" Inherits="Principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Accesos</title>
</head>
<!-- DEBAJO DE ESTA LINEA VA LA IMAGEN DE FONDO url(imagen.jpg) -->
<body style="background-image: url(/Images/bg4.jpg); text-align:center; padding-top:0px; padding-left:80px" bgcolor="#cccccc">
    <form id="form1" runat="server">

    <!-- DEBAJO DE ESTA LINEA VA EL BANNER  src="bannerimagen.jpg"-->
    <div style="width:980px" align="left"><img src="/Images/Topbiflogo_convenios.JPG" align="center"/> </div>
      
    <div align="center">
    </div>
    <br />
    <br />
    <br />
    <br />
        <asp:label  ID="lblValida" runat="server" Text="NO TIENE" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Medium" Font-Underline="False" ForeColor="Blue"></asp:label>


    </form>
</body>
</html>

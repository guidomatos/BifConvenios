<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle_cuota_pagare.aspx.cs" Inherits="Detalle_cuota_pagare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Detalle Pagare</title>
   		<LINK href="css/global.css" type="text/css" rel="stylesheet">
		<LINK href="css/StyleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript"  type="text/javascript" src="js/global.js"></script>
		 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 24px">
                </td>
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" CssClass="Campo_Texto_label" Text="Nombre Cliente :"></asp:Label>
                    <asp:Label ID="lbl_nombre" runat="server" CssClass="Campo_Texto_label"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 24px">
                </td>
                <td colspan="2">
        <asp:GridView ID="gvw_deuda_total" runat="server" AutoGenerateColumns="False" CssClass="grilla_principal">
            <Columns>
                <asp:BoundField DataField="MONEDA" HeaderText="MONEDA" />
                <asp:BoundField DataField="ANIO" HeaderText="ANIO" />
                <asp:BoundField DataField="MES" HeaderText="MES" />
                <asp:BoundField DataField="MONTO_MES" HeaderText="MONTO_MES" />
                <asp:BoundField DataField="MONTO_PAGADO" HeaderText="MONTO_PAGADO" />
                <asp:BoundField DataField="ITF" HeaderText="ITF" />
            </Columns>
            <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
            <HeaderStyle CssClass="Grilla_Cabecera_principal" />
        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 24px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
        &nbsp;
    
    </div>
    </form>
</body>
</html>

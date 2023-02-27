<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Detalle_Cuota_ClienteEmpresa.aspx.cs" Inherits="Detalle_Cuota_ClienteEmpresa" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>BIF::Convenios</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../css/global.css" type="text/css" rel="stylesheet">
		<link href="../css/StyleSheet.css" type="text/css" rel="stylesheet">
		<link href="../css/MouseEvent.css" type="text/css" rel ="stylesheet" />
	     <script type="text/javascript" src="js/global.js"></script>
		
		<script language="javascript"  type="text/javascript">

		function grillaMouseOver(src,classOver) {
		    
            if (!src.contains(event.fromElement)) {
                src.style.cursor = 'hand';
                src.className = classOver;
            }
            }

            function grillaMouseOut(src,classIn) {
            if (!src.contains(event.toElement)) {
                src.style.cursor = 'default';
                src.className = classIn;
                
                
            }
           } 
            
            function Redirecciona(url)
            {
            var vValorRetorno;
                if(url != '')
                vValorRetorno = window.location = url;
            } 
           
          function AbrirDialogoPeriodoModalResultados(url)
            {
    
                var vValorRetorno;
            if(url != '')
            {
        
            vValorRetorno = window.open(url,"GG","width=450,height=500,scrollbars=yes,resizable=no ");
           
            }   
            
            vValorRetorno = null ;
            url = null;
        }
          
		</script>
		
</head>
<body>
    <form id="form1" runat="server">
   <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
				<td style="height: 302px"><uc1:banner id="Banner1" title="Detalle Cuota Empresa" runat="server"></uc1:banner></td>
				</tr>
    </table>  
        <table>
            <tr>
                <td style="width: 8px">
                </td>
                <td style="width: 100px">
        <asp:Label ID="lbl_error" runat="server"></asp:Label></td>
                <td style="width: 100px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="EMPRESA:"></asp:Label>
                    <asp:Label ID="LBL_EMPRESA_NOMBRE" runat="server"></asp:Label></td>
                <td style="width: 200px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td style="width: 100px">
                    <asp:Label ID="Label14" runat="server" CssClass="Campo_Texto_label_titulo" Text="Deuda Mes"></asp:Label></td>
                <td style="width: 100px">
                </td>
                <td style="width: 200px">
                    &nbsp;</td>
                <td style="width: 100px">
                    <asp:Label ID="Label13" runat="server" CssClass="Campo_Texto_label_titulo" Text="Deuda Total"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" CssClass="Campo_Texto_label" Text="Monto mes:"></asp:Label>
                    <asp:Label ID="lbl_monto_mes" runat="server" CssClass="Campo_Texto_label_resultado"></asp:Label></td>
                <td colspan="1" style="width: 200px">
                </td>
                <td colspan="1">
                    &nbsp;<asp:Label ID="Label1" runat="server" CssClass="Campo_Texto_label" Text="Monto Total :"></asp:Label>
                    <asp:Label ID="lbl_monto_total" runat="server" CssClass="Campo_Texto_label_resultado"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2">
                    <asp:Label ID="Label10" runat="server" CssClass="Campo_Texto_label" Text="Nro registros:"></asp:Label>
                    <asp:Label ID="lbl_nro_registro_mes" runat="server" CssClass="Campo_Texto_label_resultado"></asp:Label></td>
                <td colspan="1" style="width: 200px">
                </td>
                <td colspan="1">
                    &nbsp;<asp:Label ID="lbl_nroregistros" runat="server" CssClass="Campo_Texto_label" Text="Nro registros :"></asp:Label>
                    <asp:Label ID="lbl_cantidad_registros_total" runat="server" CssClass="Campo_Texto_label_resultado"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 8px; height: 16px">
                </td>
                <td colspan="2" rowspan="2" style="vertical-align: top">
                    <asp:GridView ID="gvw_deuda_mes" runat="server" AutoGenerateColumns="False" CssClass="grilla_principal">
                        <Columns>
                            <asp:BoundField DataField="PAGARE" HeaderText="PAGARE" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                            <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" />
                            <asp:BoundField DataField="ANIO" HeaderText="ANIO" />
                            <asp:BoundField DataField="MES" HeaderText="MES" />
                            <asp:BoundField DataField="SALDO_DEUDA" HeaderText="DEUDA" />
                        </Columns>
                        <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                        <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                    </asp:GridView>
                </td>
                <td colspan="1" rowspan="2" style="vertical-align: top; width: 200px">
                </td>
                <td colspan="1" rowspan="2" style="vertical-align: top">
                    <asp:GridView ID="gvw_deuda_total" runat="server" AutoGenerateColumns="False" CssClass="grilla_principal" OnRowDataBound="gvw_deuda_total_RowDataBound">
                        <Columns>
                        
                            <asp:BoundField DataField="PAGARE" HeaderText="PAGARE" />
                            <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                            <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" />
                            <asp:BoundField DataField="ANIO" HeaderText="ANIO" />
                            <asp:BoundField DataField="MES" HeaderText="MES" />
                            <asp:BoundField DataField="SALDO_DEUDA" HeaderText="DEUDA" />
                        </Columns>
                        <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                        <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2">
                    <asp:Label ID="Label2" runat="server" CssClass="Campo_Texto_label_titulo" Text="Clientes no tienen deuda mes:" Width="225px"></asp:Label></td>
                <td colspan="1" style="width: 200px">
                </td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td colspan="2"><asp:GridView ID="gvw_otros_pagares" runat="server" AutoGenerateColumns="False" CssClass="grilla_principal" OnRowDataBound="gvw_otros_pagares_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="PAGARE" HeaderText="PAGARE" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                        <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" />
                        <asp:BoundField DataField="ANIO" HeaderText="ANIO" />
                        <asp:BoundField DataField="MES" HeaderText="MES" />
                        <asp:BoundField DataField="SALDO_DEUDA" HeaderText="DEUDA" />
                    </Columns>
                    <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                    <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                </asp:GridView>
                </td>
                <td colspan="1" style="width: 200px">
                </td>
                <td colspan="1">
                </td>
            </tr>
            <tr>
                <td style="width: 8px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    
    </form>
</body>
</html>

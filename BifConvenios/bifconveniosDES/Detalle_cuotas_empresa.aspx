<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Detalle_cuotas_empresa.aspx.vb" Inherits="Detalle_cuotas_empresa" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BIF::Convenios</title>
   <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/global.css" type="text/css" rel="stylesheet">
		<LINK href="../css/StyleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript"  type="text/javascript" src="js/global.js"></script>
		
		 
</head>
<body  leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server" >
   <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
				<td><uc1:banner id="Banner1" title="Detalle Cuota Empresa" runat="server"></uc1:banner></td>
				</tr>
    </table>  
    
        <br />
        <table>
            <tr>
                <td style="width: 40px">
                </td>
                <td>
                    <asp:ScriptManager id="ScriptManager1" runat="server">
                    </asp:ScriptManager></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 40px">
                    &nbsp; &nbsp;
                </td>
                <td>
                    <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <contenttemplate>
<TABLE>
    <tr>
        <td style="width: 100px; height: 18px">
        </td>
        <td colspan="2" style="height: 18px">
        </td>
        <td style="height: 18px">
        </td>
        <td style="height: 18px">
        </td>
    </tr>
    <TR><TD style="WIDTH: 100px; height: 18px;"></TD><TD style="height: 18px"></TD><TD style="height: 18px;"></TD><TD style="height: 18px;"></TD>
        <td style="height: 18px">
        </td>
    </TR><TR><TD style="WIDTH: 100px"></TD><TD colspan="3">
    &nbsp;<asp:Label id="Label1" runat="server" Text="Añio:" CssClass="Campo_Texto_label"></asp:Label>
    <asp:DropDownList id="ddl_anio" runat="server" CssClass="Campo_Texto" Width="120px">
        <asp:ListItem Value="08">2008</asp:ListItem>
        <asp:ListItem Value="09">2009</asp:ListItem>
        <asp:ListItem Value="10">2010</asp:ListItem>
        <asp:ListItem Value="11">2011</asp:ListItem>
        <asp:ListItem Value="12">2012</asp:ListItem>
        <asp:ListItem Value="13">2013</asp:ListItem>
    </asp:DropDownList>
    &nbsp;
    <asp:Label id="Label2" runat="server" Text="Mes:" CssClass="Campo_Texto_label"></asp:Label>
    <asp:DropDownList id="ddl_mes" runat="server" CssClass="Campo_Texto" Width="120px">
        <asp:ListItem Value="1">ENERO</asp:ListItem>
        <asp:ListItem Value="2">FEBRERO</asp:ListItem>
        <asp:ListItem Value="3">MARZO</asp:ListItem>
        <asp:ListItem Value="4">ABRIL</asp:ListItem>
        <asp:ListItem Value="5">MAYO</asp:ListItem>
        <asp:ListItem Value="6">JUNIO</asp:ListItem>
        <asp:ListItem Value="7">JULIO</asp:ListItem>
        <asp:ListItem Value="8">AGOSTO</asp:ListItem>
        <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
        <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
        <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
        <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" CssClass="BotonBuscar" OnClick="Button1_Click"
        Text="Buscar" Width="80px" /></TD>
    <td>
    </td>
</TR><TR>
    <td colspan="5">
        <asp:GridView id="gvw_deuda_cliente" runat="server" AutoGenerateColumns="False" CssClass="grilla_principal">
    <Columns>
        <asp:BoundField DataField="Codigo" HeaderText="c&#243;digo">
            <ItemStyle Width="100px" />
        </asp:BoundField>
        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="MES" HeaderText="Mes" />
        <asp:BoundField DataField="ANIO" HeaderText="A&#241;io" />
        <asp:BoundField DataField="N_Clientes_Mes" HeaderText="No_CLIENTES_MES" />
        <asp:BoundField DataField="DEUDA_MES" HeaderText="DEUDA_MES" />
        <asp:BoundField DataField="N_total_clientes" HeaderText="No_TOTAL_CLIENTES" />
        <asp:BoundField DataField="DEUDA_TOTAL" HeaderText="DEUDA_TOTAL" />
        <asp:TemplateField>
       <ItemTemplate>
                    <a href ="Detalle_Cuota_ClienteEmpresa.aspx?EmpresaID=<%# Eval("Codigo")%>&mes=<%# Eval("MES")%>&anio=<%# Eval("ANIO")%>">Detalle</a> 
       </ItemTemplate> 
       
        </asp:TemplateField>
    </Columns>
    <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
    <HeaderStyle CssClass="Grilla_Cabecera_principal" />
</asp:GridView>
    </td>
</TR></TABLE>
</contenttemplate>
                    </asp:UpdatePanel></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 40px">
                </td>
                <td>
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    
    
    </form>
</body>
</html>

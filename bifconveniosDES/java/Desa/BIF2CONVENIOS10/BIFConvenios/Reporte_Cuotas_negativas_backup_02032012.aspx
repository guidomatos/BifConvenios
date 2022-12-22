<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reporte_Cuotas_negativas.aspx.cs" Inherits="Reporte_Cuotas_negativas" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>::BIFConvenios:: Reporte cuotas negativas</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="css/global.css" type="text/css" rel="stylesheet">
		<LINK href="css/StyleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript"  type="text/javascript" src="js/global.js"></script>
		
		  
</head>
<body>
    <form id="form1" runat="server">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                 <tr>
				    <td><uc1:banner id="Banner1" title="Listado cuotas negativas" runat="server"></uc1:banner></td>
				</tr>
    </table> 
        <table>
            <tr>
                <td style="width: 23px">
                </td>
                <td colspan="2">
        <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td style="width: 23px">
                </td>
                <td colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
<TABLE><TR><TD></TD><TD colSpan=4></TD></TR><TR><TD></TD><TD style="HEIGHT: 18px">&nbsp;<asp:Label id="Label1" runat="server" Text="Añio:" CssClass="Campo_Texto_label"></asp:Label><asp:DropDownList id="ddl_anio" runat="server" CssClass="Campo_Texto" Width="120px"><asp:ListItem>2009</asp:ListItem>
<asp:ListItem>2010</asp:ListItem>
<asp:ListItem>2011</asp:ListItem>
</asp:DropDownList></TD><TD style="HEIGHT: 18px"> <asp:Label id="Label2" runat="server" Text="Mes:" CssClass="Campo_Texto_label"></asp:Label> <asp:DropDownList id="ddl_mes" runat="server" CssClass="Campo_Texto" Width="120px"><asp:ListItem Value="1">Enero</asp:ListItem>
<asp:ListItem Value="2">Febrero</asp:ListItem>
<asp:ListItem Value="3">Marzo</asp:ListItem>
<asp:ListItem Value="4">Abril</asp:ListItem>
<asp:ListItem Value="5">Mayo</asp:ListItem>
<asp:ListItem Value="6">Junio</asp:ListItem>
<asp:ListItem Value="7">Julio</asp:ListItem>
<asp:ListItem Value="8">Agosto</asp:ListItem>
<asp:ListItem Value="9">Septiembre</asp:ListItem>
<asp:ListItem Value="10">Octubre</asp:ListItem>
<asp:ListItem Value="11">Noviembre</asp:ListItem>
<asp:ListItem Value="12">Diciembre</asp:ListItem>
</asp:DropDownList></TD><TD style="HEIGHT: 18px"> <asp:Button id="Button1" onclick="Button1_Click" runat="server" Text="Buscar" CssClass="BotonBuscar" Width="80px"></asp:Button></TD><TD></TD></TR><TR>
    <td colspan="5" rowspan="5">
    </td>
</TR>
    <tr>
    </tr>
    <TR></TR>
    <tr>
    </tr>
    <tr>
    </tr>
</TABLE>
                            <table>
                                <tr>
                                    <td style="height: 18px">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="13px">CUOTAS EN NEGATIVAS IBS CON SALDO CAPITAL MAYOR A CERO</asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
            <asp:GridView id="gvw_negativos_ibs" runat="server" CssClass="grilla_principal" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="DLEDSC" HeaderText="Empresa">
                        <ItemStyle Width="100px"  />
                    </asp:BoundField>
                    <asp:BoundField DataField="DLNP" HeaderText="Pagare" />
                    <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE" />
                    <asp:BoundField DataField="ANIO" HeaderText="A&#241;io" />
                    <asp:BoundField DataField="MES" HeaderText="MES" />
                    <asp:BoundField DataField="CUOTA_COBRAR" HeaderText="CUOTA_COBRAR" />
                </Columns>
                <RowStyle CssClass="Grilla_Filas_Principal_deuda"  />
                <HeaderStyle CssClass="Grilla_Cabecera_principal"  />
            </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;<asp:Label ID="lbl_bifconvenios" runat="server" Font-Bold="True" Font-Size="13px">CONVENIOS SIN SALDO CAPITAL (No depende del mes de selecciòn de bùsqueda)</asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
    <asp:GridView id="gvw_deuda_cliente" runat="server" CssClass="grilla_principal" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="DLNCC" HeaderText="Empresa">
            <ItemStyle Width="100px"  />
        </asp:BoundField>
        <asp:BoundField DataField="DEAACC" HeaderText="Pagare" ></asp:BoundField>
        <asp:BoundField DataField="DLNCL" HeaderText="NOMBRE" ></asp:BoundField>
        <asp:BoundField DataField="DEAPRI" HeaderText="CAPITAL" ></asp:BoundField>
        <asp:BoundField DataField="DEAMEI" HeaderText="INTERES" ></asp:BoundField>
        <asp:BoundField DataField="DEAMEM" HeaderText="MORA" ></asp:BoundField>
    </Columns>
    <RowStyle CssClass="Grilla_Filas_Principal_deuda"  />
    <HeaderStyle CssClass="Grilla_Cabecera_principal"  />
</asp:GridView> 
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 23px">
                </td>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

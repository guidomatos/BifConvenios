<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaCuotasCliente.aspx.vb" 
    explicit="true" Inherits="frmConsultaCuotasCliente" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>    

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>
<%@ Register src="controls/Pager.ascx" TagName="pager" TagPrefix="custom" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>BIFConvenios - Consulta de Cuotas</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>
        
    <style type="text/css">
        
    .ui-dialog-title
    {
	    font-size:14px;
    }    
    .ui-button-text
    {
	    font-size:14px;
    }
    .ui-dialog-content
    {
	    font-size:12px;
    }
    #container
    {
        display:table;
        border:dashed 1px gray;
        margin: 30px 30px;
    }
    .row
    {
        display:table-row;
    }
    .cell
    {
        display:table-cell;
    }
    .containercell
    {
        padding:1px;
    }
    #search
    {
        display:table;
        width:100%;
        height:30px;
    }
    .searchborder
    {
        border: 1px solid black;
        padding: 5px;
    }
    .button
    {
        font-weight:bold;
        color:#FFFFFF;
        background-color:#555555;
        border-style:solid;
        border-color:#000000;
        border-width:1px;
        height:25px;
    }
    .textbox 
    {
        padding:2px 2px 2px 2px;
        border:1px solid #CCCCCC;  
        height:15px;      
    }
    .label
    {
        padding:4px 4px 4px 22px;         
        height:15px;  
    }   
    
    </style>
</head>
<body style="margin-left:0; margin-top:0; margin-right:0;">
    <form id="form1" method="post" runat="server">
        <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:Banner id="Banner1" runat="server" Title="Consulta de Cuotas" />
                </td>
            </tr>            
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:1000px;">            
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">                                
                        <div style="width:1000px; background-color:#e6f5ff;">
                            <div class="row">
                                <div class="cell">                                    
                                     <table>
                                        <tr>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleEmpresa" runat="server" Font-Bold="true" Width="150" Text="Empresa:" />                                                            
                                            </td>
                                            <td style="width:650px;">
                                                <asp:literal id="ltrlCliente" runat="server" />
                                            </td>
                                        </tr>
                                     </table>                                
                                </div>                            
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleDocumento" Font-Bold="true" runat="server" Width="150" Text="Documento:" />                                                            
                                            </td>
                                            <td style="width:650px;">
                                                <asp:literal id="ltrlDocumento" runat="server" />
                                            </td>                                            
                                        </tr>
                                    </table>                                
                                </div>
                            </div>
                            <div class="row">
                                 <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitlePeriodo" Font-Bold="true" runat="server" Width="150" Text="Periodo:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:literal id="ltrlPeriodo" runat="server" />
                                            </td>
                                        </tr>
                                    </table>                                         
                                 </div>
                            </div>
                        </div>
                     </div>
                     <br />
                    <div class="searchborder">
                        <div id="search" style="width:1000px; background-color:#e6f5ff;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:800px;">
                                                <asp:Label  ID="Label1" Text="Buscar por:" runat="server" CssClass="label" Width="120px" />
                                                <asp:DropDownList ID="ddlCriterio" runat="server" Width="160px" Height="25px" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                                    <asp:ListItem Value="1">Número Pagaré</asp:ListItem>
							                        <asp:ListItem value="2">Trabajador</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label2" Text="Valor:" runat="server" CssClass="label" Width="60px" />
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Width="180px" Enabled="false" />
                                                <asp:Button ID="btnSearch" runat="server" Text="Filtrar" CssClass="button" />
                                            </td>
                                            <td style="width:200px;" align="right">
                                                <asp:linkbutton id="lnkBack" Runat="server" Height="15px">
                                                    <img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Regresar' />
                                                </asp:linkbutton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>                        
                    </div>                    
                </div>
            </div>
            <asp:Panel ID="pnlQueryResult" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvQuery" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    AllowSorting="true"
                                    OnPageIndexChanging="gvQuery_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="100" DataField="DLECC" HeaderText="Codigo IBS de la Empresa" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="DLENP" HeaderText="Numero Pagaré" ItemStyle-HorizontalAlign="center" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="FECHADESEMBOLSO" HeaderText="Fecha Desembolso" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField ItemStyle-Width="250" DataField="DLENE" HeaderText="Trabajador" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="DLEMO" HeaderText="Moneda" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="MONTOORIGINAL" HeaderText="Monto Original" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="CUOTASPACTADAS" HeaderText="Cuotas Pactadas" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="CUOTASPAGADAS" HeaderText="Cuotas Pagadas" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="CUOTASPENDIENTES" HeaderText="Cuotas Pendientes" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="FechaCargo" HeaderText="Fecha Cargo" />
                                        <asp:BoundField ItemStyle-Width="100" DataField="DLEIC" HeaderText="Importe" />
                                    </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="center" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="true" ForeColor="white" />
                                    <HeaderStyle BackColor="black" Font-Bold="true" ForeColor="white" />
                                    <AlternatingRowStyle BackColor="#cccccc" />
                                </asp:GridView>                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <div class="searchborder">
                            <div class="search">
                                <div class="row">
                                    <div class="cell">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red" />
                                        <asp:Literal ID="ltrlScript" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

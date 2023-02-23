<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios._default" CodeFile="default.aspx.vb" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Fechas de Cargo por Empresa</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0"/>
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0"/>
    <meta name="vs_defaultClientScript" content="JavaScript"/>
    <meta http-equiv="Page-Enter" content="blendtrans(duration=1.0, transition=8)"/>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
    <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">--%>
		<LINK href="/css/global.css" type="text/css" rel="stylesheet">
		<%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
		<script language="javascript" src="/js/global.js" type="text/javascript"></script>
        
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
        padding:2px 2px 2px 3px;
        border:1px solid #CCCCCC;  
        height:25px;      
    }
    .label
    {
        padding:4px 4px 4px 22px;         
        height:25px;  
    }   
    
    </style>

</head>
<body style="margin-top:0; margin-left:0; margin-right:0;">
    <form id="Form1" method="post" runat="server">
        <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">            
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" Title="Fechas de Cargo por Empresa" runat="server" />
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:750px;">
            <asp:Panel ID="pnlListaClientesIBS" runat="server">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upListaClientesIBS" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDatosCarga" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="Vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvDatosCarga_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="450" DataField="DLEDSC" HeaderText="Nombre de Empresa" />
                                        <asp:TemplateField ItemStyle-Width="80" HeaderText="Año">
                                            <ItemTemplate>
                                                <%#NormalizaAnhio(DataBinder.Eval(Container.DataItem, "DLEAEN"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="80" HeaderText="Mes">
                                            <ItemTemplate>
                                                <%#BIFConvenios.Periodo.GetMonthByNumber(DataBinder.Eval(Container.DataItem, "DLEMEN"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="80" DataField="DLEDEN" HeaderText="Dia" />
                                    </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="center" />
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
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red"/>                                        
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

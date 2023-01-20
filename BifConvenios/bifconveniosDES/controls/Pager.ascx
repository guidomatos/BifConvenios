<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Pager.ascx.vb" Inherits="controls_Pager" %>
<div style="font-size:8pt; font-family:Verdana;">
    <div id="left" style="float:left; margin-top: 0.4em">
        <b>
            <span>Total <span><asp:Label ID="lblRecordCount" runat="server"></asp:Label></span> Registros encontrados </span>
        </b>
    </div>
    <div id="inherit" style="float:left; margin: auto 5.5em;">
        <span>Pagina </span>
        <asp:DropDownList ID="ddlPageNumber" runat="server" 
            AutoPostBack="true"></asp:DropDownList>
        <span> de</span>
        <asp:Label ID="lblTotalPages" runat="server" ></asp:Label>
        <span> paginas(s) </span>
    </div>
    <div id="right" style="float:left">
        <span>Mostrar </span>
        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true">
            <asp:ListItem Text="1" Value="1"></asp:ListItem>
            <asp:ListItem Text="5" Value="5"></asp:ListItem>
            <asp:ListItem Text="10" Value="10" Selected="true"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
            <asp:ListItem Text="25" Value="25"></asp:ListItem>
            <asp:ListItem Text="50" Value="50"></asp:ListItem>
        </asp:DropDownList>
        <span> Reg. por Pag.</span>
    </div>
</div>



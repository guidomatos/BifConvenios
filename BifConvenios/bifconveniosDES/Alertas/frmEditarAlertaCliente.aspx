<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEditarAlertaCliente.aspx.vb" Inherits="Alertas_frmEditarAlertaCliente" 
    EnableEventValidation="false"%>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>BIFConvenios - Mantenimiento de Alertas a Empresas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    <%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
    <script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
        
    <script type="text/javascript">
        function ValidarGuardar(cadena) {
            if (confirm('¿Desea guardar la información?')){
                document.all("hdGuardar").value = cadena;
                __doPostBack('lnkGuardar','');
            }
        }
        
        function BuscarAlerta() {
            var returnValue = OpenFormatPageDialog('frmListaAlertas.aspx',400,980);
            if (fctTrim(returnValue) != ''){
                document.all('hdCodAlerta').value = returnValue;                    
                __doPostBack('lnkCargarAlerta','');
            }
        }
        
        function OpenFormatPageDialog(url, height , width ) {
			    var returnValue = window.showModalDialog(url,'', 'dialogTop: 150px; dialogLeft: 150px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
			    if (typeof (returnValue) == "undefined"){
				    returnValue = '';
			    }
			    return returnValue;
	    }
    </script>
    
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
        height:20px;
    }
    .textbox 
    {        
        padding:4px 4px 4px 4px;
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
<body style="margin-left:0; margin-top:0;">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Mantenimiento de Alertas a Empresas"></uc1:Banner>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        <asp:HiddenField ID="hdCodAlerta" runat="server" />
        <asp:HiddenField ID="hdCodAlertaCliente" runat="server" />
        <asp:HiddenField ID="hdCodCliente" runat="server" />
        <asp:HiddenField ID="hdTipoAlerta" runat="server" />
        <asp:HiddenField ID="hdGuardar" runat="server" />
        <asp:LinkButton ID="lnkGuardar" runat="server" Visible="false"/>
        <asp:LinkButton ID="lnkCargarAlerta" runat="server" Visible="false" />
        <asp:LinkButton ID="lnkEliminar" runat="server" Visible="false" />
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:750px;">
            <div class="row">
                <div class="cell container">
                    <div class="searchborder">
                        <div id="search" style="background-color:#E6F5FF;">
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:" />                                                
                                            </td>
                                            <td style="width:80px;">
                                                <asp:TextBox ID="txtCodEmpresa" runat="server" Width="80px" Enabled="false" />
                                            </td>
                                            <td style="width:420px;">
                                                <asp:TextBox ID="txtNomEmpresa" runat="server" Width="420px" Enabled="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblAlerta" runat="server" Text="Alerta:" />
                                            </td>
                                            <td style="width:70px;">
                                                <asp:TextBox ID="txtCodAlerta" runat="server" Width="70px" Enabled="false"/>
                                            </td>
                                            <td style="width:430px;">
                                                <asp:TextBox ID="txtNomAlerta" runat="server" Width="430px" Enabled="false"/>
                                            </td>
                                            <td style="width:80px;">
                                                <asp:Button ID="btnBuscarAlerta" runat="server" CssClass="button" Text="Buscar" Width="80px" OnClientClick="BuscarAlerta();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblAsuntoAlerta" runat="Server" Text="Asunto:" />
                                            </td>
                                            <td style="width:505px;">
                                                <asp:TextBox ID="txtAsuntoAlerta" runat="server" Width="505px" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; vertical-align:text-top" align="right">
                                                <asp:Label ID="lblCuerpoAlerta" runat="server" Text="Cuerpo:" />
                                            </td>
                                            <td style="width:505px;">
                                                <asp:TextBox ID="txtCuerpoAlerta" runat="server" Width="505px" TextMode="MultiLine" Height="80px" Font-Names="arial" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblDiasAntes" runat="server" Text="Dias Antes:" />
                                            </td>
                                            <td style="width:50px;">
                                                <asp:TextBox ID="txtDiasAntes" runat="server" Width="50px" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblDiasDespues" runat="server" Text="Dias Después:" />
                                            </td>
                                            <td style="width:50px;">
                                                <asp:TextBox ID="txtDiasDespues" runat="server" Width="50px" />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAdjunto" runat="server" Text="Considerar Archivos Adjuntos" TextAlign="left" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Size="14" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" />
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

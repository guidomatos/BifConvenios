<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEditarAlertas.aspx.vb" Inherits="Alertas_frmEditarAlertas" 
    EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>BIFConvenios - Mantenimiento de Alertas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    <%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
    <script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        function ValidarGuardar(cadena){
            if (confirm('¿Desea guardar la información?')) {
                document.all("hdGuardar").value = cadena;
                
                __doPostBack('lnkGuardar','');
            }
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
        padding:4px 4px 4px 22px;
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
                    <uc1:Banner ID="Banner1" runat="server" Title="Mantenimiento de Alertas"></uc1:Banner>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        <asp:HiddenField ID="hdCodAlerta" runat="server" />
        <asp:HiddenField ID="hdGuardar" runat="server" />
        <asp:LinkButton ID="lnkGuardar" runat="server" Visible="false"/>
        <asp:LinkButton ID="lnkEliminar" runat="server" Visible="false" />        
        <%--<asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>--%>
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
                                                <asp:Label ID="lblTipoAlerta" runat="server" Width="100" Text="Tipo Alerta:" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:DropDownList ID="ddlTipoAlerta" runat="server" Width="150px" DataTextField="vDescripcion" DataValueField="vValor" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblNombreAlerta" runat="server" Width="100px" Text="Nombre:" />
                                            </td>
                                            <td style="width:350px;">
                                                <asp:TextBox ID="txtNombreAlerta" runat="server" Width="350px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; vertical-align:text-top;" align="right">
                                                <asp:Label ID="lblDescripcion" runat="server" Width="100px" Text="Descripción:" />
                                            </td>
                                            <td style="width:610px">
                                                <asp:TextBox ID="txtDescripcion" runat="server" Width="610px" Height="50px" TextMode="MultiLine" Font-Names="arial" />
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
                                                <asp:Label ID="lblMetadata1" runat="server" Width="100px" Text="Metadata:" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:DropDownList ID="ddlMetadataAsunto" runat="server" Width="150px" DataTextField="vDescripcion" DataValueField="vValor" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:Button ID="btnAgregar1" runat="server" CssClass="button" Text="Agregar" Width="100px" />
                                            </td>
                                            <td style="width:345px;" align="left">
                                                <asp:Label ID="lblNota1" runat="server" Width="345px" ForeColor="red" Text="Nota: Seleccionar un metadato, y agregar al ASUNTO." />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; vertical-align:text-top;" align="right">
                                                <asp:Label ID="lblAsunto" runat="server" Width="100px" Text="Asunto:" />
                                            </td>
                                            <td style="width:600px;">
                                                <asp:TextBox ID="txtAsunto" runat="server" Width="610px" />
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
                                            <td style="width:100px; height: 24px;" align="right">
                                                <asp:Label ID="lblMetadata2" runat="server" Width="100px" Text="Metadata:" />
                                            </td>
                                            <td style="width:150px; height: 24px;">
                                                <asp:DropDownList ID="ddlMetadataCuerpo" runat="server" Width="150px" DataTextField="vDescripcion" DataValueField="vValor" />
                                            </td>
                                            <td style="width:100px; height: 24px;">
                                                <asp:Button ID="btnAgregar2" runat="server" CssClass="button" Text="Agregar" Width="100px" />
                                            </td>
                                            <td style="width:345px; height: 24px;" align="left">
                                                <asp:Label ID="lblNota2" runat="server" Width="345px" ForeColor="red" Text="Nota: Seleccionar un metadato, y agregar al CUERPO." />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; vertical-align:text-top;" align="right">
                                                <asp:Label ID="lblCuerpo" runat="server" Width="100px" Text="Cuerpo:" />
                                            </td>
                                            <td style="width:610px;">
                                                <asp:TextBox ID="txtCuerpo" runat="server" Width="610px" TextMode="MultiLine" Height="80" Font-Names="arial" MaxLength="5000"/>
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
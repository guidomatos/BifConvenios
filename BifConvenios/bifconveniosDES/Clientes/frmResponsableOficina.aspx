<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResponsableOficina.aspx.vb" Inherits="Clientes_frmResponsableOficina"
    EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BIFConvenios - Responsable de Oficina</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />

    <script language="javascript" src="../js/global.js"
        type="text/javascript"></script>

    <script type="text/javascript">
    
            function BuscarOficina(){
                var returnValue = OpenFormatPageDialog('frmListadoOficina.aspx',400,670);
                
                if (fctTrim(returnValue) != ''){
                    document.all('hdId').value = returnValue;
                    __doPostBack('lnkCargarOficinaIBS','');
                }
            }
            
            function OpenFormatPageDialog(url, height , width ) {
					var returnValue = window.showModalDialog(url,'', 'dialogTop: 250px; dialogLeft: 300px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
					if (typeof (returnValue) == "undefined"){
						returnValue = '';
					}
					return returnValue;
			}
		
			function Valida (obj, args ){
				args.IsValid = true;					
				
				if ( args.IsValid ) {
					args.IsValid = confirm ("¿Desea actualizar la información de los Responsables de la Oficina?");
				}
			}
			
			function EditarResponsable(idResponsable) {
			    document.all("hdResponsableId").value = idResponsable;		    
			    
			    __doPostBack('lnkEditar','');
			}
			
			function EliminarResponsable(idResponsable) {
			    if (confirm('Desea eliminar el Responsable ?')) {
			        document.all("hdResponsableId").value = idResponsable
			        
			        __doPostBack('lnkEliminar','');
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
        height:150px;
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
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Mantenimiento de Responsable de Oficina"></uc1:Banner>
                </td>
            </tr>            
        </table>
        <asp:HiddenField ID="hdId" runat="server" />
        <asp:HiddenField ID="hdResponsableId" runat="server" />        
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        
        <asp:LinkButton ID="lnkEditar" runat="server" Visible="false"/>
        <asp:LinkButton ID="lnkEliminar" runat="server" Visible="false" />
        
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>        
        
        <div id="container" style="width:800px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search" style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTitResponsable" runat="server" Font-Bold="true" Font-Size="14" Text="Datos del Representante de Oficina" /> 
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
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblOficina" runat="server" Width="150px" Text="Oficina:" Font-Bold="true" />
                                            </td>
                                            <td style="width:50px;">
                                                <asp:TextBox ID="txtCodOficina" runat="server" Width="50px" MaxLength="4" Enabled="false" />
                                            </td>
                                            <td style="width:300px;">
                                                <asp:TextBox ID="txtNomOficina" runat="server" Width="300px" MaxLength="100" Enabled="false" />
                                            </td>
                                            <td style="width:120px;" align="left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Seleccionar..." CssClass="button" OnClientClick="BuscarOficina();" Width="115px" Enabled="false" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkCargarOficinaIBS" runat="server" Visible="false" />
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
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblNombreResponsable" runat="server" Width="150px" Text="Nombre Responsable:" Font-Bold="true" />
                                            </td>
                                            <td style="width:360px;">
                                                <asp:TextBox ID="txtNombreResponsable" runat="server" Width="360px" Enabled="false" />
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
                                                <asp:Label ID="lblCorreoResponsable" runat="server" Width="150px" Text="Correo Responsable:" Font-Bold="true" />
                                            </td>
                                            <td style="width:360px;">
                                                <asp:TextBox ID="txtCorreoResponsable" runat="server" Width="360px" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>                    
                </div>                
            </div>
            <br />
            <div class="row">
                <div class="cell">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnNuevo" runat="server" CssClass="button" Text="Nuevo" />
                            </td>
                            <td>
                                <asp:Button ID="btnRegresar" runat="server" CssClass="button" Text="Regresar" />
                            </td>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="cell container">
                    <asp:UpdatePanel ID="upQuery" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvResponsables" runat="server" CellPadding="3" ForeColor="black"
                                GridLines="vertical" EnableViewState="false" BackColor="White"
                                BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="5"
                                
                                AllowSorting="true">
                                <Columns>
                                    <asp:BoundField DataField="iResponsableId" HeaderText="Codigo" HeaderStyle-Width="80px" Visible="false" />
                                    <asp:BoundField DataField="iOficinaId" HeaderText="Cod. Oficina" HeaderStyle-Width="80px" Visible="false"/>
                                    <asp:BoundField DataField="vOficina" HeaderText="Oficina" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="vNombreResponsable" HeaderText="Responsable" HeaderStyle-Width="200px" />
                                    <asp:BoundField DataField="vCorreoResponsable" HeaderText="Correo Electronico" HeaderStyle-Width="200px" />                                    
                                    <asp:TemplateField ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <a href='JavaScript:EditarResponsable("<%#Databinder.Eval (Container.DataItem, "iResponsableId") %>");'>Editar</a>    
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <a href='JavaScript:EliminarResponsable("<%#Databinder.Eval (Container.DataItem, "iResponsableId") %>");'>Eliminar</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
            <br />
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
<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.cargageneracioncf"
    Explicit="True" CodeFile="cargageneracioncf.aspx.vb" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Obtener Cuotas para Envío a Empresa</title>
    
    <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
			
			function ProcesaCarga (codigoCliente, nombreCliente, mes, anhio, fechaProcesoAS400, monthName ){
				if ( confirm('Desea cargar los importes del Convenio de la empresa: ' + nombreCliente  + '\npara el mes de ' + monthName +  ' del '  + anhio + ', cargado el ' + fechaProcesoAS400.substring(6, 8) + '/'+ fechaProcesoAS400.substring(4, 6) +'/'+  fechaProcesoAS400.substring(0, 4) )) {
					document.all('hdcodigoCliente').value = codigoCliente;
					document.all('hdmes').value = mes;
					document.all('hdanhio').value = anhio;
					document.all('hdfechaProcesoAS400').value = fechaProcesoAS400;
					__doPostBack('lnkProcesar','');
				}
			}
			
			function ProcesaConsulta(CodigoCliente, NombreCliente, mes, anhio, FechaProcesoAS400, monthName){
			    if ( confirm('Desea Consultar los Importes para el mes de\n' + monthName + ' del ' + anhio + ', del convenio de:\n\n' + NombreCliente + '. \n\nNota: No se carga las consultas en el Convenio.')){
			        document.all('hdcodigoCliente').value = CodigoCliente;
					document.all('hdmes').value = mes;
					document.all('hdanhio').value = anhio;
					document.all('hdfechaProcesoAS400').value = FechaProcesoAS400;
					__doPostBack('lnkConsultar','');
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
        margin: 10px 30px 30px;
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
<body style="margin-left:0; margin-top:0; margin-right:0;">
    <form id="Form1" method="post" runat="server">
        <input type="hidden" id="hdcodigoCliente" name="hdcodigoCliente" runat="server" />
        <input type="hidden" id="hdmes" name="hdmes" runat="server" />
        <input type="hidden" id="hdanhio" name="hdanhio" runat="server" />
        <input type="hidden" id="hdfechaProcesoAS400" name="hdfechaProcesoAS400" runat="server" />
        
        <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:Banner id="Banner1" runat="server" Title="Obtener cuotas para envío a Empresa" />
                </td>
            </tr>
        </table>
        <asp:LinkButton ID="lnkProcesar" runat="server" />
        <asp:LinkButton ID="lnkReprocesar" runat="server" />
        <asp:LinkButton ID="lnkConsultar" runat="server" />
        <div id="actualizacionBatch" style="width:750px;margin: 10px 30px 10px;">
            <asp:Label ID="lblUltimaActualizacionProcesoBatch" runat="server" Text="" width="100%" style="text-align: right; padding-right: 10px; color:red"></asp:Label> 
        </div>
        <div id="container" style="width:750px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search">
                            <div class="row">
                                <div class="cell">
                                    <asp:Label  ID="lblCriterio" Text="Buscar por:" runat="server" CssClass="label" Width="120px" />
                                    <asp:DropDownList ID="ddlCriterio" runat="server" Width="160px" AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                        <asp:ListItem Value="1">Empresa</asp:ListItem>
							            <asp:ListItem value="2">Codigo IBS</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblValor" Text="Valor:" runat="server" CssClass="label" Width="60px" />
                                    <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Width="180px" Enabled="false" />
                                    <asp:Button ID="btnSearch" runat="server" Text="Filtrar" CssClass="button" />
                                </div>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlInputProceso" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upProcesos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvDatosCarga" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="Vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvDatosCarga_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="450" DataField="CUSNA1" HeaderText="Cliente" />
                                        <asp:BoundField ItemStyle-Width="80" DataField="CUSCUN" HeaderText="Codigo IBS" />
                                        <asp:TemplateField ItemStyle-Width="180" HeaderText="Periodo Consulta">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# BIFConvenios.Periodo.GetMonthByNumber(DataBinder.Eval(Container, "DataItem.DLEMP"))  +  " " + DataBinder.Eval(Container, "DataItem.DLEAP")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha de Carga" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="140">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%#BIFConvenios.Utils.GetFechaCanonica( DataBinder.Eval(Container, "DataItem.DLEFP")) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="140" ItemStyle-HorizontalAlign="center" HeaderText="Acciones">
                                            <ItemTemplate>
                                                <%#GetMensajeProceso(DataBinder.Eval(Container.DataItem, "CUSTID"), DataBinder.Eval(Container.DataItem, "CUSIDN"), DataBinder.Eval(Container.DataItem, "CUSNA1"), DataBinder.Eval(Container.DataItem, "DLEMP"), DataBinder.Eval(Container.DataItem, "DLEAP"), DataBinder.Eval(Container.DataItem, "DLEFP"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="140" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <%#GetMensajeConsulta(DataBinder.Eval(Container.DataItem, "CUSTID"), DataBinder.Eval(Container.DataItem, "CUSIDN"), DataBinder.Eval(Container.DataItem, "CUSNA1"), DataBinder.Eval(Container.DataItem, "DLEMP"), DataBinder.Eval(Container.DataItem, "DLEAP"), DataBinder.Eval(Container.DataItem, "DLEFP"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
            <asp:Panel ID="pnlReprocesar" Visible="false" runat="server">
                <div class="row">
                    <div class="cell container">
                        <div class="searchborder">
                            <div class="search">
                                <div class="row">
                                    <div class="cell">
                                        <asp:Label ID="lblInfoProceso" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="cell">
                                        <input id="hdIdProcess" type="hidden" runat="server" />
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

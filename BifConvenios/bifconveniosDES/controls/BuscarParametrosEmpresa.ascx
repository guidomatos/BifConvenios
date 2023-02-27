<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BuscarParametrosEmpresa.ascx.vb" Inherits="controls_BuscarParametrosEmpresa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript">
    function Seleccionar() {
        var resultadoSeleccion = '';

        var GridView = document.getElementById('<%=dgDatos.ClientID %>');
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            if (inputList[i].type == "radio") {
                if (inputList[i].checked) {
                    resultadoSeleccion = inputList[i].value;
                    break;
                }
            }
        }

        if (resultadoSeleccion == '') {
            alert('Seleccione una empresa y periodo');
            return false;
        }

        document.getElementById('<%=hfResultadoBusqueda.ClientID %>').value = resultadoSeleccion;
        document.getElementById('<%=BtnSeleccionarRegistro.ClientID%>').click();
	}
	
	function Cancelar() {
        document.getElementById('<%=btnCerrarPanel.ClientID%>').click();
    }
</script>

<link href="<%=ResolveUrl("~/css/global.css") %>" rel="stylesheet" type="text/css" />
<script src='<%=ResolveUrl("~/js/global.js") %>' type="text/javascript"></script>

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
        margin: 20px 10px;
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
        padding:4px 4px 4px 4px;
        border:1px solid #CCCCCC;  
        height:15px;      
    }
    .label
    {        
        padding:4px 4px 4px 4px;         
        height:15px;        
    }
</style>

<asp:ImageButton ID="btnVerPanel" ImageUrl="../images/texto.gif" AlternateText="Buscar" runat="server" />
<asp:Panel ID="pnlBuscarParametroEmpresa" runat="server">
	<div id="container" style="width:800px;">
		<div class="row">
            <div class="cell containercell">
                <div class="searchborder">
					<div class="search" style="background-color:#E6F5FF;">
						
						<div class="row">
                            <div class="cell">
                                <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <h2>
                                                <asp:Literal ID="ltrTitulo" Text="Selección de Empresa y Periodo" runat="server" />
                                            </h2>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
						<br />
                        <div class="row">
                            <div class="cell">
								<table border="0" cellpadding="0" width="100%">
                                    <tr>
										<td class="SubHead">Año</td>
										<td width="650">
											<asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" DataTextField="Anio_periodo" DataValueField="Anio_periodo" Width="200px"></asp:DropDownList></td>
									</tr>
									<tr>
										<td class="SubHead">Mes</td>
										<td width="650"><asp:DropDownList id="ddlMes" runat="server" AutoPostBack="True" DataTextField="MonthName" DataValueField="MonthOrder" Width="200px"></asp:DropDownList></td>
									</tr>
									<tr>
										<td colspan="2" class="SubHead">
											Empresas
										</td>
									</tr>
                                </table>
							</div>
						</div>

						<div class="row">
                            <div class="cell">
								<table border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="CommandButton"><a href="javascript:Seleccionar();">Seleccionar</a>&nbsp;</td>
										<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a></td>
									</tr>
								</table>
							</div>
						</div>

						<div class="row">
							<div class="cell container">
								<asp:GridView ID="dgDatos" runat="server" Width="100%" CellPadding="3" ForeColor="Black"
                                    GridLines="Vertical" EnableViewState="False" BackColor="White"
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="15"
                                    AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField>
											<ItemTemplate>
												<input type="radio" id="rData" name="rData" value='<%#DataBinder.Eval(Container.DataItem, "Codigo_proceso")%>|<%#DataBinder.Eval(Container.DataItem, "Anio_periodo")%>|<%#DataBinder.Eval(Container.DataItem, "Mes_Periodo")%>|<%#DataBinder.Eval(Container.DataItem, "Codigo_Cliente")%>|<%#DataBinder.Eval(Container.DataItem, "Nombre_Cliente")%>|<%#BIFConvenios.Periodo.GetMonthByNumber(DataBinder.Eval(Container.DataItem, "Mes_Periodo"))%> <%#DataBinder.Eval(Container.DataItem, "Anio_periodo")%>|<%#DataBinder.Eval(Container.DataItem, "TipoDocumento")%>|<%#DataBinder.Eval(Container.DataItem, "NumeroDocumento")%>|<%#DataBinder.Eval(Container.DataItem, "FInicial")%>|<%#DataBinder.Eval(Container.DataItem, "FFinal")%>'>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre Cliente"></asp:BoundField>
										<asp:TemplateField HeaderText="Periodo">
											<ItemTemplate>
												<%#BIFConvenios.Periodo.GetMonthByNumber(DataBinder.Eval(Container.DataItem, "Mes_Periodo"))%>
												<%#DataBinder.Eval(Container.DataItem, "Anio_periodo")%>
											</ItemTemplate>
										</asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#CCCCCC" />
                                </asp:GridView>
							</div>
						</div>

						<div class="row">
                            <div class="cell container">
                                <div class="searchborder">
                                    <div class="search" style="background-color:#E6F5FF;">
                                        <div class="row">
                                            <div class="cell">
                                                <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="height: 40px" align="center">
                                                            <h3>
                                                                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Size="12pt" />
                                                            </h3>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

						<div class="row">
                            <div class="cell">
								<span class="SubHead">Número de Registros&nbsp;:&nbsp;</span>
								<asp:Label ID="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label>
							</div>
						</div>

						<div class="row">
                            <div class="cell">

								<table border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="CommandButton"><a href="javascript:Seleccionar();">Seleccionar</a>&nbsp;</td>
										<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a></td>
									</tr>
								</table>
								
							</div>
						</div>
		
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Panel>

<asp:Button ID="BtnSeleccionarRegistro" runat="server" style="display: none;" />
<asp:ImageButton ID="btnCerrarPanel" runat="server" style="display: none"  />
<asp:HiddenField ID="hfResultadoBusqueda" runat="server" />

<cc1:ModalPopupExtender ID="mpeBuscarParametroEmpresa" runat="server"
    TargetControlID="btnVerPanel"
    PopupControlID="pnlBuscarParametroEmpresa"
    CancelControlID="btnCerrarPanel"
    X="460" Y="200" />
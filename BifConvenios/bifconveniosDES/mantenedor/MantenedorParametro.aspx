<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MantenedorParametro.aspx.vb" Inherits="BIFConvenios.mantenedor_MantenedorParametro" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>::BIF Convenios ::</title>
        <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">-->
		<link href="~/css/global.css" type="text/css" rel="stylesheet">
		<!--<LINK href="<%=Request.ApplicationPath%>/css/StyleSheet.css" type="text/css" rel="stylesheet">-->
		<link href="~/css/StyleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript"  type="text/javascript" src="~/js/global.js"></script>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">

<script language="javascript" type="text/javascript">

    function ConfirmaEliminacion(sender) {
        return confirm('¿Está seguro que desea eliminar este registro ?');
    }
    
    function ConfirmaEliminacionHijo(sender) {
        return confirm('¿Está seguro que desea eliminar este registro ?');
    }

</script>

    <form id="form1" runat="server" method="post">

    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
				<td><uc1:banner id="Banner1" title="Mantenimiento Parámetros" runat="server"></uc1:banner></td>
				</tr>
    </table> 
    
        <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="lbl_resultado" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label>
    
   <br />
   <table cellSpacing="0" cellPadding="0" width="100%" border="0">
        <tr>
            <td style="width:10%">
                <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnNuevoPadre" runat="server"  Text="Nuevo Padre" CssClass="Boton" OnClick="btnNuevoPadre_Click"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width:90%">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel id="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="divAgregaPadre" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width:10%">
                                Descripción
                            </td>
                            <td style="width:2%">
                                :
                            </td>
                            <td style="width:48%">
                                <asp:TextBox ID="txtDescripcionF" runat="server" Text="" Width="90%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Valor
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtValorF" runat="server" Text="" Width="5%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Orden
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdenF" runat="server" Text="" Width="5%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="Boton" OnClick="btnGrabar_Click"/>
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Boton" OnClick="btnCancelar_Click"/>
                            </td>
                            <td>
                                <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
   </table>
   <br />
        <asp:UpdatePanel id="updMantenimiento" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvSytemParameter" runat="server" Width="95%" AutoGenerateColumns="False" 
                                OnRowCommand="gvSytemParameter_RowCommand" OnRowDeleting="gvSytemParameter_RowDeleting" 
                                OnRowEditing="gvSytemParameter_RowEditing" OnRowUpdating="gvSytemParameter_RowUpdating" 
                                OnRowCancelingEdit="gvSytemParameter_RowCancelingEdit" OnRowCreated="gvSytemParameter_RowCreated"
                                OnPageIndexChanging="gvSytemParameter_PageIndexChanging" AllowPaging="true" 
                                OnRowDataBound="gvSytemParameter_RowDataBound"
                                CssClass="grilla_principal" PageSize="10">
                	<PagerSettings Mode="NumericFirstLast" />			
                    <Columns>
                        <asp:TemplateField HeaderText="N&#176;">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNumero" runat="server" CommandArgument='<%# Eval("iGrupoId") & "," & Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# String.Format("{0:000000}", Eval("Correlativo")) %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="iGrupoId" headertext="Grupo" Visible="False" />
                        <asp:boundfield datafield="iParametroId" headertext="Parametro" Visible="False"  />
                        
                        <asp:TemplateField HeaderText="Descripci&#243;n">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescripcion" runat="server" readonly ="false" 
                                  MaxLength="200" Text='<%# Eval("vDescripcion") %>' Width="98%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Descripcion" runat="server" Value='<%#Eval("vDescripcion")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDescripcion" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("vDescripcion") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="55%" />
                            <ItemStyle Width="55%" />
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Valor">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" readonly ="false" 
                                  MaxLength="50000" Text='<%# Eval("vValor") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Valor" runat="server" Value='<%#Eval("vValor")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkValor" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("vValor") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Orden">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOrden" runat="server" readonly ="false" 
                                  MaxLength="2" Text='<%# Eval("iOrden") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Orden" runat="server" Value='<%#Eval("iOrden")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkOrden" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("iOrden") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="iEstado" headertext="Id Estado" Visible="False"/>
                        
                        <asp:TemplateField HeaderText="Estado">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstado" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="Hidd_Estado" runat="server" Value='<%#Eval("iEstado")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNombreEstado" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("NombreEstado") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="NUM_HIJOS" headertext="NroHijos" Visible="False"  />

                         <asp:TemplateField HeaderText="Editar">
                              <EditItemTemplate>
                                <asp:ImageButton ID="btnActualizar" runat="server" AlternateText="Actualizar" CommandName="update"
                                  ImageUrl="~/Images/Icono_PreCalificar_1.gif" ValidationGroup="GroupActivoFijoInmueble"
                                  Width="21px" />
                                <asp:ImageButton ID="btnCancelar" runat="server" AlternateText="Cancelar" CommandName="cancel"
                                  ImageUrl="~/Images/Icono_Cancelar_1.ico" Visible="true" />
                              </EditItemTemplate>
                              <ItemStyle HorizontalAlign="Center" Width="10%" />
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CommandName="edit"
                                  ImageUrl="~/Images/Editar.gif" Width="21px" />
                              </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Eliminar">
                              <ItemStyle HorizontalAlign="Center" Width="5%" />
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" CommandName="delete" CommandArgument='<%# Eval("iGrupoId") & "," & Eval("iParametroId") %>'
                                  ImageUrl="~/images/Icono_Cancelar_1.ico" Width="21px" OnClientClick="return ConfirmaEliminacion(this);"/>
                              </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                    <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNuevoPadre" 
                            EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

   <br />
   <br />
                   <asp:UpdatePanel id="UpdatePanel5" runat="server">
                    <ContentTemplate>  
 
   <div id="divHijo" runat="server">
        <table cellSpacing="0" cellPadding="0" width="100%" border="0">
       <tr>
            <td colspan="2">
          
                        <asp:Label ID="lblNombrePadre" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label>
              <asp:HiddenField id="hdnGrupo" runat="server"/>
              <asp:HiddenField id="hdnParametro" runat="server"/>
            </td>
       </tr>
        <tr>
            <td style="width:10%">
                <asp:UpdatePanel id="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnHijo" runat="server"  Text="Nuevo Hijo" CssClass="Boton" OnClick="btnNuevoHijo_Click"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td style="width:90%">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:UpdatePanel id="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <div id="divSeccionHijo" runat="server">
                    <table width="100%">
                        <tr>
                            <td style="width:10%">
                                Descripción
                            </td>
                            <td style="width:2%">
                                :
                            </td>
                            <td style="width:48%">
                                <asp:TextBox ID="txtDescripcionH" runat="server" Text="" Width="90%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Valor
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtValorH" runat="server" Text="" Width="5%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Orden
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdenH" runat="server" Text="" Width="5%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Visible
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVisibleH" runat="server" Width="15%">
                                            </asp:DropDownList>
                            </td>
                            <td style="width:40%">

                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha Inicio
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtFInicioH" runat="server" Text="" Width="15%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha Fin
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtFFinH" runat="server" Text="" Width="15%">
                                </asp:TextBox>
                            </td>
                            <td style="width:40%">
                            
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right">
                                <asp:Button ID="btnGrabarHijo" runat="server" Text="Grabar" CssClass="Boton" OnClick="btnGrabarHijo_Click"/>
                                <asp:Button ID="btnCancelarHijo" runat="server" Text="Cancelar" CssClass="Boton" OnClick="btnCancelarHijo_Click"/>
                            </td>
                            <td>
                                <asp:Label ID="lblMensajeHijo" runat="server" ForeColor="red" Font-Bold="true" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
   </table>
   <br />
   
                   <asp:GridView ID="gvHijos" runat="server" Width="95%" AutoGenerateColumns="False" 
                                OnRowCommand="gvHijos_RowCommand" OnRowDeleting="gvHijos_RowDeleting" 
                                OnRowEditing="gvHijos_RowEditing" OnRowUpdating="gvHijos_RowUpdating" 
                                OnRowCancelingEdit="gvHijos_RowCancelingEdit" OnRowCreated="gvHijos_RowCreated"
                                OnPageIndexChanging="gvHijos_PageIndexChanging" AllowPaging="true" 
                                OnRowDataBound="gvHijos_RowDataBound"
                                CssClass="grilla_principal" PageSize="10">
                	<PagerSettings Mode="NumericFirstLast" />			
                    <Columns>
                        <asp:TemplateField HeaderText="N&#176;">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNumero" runat="server" CommandArgument='<%# Eval("iGrupoId") & "," & Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# String.Format("{0:000000}", Eval("Correlativo")) %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Right" Width="5%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="iGrupoId" headertext="Grupo" Visible="False" />
                        <asp:boundfield datafield="iParametroId" headertext="Parametro" Visible="False"  />
                        
                        <asp:TemplateField HeaderText="Descripci&#243;n">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescripcion" runat="server" readonly ="false" 
                                  MaxLength="200" Text='<%# Eval("vDescripcion") %>' Width="98%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Descripcion" runat="server" Value='<%#Eval("vDescripcion")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDescripcion" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("vDescripcion") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="35%" />
                            <ItemStyle Width="35%" />
                        </asp:TemplateField>                        

                        <asp:TemplateField HeaderText="Valor">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtValor" runat="server" readonly ="false" 
                                  MaxLength="50000" Text='<%# Eval("vValor") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Valor" runat="server" Value='<%#Eval("vValor")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkValor" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("vValor") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Orden">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOrden" runat="server" readonly ="false" 
                                  MaxLength="2" Text='<%# Eval("iOrden") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_Orden" runat="server" Value='<%#Eval("iOrden")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkOrden" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("iOrden") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Visible">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlVisible" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="Hidd_Visible" runat="server" Value='<%#Eval("iVisible")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkVisible" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("NombreVisible") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="F. Inicio">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFInicio" runat="server" readonly ="false" 
                                  Text='<%# Eval("dFechaInicio") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_FInicio" runat="server" Value='<%#Eval("dFechaInicio")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFInicio" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("dFechaInicio") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="F. Fin">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFFin" runat="server" readonly ="false" 
                                  Text='<%# Eval("dFechaFin") %>' Width="95%">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_FFin" runat="server" Value='<%#Eval("dFechaFin")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFFin" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("dFechaFin") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="iEstado" headertext="Id Estado" Visible="False"/>
                        
                        <asp:TemplateField HeaderText="Estado">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEstado" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="Hidd_Estado" runat="server" Value='<%#Eval("iEstado")%>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNombreEstado" runat="server" CommandArgument='<%# Eval("iParametroId") %>' CommandName="Seleccionar" CssClass="Estilo_Link_Grilla"
                                                Text='<%# Eval("NombreEstado") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateField>

                        <asp:boundfield datafield="NUM_HIJOS" headertext="NroHijos" Visible="False"  />

                         <asp:TemplateField HeaderText="Editar">
                              <EditItemTemplate>
                                <asp:ImageButton ID="btnActualizar" runat="server" AlternateText="Actualizar" CommandName="update"
                                  ImageUrl="~/Images/Icono_PreCalificar_1.gif" ValidationGroup="GroupActivoFijoInmueble"
                                  Width="21px" />
                                <asp:ImageButton ID="btnCancelar" runat="server" AlternateText="Cancelar" CommandName="cancel"
                                  ImageUrl="~/Images/Icono_Cancelar_1.ico" Visible="true" />
                              </EditItemTemplate>
                              <ItemStyle HorizontalAlign="Center" Width="10%" />
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CommandName="edit"
                                  ImageUrl="~/Images/Editar.gif" Width="21px" />
                              </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Eliminar">
                              <ItemStyle HorizontalAlign="Center" Width="5%" />
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                <asp:ImageButton ID="btnEliminar" runat="server" AlternateText="Eliminar" CommandName="delete" CommandArgument='<%# Eval("iGrupoId") & "," & Eval("iParametroId") %>'
                                  ImageUrl="~/images/Icono_Cancelar_1.ico" Width="21px" OnClientClick="return ConfirmaEliminacionHijo(this);"/>
                              </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                    <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>



    </div>

                    </ContentTemplate>
                </asp:UpdatePanel>  
    </form>
</body>
</html>

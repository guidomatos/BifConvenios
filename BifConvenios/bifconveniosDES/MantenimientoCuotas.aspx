<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MantenimientoCuotas.aspx.vb" Inherits="BIFConvenios.MantenimientoCuotas" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/Banner.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >  
<head runat ="server" >
    <title>::BIF Convenios ::</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

        <LINK href="../css/global.css" type="text/css" rel="stylesheet">
		<LINK href="../css/styleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript"  type="text/javascript" src="js/global.js"></script>
		
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server" method="post">
    <table cellSpacing="0" cellPadding="0" width="100%" border="0">
                    <tr>
				<td><uc1:banner id="Banner1" title="Mantenimiento Cuotas" runat="server"></uc1:banner></td>
				</tr>
    </table> 
   
        <asp:ScriptManager id="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Label ID="lbl_resultado" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label>
    
   
        <asp:UpdatePanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="Gvw_datos_cliente" runat="server" AutoGenerateColumns="False" 
                    OnRowCommand="Gvw_datos_cliente_RowCommand" OnRowDeleting="Gvw_datos_cliente_RowDeleting" 
                    OnRowEditing="Gvw_datos_cliente_RowEditing" OnRowUpdating="Gvw_datos_cliente_RowUpdating" 
                    OnRowCancelingEdit="Gvw_datos_cliente_RowCancelingEdit" CssClass="grilla_principal">
                				
                    <Columns>
                        <asp:TemplateField HeaderText="C&#243;digo Cliente">
                              <ItemStyle HorizontalAlign="Left" />
                              <HeaderStyle HorizontalAlign="Center" Width="80px" />
                              <ItemTemplate>
                                <asp:Label ID="lbl_cliente_id" runat="server" Text='<%# Eval("CODIGO_CLIENTE")  %>'></asp:Label>
                              </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre Cliente">
                         <ItemStyle HorizontalAlign="Left" />
                              <HeaderStyle HorizontalAlign="Center" Width="360px" />
                              <ItemTemplate>
                                <asp:Label ID="lbl_descripcion_cliente" runat="server" Text='<%# Eval("NOMBRE_CLIENTE") %>'></asp:Label>
                              </ItemTemplate>
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="A&#241;io Actual">
                              <EditItemTemplate>
                                <asp:TextBox ID="txtAnioCargo_Cliente" runat="server" readonly ="false" 
                                  MaxLength="2" Text='<%# Eval("ANIO_ACTUAL") %>' Width="50px">
                                </asp:TextBox>
                                  <asp:HiddenField ID="Hidd_AnioCargo_Cliente" runat="server" Value='<%#Eval("ANIO_ACTUAL")%>' />
                              </EditItemTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              <ItemTemplate>
                                <asp:Label ID="lblAnioCargo_cliente" runat="server" Text='<%# Eval("ANIO_ACTUAL") %>'></asp:Label>
                              </ItemTemplate>
                           </asp:TemplateField>
                           
                           <asp:TemplateField HeaderText="Mes Actual">
                              <EditItemTemplate>
                                <asp:TextBox ID="txtMesCargo_Cliente" runat="server" readonly ="false" 
                                  MaxLength="2" Text='<%# Eval("MES_ACTUAL") %>' Width="50px">
                                </asp:TextBox>
                                  <asp:HiddenField ID="HiddMesCargo_Cliente" runat="server"  Value ='<%# Eval("MES_ACTUAL") %>' />
                              </EditItemTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              <ItemTemplate>
                                <asp:Label ID="lblMesCargo_cliente" runat="server" Text='<%# Eval("MES_ACTUAL") %>'></asp:Label>
                              </ItemTemplate>
                           </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="&#218;ltimo a&#241;io cargo">
                              <ItemStyle HorizontalAlign="Right" />
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              <ItemTemplate>
                                <asp:Label ID="lblultimoanioCargo_cliente" runat="server" Text='<%# Eval("ULTIMO_ANIO_CARGO") %>'></asp:Label>
                              </ItemTemplate>
                           </asp:TemplateField>
                                 <asp:TemplateField  HeaderText="&#218;ltimo mes cargo">
                              <ItemStyle HorizontalAlign="Right" />
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                              <ItemTemplate>
                                <asp:Label ID="lblultimoMesCargo_cliente" runat="server" Text='<%# Eval("ULTIMO_MES_CARGO") %>'></asp:Label>
                              </ItemTemplate>
                           </asp:TemplateField>
                           
                        <asp:TemplateField HeaderText="Tipo Cuota">
                             <EditItemTemplate>
                                 <asp:DropDownList ID="ddl_tipocuota" runat="server"  DataSource='<%#Get_Tipo_cuota()%>' DataTextField ="Parametro_ItemValor" DataValueField ="Parametro_ItemID"  DataMember='<%# Eval("TIPO_CUOTA") %>' >
                                    <asp:ListItem Selected ="True" ></asp:ListItem>
                                 </asp:DropDownList>
                              </EditItemTemplate>
                            <HeaderStyle Width="120px" />
                            <ItemTemplate>
                                <asp:Label ID="lbltIPO_CUOTA" runat="server" Text='<%# Eval("Parametro_ItemValor") %>'></asp:Label>
                             </ItemTemplate>
                        </asp:TemplateField>
                           
                           
                         <asp:TemplateField HeaderText="Editar">
                              <EditItemTemplate>
                                <asp:ImageButton ID="btnActualizar" runat="server" AlternateText="Actualizar" CommandName="update"
                                  ImageUrl="../images/Icono_PreCalificar_1.gif" ValidationGroup="GroupActivoFijoInmueble"
                                  Width="21px" />
                                <asp:ImageButton ID="btnCancelar" runat="server" AlternateText="Cancelar" CommandName="cancel"
                                  ImageUrl="../images/Icono_Cancelar_1.ico" Visible="true" />
                              </EditItemTemplate>
                              <ItemStyle HorizontalAlign="Center" />
                              <HeaderStyle HorizontalAlign="Center" />
                              <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" AlternateText="Editar" CommandName="edit"
                                  ImageUrl="../images/Editar.gif" Width="21px" />
                              </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Grilla_Cabecera_principal" />
                    <RowStyle CssClass="Grilla_Filas_Principal_deuda" />
                 
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

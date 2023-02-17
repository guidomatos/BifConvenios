<%@ Page Language="VB" AutoEventWireup="false" CodeFile="buscarEmpresaIBS.aspx.vb" Inherits="reportes_buscarEmpresaIBS" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>  

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>Listado de Empresas</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="../css/global.css" rel="stylesheet" type="text/css" />    
    <script src ='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>


    <base target="_self" />
    
    <script type="text/javascript">
                
       function Seleccionar(id, empresa) 
       {
            var cadena = id + '|' + empresa     
            top.returnValue = cadena;                        
            this.close();
        }		
		
        
    </script>
    
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />   
    
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
    #search
    {
        display:table;
        width:100%;
        height:30px;
    }
    .searchborder
    {
        border: 1px solid black;
        padding: 1px;
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
    

</head>
<body style="margin-left:20px; margin-top:10px;">

    <form id="form1" method="post"  runat="server">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
        
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
                                                    <asp:Literal ID="ltrTitulo" Text="Listado de Empresas" runat="server" />
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
                                            <td>
                                                Empresa:&nbsp;<asp:TextBox ID="txtfEmpresa" runat="server" Width="150px"></asp:TextBox>
                                            </td>                
                                            <td>                        
                                                  <asp:Button ID="btnSearch" runat="server" Text="Filtrar" CssClass="button" />                       
                                                
                                            </td>
                                        </tr>                                           			
					    
			                        	<tr>
					                        <td colspan="2">
						                        <span class="SubHead">Número de Registros&nbsp;:&nbsp;</span>
						                        <asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label>
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
                <div class="cell container">
                
                    <asp:UpdatePanel ID="upQuery" runat="server">
                        <ContentTemplate>
                                                
                                <asp:GridView ID="dgDatos" runat="server" CellPadding="3" ForeColor="Black"
                                        GridLines="Vertical" EnableViewState="False" BackColor="White"
                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                        AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="15"
                                        AllowSorting="True">
                                                                               
                                        
                                        <Columns>
                                        
                                            <asp:BoundField DataField="Codigo_IBS" HeaderText="C&#243;digo IBS"   >
                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre Cliente" >
                                                <ItemStyle HorizontalAlign="Left" Width="65%" />
                                            </asp:BoundField>
                                                                                
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <%#GetMensajeSeleccionar(DataBinder.Eval(Container.DataItem, "Codigo_IBS"), DataBinder.Eval(Container.DataItem, "Nombre_Cliente"))%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:TemplateField>
                                            
                                        </Columns>
                                        
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>							            
                                        <AlternatingRowStyle BackColor="#CCCCCC" />                                        
                                                                                
                                    </asp:GridView>				     
                           
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
        </div>    
        
        
    
    </form>
    
</body>
</html>

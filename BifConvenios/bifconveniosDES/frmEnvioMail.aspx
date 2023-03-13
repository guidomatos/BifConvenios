<%@ Page Language="vb" AutoEventWireup="false" Inherits="frmEnvioMail" CodeFile="frmEnvioMail.aspx.vb"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Seleccione el formato del archivo</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    
    <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>

    <script type="text/javascript">
    
        function SendMail(controls){
			var a = controls.split(',');
			var i = 0 ;
			var anyChecked = false;
			
			for ( i = 0; i<= a.length -1; i++ ) {
				if ( document.all( a[i] ).checked ) {
					anyChecked = true;
				}
			}   
			
			if ( !anyChecked ) {
				alert ( 'Debe seleccionar por lo menos un correo electronico.');
			}
			else{
				if ( confirm ( '¿Desea enviar el correo electronico con el archivo de cuotas?')){
					__doPostBack('lnkEnviarEmail', '');	
				}
			}
		}        

        function Cerrar2() {
            top.returnValue = 0;
            this.close();
        }

        function ShowModalidad() {
            var val = document.getElementById("lstFormatFile").value;

            if (val == 'standardXls') {
                document.getElementById('lblModalidad').style.display = 'inline';
                document.all('lstModalidad').style.display = 'inline';
            } else {
                document.getElementById('lblModalidad').style.display = 'none';
                document.all('lstModalidad').style.display = 'none';
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
<body style="margin-left:20; margin-top:10;">
    <form id="Form1" method="post" runat="server">    
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Elaboración de Formato de Correo"></uc1:Banner>
                </td>
            </tr>            
        </table>
        <input id="hdAnio" type="hidden" name="hdAnio" runat="server" />
        <input id="hdMes" type="hidden" name="hdMes" runat="server" />
        <asp:LinkButton ID="lnkFinish" runat="server" />
        <div id="container" style="width:500px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search">
                            <div class="row">
                                <div class="cell">
                                    <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <h3>
                                                    <asp:Literal ID="Literal1" Text="Envio de Correos Electrónicos a Destinatarios" runat="server"></asp:Literal>
                                                </h3>
                                            </td>                
                                        </tr>
                                    </table>
                                    <table style="border:0; width:500px">
                                        <tr style="height:40px;">
                                            <td style="width:130px;">
                                                <asp:Label ID="Label1" Text="De: " runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDE" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr style="height:40px;">
                                            <td style="width:130px;">
                                                <asp:Label ID="Label3" Text="Contenido: " runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtComentario" runat="server" Width="400px" TextMode="MultiLine" Height="61px" />
                                            </td>                                            
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="border:0; width:500px;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" Text="Listado de Correos" runat="server" Font-Bold="true" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />                                    
                                    <table style="border:0; width:500px;">
                                        <tr style="height:40px;">                                            
                                            <td style="width:150px;" align="left">
                                                <asp:GridView ID="gvCoordinadores" runat="server" Height="100px" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Mail" ReadOnly="True" HeaderText="Coordinadores" />
                                                    </Columns>
                                                </asp:GridView>                                                
                                            </td>
                                            <td style="width:150px;" align="left">
                                                <asp:GridView ID="gvFuncionarios" runat="server" Height="100px" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Mail" ReadOnly="True" HeaderText="Funcionarios" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                            <td style="width:150px;" align="left">
                                                <asp:GridView ID="gvResponsables" runat="server" Height="100px" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Mail" ReadOnly="true" HeaderText="Responsables" />
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>                                                                                
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
							            <tr>
								            <td style="width:50%;" align="center">
								                <asp:LinkButton runat="server" ID="lnkEnviarEmail"></asp:LinkButton>
								                <a href="#" onclick="Javascript:SendMail('<%=GetControlNames()%>')">
								                    Enviar email
								                </a>
								            </td>
								            <td style="width:50%;" align="center">
								                <asp:LinkButton runat="server" ID="lnkCancelar">Cancelar</asp:LinkButton>
								                <%--<a href="#" onclick="JavaScript:Cerrar2();">Cerrar</a>--%>
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
        <asp:Panel ID="pnlMensaje" runat="server" Visible="False">
            <div class="row">
                <div class="cell">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
		                <tr>
			                <td style="width:50%;" align="center">
			                    <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
			                </td>
		                </tr>
	                </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlClose" runat="server" Visible="False">
            <script type="text/javascript">
                alert('Mensaje enviado exitosamente')
                
                __doPostBack('lnkFinish','');	
            </script>
        </asp:Panel>
    </form>
</body>
</html>

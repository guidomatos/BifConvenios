<%@ Page Language="VB" AutoEventWireup="false" CodeFile="editarCodigoIBS.aspx.vb" Inherits="Clientes_editarCodigoIBS" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta content="blendTrans(Duration=0.5)" http-equiv="Page-Enter" />
	<meta content="blendTrans(Duration=0.5)" http-equiv="Page-Exit" />
    <title></title>
    <link href="<%= ResolveUrl("~/App_Themes/Default/Default.css") %>" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function permite(elEvento, permitidos) {
          // Variables que definen los caracteres permitidos
          var numeros = "0123456789";
          var caracteres = " abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
          var numeros_caracteres = numeros + caracteres;
          var teclas_especiales = [8, 37, 39, 46];
          // 8 = BackSpace, 46 = Supr, 37 = flecha izquierda, 39 = flecha derecha
         
         
          // Seleccionar los caracteres a partir del parámetro de la función
          switch(permitidos) {
            case 'num':
              permitidos = numeros;
              break;
            case 'car':
              permitidos = caracteres;
              break;
            case 'num_car':
              permitidos = numeros_caracteres;
              break;
          }
         
          // Obtener la tecla pulsada 
          var evento = elEvento || window.event;
          var codigoCaracter = evento.charCode || evento.keyCode;
          var caracter = String.fromCharCode(codigoCaracter);
         
          // Comprobar si la tecla pulsada es alguna de las teclas especiales
          // (teclas de borrado y flechas horizontales)
          var tecla_especial = false;
          for(var i in teclas_especiales) {
            if(codigoCaracter == teclas_especiales[i]) {
              tecla_especial = true;
              break;
            }
          }
         
          // Comprobar si la tecla pulsada se encuentra en los caracteres permitidos
          // o si es una tecla especial
          return permitidos.indexOf(caracter) != -1 || tecla_especial;
        }
            
        function getbacktostepone() {
            window.location = "editarCodigoIBS.aspx";
        }
        function onSuccess() {
            alert("sucesos");
            setTimeout(okay, 2000);
            debugger;
            var strx = document.getElementById('<%=hdExiste.ClientID%>').value;
        }
        function onError() {
            setTimeout(getbacktostepone, 2000);
        }
        
        function cancel() {
                
                window.parent.document.getElementById('ButtonEditCancel').click();
        }
        
        function okay() {
        
        }
        
        function ExisteCodigoIBS(){
            debugger;
            var _codigo = document.getElementById('<%=hdExiste.ClientID%>').value;
            document.getElementById('<%=hdresult.ClientID%>').value = _codigo;
        }
  
    

    
        function ActualizarCodigoIBS() {
                debugger;
                
                
                var txtcodigo = document.getElementById('txtcodigoibsnuevo').value;
                
                if(txtcodigo == ""){
                    alert("Ingrese el nuevo código IBS");
                    return false;
                }
			    if (confirm('Realmente desea cambiar el codigo IBS de este Cliente?')) {
			        window.parent.document.getElementById('ButtonEditDone').click();
			        getbacktostepone();
			    }
			}
		
		
    </script>
    <style type="text/css">
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
    </style>
</head>
<body style="margin: 0px; padding: 0px;">
    <form id="form1" runat="server">
    <input type="hidden" value="" runat="server" id="hdnWindowUIMODE" />
    <asp:HiddenField ID="hdIdCliente" runat="server" />
    <asp:HiddenField ID="hdExiste" runat="server" />
    <asp:HiddenField ID="hdresult" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <div class="popup_Container">
        <div class="popup_Titlebar" id="PopupHeader">
            <div class="TitlebarLeft">
                Editar Código IBS
            </div>
            <div class="TitlebarRight" onclick="cancel();">
            </div>
        </div>
        <div class="popup_Body">
            <asp:MultiView ID="MultiViewExpanse" runat="server">
                <asp:View ID="ViewInput" runat="server">
                    <table>
                        <tr>
                            <td>
                                Nuevo Código IBS:
                            </td>
                            <td>
                                <%--<asp:TextBox ID="txtcodigoibsnuevo" runat="server" Width="100px"></asp:TextBox>--%>
                                <input type="text" id="txtcodigoibsnuevo" runat="server" style="width:100px;" onkeypress="return permite(event, 'num')" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ErrorMessage="*" ControlToValidate="txtcodigoibsnuevo"></asp:RequiredFieldValidator>
                                
                            </td>
                        </tr>
                    </table>
                </asp:View>
 
            </asp:MultiView>
        </div>
        <div class="popup_Buttons">
            <asp:Button ID="btnOkay" Text="Grabar" runat="server" CssClass="button" OnClientClick="ActualizarCodigoIBS();" />
            <input id="btnCancel" value="Cancelar" type="button" class ="button" onclick ="cancel();" />
            <%--<asp:Button ID="btnvalidar" runat="server" Text="validar" Visible="true" OnClick="btnvalidar_Click"  />--%>
            </div>
    </div>
    </form>
</body>
</html>

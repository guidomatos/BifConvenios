<%@ Page Language="vb" AutoEventWireup="false" Inherits="selectFileFormat" CodeFile="selectFileFormat.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Seleccione el formato del archivo</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />

    <script type="text/javascript">	
		
		function EnviaCorreo(id){
		    //alert('enviomail.aspx?idp='+ id +  '&formatoArchivo=' + formatoArchivo +'&tipoCliente='+tipoCliente)
			//openPage( 'enviomail.aspx?idp='+ id +  '&formatoArchivo=' + formatoArchivo +'&tipoCliente='+tipoCliente, 430,500 );
			//top.returnValue	= id + "|" + formatoArchivo + "|" + situacionTrabajador;					
			top.returnValue	= id + "|" + document.all('lstFormatFile').value + "|-"
			this.close();			
		}		
		
		function Cerrar2() {
			top.returnValue	= '';
			this.close();
		}
		
		function ShowModalidad(){
		var val = document.getElementById("lstFormatFile").value;
		
		    if(val== 'standardXls')
		    { 
		        document.getElementById('lblModalidad').style.display = 'inline'
		        document.all('lstModalidad').style.display= 'inline'
		    }else{
		        document.getElementById('lblModalidad').style.display= 'none'
		        document.all('lstModalidad').style.display= 'none'
		    }
		    
		}
		-->
    </script>

</head>
<body style="margin-left:20; margin-top:10px;">
    <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="TableSmooth">
            <tr>
                <td class="SubHead" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Text" colspan="2">
                    &nbsp; Generación del archivo de cuotas
                </td>
            </tr>
            <tr>
                <td class="Text" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table border="0" width="430">
                        <tr>
                            <td style="width:140px;" class="SubHead">
                                <font class="Normal">&nbsp;Empresa&nbsp;: </font>
                            </td>
                            <td class="Normal" style="width: 162px">
                                <%=nombre%>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <font class="Normal">&nbsp;Periodo&nbsp;:</font>
                            </td>
                            <td class="Normal" style="width: 162px">
                                <%=mes%>
                                -
                                <%=anio%>
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <font class="Normal">&nbsp;Fecha de Proceso IBS&nbsp;: </font>
                            </td>
                            <td class="Normal" style="width: 162px">
                                <%=fechaProcesoAS400%>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="SubHead" colspan="2" style="height: 21px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                <font class="Normal">&nbsp; Formato del archivo&nbsp;:&nbsp; </font>
                            </td>
                            <td style="width: 162px">
                                <asp:DropDownList runat="server" ID="lstFormatFile">
                                    <asp:ListItem Value="csv">Delimitado por comas (.csv)</asp:ListItem>
                                    <asp:ListItem Value="txt">Texto encolumnado (.txt)</asp:ListItem>
                                </asp:DropDownList>
                                <!--
						            <select id="lstFormatFile" name="lstFormatFile" >
							            <option value="csv" selected>Delimitado por comas (.csv)</option>
							            <option value="txt">Texto encolumnado (.txt)</option>
						            </select>-->
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead" style="height: 10px">
                            </td>
                            <td style="height: 10px; width: 162px;">
                            </td>
                        </tr>
                        <tr>
                            <td class="SubHead">
                                &nbsp;
                                <asp:Label ID="lblModalidad" runat="server" Text="Modalidad:"></asp:Label></td>
                            <td style="width: 162px">
                                <asp:DropDownList ID="lstModalidad" runat="server" DataTextField="textoModalidad"
                                    DataValueField="codigoModalidad">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SubHead" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="SubHead" align="center">
                                <!--onclick="JavaScript:Send('<%=idP%>');"-->
                                <a href="#" onclick="JavaScript:EnviaCorreo('<%=idP%>')">
                                    <%--Enviar archivo en correo electronico--%>
                                    Obtener archivo </a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="JavaScript:Cerrar2();">
                                        Cancelar</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SubHead" colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </form>
</body>

<script language="javascript">
		    ShowModalidad()
</script>

</html>

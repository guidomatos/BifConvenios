<%@ Page Language="vb" AutoEventWireup="false" Inherits="selectFileFormat" CodeFile="selectFileFormat.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Seleccione el formato del archivo</title>
    <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">	

        var tipoFormatoSeleccionado = '';

        function fnSeleccionarTipoArchivo(id) {
            tipoFormatoSeleccionado = id + "|" + document.all('lstFormatFile').value + "|-";
            window.close();
        }
        function fnCerrarVentana() {
            window.close();
        }
        function ShowModalidad() {
            var val = document.getElementById("lstFormatFile").value;

            if (val == 'standardXls') {
                document.getElementById('lblModalidad').style.display = 'inline'
                document.all('lstModalidad').style.display = 'inline'
            } else {
                document.getElementById('lblModalidad').style.display = 'none'
                document.all('lstModalidad').style.display = 'none'
            }

        }
        function ReturnValueSeleccionado() {
            return tipoFormatoSeleccionado;
        }
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
                                <a href="#" onclick="JavaScript:fnSeleccionarTipoArchivo('<%=idP%>')">
                                    <%--Enviar archivo en correo electronico--%>
                                    Obtener archivo </a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="JavaScript:fnCerrarVentana();">
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

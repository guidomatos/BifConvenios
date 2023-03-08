<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EnvioReporteMail"
    CodeFile="EnvioReporteMail.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Seleccione los correos electronicos</title>
    <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function Cerrar() {
            window.close();
        }
        function SendMail(controls) {
            var a = controls.split(',');
            var i = 0;
            var anyChecked = false;
            for (i = 0; i <= a.length - 1; i++) {
                if (document.all(a[i]).checked) {
                    anyChecked = true;
                }
            }

            if (!anyChecked) {
                alert('Debe seleccionar por lo menos un correo electronico.');
            }
            else {
                if (confirm('¿Desea enviar el correo electronico con el archivo de cuotas?')) {
                    __doPostBack('lnkEnviarEmail', '');
                }
            }
        }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table class="TableSmooth" height="300" cellspacing="0" cellpadding="0" width="100%"
            border="0">
            <tr>
                <td class="SubHead" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="Text" colspan="2">
                    &nbsp; Generación de archivo de descuentos - Envió de archivo vía e-mail</td>
            </tr>
            <tr>
                <td class="Text" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" border="0">
                        <tr>
                            <td class="SubHead">
                                &nbsp;&nbsp;&nbsp;Empresa
                            </td>
                            <td class="Normal">
                                <asp:Label ID="lblCliente" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="SubHead">
                    <br>
                    &nbsp;&nbsp;&nbsp;&nbsp;Cuerpo del correo<br>
                </td>
            </tr>
            <tr>
                <td align="middle" colspan="2">
                    <asp:TextBox ID="txtComentario" runat="server" Width="400px" TextMode="MultiLine"
                        Height="61px"></asp:TextBox></td>
                <tr>
                    <td class="Text" colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="middle" colspan="2" height="160">
                        <asp:DataGrid ID="dgGen" runat="server" BorderColor="Silver" BackColor="White" BorderWidth="1px"
                            CellPadding="4" Width="401px" AutoGenerateColumns="False">
                            <ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
                            <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Mail" ReadOnly="True" HeaderText="Correo Electr&#243;nico">
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid></td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td class="SubHead" align="middle">
                                    &nbsp;
                                    <asp:LinkButton runat="server" ID="lnkEnviarEmail"></asp:LinkButton>
                                    <a href="#" onclick="Javascript:SendMail('<%=GetControlNames()%>')">Enviar email</a>
                                    &nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="JavaScript:Cerrar();">Cerrar</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                        <asp:Panel ID="pnlClose" runat="server" Visible="False">

                            <script type="text/javascript" language="javascript">
                                window.close();
                            </script>

                        </asp:Panel>
                    </td>
                </tr>
        </table>
    </form>
</body>
</html>

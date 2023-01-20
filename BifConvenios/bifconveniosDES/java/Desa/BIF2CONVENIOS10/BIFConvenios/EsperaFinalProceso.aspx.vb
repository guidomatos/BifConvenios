

Namespace BIFConvenios

    Partial Class EsperaFinalProceso
        Inherits System.Web.UI.Page

        Protected oProcess As New Proceso()
        Protected EstadoProceso As String = ""

        Dim NombreEstado As String
        Dim DescripcionEstado As String

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Request.Params("id") Is Nothing Then
                'Revisamos que el servidor siga disponible
                'If Not Utils.TestServer() Then
                '    lblMensaje.Text = Utils.SERVER_UNAVAILABLE
                '    lrtlSwf.Visible = False
                '    ltrlScript.Visible = False
                '    Exit Sub
                'End If

                If oProcess.EsperaFinalProceso(Request.Params("id")) Then
                    'Aqui tenemos que el proceso ha sido finalizado, y podemos mostrar el reporte de
                    'de la validacion culminada
                    ltrlScript.Visible = True
                    SetScript(CType(Request.Params("id"), String))
                    lblMensaje.Text = "El proceso de válidacion ha terminado exitosamente, espere un momento."
                Else
                    EstadoProceso = oProcess.GetEstadoProceso(Request.Params("id"))

                    If EstadoProceso Is Nothing Then Exit Sub
                    Select Case EstadoProceso.Trim
                        Case "EP"
                            oProcess.GetMensaje("EC0", "Proceso", NombreEstado, DescripcionEstado)
                            lblMensaje.Text = NombreEstado + " - " + DescripcionEstado

                            Exit Select
                        Case Else
                            'lblMensaje.Text = "Procesando..."
                            lblMensaje.Text = ""
                            lrtlSwf.Visible = True
                    End Select

                End If
            End If
        End Sub


        'Establecemos el script con las instruccione a ser procesadas
        Private Sub SetScript(ByVal id As String)
            ltrlScript.Text = "<SCRIPT language=""javascript"">" + vbCrLf
            ltrlScript.Text += "<!--" + vbCrLf
            ltrlScript.Text += "//Mostramos los reportes de la carga" + vbCrLf
            ltrlScript.Text += "	if ( window.opener != null ) {" + vbCrLf
            ltrlScript.Text += "		window.opener.location.href = ""/BIFConvenios/ResultadoProcesoCronogramaFuturo.aspx?id=" + id + """; " + vbCrLf
            ltrlScript.Text += "		window.close();" + vbCrLf
            ltrlScript.Text += "	}" + vbCrLf
            ltrlScript.Text += "-->" + vbCrLf
            ltrlScript.Text += "</SCRIPT>" + vbCrLf
        End Sub


    End Class


End Namespace


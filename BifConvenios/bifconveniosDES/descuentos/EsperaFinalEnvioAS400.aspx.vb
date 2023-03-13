Namespace BIFConvenios

    Partial Class EsperaFinalEnvioAS400
        Inherits Page
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub


#End Region
        Protected oProcess As New Proceso()

        Dim mes As String = ""
        Dim anio As String = ""
        Dim Fecha_ProcesoAS400 As String = ""

        Dim TipoDocumento As String
        Dim NumeroDocumento As String
        Dim CustomerNumber As String
        Dim idProcess As String = ""

        'Informacion de registros que han sido procesados
        Dim strRegistrosSQL As String = ""
        Dim strRegistrosAS_400 As String = ""

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            If Request.Params("id") IsNot Nothing Then
                idProcess = Request.Params("id")
                'If Not Utils.TestServer() Then
                '    lblMensaje.Text = Utils.SERVER_UNAVAILABLE
                '    pnlFinal.Visible = False
                '    pnlSwf.Visible = False
                '    lblMensaje.Visible = True
                '    pnlMensaje.Visible = True
                '    Exit Sub
                'End If
                If oProcess.EsperaFinalEnvioDescuentosAS400(idProcess) Then
                    pnlSwf.Visible = False
                    pnlFinal.Visible = True
                Else
                    pnlSwf.Visible = True
                    pnlFinal.Visible = False

                    oProcess.getProcesoMesAnio(idProcess, mes, anio, Fecha_ProcesoAS400,
                                                TipoDocumento, NumeroDocumento)
                    CustomerNumber = oProcess.GetCustomerNumber(TipoDocumento, NumeroDocumento)
                    strRegistrosSQL = oProcess.GetTotalRegistrosEnvio_AS400(idProcess)
                    strRegistrosAS_400 = oProcess.ObtenerRegistrosEnviados(CustomerNumber, anio, mes)
                    lblAvance.Text = "Se han procesado " & strRegistrosAS_400 & " de " & strRegistrosSQL & " registros."
                End If
            End If
        End Sub
    End Class
End Namespace

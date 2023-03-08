Namespace BIFConvenios

    Partial Class EsperaFinalAD
        Inherits Page
        Protected oProceso As New Proceso()
        Protected Pid As String = ""

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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Request.Params("id") IsNot Nothing Then
                'Revisamos que el servidor siga disponible
                'If Not Utils.TestServer() Then
                '    lblMensaje.Text = Utils.SERVER_UNAVAILABLE
                '    pnlFinal.Visible = False
                '    pnlSwf.Visible = False
                '    pnlMensaje.Visible = True
                '    Exit Sub
                'End If
                Pid = Request.Params("id")
                Try
                    If oProceso.FinalProcesoCargaDescuentos(Pid) Then
                        pnlFinal.Visible = True
                    Else
                        pnlSwf.Visible = True
                    End If
                Catch
                    pnlSwf.Visible = True
                End Try

            End If
        End Sub

    End Class
End Namespace

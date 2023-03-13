Namespace BIFConvenios

    Partial Class ProcesarProrrogas
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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Dim pagares As String
            Dim proceso As String
            Dim loteProrroga As Integer
            'en este punto comenzamos a necesitar COM+ para soportar entorno transaccional
            'nada garantizado
            If Request.Params("pagares") IsNot Nothing Then
                pagares = Request.Params("pagares").Replace("!", "")
                proceso = Request.Params("proceso")
                Dim userName As String = ""
                If Context.User.Identity.Name.IndexOf("\") > 0 Then
                    userName = Context.User.Identity.Name.Split("\")(1)
                End If
                loteProrroga = Prorroga.procesaProrroga(proceso, pagares, userName)
                Response.Redirect("ProcesarProrrogasWaitEnd.aspx?numeroLote=" + CStr(loteProrroga))
            Else
                lblMensaje.Text = "Envío inválido"
            End If
        End Sub

    End Class
End Namespace

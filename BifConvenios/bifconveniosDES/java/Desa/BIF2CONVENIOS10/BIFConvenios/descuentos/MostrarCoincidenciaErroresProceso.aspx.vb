Namespace BIFConvenios


    Partial Class MostrarCoincidenciaErroresProceso
        Inherits System.Web.UI.Page

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Dim id As String
            Dim pid As String
            If Not Request.Params("id") Is Nothing And Not Request.Params("pid") Is Nothing Then

                id = Request.Params("id")
                pid = Request.Params("pid")

                dgData.DataSource = BIFConvenios.ArchivosDescuento.getDatosArchivoTextoBusqueda(pid, id)
                dgData.DataBind()

                lblNumReg.Text = dgData.Items.Count.ToString
            End If

            'dgData.DataSource = BIFConvenios.ArchivosDescuento.getDatosArchivoTextoBusqueda( 

        End Sub

    End Class
End Namespace

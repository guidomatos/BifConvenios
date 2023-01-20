
Namespace BIFConvenios

    Partial Class DetalleConsultaEnvioPorTrabajador
        Inherits System.Web.UI.Page
        Protected nombreEmpleado As String
        Protected codigoModular As String
        Protected oProceso As New Proceso()
        Protected DLNE As String = ""
        Protected DLCM As String = ""


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
            If Not Request.Params("DLNE") Is Nothing Then
                DLNE = CType(Request.Params("DLNE"), String)
                lblNombreCliente.Text = DLNE
            End If

            If Not Request.Params("DLCM") Is Nothing Then
                DLCM = CType(Request.Params("DLCM"), String)
            End If


            If Not Page.IsPostBack Then
                dgPagosRealizados.DataSource = oProceso.GetPagosRealizados(DLNE, DLCM)
                dgPagosRealizados.DataBind()

            End If
        End Sub

    End Class
End Namespace

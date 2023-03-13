Imports BIFConvenios

Partial Class ConsultaDetallePagosIBS
    Inherits Page

    Protected codEmpresa As String
    Protected fechaDesde As String
    Protected fechaHasta As String
    Protected numeroPagare As String


    Protected nombreTrabajador As String

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            If Not (Request.Params("p1") Is Nothing And Request.Params("p2") Is Nothing And Request.Params("p3") Is Nothing And Request.Params("p4") Is Nothing) Then
                'Obtenemos los parametros de la funcion de los parametros de la pagina
                codEmpresa = Request.Params("p1")
                numeroPagare = Request.Params("p2")
                fechaDesde = Request.Params("p3")
                fechaHasta = Request.Params("p4")
                nombreTrabajador = Request.Params("nt")

                lblCuotaMes.Text = Request.Params("cuotaMes")
                lblimporteDescontado.Text = Request.Params("importeDescontado")
                lbldeudaPeriodo.Text = Request.Params("deudaPeriodo")

                lblNombreTrabajador.Text = nombreTrabajador
                lblNumeroPagare.Text = numeroPagare
                lblFechaDesde.Text = Utils.GetFechaCanonica(fechaDesde)
                lblFechaHasta.Text = Utils.GetFechaCanonica(fechaHasta)

                dgData.DataSource = PostConciliacion.getDetallePagosIBS(codEmpresa, numeroPagare, fechaDesde, fechaHasta)
                dgData.DataBind()
                dgData.Visible = True
            End If
        End If
    End Sub

End Class

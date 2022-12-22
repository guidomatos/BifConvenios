Partial Class ConsultaDetallePagosIBSFechas
    Inherits System.Web.UI.Page
    'Protected WithEvents lblFechaDesde As System.Web.UI.WebControls.Label
    'Protected WithEvents lblFechaHasta As System.Web.UI.WebControls.Label

    Protected codEmpresa As String
    Protected fechaDesde As String
    Protected fechaHasta As String
    Protected numeroPagare As String
    Protected nombreTrabajador As String
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
        If Not Page.IsPostBack Then
            If Not (Request.Params("p1") Is Nothing And Request.Params("p2") Is Nothing And Request.Params("p3") Is Nothing And Request.Params("p4") Is Nothing) Then
                'Obtenemos los parametros de la funcion de los parametros de la pagina

                'lblFechaDesde.Text = BIFConvenios.Utils.GetFechaCanonica(fechaDesde)
                'lblFechaHasta.Text = BIFConvenios.Utils.GetFechaCanonica(fechaHasta)

                txtFechaDesde.Text = BIFConvenios.Utils.GetFechaCanonica(fechaDesde)
                txtFechaHasta.Text = BIFConvenios.Utils.GetFechaCanonica(fechaHasta)
                dgData.DataSource = BIFConvenios.PostConciliacion.getDetallePagosIBS(codEmpresa, numeroPagare, fechaDesde, fechaHasta)
                dgData.DataBind()
                dgData.Visible = True
            End If
        End If
    End Sub

    Public Function PintarBotonCalendario(ByVal IDContenedor As String, ByVal nombrecontrol As String) As String
        Dim respuesta As String = ""


        respuesta = "<A href=""""><img title=""Calendario"" onclick=""return showCalendar('" & IDContenedor & IIf(IsNothing(IDContenedor), "", "_") & nombrecontrol.ToString.Trim() & "', 'dd/mm/y');"" height=""18"" alt=""Calendario"" src=""../images/calendario.gif"" width=""18"" align=""absMiddle"" border=""0""></A>"

        Return respuesta
    End Function

    Private Sub lnkEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviar.Click
        dgData.DataSource = BIFConvenios.PostConciliacion.getDetallePagosIBS(codEmpresa, numeroPagare, BIFConvenios.Utils.GetSQLDate(txtFechaDesde.Text), BIFConvenios.Utils.GetSQLDate(txtFechaHasta.Text))
        dgData.DataBind()
        dgData.Visible = True
    End Sub
End Class

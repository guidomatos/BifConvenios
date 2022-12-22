Imports System.Data.SqlClient

Namespace BIFConvenios
    Partial Class reporteProcesoDescuentosDetalle
        Inherits System.Web.UI.Page
        Protected oproc As New Proceso()

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
            If Not Page.IsPostBack Then
                Utils.AddSwap(lnkBack, "Image1", "/BIFConvenios/images/regresar_on.jpg")

                Dim dr As SqlDataReader
                Dim ShowGrid As Boolean
                dr = oproc.InformeProceso(CType(Request.Params("id"), String))
                If dr.Read Then
                    ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                    ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                    ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                    ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                    ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                    dgProcesoResult.CurrentPageIndex = 0
                    dgProcesoResult.DataSource = oproc.GetRegistrosResultadoProcesoDescuentos(Request.Params("id"), "0", "")
                    dgProcesoResult.DataBind()
                    dgProcesoResult.Visible = True
                    lblTotalReg.Text = dgProcesoResult.Items.Count.ToString.Trim()
                End If
            End If
        End Sub

        Private Sub lnkBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBack.Click
            If Not Request.Params("id") Is Nothing Then
                Response.Redirect(Request.ApplicationPath + "/ReporteProcesoDescuentosResumen.aspx?id=", True)
            End If
        End Sub

        Private Sub dgProcesoResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResult.PageIndexChanged
            dgProcesoResult.CurrentPageIndex = IIf(dgProcesoResult.PageCount < e.NewPageIndex, 0, e.NewPageIndex)

            dgProcesoResult.DataSource = oproc.GetRegistrosResultadoProcesoDescuentos(Request.Params("id"), "0", "")
            dgProcesoResult.DataBind()

        End Sub
    End Class
End Namespace
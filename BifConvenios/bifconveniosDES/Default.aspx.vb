
Namespace BIFConvenios
    Partial Class _default
        Inherits System.Web.UI.Page
        Protected oCliente As New BIFConvenios.Cliente()

        Dim _dtListaCronograma As New DataTable

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
                'ADD 23/09/2013 NCA: VALIDACION DE FINALIZACION PROCESO BATCH
                If AperturaDia.ValidarFinProcesoBatch("ACT") = 0 Then
                    Server.Transfer("AperturaDia.aspx")
                End If
                'END ADD

                _dtListaCronograma = oCliente.GetCronogramaEnvioCliente.Tables(0)

                Session("dtCronograma") = _dtListaCronograma

                Dim dw As DataView = _dtListaCronograma.DefaultView
                dw.Sort = "DLEDEN ASC"

                gvDatosCarga.DataSource = dw
                gvDatosCarga.DataBind()
            End If
        End Sub


        'Procedemos a normalizar el año que se obtiene desde AS/400
        Protected Function NormalizaAnhio(ByVal anio As Integer) As String
            Return (2000 + anio).ToString
        End Function

        Protected Sub gvDatosCarga_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDatosCarga.PageIndexChanging
            gvDatosCarga.PageIndex = e.NewPageIndex

            If (Not Session("dtCronograma") Is DBNull.Value) Then
                _dtListaCronograma = CType(Session("dtCronograma"), DataTable)

                Dim dw As DataView = _dtListaCronograma.DefaultView
                dw.Sort = "DLEDEN ASC"

                gvDatosCarga.DataSource = dw
                gvDatosCarga.DataBind()
            End If
        End Sub
    End Class
End Namespace

Imports System.Data
Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class ListadoCuotasPorVencer
        Inherits System.Web.UI.Page
        Protected WithEvents lblEmpresa As System.Web.UI.WebControls.Label
        Protected WithEvents lblDireccion As System.Web.UI.WebControls.Label
        Protected WithEvents lblAtencion As System.Web.UI.WebControls.Label
        Protected WithEvents lblCuidad As System.Web.UI.WebControls.Label
        Protected oCliente As New Cliente()
        Protected Codigo_proceso As String = ""
        Protected TipoDocumento As String = ""
        Protected NumeroDocumento As String = ""
        Protected Anio_periodo As String = ""
        Protected WithEvents dgListadoR As System.Web.UI.WebControls.DataGrid
        Protected WithEvents dgListado As System.Web.UI.WebControls.DataGrid
        Protected Mes_Periodo As String = ""


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


                If Not Request.Params("id") Is Nothing Then
                    Codigo_proceso = CType(Request.Params("id"), String)
                Else
                    Exit Sub
                End If
                'Codigo_proceso = "27818fd3-cd14-4b87-a583-df5cf64282de"

                Dim dr As SqlDataReader = oCliente.GetInfoClienteProceso(Codigo_proceso)
                If dr.Read Then
                    TipoDocumento = CType(dr("TipoDocumento"), String)
                    NumeroDocumento = CType(dr("NumeroDocumento"), String)
                    Anio_periodo = CType(dr("Anio_periodo"), String)
                    Mes_Periodo = CType(dr("Mes_Periodo"), String)
                End If
                Dim ds2 As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)

                drep.DataSource = ds2
                drep.DataBind()


                'Dim ds As DataSet = oCliente.GetInfoContactoConvenio("8", "20131257750", "2004", "7")
                'Dim ds As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)
                'Dim i As Integer = 1
                'For i = 0 To ds.Tables("DLinfo").Rows.Count - 1
                '    lblEmpresa.Text = ds.Tables("DLinfo").Rows(i).Item("CUSNA1").ToString()
                '    lblDireccion.Text = ds.Tables("DLinfo").Rows(i).Item("CUSNA2").ToString()
                '    lblCuidad.Text = ds.Tables("DLinfo").Rows(i).Item("CUSCTY").ToString()
                '    lblAtencion.Text = ds.Tables("DLinfo").Rows(i).Item("NOMCNT").ToString()
                'Next
                'dgListado.DataSource = oCliente.GetListadoCuotasPorVencer(Codigo_proceso, "A")
                'dgListado.DataBind()
            End If
        End Sub

        'Obtenemos el listado de cuotas por vencer
        Protected Function GetListadoCuotas(ByVal estadoCliente As String, ByVal moneda As String, ByVal AG As Decimal) As DataSet
            Return oCliente.GetListadoCuotasPorVencer(Codigo_proceso, estadoCliente, moneda, AG)
        End Function

        'Obtenemos el resumen del Listado de Cuotas
        Protected Function GetResumenListado(ByVal estadoCliente As String, ByVal moneda As String, ByVal AG As Decimal) As DataSet
            Return oCliente.GetListadoCuotasPorVencerResumen(Codigo_proceso, estadoCliente, moneda, AG)
        End Function

    End Class
End Namespace
Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class reporteProcesoDescuentosResumen
        Inherits Page
        Protected oProceso As New Proceso()

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
            If Not Page.IsPostBack Then
                ' Utils.AddSwap(lnkRegresar, "Image12", "/BIFConvenios/images/regresar_on.jpg")
                If Request.Params("id") IsNot Nothing Then

                    Dim dr0 As SqlDataReader
                    dr0 = oProceso.InformeProceso(Request.Params("id"))
                    If dr0.Read Then
                        ltrlCliente.Text = CType(dr0("Nombre_Cliente"), String)
                        ltrlDocumento.Text = CType(dr0("TipoDocumento"), String) + " - " + CType(dr0("NumeroDocumento"), String)
                        ltrlEstado.Text = CType(dr0("CodigoNombre"), String)
                        ltrlPeriodo.Text = MonthName(CType(dr0("Mes_Periodo"), Integer)) + " " + dr0("Anio_periodo")
                        ltrlFechaProceso.Text = CType(dr0("Fecha_ProcesoAD"), String)
                        ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr0("Fecha_ProcesoAS400"), String))

                        If CType(dr0("Estado"), String).Trim = "ED" Then
                            tblnfo.Visible = False
                        End If
                    End If

                    Dim dr As SqlDataReader = oProceso.GetResumenProcesoDescuentos(Request.Params("id"))
                    If dr.Read Then
                        hpErrores.Text = CType(dr("ErroresArchivo"), String)

                        hpValidos.Text = CType(dr("Validos"), String)
                        hpValidos.NavigateUrl += "?id=" & Request.Params("id") & "&type=" & "val"

                        hlValidos.Text = CType(dr("Validos"), String)
                        hlValidos.NavigateUrl += "?id=" & Request.Params("id") & "&type=" & "val"

                        hpTotal.Text = CType(dr("Total"), String)
                        hpTotal.NavigateUrl += "?id=" & Request.Params("id")

                        hlTotal.Text = CType(dr("TotalArchivo"), String)
                        hlErrorenvio.Text = CType(dr("ErroresTablaEnvio"), String)

                        hlErrorArchivo.Text = CType(dr("ErroresArchivoDescuento"), String)

                        hlErrorArchivo.NavigateUrl += "?id=" & Request.Params("id")

                        If CType(dr("flgCorreccion"), String) Then
                            hlErrorenvio.NavigateUrl += "reporteProcesoDescuentosCat.aspx?id=" & Request.Params("id") & "&type=" & "Err"
                        Else
                            hpErrores.NavigateUrl += "reporteProcesoDescuentosCat.aspx?id=" & Request.Params("id") & "&type=" & "Err"
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub lnkRegresar_Click(sender As Object, e As EventArgs) Handles lnkRegresar.Click
            Response.Redirect(ResolveUrl("ReporteProcesoDescuento.aspx"))
        End Sub
    End Class
End Namespace

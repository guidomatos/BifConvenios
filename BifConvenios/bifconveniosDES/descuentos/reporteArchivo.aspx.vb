Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class reporteArchivo
        Inherits Page
        Protected oproc As New Proceso()

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Dim id As String
            Dim dr As SqlDataReader

            If Not Page.IsPostBack Then
                Utils.AddSwap(lnkBack, "Image1", ResolveUrl("/images/regresar_on.jpg"))

                Try
                    If Not Request.Params("id") Is Nothing Then
                        id = CType(Request.Params("id"), String)
                        dr = oproc.InformeProceso(id)
                        If dr.Read Then
                            ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                            ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                            ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                            ltrlPeriodo.Text = MonthName(dr("Mes_Periodo")) + " " + dr("Anio_periodo")
                            ltrlFechaProceso.Text = CType(dr("Fecha_ProcesoAD"), String)
                            ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                        End If
                        dgData.DataSource = ArchivosDescuento.getErroresArchivo(id)
                        dgData.DataBind()

                        dgInformacionNoProcesada.DataSource = ArchivosDescuento.getDatosArchivoTexto(id)
                        dgInformacionNoProcesada.DataBind()
                    End If
                Catch ex As Exception
                    Utils.HandleError(ex)
                End Try
            End If
        End Sub

        Private Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
            If Request.Params("id") IsNot Nothing Then
                Response.Redirect(ResolveUrl("reporteProcesoDescuentosresumen.aspx?id=" & CType(Request.Params("id"), String)))
            End If
        End Sub

        Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
            If Not Request.Params("id") Is Nothing Then
                Dim id As String = CType(Request.Params("id"), String)
                Dim dr As SqlDataReader = ArchivosDescuento.getErroresArchivo(id)

                Response.Clear()
                Dim s As New IO.StreamWriter(Response.OutputStream, Encoding.UTF8)

                While dr.Read
                    s.WriteLine(dr("lineainformacion"))
                End While

                s.Close()
                Response.AppendHeader("content-disposition", "attachment; filename=" + id + ".txt")
                Response.ContentType = "text/HTML"

                Response.End()
            End If
        End Sub
    End Class
End Namespace

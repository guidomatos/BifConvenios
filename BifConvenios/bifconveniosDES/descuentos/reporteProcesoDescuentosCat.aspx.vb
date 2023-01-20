Imports System.Data
Imports System.Data.SqlClient

Namespace BIFConvenios
    Partial Class reporteProcesoDescuentosCat
        Inherits System.Web.UI.Page
        Protected oproc As New Proceso()
        Protected oCliente As New Cliente()

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

                Try
                    If Not Request.Params("id") Is Nothing And Not Request.Params("type") Is Nothing Then
                        Dim dr As SqlDataReader
                        dr = oproc.InformeProceso(CType(Request.Params("id"), String))
                        If dr.Read Then
                            ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                            ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                            ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                            ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                            ltrlFechaProceso.Text = CType(dr("Fecha_ProcesoAD"), String)
                            ltrlProcesoAS400.text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                            If CType(Request.Params("type"), String) = "val" Then
                                dgProcesoResultVA.CurrentPageIndex = 0
                                Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosValidos(Request.Params("id"))
                                dgProcesoResultVA.DataSource = ds 'oproc.GetRegistrosResultadoProcesoDescuentosValidos(Request.Params("id"))
                                dgProcesoResultVA.DataBind()
                                dgProcesoResultVA.Visible = True
                                Banner1.Title += " - Registros válidos"
                                lblTotalReg.Text = ds.Tables(0).Rows.Count  'dgProcesoResultVA.Items.Count.ToString.Trim()
                            End If
                            If CType(Request.Params("type"), String) = "Err" Then
                                If oproc.GetExisteErrorTabla(Request.Params("id")) Then
                                    Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
                                    dgProcesoResultErrTabla.DataSource = ds 'oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
                                    dgProcesoResultErrTabla.DataBind()
                                    dgProcesoResultErrTabla.Visible = True

                                    Banner1.Title += " - Registros con errores"
                                    lblTotalReg.Text = ds.Tables(0).Rows.Count 'dgProcesoResultErrTabla.Items.Count.ToString.Trim()
                                Else
                                    Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))

                                    dgProcesoResultErr.DataSource = ds 'oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
                                    dgProcesoResultErr.DataBind()
                                    dgProcesoResultErr.Visible = True
                                    dgProcesoResultErr.CurrentPageIndex = 0

                                    pnlCuota.Visible = True
                                    Banner1.Title += " - Registros con errores"
                                    lblTotalReg.Text = ds.Tables(0).Rows.Count    'dgProcesoResultErr.Items.Count.ToString.Trim()
                                End If

                            End If
                        End If
                    End If
                Catch ex As Exception
                    Utils.HandleError(ex)
                End Try
            End If
        End Sub


        'Obtenemos el valor del colo que queremos poner
        Protected Function GetColor(ByVal Alert As String) As System.Drawing.Color
            Dim returnValue As System.Drawing.Color
            If Alert = "black" Then
                returnValue = System.Drawing.Color.Black
            ElseIf Alert = "red" Then
                returnValue = System.Drawing.Color.Red
            ElseIf Alert = "blue" Then
                returnValue = System.Drawing.Color.Blue
            End If
            Return returnValue
        End Function

        Private Sub lnkBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBack.Click
            If Not Request.Params("id") Is Nothing Then
                Response.Redirect(ResolveUrl("reporteProcesoDescuentosresumen.aspx?id=" & CType(Request.Params("id"), String)))
            End If
        End Sub


        'Obtiene la lista de los regitros seleccionados
        Private Function GetCheckedRegs() As String
            Dim chk As CheckBox
            Dim str As String = ""
            Dim i As Integer

            For i = 0 To dgProcesoResultErr.Items.Count - 1
                chk = dgProcesoResultErr.Items(i).FindControl("chk")
                If chk.Checked Then
                    str = str & dgProcesoResultErr.Items(i).Cells(2).Text + ","
                End If
            Next
            If str <> "" Then
                str = str.Substring(0, str.Length - 1)
            End If
            Return str
        End Function

        Private Sub lnkEnviaPrestamos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviaPrestamos.Click
            Dim Codes As String = GetCheckedRegs()
            Dim str As String
            Dim i As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            myConnection.Open()

            For Each str In Codes.Split(",")
                Try
                    ' Response.Write(str + "<br>")
                    oCliente.ActualizaDescuentoRegistroErrorEnvioA(myConnection, Request.Params("id"), str, "0", context.User.Identity.Name)

                Catch ex As Exception
                    Response.Write(Utils.HandleError(ex))
                End Try
            Next
            myConnection.Close()
            Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
            dgProcesoResultErr.CurrentPageIndex = 0
            dgProcesoResultErr.DataSource = ds 'oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
            dgProcesoResultErr.DataBind()
            dgProcesoResultErr.Visible = True
            lblTotalReg.Text = ds.Tables(0).Rows.Count  'dgProcesoResultErr.Items.Count.ToString.Trim()
        End Sub

        Private Sub dgProcesoResultErr_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResultErr.PageIndexChanged
            dgProcesoResultErr.CurrentPageIndex = IIf(dgProcesoResultErr.PageCount < e.NewPageIndex, 0, e.NewPageIndex)

            Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
            dgProcesoResultErr.DataSource = ds 'oproc.GetRegistrosResultadoProcesoDescuentosErrores(Request.Params("id"))
            dgProcesoResultErr.DataBind()
            lblTotalReg.Text = ds.Tables(0).Rows.Count  'dgProcesoResultErr.Items.Count.ToString.Trim()

        End Sub

        Private Sub dgProcesoResultVA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResultVA.PageIndexChanged
            dgProcesoResultVA.CurrentPageIndex = IIf(dgProcesoResultVA.PageCount < e.NewPageIndex, 0, e.NewPageIndex)
            Dim ds As DataSet = oproc.GetRegistrosResultadoProcesoDescuentosValidos(Request.Params("id"))
            dgProcesoResultVA.DataSource = ds
            dgProcesoResultVA.DataBind()
            lblTotalReg.Text = ds.Tables(0).Rows.Count
        End Sub
    End Class
End Namespace

Imports System.Data.SqlClient
Imports System.IO
Namespace BIFConvenios


    Partial Class AccesoArchivoGenerado
        Inherits Page
        Protected WithEvents hlEnlaceArchivo As HyperLink
        Protected oProceso As New Proceso()
        Protected PID As String = ""
        Protected formatoArchivo As String = ""
        Protected situacionTrabajador As String = ""
        Protected strTipoCliente As String = ""

        Protected strCodProceso As String
        Protected strCodIBS As String = String.Empty
        Protected strConsulta As String = String.Empty

        Protected oproc As New Proceso()

        ''Protected WithEvents ltrlNombre As System.Web.UI.WebControls.Literal
        'Protected WithEvents ltrlAnhio As System.Web.UI.WebControls.Literal
        'Protected WithEvents ltrlMes As System.Web.UI.WebControls.Literal
        'Protected WithEvents ltrlFechaIBS As System.Web.UI.WebControls.Literal
        Protected idGenFile As String = ""
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
            'Put user code to initialize the page here

            strCodIBS = Session("strCodIBS")
            strConsulta = Session("consultar")
            strCodProceso = Session("strCodProceso")

            If Not Page.IsPostBack Then

                If Request.Params("id") IsNot Nothing Then

                    PID = Request.Params("id").Split("|")(0)
                    formatoArchivo = Request.Params("id").Split("|")(1)
                    situacionTrabajador = Request.Params("id").Split("|")(2)
                    Dim dr As SqlDataReader = oproc.InformeProceso(PID)
                    If dr.Read Then
                        ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                        ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                        ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                        ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                        ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                        ltrlFechaProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                        ltrlFormato.Text = formatoArchivo
                        'pnlMail.Visible = CType(dr("bCorreoElectronico"), Boolean)

                        ltrlNombre.Text = CType(dr("Nombre_Cliente"), String)
                        ltrlAnhio.Text = CType(dr("Anio_periodo"), String)
                        ltrlMes.Text = MonthName(dr("Mes_Periodo"))
                        ltrlFechaIBS.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                    End If

                    If situacionTrabajador.Trim <> "" Then
                        If situacionTrabajador.Trim = "-" Then
                            strTipoCliente = "-Todos"
                        Else
                            strTipoCliente = "-" + situacionTrabajador
                        End If
                    Else
                        strTipoCliente = ""
                    End If


                    oProceso.UpdRestauraEstadoInicial(Request.Params("id"), Context.User.Identity.Name)

                End If
            End If
        End Sub

        Private Sub lnkDescargar_Click(sender As Object, e As EventArgs) Handles lnkDescargar.Click
            Try
                If Request.Params("id") IsNot Nothing Then
                    PID = Request.Params("id").Split("|")(0)
                    Dim fileFormat As String = Request.Params("id").Split("|")(1)
                    situacionTrabajador = Request.Params("id").Split("|")(2)

                    If situacionTrabajador.Trim <> "" Then
                        If situacionTrabajador.Trim = "-" Then
                            strTipoCliente = "-Todos"
                        Else
                            strTipoCliente = "-" + situacionTrabajador
                        End If
                    Else
                        strTipoCliente = ""
                    End If

                    oProceso.UpdateFechaObtencionArchivo(PID, False, Context.User.Identity.Name)
                    If fileFormat.Trim() = "csv" Then
                        Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".csv")
                    ElseIf fileFormat.Trim() = "xls" Then
                        Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".xls")
                    ElseIf fileFormat.Trim() = "txt" Then
                        Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".txt")
                    ElseIf fileFormat.Trim() = "defaultXls" Then
                        Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".xls")
                    Else
                        If fileFormat.Trim.IndexOf("=") > 0 Then
                            Dim format As String
                            Dim fileType As String
                            format = fileFormat.Split("=")(0)
                            fileType = UCase(fileFormat.Split("=")(1))
                            If fileType = "SDBF" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".DBF")
                            ElseIf fileType = "TXT2" Then
                                If format = "TXTMinsaCAS" Or format = "ESSALUDCESANTES" Or format = "ESSALUDACTIVOS" Or format = "ESSALUDDESEMBOLSOS" Or format = "MDGPTXTACTIVOS" Or format = "MDGPTXTCESANTES" Then 'INI- MODIF 14/07/2020 (P.O.B)
                                    Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID, format) + ".txt")
                                Else 'FIN- MODIF 14/07/2020 (P.O.B)
                                    Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".txt")
                                End If
                            ElseIf fileType = "CSV2" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".csv")
                            ElseIf fileType = "CSVD" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".csv")
                            ElseIf fileType = "XLS2" Then
                                If format = "MDGPEXCELACTIVOS" Or format = "MDGPEXCELCESANTES" Then
                                    Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID, format) + ".xls")
                                Else
                                    Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".xls")
                                End If
                            ElseIf fileType = "SANFERNAND" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".txt")
                            ElseIf fileType = "ADBF" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + situacionTrabajador + oProceso.GetNombreArchivoProceso(PID) + ".26")
                            ElseIf fileType = "JDBF" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + situacionTrabajador + oProceso.GetNombreArchivoProceso(PID) + ".26")
                            ElseIf fileType = "UDBF" Then
                                'ADD 09/10/2013 NCA: ISSUE UNIV GONZAGA, NOS DESCARGA ARCHIVO GENERADO*******
                                If situacionTrabajador.Trim = "-" Then situacionTrabajador = ""
                                '****************************************************************************       
                                Dim Path1 As String = ConfigurationManager.AppSettings("GenFolder") + situacionTrabajador + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".DBF"
                                Dim Path2 As String = ConfigurationManager.AppSettings("GenFolder") + oProceso.getValorAsociadoFormato(PID, format, fileType) + oProceso.GetNombreArchivoProceso(PID) + ".DBF"
                                If File.Exists(Path1) = False Then
                                    ' Noa aseguramos que el archvo origen ha sido creado. 
                                    ' Cerramos el handle. 
                                    Dim fs As FileStream = File.Create(Path1)
                                    fs.Close()
                                End If
                                ' Nos aseguramos que el archivo destino no exista. 
                                If File.Exists(Path2) Then
                                    File.Delete(Path2)
                                End If
                                ' Movemos el archivo.
                                File.Move(Path1, Path2)
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.getValorAsociadoFormato(PID, format, fileType) + oProceso.GetNombreArchivoProceso(PID) + ".DBF")
                            ElseIf fileType = "VDBF" Then   'TODO: AHSP 20080213- Generacion DBF para UNFV
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".DBF")
                            ElseIf fileType = "DEFAULTXLS" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".xls")
                            ElseIf fileType = "ASDF" Then   'TODO: AHSP 20080213 - Generacion de sdf para DIRE Amazonas
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + IIf(situacionTrabajador = "-", "T", situacionTrabajador) + oProceso.GetNombreArchivoProceso(PID) + ".0205")
                            ElseIf fileType = "MINSA" Then   'TODO: AHSP 20080213- Generacion DBF para UNFV
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".txt")
                                'ADD 02/09/2013 NCALLAPINA: GENERAR ARCHIVO COBRANZA PARA UNMSM
                            ElseIf fileType = "MDBF" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".DBF")
                                'END ADD
                            ElseIf fileType = "PRN" Then
                                Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID, format) + ".prn")
                                'END ADD
                            End If
                        Else
                            Response.Redirect(ResolveUrl("../DownloadFile.aspx?File=") + ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim() + oProceso.GetNombreArchivoProceso(PID) + strTipoCliente + ".txt")
                        End If


                    End If
                End If
            Catch ex As Exception
                Response.Write(ex.ToString)
            End Try
        End Sub

        Private Sub lnkGenerarArchivo_Click(sender As Object, e As EventArgs) Handles lnkGenerarArchivo.Click
            If hdId.Value IsNot Nothing And hdId.Value.Trim <> "" Then
                oproc.UpdRestauraEstadoInicial(hdId.Value.Trim.Split("|")(0), Context.User.Identity.Name)
                idGenFile = hdId.Value.Trim
                hdId.Value = ""
                formatoArchivo = idGenFile.Trim.Split("|")(1)

                If formatoArchivo = "defaultXls" Then
                    Response.Redirect(ResolveUrl("/frmEnvioMail.aspx?id=" + strCodProceso + "&ibs=" + strCodIBS + "&consultar=" + strConsulta.ToString()), True)
                Else
                    pnlGenArchivos.Visible = True
                End If

            End If

        End Sub

        Protected Sub lnkCancelar_Click(sender As Object, e As EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ResolveUrl("/ResultadoProcesoCronogramaFuturo.aspx?id=" & strCodProceso + "&codIBS=" + strCodIBS + "&consultar=" + strConsulta), True)
        End Sub
    End Class
End Namespace
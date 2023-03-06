Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.Guid
Imports Resource
'*****************pase - copiar - Ticket 59416*************************
Imports BIFConvenios.Logica
'***********************************************************************

Namespace BIFConvenios

    Partial Class processfile
        Inherits System.Web.UI.Page
        Protected oproc As New Proceso()
        '*****************pase - copiar - Ticket 59416*************************
        Protected oprocesoBL As New ProcesoBL
        '**********************************************************************
        Protected odesc As New ArchivosDescuento()
        Protected pass As Boolean
        Protected Pid As String = ""

        Dim objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient

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
                'If Not oproc.ArchivoDescuentosEnProceso Then
                If Not False Then
                    If Not Request.Params("id") Is Nothing Then
                        pnlResults.Visible = False
                        pnlControls.Visible = True
                        Dim dr As SqlDataReader
                        Pid = CType(Request.Params("id"), String)
                        dr = oproc.InformeProceso(Pid)
                        If dr.Read Then
                            ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                            ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                            ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                            hdCodigoIBS.Value = dr("Codigo_IBS").ToString()
                            hdAnio.Value = dr("Anio_periodo").ToString()
                            hdMes.Value = dr("Mes_Periodo").ToString()
                            ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                            ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                            ltrlProcesoAS400.Text = BIFConvenios.Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                            hdIdProceso.Value = dr("Codigo_proceso").ToString()

                            If dr("FormatoArchivoImportacion").ToString().Trim() <> "default" Then
                                lstFormatFile.Items.Clear()
                                lstFormatFile.Items.Add(New ListItem("Formato Estándar - MS Excel (.xls)", "xls"))
                                lstFormatFile.Items.Add(New ListItem(dr("FormatoArchivoImportacion").ToString().Trim(), dr("FormatoArchivoImportacion").ToString().Trim()))
                                lstFormatFile.Items.Add(New ListItem("Archivo .txt", "txt"))
                                'ADD NCA 20/06/2014 REQ: EA2013 - 273 OPT. CONVENIOS.
                                'lstFormatFile.Items.Add(New ListItem("Formato Estándar - MS Excel (.xls)", "xls"))
                                'END ADD
                            End If

                            lstFormatFile.Items(0).Selected = True
                        End If
                        Utils.AddSwap(lnkProcesar, "Image1", "/BIFConvenios/images/procesar_on.jpg")
                        Utils.AddSwap(lnkCancelar, "Image2", "/BIFConvenios/images/cancelar_on.jpg")
                    End If
                Else
                    pnlResults.Visible = True
                    pnlControls.Visible = False
                    lblResults.Text = "Se esta procesando un archivo de cuotas, intente realizar otro proceso en unos minutos"
                End If
            End If
        End Sub


        'Muestra/oculta los paneles con los mensajes
        Private Sub Result(ByVal bln As Boolean)
            pnlControls.Visible = Not bln
            pnlResults.Visible = bln
            pnlEsperaFinal.VISIBLE = bln
        End Sub

        Private Sub lnkProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkProcesar.Click
            Dim intTipoProceso As Integer = 0
            Dim strSeparador As String = "\\"

            If oproc.ConsultaFlagCargaAutomatica Then
                lblMensaje.Text = "La carga automatica de pagos se esta ejecutando, intentar mas tarde"
            Else
                lblMensaje.Text = ""
                oproc.ActualizaFlagCargaAutomatica("L", 1)
                Dim format As String
                Dim extension As String = ""
                Dim lRuta As String = ""
                Dim lRutaArchivo As String = ""
                Dim lNombreArchivo As String = ""

                format = lstFormatFile.Value.Trim.ToUpper

                Select Case format
                    Case "SDBF", "VDBF"
                        extension = ".dbf"
                        intTipoProceso = 1
                    Case "CSV"
                        extension = ".csv"
                        intTipoProceso = 1
                    Case "TXT"
                        extension = ".txt"
                        intTipoProceso = 1
                        'ADD NCA 20/06/2014 REQ: EA2013 - 273 OPT. CONVENIOS.
                    Case "XLS"
                        extension = ".xls"
                        intTipoProceso = 2
                        'END ADD
                    Case Else
                        'format = "TXT"
                        extension = ".txt"
                        intTipoProceso = 1
                End Select

                If intTipoProceso = 1 Then
                    lNombreArchivo = "AD" + format + hdIdProceso.Value + extension
                    
                    Dim strFileName As String = ConfigurationManager.AppSettings("LoadFolder").ToString() + lNombreArchivo
                    Pid = hdIdProceso.Value

                    Try
                        If File.Exists(strFileName) Then
                            '--eliminamos el existentey cargamos
                            File.Delete(strFileName)
                        End If
                        flnArchivoDescuento.PostedFile.SaveAs(strFileName)
                        pass = True

                        objWSConvenios.ImportaDescuentosEmpresa(Pid, lNombreArchivo, format, Context.User.Identity.Name)

                        lblResults.Text = "El archivo fue cargado exitosamente, recibirá una notificación con los resultados del proceso en unos instantes."
                        'Actualizamos el flag de cargaPagos
                        oproc.ActualizaFlagCargaAutomatica("L", 0)
                    Catch ex As Exception
                        'TODO: Error
                        lblMensaje.Text = "A ocurrido un error en la carga : " + ex.ToString()
                        'Actualizamos el flag de cargaPagos
                        oproc.ActualizaFlagCargaAutomatica("L", 0)
                        '*****************pase - copiar - Ticket 59416*************************
                        oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)
                        '**********************************************************************
                        pass = False
                        Return
                    End Try
                ElseIf intTipoProceso = 2 Then
                    lRuta = ConfigurationManager.AppSettings("ArchivosConvenio").ToString()
                    Dim intResult As Integer = clsFiles.VerifyPath(lRuta, ltrlCliente.Text.Trim() + "-" + hdCodigoIBS.Value, Convert.ToInt32(hdAnio.Value.ToString()), clsFiles.ConvertMes(Convert.ToInt32(hdMes.Value.ToString())), enumProcessType.Recepcion.ToString(), lRutaArchivo)

                    'Verifica Columna
                    intResult = ValidarColumna(lRuta)

                    Pid = hdIdProceso.Value

                    If intResult = 1 Then
                        lNombreArchivo = lRutaArchivo + strSeparador + ltrlCliente.Text.Trim() + "-" + hdCodigoIBS.Value + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ")" + extension

                        If File.Exists(lNombreArchivo) Then
                            File.Delete(lNombreArchivo)
                        End If

                        Try
                            flnArchivoDescuento.PostedFile.SaveAs(lNombreArchivo)

                            pass = True

                            Try
                                Dim strMensaje As String = String.Empty

                                strMensaje = objWSConvenios.ImportaDescuentoEmpresa(Pid, lNombreArchivo, Context.User.Identity.Name)

                                If Len(strMensaje) > 0 Then
                                    'TODO: Error
                                    lblMensaje.Text = "A ocurrido un error en la carga : " + strMensaje
                                    'Actualizamos el flag de cargaPagos
                                    oproc.ActualizaFlagCargaAutomatica("L", 0)
                                    pass = False
                                    Return
                                End If

                            Catch ex As Exception
                                'TODO: Error
                                lblMensaje.Text = "A ocurrido un error en la carga : " + ex.ToString()
                                'Actualizamos el flag de cargaPagos
                                oproc.ActualizaFlagCargaAutomatica("L", 0)
                                '*****************pase - copiar - Ticket 59416*************************
                                oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)
                                '**********************************************************************
                                pass = False
                                Return
                            End Try
                        Catch ex1 As HandledException
                            'TODO: Error
                            lblMensaje.Text = "A ocurrido un error en la carga : " + ex1.ErrorMessageFull
                            'Actualizamos el flag de cargaPagos
                            oproc.ActualizaFlagCargaAutomatica("L", 0)
                            '*****************pase - copiar - Ticket 59416*************************
                            oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)
                            '**********************************************************************
                            pass = False
                            Return
                        Catch ex2 As Exception
                            'TODO: Error
                            lblMensaje.Text = "A ocurrido un error en la carga : " + ex2.ToString()
                            'Actualizamos el flag de cargaPagos
                            oproc.ActualizaFlagCargaAutomatica("L", 0)
                            '*****************pase - copiar - Ticket 59416*************************
                            oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)
                            '**********************************************************************
                            pass = False
                            Return
                        End Try

                        'Actualizamos el flag de cargaPagos
                        oproc.ActualizaFlagCargaAutomatica("L", 0)

                        lblResults.Text = "El archivo fue cargado exitosamente, recibirá una notificación con los resultados del proceso en unos instantes."
                    Else
                        'TODO: Error
                        lblMensaje.Text += "A ocurrido un error en la carga." '+ ex2.ToString()
                        'Actualizamos el flag de cargaPagos
                        oproc.ActualizaFlagCargaAutomatica("L", 0)
                        '*****************pase - copiar - Ticket 59416*************************
                        oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)
                        '**********************************************************************
                        pass = False
                        Return
                    End If

                End If

                Call Result(pass)
            End If

        End Sub

        Private Function ValidarColumna(ByVal lstrRuta As String) As Integer

            Dim lintResultado As Integer = 1
            'Dim lstrMensaje As String = ""
            Dim excelPath As String = Server.MapPath("~/files/") + Path.GetFileName(flnArchivoDescuento.PostedFile.FileName)
            flnArchivoDescuento.PostedFile.SaveAs(excelPath)
            Dim text() As String = System.IO.File.ReadAllLines(excelPath)

            For index As Integer = 1 To (text.Length - 1)

                Dim lstrFila As String = text(index)
                Dim arArray() As String = lstrFila.Split("	")

                For index1 As Integer = 0 To (arArray.Length - 1)

                    If index1 = 15 Then

                        'Validar Columna
                        Dim lstrColumna As String = arArray(index1)

                        If lstrColumna.Length = 0 Then
                            lintResultado = 0
                            lblMensaje.Text += String.Concat("[CuotaDescontada] La fila número ", index, " se encuentra vacía. ")
                        Else
                            Try
                                Dim lintConversion As Integer
                                lintConversion = CDec(lstrColumna)

                                If lstrColumna.IndexOf("E") > -1 Then
                                    lintResultado = 0
                                    lblMensaje.Text += String.Concat("[CuotaDescontada] La fila número ", index, " tiene un valor: ", lstrColumna, ", que no puede convertir. ")
                                End If

                            Catch ex As Exception
                                lintResultado = 0
                                lblMensaje.Text += String.Concat("[CuotaDescontada] La fila número ", index, " tiene un valor: ", lstrColumna, ", que no puede convertir. ")
                            End Try
                        End If

                    End If

                Next

            Next

            Return lintResultado

        End Function

        Private Sub lnkCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ResolveUrl("ProcesarArchivoDescuento.aspx"))
        End Sub
    End Class
End Namespace
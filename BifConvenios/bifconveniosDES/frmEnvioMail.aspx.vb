Imports BIFConvenios
Imports System.Data.SqlClient

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class frmEnvioMail
    Inherits Page

    Protected strMails As String = String.Empty
    Protected strCEFuncionarios As String = String.Empty
    Protected strClient As String = String.Empty
    Protected strConsultar As String = String.Empty

    Protected objSystemParemeters As New clsSystemParametersBL()
    Protected _dtParametrosEnvioMail As New DataTable()

    Protected objArchivosConvenioBL As New clsArchivosConveniosBL()
    Protected objArchivosConvenio As New clsArchivosConvenios()

    Protected objClienteBL As New clsClienteBL()

    Protected objResponsableOficinaBL As New clsResponsableOficinaBL()
    Protected objResponsableOficina As New clsReponsableOficina

    Protected objProcesoBL As New clsProcesoBL()

    Dim strCodigoProceso As String = String.Empty
    Dim strTrabajador As String = String.Empty
    Dim strDocumento As String = String.Empty
    Dim decPagare As Decimal = 0
    Dim strEstadoTrabajador As String = String.Empty
    Dim strZonaUse As String = String.Empty

    Protected strCodCliente As String
    Protected strCodIBS As String

    Protected strMensaje As String

    Protected oCliente As New BIFConvenios.Cliente()
    Protected oProceso As New BIFConvenios.Proceso()
    Protected formatoArchivo As String = ""
    Protected situacionTrabajador As String = ""
    Protected strTipoCliente As String = ""

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

    'Clase para funcionar como container para el origen de datos del DG
    Protected Class MailSource
        Private mmail As String

        Sub New(mail As String)
            mmail = mail
        End Sub

        Property Mail() As String
            Get
                Return mmail
            End Get
            Set(Value As String)
                mmail = Value
            End Set
        End Property
    End Class

    Private Sub CargarParametrosEnvioMail(ByRef _dtParametro As DataTable)
        Try
            _dtParametro = objSystemParemeters.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.ParametroEnvioMail.ToString()))
        Catch ex As HandledException

        End Try
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" 

        strCodigoProceso = Request.Params("id").ToString()
        strCodIBS = Request.Params("ibs").ToString()
        strConsultar = Request.Params("consultar").ToString()

        Dim Anio_periodo As String = String.Empty
        Dim Mes_Periodo As String = String.Empty

        If Not Page.IsPostBack Then
            Dim strEmailCliente As String = String.Empty
            Dim dr1 As SqlDataReader = oCliente.GetEmails(strCodigoProceso)

            CargarParametrosEnvioMail(_dtParametrosEnvioMail)

            Dim CorreoElectronico As String
            If Context.User.Identity.Name.ToString() = "" Then
                CorreoElectronico = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailEnvio))("vValor").ToString().Trim()
            Else
                CorreoElectronico = Usuario.spObtenerCorreoUsuario(Context.User.Identity.Name.ToString())
            End If

            txtDE.Text = CorreoElectronico

            If dr1.Read Then
                strCodCliente = CType(dr1("Codigo_Cliente"), String)
                strClient = CType(dr1("Nombre_Cliente"), String)
                Session("strCliente") = strClient
                Session("strCodCliente") = strCodCliente
                'strEmailCliente = CType(dr("CorreoElectronico"), String)
                Anio_periodo = CType(dr1("Anio_periodo"), String)
                Mes_Periodo = CType(dr1("Mes_Periodo"), String)

                hdAnio.Value = Anio_periodo
                hdMes.Value = Mes_Periodo
            End If

            Dim ar As New ArrayList()
            Dim str As Object

            Dim dr2 As SqlDataReader = oCliente.spObtenerEMailsEnviosClientes(strCodCliente)

            If dr2.Read Then
                strEmailCliente = CType(dr2("CORREOS"), String)
            End If

            For Each str In strEmailCliente.Split(",")
                ar.Add(New MailSource(str))
            Next

            gvCoordinadores.DataSource = ar
            gvCoordinadores.DataBind()

            'Listado de Funcionarios, segun requerimiento de cambio.

            Dim _dtFuncionarios As DataTable = objClienteBL.ObtenerListaFuncionariosConveniosDesdeAS400()

            ar.Clear()
            strEmailCliente = ""

            If _dtFuncionarios.Rows.Count > 0 Then
                For Each _dr As DataRow In _dtFuncionarios.Rows
                    If Len(_dr("FUNCOR").ToString()) > 0 Then
                        If Len(strEmailCliente) = 0 Then
                            strEmailCliente = _dr("FUNCOR").ToString()
                        Else
                            strEmailCliente = strEmailCliente & "," & _dr("FUNCOR").ToString()
                        End If
                    End If
                Next
            End If

            For Each str In strEmailCliente.Split(",")
                ar.Add(New MailSource(str))
            Next

            ar.Add(New MailSource("alanazabache@gmail.com"))

            gvFuncionarios.DataSource = ar
            gvFuncionarios.DataBind()

            'fin de listado de funcionarios

            'listado de Responsables de oficina, segun requerimiento de cambio.

            Dim _dtCliente As DataTable = objClienteBL.ObtenerClienteDesdeAS400PorCodIBS(strCodIBS)

            objResponsableOficina.iResponsableId = 0
            objResponsableOficina.vOficina = _dtCliente.Rows(0)("NOMOFICINA").ToString()
            objResponsableOficina.vNombreResponsable = ""

            Try
                Dim _dtResponsableOficina As DataTable = objResponsableOficinaBL.ObtenerResponsableOficinaPorCriterio(objResponsableOficina)

                ar.Clear()
                strEmailCliente = ""

                If _dtResponsableOficina.Rows.Count > 0 Then
                    For Each _dr As DataRow In _dtResponsableOficina.Rows
                        If Len(_dr("vCorreoResponsable").ToString()) > 0 Then
                            If Len(strEmailCliente) = 0 Then
                                strEmailCliente = _dr("vCorreoResponsable").ToString()
                            Else
                                strEmailCliente = strEmailCliente & "," & _dr("vCorreoResponsable").ToString()
                            End If
                        End If
                    Next
                End If

                For Each str In strEmailCliente.Split(",")
                    ar.Add(New MailSource(str))
                Next

                gvResponsables.DataSource = ar
                gvResponsables.DataBind()

            Catch ex1 As HandledException
                pnlMensaje.Visible = True
                lblMensaje.Text = "No se encuentra el correo del Responsable de la Oficina: " & objResponsableOficina.vOficina & ". Se mostrará la Lista Vacia"
            End Try

            'fin de listado de responsables de oficina

            'txtComentario.Text = ConfigurationManager.AppSettings("mailCFBody").ToString.Replace("#1", Periodo.GetMonthByNumber(Mes_Periodo)).Replace("#2", Anio_periodo)
            txtComentario.Text = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.CuerpoNomina))("vValor").ToString().Trim().Replace("&1", Periodo.GetMonthByNumber(Mes_Periodo)).Replace("&2", Anio_periodo)

        End If
    End Sub

    Private Sub lnkEnviarEmail_Click(sender As Object, e As EventArgs) Handles lnkEnviarEmail.Click
        CargarParametrosEnvioMail(_dtParametrosEnvioMail)

        Dim strMails As String
        'Dim strPath As String = System.Configuration.ConfigurationManager.AppSettings("ArchivosConvenio").ToString()
        Dim strPath As String = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.RutaDescargaNominas))("vValor").ToString().Trim()
        'Dim intAnio As Integer = Year(DateTime.Now)
        'Dim intMonth As Integer = Month(DateTime.Now)
        Dim strProcessType As String = enumProcessType.Envio.ToString()
        Dim strNameFile As String = String.Empty
        Dim strPathFile As String = String.Empty

        strCodigoProceso = Request.Params("id")
        strCodIBS = Request.Params("ibs").ToString()

        lblMensaje.Text = ""

        strDocumento = Session("documento").ToString()
        strTrabajador = Session("trabajador").ToString()
        decPagare = Session("pagare").ToString()
        strEstadoTrabajador = Session("EstadoTrabajador").ToString()
        strZonaUse = CType(Session("ZonaUse"), String)

        Dim strC As String = Session("strCliente").ToString().Trim()

        'Si no este TEST hacemos el envio regular
        Dim strTestOnly As String = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ModoPrueba))("vValor").ToString().Trim()

        If strTestOnly = "0" Then
            strMails = GetCheckedMails()
        Else    ' en otro caso enviamos el correo a una direccion de prueba
            strMails = ConfigurationManager.AppSettings("mailTest").ToString()
            'strMails = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ListaMailTest))("vValor").ToString().Trim()
        End If

        Dim strCorreoFun As String = GetCheckedFun()

        If strCorreoFun.Length > 0 Then
            strMails = strMails + "," + strCorreoFun
        End If

        Dim intResult As Integer

        Dim strAnioPeriodo As String = hdAnio.Value
        Dim strMesPeriodo As String = hdMes.Value

        'Colocamos la actualizacion del proceso, del estado P2 a G0
        oProceso.GetFinalGeneracionArchivo(strCodigoProceso, "Server")
        'fin de actualizacion del estado del proceso

        Try

            '_dtExportaRegistroProceso = oProceso.ExportRegistrosResultadoProceso(strCodigoProceso, strDocumento, Str, Pagare, EstadoTrabajador, ZonaUse).Tables(0)
            Dim _dtExportaRegistroProceso As DataTable = objProcesoBL.ExportaRegistroResultadoProcesoPorFiltros(strCodigoProceso, strDocumento, strTrabajador, decPagare, strEstadoTrabajador, strZonaUse)

            intResult = clsFiles.ExportToExcel(_dtExportaRegistroProceso, "xls", strPath, strC + "-" + strCodIBS, Convert.ToInt32(strAnioPeriodo), Convert.ToInt32(strMesPeriodo), strProcessType, strNameFile, strPathFile, strMensaje)

            If (intResult = 0) Then
                'Colocamos la actualizacion del proceso, del estado: G0 a G1:
                oProceso.UpdEstadoGeneracionExito(strCodigoProceso, "Server")
                'Fin actualizacion del proceso

                Dim strFullName As String = strPathFile + "\\" + strNameFile
                strCodCliente = Session("strCodCliente").ToString()

                Dim dr3 As SqlDataReader = oCliente.spObtenerEMailsFuncionarioCliente(strCodCliente)

                If dr3.Read Then
                    If Len(dr3("CORREOS").ToString()) > 0 Then
                        strCEFuncionarios = txtDE.Text + ";" + CType(dr3("CORREOS"), String)
                    Else
                        strCEFuncionarios = txtDE.Text
                    End If

                End If

                If strCEFuncionarios.Length = 0 Then
                    strCEFuncionarios = txtDE.Text
                End If

                Utils.SendNotification(txtDE.Text,
                                        strMails,
                                        strCEFuncionarios,
                                        _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.AsuntoNomina))("vValor").ToString().Trim(),
                                        txtComentario.Text,
                                        strFullName,
                                        notifyTo:=(_dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailEnvio))("vValor").ToString().Trim()))


                'Utils.SendNotification(txtDE.Text, strMails, strCEFuncionarios, _
                '                            ConfigurationManager.AppSettings("mailCFSubject").ToString(), txtComentario.Text, strFullName, _
                '                            notifyTo:=(ConfigurationManager.AppSettings("mailSender").ToString()))

                pnlClose.Visible = True

                objArchivosConvenio.vCodProceso = strCodigoProceso
                objArchivosConvenio.vNombreArchivo = strNameFile
                objArchivosConvenio.vRutaCreacion = strPathFile
                objArchivosConvenio.vRutaModificacion = String.Empty
                objArchivosConvenio.vRutaHistorico = String.Empty
                objArchivosConvenio.iEstado = 1
                objArchivosConvenio.vUsuarioCreacion = Context.User.Identity.Name
                objArchivosConvenio.dFechaCreacion = DateTime.Now()

                Dim iInsert As Integer = objArchivosConvenioBL.Insert(objArchivosConvenio)
            Else
                lblMensaje.Text = strMensaje
                pnlMensaje.Visible = True
            End If

        Catch ex1 As SqlException
            strMensaje = "Error en la base de datos, se produjo el siguiente error: " + ex1.Message + ", con el numero: " + ex1.Number

            lblMensaje.Text = strMensaje
            pnlMensaje.Visible = True
        Catch ex2 As Exception
            strMensaje = "Error: " + ex2.Message

            lblMensaje.Text = strMensaje
            pnlMensaje.Visible = True
        End Try

        'Colocamos la actualizacion del proceso a su estado inicial para que pueda ser descargado nuevamente
        oProceso.UpdRestauraEstadoInicial(strCodigoProceso, "Server")
        'Fin actualizacion del proceso
    End Sub

    'Obtenemos los nombres de los controles que se generan para verificar si
    'estan checkados en el lado del cliente o no
    Protected Function GetControlNames()
        Dim chk As CheckBox
        Dim strNames As String = ""
        Dim i As Integer

        For i = 0 To gvCoordinadores.Rows.Count - 1
            chk = gvCoordinadores.Rows(i).FindControl("chk")
            strNames = strNames + chk.ClientID + ","
        Next

        For i = 0 To gvFuncionarios.Rows.Count - 1
            chk = gvFuncionarios.Rows(i).FindControl("chk")
            strNames = strNames + chk.ClientID + ","
        Next

        If strNames.Trim <> "" Then
            strNames = strNames.Substring(0, strNames.Length - 1)
        End If

        Return strNames
    End Function

    'Obtiene la lista de correos electronicos que han sido seleccionados
    Private Function GetCheckedMails() As String
        Dim chk As CheckBox
        Dim strMails As String = ""
        Dim i As Integer

        For i = 0 To gvCoordinadores.Rows.Count - 1
            chk = gvCoordinadores.Rows(i).FindControl("chk")
            If chk.Checked Then
                strMails &= gvCoordinadores.Rows(i).Cells(1).Text + ","
            End If
        Next

        For i = 0 To gvFuncionarios.Rows.Count - 1
            chk = gvFuncionarios.Rows(i).FindControl("chk")
            If chk.Checked Then
                strMails &= gvFuncionarios.Rows(i).Cells(1).Text + ","
            End If
        Next

        Return strMails
    End Function

    Private Function GetCheckedFun() As String
        Dim chk As CheckBox
        Dim strMails As String = ""
        Dim i As Integer

        For i = 0 To gvFuncionarios.Rows.Count - 1
            chk = gvFuncionarios.Rows(i).FindControl("chk")
            If chk.Checked Then
                strMails &= gvFuncionarios.Rows(i).Cells(1).Text + ","
            End If
        Next

        Return strMails
    End Function

    Protected Sub lnkFinish_Click(sender As Object, e As EventArgs) Handles lnkFinish.Click
        Session("Criterio") = Nothing
        Session("Valor") = Nothing

        Response.Redirect(ResolveUrl("cargageneracioncf.aspx"), True)
    End Sub

    Protected Sub lnkCancelar_Click(sender As Object, e As EventArgs) Handles lnkCancelar.Click
        Session("Criterio") = Nothing
        Session("Valor") = Nothing

        Response.Redirect(ResolveUrl("ResultadoProcesoCronogramaFuturo.aspx?id=" & strCodigoProceso + "&codIBS=" + strCodIBS + "&consultar=" + strConsultar.ToString()))
    End Sub
End Class

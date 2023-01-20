Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports BIFConvenios.BL
Imports BIFConvenios.BE

Imports Resource

Namespace BIFConvenios
    Partial Class cargageneracioncf
        Inherits System.Web.UI.Page

        Protected oCliente As New Cliente()
        Protected oProceso As New Proceso()
        Protected PID As String = ""
        Protected strFilter As String = ""

        Protected strCriterio As String = String.Empty
        Protected strValor As String = String.Empty

        Protected objProcesos As New clsCuotaBL
        Dim objWSConvenios As New wsConvenios.WSBIFConvenios
        Dim objWSEnvioAutomatico As New wsConvenios.wsEnvioAutomatico

        Private objProcesosAutomaticos As New clsProcesosAutomaticos()
        Private objProcesosAutomaticosBL As New clsProcesosAutomaticosBL()


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

#Region "Metodos"

        Protected Sub GetList(ByVal dt As DataTable, ByVal pstrCriterio As String, ByVal pstrValor As String)
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            Try
                Dim dtTemp As New DataTable()
                dtTemp = dt.Clone()

                If pstrCriterio = "0" Then
                    For count As Integer = 0 To dt.Rows.Count - 1
                        dtTemp.ImportRow(dt.Rows(count))
                    Next
                ElseIf pstrCriterio = "1" And Len(pstrValor) > 0 Then
                    For Each row As DataRow In dt.Rows
                        Dim len, len1, len2 As Integer

                        len1 = row(0).ToString().Length
                        len2 = pstrValor.Length

                        len = IIf(len1 >= len2, len2, len1)

                        Dim strCadena As String = row(0).ToString().Substring(0, len)

                        'Dim strCadena As String = IIf(len1 >= len2, row(0).ToString().Substring(0, pstrValor.Length), row(0).ToString())

                        If UCase(strCadena.ToString()) = UCase(pstrValor) Then
                            dtTemp.ImportRow(row)
                        End If
                    Next
                ElseIf pstrCriterio = "2" And Len(pstrValor) > 0 Then
                    For Each row As DataRow In dt.Rows
                        If row(5).ToString.Equals(pstrValor) Then
                            dtTemp.ImportRow(row)
                        End If
                    Next
                Else
                    For count As Integer = 0 To dt.Rows.Count - 1
                        dtTemp.ImportRow(dt.Rows(count))
                    Next
                End If

                If dtTemp.Rows.Count > 0 Then
                    gvDatosCarga.DataSource = dtTemp
                    gvDatosCarga.DataBind()
                Else
                    Throw New HandledException(enumGeneric.NoRecords, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
                End If

            Catch ex1 As HandledException
                pnlMensaje.Visible = True
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                pnlMensaje.Visible = True
                lblMensaje.Text = "Error: " + ex2.Message
            End Try
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here                       

            lblUltimaActualizacionProcesoBatch.Text = AperturaDia.ObtenerUltimaActualizacionProcesoBatch()

            If Not IsPostBack Then
                objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
                objWSEnvioAutomatico.Credentials = System.Net.CredentialCache.DefaultCredentials

                Dim dtEmpresas As New DataTable()

                If Not oProceso.ProcesandoCargaCronogramaFuturo Then
                    dtEmpresas = oProceso.GetInfoProcesosDisponibles("").Tables(0)

                    Session("dtEmpresa") = dtEmpresas

                    pnlInputProceso.Visible = True

                    If Not Request.Params("filter") Is Nothing Then
                        strFilter = Request.Params("filter")
                    Else
                        strFilter = ""
                    End If

                    If Session("Criterio") Is Nothing Then
                        strCriterio = ""
                    Else
                        strCriterio = Session("Criterio").ToString()

                        ddlCriterio.SelectedValue = Session("Criterio").ToString()
                    End If

                    If Session("Valor") Is Nothing Then
                        strValor = ""
                    Else
                        strValor = Session("Valor").ToString()

                        txtValor.Text = strValor
                    End If

                    GetList(dtEmpresas, strCriterio, strValor)

                Else
                    If ViewState("PID") Is Nothing Then
                        Call MostrarResultado(False)
                        pnlInputProceso.Visible = False
                        pnlMensaje.Visible = True
                        lblMensaje.Text = "Se esta realizando el proceso de cronograma futuro para otro cliente, la tabla temporal esta llena. Intente mas tarde."
                    End If
                End If
            End If

        End Sub

        Private Sub lnkConsultar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsultar.Click
            Dim lCodigo_proceso As String = ""

            'Obtenemos el tipo y el numero de documento del cliente
            If (hdcodigoCliente.Value.Trim <> "" And _
                        hdanhio.Value.Trim <> "" And _
                        hdfechaProcesoAS400.Value.Trim <> "" And _
                        hdmes.Value.Trim <> "") Then

                'la carga de la pagina
                Response.Redirect("frmConsultaCuotasCliente.aspx?CodCliente=" + CType(hdcodigoCliente.Value.Trim, String) + "&anio=" + CType(hdanhio.Value.Trim, String) + "&mes=" + CType(hdmes.Value.Trim, String) + "&FechaIBS=" + CType(hdfechaProcesoAS400.Value.Trim, String))
            End If

        End Sub

        Private Sub lnkProcesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkProcesar.Click
            Dim lCodigo_proceso As String = ""

            'Verificamos si la aplicacion del servidor esta disponible para poder utilizarse

            'If Not Utils.TestServer() Then
            '    lblMensajeError.Text = Utils.SERVER_UNAVAILABLE
            '    Exit Sub
            'End If

            'Obtenemos el tipo y el numero de documento del cliente
            If (hdcodigoCliente.Value.Trim <> "" And _
                        hdanhio.Value.Trim <> "" And _
                        hdfechaProcesoAS400.Value.Trim <> "" And _
                        hdmes.Value.Trim <> "") Then

                'Obtener el tipo y el numero de documento del cliente
                Dim Codigo_Cliente As String = CType(hdcodigoCliente.Value.Trim, String)
                Dim fechaProcesoAS400 As String = CType(hdfechaProcesoAS400.Value.Trim, String)

                Dim dr As SqlDataReader = oCliente.GetCliente(Codigo_Cliente)
                'Variables temporales para almacenar informacion del proceso
                Dim NombreCliente As String = ""
                Dim tipoDocumento As String
                Dim numeroDocumento As String
                Dim CustomerNumber As String
                Dim mes As String
                Dim anio As String

                anio = hdanhio.Value.Trim
                mes = CType(hdmes.Value.Trim, Integer).ToString

                'PID = oProceso.AddProceso(Codigo_Cliente, anio, mes, fechaProcesoAS400, context.User.Identity.Name)
                'ViewState("PID") = PID

                '--------Usado en la llamada remoting
                'Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
                'Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
                'Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
                'Dim serverUriSubmition As String
                'Dim serverUriSink As String
                'Dim args As Object() = {}
                '------fin de variables 

                If dr.Read Then
                    tipoDocumento = CType(dr("TipoDocumento"), String)
                    numeroDocumento = CType(dr("NumeroDocumento"), String)
                    NombreCliente = CType(dr("Nombre_Cliente"), String)
                    CustomerNumber = oCliente.GetCustomerNumber(tipoDocumento, numeroDocumento)
                    If CustomerNumber.Trim = "" Then
                        Call MostrarMensajeClienteNoExiste(True)
                        Exit Sub
                    End If
                    'If PID <> "-1" Then
                    '    Call MostrarResultado(True)
                    'Else
                    '    Call MuestraSolicitudReproceso(NombreCliente, Codigo_Cliente, CustomerNumber, anio, mes, fechaProcesoAS400)
                    '    Exit Sub
                    'End If

                    'Call MostrarResultado(True)

                    lblMensaje.Text = "Espere un momento mientras se realiza la carga de la información."

                    'Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
                    Try
                        'serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                        'serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                        'objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                        'objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                        'AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                        'objSender.Submit(PID & "|" & CustomerNumber & "|" & mes & "|" & anio & "|" & fechaProcesoAS400, "ObtenerCronogramaFuturo")

                        'RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                        'Response.Redirect("cargasprop.aspx")

                        Dim objWSConvenios As New wsConvenios.WSBIFConvenios
                        objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
                        lCodigo_proceso = objWSConvenios.ImportaPagaresDeIBS(CustomerNumber, anio, mes, fechaProcesoAS400, Codigo_Cliente, Context.User.Identity.Name)

                        'lCodigo_proceso = objProcesos.ImportaPagareDeIBS(CustomerNumber, anio, mes, fechaProcesoAS400, Codigo_Cliente, Context.User.Identity.Name)

                        If (lCodigo_proceso = "-1") Then
                            MostrarMensajeGeneral(True, "Error: El Cliente ya tiene generado un Proceso para la Fecha: " + anio + " " + mes)
                        ElseIf (lCodigo_proceso = "") Then
                            MostrarMensajeGeneral(True, "Error: No se pudo procesar la solicitud")
                        Else
                            Response.Redirect("ResultadoProcesoCronogramaFuturo.aspx?id=" & lCodigo_proceso + "&codIBS=" + CustomerNumber + "&consultar=0")
                        End If

                    Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                        Call MostrarMensajeGeneral(True, Utils.HandleError(excp))
                    End Try

                End If

            End If
        End Sub

        Private Sub MostrarMensajeClienteNoExiste(ByVal b As Boolean)
            pnlInputProceso.Visible = Not b
            pnlMensaje.Visible = b
            ltrlScript.Visible = False
            lblMensaje.Visible = True
            lblMensaje.Text = "No se encontro la empresa en IBS, verifique el tipo y número de documento de la empresa en esta aplicación."
        End Sub


        Private Sub MostrarMensajeGeneral(ByVal b As Boolean, ByVal str As String)
            pnlInputProceso.Visible = Not b
            pnlMensaje.Visible = b
            ltrlScript.Visible = False
            lblMensaje.Visible = True
            pnlReprocesar.Visible = False
            lblMensaje.Text = str
        End Sub


        Private Sub MostrarResultado(ByVal b As Boolean)
            pnlInputProceso.Visible = Not b
            pnlMensaje.Visible = b
            ltrlScript.Visible = b

            ltrlScript.Text = "<script language=""javascript"">" + vbCrLf
            ltrlScript.Text += "<!---" + vbCrLf
            ltrlScript.Text += "openPage('EsperaFinalProceso.aspx?id=" + PID.ToString() + "', 300, 390);" + vbCrLf
            ltrlScript.Text += "-->" + vbCrLf
            ltrlScript.Text += "</script>" + vbCrLf
        End Sub

        Private Sub MostrarResultadoReproceso(ByVal b As Boolean, ByVal PID As String)
            pnlInputProceso.Visible = Not b
            pnlReprocesar.Visible = Not b
            pnlMensaje.Visible = b
            ltrlScript.Visible = b

            lblMensaje.Text = "Espere unos momentos mientras cargamos los datos nuevamente."
            ltrlScript.Text = "<script language=""javascript"">" + vbCrLf
            ltrlScript.Text += "<!---" + vbCrLf
            ltrlScript.Text += "openPage('EsperaFinalProceso.aspx?id=" + PID.ToString() + "', 300, 390);" + vbCrLf
            ltrlScript.Text += "-->" + vbCrLf
            ltrlScript.Text += "</script>" + vbCrLf
        End Sub

        'Mostramos una ventana para realizar el reproceso 
        'Modificacion 20040309: Adicionado Fecha_procesoAS400 como clave alterna en el programa
        Private Sub MuestraSolicitudReproceso(ByVal NombreCliente As String, _
                                    ByVal Codigo_Cliente As String, _
                                    ByVal CustomerNumber As String, _
                                    ByVal anio As String, ByVal mes As String, _
                                    ByVal Fecha_ProcesoAS400 As String)

            Dim dr As SqlDataReader = oProceso.GetInfoProceso(Codigo_Cliente, anio, mes, Fecha_ProcesoAS400)
            Dim Codigo_proceso As String = String.Empty
            Dim CanReprocess As Boolean = False
            If dr.Read Then
                Codigo_proceso = CType(dr("Codigo_proceso"), Guid).ToString()
                CanReprocess = CType(dr("CanReprocess"), Boolean)
            End If
            If CanReprocess Then
                pnlReprocesar.Visible = True
                pnlMensaje.Visible = False
                pnlInputProceso.Visible = False
                hdIdProcess.Value = Codigo_proceso & "|" & CustomerNumber & "|" & mes & "|" & anio & "|" & Fecha_ProcesoAS400 'Codigo_proceso
                lblInfoProceso.Text = NombreCliente.Trim() + " y el periodo " + BIFConvenios.Periodo.GetMonthByNumber(mes) + " - " + anio + " generado en IBS el " + BIFConvenios.Utils.GetFechaCanonica(Fecha_ProcesoAS400)
            Else
                pnlReprocesar.Visible = False
                pnlMensaje.Visible = True
                pnlInputProceso.Visible = False
                lblMensaje.Text = "No se puede reprocesar, la información se ha enviado a IBS para el empresa " + NombreCliente.Trim() + ", periodo " + BIFConvenios.Periodo.GetMonthByNumber(mes) + " - " + anio + ", procesado en IBS el " + BIFConvenios.Utils.GetFechaCanonica(Fecha_ProcesoAS400) + "."
                lblMensaje.Text += "<BR><BR><a href='cargageneracioncf.aspx'>Regresar</a>"
            End If
        End Sub



        Private Sub Reprocesar()
            '--------Usado en la llamada remoting
            Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
            Dim serverUriSubmition As String
            Dim serverUriSink As String
            Dim args As Object() = {}
            '------fin de variables 

            'Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
            Try
                serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                objSender.Submit(hdIdProcess.Value.Trim(), "ObtenerCronogramaFuturo")

                RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                'Response.Redirect("cargasprop.aspx")
            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                Call MostrarMensajeGeneral(True, Utils.HandleError(excp))
            End Try
        End Sub

        Private Sub lblReprocesar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkReprocesar.Click
            oProceso.DelInfoProceso(hdIdProcess.Value.Split("|")(0), Context.User.Identity.Name)
            MostrarResultadoReproceso(True, hdIdProcess.Value.Split("|")(0))
            Call Reprocesar()
        End Sub

        'Obtiene el enlace con la informacion del proceso
        Protected Function GetMensajeProceso(ByVal tipoDocumento As String, ByVal numeroDocumento As String, ByVal nombreCliente As String, _
                                             ByVal mes As String, ByVal anhio As String, ByVal fechaProcesoAS400 As String) As String
            Dim strEnlace As String = ""
            Dim strCodigoCliente As String = oCliente.ExisteCliente(tipoDocumento, numeroDocumento)
            If strCodigoCliente.Trim <> "-1" Then
                strEnlace = "<a href=""JavaScript:ProcesaCarga('" + strCodigoCliente + "','" + nombreCliente.Replace("'", "\'") + "','" + mes + "','" + anhio + "','" + fechaProcesoAS400 + "','" + BIFConvenios.Periodo.GetMonthByNumber(mes) + "');"">Cargar importes</a>"
            Else
                strEnlace = "No registrado o documento incorrecto"
            End If

            Return strEnlace
        End Function

        Protected Function GetMensajeConsulta(ByVal tipoDocumento As String, ByVal numeroDocumento As String, ByVal nombreCliente As String, _
                                             ByVal mes As String, ByVal anhio As String, ByVal fechaProcesoAS400 As String) As String

            Dim strEnlace As String = ""
            Dim strCodigoCliente As String = oCliente.ExisteCliente(tipoDocumento, numeroDocumento)
            If strCodigoCliente.Trim <> "-1" Then
                strEnlace = "<a href=""JavaScript:ProcesaConsulta('" + strCodigoCliente + "','" + nombreCliente.Replace("'", "\'") + "','" + mes + "','" + anhio + "','" + fechaProcesoAS400 + "','" + BIFConvenios.Periodo.GetMonthByNumber(mes) + "');"">Consultar Cuotas</a>"
            Else
                strEnlace = "No registrado o documento incorrecto"
            End If

            Return strEnlace

        End Function

        'Private Sub dgDatosCarga_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDatosCarga.ItemCreated
        '    Dim intCounter As Integer
        '    Dim objLinkButton As LinkButton

        '    If (e.Item.ItemType = ListItemType.Pager) Then
        '        Dim objCell As TableCell = CType(e.Item.Controls(0), TableCell)
        '        objCell.Controls.Clear()
        '        For intCounter = Asc("A") To Asc("Z")
        '            objLinkButton = New LinkButton()
        '            objLinkButton.CausesValidation = False
        '            objLinkButton.Text = Chr(intCounter)
        '            objLinkButton.CssClass = "CommandButton"
        '            objLinkButton.CommandName = "filter"
        '            objLinkButton.CommandArgument = objLinkButton.Text
        '            objCell.Controls.Add(objLinkButton)
        '            objCell.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
        '        Next

        '        objLinkButton = New LinkButton()
        '        objLinkButton.Text = "(Todos)"
        '        objLinkButton.CssClass = "CommandButton"
        '        objLinkButton.CausesValidation = False
        '        objLinkButton.CommandName = "filter"
        '        objLinkButton.CommandArgument = ""
        '        objCell.Controls.Add(objLinkButton)
        '        objCell.Controls.Add(New LiteralControl("&nbsp;&nbsp;"))
        '    End If
        'End Sub

        'Private Sub dgDatosCarga_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgDatosCarga.ItemCommand
        '    If (e.CommandName = "filter") Then
        '        strFilter = e.CommandArgument

        '        Call BindData()
        '    End If
        'End Sub        

        Protected Sub gvDatosCarga_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDatosCarga.PageIndexChanging
            gvDatosCarga.PageIndex = e.NewPageIndex

            Dim dtSessionEmpresa As New DataTable()

            If (Not Session("dtEmpresa") Is DBNull.Value) Then
                dtSessionEmpresa = CType(Session("dtEmpresa"), DataTable)

                Session("Criterio") = ddlCriterio.Text
                Session("Valor") = txtValor.Text

                GetList(dtSessionEmpresa, ddlCriterio.Text, txtValor.Text)
            End If
        End Sub

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Dim dtSessionEmpresa As New DataTable()

            If (Not Session("dtEmpresa") Is DBNull.Value) Then
                dtSessionEmpresa = CType(Session("dtEmpresa"), DataTable)

                Session("Criterio") = ddlCriterio.Text
                Session("Valor") = txtValor.Text

                GetList(dtSessionEmpresa, ddlCriterio.Text, txtValor.Text)
            End If
        End Sub

        Protected Sub ddlCriterio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCriterio.SelectedIndexChanged
            Dim dtEmpresas As New DataTable()

            Select Case ddlCriterio.SelectedIndex
                Case 0
                    txtValor.Text = ""
                    txtValor.Enabled = False
                Case 1
                    txtValor.Text = ""
                    txtValor.Enabled = True
                Case 2
                    txtValor.Text = ""
                    txtValor.Enabled = True
            End Select

            strCriterio = ""
            strValor = ""

            pnlInputProceso.Visible = True

            dtEmpresas = Session("dtEmpresa")

            GetList(dtEmpresas, strCriterio, strValor)

        End Sub
    End Class
End Namespace
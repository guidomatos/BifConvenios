Imports system.data.sqlclient
Imports System.Configuration.ConfigurationManager

Namespace BIFConvenios

    Partial Class EnvioMail
        Inherits System.Web.UI.Page
        Protected WithEvents lblEnviarEmail As System.Web.UI.WebControls.Label
        Protected formatoArchivo As String = ""
        Protected formatoArchivoDesc As String = ""
        Protected tipoCliente As String = ""
        Protected strEmailCliente As String
        Protected PID As String = ""
        Protected oCliente As New BIFConvenios.Cliente()
        Protected oProceso As New Proceso()
        Protected strNombreArchivo As String = ""
        Protected situacionTrabajador As String = ""
        Protected MODALIDAD As String = ""
        Protected extension As String = ""
        Protected contador As Integer = 0
        Protected sumacuotasol As Double = 0.0
        Protected sumacuotadol As Double = 0.0
        Protected indicadorFormato As String = ""
        'Protected tipoNomina = "E"
        'Protected tipoProceso = "O"

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

            If Not Request.Params("idP") Is Nothing And Not Request.Params("formatoArchivo") Is Nothing Then

                PID = CType(Request.Params("idP"), String)
                formatoArchivo = CType(Request.Params("formatoArchivo"), String)
                tipoCliente = CType(Request.Params("tipoCliente"), String)
                'situacionTrabajador = CType(Request.Params("situacionTrabajador"), String)
                'tipoCliente = cargarDatosMail(situacionTrabajador)

                'If oProceso.EnviarMensajeGeneracionArchivo(PID) Then
                '    Try
                '        If (UCase(formatoArchivo).Equals("STANDARDXLS")) Then
                '            'ADD JCHAVEZH 21/10/2014
                '            MODALIDAD = CType(Request.Params("modalidad"), String)
                '            'END JCHAVEZH 21/10/2014

                '            strNombreArchivo = oProceso.GetNombreArchivoProceso(PID) & tipoCliente & ".xls"
                '            Call (New ArchivosDescuento).GenerarArchivExcelDescuentos(PID, strNombreArchivo, situacionTrabajador, contador, sumacuotasol, sumacuotadol, MODALIDAD)
                '            oProceso.GetFinalGeneracionArchivo(PID, Context.User.Identity.Name)
                '            Proceso.UpdEstadoGeneracionExito(PID, Context.User.Identity.Name)
                '            indicadorFormato = "N"
                '            formatoArchivoDesc = ""
                '        Else
                '            Dim objWSConvenios As New WSConvenios.WSBIFConvenios
                '            objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
                '            objWSConvenios.GeneraCronogramaFuturo(PID, formatoArchivo, situacionTrabajador, Context.User.Identity.Name)
                '            If InStr(formatoArchivo, "=", CompareMethod.Text) > 0 Then
                '                formatoArchivoDesc = formatoArchivo.Split("=")(0)
                '                formatoArchivo = formatoArchivo.Split("=")(1)
                '            End If
                '            indicadorFormato = "A"
                '        End If

                '        If oProceso.GetFinalGeneracionArchivo(PID, Context.User.Identity.Name) Then
                '            pnlMensaje.Visible = True
                '            pnlSwf.Visible = False
                '            lblMensaje.Text = "El Archivo se genero correctamente....."
                '        Else
                '            pnlSwf.Visible = True
                '            pnlMensaje.Visible = False
                '        End If

                '    Catch ex As Exception
                '        Call MostrarMensajeGeneral(True, Utils.HandleError(ex))
                '        Exit Sub
                '    End Try
                'End If

            End If

            If Not Page.IsPostBack Then
                'If Not Request.Params("idP") Is Nothing And Not Request.Params("formatoArchivo") Is Nothing And pnlMensaje.Visible = True Then
                If Not Request.Params("idP") Is Nothing And Not Request.Params("formatoArchivo") Is Nothing Then
                    'Dim Nombre_funcionario As String = ""
                    'Dim Correo_funcionario As String = ""
                    'Dim Telefono_funcionario As String = ""
                    Dim Anio_periodo As String = ""
                    Dim Mes_Periodo As String = ""
                    'Dim Nombre_Cliente As String = ""
                    'Dim Cuerpo_Mensaje As String = ""
                    'Dim Asunto_Mensaje As String = ""
                    'Dim Codigo_Cliente As String = DocumentoCobranza.getClienteProceso(CType(Request.Params("idP"), String))
                    Dim dr As SqlDataReader = oCliente.GetEmails(CType(Request.Params("idP"), String))

                    'Dim dr As SqlDataReader = oProceso.GetInfoProcesoCliente(CType(Request.Params("idP"), String))
                    If dr.Read Then
                        'Mes_Periodo = CType(dr("Mes_Periodo"), String)
                        'Mes_Periodo = Me.GetMesTexto(Mes_Periodo)
                        'Anio_periodo = CType(dr("Anio_periodo"), String)
                        'Nombre_Cliente = CType(dr("Nombre_Cliente"), String)
                        lblCliente.Text = CType(dr("Nombre_Cliente"), String)
                        strEmailCliente = CType(dr("CorreoElectronico"), String)
                        Anio_periodo = CType(dr("Anio_periodo"), String)
                        Mes_Periodo = CType(dr("Mes_Periodo"), String)
                    End If

                    txtComentario.Text = CType(AppSettings("mailCFBody"), String).Replace("#1", Periodo.GetMonthByNumber(Mes_Periodo)).Replace("#2", Anio_periodo)

                    Dim ar As New ArrayList()

                    Dim str As Object
                    For Each str In strEmailCliente.Split(";")
                        ar.Add(New MailSource(str))
                    Next

                    dgGen.DataSource = ar
                    dgGen.DataBind()

                    'dr = oCliente.ObtieneCoordinadorCliente(Codigo_Cliente)
                    'While dr.Read
                    '    strEmailCliente = strEmailCliente & "," & CType(dr("email_coordinador"), String)
                    'End While

                    'dr = oCliente.GetFuncionarioCliente(Codigo_Cliente)
                    'While dr.Read
                    '    Nombre_funcionario = CType(dr("nombre_funcionario"), String)
                    '    Correo_funcionario = CType(dr("email_funcionario"), String)
                    '    Telefono_funcionario = CType(dr("telefono_funcionario"), String)
                    'End While

                    ''leer esto de BD
                    'dr = oProceso.getClienteEmailAviso(3)
                    'If dr.Read Then
                    '    If CType(dr("descripcion_aviso"), String).Equals("Cronograma Futuro") Then
                    '        Cuerpo_Mensaje = CType(dr("cuerpo_mensaje"), String).Replace("</BR>", ChrW(13))
                    '        Asunto_Mensaje = CType(dr("asunto_mensaje"), String)
                    '        Cuerpo_Mensaje = Cuerpo_Mensaje.Replace("[Nombre_Empresa]", Nombre_Cliente).Replace("[Mes]", Mes_Periodo).Replace("[Anio]", Anio_periodo).Replace("[Nombre_Funcionario_Convenios]", Nombre_funcionario).Replace("[Email_Funcionario_Convenios]", Correo_funcionario).Replace("[Anexo_Funcionario_Convenios]", Telefono_funcionario)
                    '        Asunto_Mensaje = Asunto_Mensaje.Replace("[Nombre_Empresa]", Nombre_Cliente)
                    '    End If
                    'End If

                    'Dim strMails As String
                    'If CType(AppSettings("testOnly"), String).Trim = "0" Then
                    '    strMails = strEmailCliente
                    'Else    ' en otro caso enviamos el correo a una direccion de prueba
                    '    strMails = CType(AppSettings("mailBcc"), String)
                    'End If

                    'Dim strNombreArchivoProceso As String
                    'Dim prefijoNombre As String



                    'strNombreArchivoProceso = CType(AppSettings("GenFolder"), String) + _
                    '                             GetNombreFormatoArchivo(formatoArchivo, formatoArchivoDesc, situacionTrabajador, oProceso.GetNombreArchivoProceso(PID), tipoCliente)


                    ''TODO: colocar el cuerpo del correo electronico con los atachados
                    'Utils.SendNotification(CType(AppSettings("mailSender"), String), strMails, _
                    '                        CType(AppSettings("mailBcc"), String), _
                    '                        Asunto_Mensaje, _
                    '                        Cuerpo_Mensaje, strNombreArchivoProceso)

                    'oProceso.UpdateFechaObtencionArchivo(PID, True, Context.User.Identity.Name)

                    'lblMensaje.Text = "El correo fue enviado correctamente...."

                    'oProceso.InsertaNominaEntradaSalida(PID, contador, sumacuotasol, sumacuotadol, tipoNomina, indicadorFormato, tipoProceso, Context.User.Identity.Name)
                    'contador = 0
                    'sumacuotasol = 0.0
                    'sumacuotadol = 0.0
                    'pnlClose.Visible = True
                End If
            End If
        End Sub


        'Obtiene la lista de correos electronicos que han sido seleccionados
        Private Function GetCheckedMails() As String
            Dim chk As CheckBox
            Dim strMails As String = ""
            Dim i As Integer

            For i = 0 To dgGen.Items.Count - 1
                chk = dgGen.Items(i).FindControl("chk")
                If chk.Checked Then
                    strMails = strMails & dgGen.Items(i).Cells(1).Text + ","
                End If
            Next

            Return strMails
        End Function


        'Obtenemos los nombres de los controles que se generan para verificar si
        'estan checkados en el lado del cliente o no
        Protected Function GetControlNames()
            Dim chk As CheckBox
            Dim strNames As String = ""
            Dim i As Integer

            For i = 0 To dgGen.Items.Count - 1
                chk = dgGen.Items(i).FindControl("chk")
                strNames = strNames + chk.ClientID + ","
            Next
            If strNames.Trim <> "" Then
                strNames = strNames.Substring(0, strNames.Length - 1)
            End If

            Return strNames
        End Function

        'Clase para funcionar como container para el origen de datos del DG
        Protected Class MailSource
            Private mmail As String

            Sub New(ByVal mail As String)
                mmail = mail
            End Sub

            Property Mail() As String
                Get
                    Return mmail
                End Get
                Set(ByVal Value As String)
                    mmail = Value
                End Set
            End Property


        End Class

        Private Sub lnkEnviarEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviarEmail.Click
            Dim strMails As String
            'Si no este TEST hacemos el envio regular
            If CType(AppSettings("testOnly"), String).Trim = "0" Then
                strMails = GetCheckedMails()
            Else    ' en otro caso enviamos el correo a una direccion de prueba
                strMails = CType(AppSettings("mailBcc"), String)
            End If


            Dim strNombreArchivoProceso As String

            strNombreArchivoProceso = CType(AppSettings("GenFolder"), String) + _
                                        oProceso.GetNombreArchivoProceso(PID) + tipoCliente + "." + formatoArchivo


            'TODO: colocar el cuerpo del correo electronico con los atachados
            Utils.SendNotification(CType(AppSettings("mailSender"), String), strMails, _
                                    CType(AppSettings("mailBcc"), String), _
                                    CType(AppSettings("mailCFSubject"), String), _
                                    txtComentario.Text, strNombreArchivoProceso)

            oProceso.UpdateFechaObtencionArchivo(PID, True, context.User.Identity.Name)
            pnlClose.Visible = True
        End Sub

        'Rutina para mostrar un error generico
        'Private Sub MostrarMensajeGeneral(ByVal b As Boolean, ByVal str As String)
        '    pnlMensaje.Visible = b
        '    'pnlMail.Visible = Not b
        '    pnlSwf.Visible = Not b
        '    lblMensaje.Text = str
        'End Sub

        'Private Function GetMesTexto(ByVal mes As String)
        '    Dim mesTexto As String = ""
        '    Select Case mes.Trim()
        '        Case "1"
        '            mesTexto = "Enero"
        '        Case "2"
        '            mesTexto = "Febrero"
        '        Case "3"
        '            mesTexto = "Marzo"
        '        Case "4"
        '            mesTexto = "Abril"
        '        Case "5"
        '            mesTexto = "Mayo"
        '        Case "6"
        '            mesTexto = "Junio"
        '        Case "7"
        '            mesTexto = "Julio"
        '        Case "8"
        '            mesTexto = "Agosto"
        '        Case "9"
        '            mesTexto = "Septiembre"
        '        Case "10"
        '            mesTexto = "Octubre"
        '        Case "11"
        '            mesTexto = "Noviembre"
        '        Case "12"
        '            mesTexto = "Diciembre"
        '        Case Else
        '            mesTexto = ""
        '    End Select
        '    Return mesTexto
        'End Function

        'Private Function GetNombreFormatoArchivo(ByVal formatoArchivo As String, ByVal formatoArchivoDesc As String, ByVal pSituacionTrabajador As String, ByVal NombreArchivo As String, ByVal tipoCliente As String)
        '    Dim extension As String = ""
        '    Dim NombreCompleto As String = ""
        '    NombreCompleto = NombreArchivo + tipoCliente + formatoArchivoDesc
        '    Select Case UCase(formatoArchivo.Trim())
        '        Case "CSV"
        '            extension = "csv"
        '        Case "CSV2"
        '            extension = "csv"
        '        Case "CSVD"
        '            extension = "csv"
        '        Case "TXT"
        '            extension = "txt"
        '        Case "TXT2"
        '            extension = "txt"
        '        Case "XLS"
        '            extension = "xls"
        '        Case "XLS2"
        '            extension = "xls"
        '        Case "DEFAULTXLS"
        '            extension = "xls"
        '        Case "SANFERNAND"
        '            extension = "txt"
        '        Case "ADBF"
        '            extension = "26"
        '            NombreCompleto = pSituacionTrabajador + NombreCompleto.Split("-")(0)
        '        Case "JDBF"
        '            extension = "26"
        '            NombreCompleto = pSituacionTrabajador + NombreCompleto.Split("-")(0)
        '        Case "XDBF"
        '            extension = "26"
        '            NombreCompleto = pSituacionTrabajador + NombreCompleto.Split("-")(0)
        '        Case "IDBF"
        '            extension = "DBF"
        '        Case "BDBF"
        '            extension = "26"
        '            NombreCompleto = pSituacionTrabajador + NombreArchivo.Split("-")(0)
        '        Case "UDBF"
        '            extension = "DBF"
        '        Case "VDBF"
        '            extension = "DBF"
        '        Case "MINSA"
        '            extension = "txt"
        '        Case "ASDF"
        '            extension = "0205"
        '            NombreCompleto = IIf(pSituacionTrabajador = "-", "T", pSituacionTrabajador) + NombreArchivo
        '        Case "SDBF"
        '            extension = "DBF"
        '        Case "MDBF"
        '            extension = "DBF"
        '            'ADD 31/10/2014 NCA 
        '        Case "XLS3"
        '            extension = "xls"
        '            'END ADD
        '        Case Else
        '            NombreCompleto = ""
        '            extension = ""
        '    End Select

        '    Return NombreCompleto + "." + extension
        'End Function

        'Protected Function cargarDatosMail(ByVal situacionTrabajador As String)
        '    Dim strTipoCliente As String
        '    If situacionTrabajador.Trim <> "" Then
        '        If situacionTrabajador.Trim = "-" Then
        '            strTipoCliente = "-Todos"
        '        Else
        '            strTipoCliente = "-" + situacionTrabajador
        '        End If
        '    Else
        '        strTipoCliente = ""
        '    End If
        '    Return strTipoCliente
        'End Function

    End Class
End Namespace

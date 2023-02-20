Imports System.Configuration.ConfigurationSettings
Imports System.Web.Mail
Imports System.Data.SqlClient
Imports System.Reflection
Imports BroadcasterClass.GOIntranet
Imports BIFData.GOIntranet
Imports System.IO


Namespace BIFConvenios

    Public Module Utils

        Public Const SERVER_UNAVAILABLE = "El programa de carga y actualización no esta disponible en estos momentos.<br>Por favor, consulte con Tecnología."

        Public Function GetDBConnectionString() As String
            'Return AppSettings("ConnectionString")
            Return strCadenaConexion
            'Return DescifrarCadenaConexion(System.Configuration.ConfigurationManager.AppSettings("ConnectionString"))
            'Return ConfigurationManager.AppSettings("ConnectionString").ToString()

        End Function

        'Public Function DescifrarCadenaConexion(ByVal cadena_cifrada) As String
        '    Dim cadena_descifrada As String = ""
        '    Dim cadena_conexion_antes_password_cifrado As String
        '    Dim password_cifrado As String
        '    Dim cadena_conexion_despues_password_cifrado As String
        '    Dim temporal As String
        '    Dim password_descifrado As String

        '    'MOD NCA 24/10
        '    cadena_descifrada = cadena_cifrada
        '    If System.Configuration.ConfigurationManager.AppSettings("cifrado") = 1 Then
        '        'codigo original 
        '        Dim c As New bifwebservices.DecodificarClave
        '        Try
        '            cadena_conexion_antes_password_cifrado = cadena_cifrada.Substring(0, cadena_cifrada.IndexOf("Password") + 9)
        '            temporal = cadena_cifrada.Substring(cadena_cifrada.IndexOf("Password") + 9)
        '            cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"))
        '            password_cifrado = temporal.Substring(0, temporal.IndexOf(";"))
        '            password_descifrado = c.Decodifica(password_cifrado)
        '            'password_descifrado = temporal.Substring(0, temporal.IndexOf(";"))
        '            cadena_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado
        '            '''Return cadena_descifrada --descomentar
        '        Catch ex As Exception
        '            '''Return ""    --descomentar
        '            cadena_descifrada = ""
        '        End Try
        '        'codigo original 
        '    End If
        '    'END ADD
        '    Return cadena_descifrada
        'End Function

#Region "Funciones de Interfaz de usuario"
        Public Sub AddSwap(ByVal lnkBtn As LinkButton, ByVal objectName As String, ByVal imgName As String)
            lnkBtn.Attributes.Add("onMouseOut", "MM_swapImgRestore()")
            lnkBtn.Attributes.Add("onMouseOver", "MM_swapImage('" + objectName + "','','" + imgName + "',1)")
        End Sub


        'Obtenemos los nombres de los controles que se generan para verificar si
        'estan checkados en el lado del cliente o no
        Public Function GetControlNames(ByVal dgGen As DataGrid, ByVal CheckBoxName As String)
            Dim chk As CheckBox
            Dim strNames As String = ""
            Dim i As Integer

            For i = 0 To dgGen.Items.Count - 1
                chk = dgGen.Items(i).FindControl(CheckBoxName)
                strNames = strNames + chk.ClientID + ","
            Next
            If strNames.Trim <> "" Then
                strNames = strNames.Substring(0, strNames.Length - 1)
            End If

            Return strNames
        End Function
#End Region
#Region "Manejo de excepciones en el servidor"

        'Maneja los errores que podrian ocurrir en la aplicación
        'TODO: extender el manejo de errores
        Public Function HandleError(ByVal e As Exception) As String
            Dim returnValue As String = ""
            If TypeOf e Is System.Net.Sockets.SocketException Then
                returnValue = SERVER_UNAVAILABLE
            ElseIf TypeOf e Is System.Threading.ThreadAbortException Then
                'Esta Excepcion se origina al cambiar la pagina haciendo un redirect
                Return returnValue
            Else
                returnValue = "Ha ocurrido un error: " + e.ToString
                Utils.SendNotification("BIFConvenios@bif.com.pe", AppSettings("NotificarA"), "", "Error", returnValue)
            End If
            Return returnValue
        End Function


        'Verificamos si la aplicacion servidor esta disponible 
        'Establece si se puede o no utilizar el servidor
        Public Function TestServer() As Boolean
            '--------Usado en la verificacion
            Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
            Dim serverUriSubmition As String
            Dim serverUriSink As String
            Dim args As Object() = {}
            Dim blnResult As Boolean = True

            '------fin de variables usadas en la validacion

            Try
                serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                objSender.Submit("Test", "Test")

                RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                blnResult = False
            End Try
            '-------------------
            Return blnResult
        End Function


#End Region
#Region "Funciones utiles"


        'Devuelve la fecha en formato dd/MM/yyyy
        Public Function GetFechaCanonica(ByVal strFecha As String) As String
            Dim strFechaConfigurada As String = Right(strFecha, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Left(strFecha, 4)
            Return strFechaConfigurada
        End Function


        ' sends a simple email
        Public Sub SendNotification(ByVal strFrom As String, ByVal strTo As String, _
                    ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, _
                    Optional ByVal strAttachment As String = "", Optional ByVal FormatHtml As Boolean = False, _
                    Optional ByVal notifyTo As String = "")
            ' Obtain PortalSettings from Current Context

            Dim mail As New MailMessage()

            If FormatHtml Then
                mail.BodyFormat = MailFormat.Html
            End If

            mail.From = strFrom
            mail.To = strTo
            If strBcc <> "" Then
                mail.Bcc = strBcc
            End If
            mail.Subject = strSubject
            mail.Body = strBody


            'Adicionar el header de notificacion a un correo opcional 
            'X-Confirm-reading-to Address to which notifications are to be sent and a request to get delivery notifications 
            'Read-Receipt-To Address to which notifications are to be sent and a request to get delivery notifications 

            If Trim(notifyTo) <> "" Then
                mail.Headers.Add("X-Confirm-reading-to", notifyTo)
                mail.Headers.Add("Read-Receipt-To", notifyTo)
                'mail.Headers.Add("return-receipt-to", notifyTo)
                mail.Headers.Add("Disposition-Notification-To", notifyTo)
                'urn:schemas:mailheader:return-receipt-to
                'mail.Headers.Add("Return-Path", notifyTo)
                'mail.Headers.Add("Return-Receipt-Requested", notifyTo)
                'mail.Headers.Add("Return-Receipt-To Address", notifyTo)
            End If

            If strAttachment <> "" Then
                mail.Attachments.Add(New MailAttachment(strAttachment))
            End If

            ' external SMTP server
            If AppSettings("SMTPServer") <> "" Then
                SmtpMail.SmtpServer = AppSettings("SMTPServer")
            End If

            Try
                SmtpMail.Send(mail)
            Catch
                ' mail configuration problem
            End Try

        End Sub


        'Obtenemos la informacion acerca del año del proceso
        Public Function ExistsUser(ByVal UserId As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId})

            myCommand.CommandTimeout = 10000000
            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, Boolean)
            myCommand.Dispose()
            myConnection.Close()
            Return returnValue
        End Function

        'Establecer si el usuario puede tener acceso a opcion
        Public Function isAccessallowed(ByVal UserId As String, ByVal Nombre As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, Nombre})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, Boolean)
            myCommand.Dispose()
            myConnection.Close()
            Return returnValue
        End Function

        'Función para dar formato a una fecha, la máscara usada es yyyymmdd
        Public Function GetSQLDate(ByVal strDate As String) As String
            If (strDate <> "") Then
                Dim strYear As String = Right(strDate, 4)
                Dim strMonth As String = Mid(strDate, 4, 2)
                Dim strDay As String = Left(strDate, 2)

                strDate = strYear & strMonth & strDay
            End If

            Return strDate
        End Function

        Public Function getWebServerDateId() As String
            Return Now.Year.ToString & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & Now.Millisecond.ToString & Now.Ticks.ToString
        End Function


        'Removemos archivos en funcion de un lapso de tiempo
        Public Sub RemoveFiles(ByVal strPath As String, ByVal tmsp As TimeSpan)
            Dim di As New DirectoryInfo(strPath)
            Dim fiArr As FileInfo() = di.GetFiles()
            Dim fri As FileInfo

            For Each fri In fiArr
                If fri.Extension.ToString() = ".xls" Or fri.Extension.ToString() = ".csv" Then
                    If (fri.CreationTime < DateTime.Now.Subtract(tmsp)) Then
                        fri.Delete()
                    End If
                End If
            Next
        End Sub

        'Obtenemos la informacion de una imagen desde el sistema de archivos como byte
        Public Function getImageAsByteArray(ByVal path As String) As Byte()
            Dim FilStr As FileStream = File.OpenRead(path) 'New FileStream(path, FileMode.Open)
            Dim BinRed As BinaryReader = New BinaryReader(FilStr)
            Return BinRed.ReadBytes(BinRed.BaseStream.Length)
            FilStr.Close()
            BinRed.Close()
        End Function

        Public Function getUrlPathApplication() As String
            Return HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.PathAndQuery, "")
        End Function

        Public Function getUrlPathApplicationRedirectPage(ByVal newurl As String) As String
            Return HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Url.PathAndQuery, newurl)
        End Function

#End Region

        '/* ADD NCA 08/07/2014 EA2013-273 OPT PROCESOS CONVENIOS */
        Public Sub InvocarArchivo(ByVal RutaArchivo As String, ByVal nombreArchivo As String)
            Dim ms As System.IO.MemoryStream
            Dim fileS As FileStream = File.OpenRead(RutaArchivo & nombreArchivo)

            ms = New MemoryStream(fileS.Length)
            Dim br As BinaryReader = New BinaryReader(fileS)
            Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

            ms.Write(bytesRead, 0, fileS.Length)

            With HttpContext.Current.Response
                .ClearContent()
                .ClearHeaders()
                .ContentType = "application/vnd.ms-excel"
                .AddHeader("Content-Disposition", "inline; filename=" & nombreArchivo)
                .BinaryWrite(ms.ToArray)

                .End()
            End With
        End Sub

        '/* END */

    End Module


End Namespace
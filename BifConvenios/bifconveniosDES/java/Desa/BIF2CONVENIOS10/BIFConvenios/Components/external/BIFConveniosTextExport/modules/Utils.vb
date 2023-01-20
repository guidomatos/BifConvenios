Imports System.Configuration.ConfigurationSettings
Imports System.Web.Mail
Imports System.IO

Module Utils
    Public Function GetDBConnectionString() As String
        Return BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Function

    Public Function DescifrarCadenaConexion(ByVal cadena_cifrada) As String
        Dim cadena_descifrada As String = ""
        Dim cadena_conexion_antes_password_cifrado As String
        Dim password_cifrado As String
        Dim cadena_conexion_despues_password_cifrado As String
        Dim temporal As String
        Dim password_descifrado As String
        'Dim c As New bifwebservices.DecodificarClave
        Try
            cadena_conexion_antes_password_cifrado = cadena_cifrada.Substring(0, cadena_cifrada.IndexOf("Password") + 9)
            temporal = cadena_cifrada.Substring(cadena_cifrada.IndexOf("Password") + 9)
            cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"))
            password_cifrado = temporal.Substring(0, temporal.IndexOf(";"))
            'password_descifrado = c.Decodifica(password_cifrado)
            password_descifrado = temporal.Substring(0, temporal.IndexOf(";"))
            cadena_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado
            Return cadena_descifrada
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub add2log(ByVal fileName As String, ByVal mess As String)
        Try

            Dim myMess As String = Replace(mess, ",", "")
            myMess = Trim(myMess)
        myMess = myMess.Replace(Environment.NewLine, " -- ")
        Dim nd As New DirectoryInfo(System.Environment.CurrentDirectory & "\logs")
        If nd.Exists = False Then
            nd.Create()
        End If
        Dim myFileName As String = System.Environment.CurrentDirectory & "\logs\" & fileName.Replace("\", "") & ".txt"
        Dim myFileStream As New System.IO.FileStream(myFileName, FileMode.Append, FileAccess.Write)
        Dim myWriter As New System.IO.StreamWriter(myFileStream)

        myWriter.WriteLine(myMess)

        myWriter.Close()
            myFileStream.Close()
        Catch
            'Simplemente ignoramos el error de escritura
        End Try
    End Sub


    ' sends a simple email
    Public Sub SendNotification(ByVal strFrom As String, ByVal strTo As String, ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String, Optional ByVal strAttachment As String = "", Optional ByVal FormatHtml As Boolean = False)
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


    'Adicionamos informacion al log de mensajes de pantalla
    Public Sub AddLogMessage(ByVal lstbx As ListBox, ByVal message As String)
        'SyncLock GetType(frmMonitor)
        '    Dim dt As New DateTime()
        '    dt = System.DateTime.UtcNow
        '    lstbx.Items.Add(dt.ToLocalTime() + " " + message) '' AQUI regresar
        '    Utils.add2log(Format(Now, "yyyyMMdd"), dt.ToLocalTime() + " " + message)
        'End SyncLock
    End Sub

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

    'Obtener la informacion en años, meses, dias, horas, minutos y segundos
    Public Function getyyyyMMddhhmmss() As String
        Return Format(Now, "yyyyMMddhhmmss")
    End Function

End Module

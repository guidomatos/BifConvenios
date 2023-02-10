Imports System.Configuration
Imports System.IO
Imports System.Web.Mail
Imports Microsoft.Reporting.WebForms

Namespace WS
    Public Class Utils

        'Removemos archivos en funcion de un lapso de tiempo
        Public Shared Sub RemoveFiles(ByVal strPath As String, ByVal tmsp As TimeSpan)
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

        Public Shared Function CadenaConexion(ByVal strClaveConfiguracion As String) As String
            Dim strCadena As String = ConfigurationManager.AppSettings(strClaveConfiguracion).ToString()
            Dim desEncr As Boolean = IIf(strCadena.IndexOf("{CLAVE}") > 0, True, False)

            If (desEncr) Then
                Dim semilla As String = ConfigurationManager.AppSettings("ENCRIP_SEMILLA").ToString()
                Dim strClaveEn As String = ConfigurationManager.AppSettings(String.Concat(strClaveConfiguracion, "_PWD")).ToString()

                Dim strClaveDes As String
                strClaveDes = New Desencripta.GODesencripta().DesencriptaTexto(semilla, strClaveEn)
                strCadena = strCadena.Replace("{CLAVE}", strClaveDes)
            End If

            Return strCadena
        End Function


        Public Function DescifrarCadenaConexion(ByVal cadena_cifrada) As String

            Return CadenaConexion(cadena_cifrada)

            'Dim cadena_descifrada As String = ""
            'Dim cadena_conexion_antes_password_cifrado As String
            'Dim password_cifrado As String
            'Dim cadena_conexion_despues_password_cifrado As String
            'Dim temporal As String
            'Dim password_descifrado As String

            'cadena_descifrada = cadena_cifrada
            'If ConfigurationManager.AppSettings("cifrado").ToString() = 1 Then
            '    Dim c As New BIFUtils.WebReference.DecodificarClave
            '    Try
            '        cadena_conexion_antes_password_cifrado = cadena_cifrada.Substring(0, cadena_cifrada.IndexOf("Password") + 9)
            '        temporal = cadena_cifrada.Substring(cadena_cifrada.IndexOf("Password") + 9)
            '        cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"))
            '        password_cifrado = temporal.Substring(0, temporal.IndexOf(";"))
            '        password_descifrado = c.Decodifica(password_cifrado)
            '        'password_descifrado = temporal.Substring(0, temporal.IndexOf(";"))
            '        cadena_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado

            '    Catch ex As Exception
            '        cadena_descifrada = ""
            '    End Try
            'End If
            'Return cadena_descifrada

        End Function

        'Devuelve la fecha en formato dd/MM/yyyy
        Public Function GetFechaCanonica(ByVal strFecha As String) As String
            Dim strFechaConfigurada As String = Right(strFecha, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Left(strFecha, 4)
            Return strFechaConfigurada
        End Function

        ' sends a simple email
        Public Shared Sub SendNotification(ByVal strFrom As String, ByVal strTo As String,
                    ByVal strBcc As String, ByVal strSubject As String, ByVal strBody As String,
                    Optional ByVal strAttachment As String = "", Optional ByVal FormatHtml As Boolean = False,
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
            If ConfigurationManager.AppSettings("SMTPServer") <> "" Then
                SmtpMail.SmtpServer = ConfigurationManager.AppSettings("SMTPServer")
            End If

            Try
                SmtpMail.Send(mail)
            Catch
                ' mail configuration problem
            End Try

        End Sub

        Public Function getWebServerDateId() As String
            Return Now.Year.ToString & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & Now.Millisecond.ToString & Now.Ticks.ToString
        End Function


        Public Shared Function RDLC_ExportarExcel(ByVal ReportViewer1 As ReportViewer) As Byte()
            Dim mimeType As String
            Dim encoding As String
            Dim extension As String
            Dim streamIds As String()
            Dim warnings As Warning()
            Dim bytes = ReportViewer1.LocalReport.Render("Excel", Nothing, mimeType, encoding, extension, streamIds, warnings)
            Return bytes
        End Function

    End Class
End Namespace
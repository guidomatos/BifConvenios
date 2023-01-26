Imports System.Net.Mail

Public Class clsUtils
#Region "Funciones Utiles"
    'Devuelve la fecha en formato dd/MM/yyyy
    Public Function GetFechaCanonica(strFecha As String) As String
        Dim strFechaConfigurada As String = Right(strFecha, 2) & "/" & Mid(strFecha, 5, 2) & "/" & Left(strFecha, 4)
        Return strFechaConfigurada
    End Function

    ' sends a simple email
    Public Sub SendNotification(strFrom As String, strTo As String,
                strBcc As String, strSubject As String, strBody As String,
                Optional strAttachment As String = "", Optional FormatHtml As Boolean = False,
                Optional notifyTo As String = "")
        ' Obtain PortalSettings from Current Context

        Dim Smtp_Server As New SmtpClient
        Dim mail As New MailMessage()

        If FormatHtml Then
            mail.IsBodyHtml = True
        End If

        mail.From = New MailAddress(strFrom)
        mail.To.Add(strTo)
        If strBcc <> "" Then
            mail.Bcc.Add(strBcc)
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
            'mail.Attachments.Add(New MailAttachment(strAttachment))
            mail.Attachments.Add(New Attachment(strAttachment))
        End If

        ' external SMTP server
        If ConfigurationManager.AppSettings("SMTPServer") <> "" Then
            Smtp_Server.Host = ConfigurationManager.AppSettings("SMTPServer")
        End If

        'Smtp_Server.UseDefaultCredentials = False
        'Smtp_Server.Credentials = New Net.NetworkCredential("username@gmail.com", "password")
        'Smtp_Server.Port = 587
        'Smtp_Server.EnableSsl = True

        Try
            Smtp_Server.Send(mail)
        Catch
            ' mail configuration problem
        End Try

    End Sub

    Public Function getWebServerDateId() As String
        Return Now.Year.ToString & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & Now.Millisecond.ToString & Now.Ticks.ToString
    End Function

#End Region

End Class

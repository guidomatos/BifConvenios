Imports System.Configuration
Imports System.Web.Mail
Imports Microsoft.VisualBasic

Public Class clsUtils
#Region "Funciones Utiles"
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

#End Region

End Class

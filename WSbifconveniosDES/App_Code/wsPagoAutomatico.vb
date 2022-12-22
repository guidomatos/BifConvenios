Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource
Imports Microsoft.VisualBasic

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ToolboxItem(False)> _
Public Class wsPagoAutomatico
    Inherits System.Web.Services.WebService

    Dim objCobranza As New BIFConvenios.BL.CobranzaBL()

    Protected objSystemParametersBL As New BIFConvenios.BL.clsSystemParametersBL()
    Protected _dtParametrosEnvioMail As New DataTable()

    Private Function EnviarInfoAS400(ByVal pstrCodigoProceso As String) As String

        '--------Usado en la llamada remoting
        Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
        Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
        Dim ComputerName As String = ConfigurationManager.AppSettings("RemotingServer")
        Dim serverUriSubmition As String
        Dim serverUriSink As String
        Dim args As Object() = {}

        Dim strMensaje As String = String.Empty

        '------fin de variables 
        'Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
        Try
            'Desde este punto realizaremos la llamada al procedimiento remoto para enviar la informacion de los descuentos 
            'realizados al AS/400
            'Realizamos la llamada remoting

            serverUriSubmition = "tcp://" & ComputerName & ":" + ConfigurationManager.AppSettings("ipPort") + "/BIFRemotingSubmition"
            serverUriSink = "tcp://" & ComputerName & ":" + ConfigurationManager.AppSettings("ipPort") + "/BIFRemotingEventSink"

            objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
            objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

            AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

            objSender.Submit(pstrCodigoProceso, "EnvioInformacionActualizada")

            RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

            strMensaje = ""
        Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
            'Response.Write(excp.ToString)
            strMensaje = excp.ToString()
        End Try

        Return strMensaje
    End Function

    <WebMethod()> _
    Public Function ProcesarArchivos() As String
        Try

            Dim strPath As String = ConfigurationManager.AppSettings("ArchivoRecepcionConvenio").ToString()
            Dim strMensaje As String = String.Empty
            Dim strCodigoProceso As String = String.Empty

            Dim listFiles As List(Of String) = clsFiles.ObtenerListaArchivos(strPath, strMensaje)

            If strMensaje = "" And listFiles.Count = 0 Then
                Return "No existe Archivos a procesar o hay un error"
            Else
                For Each strFile As String In listFiles
                    strMensaje = objCobranza.ImportaDescuentosEmpresaAutomatico(strFile, "OperadorDES", strCodigoProceso, 0, 0, 0, 0, 0)

                    If strMensaje = "" And strCodigoProceso <> "" Then
                        strMensaje = EnviarInfoAS400(strCodigoProceso)

                        If Len(strMensaje) > 0 Then
                            Return strMensaje
                        End If
                    End If
                Next

                Return ""
            End If
        Catch ex As Exception
            Return ex.ToString()
        End Try

    End Function

End Class

Imports System.Configuration.ConfigurationSettings

Namespace BIFConvenios
    Public Class Remoting
        'Funcion para procesar el envió de los mensajes usando el servidor Remoting
        Public Shared Function sendMessage(ByVal contributor As String, ByVal contribution As String)
            '--------Usado en la llamada remoting
            Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            Dim ComputerName As String = AppSettings("RemotingServer")
            Dim serverUriSubmition As String
            Dim serverUriSink As String
            Dim args As Object() = {}
            '------fin de variables 

            Try
                serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                objSender.Submit(contribution, contributor)
                RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                Utils.HandleError(excp)
                Throw excp
            End Try
        End Function
    End Class

End Namespace

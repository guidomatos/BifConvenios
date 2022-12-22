Imports System
Imports System.Runtime.Remoting.Messaging

Namespace GOIntranet
    'Esta clase es la encargada de recibir el evento que avisa del envio de una notificacion
    Public Class EventSink
        Inherits MarshalByRefObject


        ' <summary>
        ' Esto es para asegurar cuando creamos una instancia Singleton, 
        ' que a pesar de terminar el tiempo la primera instancia nunca muere
        ' </summary>
        ' <returns></returns>
        Public Overrides Function InitializeLifetimeService() As Object
            Return Nothing
        End Function



        <OneWay()> _
        Public Sub SubmissionReceiver(ByVal sender As Object, ByVal e As SubmitEventArgs)

        End Sub
    End Class
End Namespace

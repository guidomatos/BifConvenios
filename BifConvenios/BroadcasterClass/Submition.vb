Namespace GOIntranet

#Region "Eventos"

    'Clase a utilizar como argumento de la funcion
    'serializable para que se pueda enviar a travez de una llamada remota
    <Serializable()> _
     Public Class SubmitEventArgs
        Inherits EventArgs

        Private _string As String = Nothing
        Private _alias As String = Nothing

        Public Sub New(ByVal contribution As String, ByVal contributor As String)
            Me._string = contribution
            Me._alias = contributor
        End Sub


        Public ReadOnly Property Contribution() As String
            Get
                Return _string
            End Get
        End Property



        Public ReadOnly Property Contributor() As String
            Get
                Return _alias
            End Get
        End Property
    End Class



    Public Delegate Sub SubmissionEventHandler(ByVal sender As Object, ByVal submitArgs As SubmitEventArgs)



#End Region



    'Esta clase realiza la llamada a las suscripciones y envia las notificaciones de cada uno de los involucrados
    Public Class SubmitSuscription
        Inherits MarshalByRefObject

#Region "Respuesta a los eventos de la clase"
        '/// <summary>
        '/// Evento para permitir al cliente subscribirse
        '/// </summary>
        Public Event Submision As SubmissionEventHandler

        '/// <summary>
        '/// Verfica el envio de los datos
        '/// </summary>
        '/// <param name="contribution"></param>
        '/// <param name="contributor"></param>
        Public Sub Submit(ByVal contribution As String, ByVal contributor As String)
            Dim e As SubmitEventArgs = New SubmitEventArgs(contribution, contributor)
            RaiseEvent Submision(Me, e)
        End Sub

#End Region


        Public Overrides Function InitializeLifetimeService() As Object
            Return Nothing
        End Function

    End Class
End Namespace

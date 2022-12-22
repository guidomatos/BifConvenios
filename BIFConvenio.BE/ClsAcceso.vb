Public Class ClsAcceso
    ' Fields
    Private _idUsuario As String

    ' Properties
    Public Property idUsuario() As String
        Get
            Return Me._idUsuario
        End Get
        Set(ByVal value As String)
            Me._idUsuario = value
        End Set
    End Property

    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
End Class

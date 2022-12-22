Public Class conexion
    Public Shared lstrCadenaConexion
    Public Shared lstrCadenaConexionIBS

    Public Property CadenaConexion() As String
        Get
            Return lstrCadenaConexion
        End Get
        Set(ByVal value As String)
            lstrCadenaConexion = value
        End Set
    End Property

    Public Property CadenaConexionIBS() As String
        Get
            Return lstrCadenaConexionIBS
        End Get
        Set(ByVal value As String)
            lstrCadenaConexionIBS = value
        End Set
    End Property
End Class

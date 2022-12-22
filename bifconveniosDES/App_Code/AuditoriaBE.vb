Imports Microsoft.VisualBasic

Public Class AuditoriaBE

#Region "Variables Privadas"

    Private iCorrelativo As Integer
    Private iEstado As Integer
    Private vEstado As String
    Private vUsuarioCreacion As String
    Private dFechaCreacion As Date
    Private vUsuarioModificacion As String
    Private dFechaModificacion As Date

#End Region

#Region "Propiedades Publicas"

    Public Property Correlativo() As Integer
        Get
            Return Me.iCorrelativo
        End Get
        Set(ByVal value As Integer)
            Me.iCorrelativo = value
        End Set
    End Property

    Public Property Estado() As Integer
        Get
            Return Me.iEstado
        End Get
        Set(ByVal value As Integer)
            Me.iEstado = value
        End Set
    End Property

    Public Property NombreEstado() As String
        Get
            Return Me.vEstado
        End Get
        Set(ByVal value As String)
            Me.vEstado = value
        End Set
    End Property

    Public Property UsuarioCreacion() As String
        Get
            Return Me.vUsuarioCreacion
        End Get
        Set(ByVal value As String)
            Me.vUsuarioCreacion = value
        End Set
    End Property

    Public Property FechaCreacion() As Date
        Get
            Return Me.dFechaCreacion
        End Get
        Set(ByVal value As Date)
            Me.dFechaCreacion = value
        End Set
    End Property

    Public Property UsuarioModificacion() As String
        Get
            Return Me.vUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me.vUsuarioModificacion = value
        End Set
    End Property

    Public Property FechaModificacion() As Date
        Get
            Return Me.dFechaModificacion
        End Get
        Set(ByVal value As Date)
            Me.dFechaModificacion = value
        End Set
    End Property

#End Region

End Class

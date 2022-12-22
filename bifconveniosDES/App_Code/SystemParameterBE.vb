Imports Microsoft.VisualBasic

Public Class SystemParameterBE : Inherits AuditoriaBE

#Region "Variables Privadas"

    Private iGrupoId As Integer
    Private iParametroId As Integer
    Private vDescripcion As String
    Private vValor As String
    Private iOrden As Integer
    Private iVisible As Integer
    Private vVisible As String
    Private dFechaInicio As Nullable(Of Date)
    Private dFechaFin As Nullable(Of Date)

#End Region

#Region "Propiedades Publicas"

    Public Property GrupoId() As Integer
        Get
            Return Me.iGrupoId
        End Get
        Set(ByVal value As Integer)
            Me.iGrupoId = value
        End Set
    End Property

    Public Property ParametroId() As Integer
        Get
            Return Me.iParametroId
        End Get
        Set(ByVal value As Integer)
            Me.iParametroId = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return Me.vDescripcion
        End Get
        Set(ByVal value As String)
            Me.vDescripcion = value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return Me.vValor
        End Get
        Set(ByVal value As String)
            Me.vValor = value
        End Set
    End Property

    Public Property Orden() As Integer
        Get
            Return Me.iOrden
        End Get
        Set(ByVal value As Integer)
            Me.iOrden = value
        End Set
    End Property

    Public Property Visible() As Integer
        Get
            Return Me.iVisible
        End Get
        Set(ByVal value As Integer)
            Me.iVisible = value
        End Set
    End Property

    Public Property NombreVisible() As String
        Get
            Return Me.vVisible
        End Get
        Set(ByVal value As String)
            Me.vVisible = value
        End Set
    End Property

    Public Property FechaInicio() As Nullable(Of Date)
        Get
            Return Me.dFechaInicio
        End Get
        Set(ByVal value As Nullable(Of Date))
            Me.dFechaInicio = value
        End Set
    End Property

    Public Property FechaFin() As Nullable(Of Date)
        Get
            Return Me.dFechaFin
        End Get
        Set(ByVal value As Nullable(Of Date))
            Me.dFechaFin = value
        End Set
    End Property

#End Region

End Class

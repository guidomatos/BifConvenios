Public Class clsParametro

    Private _iGrupoId As Integer
    Private _iParametroId As Integer
    Private _sDescripcion As String
    Private _sValor As String
    Private _iOrden As Integer
    Private _iVisible As Integer
    Private _iEstado As Integer

    ' Properties
    Public Property GrupoId() As Integer
        Get
            Return Me._iGrupoId
        End Get
        Set(ByVal value As Integer)
            Me._iGrupoId = value
        End Set
    End Property

    Public Property ParametroId() As Integer
        Get
            Return Me._iParametroId
        End Get
        Set(ByVal value As Integer)
            Me._iParametroId = value
        End Set
    End Property

    Public Property Descripcion() As String
        Get
            Return Me._sDescripcion
        End Get
        Set(ByVal value As String)
            Me._sDescripcion = value
        End Set
    End Property

    Public Property Valor() As String
        Get
            Return Me._sValor
        End Get
        Set(ByVal value As String)
            Me._sValor = value
        End Set
    End Property

    Public Property Orden() As Integer
        Get
            Return Me._iOrden
        End Get
        Set(ByVal value As Integer)
            Me._iOrden = value
        End Set
    End Property

    Public Property Visible() As Integer
        Get
            Return Me._iVisible
        End Get
        Set(ByVal value As Integer)
            Me._iVisible = value
        End Set
    End Property

    Public Property TotalRegistros() As Integer
        Get
            Return Me._iEstado
        End Get
        Set(ByVal value As Integer)
            Me._iEstado = value
        End Set
    End Property




End Class

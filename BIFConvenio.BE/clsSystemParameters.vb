Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsSystemParameters
    ' Fields
    Private _iGrupoId As Integer
    Private _iParametroId As Integer
    Private _vDescripcion As String
    Private _vValor As String
    Private _iOrden As Integer
    Private _iVisible As Integer
    Private _dFechaInicio As DateTime
    Private _dFechaFin As DateTime
    Private _iEstado As Integer
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As DateTime
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As DateTime

    ' Properties
    Public Property iGrupoId() As Integer
        Get
            Return Me._iGrupoId
        End Get
        Set(ByVal value As Integer)
            Me._iGrupoId = value
        End Set
    End Property

    Public Property iParametroId() As Integer
        Get
            Return Me._iParametroId
        End Get
        Set(ByVal value As Integer)
            Me._iParametroId = value
        End Set
    End Property

    Public Property vDescripcion() As String
        Get
            Return Me._vDescripcion
        End Get
        Set(ByVal value As String)
            Me._vDescripcion = value
        End Set
    End Property

    Public Property vValor() As String
        Get
            Return Me._vValor
        End Get
        Set(ByVal value As String)
            Me._vValor = value
        End Set
    End Property

    Public Property iOrden() As Integer
        Get
            Return Me._iOrden
        End Get
        Set(ByVal value As Integer)
            Me._iOrden = value
        End Set
    End Property

    Public Property iVisible() As Integer
        Get
            Return Me._iVisible
        End Get
        Set(ByVal value As Integer)
            Me._iVisible = value
        End Set
    End Property

    Public Property dFechaInicio() As DateTime
        Get
            Return Me._dFechaInicio
        End Get
        Set(ByVal value As DateTime)
            Me._dFechaInicio = value
        End Set
    End Property

    Public Property dFechaFin() As DateTime
        Get
            Return Me._dFechaFin
        End Get
        Set(ByVal value As DateTime)
            Me._dFechaFin = value
        End Set
    End Property

    Public Property iEstado() As Integer
        Get
            Return Me._iEstado
        End Get
        Set(ByVal value As Integer)
            Me._iEstado = value
        End Set
    End Property

    Public Property vUsuarioCreacion() As String
        Get
            Return Me._vUsuarioCreacion
        End Get
        Set(ByVal value As String)
            Me._vUsuarioCreacion = value
        End Set
    End Property

    Public Property dFechaCreacion() As DateTime
        Get
            Return Me._dFechaCreacion
        End Get
        Set(ByVal value As DateTime)
            Me._dFechaCreacion = value
        End Set
    End Property

    Public Property vUsuarioModificacion() As String
        Get
            Return Me._vUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._vUsuarioModificacion = value
        End Set
    End Property

    Public Property dFechaModificacion() As DateTime
        Get
            Return Me._dFechaModificacion
        End Get
        Set(ByVal value As DateTime)
            Me._dFechaModificacion = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._iGrupoId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iGrupoId"), Convert.ToInt32(dr.Table.Columns("iGrupoId").ToString), 0))
            Me._iParametroId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iParametroId"), Convert.ToInt32(dr.Table.Columns.Contains("iParametroId").ToString), 0))
            Me._vDescripcion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vDescripcion"), dr.Table.Columns("vDescripcion").ToString, ""))
            Me._vValor = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vValor"), dr.Table.Columns("vValor").ToString, ""))
            Me._iOrden = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iOrden"), Convert.ToInt32(dr.Table.Columns("iOrden").ToString), 0))
            Me._iVisible = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iVisible"), Convert.ToInt32(dr.Table.Columns("iVisible").ToString), 0))
            Me._dFechaInicio = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaInicio"), Convert.ToDateTime(dr.Table.Columns("dFechaInicio").ToString), Convert.ToDateTime("")))
            Me._dFechaFin = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaFin"), Convert.ToDateTime(dr.Table.Columns("dFechaFin").ToString), Convert.ToDateTime("")))
            Me._iEstado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstado"), Convert.ToInt32(dr.Table.Columns("iEstado").ToString), 0))
            Me._vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString, ""))
            Me._dFechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), Convert.ToDateTime(dr.Table.Columns("dFechaCreacion").ToString), Convert.ToDateTime("")))
            Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns("vUsuarioModificacion").ToString, ""))
            Me._dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub

End Class

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsReponsableOficina
    ' Fields
    Private _iResponsableId As Integer
    Private _iOficinaId As Integer
    Private _vOficina As String
    Private _vNombreResponsable As String
    Private _vCorreoResponsable As String
    Private _iEstado As Integer
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As DateTime
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As DateTime

    ' Properties
    Public Property iResponsableId() As Integer
        Get
            Return Me._iResponsableId
        End Get
        Set(ByVal value As Integer)
            Me._iResponsableId = value
        End Set
    End Property

    Public Property iOficinaId() As Integer
        Get
            Return Me._iOficinaId
        End Get
        Set(ByVal value As Integer)
            Me._iOficinaId = value
        End Set
    End Property

    Public Property vOficina() As String
        Get
            Return Me._vOficina
        End Get
        Set(ByVal value As String)
            Me._vOficina = value
        End Set
    End Property

    Public Property vNombreResponsable() As String
        Get
            Return Me._vNombreResponsable
        End Get
        Set(ByVal value As String)
            Me._vNombreResponsable = value
        End Set
    End Property

    Public Property vCorreoResponsable() As String
        Get
            Return Me._vCorreoResponsable
        End Get
        Set(ByVal value As String)
            Me._vCorreoResponsable = value
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
            Me._iResponsableId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iResponsableId"), Convert.ToInt32(dr.Table.Columns("iResponsableId").ToString), 0))
            Me._iOficinaId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iOficinaId"), Convert.ToInt32(dr.Table.Columns("iOficinaId").ToString), 0))
            Me._vOficina = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vOficina"), dr.Table.Columns("vOficina").ToString, ""))
            Me._vNombreResponsable = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vNombreResponsable"), dr.Table.Columns("vNombreResponsable").ToString, ""))
            Me._vCorreoResponsable = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vCorreoResponsable"), dr.Table.Columns("vCorreoResponsable").ToString, ""))
            Me._iEstado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstado"), Convert.ToInt32(dr.Table.Columns("iEstado").ToString), 0))
            Me._vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString, ""))
            Me._dFechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), Convert.ToDateTime(dr.Table.Columns("dFechaCreacion").ToString), Convert.ToDateTime("")))
            Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns.Contains("vUsuarioModificacion").ToString, ""))
            Me._dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub

End Class

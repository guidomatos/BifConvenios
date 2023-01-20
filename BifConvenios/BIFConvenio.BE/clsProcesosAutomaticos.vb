Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsProcesosAutomaticos
    ' Fields
    Private _iProcesoAutomaticoId As Integer
    Private _iTotalRegistros As Integer
    Private _iProcesados As Integer
    Private _iErroneos As Integer
    Private _vMensajeProceso As String
    Private _iEstado As Integer
    Private _vEstado As String
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As String
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As String

    ' Properties
    Public Property iProcesoAutomaticoId() As Integer
        Get
            Return Me._iProcesoAutomaticoId
        End Get
        Set(ByVal value As Integer)
            Me._iProcesoAutomaticoId = value
        End Set
    End Property

    Public Property iTotalRegistros() As Integer
        Get
            Return Me._iTotalRegistros
        End Get
        Set(ByVal value As Integer)
            Me._iTotalRegistros = value
        End Set
    End Property

    Public Property iProcesados() As Integer
        Get
            Return Me._iProcesados
        End Get
        Set(ByVal value As Integer)
            Me._iProcesados = value
        End Set
    End Property

    Public Property iErroneos() As Integer
        Get
            Return Me._iErroneos
        End Get
        Set(ByVal value As Integer)
            Me._iErroneos = value
        End Set
    End Property

    Public Property vMensajeProceso() As String
        Get
            Return Me._vMensajeProceso
        End Get
        Set(ByVal value As String)
            Me._vMensajeProceso = value
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

    Public Property vEstado() As String
        Get
            Return Me._vEstado
        End Get
        Set(ByVal value As String)
            Me._vEstado = value
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

    Public Property dFechaCreacion() As String
        Get
            Return Me._dFechaCreacion
        End Get
        Set(ByVal value As String)
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

    Public Property dFechaModificacion() As String
        Get
            Return Me._dFechaModificacion
        End Get
        Set(ByVal value As String)
            Me._dFechaModificacion = value
        End Set
    End Property
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._iProcesoAutomaticoId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iProcesoAutomaticoId"), Convert.ToInt32(dr.Table.Columns("iProcesoAutomaticoId").ToString), 0))
            Me._iTotalRegistros = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iTotalRegistros"), Convert.ToInt32(dr.Table.Columns("iTotalRegistros").ToString), 0))
            Me._iProcesados = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iProcesados"), Convert.ToInt32(dr.Table.Columns("iProcesados").ToString), 0))
            Me._iErroneos = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iErroneos"), Convert.ToInt32(dr.Table.Columns("iErroneos").ToString), 0))
            Me._vMensajeProceso = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vMensajeProceso"), dr.Table.Columns("vMensajeProceso").ToString, ""))
            Me._iEstado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstado"), Convert.ToInt32(dr.Table.Columns("iEstado").ToString), 0))
            Me._vEstado = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vEstado"), dr.Table.Columns("vEstado"), ""))
            Me._vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString, ""))
            Me._dFechaCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), dr.Table.Columns("dFechaCreacion").ToString, ""))
            Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns("vUsuarioModificacion").ToString, ""))
            Me._dFechaModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), dr.Table.Columns("dFechaModificacion").ToString, ""))
        End If
    End Sub
End Class

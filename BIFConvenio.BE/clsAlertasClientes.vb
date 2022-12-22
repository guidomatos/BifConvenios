Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsAlertasClientes
    Private _iAlertaClienteId As Integer

    Private _iAlertaId As Integer

    Private _iClienteId As Integer

    Private _iDiasAntes As Integer

    Private _iDiasDespues As Integer

    Private _iAdjunto As Integer

    Private _iEstado As Integer

    Private _vUsuarioCreacion As String

    Private _dFechaCreacion As DateTime

    Private _vUsuarioModificacion As String

    Private _dFechaModificacion As DateTime
    Public Property dFechaCreacion() As DateTime
        Get
            Return Me._dFechaCreacion
        End Get
        Set(ByVal value As DateTime)
            Me._dFechaCreacion = value
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

    Public Property iAdjunto() As Integer
        Get
            Return Me._iAdjunto
        End Get
        Set(ByVal value As Integer)
            Me._iAdjunto = value
        End Set
    End Property

    Public Property iAlertaClienteId() As Integer
        Get
            Return Me._iAlertaClienteId
        End Get
        Set(ByVal value As Integer)
            Me._iAlertaClienteId = value
        End Set
    End Property

    Public Property iAlertaId() As Integer
        Get
            Return Me._iAlertaId
        End Get
        Set(ByVal value As Integer)
            Me._iAlertaId = value
        End Set
    End Property

    Public Property iClienteId() As Integer
        Get
            Return Me._iClienteId
        End Get
        Set(ByVal value As Integer)
            Me._iClienteId = value
        End Set
    End Property

    Public Property iDiasAntes() As Integer
        Get
            Return Me._iDiasAntes
        End Get
        Set(ByVal value As Integer)
            Me._iDiasAntes = value
        End Set
    End Property

    Public Property iDiasDespues() As Integer
        Get
            Return Me._iDiasDespues
        End Get
        Set(ByVal value As Integer)
            Me._iDiasDespues = value
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

    Public Property vUsuarioModificacion() As String
        Get
            Return Me._vUsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._vUsuarioModificacion = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal dr As DataRow)
        MyBase.New()
        If (Not Information.IsDBNull(dr)) Then
            Me._iAlertaClienteId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iAlertaClienteId"), Convert.ToInt32(dr.Table.Columns("iAlertaClienteId").ToString()), 0))
            Me._iAlertaId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iAlertaId"), Convert.ToInt32(dr.Table.Columns("iAlertaId").ToString()), 0))
            Me._iClienteId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iClienteId"), Convert.ToInt32(dr.Table.Columns("iClienteId").ToString()), 0))
            Me._iDiasAntes = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iDiasAntes"), Convert.ToInt32(dr.Table.Columns("iDiasAntes").ToString()), 0))
            Me._iDiasDespues = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iDiasDespues"), Convert.ToInt32(dr.Table.Columns("iDiasDespues").ToString()), 0))
            Me._iAdjunto = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iAdjunto"), Convert.ToInt32(dr.Table.Columns("iAdjunto").ToString()), 0))
            Me._iEstado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstado"), Convert.ToInt32(dr.Table.Columns("iEstado").ToString()), 0))
            Me._vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString(), ""))
            Me._dFechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), Convert.ToDateTime(dr.Table.Columns("dFechaCreacion").ToString()), Convert.ToDateTime("")))
            Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns.Contains("vUsuarioModificacion").ToString, ""))
            Me._dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString), Convert.ToDateTime("")))

            'Dim flag As Boolean = dr.Table.Columns.Contains("vUsuarioModificacion")
            'Dim flag1 As Boolean = dr.Table.Columns.Contains("vUsuarioModificacion")
            'Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(flag, flag1.ToString(), ""))
            'Me._dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString()), Convert.ToDateTime("")))
        End If
    End Sub
End Class

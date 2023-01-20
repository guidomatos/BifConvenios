Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsLogEnvioCorreo
    ' Fields
    Private _iEnvioCorreoId As Integer
    Private _iProcesoAutomaticoId As Integer
    Private _iTipoEnvioCorreoId As Integer
    Private _vTipoEnvioCorreoId As String
    Private _iCodigoCliente As Integer
    Private _iCodigoIBS As Integer
    Private _vCodigoProceso As String
    Private _iAnioPeriodo As Integer
    Private _iMesPeriodo As Integer
    Private _vMensajeProceso As String
    Private _iEstado As Integer
    Private _vEstado As String
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As String
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As String

    ' Properties
    Public Property iEnvioCorreoId() As Integer
        Get
            Return Me._iEnvioCorreoId
        End Get
        Set(ByVal value As Integer)
            Me._iEnvioCorreoId = value
        End Set
    End Property

    Public Property iProcesoAutomaticoId() As Integer
        Get
            Return Me._iProcesoAutomaticoId
        End Get
        Set(ByVal value As Integer)
            Me._iProcesoAutomaticoId = value
        End Set
    End Property

    Public Property iTipoEnvioCorreoId() As Integer
        Get
            Return Me._iTipoEnvioCorreoId
        End Get
        Set(ByVal value As Integer)
            Me._iTipoEnvioCorreoId = value
        End Set
    End Property

    Public Property vTipoEnvioCorreoId() As String
        Get
            Return Me._vTipoEnvioCorreoId
        End Get
        Set(ByVal value As String)
            Me._vTipoEnvioCorreoId = value
        End Set
    End Property

    Public Property iCodigoCliente() As Integer
        Get
            Return Me._iCodigoCliente
        End Get
        Set(ByVal value As Integer)
            Me._iCodigoCliente = value
        End Set
    End Property

    Public Property iCodigoIBS() As Integer
        Get
            Return Me._iCodigoIBS
        End Get
        Set(ByVal value As Integer)
            Me._iCodigoIBS = value
        End Set
    End Property

    Public Property vCodigoProceso() As String
        Get
            Return Me._vCodigoProceso
        End Get
        Set(ByVal value As String)
            Me._vCodigoProceso = value
        End Set
    End Property

    Public Property iAnioPeriodo() As Integer
        Get
            Return Me._iAnioPeriodo
        End Get
        Set(ByVal value As Integer)
            Me._iAnioPeriodo = value
        End Set
    End Property

    Public Property iMesPeriodo() As Integer
        Get
            Return Me._iMesPeriodo
        End Get
        Set(ByVal value As Integer)
            Me._iMesPeriodo = value
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
            Me._iEnvioCorreoId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEnvioCorreoId"), Convert.ToInt32(dr.Table.Columns("iEnvioCorreoId").ToString), 0))
            Me._iProcesoAutomaticoId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iProcesoAutomaticoId"), Convert.ToInt32(dr.Table.Columns("iProcesoAutomaticoId").ToString), 0))
            Me._iTipoEnvioCorreoId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iTipoEnvioCorreoId"), Convert.ToInt32(dr.Table.Columns("iTipoEnvioCorreoId").ToString), 0))
            Me._vTipoEnvioCorreoId = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vTipoEnvioCorreoId"), dr.Table.Columns("vTipoEnvioCorreoId"), ""))
            Me._iCodigoCliente = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iCodigoCliente"), Convert.ToInt32(dr.Table.Columns("iCodigoCliente").ToString), 0))
            Me._iCodigoIBS = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iCodigoIBS"), Convert.ToInt32(dr.Table.Columns("iCodigoIBS").ToString), 0))
            Me._vCodigoProceso = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vCodigoProceso"), dr.Table.Columns("vCodigoProceso").ToString, ""))
            Me._iAnioPeriodo = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iAnioPeriodo"), Convert.ToInt32(dr.Table.Columns("iAnioPeriodo").ToString), 0))
            Me._iMesPeriodo = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iMesPeriodo"), Convert.ToInt32(dr.Table.Columns("iMesPeriodo").ToString), 0))
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

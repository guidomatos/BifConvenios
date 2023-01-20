Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data

Public Class clsAlertas
    ' Fields
    Private _iAlertaID As Integer
    Private _iTipoAlerta As Integer
    Private _vTipoAlerta As String
    Private _vNombreAlerta As String
    Private _vDescripcionAlerta As String
    Private _vAsuntoMensaje As String
    Private _vCuerpoMensaje As String
    Private _iEstadoAlerta As Integer
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As DateTime
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As DateTime

    ' Properties
    Public Property iAlertaId() As Integer
        Get
            Return Me._iAlertaID
        End Get
        Set(ByVal value As Integer)
            Me._iAlertaID = value
        End Set
    End Property

    Public Property iTipoAlerta() As Integer
        Get
            Return Me._iTipoAlerta
        End Get
        Set(ByVal value As Integer)
            Me._iTipoAlerta = value
        End Set
    End Property

    Public Property vTipoAlerta() As String
        Get
            Return Me._vTipoAlerta
        End Get
        Set(ByVal value As String)
            Me._vTipoAlerta = value
        End Set
    End Property

    Public Property vNombreAlerta() As String
        Get
            Return Me._vNombreAlerta
        End Get
        Set(ByVal value As String)
            Me._vNombreAlerta = value
        End Set
    End Property

    Public Property vDescripcionAlerta() As String
        Get
            Return Me._vDescripcionAlerta
        End Get
        Set(ByVal value As String)
            Me._vDescripcionAlerta = value
        End Set
    End Property

    Public Property vAsuntoMensaje() As String
        Get
            Return Me._vAsuntoMensaje
        End Get
        Set(ByVal value As String)
            Me._vAsuntoMensaje = value
        End Set
    End Property

    Public Property vCuerpoMensaje() As String
        Get
            Return Me._vCuerpoMensaje
        End Get
        Set(ByVal value As String)
            Me._vCuerpoMensaje = value
        End Set
    End Property

    Public Property iEstadoAlerta() As Integer
        Get
            Return Me._iEstadoAlerta
        End Get
        Set(ByVal value As Integer)
            Me._iEstadoAlerta = value
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
            Me._iAlertaID = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iAlertaId"), Convert.ToInt32(dr.Table.Columns("iAlertaId").ToString), 0))
            Me._iTipoAlerta = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iTipoAlerta"), Convert.ToInt32(dr.Table.Columns("iTipoAlerta").ToString), 0))
            Me._vTipoAlerta = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vTipoAlerta"), dr.Table.Columns("vTipoAlerta").ToString, ""))
            Me._vNombreAlerta = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vNombreAlerta"), dr.Table.Columns("vNombreAlerta").ToString, ""))
            Me._vDescripcionAlerta = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vDescripcionAlerta"), dr.Table.Columns("vDescripcionAlerta").ToString, ""))
            Me._vAsuntoMensaje = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vAsuntoMensaje"), dr.Table.Columns("vAsuntoMensaje").ToString, ""))
            Me._vCuerpoMensaje = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vCuerpoMensaje"), dr.Table.Columns("vCuerpoMensaje").ToString, ""))
            Me._iEstadoAlerta = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstadoAlerta"), Convert.ToInt32(dr.Table.Columns("iEstadoAlerta").ToString), 0))
            Me._vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString, ""))
            Me._dFechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), Convert.ToDateTime(dr.Table.Columns("dFechaCreacion")), Convert.ToDateTime("")))
            Me._vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns("vUsuarioModificacion").ToString, ""))
            Me._dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub
End Class

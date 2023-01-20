Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsArchivosConvenios
    ' Fields
    Private _iArchivoConvenioId As Integer
    Private _vCodProceso As String
    Private _vNombreArchivo As String
    Private _vRutaCreacion As String
    Private _vRutaModificacion As String
    Private _vRutaHistorico As String
    Private _iEstado As Integer
    Private _vUsuarioCreacion As String
    Private _dFechaCreacion As DateTime
    Private _vUsuarioModificacion As String
    Private _dFechaModificacion As DateTime

    ' Properties
    Public Property iArchivoConvenioId() As Integer
        Get
            Return Me._iArchivoConvenioId
        End Get
        Set(ByVal value As Integer)
            Me._iArchivoConvenioId = value
        End Set
    End Property

    Public Property vCodProceso() As String
        Get
            Return Me._vCodProceso
        End Get
        Set(ByVal value As String)
            Me._vCodProceso = value
        End Set
    End Property

    Public Property vNombreArchivo() As String
        Get
            Return Me._vNombreArchivo
        End Get
        Set(ByVal value As String)
            Me._vNombreArchivo = value
        End Set
    End Property

    Public Property vRutaCreacion() As String
        Get
            Return Me._vRutaCreacion
        End Get
        Set(ByVal value As String)
            Me._vRutaCreacion = value
        End Set
    End Property

    Public Property vRutaModificacion() As String
        Get
            Return Me._vRutaModificacion
        End Get
        Set(ByVal value As String)
            Me._vRutaModificacion = value
        End Set
    End Property

    Public Property vRutaHistorico() As String
        Get
            Return Me._vRutaHistorico
        End Get
        Set(ByVal value As String)
            Me._vRutaHistorico = value
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
            Me.iArchivoConvenioId = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iArchivoConvenioId"), Convert.ToInt32(dr.Table.Columns("iArchivoConvenioId").ToString), 0))
            Me.vCodProceso = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vCodProceso"), dr.Table.Columns("iCodConvenio").ToString, ""))
            Me.vNombreArchivo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vNombreArchivo"), dr.Table.Columns("vNombreArchivo").ToString, ""))
            Me.vRutaCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vRutaCreacion"), dr.Table.Columns("vRutaCreacion").ToString, ""))
            Me.vRutaModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vRutaModificacion"), dr.Table.Columns("vRutaModificacion").ToString, ""))
            Me.vRutaHistorico = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vRutaHistorico"), dr.Table.Columns("vRutaHistorico").ToString, ""))
            Me.iEstado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("iEstado"), Convert.ToInt32(dr.Table.Columns("iEstado").ToString), 0))
            Me.vUsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioCreacion"), dr.Table.Columns("vUsuarioCreacion").ToString, ""))
            Me.dFechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaCreacion"), Convert.ToDateTime(dr.Table.Columns("dFechaCreacion").ToString), Convert.ToDateTime("")))
            Me.vUsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("vUsuarioModificacion"), dr.Table.Columns("vUsuarioModificacion").ToString, ""))
            Me.dFechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("dFechaModificacion"), Convert.ToDateTime(dr.Table.Columns("dFechaModificacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub
End Class

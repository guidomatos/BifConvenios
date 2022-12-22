Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data

Public Class clsCoordinadorCliente
    ' Fields
    Private _CodigoCliente As Integer
    Private _CodigoCoordinador As Integer
    Private _NombreCoordinador As String
    Private _EmailCoordinador As String
    Private _Telefono As String
    Private _Anexo As String
    Private _Celular As String
    Private _Cargo As String
    Private _TipoPlanilla As String
    Private _EstadoCoordinador As Integer
    Private _UsuarioCreacion As String
    Private _FechaCreacion As DateTime
    Private _UsuarioModificacion As String
    Private _FechaModificacion As DateTime
    Private _Estado As Boolean

    ' Properties
    Public Property CodigoCliente() As Integer
        Get
            Return Me._CodigoCliente
        End Get
        Set(ByVal value As Integer)
            Me._CodigoCliente = value
        End Set
    End Property

    Public Property CodigoCoordinador() As Integer
        Get
            Return Me._CodigoCoordinador
        End Get
        Set(ByVal value As Integer)
            Me._CodigoCoordinador = value
        End Set
    End Property

    Public Property NombreCoordinador() As String
        Get
            Return Me._NombreCoordinador
        End Get
        Set(ByVal value As String)
            Me._NombreCoordinador = value
        End Set
    End Property

    Public Property EmailCoordinador() As String
        Get
            Return Me._EmailCoordinador
        End Get
        Set(ByVal value As String)
            Me._EmailCoordinador = value
        End Set
    End Property

    Public Property Telefono() As String
        Get
            Return Me._Telefono
        End Get
        Set(ByVal value As String)
            Me._Telefono = value
        End Set
    End Property

    Public Property Anexo() As String
        Get
            Return Me._Anexo
        End Get
        Set(ByVal value As String)
            Me._Anexo = value
        End Set
    End Property

    Public Property Celular() As String
        Get
            Return Me._Celular
        End Get
        Set(ByVal value As String)
            Me._Celular = value
        End Set
    End Property

    Public Property Cargo() As String
        Get
            Return Me._Cargo
        End Get
        Set(ByVal value As String)
            Me._Cargo = value
        End Set
    End Property

    Public Property TipoPlanilla() As String
        Get
            Return Me._TipoPlanilla
        End Get
        Set(ByVal value As String)
            Me._TipoPlanilla = value
        End Set
    End Property

    Public Property EstadoCoordinador() As Integer
        Get
            Return Me._EstadoCoordinador
        End Get
        Set(ByVal value As Integer)
            Me._EstadoCoordinador = value
        End Set
    End Property

    Public Property UsuarioCreacion() As String
        Get
            Return Me._UsuarioCreacion
        End Get
        Set(ByVal value As String)
            Me._UsuarioCreacion = value
        End Set
    End Property

    Public Property FechaCreacion() As DateTime
        Get
            Return Me._FechaCreacion
        End Get
        Set(ByVal value As DateTime)
            Me._FechaCreacion = value
        End Set
    End Property

    Public Property UsuarioModificacion() As String
        Get
            Return Me._UsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._UsuarioModificacion = value
        End Set
    End Property

    Public Property FechaModificacion() As DateTime
        Get
            Return Me._FechaModificacion
        End Get
        Set(ByVal value As DateTime)
            Me._FechaModificacion = value
        End Set
    End Property

    Public Property Estado() As Boolean
        Get
            Return Me._Estado
        End Get
        Set(ByVal value As Boolean)
            Me._Estado = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._CodigoCliente = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("codigo_cliente"), Convert.ToInt32(dr.Table.Columns("codigo_cliente").ToString), 0))
            Me._CodigoCoordinador = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("id_coordinador"), Convert.ToInt32(dr.Table.Columns("id_coordinador").ToString), 0))
            Me._NombreCoordinador = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("nombre_coordinador"), dr.Table.Columns("nombre_coordinador").ToString, ""))
            Me._EmailCoordinador = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("email_coordinador"), dr.Table.Columns("email_coordinador").ToString, ""))
            Me._Telefono = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Telefono"), dr.Table.Columns("Telefono").ToString, ""))
            Me._Anexo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Anexo"), dr.Table.Columns("Anexo").ToString, ""))
            Me._Celular = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Celular"), dr.Table.Columns("Celular").ToString, ""))
            Me._Cargo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Cargo"), dr.Table.Columns("Cargo").ToString, ""))
            Me._TipoPlanilla = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("TipoPlanilla"), dr.Table.Columns("TipoPlanilla").ToString, ""))
            Me._EstadoCoordinador = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("estado_usuario"), dr.Table.Columns("estado_usuario").ToString, ""))
            Me._UsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("usuario_creacion"), dr.Table.Columns("usuario_creacion").ToString, ""))
            Me._FechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("fecha_creacion"), dr.Table.Columns("fecha_creacion").ToString, Convert.ToDateTime("")))
            Me._UsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("usuario_modificacion"), dr.Table.Columns("fecha_modificacion").ToString, ""))
            Me._FechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("fecha_modificacion"), dr.Table.Columns("fecha_modificacion").ToString, ""))
            Me._Estado = Conversions.ToBoolean(Interaction.IIf(dr.Table.Columns.Contains("estado_usuario"), dr.Table.Columns("estado_usuario").ToString(), False))
        End If
    End Sub
End Class

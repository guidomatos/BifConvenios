Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsEventoSistema
    ' Fields
    Private _idEventoSistema As Integer
    Private _Fecha As DateTime
    Private _Hilo As String
    Private _Nivel As String
    Private _Accion As String
    Private _Mensaje As String
    Private _Excepcion As String
    Private _Usuario As String

    ' Properties
    Public Property IdEventoSistema() As Integer
        Get
            Return Me._idEventoSistema
        End Get
        Set(ByVal value As Integer)
            Me._idEventoSistema = value
        End Set
    End Property

    Public Property Fecha() As DateTime
        Get
            Return Me._Fecha
        End Get
        Set(ByVal value As DateTime)
            Me._Fecha = value
        End Set
    End Property

    Public Property Hilo() As String
        Get
            Return Me._Hilo
        End Get
        Set(ByVal value As String)
            Me._Hilo = value
        End Set
    End Property

    Public Property Nivel() As String
        Get
            Return Me._Nivel
        End Get
        Set(ByVal value As String)
            Me._Nivel = value
        End Set
    End Property

    Public Property Accion() As String
        Get
            Return Me._Accion
        End Get
        Set(ByVal value As String)
            Me._Accion = value
        End Set
    End Property

    Public Property Mensaje() As String
        Get
            Return Me._Mensaje
        End Get
        Set(ByVal value As String)
            Me._Mensaje = value
        End Set
    End Property

    Public Property Excepcion() As String
        Get
            Return Me._Excepcion
        End Get
        Set(ByVal value As String)
            Me._Excepcion = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return Me._Usuario
        End Get
        Set(ByVal value As String)
            Me._Usuario = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._idEventoSistema = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("Ident_EventoSistema"), Convert.ToInt32(dr.Table.Columns("Ident_EventoSistema").ToString), 0))
            Me._Fecha = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha"), Convert.ToDateTime(dr.Table.Columns("Fecha").ToString), Convert.ToDateTime("")))
            Me._Hilo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Hilo"), dr.Table.Columns("Hilo").ToString, ""))
            Me._Nivel = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Nivel"), dr.Table.Columns("Nivel").ToString, ""))
            Me._Accion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Accion"), dr.Table.Columns("Accion").ToString, ""))
            Me._Mensaje = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Mensaje"), dr.Table.Columns("Mensaje").ToString, ""))
            Me._Excepcion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Excepcion"), dr.Table.Columns("Excepcion").ToString, ""))
            Me._Usuario = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Usuario"), dr.Table.Columns("Usuario").ToString, ""))
        End If
    End Sub
End Class

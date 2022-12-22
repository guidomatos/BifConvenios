Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Public Class clsCoordinadorClienteList
    ' Fields
    Private _objElements As List(Of clsCoordinadorCliente)

    ' Properties
    Public Property Elements() As List(Of clsCoordinadorCliente)
        Get
            Return Me._objElements
        End Get
        Set(ByVal value As List(Of clsCoordinadorCliente))
            Me._objElements = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._objElements = New List(Of clsCoordinadorCliente)()
    End Sub

    Public Sub New(ByVal Entidad As DataTable)
        Me._objElements = New List(Of clsCoordinadorCliente)
        Dim flag As Boolean = (Information.IsDBNull(Entidad) Or (Entidad.Rows.Count = 0))
        If Not flag Then
            Dim enumerator As IEnumerator = Nothing
            Try
                enumerator = Entidad.Rows.GetEnumerator
                Do While True
                    flag = enumerator.MoveNext
                    If Not flag Then
                        Exit Do
                    End If
                    Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                    Me.Elements.Add(New clsCoordinadorCliente(current))
                Loop
            Finally
                If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
        End If
    End Sub
End Class

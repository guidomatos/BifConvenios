Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data

Public Class clsAlertasList
    ' Fields
    Private _objElements As List(Of clsAlertas)

    ' Properties
    Public Property Elements() As List(Of clsAlertas)
        Get
            Return Me._objElements
        End Get
        Set(ByVal value As List(Of clsAlertas))
            Me._objElements = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        Me._objElements = New List(Of clsAlertas)()
    End Sub

    Public Sub New(ByVal Entidad As DataTable)
        MyBase.New()
        Dim enumerator As IEnumerator = Nothing
        Me._objElements = New List(Of clsAlertas)()
        If (Not (Information.IsDBNull(Entidad) Or Entidad.Rows.Count = 0)) Then
            Try
                enumerator = Entidad.Rows.GetEnumerator()
                While enumerator.MoveNext()
                    Dim _drw As DataRow = DirectCast(enumerator.Current, DataRow)
                    Me.Elements.Add(New clsAlertas(_drw))
                End While
            Finally
                If (TypeOf enumerator Is IDisposable) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
        End If
    End Sub
End Class

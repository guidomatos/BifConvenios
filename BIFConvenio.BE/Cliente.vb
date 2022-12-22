Imports System

Public Class Cliente
    ' Fields
    Private _codibs As Integer
    ' Properties
    Public Property codibs() As Integer
        Get
            Return Me._codibs
        End Get
        Set(ByVal value As Integer)
            Me._codibs = value
        End Set
    End Property

    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
End Class

Imports BIFConvenios.DO
Imports System
Public Class clsAccesoBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Fields
    Private AccDO As clsAccesoDO = New clsAccesoDO
    ' Methods
    Public Function GetBuscarPerfilUsuario(ByVal pstridUsuario As String) As String
        Return Me.AccDO.GetBuscarPerfilUsuario(pstridUsuario)
    End Function
End Class

Imports BIFConvenios.DO
Imports System
Public Class ClienteBL
    ' Fields
    Private CodDO As ClienteDO = New ClienteDO

    ' Methods
    Public Function ExisteCodigoIBS(ByVal codibs As Integer) As Boolean
        Return Me.CodDO.ExisteCodigoIBS(codibs)
    End Function
End Class

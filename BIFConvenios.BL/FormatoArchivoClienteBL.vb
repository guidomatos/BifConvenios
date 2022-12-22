Imports BIFConvenios.DO
Imports System
Imports System.Diagnostics

Public Class FormatoArchivoClienteBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Private Function GetNombreFormatoArchivo(ByVal pCodigo_proceso As String) As String
        Return New FormatoArchivoClienteDO().ObtieneNombreFormatoArchivo(pCodigo_proceso)
    End Function
End Class

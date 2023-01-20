Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class ClsFuncionarioBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function GetFuncionarios() As DataTable
        Dim funcionarios As DataTable
        Try
            funcionarios = Singleton(Of ClsFuncionarioDO).Create.GetFuncionarios
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return funcionarios
    End Function
End Class

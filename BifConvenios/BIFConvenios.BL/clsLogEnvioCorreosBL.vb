Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class clsLogEnvioCorreosBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal objLogEnvioCorreos As clsLogEnvioCorreo) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsLogEnvioCorreosDO).Create.Insert(objLogEnvioCorreos)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return num
    End Function

    Public Function Seleccionar(ByVal objLogEnvioCorreo As clsLogEnvioCorreo) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsLogEnvioCorreosDO).Create.Seleccionar(objLogEnvioCorreo)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

End Class

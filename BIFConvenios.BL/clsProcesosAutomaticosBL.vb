Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class clsProcesosAutomaticosBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsProcesosAutomaticosDO).Create.Insert(pobjProcesosAutomaticos)
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

    Public Function Seleccionar(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsProcesosAutomaticosDO).Create.Seleccionar(pobjProcesosAutomaticos)
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

    Public Function Update(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsProcesosAutomaticosDO).Create.Update(pobjProcesosAutomaticos)
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
End Class

Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class clsAlertasClientesBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ChangeStatus(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsAlertasClientesDO).Create.ChangeStatus(pobjAlertasClientes)
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

    Public Function Insert(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsAlertasClientesDO).Create.Insert(pobjAlertasClientes)
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

    Public Function ObtenerAlertasClientesEnviar(ByVal pintCodigoCliente As Integer, ByVal pdecSaldoContable As Decimal, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsAlertasClientesDO).Create.ObtenerAlertasClientesEnviar(pintCodigoCliente, pdecSaldoContable, pintAnioPeriodo, pintMesPeriodo)
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

    Public Function ObtenerCuotasVencidasAlertasEnviar(ByVal pintCodigoIBS As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsAlertasClientesDO).Create.ObtenerCuotasVencidasAlertasEnviar(pintCodigoIBS)
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

    Public Function ObtieneAlertasClientesPorCriterio(ByVal pobjAlertasClientes As clsAlertasClientes) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsAlertasClientesDO).Create.ObtieneAlertasClientesPorCriterio(pobjAlertasClientes)
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

    Public Function Update(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsAlertasClientesDO).Create.Update(pobjAlertasClientes)
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

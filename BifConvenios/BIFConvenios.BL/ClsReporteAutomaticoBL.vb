Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class ClsReporteAutomaticoBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ReporteNominaAutomaticaCabecera(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReporteNominaAutomaticaCabecera(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReporteNominaAutomaticaCabeceraObservada(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReporteNominaAutomaticaCabeceraObservada(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReporteNominaAutomaticaDetalle(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReporteNominaAutomaticaDetalle(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReporteNominaAutomaticaDetalleObservada(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReporteNominaAutomaticaDetalleObservada(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReportePagoAutomaticoCabecera1(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReportePagoAutomaticoCabecera1(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReportePagoAutomaticoCabecera2(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReportePagoAutomaticoCabecera2(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ReportePagoAutomaticoDetalle(ByVal idFuncionario As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ReportePagoAutomaticoDetalle(idFuncionario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ValidaExistenciaFuncionario(ByVal idFuncionario As Integer, ByVal intTipoEnvioCorreo As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of ClsReporteAutomaticoDO).Create.ValidaExistenciaFuncionario(idFuncionario, intTipoEnvioCorreo)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function
End Class

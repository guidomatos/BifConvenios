Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsLogEnvioCorreosDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal objLogEnvioCorreos As clsLogEnvioCorreo) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[LogEnvioCorreoInsert]")
            dasql.AddParameter(command, "@iProcesoAutomaticoId", objLogEnvioCorreos.iProcesoAutomaticoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iTipoEnvioCorreoId", objLogEnvioCorreos.iTipoEnvioCorreoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodigoCliente", objLogEnvioCorreos.iCodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodigoIBS", objLogEnvioCorreos.iCodigoIBS, SqlDbType.Int)
            dasql.AddParameter(command, "@vCodigoProceso", objLogEnvioCorreos.vCodigoProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iAnioPeriodo", objLogEnvioCorreos.iAnioPeriodo, SqlDbType.Int)
            dasql.AddParameter(command, "@iMesPeriodo", objLogEnvioCorreos.iMesPeriodo, SqlDbType.Int)
            dasql.AddParameter(command, "@vMensajeProceso", objLogEnvioCorreos.vMensajeProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", objLogEnvioCorreos.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", objLogEnvioCorreos.vUsuarioCreacion, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            num = 1
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return num
    End Function

    Public Function Seleccionar(ByVal objLogEnvioCorreo As clsLogEnvioCorreo) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[LogEnvioCorreoSelect]")
            dasql.AddParameter(command, "@iProcesoAutomaticoId", objLogEnvioCorreo.iProcesoAutomaticoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEnvioCorreoId", objLogEnvioCorreo.iEnvioCorreoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodigoCliente", objLogEnvioCorreo.iCodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodigoIBS", objLogEnvioCorreo.iCodigoIBS, SqlDbType.Int)
            dasql.AddParameter(command, "@iTipoEnvioCorreoId", objLogEnvioCorreo.iTipoEnvioCorreoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", objLogEnvioCorreo.iEstado, SqlDbType.Int)
            Dim table2 As New DataTable
            table2 = dasql.ExecuteReader(command)
            If (table2.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            dasql.ConnectionClose()
            dasql = Nothing
            table = table2
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return table
    End Function
End Class

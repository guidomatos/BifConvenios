Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsProcesosAutomaticosDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ProcesosAutomaticosInsert]")
            dasql.AddParameter(command, "@iProcesoAutomaticoId", pobjProcesosAutomaticos.iProcesoAutomaticoId, SqlDbType.Int, ParameterDirection.Output)
            dasql.AddParameter(command, "@iTotalRegistros", pobjProcesosAutomaticos.iTotalRegistros, SqlDbType.Int)
            dasql.AddParameter(command, "@iProcesados", pobjProcesosAutomaticos.iProcesados, SqlDbType.Int)
            dasql.AddParameter(command, "@iErroneos", pobjProcesosAutomaticos.iErroneos, SqlDbType.Int)
            dasql.AddParameter(command, "@vMensajeProceso", pobjProcesosAutomaticos.vMensajeProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", pobjProcesosAutomaticos.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjProcesosAutomaticos.vUsuarioCreacion, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            'dasql = Nothing
            num = Integer.Parse(dasql.OutputParameter(command, "@iProcesoAutomaticoId"))
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
        Finally
            dasql.ConnectionClose()
            dasql = Nothing
        End Try
        Return num
    End Function

    Public Function Seleccionar(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ProcesosAutomaticosSelect]")
            dasql.AddParameter(command, "@iProcesoAutomaticoId", pobjProcesosAutomaticos.iProcesoAutomaticoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjProcesosAutomaticos.iEstado, SqlDbType.Int)
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

    Public Function Update(ByVal pobjProcesosAutomaticos As clsProcesosAutomaticos) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ProcesosAutomaticosUpdate]")
            dasql.AddParameter(command, "@iProcesoAutomaticoId", pobjProcesosAutomaticos.iProcesoAutomaticoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iTotalRegistros", pobjProcesosAutomaticos.iTotalRegistros, SqlDbType.Int)
            dasql.AddParameter(command, "@iProcesados", pobjProcesosAutomaticos.iProcesados, SqlDbType.Int)
            dasql.AddParameter(command, "@iErroneos", pobjProcesosAutomaticos.iErroneos, SqlDbType.Int)
            dasql.AddParameter(command, "@vMensajeProceso", pobjProcesosAutomaticos.vMensajeProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", pobjProcesosAutomaticos.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjProcesosAutomaticos.vUsuarioModificacion, SqlDbType.VarChar)
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
End Class

Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsSystemParametersDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal objSystemParameters As clsSystemParameters) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[SystemParametersInsert]")
            dasql.AddParameter(command, "@iGrupoId", objSystemParameters.iGrupoId, SqlDbType.Int)
            dasql.AddParameter(command, "@iParametroId", objSystemParameters.iParametroId, SqlDbType.Int)
            dasql.AddParameter(command, "@vDescripcion", objSystemParameters.vDescripcion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vValor", objSystemParameters.vValor, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iOrden", objSystemParameters.iOrden, SqlDbType.Int)
            dasql.AddParameter(command, "@iVisible", objSystemParameters.iVisible, SqlDbType.Int)
            dasql.AddParameter(command, "@dFechaInicio", objSystemParameters.dFechaInicio, SqlDbType.DateTime)
            dasql.AddParameter(command, "@dFechaFin", objSystemParameters.dFechaFin, SqlDbType.DateTime)
            dasql.AddParameter(command, "@iEstado", objSystemParameters.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", objSystemParameters.vUsuarioCreacion, SqlDbType.VarChar)
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

    Public Function Seleccionar(ByVal pintGrupoId As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[SystemParametersSelect]")
            dasql.AddParameter(command, "@iGrupoId", pintGrupoId, SqlDbType.Int)
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

    Public Function SeleccionarGrupos() As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[SystemParametersSelectGrupo]")
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

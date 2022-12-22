Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsArchivosConveniosDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal objArchivosConvenio As clsArchivosConvenios) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ArchivoConveniosInsert]")
            dasql.AddParameter(command, "@iArchivoConvenioId", objArchivosConvenio.iArchivoConvenioId, SqlDbType.Int, ParameterDirection.Output)
            dasql.AddParameter(command, "@vCodProceso", objArchivosConvenio.vCodProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNombreArchivo", objArchivosConvenio.vNombreArchivo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vRutaCreacion", objArchivosConvenio.vRutaCreacion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", objArchivosConvenio.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuarioCreacion", objArchivosConvenio.vUsuarioCreacion, SqlDbType.VarChar)
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

    Public Function Seleccionar(ByVal objArchivosConvenio As clsArchivosConvenios, ByVal iStartRowIndex As Integer, ByVal iMaxRows As Integer, ByRef iTotalRows As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ArchivoConveniosSelect]")
            dasql.AddParameter(command, "@iArchivoConvenioId", objArchivosConvenio.iArchivoConvenioId, SqlDbType.Int)
            dasql.AddParameter(command, "@vCodProceso", objArchivosConvenio.vCodProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNombreArchivo", objArchivosConvenio.vNombreArchivo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vRuta", objArchivosConvenio.vRutaCreacion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTipoRuta", 1, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", objArchivosConvenio.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuarioCreacion", objArchivosConvenio.vUsuarioCreacion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iStartRowIndex", iStartRowIndex, SqlDbType.Int)
            dasql.AddParameter(command, "@iMaxRows", iMaxRows, SqlDbType.Int)
            dasql.AddParameter(command, "@iTotalRows", CInt(iTotalRows), SqlDbType.Int, ParameterDirection.Output)
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

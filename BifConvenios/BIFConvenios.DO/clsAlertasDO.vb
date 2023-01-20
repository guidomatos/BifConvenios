Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAlertasDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub

    ' Methods
    Public Function ChangeStatus(ByVal pobjAlertas As clsAlertas) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertasChangeStatus]")
            dasql.AddParameter(command, "@iAlertaId", pobjAlertas.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstadoAlerta", pobjAlertas.iEstadoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertas.vUsuarioCreacion, SqlDbType.VarChar)
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

    Public Function Insert(ByVal pobjAlertas As clsAlertas) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertasInsert]")
            dasql.AddParameter(command, "@iTipoAlerta", pobjAlertas.iTipoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreAlerta", pobjAlertas.vNombreAlerta, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vDescripcionAlerta", pobjAlertas.vDescripcionAlerta, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vAsuntoMensaje", pobjAlertas.vAsuntoMensaje, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCuerpoMensaje", pobjAlertas.vCuerpoMensaje, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstadoAlerta", pobjAlertas.iEstadoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertas.vUsuarioCreacion, SqlDbType.VarChar)
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

    Public Function ObtieneAlertasPorCriterio(ByVal pobjAlertas As clsAlertas) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertasSelect]")
            dasql.AddParameter(command, "@iAlertaId", pobjAlertas.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iTipoAlerta", pobjAlertas.iTipoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreAlerta", pobjAlertas.vNombreAlerta, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstadoAlerta", pobjAlertas.iEstadoAlerta, SqlDbType.Int)
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

    Public Function Update(ByVal pobjAlertas As clsAlertas) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertasUpdate]")
            dasql.AddParameter(command, "@iAlertaId", pobjAlertas.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iTipoAlerta", pobjAlertas.iTipoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreAlerta", pobjAlertas.vNombreAlerta, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vDescripcionAlerta", pobjAlertas.vDescripcionAlerta, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vAsuntoMensaje", pobjAlertas.vAsuntoMensaje, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCuerpoMensaje", pobjAlertas.vCuerpoMensaje, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstadoAlerta", pobjAlertas.iEstadoAlerta, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertas.vUsuarioCreacion, SqlDbType.VarChar)
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

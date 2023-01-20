Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAlertasClientesDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ChangeStatus(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertasClientesChangeStatus]")
            dasql.AddParameter(command, "@iAlertaClienteId", pobjAlertasClientes.iAlertaClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjAlertasClientes.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertasClientes.vUsuarioModificacion, SqlDbType.VarChar)
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

    Public Function Insert(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertaClienteInsert]")
            dasql.AddParameter(command, "@iAlertaId", pobjAlertasClientes.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pobjAlertasClientes.iClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iDiasAntes", pobjAlertasClientes.iDiasAntes, SqlDbType.Int)
            dasql.AddParameter(command, "@iDiasDespues", pobjAlertasClientes.iDiasDespues, SqlDbType.Int)
            dasql.AddParameter(command, "@iAdjunto", pobjAlertasClientes.iAdjunto, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjAlertasClientes.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertasClientes.vUsuarioCreacion, SqlDbType.VarChar)
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

    Public Function ObtenerAlertasClientesEnviar(ByVal pintCodigoCliente As Integer, ByVal pdecSaldoContable As Decimal, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[spObtenerAlertasEnviar]")
            dasql.AddParameter(command, "@CODCLIENTE", pintCodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@SALDO", pdecSaldoContable, SqlDbType.Decimal)
            dasql.AddParameter(command, "@ANIO", pintAnioPeriodo, SqlDbType.Int)
            dasql.AddParameter(command, "@Mes", pintMesPeriodo, SqlDbType.Int)
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

    Public Function ObtenerCuotasVencidasAlertasEnviar(ByVal pintCodigoIBS As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[spObtenerCuotasVencidasAlertasEnviar]")
            dasql.AddParameter(command, "@CODIGO_IBS", pintCodigoIBS, SqlDbType.Int)
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

    Public Function ObtieneAlertasClientesPorCriterio(ByVal pobjAlertasClientes As clsAlertasClientes) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertaClienteSelect]")
            dasql.AddParameter(command, "@iAlertaClienteId", pobjAlertasClientes.iAlertaClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iAlertaId", pobjAlertasClientes.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pobjAlertasClientes.iClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjAlertasClientes.iEstado, SqlDbType.Int)
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

    Public Function Update(ByVal pobjAlertasClientes As clsAlertasClientes) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AlertaClienteUpdate]")
            dasql.AddParameter(command, "@iAlertaClienteId", pobjAlertasClientes.iAlertaClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iAlertaId", pobjAlertasClientes.iAlertaId, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pobjAlertasClientes.iClienteId, SqlDbType.Int)
            dasql.AddParameter(command, "@iDiasAntes", pobjAlertasClientes.iDiasAntes, SqlDbType.Int)
            dasql.AddParameter(command, "@iDiasDespues", pobjAlertasClientes.iDiasDespues, SqlDbType.Int)
            dasql.AddParameter(command, "@iAdjunto", pobjAlertasClientes.iAdjunto, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjAlertasClientes.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjAlertasClientes.vUsuarioModificacion, SqlDbType.VarChar)
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

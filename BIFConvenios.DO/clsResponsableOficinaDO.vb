Imports ADODB
Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsResponsableOficinaDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ChangeStatus(ByVal pobjResponsableOficina As clsReponsableOficina) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ResponsableOficinaChangeStatus]")
            dasql.AddParameter(command, "@iResponsableId", pobjResponsableOficina.iResponsableId, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjResponsableOficina.iEstado, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjResponsableOficina.vUsuarioModificacion, SqlDbType.VarChar)
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

    Public Function Insert(ByVal pobjResponsableOficina As clsReponsableOficina) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ResponsableOficinaInsert]")
            dasql.AddParameter(command, "@iOficinaId", pobjResponsableOficina.iOficinaId, SqlDbType.Int)
            dasql.AddParameter(command, "@vOficina", pobjResponsableOficina.vOficina, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNombreResponsable", pobjResponsableOficina.vNombreResponsable, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCorreoResponsable", pobjResponsableOficina.vCorreoResponsable, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vUsuario", pobjResponsableOficina.vUsuarioCreacion, SqlDbType.VarChar)
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
            Throw ex
        End Try
        Return num
    End Function

    Public Function ObtenerListaOficinasDesdeAS400() As DataTable
        Dim table2 As DataTable
        Dim connectionString As String = New DASQL().ConnectionAS400
        Dim connection As Connection = New ConnectionClass
        Dim adapter As New OleDbDataAdapter
        Dim dataSet As New DataSet
        Dim table As New DataTable
        Dim aDODBRecordSet As Recordset = New RecordsetClass
        Try
            connection.CursorLocation = CursorLocationEnum.adUseClient
            connection.Open(connectionString, "", "", -1)
            aDODBRecordSet = connection.Execute(("SELECT OFI.BRNBNK, OFI.BRNNUM, OFI.BRNNME, OFI.BRNADR" & " FROM CNTRLBRN OFI"), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            connection.Close()
            connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtOficinaIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            connection.Close()
            connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            connection.Close()
            connection = Nothing
            Throw ex
        End Try
        Return table2
    End Function

    Public Function ObtenerOficinaDesdeAS400PorCriterio(ByVal pintTipo As Integer, ByVal pstrValor As String) As DataTable
        Dim table2 As DataTable
        Dim connectionString As String = New DASQL().ConnectionAS400
        Dim connection As Connection = New ConnectionClass
        Dim adapter As New OleDbDataAdapter
        Dim dataSet As New DataSet
        Dim table As New DataTable
        Dim aDODBRecordSet As Recordset = New RecordsetClass
        Dim str3 As String = ""
        Try
            connection.CursorLocation = CursorLocationEnum.adUseClient
            connection.Open(connectionString, "", "", -1)
            str3 = ("SELECT OFI.BRNBNK, OFI.BRNNUM, OFI.BRNNME, OFI.BRNADR" & " FROM CNTRLBRN OFI")
            If (pintTipo = 1) Then
                str3 = (str3 & " WHERE OFI.BRNNUM = " & pstrValor)
            ElseIf (pintTipo = 2) Then
                str3 = (str3 & " WHERE OFI.BRNNME LIKE '" & Strings.UCase(pstrValor) & "%'")
            End If
            aDODBRecordSet = connection.Execute((str3 & " ORDER BY BRNNUM ASC "), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            connection.Close()
            connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtOficinaIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            connection.Close()
            connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            connection.Close()
            connection = Nothing
            Throw ex
        End Try
        Return table2
    End Function

    Public Function ObtenerResponsableOficinaPorCriterio(ByVal pobjResponsableOficina As clsReponsableOficina) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ResponsableOficinaSelect]")
            dasql.AddParameter(command, "@iResponsableId", pobjResponsableOficina.iResponsableId, SqlDbType.Int)
            dasql.AddParameter(command, "@vOficina", pobjResponsableOficina.vOficina, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNombreResponsable", pobjResponsableOficina.vNombreResponsable, SqlDbType.VarChar)
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

    Public Function Update(ByVal pobjResponsableOficina As clsReponsableOficina) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ResponsableOficinaUpdate]")
            dasql.AddParameter(command, "@iResponsableId", pobjResponsableOficina.iResponsableId, SqlDbType.Int)
            dasql.AddParameter(command, "@iOficinaId", pobjResponsableOficina.iOficinaId, SqlDbType.Int)
            dasql.AddParameter(command, "@vOficina", pobjResponsableOficina.vOficina, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNombreResponsable", pobjResponsableOficina.vNombreResponsable, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCorreoResponsable", pobjResponsableOficina.vCorreoResponsable, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vUsuario", pobjResponsableOficina.vUsuarioModificacion, SqlDbType.VarChar)
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

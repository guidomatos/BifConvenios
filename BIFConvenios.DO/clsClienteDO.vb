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

Public Class clsClienteDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    Public Function ActualizarCliente(ByVal pobjCliente As clsCliente) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[ActualizarCliente]")
            dasql.AddParameter(command, "@iCodCliente", pobjCliente.CodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreCliente", pobjCliente.NombreCliente, SqlDbType.VarChar)
            dasql.AddParameter(command, "@TipoArchivoEnviar", pobjCliente.TipoArchivoEnviar, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTipoDocumento", pobjCliente.TipoDocumento, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNumeroDocumento", pobjCliente.NumeroDocumento, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCorreoElectronicos", pobjCliente.CorreoElectronico, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono1", pobjCliente.Telefono1, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono2", pobjCliente.Telefono2, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono3", pobjCliente.Telefono3, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono4", pobjCliente.Telefono4, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vDiaEnvioPlanilla", pobjCliente.DiaEnvioPlanilla, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vDiaCierrePlanilla", pobjCliente.DiaCierrePlanilla, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vMesesEnvioListado", pobjCliente.MesesAnticipacionEnvioListado, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vDiaCorte", pobjCliente.DiaCorte, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iCodFuncionario", pobjCliente.IdFuncionario, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodIBS", pobjCliente.CodigoIBS, SqlDbType.Int)
            dasql.AddParameter(command, "@vCodigoInstitucion", pobjCliente.CodigoInstitucion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vEnvioAutomaticoListado", pobjCliente.IndEnvioAutomaticoListado, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iBloquearCredito", pobjCliente.BloquearCredito, SqlDbType.Int)
            dasql.AddParameter(command, "@iCodigoOficina", pobjCliente.CodigoOficina, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreOficina", pobjCliente.NombreOficina, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iCodigoGestor", pobjCliente.CodigoGestor, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreGestor", pobjCliente.NombreGestor, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vUsuario", pobjCliente.UsuarioCreacion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCodigoInstitucionCAS", pobjCliente.CodigoInstitucionCAS, SqlDbType.VarChar)

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

    Public Function EliminarCliente(ByVal pintCodigoCliente As Integer, ByVal pstrUsuario As String) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[DeleteCliente]")
            dasql.AddParameter(command, "@Codigo_Cliente", pintCodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@Usuario", pstrUsuario, SqlDbType.VarChar)
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

    Public Function ExisteClienteBifConvenio(ByVal pstrTipoDocumento As Object, ByVal pstrNumeroDocumento As Object) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetClientePorDocumento]")
            dasql.AddParameter(command, "@vTipoDocumento", pstrTipoDocumento, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNumeroDocumento", pstrNumeroDocumento, SqlDbType.VarChar)
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

    Public Function ExisteClienteDesdeAS400PorCodIBS(ByVal pintCodIBS As Integer) As Integer
        Dim intContar As Integer
        Dim connectionString As String = New DASQL().ConnectionAS400
        Dim connection As Connection = New ConnectionClass
        Dim adapter As New OleDbDataAdapter
        Dim dataSet As New DataSet
        Dim table As New DataTable
        Dim aDODBRecordSet As Recordset = New RecordsetClass
        Try
            connection.CursorLocation = CursorLocationEnum.adUseClient
            connection.Open(connectionString, "", "", -1)
            'aDODBRecordSet = connection.Execute((((("SELECT DISTINCT CUMST.CUSCUN AS CUSCUN," & ChrW(9) & "CUMST.CUSTID AS TIPODOCUMENTO, CUMST.CUSIDN AS NUMERODOCUMENTO," & " CUMST.CUSNA1 AS NOMEMPRESA, CNV.OFICINA AS CODOFICINA, OFI.BRNNME AS NOMOFICINA, CNV.GESTOR AS CODGESTOR,") & " CASE WHEN TRIM(GEST.GSTNOM)  = '' AND TRIM(GEST.GSTPAT) = '' THEN '' ELSE TRIM(GEST.GSTNOM) CONCAT ', ' CONCAT TRIM(GEST.GSTPAT) END AS NOMGESTOR," & " CNV.FUNCIO AS CODFUNCIONARIO, FUNC.FUNNOM AS NOMFUNCIONARIO, FUNC.FUNCOR AS CORREOFUNCIONARIO ") & " FROM CUMST CUMST LEFT OUTER JOIN DLCNV CNV ON CUMST.CUSCUN = CNV.CNVCUN LEFT OUTER JOIN CNTRLBRN AS OFI ON CNV.OFICINA = OFI.BRNNUM" & " LEFT OUTER JOIN DLGSCONV AS GEST ON CNV.GESTOR = GEST.GSTCOD LEFT OUTER JOIN DLFNCONV AS FUNC ON CNV.FUNCIO = FUNC.FUNCOD") & " WHERE CUMST.CUSCUN=" & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet = connection.Execute((((("SELECT DISTINCT CUMST.CUSCUN AS CUSCUN," & ChrW(9) & "CUMST.CUSTID AS TIPODOCUMENTO, CUMST.CUSIDN AS NUMERODOCUMENTO, CUMST.CUSNA1 AS NOMEMPRESA, CNV.OFICINA AS CODOFICINA, OFI.BRNNME AS NOMOFICINA, CNV.GESTOR AS CODGESTOR,") & " CASE WHEN TRIM(GEST.GSTNOM)  = '' AND TRIM(GEST.GSTPAT) = '' THEN '' ELSE TRIM(GEST.GSTNOM) CONCAT ', ' CONCAT TRIM(GEST.GSTPAT) END AS NOMGESTOR," & " CNV.FUNCIO AS CODFUNCIONARIO, FUNC.FUNNOM AS NOMFUNCIONARIO, FUNC.FUNCOR AS CORREOFUNCIONARIO ") & " FROM CUMST CUMST LEFT OUTER JOIN DLCNV CNV ON CUMST.CUSCUN = CNV.CNVCUN LEFT OUTER JOIN CNTRLBRN AS OFI ON CNV.OFICINA = OFI.BRNNUM" & " LEFT OUTER JOIN DLGSCONV AS GEST ON CNV.GESTOR = GEST.GSTCOD LEFT OUTER JOIN DLFNCONV AS FUNC ON CNV.FUNCIO = FUNC.FUNCOD") & " WHERE CUMST.CUSCUN=" & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            intContar = aDODBRecordSet.RecordCount
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return intContar
    End Function

    Public Function ObtenerClienteDesdeAS400PorCodIBS(ByVal pintCodIBS As Integer) As DataTable
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
            'aDODBRecordSet = connection.Execute((((("SELECT DISTINCT CUMST.CUSCUN AS CUSCUN," & ChrW(9) & "CUMST.CUSTID AS TIPODOCUMENTO, CUMST.CUSIDN AS NUMERODOCUMENTO," & " CUMST.CUSNA1 AS NOMEMPRESA, CNV.OFICINA AS CODOFICINA, OFI.BRNNME AS NOMOFICINA, CNV.GESTOR AS CODGESTOR,") & " CASE WHEN TRIM(GEST.GSTNOM)  = '' AND TRIM(GEST.GSTPAT) = '' THEN '' ELSE TRIM(GEST.GSTNOM) CONCAT ', ' CONCAT TRIM(GEST.GSTPAT) END AS NOMGESTOR," & " CNV.FUNCIO AS CODFUNCIONARIO, FUNC.FUNNOM AS NOMFUNCIONARIO, FUNC.FUNCOR AS CORREOFUNCIONARIO ") & " FROM CUMST CUMST LEFT OUTER JOIN DLCNV CNV ON CUMST.CUSCUN = CNV.CNVCUN LEFT OUTER JOIN CNTRLBRN AS OFI ON CNV.OFICINA = OFI.BRNNUM" & " LEFT OUTER JOIN DLGSCONV AS GEST ON CNV.GESTOR = GEST.GSTCOD LEFT OUTER JOIN DLFNCONV AS FUNC ON CNV.FUNCIO = FUNC.FUNCOD") & " WHERE CUMST.CUSCUN=" & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet = connection.Execute((((("SELECT DISTINCT CUMST.CUSCUN AS CUSCUN," & ChrW(9) & "CUMST.CUSTID AS TIPODOCUMENTO, CUMST.CUSIDN AS NUMERODOCUMENTO, CUMST.CUSNA1 AS NOMEMPRESA, CNV.OFICINA AS CODOFICINA, OFI.BRNNME AS NOMOFICINA, CNV.GESTOR AS CODGESTOR,") & " CASE WHEN TRIM(GEST.GSTNOM)  = '' AND TRIM(GEST.GSTPAT) = '' THEN '' ELSE TRIM(GEST.GSTNOM) CONCAT ', ' CONCAT TRIM(GEST.GSTPAT) END AS NOMGESTOR," & " CNV.FUNCIO AS CODFUNCIONARIO, FUNC.FUNNOM AS NOMFUNCIONARIO, FUNC.FUNCOR AS CORREOFUNCIONARIO ") & " FROM CUMST CUMST LEFT OUTER JOIN DLCNV CNV ON CUMST.CUSCUN = CNV.CNVCUN LEFT OUTER JOIN CNTRLBRN AS OFI ON CNV.OFICINA = OFI.BRNNUM" & " LEFT OUTER JOIN DLGSCONV AS GEST ON CNV.GESTOR = GEST.GSTCOD LEFT OUTER JOIN DLFNCONV AS FUNC ON CNV.FUNCIO = FUNC.FUNCOD") & " WHERE CUMST.CUSCUN=" & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtClienteIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerClienteDesdeAS400PorDocumento(ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String) As DataTable
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
            aDODBRecordSet = connection.Execute(String.Concat(New String() {"SELECT * FROM CUMST WHERE CUSTID  ='", pstrTipoDocumento.Trim, "' AND CUSIDN = '", pstrNumeroDocumento.Trim, "'"}), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtClienteIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerClientePorCodigo(ByVal pintCodigoCliente As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetCliente]")
            dasql.AddParameter(command, "@Codigo_cliente", pintCodigoCliente, SqlDbType.Int)
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
            Dim exception As HandledException = ex
            dasql.ConnectionClose()
            Throw exception
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerEmailsEnviosClientes(ByVal pintCodigoCliente As String) As String
        Dim str As String
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[spObtenerEMailsEnviosClientes]")
            dasql.AddParameter(command, "@iCodigoCliente", pintCodigoCliente, SqlDbType.VarChar)
            Dim table As New DataTable
            str = dasql.ExecuteReader(command).Rows(0)("CORREOS").ToString

            dasql.ConnectionClose()
            dasql = Nothing
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
        Return str
    End Function

    Public Function ObtenerFuncionarioConvenioPorCodigoIBSDesdeAS400(ByVal pintCodIBS As Integer) As DataTable
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
            aDODBRecordSet = connection.Execute((("SELECT FUNCIO.FUNCOD, FUNCIO.FUNNOM, FUNCIO.FUNCOR, FUNCIO.FUNTEL, FUNCIO.FUNEST, FUNCIO.FUNUCR " & " FROM DLFNCONV FUNCIO INNER JOIN DLCNV CNV ON FUNCIO.FUNCOD = CNV.FUNCIO ") & " WHERE CNV.CNVCUN = " & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtFuncionarioIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerGestorConvenioPorCodigoIBSDesdeAS400(ByVal pintCodIBS As Integer) As DataTable
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
            aDODBRecordSet = connection.Execute((("SELECT GESTOR.GSTCOD, GESTOR.GSTNOM, GESTOR.GSTPAT, " & " GESTOR.GSTMAT, GESTOR.GSTCOR, GESTOR.GSTRED " & " FROM DLGSCONV GESTOR INNER JOIN DLCNV CNV ON GESTOR.GSTCOD = CNV.GESTOR ") & " WHERE CNV.CNVCUN = " & Conversions.ToString(pintCodIBS)), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtGestorIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerListaClienteDesdeAS400(ByVal pstrCliente As String, ByVal pstrCodIBS As String, ByVal pstrNumDocumento As String, ByVal pstrCantidadRegistros As String) As DataTable
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
            str3 = (("SELECT CUM.CUSCUN, CUM.CUSNA1, CUM.CUSIDN, CUM.CUSTID" & " FROM CUMST CUM " & " WHERE CUM.CUSLGT = '1'") & " AND CUM.CUSNA1 LIKE '" & Strings.UCase(pstrCliente) & "%' ")
            If (Strings.Len(pstrCodIBS) > 0) Then
                str3 = (str3 & " AND CUM.CUSCUN =" & pstrCodIBS)
            End If
            If (Strings.Len(pstrNumDocumento) > 0) Then
                str3 = (str3 & " AND CUM.CUSIDN ='" & pstrNumDocumento & "'")
            End If
            aDODBRecordSet = connection.Execute((str3 & " ORDER BY CUM.CUSNA1 ASC FETCH FIRST " & pstrCantidadRegistros & " ROW ONLY"), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtClientesIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If

        End Try
        Return table2
    End Function

    Public Function ObtenerListaClientePorCriterio(ByVal objCliente As clsCliente, ByVal intStartRowIndex As Integer, ByVal intMaxRows As Integer, ByRef intTotalRows As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[spObtenerListaClientes]")
            dasql.AddParameter(command, "@CodigoCliente", objCliente.CodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@NombreCliente", objCliente.NombreCliente, SqlDbType.VarChar)
            dasql.AddParameter(command, "@TipoDocumento", objCliente.TipoDocumento, SqlDbType.VarChar)
            dasql.AddParameter(command, "@NumeroDocumento", objCliente.NumeroDocumento, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iStartRowIndex", intStartRowIndex, SqlDbType.Int)
            dasql.AddParameter(command, "@iMaxRows", intMaxRows, SqlDbType.Int)
            dasql.AddParameter(command, "@iTotalRows", CInt(intTotalRows), SqlDbType.Int, ParameterDirection.Output)
            Dim table2 As New DataTable
            table2 = dasql.ExecuteReader(command)
            intTotalRows = Convert.ToInt32(dasql.OutputParameter(command, "@iTotalRows"))
            If (table2.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecords)
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

    Public Function ObtenerListaClientesByDiaEnvio(ByVal pintDiaEnvio As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetClientesByDiaEnvio]")
            dasql.AddParameter(command, "@intDiaEnvio", pintDiaEnvio, SqlDbType.Int)
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

    Public Function ObtenerListaDocumentosClientesRegistrados() As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetDocumentoClientesRegistrados]")
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

    Public Function ObtenerListaFuncionarios() As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetFuncionarioConvenios]")
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

    Public Function ObtenerListaFuncionariosConveniosDesdeAS400() As DataTable
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
            aDODBRecordSet = connection.Execute(("SELECT FUNCIO.FUNCOD, FUNCIO.FUNNOM, FUNCIO.FUNCOR, FUNCIO.FUNTEL, FUNCIO.FUNEST, FUNCIO.FUNUCR " & " FROM DLFNCONV FUNCIO "), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtListaFuncionarioIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerListaGestoresConvenioDesdeAS400() As DataTable
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
            aDODBRecordSet = connection.Execute(("SELECT GESTOR.GSTCOD, GESTOR.GSTNOM, GESTOR.GSTPAT, " & " GESTOR.GSTMAT, GESTOR.GSTCOR, GESTOR.GSTRED " & " FROM DLGSCONV GESTOR "), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtListaGestorIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function

    Public Function ObtenerSaldoContablePorCodigoIBS(ByVal pstrCodIBS As String) As DataTable
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
            aDODBRecordSet = connection.Execute((((("SELECT DISTINCT ST.ACMACC, ST.ACMMGB, CRE.DLCCC, " & " CAST(2000 + C.DLVCA  AS INTEGER) AS DLEAP, " & " CAST(C.DLVCM AS INTEGER) AS DLEMP ") & " FROM ACMST ST INNER JOIN DLCRE CRE ON ST.ACMACC = CRE.DLCTA " & " INNER JOIN DLCCR C ON CRE.DLACC = C.DLACC AND C.DLSTS = ''") & " WHERE CRE.DLCCC = " & pstrCodIBS) & " ORDER BY DLEAP DESC, DLEMP DESC"), Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            'connection.Close()
            'connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtClientesIBS")
            table = dataSet.Tables(0)
            If (table.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            table2 = table
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            'connection.Close()
            'connection = Nothing
            Throw ex
        Finally
            If (Not connection Is Nothing) And connection.State = ConnectionState.Open Then
                connection.Close()
                connection = Nothing
            End If
        End Try
        Return table2
    End Function
End Class

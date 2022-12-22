Imports ADODB
Imports DAL
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Public Class clsCuotaDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function FinalizaImportacionPagares(ByVal pstrCodigoProceso As String, ByVal pstrUsuario As String) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[EnviaEspacioTrabajo]")
            dasql.AddParameter(command, "@Codigo_proceso", pstrCodigoProceso, SqlDbType.VarChar)
            dasql.AddParameter(command, "@usuario", pstrUsuario, SqlDbType.VarChar)
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

    Public Function InsertaDLENV(ByVal _drw As DataRow) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AdicionaDLENV]")
            dasql.AddParameter(command, "@Codigo_Proceso", _drw("Codigo_Proceso").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEAG", _drw("DLEAG").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEAN", _drw("DLEAN").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEAP", _drw("DLEAP").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLECC", _drw("DLECC").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLECM", _drw("DLECM").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLECO", _drw("DLECO").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLECR", _drw("DLECR").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEFP", _drw("DLEFP").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEIC", _drw("DLEIC").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEMA", _drw("DLEMA").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEMN", _drw("DLEMN").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEMO", _drw("DLEMO").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEMP", _drw("DLEMP").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLENE", _drw("DLENE").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLENP", _drw("DLENP").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEPA", _drw("DLEPA").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLESD", _drw("DLESD").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLEST", _drw("DLEST").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLFLI", _drw("DLFLI").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@NUMCUOTAS", _drw("NUMCUOTAS").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@FECDESEMBOLSO", _drw("FECHADESEMBOLSO").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@MONTOORIGINAL", _drw("MONTOORIGINAL").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@CUOTASINFORMADAS", Convert.ToInt32(_drw("CuotasInformada").ToString), SqlDbType.Int)
            dasql.AddParameter(command, "@FECCARGO", _drw("FechaCargo").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@CUOTASPACTADAS", Convert.ToInt32(_drw("CuotasPactadas").ToString), SqlDbType.Int)
            dasql.AddParameter(command, "@CUOTASPAGADAS", Convert.ToInt32(_drw("CuotasPagadas").ToString), SqlDbType.Int)
            dasql.AddParameter(command, "@CUOTASPENDIENTES", Convert.ToInt32(_drw("CuotasPendientes").ToString), SqlDbType.Int)
            dasql.AddParameter(command, "@NROTIPOCREDITO", Convert.ToInt32(_drw("NroTipoCredito").ToString), SqlDbType.Int)
            'Add 2022-06-15 Agregar parametros de Tipo y Numero de Documento
            dasql.AddParameter(command, "@TIPODOCUMENTO", _drw("TipoDocumento").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@NRODOCUMENTO", _drw("NroDocumento").ToString, SqlDbType.VarChar)
            'End Add
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

    Public Function InsertarHistoricoDLCCR(ByVal _drw As DataRow) As Object
        Dim obj2 As Object
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[AddHistoricoDLCCR]")
            dasql.AddParameter(command, "@Codigo_Proceso", _drw("Codigo_Proceso").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLACC", _drw("DLACC").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLCCY", _drw("DLCCY").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLVCA", _drw("DLVCA").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLVCM", _drw("DLVCM").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLVCD", _drw("DLVCD").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLNCT", _drw("DLNCT").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLIMC", _drw("DLIMC").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLIPC", _drw("DLIPC").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLITF", _drw("DLITF").ToString, SqlDbType.VarChar)
            dasql.AddParameter(command, "@DLSTS", _drw("DLSTS").ToString, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            obj2 = 1
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
        Return obj2
    End Function

    Public Function ObtenerDeudaDeIBS(ByVal pstrCodigoCliente As String, ByVal pstrAnio As String, ByVal pstrMes As String) As DataTable
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
            Dim str As String = ((Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(((((((("SELECT '' AS Codigo_proceso, DLCCR.DLACC, DLCCR.DLCCY, DLCCR.DLVCA, DLCCR.DLVCM, DLCCR.DLVCD, " & ChrW(13) & ChrW(10) & "(DLCCR.DLNCT - (SELECT COUNT(*) FROM DLPMT WHERE DLPACC = DLCCR.DLACC AND DLPPNU = 999)) AS DLNCT, " & ChrW(13) & ChrW(10)) & "DLCCR.DLIMC, DLCCR.DLIPC, DLCCR.DLITF, DLCCR.DLSTS " & ChrW(13) & ChrW(10) & "FROM DLCCR ") & "WHERE " & " CAST ( 2000+DLVCA  AS CHARACTER ( 4 ) ) ") & " CONCAT " & " CASE WHEN LENGTH(TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN ") & " " & ChrW(9) & "'0' CONCAT TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) )) " & " ELSE ") & " " & ChrW(9) & "TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))" & " END") & " <='" & pstrAnio), Interaction.IIf((Strings.Len(Strings.Trim(pstrMes)) = 1), ("0" & Strings.Trim(pstrMes)), Strings.Trim(pstrMes))), "'")) & " AND DLACC IN (SELECT DLACC FROM   DLCRE WHERE DLCCC  = " & pstrCodigoCliente & " )" & ChrW(13) & ChrW(10)) & "AND DLSTS = ''")
            aDODBRecordSet = connection.Execute(str, Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            connection.Close()
            connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtDeudaIBS")
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

    Public Function ObtenerPagareDeIBS(ByVal pstrCodigoCliente As String, ByVal pstrAnio As String, ByVal pstrMes As String) As DataTable
        Dim table2 As DataTable
        Dim connectionString As String = New DASQL().ConnectionAS400
        Dim connection As Connection = New ConnectionClass
        Dim adapter As New OleDbDataAdapter
        Dim dataSet As New DataSet
        Dim table As New DataTable
        Dim aDODBRecordSet As Recordset = New RecordsetClass
        Dim str As String = ""
        Try
            Dim table3 As New DataTable
            table3 = Me.ObtenerTipoCuotaByCodigoIBS(pstrCodigoCliente)
            Dim num As Integer = Conversions.ToInteger(table3.Rows(0)("TIPO_CUOTA").ToString)
            connection.CursorLocation = CursorLocationEnum.adUseClient
            connection.Open(connectionString, "", "", -1)
            Dim strArray As String() = New String() {(((("SELECT '' AS Codigo_proceso, e.DLCCC AS DLECC, e.DLAÑO AS DLEAN, e.DLAGC AS DLEAG, e.DLCOC AS DLECO, e.DLCCY AS DLEMO, e.DLACC AS DLENP, e.DLCEM AS DLECM, " & ChrW(13) & ChrW(10) & "                      e.DLNCL AS DLENE, e.DLAPP AS DLEPA, e.DLAPM AS DLEMA, trim(DLPRN) CONCAT ' ' CONCAT trim(DLSGN) AS DLEMN, " & ChrW(13) & ChrW(10) & "                      CASE WHEN TRIM(CAST(DLCCR AS CHARACTER(8)) CONCAT DLPLA CONCAT DLCUS) = '0' THEN '' ELSE CAST(SUBSTRING(TRIM(DLCUS) " & ChrW(13) & ChrW(10)) & "                      CONCAT '0000', 1, 4)  CONCAT DLPLA CONCAT CAST(DLCCR AS CHARACTER(8))  AS CHARACTER(20)) END AS DLECR, 2000 + c.DLVCA AS DLEAP, " & ChrW(13) & ChrW(10) & "                      c.DLVCM AS DLEMP, SUM(r.DLIMC - r.DLIPC + r.DLITF) AS DLEIC, e.DLSTS AS DLEST, CAST(YEAR(CURRENT DATE) AS CHARACTER(4)) " & ChrW(13) & ChrW(10)) & "                      CONCAT CASE LENGTH(TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(MONTH(CURRENT DATE) " & ChrW(13) & ChrW(10) & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(MONTH(CURRENT DATE) AS CHARACTER(2)), 3, 2) " & ChrW(13) & ChrW(10)) & "                      END CONCAT CASE LENGTH(TRIM(CAST(DAY(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(DAY(CURRENT DATE) " & ChrW(13) & ChrW(10) & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(DAY(CURRENT DATE) AS CHARACTER(2)), 3, 2) END AS DLEFP, " & ChrW(13) & ChrW(10)), "                      CASE WHEN COUNT(1) > 1 THEN 1 ELSE 0 END DLESD, e.DLFLI, COUNT(1) AS NUMCUOTAS,(SELECT  PDEDAP CONCAT '/' CONCAT PDEMAP CONCAT '/' CONCAT PDEAAP FROM PLPDE WHERE PDEACC = E.DLACC ORDER BY PDESEQ  DESC FETCH FIRST 1 ROW ONLY ) AS FechaDesembolso, e.DLOAM as MontoOriginal, CASE WHEN (SELECT DLPPNU  FROM DLPMT WHERE  DLPPFL <> 'P' AND DLPACC = e.DLACC AND DLPPDY = RIGHT(", pstrAnio, " ,2) AND DLPPDM = ", pstrMes, ") IS NULL THEN 0 ELSE (SELECT DLPPNU  FROM DLPMT WHERE  DLPPFL <> 'P' AND DLPACC = e.DLACC AND DLPPDY = RIGHT( ", pstrAnio, " ,2) AND DLPPDM = ", pstrMes, ") END as CuotasInformada, " & ChrW(13) & ChrW(10)}
            'strArray(9) = ") END as CuotasInformada, " & ChrW(13) & ChrW(10)

            str = String.Concat(strArray)
            Dim str4 As String = Conversions.ToString(Interaction.IIf((Strings.Len(Strings.Trim(table3.Rows(0)("DLEMEN").ToString)) = 1), ("0" & Strings.Trim(table3.Rows(0)("DLEMEN").ToString)), Strings.Trim(table3.Rows(0)("DLEMEN").ToString)))
            If Not ((num = 2) Or (num = 3)) Then
                str = (str & "                   (SELECT DLPPDD CONCAT '/' CONCAT DLPPDM CONCAT '/' CONCAT DLPPDY FROM DLPMT WHERE DLPACC = E.DLACC AND DLPPFL <> 'P' ORDER BY DLPPNU FETCH FIRST 1 ROW ONLY) AS FechaCargo,")
            Else
                strArray = New String() {str, "                   t.dlcxd1 concat '/'  concat ", str4, " concat '/' concat ", pstrAnio, " AS FechaCargo,"}
                str = String.Concat(strArray)
            End If
            str = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(((((((Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject((((((Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(((((str & "                      CASE WHEN t.DLCNC1 IS NULL THEN 0 ELSE t.DLCNC1 END as CuotasPactadas, (SELECT COUNT(*) FROM DLPMT WHERE DLPACC =  e.DLACC AND DLPPFL = 'P') AS CuotasPagadas, CASE WHEN (t.DLCNC1 - t.DLCCP1) IS NULL THEN 0 ELSE (t.DLCNC1 - (SELECT COUNT(*) FROM DLPMT WHERE DLPACC =  e.DLACC AND DLPPFL = 'P')) END as CuotasPendientes, " & ChrW(13) & ChrW(10) & "                       (CASE WHEN  (SELECT  COUNT(*) FROM PLPAD WHERE Plcnpg = E.DLACC) = 1 THEN (CASE WHEN (c.DLVCM  CONCAT c.DLVCA) = (SELECT  pldmpv CONCAT pldapv FROM PLPAD WHERE Plcnpg = E.DLACC ORDER BY pldmpv  DESC FETCH FIRST 1 ROW ONLY) THEN 1 ELSE 3 END) ELSE (CASE WHEN (c.DLVCM  CONCAT c.DLVCA) = (SELECT  pldmpv CONCAT pldapv FROM PLPAD WHERE Plcnpg = E.DLACC ORDER BY pldmpv  DESC FETCH FIRST 1 ROW ONLY) THEN 2 ELSE 3 END ) END) AS NroTipoCredito" & ChrW(13) & ChrW(10)) & "                      FROM DLCRE e INNER JOIN" & ChrW(13) & ChrW(10) & "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & ChrW(13) & ChrW(10)) & "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & ChrW(13) & ChrW(10) & "                      INNER JOIN DLEMP D ON (D.DLECUN = E.DLCCC AND" & ChrW(13) & ChrW(10)) & "    (CAST (2000 + R.DLVCA AS CHARACTER (4))  CONCAT CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN   '0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 ))) ELSE   TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) )) END <='" & pstrAnio), Interaction.IIf((Strings.Len(Strings.Trim(pstrMes)) = 1), ("0" & Strings.Trim(pstrMes)), Strings.Trim(pstrMes))), "')) INNER JOIN DLCNT t ON e.DLACC = t.DLCACC"), ChrW(13) & ChrW(10))) & " WHERE     (TRIM(r.DLSTS) = '') ") & " AND CAST (2000 + R.DLVCA AS CHARACTER (4)) " & " CONCAT ") & " CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN " & " " & ChrW(9) & "'0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 )))") & " ELSE" & ChrW(9) & "TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) ))") & " END <='" & pstrAnio), Interaction.IIf((Strings.Len(Strings.Trim(pstrMes)) = 1), ("0" & Strings.Trim(pstrMes)), Strings.Trim(pstrMes))), "'")) & "GROUP BY e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, " & ChrW(13) & ChrW(10) & "e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS, e.DLFLI, e.DLOAM,  t.DLCNC1, t.DLCCP1, t.dlcxd1, t.dlcxm1, t.dlcxy1, t.DLCNC1" & ChrW(13) & ChrW(10)) & " HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = " & pstrCodigoCliente) & " AND CAST ( 2000+c.DLVCA  AS CHARACTER ( 4 ) ) " & " CONCAT ") & " CASE WHEN LENGTH(TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN" & " " & ChrW(9) & "'0' CONCAT TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))") & " ELSE" & " " & ChrW(9) & "TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))") & " END <='" & pstrAnio), Interaction.IIf((Strings.Len(Strings.Trim(pstrMes)) = 1), ("0" & Strings.Trim(pstrMes)), Strings.Trim(pstrMes))), "'"))
            aDODBRecordSet = connection.Execute(str, Missing.Value, -1)
            aDODBRecordSet.ActiveConnection = Nothing
            connection.Close()
            connection = Nothing
            adapter.Fill(dataSet, aDODBRecordSet, "dtPagareIBS")
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

    Public Function ObtenerTipoCuotaByCodigoIBS(ByVal pstrCodigoIBS As String) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[GetClientesDS_SelectTipoCuota]")
            dasql.AddParameter(command, "@CodigoIBS", pstrCodigoIBS, SqlDbType.VarChar)
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

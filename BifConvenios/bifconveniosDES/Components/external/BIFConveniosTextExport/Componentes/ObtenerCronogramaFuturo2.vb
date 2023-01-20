'RESPINOZA 20060130 - Modificacion de la clase para evitar problema en la utilizacion de DTS dentro del programa
Imports System.Configuration.ConfigurationSettings
Imports ADODB
Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet

Public Class ObtenerCronogramaFuturo2
    Protected mCustomerNumber As String
    Protected mAnio As String
    Protected mMes As String
    Protected mPID As String
    Protected mfecha_ProcesoAS400 As String
    Protected mblnProcesoExitoso As Boolean = True

    Public Function ExecuteImport(ByVal PID As String, ByVal CustomerNumber As String, ByVal mes As String, ByVal anio As String, ByVal Fecha_ProcesoAS400 As String) As Boolean
        Try
            mCustomerNumber = CustomerNumber
            mMes = mes
            mAnio = anio
            mfecha_ProcesoAS400 = Fecha_ProcesoAS400
            mPID = PID
            Me.procesaCarga()
            Return True
        Catch e As Exception
            frmMonitor.Add2LogMessage("Ocurrió un error: " + e.StackTrace.ToString)
            Return False
        End Try
    End Function

    Private Sub procesaCarga()
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        'Obtenemos la informacion de envio 
        Dim dsData As DataSet = getDLENV()
        Dim dt As DataTable = dsData.Tables("Result")
        Dim dr As DataRow
        For Each dr In dt.Rows
            Me.AdicionaDLENV(dr("Codigo_proceso"), dr("DLEAG"), dr("DLEAN"), dr("DLEAP"), dr("DLECC"), _
                        dr("DLECM"), dr("DLECO"), dr("DLECR"), dr("DLEFP"), dr("DLEIC"), dr("DLEMA"), _
                        dr("DLEMN"), dr("DLEMO"), dr("DLEMP"), dr("DLENE"), dr("DLENP"), dr("DLEPA"), _
                        dr("DLESD"), dr("DLEST"), dr("DLFLI"), dr("NUMCUOTAS"))
        Next

        'Obtenemos la informacion de la tabla CCR Y la almacenamos en el historico 

        dsData = Me.getDLCCR()
        dt = dsData.Tables("Result")

        For Each dr In dt.Rows
            Me.AddHistoricoDLCCR(dr("Codigo_proceso"), dr("DLACC"), dr("DLCCY"), dr("DLVCA"), _
                                dr("DLVCM"), dr("DLVCD"), dr("DLNCT"), dr("DLIMC"), dr("DLIPC"), _
                                dr("DLITF"), dr("DLSTS"))
        Next

    End Sub

#Region "Obtener informacion de cuotas"
    Public Function getDLENV() As DataSet
        Dim str As String
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim oDS As New DataSet()
        Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

        str = "SELECT '" & Trim(mPID) & "'  AS Codigo_proceso, e.DLCCC AS DLECC, e.DLAÑO AS DLEAN, e.DLAGC AS DLEAG, e.DLCOC AS DLECO, e.DLCCY AS DLEMO, e.DLACC AS DLENP, e.DLCEM AS DLECM, " & vbCrLf
        str = str & "                      e.DLNCL AS DLENE, e.DLAPP AS DLEPA, e.DLAPM AS DLEMA, trim(DLPRN) CONCAT ' ' CONCAT trim(DLSGN) AS DLEMN, " & vbCrLf
        str = str & "                      CASE WHEN TRIM(CAST(DLCCR AS CHARACTER(8)) CONCAT DLPLA CONCAT DLCUS) = '0' THEN '' ELSE CAST(SUBSTRING(TRIM(DLCUS) " & vbCrLf
        str = str & "                      CONCAT '0000', 1, 4)  CONCAT DLPLA CONCAT CAST(DLCCR AS CHARACTER(8))  AS CHARACTER(20)) END AS DLECR, 2000 + c.DLVCA AS DLEAP, " & vbCrLf
        str = str & "                      c.DLVCM AS DLEMP, SUM(r.DLIMC - r.DLIPC + r.DLITF) AS DLEIC, e.DLSTS AS DLEST, CAST(YEAR(CURRENT DATE) AS CHARACTER(4)) " & vbCrLf
        str = str & "                      CONCAT CASE LENGTH(TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(MONTH(CURRENT DATE) " & vbCrLf
        str = str & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(MONTH(CURRENT DATE) AS CHARACTER(2)), 3, 2) " & vbCrLf
        str = str & "                      END CONCAT CASE LENGTH(TRIM(CAST(DAY(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(DAY(CURRENT DATE) " & vbCrLf
        str = str & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(DAY(CURRENT DATE) AS CHARACTER(2)), 3, 2) END AS DLEFP, " & vbCrLf
        str = str & "                      CASE WHEN COUNT(1) > 1 THEN 1 ELSE 0 END DLESD, e.DLFLI, COUNT(1) AS NUMCUOTAS" & vbCrLf
        str = str & "FROM         DLCRE e INNER JOIN" & vbCrLf
        str = str & "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & vbCrLf
        str = str & "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & vbCrLf
        '' str = str & "                      INNER JOIN DLEMP D ON (D.DLECUN = E.DLCCC AND D .DLEAEN = c.DLVCA AND D.DLEMEN >= c.DLVCM)" & vbCrLf
        str = str & "                      INNER JOIN DLEMP D ON (D.DLECUN = E.DLCCC AND" & vbCrLf
        str = str & "    (CAST (2000 + R.DLVCA AS CHARACTER (4))  CONCAT CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN   '0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 ))) ELSE   TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) )) END <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'))" & vbCrLf
        str = str & " WHERE     (TRIM(r.DLSTS) = '') "
        str = str & " AND CAST (2000 + R.DLVCA AS CHARACTER (4)) "
        str = str & " CONCAT "
        str = str & " CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN "
        str = str & " 	'0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 )))"
        str = str & " ELSE"
        str = str & "	TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) ))"
        str = str & " END <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"
        str = str & "GROUP BY e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, " & vbCrLf
        str = str & "                      e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS, e.DLFLI" & vbCrLf
        str = str & " HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = " + mCustomerNumber '+ " AND 2000 + c.DLVCA <= " + mAnio + " AND c.DLVCM <= " + mMes
        str = str & " AND CAST ( 2000+c.DLVCA  AS CHARACTER ( 4 ) ) "
        str = str & " CONCAT "
        str = str & " CASE WHEN LENGTH(TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN"
        str = str & " 	'0' CONCAT TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))"
        str = str & " ELSE"
        str = str & " 	TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))"
        str = str & " END <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"

        result = myConnection.Execute(str)
        result.ActiveConnection = Nothing
        myConnection.Close()
        myConnection = Nothing
        daTransform.Fill(oDS, result, "Result")

        Return oDS
    End Function

    Public Function AdicionaDLENV(ByVal Codigo_proceso As String, ByVal DLEAG As String, ByVal DLEAN As String, _
        ByVal DLEAP As String, ByVal DLECC As String, ByVal DLECM As String, ByVal DLECO As String, _
        ByVal DLECR As String, ByVal DLEFP As String, ByVal DLEIC As String, ByVal DLEMA As String, _
        ByVal DLEMN As String, ByVal DLEMO As String, ByVal DLEMP As String, ByVal DLENE As String, _
        ByVal DLENP As String, ByVal DLEPA As String, ByVal DLESD As String, ByVal DLEST As String, _
        ByVal DLFLI As String, ByVal NUMCUOTAS As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, DLEAG, DLEAN, DLEAP, DLECC, DLECM, DLECO, DLECR, DLEFP, DLEIC, _
                        DLEMA, DLEMN, DLEMO, DLEMP, DLENE, DLENP, DLEPA, DLESD, DLEST, DLFLI, NUMCUOTAS})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function
#End Region


#Region "Obtener la informacion de las cuotas por mes"
    'Obtener informacion de la tabla de cuotas
    Function getDLCCR() As DataSet
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim oDS As New DataSet()
        Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

        'TODO AHSP: 20081023 - Antes se obtenia solo el *.DLCCR, pero cuando se realizaba un pago Extraordinario
        'Se incrementaba el Nro de Cuotas, por lo tanto ahora le disminuimos el Nro de pagos Extraordinarios para saber en que Nro de cuota estamos
        Dim strSQL As String = "SELECT '" & Trim(mPID) & "'  AS Codigo_proceso, " & vbCrLf
        strSQL = strSQL & "DLCCR.DLACC, DLCCR.DLCCY, DLCCR.DLVCA, DLCCR.DLVCM, DLCCR.DLVCD, " & vbCrLf
        strSQL = strSQL & "(DLCCR.DLNCT - (SELECT COUNT(*) FROM DLPMT WHERE DLPACC = DLCCR.DLACC AND DLPPNU = 999)) AS DLNCT, " & vbCrLf
        strSQL = strSQL & "DLCCR.DLIMC, DLCCR.DLIPC, DLCCR.DLITF, DLCCR.DLSTS " & vbCrLf
        strSQL = strSQL & "FROM DLCCR "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & " CAST ( 2000+DLVCA  AS CHARACTER ( 4 ) ) "
        strSQL = strSQL & " CONCAT "
        strSQL = strSQL & " CASE WHEN LENGTH(TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN "
        strSQL = strSQL & " 	'0' CONCAT TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) )) "
        strSQL = strSQL & " ELSE "
        strSQL = strSQL & " 	TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))"
        strSQL = strSQL & " END"
        strSQL = strSQL & " <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"
        strSQL = strSQL & " AND DLACC IN (SELECT DLACC FROM   DLCRE WHERE DLCCC  = " & mCustomerNumber & " )" & vbCrLf
        strSQL = strSQL & "AND DLSTS = ''"

        result = myConnection.Execute(strSQL)
        result.ActiveConnection = Nothing
        myConnection.Close()
        myConnection = Nothing
        daTransform.Fill(oDS, result, "Result")

        Return oDS
    End Function

    'Adicionar la información a la tabla HISTORICA DE PAGOS (HistoricoDLCCR)
    Public Function AddHistoricoDLCCR(ByVal Codigo_proceso As String, ByVal DLACC As String, _
                                ByVal DLCCY As String, ByVal DLVCA As String, ByVal DLVCM As String, _
                                ByVal DLVCD As String, ByVal DLNCT As String, ByVal DLIMC As String, _
                                ByVal DLIPC As String, ByVal DLITF As String, ByVal DLSTS As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, DLACC, DLCCY, DLVCA, DLVCM, DLVCD, DLNCT, _
                            DLIMC, DLIPC, DLITF, DLSTS})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function

#End Region


End Class

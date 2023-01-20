Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB
Imports System.Data.OleDb


Namespace BIFConvenios
    Public Class Seguimiento

        Public Shared Function GetDocumentoPagosPendientesMontosPendientes(ByVal numerosPagare As String, _
                ByVal amounts As String, ByVal anio As String, ByVal mes As String) As DataSetDocumentoCobranza

            'Dim connection As New OleDbConnection(System.Configuration.ConfigurationManager.AppSettings("AS400-ConnectionString-Convenios"))
            Dim connection As New OleDbConnection(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            Dim strQuery As String
            Dim myCommand As OleDbCommand
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()
            Dim oDS As New DataSetDocumentoCobranza()

            myCommand = connection.CreateCommand()
            strQuery = "SELECT DISTINCT c.dlacc, cusna1, CONCAT(CONCAT(CONCAT(trim(cusna2), ' ') , CONCAT(trim(cusna3), ' ' )), trim(cusna4)) AS Direccion, cuscty, cuszpc, BRNNUM, BRNNME, BRNADR, BRNPHN, " + _
                        " dlncc, '" + anio + "' as DLVCA,'" + mes + "' as DLVCM, DLVCD, CCR.DLCCY, 0 AS DLEIC, ciudag, provag, deptag, horaag , '" + amounts + "' as Data, " + _
                        " CUSFNA, CUSLN1, CUSLN2, CUSSEX,  '' as SIGN1NAME,  '' as  SIGN2NAME ,  '' as SIGN1POSITION ,  '' as  SIGN2POSITION, emp.DLEAEN, emp.DLEMEN, emp.DLEDEN " + _
                        " FROM dlcre c, CUMST st, CNTRLBRN B, DLCCR CCR, covf013 f03, DLEMP emp " + _
                        " WHERE (DLCUN = CUSCUN) AND (DLAGC = BRNNUM) AND (BRNBNK = '01') AND (CCR.DLACC = c.dlacc) AND (C.dlacc in  (" + numerosPagare + " )) AND (CCR.dlsts = '') " + _
                        " AND (cdagen = BRNNUM) AND (c.DLCCC = emp.DLECUN) "
            myCommand.CommandText = strQuery
            daTransform.SelectCommand = myCommand
            connection.Open()
            daTransform.Fill(oDS, "DatosCobranza")

            connection.Close()
            connection.ReleaseObjectPool()
            connection.Dispose()
            connection = Nothing
            Return oDS
        End Function

        'Obtenemos la informacion de los pagos pendientes para generar los documentos de cobranza
        Public Shared Function GetDocumentoPagosPendientes(ByVal numerosPagare As String, ByVal mes As String, ByVal anio As String) As DataSetDocumentoCobranza
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim strQuery As String
            'Dim oDS As New DataSet()
            Dim oDS As New DataSetDocumentoCobranza()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            'myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            strQuery = "SELECT c.dlacc, cusna1, CONCAT(CONCAT(CONCAT(trim(cusna2), ' ' ), CONCAT(trim(cusna3), ' ')), trim(cusna4)) AS Direccion, cuscty, cuszpc, BRNNUM, BRNNME, BRNADR, BRNPHN, " + _
                        " dlncc, DLVCA, DLVCM, DLVCD, CCR.DLCCY, CCR.DLIMC - CCR.DLIPC + CCR.DLITF AS DLEIC, ciudag, provag, deptag, horaag , '' as Data," + _
                        " CUSFNA, CUSLN1, CUSLN2, CUSSEX" + _
                        " FROM dlcre c, CUMST st, CNTRLBRN B, DLCCR CCR, covf013 f03 " + _
                        " WHERE (DLCUN = CUSCUN) AND (DLAGC = BRNNUM) AND (BRNBNK = '01') AND (CCR.DLACC = c.dlacc) AND (C.dlacc in  (" + numerosPagare + " )) AND (CCR.dlsts = '') " + _
                        " AND (cdagen = BRNNUM) " + _
                        " AND CAST(2000 + CCR.DLVCA AS CHARACTER(4)) " + _
                        "                      CONCAT CASE WHEN LENGTH(TRIM(CAST(CCR.DLVCM AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) " + _
                        "                      ELSE TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) END <= '" & anio & IIf(Len(Trim(mes)) = 1, "0" + Trim(mes), Trim(mes)) & "'"
            ' " AND DLVCM <=  " + mes        'TEMPORAL

            result = myConnection.Execute(strQuery)

            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            daTransform.Fill(oDS, result, "DatosCobranza")
            Return oDS
        End Function


        ' Obtener informacion de las deudas de los clientes antes del envio en caso de tener alguna aplicacion en su cuota
        'Public Shared Function GetDeudaAntesProcesoEnvio(ByVal proceso As String) As DataSet
        '    Dim myConnection As New SqlConnection(GetDBConnectionString)
        '    Dim oDS As New DataSet()
        '    Dim oAdapter As New SqlDataAdapter()

        '    Dim returnValue As Integer
        '    Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
        '        CType(MethodBase.GetCurrentMethod(), MethodInfo), _
        '        New Object() {proceso})

        '    oAdapter.SelectCommand = myCommand
        '    myConnection.Open()
        '    oAdapter.Fill(oDS, "DeudasProceso")
        '    myConnection.Close()
        '    Return oDS
        'End Function

#Region "Reportes"
        'RESPINOZA 20070914 - Obtenemos informacion adicional de los clientes del convenio
        Public Shared Function getDatosAdicionalesClienteConvenio(ByVal Cliente As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            'ST.CUSZPC, , ST.CUSFAX Fax
            str = "SELECT     CND.DEAACC PAGARE, CND.CDGSTS AS SITUACIONLABORAL,  CONCAT(CONCAT(CONCAT(trim(cusna2), ' ' ), CONCAT(trim(cusna3), ' ')), trim(cusna4)) AS DIRECCION, ST.CUSCTY CUIDAD, ST.CUSHPN TELCASA, " & _
            "                      ST.CUSPHN TELTRABAJO, ST.CUSPH1 TELOTRO, ST.CUSIDN DLDNI, DLS.DEASTS, DLS.DEADLC" & _
            " FROM         DLCND CND INNER JOIN " & _
            "                       DEALS DLS ON CND.DEAACC = DLS.DEAACC INNER JOIN " & _
            "                       CUMST ST ON ST.CUSCUN = DLS.DEACUN INNER JOIN " & _
            "                       ACMST A ON A.ACMACC = DLS.DEARAC " & _
            " WHERE     (DLS.DEATYP = 'CONV') AND (A.ACMCUN = " & Cliente & ")"

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosAdicionales")
            Return oDS
        End Function


        'Obtener la informacion de descuentos actualizada desde IBS
        Public Shared Function getMontoDescuento(ByVal Cliente As String, ByVal mAnio As String, _
                    ByVal mMes As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String

            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()
            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT e.DLACC as DLNP, SUM(r.DLIMC - r.DLIPC + r.DLITF) AS DLEIC, COUNT (1) AS NUMCOUTASACTUAL, SUM (r.DLIPC) AS PAGOPARCIAL" & vbCrLf ', e.DLSTS as SITUACIONLABORALACTUAL  '-- retiramos al situacion laboral de este query por que se elimina la informacion cuando no hay cuotas
            str = str & "                      FROM DLCRE e INNER JOIN" & vbCrLf
            str = str & "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & vbCrLf
            str = str & "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & vbCrLf
            str = str & "                      WHERE     (TRIM(r.DLSTS) = '') AND CAST(2000 + R.DLVCA AS CHARACTER(4)) " & vbCrLf
            str = str & "                      CONCAT CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER(2))) " & vbCrLf
            str = str & "                      ELSE TRIM(CAST(R.DLVCM AS CHARACTER(2))) END <= '" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'" & vbCrLf
            str = str & "                      GROUP BY e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, " & vbCrLf
            str = str & "                      e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS, e.DLFLI" & vbCrLf
            str = str & "                      HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = " & Cliente & " AND 2000 + c.DLVCA <=  " & mAnio & " AND c.DLVCM <= " & mMes & "  AND CAST(2000 + c.DLVCA AS CHARACTER(4)) " & vbCrLf
            str = str & "                                            CONCAT CASE WHEN LENGTH(TRIM(CAST(c.DLVCM AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(c.DLVCM AS CHARACTER(2))) " & vbCrLf
            str = str & "                      ELSE TRIM(CAST(c.DLVCM AS CHARACTER(2))) END <= '" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosDescuento")
            Return oDS
        End Function
        'EA2017-11386 Se agrega DLMOT - MOTIVO DE DEVOLUCION.
        Public Shared Function getDatosDevolucion(ByVal Cliente As String, ByVal NumeroPagare As String, ByVal mAnio As String, _
                    ByVal mMes As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String

            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()
            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            str = "SELECT DLIDV, DLMOT FROM(" & vbCrLf
            str = str & " SELECT DLIDV, DLMOT FROM DLREC WHERE DLRCC =" & Cliente & " AND DLRNP= '" & NumeroPagare & "' AND " & vbCrLf
            str = str & " DLRAP = " & mAnio & " And DLRMP = " & mMes & " And DLEDV = 'P' " & vbCrLf
            str = str & " UNION ALL " & vbCrLf
            str = str & " SELECT DLIDV, DLMOT FROM DLRECH WHERE DLRCC = " & Cliente & " AND DLRNP= '" & NumeroPagare & "' AND " & vbCrLf
            str = str & " DLRAP = " & mAnio & " AND DLRMP =" & mMes & " And DLEDV = 'P' " & ")T "

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosDevolucion")
            Return oDS
        End Function


        'Obtener los pagares que no han sido bloqueados en el sistema
        Public Shared Function getInformacionBloqueos(ByVal Cliente As String, ByVal mAnio As String, _
                    ByVal mMes As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String

            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()
            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT DLACC " & vbCrLf
            str = str & " FROM DLCRE " & vbCrLf
            str = str & " WHERE (DLACC NOT IN " & vbCrLf
            str = str & " (SELECT     WNRPG " & vbCrLf
            str = str & "        FROM CLTSEXC)) AND (DLCCC IN ('" & Cliente & "')) AND (DLACC IN " & vbCrLf
            str = str & " (SELECT DLACC" & vbCrLf
            str = str & " FROM DLCCR" & vbCrLf
            str = str & "        WHERE CAST(2000 + DLVCA AS CHARACTER(4)) CONCAT CASE WHEN LENGTH(TRIM(CAST(DLVCM AS CHARACTER(2)))) " & vbCrLf
            str = str & "               = 1 THEN '0' CONCAT TRIM(CAST(DLVCM AS CHARACTER(2))) ELSE TRIM(CAST(DLVCM AS CHARACTER(2))) END <= '" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'))"
            'RESPINOZA 20080103 - Se quito la condicion (DLSTS = '') en el segundo subquery (Condicion de pagado (P) o no pagado ('') en el archivo DLCCR)

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosBloqueos")
            Return oDS
        End Function

        Public Shared Function getDatosCuentas(ByVal Cliente As String, ByVal TipoCuentas As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT   DLS.DEAACC PAGARE, AC.ACMCUN, AC.ACMACC, AC.ACMCCY, AC.ACMATY, AC.ACMPRO, AC.ACMCLS" & _
            " FROM  DEALS   DLS INNER JOIN " & _
            "                       ACMST A  ON DLS.DEARAC =  A.ACMACC INNER JOIN " & _
            "                       ACMST AC ON DLS.DEACUN = AC.ACMCUN " & _
            " WHERE (DLS.DEATYP = 'CONV') AND (A.ACMCUN = " & Cliente & ") AND (AC.ACMAST <> 'C') AND AC.ACMPRO IN (" & TipoCuentas & ")"

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosAdicionales")
            Return oDS
        End Function

        Public Shared Function getDatosCastigo(ByVal Cliente As String) As DataSet
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT   DLS.DEAACC PAGARE, RCD.AÑOREP, RCD.MESREP, RCD.DIAREP, RCD.CODSBS, RCD.RAZSOC" & _
            " FROM  DEALS   DLS INNER JOIN " & _
            "                       ACMST A  ON DLS.DEARAC =  A.ACMACC INNER JOIN " & _
            "                       CUMST ST ON ST.CUSCUN = DLS.DEACUN INNER JOIN " & _
            "                       RCDF067 RCD ON ST.CUSIDN = RCD.KDOC " & _
            " WHERE (DLS.DEATYP = 'CONV') AND (A.ACMCUN = " & Cliente & ")"

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosAdicionales")
            Return oDS
        End Function

        'EA2017-11386 Se agrega DLMOT - MOTIVO DE DEVOLUCION.
        Public Shared Function getCuentaConDevolucionPendiente(ByVal Cliente As String, ByVal NumeroPagare As String, ByVal mAnio As String, _
                    ByVal mMes As String) As Boolean
            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim mFlagPendiente As Boolean = False

            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()
            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")) 'book
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            'str = "SELECT * FROM DLDEVC"
            str = "SELECT DVNOPE, DVNDOC FROM DLDEVC"
            str = str & " WHERE DVNOPE = " & NumeroPagare & " AND"
            str = str & " DVPERC = " & mAnio + IIf(mMes.Length = 1, "0" + mMes, mMes)

            result = myConnection.Execute(str)
            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosDevolucion")
            Try
                If (oDS.Tables(0).Rows().Count() > 0) Then
                    mFlagPendiente = True
                End If
            Catch ex As Exception

            End Try
            Return mFlagPendiente
        End Function

#End Region

#Region "Carta de Cobranza"
        'RESPINOZA 20070911 - Obtenemos las cabeceras para las cartas de cobranza
        Public Shared Function GetHeaderPagaresCarta(ByVal numerosPagare As String, ByVal mes As String, ByVal anio As String) As DataSetCuotasPendientes

            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim oDS As New DataSetCuotasPendientes()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT DISTINCT c.DLACC, st.CUSNA1, CONCAT(CONCAT(CONCAT(trim(cusna2), ' ' ), CONCAT(trim(cusna3), ' ')), trim(cusna4)) AS Direccion, st.CUSCTY, st.CUSZPC, B.BRNNUM, B.BRNNME, B.BRNADR, B.BRNPHN, c.DLNCC, " & _
                "                      f03.CIUDAG, f03.PROVAG, f03.DEPTAG, f03.HORAAG, '' AS Data, st.CUSFNA, st.CUSLN1, st.CUSLN2, st.CUSSEX, " & _
                "    '' as SIGN1NAME,  '' as  SIGN2NAME ,  '' as SIGN1POSITION ,  '' as  SIGN2POSITION" & _
                " FROM DLCRE c INNER JOIN " & _
                " DLCCR CCR ON c.DLACC = CCR.DLACC INNER JOIN " & _
                " CUMST st ON c.DLCUN = st.CUSCUN INNER JOIN " & _
                " CNTRLBRN B ON c.DLAGC = B.BRNNUM INNER JOIN " & _
                " COVF013 f03 ON B.BRNNUM = f03.CDAGEN " & _
                " WHERE (B.BRNBNK = '01') AND (c.DLACC IN (" & _
                numerosPagare & ")) AND (CCR.DLSTS = '') " & _
                " AND CAST(2000 + CCR.DLVCA AS CHARACTER(4)) " + _
                "                      CONCAT CASE WHEN LENGTH(TRIM(CAST(CCR.DLVCM AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) " + _
                "                      ELSE TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) END <= '" & anio & IIf(Len(Trim(mes)) = 1, "0" + Trim(mes), Trim(mes)) & "'"




            'AND (CCR.DLVCM <= " & mes & ")"

            result = myConnection.Execute(str)

            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosDescuentoHeader")
            Return oDS
        End Function

        'RESPINOZA 20070911 - Obtenemos la informacion de detalle de las deudas de los cliente morosos 
        Public Shared Function GetDetailPagaresCarta(ByVal numerosPagare As String, ByVal mes As String, ByVal anio As String) As DataSetCuotasPendientes

            Dim myConnection As ADODB.Connection
            myConnection = New ADODB.Connection()

            Dim result As New ADODB.Recordset()
            Dim str As String
            Dim oDS As New DataSetCuotasPendientes()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()

            myConnection.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            str = "SELECT c.DLACC, CCR.DLVCA, CCR.DLVCM, CCR.DLVCD, CCR.DLCCY, CCR.DLIMC - CCR.DLIPC + CCR.DLITF AS DLEIC " & _
                " FROM DLCRE c INNER JOIN " & _
                " DLCCR CCR ON c.DLACC = CCR.DLACC INNER JOIN " & _
                " CUMST st ON c.DLCUN = st.CUSCUN INNER JOIN " & _
                " CNTRLBRN B ON c.DLAGC = B.BRNNUM INNER JOIN " & _
                " COVF013 f03 ON B.BRNNUM = f03.CDAGEN " & _
                " WHERE (B.BRNBNK = '01') AND (c.DLACC IN (" & _
                numerosPagare & ")) AND (CCR.DLSTS = '') " & _
                " AND CAST(2000 + CCR.DLVCA AS CHARACTER(4)) " + _
                "                      CONCAT CASE WHEN LENGTH(TRIM(CAST(CCR.DLVCM AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) " + _
                "                      ELSE TRIM(CAST(CCR.DLVCM AS CHARACTER(2))) END <= '" & anio & IIf(Len(Trim(mes)) = 1, "0" + Trim(mes), Trim(mes)) & "'"

            'AND (CCR.DLVCM <= " & mes & ")"

            result = myConnection.Execute(str)

            result.ActiveConnection = Nothing

            myConnection.Close()
            myConnection = Nothing
            daTransform.Fill(oDS, result, "DatosDescuentoDetail")
            Return oDS
        End Function

        'RESPINOZA 20070911 - Mezclamos los dataset en un solo grupo para poder armar la estructura Master / Detail
        Public Shared Function GetDetailPagaresJoin(ByVal numerosPagare As String, ByVal mes As String, ByVal anio As String) As DataSetCuotasPendientes
            Dim dsJoin As New DataSetCuotasPendientes()
            Dim dsHeader As DataSetCuotasPendientes
            Dim dsDetail As DataSetCuotasPendientes

            dsHeader = GetHeaderPagaresCarta(numerosPagare, mes, anio)
            dsDetail = GetDetailPagaresCarta(numerosPagare, mes, anio)
            Dim dtHeader As DataTable = dsHeader.Tables("DatosDescuentoHeader").Copy()
            Dim dtDetail As DataTable = dsDetail.Tables("DatosDescuentoDetail").Copy()

            'Adicionamos ambos datatable a nuestro dataset final
            dsJoin.Relations.Clear()
            dsJoin.Tables.Clear()
            dsJoin.Tables.Add(dtHeader)
            dsJoin.Tables.Add(dtDetail)

            'adicionamos las relaciones entre ambas tablas 
            Dim parent As DataColumn = dsJoin.Tables("DatosDescuentoHeader").Columns("DLACC")
            Dim child As DataColumn = dsJoin.Tables("DatosDescuentoDetail").Columns("DLACC")

            Dim CustomerRelation1 As New DataRelation("Division1", parent, child, False)
            dsJoin.Relations.Add(CustomerRelation1)

            'dsJoin.WriteXmlSchema("c:/dataset.xsd")
            Return dsJoin
        End Function
#End Region


    End Class
End Namespace

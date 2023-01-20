Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Runtime.InteropServices
Imports System.Runtime.CompilerServices
Imports BIFConvenios.BE

Public Class CuotaDO
    Private cUtils As WS.Utils

    Private conexionIBS As String

    Private conexionConvenios As String

    Public Sub New()
        MyBase.New()
        Me.cUtils = New WS.Utils()
        Me.conexionIBS = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
        Me.conexionConvenios = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Sub

    Private Sub AgregarParametro(ByRef cmd As SqlCommand, ByVal nombreParam As String, ByVal direccionParam As ParameterDirection, ByVal tipoParam As DbType, ByVal valorParam As Object)
        Dim parameter As IDbDataParameter = cmd.CreateParameter
        parameter.ParameterName = nombreParam
        parameter.DbType = tipoParam
        parameter.Direction = direccionParam
        parameter.Value = valorParam
        cmd.Parameters.Add(parameter)
    End Sub

    Public Function CronogramaFuturo(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String, Optional ByVal pNombreFormatoArchivo As String = "") As DataSet
        Dim lds As DataSet = New DataSet()
        Dim strArrays() As String = {"EXEC CronogramaFuturo", pNombreFormatoArchivo, " '", pCodigo_proceso, "','", pSituacionTrabajador, "'"}
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat(strArrays), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Function CronogramaFuturoDefault(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim pCodigoProceso() As String = {"EXEC CronogramaFuturoDefault '", pCodigo_proceso, "','", pSituacionTrabajador, "'"}
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat(pCodigoProceso), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Function CronogramaFuturoDefault_ConDescuento(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim pCodigoProceso() As String = {"EXEC CronogramaFuturoDefault_ConDescuento '", pCodigo_proceso, "','", pSituacionTrabajador, "'"}
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat(pCodigoProceso), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Function CronogramaFuturoExecutor(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String, ByVal pFormato As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim pCodigoProceso() As String = {"EXEC CronogramaFuturoExecutor '", pCodigo_proceso, "','", pSituacionTrabajador, "','", pFormato, "'"}
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat(pCodigoProceso), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Function CronogramaFuturoTexto(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String, Optional ByVal pNombreFormatoArchivo As String = "") As DataSet
        Dim lds As DataSet = New DataSet()
        Dim strArrays() As String = {"EXEC CronogramaFuturo", pNombreFormatoArchivo, "Text '", pCodigo_proceso, "','", pSituacionTrabajador, "'"}
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat(strArrays), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Sub EstableceErrorProceso(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "EstableceErrorProceso"
        Me.AgregarParametro(cmd, "@Codigo_proceso", ParameterDirection.Input, DbType.String, pCodigo_proceso)
        Me.AgregarParametro(cmd, "@usuario", ParameterDirection.Input, DbType.String, pUsuario)
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub FinalizaImportacionPagares(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "EnviaEspacioTrabajo"
        Me.AgregarParametro(cmd, "@Codigo_proceso", ParameterDirection.Input, DbType.String, pCodigo_proceso)
        Me.AgregarParametro(cmd, "@usuario", ParameterDirection.Input, DbType.String, pUsuario)
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Function GetClientesDS_SelectTipoCuota(ByVal pstrCodigoIBS As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat("EXEC [dbo].[GetClientesDS_SelectTipoCuota] '", pstrCodigoIBS, "'"), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds
    End Function

    Public Sub InsertaDLENV(ByVal pdr As DataRow)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "AdicionaDLENV"
        Me.AgregarParametro(cmd, "@Codigo_Proceso", ParameterDirection.Input, DbType.String, pdr("Codigo_Proceso").ToString)
        Me.AgregarParametro(cmd, "@DLEAG", ParameterDirection.Input, DbType.String, pdr("DLEAG").ToString)
        Me.AgregarParametro(cmd, "@DLEAN", ParameterDirection.Input, DbType.String, pdr("DLEAN").ToString)
        Me.AgregarParametro(cmd, "@DLEAP", ParameterDirection.Input, DbType.String, pdr("DLEAP").ToString)
        Me.AgregarParametro(cmd, "@DLECC", ParameterDirection.Input, DbType.String, pdr("DLECC").ToString)
        Me.AgregarParametro(cmd, "@DLECM", ParameterDirection.Input, DbType.String, pdr("DLECM").ToString)
        Me.AgregarParametro(cmd, "@DLECO", ParameterDirection.Input, DbType.String, pdr("DLECO").ToString)
        Me.AgregarParametro(cmd, "@DLECR", ParameterDirection.Input, DbType.String, pdr("DLECR").ToString)
        Me.AgregarParametro(cmd, "@DLEFP", ParameterDirection.Input, DbType.String, pdr("DLEFP").ToString)
        Me.AgregarParametro(cmd, "@DLEIC", ParameterDirection.Input, DbType.String, pdr("DLEIC").ToString)
        Me.AgregarParametro(cmd, "@DLEMA", ParameterDirection.Input, DbType.String, pdr("DLEMA").ToString)
        Me.AgregarParametro(cmd, "@DLEMN", ParameterDirection.Input, DbType.String, pdr("DLEMN").ToString)
        Me.AgregarParametro(cmd, "@DLEMO", ParameterDirection.Input, DbType.String, pdr("DLEMO").ToString)
        Me.AgregarParametro(cmd, "@DLEMP", ParameterDirection.Input, DbType.String, pdr("DLEMP").ToString)
        Me.AgregarParametro(cmd, "@DLENE", ParameterDirection.Input, DbType.String, pdr("DLENE").ToString)
        Me.AgregarParametro(cmd, "@DLENP", ParameterDirection.Input, DbType.String, pdr("DLENP").ToString)
        Me.AgregarParametro(cmd, "@DLEPA", ParameterDirection.Input, DbType.String, pdr("DLEPA").ToString)
        Me.AgregarParametro(cmd, "@DLESD", ParameterDirection.Input, DbType.String, pdr("DLESD").ToString)
        Me.AgregarParametro(cmd, "@DLEST", ParameterDirection.Input, DbType.String, pdr("DLEST").ToString)
        Me.AgregarParametro(cmd, "@DLFLI", ParameterDirection.Input, DbType.String, pdr("DLFLI").ToString)
        Me.AgregarParametro(cmd, "@NUMCUOTAS", ParameterDirection.Input, DbType.String, pdr("NUMCUOTAS").ToString)
        Me.AgregarParametro(cmd, "@FECDESEMBOLSO", ParameterDirection.Input, DbType.String, pdr("FECHADESEMBOLSO").ToString)
        Me.AgregarParametro(cmd, "@MONTOORIGINAL", ParameterDirection.Input, DbType.String, pdr("MONTOORIGINAL").ToString)
        Me.AgregarParametro(cmd, "@CUOTASINFORMADAS", ParameterDirection.Input, DbType.Int32, pdr("CuotasInformada").ToString)
        Me.AgregarParametro(cmd, "@FECCARGO", ParameterDirection.Input, DbType.String, pdr("FechaCargo").ToString)
        Me.AgregarParametro(cmd, "@CUOTASPACTADAS", ParameterDirection.Input, DbType.Int32, pdr("CuotasPactadas").ToString)
        Me.AgregarParametro(cmd, "@CUOTASPAGADAS", ParameterDirection.Input, DbType.Int32, pdr("CuotasPagadas").ToString)
        Me.AgregarParametro(cmd, "@CUOTASPENDIENTES", ParameterDirection.Input, DbType.Int32, pdr("CuotasPendientes").ToString)
        Me.AgregarParametro(cmd, "@NROTIPOCREDITO", ParameterDirection.Input, DbType.Int32, pdr("NroTipoCredito").ToString)
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub InsertaHistoricoDLCCR(ByVal pdr As DataRow)
        Try
            Dim connection As New SqlConnection(Me.conexionConvenios)
            Dim cmd As New SqlCommand
            cmd.Connection = connection
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "AddHistoricoDLCCR"
            cmd.CommandTimeout = 120
            Me.AgregarParametro(cmd, "@Codigo_Proceso", ParameterDirection.Input, DbType.String, pdr("Codigo_Proceso").ToString)
            Me.AgregarParametro(cmd, "@DLACC", ParameterDirection.Input, DbType.String, pdr("DLACC").ToString)
            Me.AgregarParametro(cmd, "@DLCCY", ParameterDirection.Input, DbType.String, pdr("DLCCY").ToString)
            Me.AgregarParametro(cmd, "@DLVCA", ParameterDirection.Input, DbType.String, pdr("DLVCA").ToString)
            Me.AgregarParametro(cmd, "@DLVCM", ParameterDirection.Input, DbType.String, pdr("DLVCM").ToString)
            Me.AgregarParametro(cmd, "@DLVCD", ParameterDirection.Input, DbType.String, pdr("DLVCD").ToString)
            Me.AgregarParametro(cmd, "@DLNCT", ParameterDirection.Input, DbType.String, pdr("DLNCT").ToString)
            Me.AgregarParametro(cmd, "@DLIMC", ParameterDirection.Input, DbType.String, pdr("DLIMC").ToString)
            Me.AgregarParametro(cmd, "@DLIPC", ParameterDirection.Input, DbType.String, pdr("DLIPC").ToString)
            Me.AgregarParametro(cmd, "@DLITF", ParameterDirection.Input, DbType.String, pdr("DLITF").ToString)
            Me.AgregarParametro(cmd, "@DLSTS", ParameterDirection.Input, DbType.String, pdr("DLSTS").ToString)
            connection.Open()
            cmd.ExecuteNonQuery()
            connection.Close()
        Catch exception1 As SqlException
            Dim ex As SqlException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
    End Sub

    Public Function ObtenerCabeceraCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT  DEM.DLEDSC,  SUM(DLS.DLSAMT) AS DLSAMT, SUM(DLS.DLSPAD) AS DLSPAD FROM DLCNV DL " & vbCrLf & "", " INNER JOIN DEALS DE  ON DL.NCTAMN  = DE.DEARAC " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " INNER JOIN DLSDE DLS ON DE.DEAACC  = DLS.DLSACC " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " INNER JOIN CUMST CU  ON DE.DEACUN  = CU.CUSCUN " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " INNER JOIN DLEMP DEM ON DEM.DLECUN = DL.CNVCUN " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " WHERE ")
        lstrsql = String.Concat(lstrsql, " DL.CNVCUN = ", Strings.Trim(pCodigo_Cliente), " AND DL.CASIMN = 'S' AND DL.ESCONV = 1" & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " AND DE.DEATYP = 'CONV' AND DLS.DLSSEQ = 2 AND DE.DEAPRI > 0")
        Dim strArrays() As String = {lstrsql, " AND DLS.DLSPDY = ", pAnio, " AND DLS.DLSPDM = ", pMes, " GROUP BY DEM.DLEDSC"}
        lstrsql = String.Concat(strArrays)
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ObtenerDetalleCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT  DE.DEAACC, CU.CUSNA1, DLS.DLSAMT, DLS.DLSPAD FROM DLCNV DL " & vbCrLf & "", " INNER JOIN DEALS DE  ON DL.NCTAMN  = DE.DEARAC " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " INNER JOIN DLSDE DLS ON DE.DEAACC  = DLS.DLSACC " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " INNER JOIN CUMST CU  ON DE.DEACUN  = CU.CUSCUN " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " WHERE ")
        lstrsql = String.Concat(lstrsql, " DL.CNVCUN = ", Strings.Trim(pCodigo_Cliente), " AND DL.CASIMN = 'S' AND DL.ESCONV = 1" & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " AND DE.DEATYP = 'CONV' AND DLS.DLSSEQ = 2 AND DE.DEAPRI > 0 ")
        Dim strArrays() As String = {lstrsql, " AND DLS.DLSPDY = ", pAnio, " AND DLS.DLSPDM = ", pMes, " ORDER BY CU.CUSCUN ASC"}
        lstrsql = String.Concat(strArrays)
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ObtenerDeudaDeIBS(ByVal pCodigo_proceso As String, ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT '", Strings.Trim(pCodigo_proceso), "'  AS Codigo_proceso, " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, "DLCCR.DLACC, DLCCR.DLCCY, DLCCR.DLVCA, DLCCR.DLVCM, DLCCR.DLVCD, " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, "(DLCCR.DLNCT - (SELECT COUNT(*) FROM DLPMT WHERE DLPACC = DLCCR.DLACC AND DLPPNU = 999)) AS DLNCT, " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, "DLCCR.DLIMC, DLCCR.DLIPC, DLCCR.DLITF, DLCCR.DLSTS " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, "FROM DLCCR DLCCR ")
        lstrsql = String.Concat(lstrsql, "WHERE ")
        lstrsql = String.Concat(lstrsql, " CAST ( 2000+DLVCA  AS CHARACTER ( 4 ) ) ")
        lstrsql = String.Concat(lstrsql, " CONCAT ")
        lstrsql = String.Concat(lstrsql, " CASE WHEN LENGTH(TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN ")
        lstrsql = String.Concat(lstrsql, " 	'0' CONCAT TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) )) ")
        lstrsql = String.Concat(lstrsql, " ELSE ")
        lstrsql = String.Concat(lstrsql, " 	TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))")
        lstrsql = String.Concat(lstrsql, " END")
        lstrsql = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(String.Concat(String.Concat(lstrsql, " <='"), pAnio), Interaction.IIf(Strings.Len(Strings.Trim(pMes)) = 1, String.Concat("0", Strings.Trim(pMes)), Strings.Trim(pMes))), "'"))
        lstrsql = String.Concat(lstrsql, " AND DLACC IN (SELECT DLACC FROM   DLCRE WHERE DLCCC  = ", pCodigo_Cliente, " )" & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, "AND DLSTS = ''")
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ObtenerMotivosDeIBS(ByVal pCodigo_proceso As String, ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT '", Strings.Trim(pCodigo_proceso), "'  AS Codigo_proceso, DLRCC, DLRNP, DLRAP, DLRMP, DLMOT, DLDMO " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " FROM DLREC " & vbCrLf & "")
        lstrsql = String.Concat(lstrsql, " WHERE ")
        Dim strArrays() As String = {lstrsql, " DLRCC = ", Strings.Trim(pCodigo_Cliente), " AND DLRAP = ", Strings.Trim(pAnio), " AND DLRMP = ", Strings.Trim(pMes)}
        lstrsql = String.Concat(strArrays)
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ObtenerPagaresDeIBS(ByVal pCodigo_proceso As String, ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        Dim iGrupoId As Integer = 14
        Dim iParamId1 As Integer = 30001
        Dim iParamId2 As Integer = 30002
        Dim iParamId3 As Integer = 30003
        Dim iParamId4 As Integer = 30004

        Dim sScript1 As String = String.Empty
        Dim sScript2 As String = String.Empty
        Dim sScript3 As String = String.Empty
        Dim sScript4 As String = String.Empty

        Dim oParametro As clsParametro
        Dim oParametroDO As New clsParametroDO

        oParametro = oParametroDO.ObtenerParametro(iGrupoId, iParamId1)

        If Not oParametro Is Nothing Then
            sScript1 = oParametro.Valor
        End If

        oParametro = oParametroDO.ObtenerParametro(iGrupoId, iParamId2)

        If Not oParametro Is Nothing Then
            sScript2 = oParametro.Valor
        End If

        oParametro = oParametroDO.ObtenerParametro(iGrupoId, iParamId3)

        If Not oParametro Is Nothing Then
            sScript3 = oParametro.Valor
        End If

        oParametro = oParametroDO.ObtenerParametro(iGrupoId, iParamId4)

        If Not oParametro Is Nothing Then
            sScript4 = oParametro.Valor
        End If

        Dim _ds As DataSet = New DataSet()
        _ds = Me.GetClientesDS_SelectTipoCuota(pCodigo_Cliente)
        Dim intTipoCuota As Integer = Conversions.ToInteger(_ds.Tables(0).Rows(0)("TIPO_CUOTA").ToString())
        Dim intMes As String = Conversions.ToString(Interaction.IIf(Strings.Len(Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())) = 1, String.Concat("0", Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())), Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())))

        sScript1 = sScript1.Replace("@pCodigo_proceso", Strings.Trim(pCodigo_proceso))
        sScript1 = sScript1.Replace("@pAnio", pAnio)
        sScript1 = sScript1.Replace("@pMes", pMes)

        sScript3 = sScript3.Replace("@pIntMes", intMes)
        sScript3 = sScript3.Replace("@pAnio", pAnio)

        sScript4 = sScript4.Replace("@pAnio", pAnio)
        'Add 2022-06-15 Completar el mes con dos digitos
        If pMes.Length = 1 Then
            pMes = "0" & pMes
        End If
        'End Add
        sScript4 = sScript4.Replace("@pMes", pMes)
        sScript4 = sScript4.Replace("@pCodigo_Cliente", pCodigo_Cliente)

        lstrsql = sScript1

        If (Not (intTipoCuota = 2 Or intTipoCuota = 3)) Then
            lstrsql = String.Concat(lstrsql, sScript2)
        Else
            lstrsql = String.Concat(lstrsql, sScript3)
        End If

        lstrsql = String.Concat(lstrsql, sScript4)
        'EventLog.WriteEntry("Application", lstrsql, EventLogEntryType.Information)

        'lstrsql = String.Concat("SELECT '", Strings.Trim(pCodigo_proceso), "'  AS Codigo_proceso, e.DLCCC AS DLECC, e.DLAÑO AS DLEAN, e.DLAGC AS DLEAG, e.DLCOC AS DLECO, e.DLCCY AS DLEMO, e.DLACC AS DLENP, e.DLCEM AS DLECM, " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      e.DLNCL AS DLENE, e.DLAPP AS DLEPA, e.DLAPM AS DLEMA, trim(DLPRN) CONCAT ' ' CONCAT trim(DLSGN) AS DLEMN, " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      CASE WHEN TRIM(CAST(DLCCR AS CHARACTER(8)) CONCAT DLPLA CONCAT DLCUS) = '0' THEN '' ELSE CAST(SUBSTRING(TRIM(DLCUS) " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      CONCAT '0000', 1, 4)  CONCAT DLPLA CONCAT CAST(DLCCR AS CHARACTER(8))  AS CHARACTER(20)) END AS DLECR, 2000 + c.DLVCA AS DLEAP, " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      c.DLVCM AS DLEMP, SUM(r.DLIMC - r.DLIPC + r.DLITF) AS DLEIC, e.DLSTS AS DLEST, CAST(YEAR(CURRENT DATE) AS CHARACTER(4)) " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      CONCAT CASE LENGTH(TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(MONTH(CURRENT DATE) " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(MONTH(CURRENT DATE) AS CHARACTER(2)), 3, 2) " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      END CONCAT CASE LENGTH(TRIM(CAST(DAY(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(DAY(CURRENT DATE) " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(DAY(CURRENT DATE) AS CHARACTER(2)), 3, 2) END AS DLEFP, " & vbCrLf & "")
        'Dim strArrays() As String = {lstrsql, "                      CASE WHEN COUNT(1) > 1 THEN 1 ELSE 0 END DLESD, e.DLFLI, COUNT(1) AS NUMCUOTAS,(SELECT  PDEDAP CONCAT '/' CONCAT PDEMAP CONCAT '/' CONCAT PDEAAP FROM PLPDE WHERE PDEACC = E.DLACC ORDER BY PDESEQ  DESC FETCH FIRST 1 ROW ONLY ) AS FechaDesembolso, e.DLOAM as MontoOriginal, CASE WHEN (SELECT DLPPNU  FROM DLPMT WHERE  DLPPFL <> 'P' AND DLPACC = e.DLACC AND DLPPDY = RIGHT(", pAnio, " ,2) AND DLPPDM = ", pMes, ") IS NULL THEN 0 ELSE (SELECT DLPPNU  FROM DLPMT WHERE  DLPPFL <> 'P' AND DLPACC = e.DLACC AND DLPPDY = RIGHT( ", pAnio, " ,2) AND DLPPDM = ", pMes, ") END as CuotasInformada, " & vbCrLf & ""}
        'lstrsql = String.Concat(strArrays)
        'Dim intMes As String = Conversions.ToString(Interaction.IIf(Strings.Len(Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())) = 1, String.Concat("0", Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())), Strings.Trim(_ds.Tables(0).Rows(0)("DLEMEN").ToString())))
        'If (Not (intTipoCuota = 2 Or intTipoCuota = 3)) Then
        '    lstrsql = String.Concat(lstrsql, "                   (SELECT DLPPDD CONCAT '/' CONCAT DLPPDM CONCAT '/' CONCAT DLPPDY FROM DLPMT WHERE DLPACC = E.DLACC AND DLPPFL <> 'P' ORDER BY DLPPNU FETCH FIRST 1 ROW ONLY) AS FechaCargo,")
        'Else
        '    strArrays = New String() {lstrsql, "                   t.dlcxd1 concat '/'  concat ", intMes, " concat '/' concat ", pAnio, " AS FechaCargo,"}
        '    lstrsql = String.Concat(strArrays)
        'End If
        'lstrsql = String.Concat(lstrsql, "                      CASE WHEN t.DLCNC1 IS NULL THEN 0 ELSE t.DLCNC1 END as CuotasPactadas, (SELECT COUNT(*) FROM DLPMT WHERE DLPACC =  e.DLACC AND DLPPFL = 'P'AND DLPPNU <> 999) AS CuotasPagadas, CASE WHEN (t.DLCNC1 - t.DLCCP1) IS NULL THEN 0 ELSE (t.DLCNC1 - (SELECT COUNT(*) FROM DLPMT WHERE DLPACC =  e.DLACC AND DLPPFL = 'P')) END as CuotasPendientes, " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                       (CASE WHEN  (SELECT  COUNT(*) FROM PLPAD WHERE Plcnpg = E.DLACC) = 1 THEN (CASE WHEN (c.DLVCM  CONCAT c.DLVCA) = (SELECT  pldmpv CONCAT pldapv FROM PLPAD WHERE Plcnpg = E.DLACC ORDER BY pldmpv  DESC FETCH FIRST 1 ROW ONLY) THEN 1 ELSE 3 END) ELSE (CASE WHEN (c.DLVCM  CONCAT c.DLVCA) = (SELECT  pldmpv CONCAT pldapv FROM PLPAD WHERE Plcnpg = E.DLACC ORDER BY pldmpv  DESC FETCH FIRST 1 ROW ONLY) THEN 2 ELSE 3 END ) END) AS NroTipoCredito" & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      FROM DLCRE e INNER JOIN" & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "                      INNER JOIN DLEMP D ON (D.DLECUN = E.DLCCC AND" & vbCrLf & "")
        'lstrsql = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(String.Concat(String.Concat(lstrsql, "    (CAST (2000 + R.DLVCA AS CHARACTER (4))  CONCAT CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN   '0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 ))) ELSE   TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) )) END <='"), pAnio), Interaction.IIf(Strings.Len(Strings.Trim(pMes)) = 1, String.Concat("0", Strings.Trim(pMes)), Strings.Trim(pMes))), "')) INNER JOIN DLCNT t ON e.DLACC = t.DLCACC"), "" & vbCrLf & ""))
        'lstrsql = String.Concat(lstrsql, " WHERE     (TRIM(r.DLSTS) = '') ")
        'lstrsql = String.Concat(lstrsql, " AND CAST (2000 + R.DLVCA AS CHARACTER (4)) ")
        'lstrsql = String.Concat(lstrsql, " CONCAT ")
        'lstrsql = String.Concat(lstrsql, " CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN ")
        'lstrsql = String.Concat(lstrsql, " 	'0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 )))")
        'lstrsql = String.Concat(lstrsql, " ELSE")
        'lstrsql = String.Concat(lstrsql, "	TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) ))")
        'lstrsql = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(String.Concat(String.Concat(lstrsql, " END <='"), pAnio), Interaction.IIf(Strings.Len(Strings.Trim(pMes)) = 1, String.Concat("0", Strings.Trim(pMes)), Strings.Trim(pMes))), "'"))
        'lstrsql = String.Concat(lstrsql, "GROUP BY e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, " & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, "e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS, e.DLFLI, e.DLOAM,  t.DLCNC1, t.DLCCP1, t.dlcxd1, t.dlcxm1, t.dlcxy1, t.DLCNC1" & vbCrLf & "")
        'lstrsql = String.Concat(lstrsql, " HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = ", pCodigo_Cliente)
        'lstrsql = String.Concat(lstrsql, " AND CAST ( 2000+c.DLVCA  AS CHARACTER ( 4 ) ) ")
        'lstrsql = String.Concat(lstrsql, " CONCAT ")
        'lstrsql = String.Concat(lstrsql, " CASE WHEN LENGTH(TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN")
        'lstrsql = String.Concat(lstrsql, " 	'0' CONCAT TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))")
        'lstrsql = String.Concat(lstrsql, " ELSE")
        'lstrsql = String.Concat(lstrsql, " 	TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))")
        'lstrsql = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(String.Concat(String.Concat(lstrsql, " END <='"), pAnio), Interaction.IIf(Strings.Len(Strings.Trim(pMes)) = 1, String.Concat("0", Strings.Trim(pMes)), Strings.Trim(pMes))), "'"))


        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function
End Class

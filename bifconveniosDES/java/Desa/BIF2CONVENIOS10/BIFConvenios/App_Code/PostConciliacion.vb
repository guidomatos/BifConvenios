Imports System.Data.SqlClient
Imports GOIntranet
Imports ADODB
Imports System.Data.OleDb

Namespace BIFConvenios
    Public Class PostConciliacion

#Region "Reportes"
        'RESPINOZA 20070906 - Obtiene la informacion del proceso en IBS en funcion a los archivos TRANS Y DEALS
        Public Shared Function getResumenProcesoIBS(ByVal codEmpresa As String, ByVal fechaInicial As String, ByVal fechaFinal As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()

            Dim strSQL As String = "SELECT DISTINCT A.ACMCUN, t.TTRACC, t.TTRACR, SUM(t.TTRAMT) AS TTRAMTS, DLS.DEASTS " + _
                                " FROM TTRAN t INNER JOIN " + _
                                " DEALS DLS ON t.TTRACC = DLS.DEAACC INNER JOIN " + _
                                " ACMST A ON A.ACMACC = DLS.DEARAC " + _
                                " WHERE CAST(2000 + t.TTRBDY AS CHARACTER(4)) CONCAT CASE WHEN LENGTH(TRIM(CAST(t.TTRBDM AS CHARACTER(2)))) " + _
                                " = 1 THEN '0' CONCAT TRIM(CAST(t.TTRBDM AS CHARACTER(2))) ELSE TRIM(CAST(t.TTRBDM AS CHARACTER(2))) " + _
                                " END CONCAT CASE WHEN LENGTH(TRIM(CAST(t.TTRBDD AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(t.TTRBDD AS CHARACTER(2))) " + _
                                " ELSE TRIM(CAST(t.TTRBDD AS CHARACTER(2))) END BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "' AND (DLS.DEATYP = 'CONV') " + _
                                " AND A.ACMCUN = " + codEmpresa + _
                                " GROUP BY A.ACMCUN, t.TTRACC, t.TTRACR, DLS.DEASTS" + _
                                " UNION SELECT DISTINCT A.ACMCUN, t.TRAACR, 0, SUM(t.TRAAMT) AS TTRAMTS, DLS.DEASTS " + _
                                " FROM TRANS t INNER JOIN " + _
                                " DEALS DLS ON t.TRAACR = DLS.DEAACC INNER JOIN " + _
                                " ACMST A ON A.ACMACC = DLS.DEARAC " + _
                                " WHERE CAST(2000 + t.TRABDY AS CHARACTER(4)) CONCAT CASE WHEN LENGTH(TRIM(CAST(t.TRABDM AS CHARACTER(2)))) " + _
                                " = 1 THEN '0' CONCAT TRIM(CAST(t.TRABDM AS CHARACTER(2))) ELSE TRIM(CAST(t.TRABDM AS CHARACTER(2))) " + _
                                " END CONCAT CASE WHEN LENGTH(TRIM(CAST(t.TRABDD AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(t.TRABDD AS CHARACTER(2))) " + _
                                " ELSE TRIM(CAST(t.TRABDD AS CHARACTER(2))) END BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "' AND (DLS.DEATYP = 'CONV') " + _
                                " AND A.ACMCUN = " + codEmpresa + _
                                " AND TRACDE = '3Y' and TRANAR LIKE 'PAGO CUOTA%'" + _
                                " GROUP BY A.ACMCUN, t.TRAACR, 0, DLS.DEASTS"

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(strSQL)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "ResumenTD")
            Return oDs
        End Function


        ' Obtener un resumen de la informacion de los pagos de IBS
        Public Shared Function getResumenPagosIBS(ByVal codEmpresa As String, ByVal fechaInicial As String, ByVal fechaFinal As String, ByVal lote As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()

            Dim strSQL As String = "SELECT  DLEMC, DLCTC, DLCNP, SUM((CASE WHEN DLCCP <> '" + lote + "' THEN DLCIC ELSE 0 END) * (CASE WHEN DLCTY = 'R' THEN - 1 ELSE 1 END)) AS IMPORTE, COUNT(1) AS PAGOS, " + _
                            " SUM(CASE WHEN DLCPP = 'B' THEN DLCIC * (CASE WHEN DLCTY = 'R' THEN - 1 ELSE 1 END) ELSE 0 END) AS PAGOVENTANILLA, " + _
                            " SUM(CASE WHEN DLCPP = 'I' THEN DLCIC * (CASE WHEN DLCTY = 'R' THEN - 1 ELSE 1 END) ELSE 0 END) AS PAGOINTERNET, " + _
                            " SUM(CASE WHEN DLCPP = 'A' THEN(CASE WHEN DLCCP <> '" + lote + "' THEN DLCIC ELSE 0 END) * (CASE WHEN DLCTY = 'R' THEN - 1 ELSE 1 END) ELSE 0 END) AS PAGOIBS, " + _
                            " SUM(CASE WHEN DLCPP = 'A' THEN(CASE WHEN DLCCP = '" + lote + "' THEN DLCIC ELSE 0 END) * (CASE WHEN DLCTY = 'R' THEN - 1 ELSE 1 END) ELSE 0 END) AS PAGOIBSPROCESOCOBRANZA " + _
                            " FROM DLCPG" + _
                            " WHERE     (DLEMC = '" + codEmpresa + "') " + _
                            " AND CAST(2000 + DLCAP AS CHARACTER(4)) CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCMp AS CHARACTER(2)))) = 1 THEN " + _
                            "           '0' CONCAT TRIM(CAST(DLCMP AS CHARACTER(2))) ELSE TRIM(CAST(DLCMP AS CHARACTER(2))) " + _
                            " END CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCdp AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(DLCdp AS CHARACTER(2))) " + _
                            " ELSE TRIM(CAST(DLCdp AS CHARACTER(2))) END BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "' " + _
                            " GROUP BY DLEMC, DLCTC, DLCNP " + _
                            " ORDER BY dlcnp "

            'RESPINOZA 20070522 - Se remplazo condicion DLCCP <> '6065' por condiciones para permitir obtener la informacion procesada por la cobranza 
            'RESPINOZA 20061104 - Se agrego condicion AND DLCCP <> '6065' para evitar que se muestren los pagos realizados desde el sistema
            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(strSQL)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "ResumenDLCPG")
            'oDs.Tables(0).DefaultView().RowFilter()
            Return oDs
        End Function

#End Region


        'Obtener informacion del detalle de los pagos de IBS
        Public Shared Function getDetallePagosIBS(ByVal codEmpresa As String, ByVal numeroPagare As String, ByVal fechaInicial As String, ByVal fechaFinal As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()
            Dim strSQL As String = "SELECT     DLEMC, DLCTC, DLCCL, DLCCY, DLCNP, DLNCT, DLDSC, DLCAV, DLCMV, DLCDV, DLCAP, DLCMP, DLCDP, DLCIC, DLCCP, DLCTY,  " + _
                          " CAST(2000 + DLCAP AS CHARACTER(4)) CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCMp AS CHARACTER(2))))  " + _
                          " = 1 THEN '0' CONCAT TRIM(CAST(DLCMp AS CHARACTER(2))) ELSE TRIM(CAST(DLCMp AS CHARACTER(2)))  " + _
                          " END CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCdp AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(DLCdp AS CHARACTER(2)))  " + _
                          " ELSE TRIM(CAST(DLCdp AS CHARACTER(2))) END AS fecha, CONCAT(DLCCP,  " + _
                          " CASE WHEN DLCCP = '1000' THEN ' PAGO POR BRANCH' WHEN DLCCP = '5555' THEN ' PAGO POR INTERNET' WHEN DLCCP = '9480' THEN ' LOTE AUTOMATICO SISTEMA' " + _
                          " WHEN DLCCP = '6065' THEN ' LOTE AUTOMATICO ESPECIAL' WHEN DLCCP >= '0662' OR  " + _
                          " DLCCP <= '0688' THEN ' LOTE DE USUARIO MANUAL' ELSE DLCCP END) AS MOTIVO,  " + _
                          " CASE WHEN DLCTY = 'P' THEN 'PAGADA' WHEN DLCTY = 'R' THEN 'REVERSO o EXTORNO' END AS TIPOCUOTA, DLCPP, " + _
                          " CASE WHEN DLCPP = 'B' THEN 'PAGO POR BRANCH' WHEN DLCPP = 'I' THEN 'PAGO POR INTERNET' WHEN DLCPP = 'A' THEN 'PAGO POR IBS-AUTOMATICO Y LOTES' " + _
                          "  ELSE DLCPP END AS PROCEDENCIA" + _
                          " FROM DLCPG  " + _
                          " WHERE     (DLEMC = '" + codEmpresa + "') AND (DLCNP = '" + numeroPagare + "') AND CAST(2000 + DLCAP AS CHARACTER(4))  " + _
                          " CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCMp AS CHARACTER(2)))) = 1 THEN '0' CONCAT TRIM(CAST(DLCMp AS CHARACTER(2)))  " + _
                          " ELSE TRIM(CAST(DLCMp AS CHARACTER(2))) END CONCAT CASE WHEN LENGTH(TRIM(CAST(DLCdp AS CHARACTER(2))))  " + _
                          " = 1 THEN '0' CONCAT TRIM(CAST(DLCdp AS CHARACTER(2))) ELSE TRIM(CAST(DLCdp AS CHARACTER(2))) END BETWEEN '" + fechaInicial + "' AND '" + fechaFinal + "'  "

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(strSQL)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "DetalleDLCPG")
            Return oDs
        End Function


    End Class
End Namespace
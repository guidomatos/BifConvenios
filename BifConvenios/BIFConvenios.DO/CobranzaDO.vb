Imports ADODB
Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports IBM.Data.DB2
Public Class CobranzaDO
    'Me.cUtils = New BIFUtils.Utils()
    Private cUtils As WS.Utils = New WS.Utils
    Private conexionIBS As String
    Private conexionConvenios As String
    ' Methods
    Public Sub New()
        MyBase.New()
        Dim lobj As New BIFConvenios.BE.conexion
        'Me.conexionIBS = lobj.CadenaConexionIBS
        Me.conexionIBS = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
        Me.conexionConvenios = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Sub

    Public Function ActivarPagoIBSOnline(ByVal strTipoOperacion As String, ByVal strCodigoClienteIBS As String, ByVal strFecha As String) As Integer
        Dim num As Integer
        Dim connection As Connection = New ConnectionClass
        Dim recordset As Recordset = New RecordsetClass
        Dim adapter As New OleDbDataAdapter
        'Dim set As New DataSet
        Dim str3 As String = ConfigurationManager.AppSettings("ProgramaPagoOnline")
        Try
            connection.CursorLocation = CursorLocationEnum.adUseClient
            connection.Open(Me.conexionIBS, "", "", -1)
            connection.Execute(str3.Replace("TipoOperacion", strTipoOperacion).Replace("CodigoClienteIBS", strCodigoClienteIBS).Replace("Fecha", strFecha), Missing.Value, -1).ActiveConnection = Nothing
            connection.Close()
            connection = Nothing
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return num
    End Function

    Public Sub ActualizaCobranza(ByVal pCodigo_proceso As String, ByVal pDLNP As String, ByVal pEstado As String)
        Dim lconn As SqlConnection = New SqlConnection(Me.conexionConvenios)
        Dim lcmd As SqlCommand = New SqlCommand()
        lconn.Open()
        lcmd.CommandType = CommandType.Text
        Dim strsql As Object = ""
        Dim strArrays() As String = {"UPDATE clientecuota SET Estado = '", pEstado, "' where Codigo_proceso ='", pCodigo_proceso, "' and DLNP = '", pDLNP, "'"}
        strsql = String.Concat(strArrays)
        lcmd = New SqlCommand(Conversions.ToString(strsql), lconn)
        lcmd.ExecuteNonQuery()
        lconn.Close()
    End Sub

    Private Sub AgregarParametro(ByRef cmd As SqlCommand, ByVal nombreParam As String, ByVal direccionParam As ParameterDirection, ByVal tipoParam As DbType, ByVal valorParam As Object)
        Dim parameter As IDbDataParameter = cmd.CreateParameter
        parameter.ParameterName = nombreParam
        parameter.DbType = tipoParam
        parameter.Direction = direccionParam
        parameter.Value = valorParam
        cmd.Parameters.Add(parameter)
    End Sub

    Private Sub AgregarParametroOledb(ByRef cmd As OleDbCommand, ByVal nombreParam As String, ByVal direccionParam As ParameterDirection, ByVal tipoParam As DbType, ByVal valorParam As Object)
        Dim parameter As IDbDataParameter = cmd.CreateParameter
        parameter.ParameterName = nombreParam
        parameter.DbType = tipoParam
        parameter.Direction = direccionParam
        parameter.Value = valorParam
        cmd.Parameters.Add(parameter)
    End Sub

    Public Sub AnulaCobranza(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC AnulaProcesoArchivoDescuentos '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(String.Concat(strArray), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub ArchivoDescuentosInserta(ByVal pCodigo_proceso As String, ByVal ldr As DataRow)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand("INSERT INTO ArchivoDescuentos (CodigoBanco, Moneda, NumeroPagare, CodigoModular, NombreTrabajador, CodigoReferencia, Anio, Mes, Cuota, SituacionLaboral, MontoDescuento, Estado, Codigo_proceso) VALUES (@CodigoBanco, @Moneda, @NumeroPagare, @CodigoModular, @NombreTrabajador, @CodigoReferencia, @Anio, @Mes, @Cuota, @SituacionLaboral, @MontoDescuento, @Estado, @Codigo_proceso) ", connection)
        cmd.CommandType = CommandType.Text
        Me.AgregarParametro(cmd, "@CodigoBanco", ParameterDirection.Input, DbType.String, ldr("CodigoBanco").ToString)
        Me.AgregarParametro(cmd, "@Moneda", ParameterDirection.Input, DbType.String, ldr("Moneda").ToString)
        Me.AgregarParametro(cmd, "@NumeroPagare", ParameterDirection.Input, DbType.String, ldr("NumeroPagare").ToString)
        Me.AgregarParametro(cmd, "@CodigoModular", ParameterDirection.Input, DbType.String, ldr("CodigoModular").ToString)
        Me.AgregarParametro(cmd, "@NombreTrabajador", ParameterDirection.Input, DbType.String, ldr("NombreTrabajador").ToString)
        Me.AgregarParametro(cmd, "@CodigoReferencia", ParameterDirection.Input, DbType.String, ldr("CodigoReferencia").ToString)
        Me.AgregarParametro(cmd, "@Anio", ParameterDirection.Input, DbType.String, ldr("Anio").ToString)
        Me.AgregarParametro(cmd, "@Mes", ParameterDirection.Input, DbType.String, ldr("Mes").ToString)
        Me.AgregarParametro(cmd, "@Cuota", ParameterDirection.Input, DbType.String, ldr("Cuota").ToString)
        Me.AgregarParametro(cmd, "@SituacionLaboral", ParameterDirection.Input, DbType.String, ldr("SituacionLaboral").ToString)
        Me.AgregarParametro(cmd, "@MontoDescuento", ParameterDirection.Input, DbType.String, Interaction.IIf((ldr("MontoDescuento").ToString.Trim = ""), 0, ldr("MontoDescuento").ToString))
        Me.AgregarParametro(cmd, "@Estado", ParameterDirection.Input, DbType.String, "")
        Me.AgregarParametro(cmd, "@Codigo_proceso", ParameterDirection.Input, DbType.String, pCodigo_proceso)
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub EliminaCobranzaEnIBS(ByVal pCobranza As DataRow)
        Dim connection As New OleDbConnection(Me.conexionIBS)
        Dim command As New OleDbCommand(Conversions.ToString("DELETE FROM DLREC WHERE DLRAG = ? AND DLRAN = ? AND DLRAP = ? AND DLRCC = ? AND DLRCM = ? AND DLRCO = ? AND DLRCR = ? AND DLRER = ? AND DLRFP = ? AND DLRIC = ? AND DLRID = ? AND DLRMO = ? AND DLRMP = ? AND DLRNE = ? AND DLRNP = ? AND DLRST = ?"), connection)
        command.CommandType = CommandType.Text
        command.Parameters.AddWithValue("DLRAG", pCobranza("DLAG"))
        command.Parameters.AddWithValue("DLRAN", pCobranza("DLAN"))
        command.Parameters.AddWithValue("DLRAP", pCobranza("DLAP"))
        command.Parameters.AddWithValue("DLRCC", pCobranza("DLCC"))
        command.Parameters.AddWithValue("DLRCM", pCobranza("DLCM"))
        command.Parameters.AddWithValue("DLRCO", pCobranza("DLCO"))
        command.Parameters.AddWithValue("DLRCR", pCobranza("DLCR"))
        command.Parameters.AddWithValue("DLRER", pCobranza("DLER"))
        command.Parameters.AddWithValue("DLRFP", pCobranza("DLFP"))
        command.Parameters.AddWithValue("DLRIC", pCobranza("DLIC"))
        command.Parameters.AddWithValue("DLRID", pCobranza("DLID"))
        command.Parameters.AddWithValue("DLRMO", pCobranza("DLMO"))
        command.Parameters.AddWithValue("DLRMP", pCobranza("DLMP"))
        command.Parameters.AddWithValue("DLRNE", pCobranza("DLNE"))
        command.Parameters.AddWithValue("DLRNP", pCobranza("DLNP"))
        command.Parameters.AddWithValue("DLRST", pCobranza("DLST"))
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub EliminaCobranzaMasivaEnIBS(ByVal pCodigo_ClienteIBS As String, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String)
        Dim connection As Connection = New ConnectionClass
        connection.CursorLocation = CursorLocationEnum.adUseClient
        connection.Open(Me.conexionIBS, "", "", -1)
        connection.Execute(String.Concat(New String() {"delete from DLREC where DLRCC =", pCodigo_ClienteIBS, " AND DLRAP = ", pAnio, " AND DLRMP = ", pMes, " AND DLRFP = ", pFecha_ProcesoAS400}), Missing.Value, -1)
        connection.Close()
    End Sub

    Public Sub ImportaDescuentosClientePrepara(ByVal pCodigo_proceso As String, ByVal pFechaFormateada As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC ProcesaDatosArchivos '", pCodigo_proceso, "','", pFechaFormateada, "'"}
        Dim command As New SqlCommand(String.Concat(strArray), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub ImportaDescuentosClienteProcesa(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC ProcesaArchivoDescuentoDefault '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(String.Concat(strArray), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub InsertaCobranzaEnIBS(ByVal pCobranza As DataRow)
        Dim connection As New OleDbConnection(Me.conexionIBS)
        Dim command As New OleDbCommand(Conversions.ToString("INSERT INTO DLREC (DLRAG, DLRAN, DLRAP, DLRCC, DLRCM, DLRCO, DLRCR, DLRER, DLRFP, DLRIC, DLRID, DLRMO, DLRMP, DLRNE, DLRNP, DLRST,DLENL) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?,?)"), connection)
        command.CommandType = CommandType.Text
        command.Parameters.AddWithValue("DLRAG", pCobranza("DLAG"))
        command.Parameters.AddWithValue("DLRAN", pCobranza("DLAN"))
        command.Parameters.AddWithValue("DLRAP", pCobranza("DLAP"))
        command.Parameters.AddWithValue("DLRCC", pCobranza("DLCC"))
        command.Parameters.AddWithValue("DLRCM", pCobranza("DLCM"))
        command.Parameters.AddWithValue("DLRCO", pCobranza("DLCO"))
        command.Parameters.AddWithValue("DLRCR", pCobranza("DLCR"))
        command.Parameters.AddWithValue("DLRER", pCobranza("DLER"))
        command.Parameters.AddWithValue("DLRFP", pCobranza("DLFP"))
        command.Parameters.AddWithValue("DLRIC", pCobranza("DLIC"))
        command.Parameters.AddWithValue("DLRID", pCobranza("DLID"))
        command.Parameters.AddWithValue("DLRMO", pCobranza("DLMO"))
        command.Parameters.AddWithValue("DLRMP", pCobranza("DLMP"))
        command.Parameters.AddWithValue("DLRNE", pCobranza("DLNE"))
        command.Parameters.AddWithValue("DLRNP", pCobranza("DLNP"))
        command.Parameters.AddWithValue("DLRST", pCobranza("DLST"))
        command.Parameters.AddWithValue("DLENL", pCobranza("DLENL"))
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub InsertaNominaEntradaSalida(ByVal Codigo_proceso As String, ByVal cantidad_clientes As Integer, ByVal monto_total_soles As Double, ByVal monto_total_dolares As Double, ByVal tipo_nomina As String, ByVal tipo_formato As String, ByVal tipo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC InsertaNominaEntradaSalida '", Codigo_proceso, "','", Conversions.ToString(cantidad_clientes), "','", Conversions.ToString(monto_total_soles), "','", Conversions.ToString(monto_total_dolares), "','"}
        strArray(9) = tipo_nomina
        strArray(10) = "','"
        strArray(11) = tipo_formato
        strArray(12) = "','"
        strArray(13) = tipo_proceso
        strArray(14) = "','"
        strArray(15) = pUsuario
        strArray(&H10) = "'"
        Dim command As New SqlCommand(String.Concat(strArray), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Function ObtieneCobranzas(ByVal pCodigo_proceso As String) As DataTable
        Dim lds As DataSet = New DataSet()
        Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat("EXEC EnvioDescuentos_AS400 '", pCodigo_proceso, "'"), Me.conexionConvenios)
        lda.Fill(lds)
        Return lds.Tables(0)
    End Function

    Public Sub TemporalArchivoTextoInserta(ByVal pCodigo_proceso As String, ByVal pUsuario As String, ByVal pNroLinea As Integer, ByVal pData As String, ByVal pFechaFormateada As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "addInformacionArchivoTexto"
        Me.AgregarParametro(cmd, "@Codigo_proceso", ParameterDirection.Input, DbType.String, pCodigo_proceso)
        Me.AgregarParametro(cmd, "@UserId", ParameterDirection.Input, DbType.String, pUsuario)
        Me.AgregarParametro(cmd, "@orden", ParameterDirection.Input, DbType.Int32, pNroLinea)
        Me.AgregarParametro(cmd, "@lineainformacion", ParameterDirection.Input, DbType.String, pData)
        Me.AgregarParametro(cmd, "@dateCode", ParameterDirection.Input, DbType.String, pFechaFormateada)
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
    End Sub
End Class

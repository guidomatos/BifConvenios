Imports BIFUtils
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Public Class ProcesoDO
    ' Fields
    Private cUtils As WS.Utils = New WS.Utils
    Private conexionIBS As String
    Private conexionConvenios As String

    Public Sub New()
        MyBase.New()
        'Me.cUtils = New WS.Utils()
        Dim lobj As BIFConvenios.BE.conexion = New BIFConvenios.BE.conexion
        Me.conexionIBS = lobj.CadenaConexionIBS
        Me.conexionConvenios = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Sub
    Public Sub ActualizaGeneracionArchivo(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC GetFinalGeneracionArchivo '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Function AdicionaProceso(ByVal pCodigo_Cliente As Integer, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String, ByVal pUsuario As String) As String
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand
        cmd.Connection = connection
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "AddProceso"
        Me.AgregarParametro(cmd, "@Codigo_Cliente", ParameterDirection.Input, DbType.Int32, pCodigo_Cliente)
        Me.AgregarParametro(cmd, "@Anio_periodo", ParameterDirection.Input, DbType.String, pAnio)
        Me.AgregarParametro(cmd, "@Mes_Periodo", ParameterDirection.Input, DbType.String, pMes)
        Me.AgregarParametro(cmd, "@Fecha_ProcesoAS400", ParameterDirection.Input, DbType.String, pFecha_ProcesoAS400)
        Me.AgregarParametro(cmd, "@usuario", ParameterDirection.Input, DbType.String, pUsuario)
        connection.Open()
        Dim str2 As String = Conversions.ToString(cmd.ExecuteScalar)
        connection.Close()
        Return str2
    End Function

    Private Sub AgregarParametro(ByRef cmd As SqlCommand, ByVal nombreParam As String, ByVal direccionParam As ParameterDirection, ByVal tipoParam As DbType, ByVal valorParam As Object)
        Dim parameter As IDbDataParameter = cmd.CreateParameter
        parameter.ParameterName = nombreParam
        parameter.DbType = tipoParam
        parameter.Direction = direccionParam
        parameter.Value = valorParam
        cmd.Parameters.Add(parameter)
    End Sub

    Public Sub FinalizaEnvioCobranza(ByVal pCodigo_proceso As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim command As New SqlCommand(Conversions.ToString(("EXEC FinalizadoEnvioInformacionAS400 '" & pCodigo_proceso & "',''")), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub FinalizaGeneracionArchivo(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC UpdEstadoGeneracionExito '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub InicioDescuentoEmpresa(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC IniciaProcesoCargaDescuentos '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Sub InicioEnvioCobranza(ByVal pCodigo_proceso As String, ByVal pUsuario As String)
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC InicioEnvioInformacionAS400 '", pCodigo_proceso, "','", pUsuario, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
    End Sub

    Public Function ObtieneNombreArchivoProceso(ByVal pCodigo_proceso As String, ByVal lFormatoArchivo As String) As String
        Dim connection As New SqlConnection(Me.conexionConvenios)
        'Dim command As New SqlCommand(Conversions.ToString(("EXEC GetNombreArchivoProceso '" & pCodigo_proceso & "'")), connection)
        Dim command As New SqlCommand(Conversions.ToString(("EXEC GetNombreArchivoProceso '" & pCodigo_proceso & "','" & lFormatoArchivo & "'")), connection)
        Command.CommandType = CommandType.Text
        connection.Open()
        Dim str As String = Conversions.ToString(command.ExecuteScalar)
        connection.Close()
        Return str
    End Function

    Public Function ObtieneProcesoCliente(ByVal pCodigo_proceso As String) As DataTable
        Dim ldtResultado As New DataTable

        ldtResultado.Columns.Add("Nombre_Cliente")
        ldtResultado.Columns.Add("Anio_periodo")
        ldtResultado.Columns.Add("Mes_Periodo")
        ldtResultado.Columns.Add("Fecha_ProcesoAS400")
        ldtResultado.Columns.Add("TipoDocumento")
        ldtResultado.Columns.Add("NumeroDocumento")

        Dim lconn As New SqlConnection(conexionConvenios)
        Dim lcmd As SqlCommand
        Dim lstrsql = "EXEC GetInfoProcesoCliente '" & pCodigo_proceso & "'"
        'Dim lResult As String

        lcmd = New SqlCommand(lstrsql, lconn)
        lcmd.CommandType = CommandType.Text
        lconn.Open()
        Dim reader As SqlDataReader = lcmd.ExecuteReader()

        Dim ldr As DataRow
        While reader.Read()
            ldr = ldtResultado.NewRow()
            ldr("Nombre_Cliente") = reader(0).ToString()
            ldr("Anio_periodo") = reader(1).ToString()
            ldr("Mes_Periodo") = reader(2).ToString()
            ldr("Fecha_ProcesoAS400") = reader(3).ToString()
            ldr("TipoDocumento") = reader(4).ToString()
            ldr("NumeroDocumento") = reader(5).ToString()
            If Not ldr Is Nothing Then
                ldtResultado.Rows.Add(ldr)
            End If
        End While
        lconn.Close()
        Return ldtResultado

        'Dim lds As DataSet = New DataSet()
        'Dim lda As SqlDataAdapter = New SqlDataAdapter(String.Concat("EXEC GetInfoProcesoCliente '", pCodigo_proceso, "'"), Me.conexionConvenios)
        'lda.Fill(lds)
    End Function
End Class

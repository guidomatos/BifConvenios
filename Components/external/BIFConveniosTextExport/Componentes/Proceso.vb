Imports System.Data.SqlClient
Imports BroadcasterClass.GOIntranet
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB


Public Class Proceso

    Public Function GetDBConnectionString() As String
        Return BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Function

    'Obtenemos el nombre del archivo de proceso
    Public Function GetNombreArchivoProceso(ByVal Codigo_proceso As String) As String
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso})

        myConnection.Open()
        returnValue = myCommand.ExecuteScalar
        myConnection.Close()
        Return returnValue
    End Function


    'Obtenemos el nombre del archivo de proceso
    Private Function GetNombreFormatoArchivo(ByVal Codigo_proceso As String) As String
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso})

        myConnection.Open()
        returnValue = myCommand.ExecuteScalar
        myConnection.Close()
        Return returnValue
    End Function


#Region "Metodos para generacion de cronograma futuro"
    'Obtenemos la informacion del archivo desde la base de datos
    Public Function CronogramaFuturoDefault(ByVal Codigo_proceso As String, ByVal situacionTrabajador As String) As SqlDataReader
        Dim NombreFormatoArchivo As String = ""
        'Obtenemos el nombre del formato de archivo que debe generarse
        'NombreFormatoArchivo = GetNombreFormatoArchivo(Codigo_proceso)
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, situacionTrabajador})
        myCommand.CommandTimeout = 10000000
        myConnection.Open()
        Dim returnValue As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        ' myConnection.Close()
        Return returnValue
    End Function

    'Obtenemos la informacion del archivo desde la base de datos
    Public Function CronogramaFuturo(ByVal Codigo_proceso As String, ByVal situacionTrabajador As String) As SqlDataReader
        Dim NombreFormatoArchivo As String = ""
        'Obtenemos el nombre del formato de archivo que debe generarse
        NombreFormatoArchivo = GetNombreFormatoArchivo(Codigo_proceso)
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, situacionTrabajador}, CommandType.StoredProcedure, "CronogramaFuturo" + NombreFormatoArchivo)
        myCommand.CommandTimeout = 10000000
        myConnection.Open()
        Dim returnValue As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        ' myConnection.Close()
        Return returnValue
    End Function


    'Obtenemos la informacion del archivo desde la base de datos
    Public Function CronogramaFuturoTexto(ByVal Codigo_proceso As String, ByVal situacionTrabajador As String) As SqlDataReader
        Dim NombreFormatoArchivo As String = ""
        'Obtenemos el nombre del formato de archivo que debe generarse
        NombreFormatoArchivo = GetNombreFormatoArchivo(Codigo_proceso)
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, situacionTrabajador}, CommandType.StoredProcedure, "CronogramaFuturo" + NombreFormatoArchivo + "Text")
        myCommand.CommandTimeout = 10000000
        myConnection.Open()
        Dim returnValue As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return returnValue
    End Function

    'Obtenemos la informacion del archivo desde la base de datos
    'REspinoza 20070613 - Adicionado para poder obtener mas formatos de archivos por cliente, incluyendo DBF
    Public Function CronogramaFuturoExecutor(ByVal Codigo_proceso As String, ByVal situacionTrabajador As String, ByVal formato As String) As SqlDataReader
        Dim NombreFormatoArchivo As String = ""
        'Obtenemos el nombre del formato de archivo que debe generarse
        Dim myConnection As New SqlConnection(GetDBConnectionString)

        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, situacionTrabajador, formato})
        myCommand.CommandTimeout = 10000000
        myConnection.Open()
        Dim returnValue As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return returnValue
    End Function
#End Region



    'Estable exito en el proceso del archivo
    Public Function UpdEstadoGeneracionExito(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function


    'Estable error en la generacion del archivo
    Public Function UpdEstadoGeneracionError(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function


    'Estable ERROR en la carga de archivo de descuentos
    Public Function UpdErrorCargaArchivoDescuentos(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function

#Region "Envio de la información de descuentos a AS/400"
    'Estable exito en el proceso del archivo
    Public Function InicioEnvioInformacionAS400(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function

    Public Function FinalizadoEnvioInformacionAS400(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function

    Public Function ErrorEnvioInformacionAS400(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function


    'Obtenemos la informacion del mes y año del proceso 
    Private Function GetInfoProcesoCliente(ByVal Codigo_proceso As String) As SqlDataReader
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso})

        myConnection.Open()
        Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Return result
    End Function

    'Metodo para devolver el mes y el año del proceso
    Public Sub getProcesoMesAnio(ByVal codigo_proceso As String, ByRef mes As String, ByRef anio As String, _
                                    ByRef Fecha_ProcesoAS400 As String, ByRef TipoDocumento As String, _
                                    ByRef NumeroDocumento As String)
        Dim dr As SqlDataReader = Me.GetInfoProcesoCliente(codigo_proceso)
        If dr.Read Then
            mes = CType(dr("Mes_Periodo"), String)
            anio = CType(dr("Anio_periodo"), String)
            TipoDocumento = CType(dr("TipoDocumento"), String)
            NumeroDocumento = CType(dr("NumeroDocumento"), String)
            Fecha_ProcesoAS400 = CType(dr("Fecha_ProcesoAS400"), String)
        End If
    End Sub



    'Obtener el CUSCUN (CUSTOMER NUMBER) desde el servidor AS/400
    Public Function GetCustomerNumber(ByVal tipoDocumento As String, ByVal NumeroDocumento As String) As String
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim returnValue As String = ""

        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
        'BIFCYFILES.
        result = myConnection.Execute("SELECT CUSCUN FROM CUMST WHERE CUSTID  ='" & tipoDocumento.Trim & "' AND CUSIDN = '" & NumeroDocumento.Trim & "'")

        result.ActiveConnection = Nothing
        myConnection.Close()
        myConnection = Nothing


        If Not result.BOF Or Not result.EOF Then
            result.MoveFirst()
            returnValue = CType(result(0).Value, String)
        End If
        Return returnValue
    End Function



    'Obtener el numero de registros en AS/400
    Protected Function GetRegistrosRecibidos(ByVal CodigoCliente As String, ByVal AnioProceso As String, ByVal MesProceso As String, ByVal FechaProcesoAS400 As String) As Integer
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim returnValue As Integer
        Dim strsql As String = ""

        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

        strsql = "SELECT COUNT(1) FROM DLREC WHERE DLRCC =" & CodigoCliente _
                                        & " AND DLRAP = " & AnioProceso & " AND DLRMP = " & _
                                        MesProceso & " and DLRFP = " & _
                                        DatePart(DateInterval.Year, Now).ToString & _
                                        IIf(DatePart(DateInterval.Month, Now) < 10, "0" & DatePart(DateInterval.Month, Now).ToString(), DatePart(DateInterval.Month, Now).ToString()) & _
                                        IIf(DatePart(DateInterval.Day, Now) < 10, "0" & DatePart(DateInterval.Day, Now).ToString(), DatePart(DateInterval.Day, Now).ToString())

        result = myConnection.Execute(strsql)

        '20050105 - Se cambia por que la fecha de proceso puede ser diferente a la fecha de envio 
        '& FechaProcesoAS400)

        result.ActiveConnection = Nothing
        myConnection.Close()
        myConnection = Nothing

        result.MoveFirst()
        If Not result.BOF Or Not result.EOF Then
            returnValue = CType(result(0).Value, Integer)
        End If
        Return returnValue
    End Function

    'Obtenemos el numero de registros en SQL Server 
    Protected Function GetCountRegistrosEnviados(ByVal codigo_proceso As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso})

        myConnection.Open()
        returnValue = CType(myCommand.ExecuteScalar, Integer)
        myConnection.Close()
        Return returnValue
    End Function

    'Establecemos si tenemos el mismo numero de registros en ambos origenes
    Public Function MatchCountRecords(ByVal codigo_proceso As String, ByVal CodigoCliente As String, _
                            ByVal AnioProceso As String, ByVal MesProceso As String, _
                            ByVal FechaProcesoAS400 As String) As Boolean
        Dim returnValue As Boolean = False
        If Me.GetRegistrosRecibidos(CodigoCliente, AnioProceso, MesProceso, FechaProcesoAS400) = Me.GetCountRegistrosEnviados(codigo_proceso) Then
            returnValue = True
        End If
        Return returnValue
    End Function


    'Estable exito en el proceso del archivo
    Public Function AnulaProcesoArchivoDescuentos(ByVal codigo_proceso As String, _
                                                ByVal usuario As String) As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigo_proceso, usuario})

        myCommand.CommandTimeout = 600000
        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function


#End Region
End Class
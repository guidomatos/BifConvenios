Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB

Namespace BIFConvenios

    Public Class Proceso

#Region "Actualizar estado"
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


#End Region

#Region "Proceso de la información básica"


        'Adiciona los datos del proceso
        Public Function AddProceso(ByVal Codigo_Cliente As Integer, ByVal Anio_periodo As String, ByVal Mes_Periodo As String, ByVal Fecha_ProcesoAS400 As String, ByVal usuario As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, Anio_periodo, Mes_Periodo, _
                                Fecha_ProcesoAS400, usuario})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar(), String)
            myConnection.Close()
            Return returnValue
        End Function

        'Eliminacion de la informacion de un proceso
        Public Function DelInfoProceso(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
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


        'Eliminacion de un proceso
        Public Function DelProceso(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
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



        'Adiciona los datos del proceso
        Public Function EsperaFinalProceso(ByVal Codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar(), Boolean)
            myConnection.Close()
            Return returnValue
        End Function

        'Adiciona los datos del proceso
        Public Function FinalProcesoCargaDescuentos(ByVal Codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar(), Boolean)
            myConnection.Close()
            Return returnValue
        End Function

        'Obtenemos el estado del proceso que se esta realizando 
        Public Function GetEstadoProceso(ByVal Codigo_proceso As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar(), String)
            myConnection.Close()
            Return returnValue
        End Function


        'Obtenemos un reader con los mensajes de error
        Private Function GetMensajeEstado(ByVal CodigoId As String, ByVal Tabla As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {CodigoId, Tabla})

            myConnection.Open()
            Dim returnValue As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return returnValue
        End Function

        Public Sub GetMensaje(ByVal CodigoEstado As String, ByVal NombreTabla As String, ByRef NombreEstado As String, ByRef DescripcionEstado As String)
            Dim dr As SqlDataReader = GetMensajeEstado(CodigoEstado, NombreTabla)
            If dr.Read Then
                NombreEstado = CType(dr("NombreEstado"), String)
                DescripcionEstado = CType(dr("DescripcionEstado"), String)
            End If
        End Sub

#End Region

        'Obtenemos la informacion de las uges que participan en este convenio
        Public Shared Function getUGES(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la informacion acerca del año del proceso
        Public Function GetAnioProceso() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtener la informacion del proceso 
        Public Function GetInfoPagoNoPago(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtener la informacion de estados del trabajador
        Public Function GetEstadosTrabajador(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        Public Function GetModalidad(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'obtenemos la informacion de un cliente usando un DS
        Public Function GetProcesosRealizados() As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Procesos")
            myConnection.Close()
            Return oDS
        End Function


        'Obtenemos la informacion acerca del año del proceso
        Public Function GetResumenProcesoDescuentos(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtenemos la informacion acerca del año del proceso
        Public Function GetAnioProcesoEspera() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la informacion acerca del año del proceso
        Public Function InformeProceso(ByVal codigo_proceso) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer            

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result


        End Function


        'Obtener la informacion de resumen del proceso
        Public Function GetResumenProceso(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtenemos la informacion acerca de los meses del proceso
        Public Function GetMesesProceso(ByVal anio As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la informacion acerca de los meses del proceso
        Public Function GetMesesProcesoEspera(ByVal anio As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtiene la informacion de los procesos realizados en un mes y año
        Public Function GetProcesos(ByVal anio_periodo As String, ByVal mes_periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio_periodo, mes_periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtiene el nombre del formato de exportacion del archivo creado 
        Public Shared Function getFormatoExportacion(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            'returnValue = CType(myCommand.ExecuteScalar(), String)
            'myConnection.Close()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            'Return returnValue
            Return result
        End Function



        'Obtiene la informacion de los procesos realizados en un mes y año
        Public Function GetInfoProceso(ByVal Codigo_Cliente As String, ByVal Anio_periodo As String, _
                                    ByVal Mes_Periodo As String, ByVal Fecha_ProcesoAS400 As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, Anio_periodo, Mes_Periodo, Fecha_ProcesoAS400})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Verifica si eXiste la informacion de un proceso
        Public Function ExistsProcess(ByVal Codigo_Cliente As String, ByVal anio_periodo As String, ByVal mes_periodo As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, anio_periodo, mes_periodo})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function


        'Verifica si eXiste la informacion de un proceso
        Public Function ArchivoDescuentosEnProceso() As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

        'Verifica si el flag de carga automatica se encuentra activado
        Public Function ConsultaFlagCargaAutomatica() As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

        'Actualiza el flag carga 
        Public Function ActualizaFlagCargaAutomatica(ByVal tipo As String, ByVal flag As Integer) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {tipo, flag})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function
        'Obtenemos la informacion para la carga de pagos IBS
        Public Function getDatosPagosIBSOnline(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtiene la verificacion del final de la generacion de un archivo
        Public Function GetFinalGeneracionArchivo(ByVal Codigo_proceso As String, ByVal usuario As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

        'Obtiene UN VALOR que indica si debe realizarse el requerimiento de generacion del archivo de proceso
        Public Function EnviarMensajeGeneracionArchivo(ByVal Codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

#Region "Funciones para el acceso a los archivos de proceso"
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

        'Obtenemos el valor asociado al formato
        Public Function getValorAsociadoFormato(ByVal Codigo_proceso As String, ByVal FormatoArchivo As String, ByVal TipoFormatoArchivo As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, FormatoArchivo, TipoFormatoArchivo})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function


#End Region

        Public Function ProcesandoCargaCronogramaFuturo() As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

        'Obtenemos la informacion del resultado del proceso
        Public Function GetRegistrosResultadoProceso(ByVal codigo_proceso As String, ByVal Documento As String, ByVal DLNE As String, ByVal DLNP As Decimal, ByVal EstadoTrabajador As String, ByVal ZonaUse As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, Documento, DLNE, DLNP, EstadoTrabajador, ZonaUse})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myConnection.Close()
            Return oDS
        End Function


        Public Function ActualizaProcesoCliente(ByVal codigo_proceso As String, ByVal codigo_pagare As String, ByVal nuevo_importe As Double, _
                         ByVal log_mensaje As String, ByVal log_usuario As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, codigo_pagare, nuevo_importe, log_mensaje, log_usuario})


            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myConnection.Close()
            Return oDS
        End Function


        'Obtenemos la informacion del resultado del proceso
        Public Function GetRegistrosResultadoProcesoDescuentos(ByVal codigo_proceso As String, ByVal codigo As String, ByVal dlne As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, codigo, dlne})

            myCommand.CommandTimeout = 150000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myConnection.Close()
            myConnection.Dispose()
            myConnection = Nothing
            Return oDS
        End Function


        'Obtenemos la informacion del resultado del proceso - solo registros validos
        Public Function GetRegistrosResultadoProcesoDescuentosValidos(ByVal codigo_proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myCommand.CommandTimeout = 150000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myConnection.Close()
            myConnection.Dispose()
            myConnection = Nothing
            Return oDS

            'myConnection.Open()
            'Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            'Return result
        End Function


        'Obtenemos la informacion del resultado del proceso - solo registros validos
        Public Function GetRegistrosResultadoProcesoDescuentosErrores(ByVal codigo_proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim oAdapter As New SqlDataAdapter()
            Dim result As New DataSet()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myCommand.CommandTimeout = 50000

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(result, "Descuentos")
            myConnection.Close()
            Return result
        End Function


        'Obtenemos la informacion del resultado del proceso - solo registros validos
        Public Shared Function GetRegistrosResultadoProcesoDescuentosErrores(ByVal codigo_proceso As String, ByVal rep As Boolean) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim result As SqlDataReader
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, IIf(rep, "1", "0")})

            myCommand.CommandTimeout = 50000

            myConnection.Open()
            result = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Consulta estado por empresa
        Public Function consultaEstadoPorEmpresa(ByVal nombre_cliente As String, ByVal id_funcionario As Integer, ByVal anio_periodo As String, ByVal mes_periodo As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oAdapter As New SqlDataAdapter()
            Dim result As New DataSet()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {nombre_cliente, id_funcionario, anio_periodo, mes_periodo})

            myCommand.CommandTimeout = 50000

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(result, "Datos")
            myConnection.Close()
            Return result
        End Function
        'Obtenemos la informacion del resultado del proceso
        Public Function GetProcesosEsperaArchivoDescuento(ByVal Anio_periodo As String, ByVal Mes_Periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Anio_periodo, Mes_Periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'registra el proceso del archivo de descuentos
        Public Function IniciaProcesoCargaDescuentos(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
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

        'Actualiza el estado inicial de la descarga
        Public Function UpdRestauraEstadoInicial(ByVal codigo_proceso As String, ByVal usuario As String) As Integer
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

        'Establecer registro como pago/no pago
        Public Function EstablecePagoNoPago(ByVal codigo_proceso As String, ByVal DLNP As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, DLNP})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        'reporte de Proceso de archivos de descuento

        'Obtenemos los años del proceso de descuentos 
        Public Function GetAniosProcesoDescuentosCompletado() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos los meses del proceso de descuentos 
        Public Function GetMesesProcesoDescuentosCompletado(ByVal Anio As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Anio})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtenemos los meses del proceso de descuentos 
        Public Function GetProcesosDescuentoCompletado(ByVal Anio_periodo As String, ByVal Mes_Periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Anio_periodo, Mes_Periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos un valor que determina si podemos mostrar la informacion de la tabla error o de la tabla ClienteCuota
        Public Function GetExisteErrorTabla(ByVal codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function


        'Actualiza el estado inicial de la descarga
        Public Function UpdateFechaObtencionArchivo(ByVal codigo_proceso As String, _
                    ByVal Email As Boolean, _
                    ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, Email, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        'Actualiza la fecha de post conciliacion del proceso. CRP 15-01-2015 EA273 Mejoras Convenios
        Public Function ActualizaFechaPostConciliacion(ByVal codigo_proceso As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

#Region "reporte de envio"

        'Obtenemos los años del proceso de descuentos 
        Public Function GetAnioEnvioArchivoAS400() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos los meses del proceso de descuentos 
        Public Function GetMesesEnvioArchivoAS400(ByVal Anio As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Anio})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtiene la informacion de los procesos realizados en un mes y año
        Public Function GetProcesosEnvioArchivoAS400(ByVal anio_periodo As String, ByVal mes_periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio_periodo, mes_periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function
        'Obtiene la informacion de consistencia de la informacion de retorno 
        Public Function GetArchivosDescuentosHistCab(ByVal anio_periodo As String, ByVal mes_periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio_periodo, mes_periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la informacion del resultado del proceso
        Public Function GetArchivosDescuentosHistDet(ByVal codigo_proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myCommand.CommandTimeout = 150000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "archivo")
            myConnection.Close()
            myConnection.Dispose()
            myConnection = Nothing
            Return oDS
        End Function


        'Obtiene la informacion de los procesos realizados en un mes y año
        Public Function GetPagosRealizados(ByVal DLNE As String, ByVal DLCM As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DLNE, DLCM})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function



#End Region

#Region "Envio de informacion de propuestas a AS400"
        'Obtenemos el nombre del archivo de proceso
        Public Function EsperaFinalEnvioDescuentosAS400(ByVal Codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function


        'Obtener la cantidad de registros que deben ser enviadas a AS/400
        Public Function GetTotalRegistrosEnvio_AS400(ByVal codigo_proceso As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

        'Esta funcion devuelve el numero de registros que han sido RECIBIDOS EN AS/400
        Public Function ObtenerRegistrosEnviados(ByVal Codigo_Cliente As String, ByVal anio_periodo As String, ByVal mes_periodo As String) As String
            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim valor As String = ""
            Dim strQuery As String
            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            strQuery = "SELECT COUNT (1) FROM DLREC "
            strQuery &= " WHERE DLRCC =  " & Codigo_Cliente
            strQuery &= " AND DLRAP = " & anio_periodo & " AND "
            strQuery &= " DLRMP = " & mes_periodo

            result = myConnection.Execute(strQuery)

            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing
            If Not result.BOF And Not result.EOF Then
                valor = CType(result.Fields(0).Value, String)
            End If
            Return valor
        End Function

#Region "Informacion del Cliente"
        'Obtener el CUSCUN (CUSTOMER NUMBER) desde el servidor AS/400
        Public Function GetCustomerNumber(ByVal tipoDocumento As String, ByVal NumeroDocumento As String) As String
            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))

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

        'Obtenemos la informacion del mes y año del proceso 
        Public Function getClienteEmailAviso(ByVal idAviso As Integer) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idAviso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        Public Function GetInfoProcesoCliente(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        Public Function InsertaNominaEntradaSalida(ByVal Codigo_proceso As String, ByVal cantidad_clientes As Integer, _
           ByVal monto_total_soles As Double, ByVal monto_total_dolares As Double, ByVal tipo_nomina As String, ByVal tipo_formato As String, ByVal tipo_proceso As String, ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer = 0
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, cantidad_clientes, monto_total_soles, monto_total_dolares, tipo_nomina, tipo_formato, tipo_proceso, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        Public Function obtenerResumenClienteCuota(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

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
#End Region

#End Region

#Region "Cancelar el envio de propuestas"
        'Esperamos el final de la anulacion del proceso del archivo d descuentos
        Public Function EsperaFinalAnulacionProceso(ByVal Codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            returnValue = myCommand.ExecuteScalar
            myConnection.Close()
            Return returnValue
        End Function

#End Region

#Region "Obtener los pagos disponibles en tabla temporal desde AS/400"

        Public Function GetInfoProcesosDisponibles(ByVal filter As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim strQuery As String
            Dim oDS As New DataSet()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            strQuery = "SELECT DISTINCT "
            'strQuery &= " CST.CUSNA1, CST.CUSTID, CST.CUSIDN, e.DLCCC AS DLECC, 2000 + c.DLVCA AS DLEAP, c.DLVCM AS DLEMP, CAST(YEAR(CURRENT DATE) "
            strQuery &= " CST.CUSNA1, CAST(CST.CUSTID as CHARACTER(2)) AS CUSTID, CAST (CST.CUSIDN AS CHARACTER (12)) AS CUSIDN, e.DLCCC AS DLECC, 	CAST ( 2000 + c.DLVCA  AS CHARACTER ( 4 ) )  AS DLEAP, MAX(CST.CUSCUN) AS CUSCUN, "
            strQuery &= " CAST(c.DLVCM AS CHARACTER(2)) AS DLEMP, CAST(YEAR(CURRENT DATE) "
            strQuery &= " AS CHARACTER(4)) CONCAT CASE LENGTH(TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2)))) "
            strQuery &= " WHEN 1 THEN '0' CONCAT TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2))) "
            strQuery &= " WHEN 2 THEN SUBSTRING('00' CONCAT CAST(MONTH(CURRENT DATE) AS CHARACTER(2)), 3, 2) "
            strQuery &= " END CONCAT CASE LENGTH(TRIM(CAST(DAY(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(DAY(CURRENT DATE) "
            strQuery &= " AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(DAY(CURRENT DATE) AS CHARACTER(2)), 3, 2) END AS DLEFP "
            strQuery &= " FROM         DLCRE e INNER JOIN "
            strQuery &= " DLCCR r ON e.DLACC = r.DLACC INNER JOIN "
            strQuery &= " DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = '' INNER JOIN "
            strQuery &= " DLCNV CNV ON (E.DLCCC = CNV.CNVCUN AND E.DLAÑO = CNV.AÑCONV AND E.DLAGC = CNV.AGCONV AND E.DLCOC = CNV.COCONV) INNER JOIN "
            strQuery &= " CUMST CST ON (CNV.CNVCUN = CST.CUSCUN) "
            'REspinoza 20040811 - Restriccion para obtener la informacion de acuerdo a lo que aparece en el cronograma de envios
            strQuery &= " INNER JOIN DLEMP D ON ( D.DLECUN = E.DLCCC AND D.DLEAEN = c.DLVCA AND D.DLEMEN = c.DLVCM )"
            'Final de la restriccion
            strQuery &= " WHERE     (trim(r.DLSTS) = '') "
            If Trim(filter) <> "" Then
                strQuery &= " AND CST.CUSNA1 LIKE '" & Trim(filter) & "%' "
            End If
            strQuery &= " GROUP BY CST.CUSNA1, CST.CUSTID, CST.CUSIDN, e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, "
            strQuery &= " e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS "
            ' strQuery &= " HAVING      (c.DLNCT = MAX(r.DLNCT))"
            strQuery &= " ORDER BY 1, DLEMP"

            result = myConnection.Execute(strQuery)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            daTransform.Fill(oDS, result, "Result")

            'Modificacion para eliminar los procesos que ya fueron realizados
            'de la pantalla del usuario
            'Copiar la estructura de el DS
            Dim dsReturn As New DataSet()
            'dsReturn = oDS.Clone

            'Obtenemos el DataSet para restringir la obtencion de los ya procesados
            Dim dsJoin As New DataSet()
            dsJoin = Me.GetProcesosRealizados()

            'Adicionamos la tabla cliente a nuestro DataSet
            Dim dt As DataTable = dsJoin.Tables("Procesos").Copy()
            oDS.Tables.Add(dt)

            'Adicionamos las relaciones entre ambas tablas
            'Son 4 campos en cada tabla
            'Columnas del Padre
            Dim ParentColumns() As DataColumn = New DataColumn() _
                        {oDS.Tables("Result").Columns("CUSTID"), _
                            oDS.Tables("Result").Columns("CUSIDN"), _
                            oDS.Tables("Result").Columns("DLEAP"), _
                            oDS.Tables("Result").Columns("DLEMP")}

            'Procesos registrados
            'Columnas del hijo
            'TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo
            Dim ChildColumns() As DataColumn = New DataColumn() { _
                                    oDS.Tables("Procesos").Columns("TipoDocumento"), _
                                    oDS.Tables("Procesos").Columns("NumeroDocumento"), _
                                    oDS.Tables("Procesos").Columns("Anio_periodo"), _
                                    oDS.Tables("Procesos").Columns("Mes_Periodo")}
            ' Procesos disponibles AS/400

            Dim CustomerRelation1 As New DataRelation("Division1", ParentColumns, ChildColumns, False)
            oDS.Relations.Add(CustomerRelation1)

            'Eliminamos todos los registros del los procesos que existen 
            'en la base de datos SQL Server 
            Dim dr As DataRow
            Dim dr1 As DataRow
            For Each dr In oDS.Tables("Procesos").Rows
                For Each dr1 In dr.GetParentRows(CustomerRelation1)
                    dr1.Delete()
                Next
            Next
            oDS.Tables("Result").AcceptChanges()
            'dsReturn.Tables.Add(oDS.Tables("Result").Copy)

            'Respinoza 20040719 - Eliminar la informacion de los clientes que no han sido registrados 
            'en la base
            Dim oCliente As New Cliente()
            'Obtenemos la informacion de los clientes registrados
            'y eliminamos los que no estan registrados
            Dim odsCliente = oCliente.GetDocumentoClientesRegistrados()
            'Adicionamos la tabla cliente a nuestro Dataset
            dt = odsCliente.Tables("Cliente").Copy()
            oDS.Tables.Add(dt)

            Dim ChildProcesos() As DataColumn = New DataColumn() { _
                                                oDS.Tables("Result").Columns("CUSTID"), _
                                                oDS.Tables("Result").Columns("CUSIDN")}
            Dim ParentRegistrados() As DataColumn = New DataColumn() { _
                                    oDS.Tables("Cliente").Columns("TipoDocumento"), _
                                    oDS.Tables("Cliente").Columns("NumeroDocumento")}
            Dim CustomerRelation2 As New DataRelation("Division2", ParentRegistrados, ChildProcesos, False)
            oDS.Relations.Add(CustomerRelation2)

            For Each dr In oDS.Tables("Result").Rows
                If dr.GetParentRows(CustomerRelation2).Length = 0 Then
                    dr.Delete()
                End If
            Next
            oDS.Tables("Result").AcceptChanges()
            'Fin de las modificaciones

            dsReturn.Tables.Add(oDS.Tables("Result").Copy)
            Return dsReturn
        End Function
#End Region

#Region "Informacion de parametros de seguimiento"
        'Obtenemos los años del proceso de descuentos 
        Public Shared Function GetAniosProcesoSeguimiento() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos los meses del proceso de descuentos 
        Public Shared Function GetMesesProcesoSeguimiento(ByVal Anio As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Anio})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        Public Shared Function GetProcesosSeguimiento(ByVal anio_periodo As String, ByVal mes_periodo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {anio_periodo, mes_periodo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtenemos la informacion del resultado del proceso
        Public Shared Function GetRegistrosDeudasResultadoProcesoDescuentos(ByVal codigo_proceso As String, ByVal codigo As String, ByVal dlne As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, codigo, dlne})

            myCommand.CommandTimeout = 150000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myCommand.Dispose()
            myConnection.Close()
            myConnection.Dispose()
            myConnection = Nothing
            Return oDS

        End Function


#End Region

        'Agregacion nuevo requerimiento
#Region "Agregación Nuevos Requerimientos"

        'Obtener la informacion de estados del trabajador
        Public Function GetDatosZonaUse(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Actualiza en la Tabla Cliente Cuota
        Public Function ActualizarClienteCuota(ByVal Codigo_proceso As String, ByVal codigo_clienteIBS As String, ByVal Pagare As String, _
                                            ByVal Importe As Decimal, ByVal CuotaPactadas As Integer, ByVal CuotaPagadas As Integer, ByVal CuotaPendientes As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, codigo_clienteIBS, Pagare, Importe, CuotaPactadas, CuotaPagadas, CuotaPendientes})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function


        'Inserta en la Tabla Historica Cliente Cuota
        Public Function AddHistorico_ClienteCuota(ByVal Codigo_proceso As String, ByVal codigo_clienteIBS As String, ByVal Pagare As String, _
                                                  ByVal Trabajador As String, ByVal Importe As Decimal, ByVal Usuario As String, ByVal CuotaPactadas As Integer, _
                                                  ByVal CuotaPagadas As Integer, ByVal CuotaPendientes As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, codigo_clienteIBS, Pagare, Trabajador, Importe, Usuario, CuotaPactadas, CuotaPagadas, CuotaPendientes})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function

        'Elimina Cliente en la Tabla Cliente Cuota
        Public Function EliminarTrabajadorClienteCuota(ByVal Codigo_proceso As String, ByVal codigo_clienteIBS As String, ByVal Pagare As String) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, codigo_clienteIBS, Pagare})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function

        'Registra en la Tabla Cliente Cuota
        Public Function InsertaClienteCuota(ByVal Codigo_proceso As String, ByVal DLNP As String, ByVal DLCM As String, ByVal DLPA As String, _
                                            ByVal DLMA As String, ByVal DLMN As String, ByVal DLCR As String, ByVal DLIC As Decimal, ByVal Cuotas As Integer, _
                                            ByVal DeudaPeriodo As Decimal, ByVal NroDocumento As String, ByVal FechaDesembolso As DateTime, ByVal MontoOriginal As Decimal, _
                                            ByVal CuotaInformada As Integer, ByVal FechaCargo As DateTime, ByVal CuotaPactadas As Integer, ByVal CuotaPagadas As Integer) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, DLNP, DLCM, DLPA, DLMA, DLMN, DLCR, DLIC, Cuotas, DeudaPeriodo, NroDocumento, FechaDesembolso, _
                                MontoOriginal, CuotaInformada, FechaCargo, CuotaPactadas, CuotaPagadas})

            myConnection.Open()

            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function

        'Obtenemos la informacion acerca del año del proceso
        Public Function FiltroInformeProceso(ByVal codigo_proceso As String, ByVal Documento As String, ByVal DLNE As String, ByVal DLNP As String, _
        ByVal EstadoTrabajador As String, ByVal ZonaUse As Integer) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer            

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, Documento, DLNE, DLNP, EstadoTrabajador, ZonaUse})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function

        'Exporta la informacion del resultado del proceso
        Public Function ExportRegistrosResultadoProceso(ByVal codigo_proceso As String, ByVal Documento As String, ByVal DLNE As String, ByVal DLNP As String, ByVal EstadoTrabajador As String, ByVal ZonaUse As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, Documento, DLNE, DLNP, EstadoTrabajador, ZonaUse})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Descuentos")
            myConnection.Close()
            Return oDS
        End Function

        Public Function GetCodigoClienteIBS(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer            

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result

        End Function

        Public Function ExportarEfectividadRecaudacion(ByVal codigo_ibs As String, ByVal anio As Integer, ByVal mes As String, ByVal Empresa As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_ibs, anio, mes, Empresa})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Recaudacion")
            myConnection.Close()
            Return oDS
        End Function

        Public Function GetClienteBIFConvenios(ByVal Nombre_Cliente As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Nombre_Cliente})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Clientes")
            myConnection.Close()
            Return oDS
        End Function


#End Region

        Public Function GetAnioCasillero() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


    End Class
End Namespace

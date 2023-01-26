Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports ADODB
Imports System.Data.OleDb

Namespace BIFConvenios
    'Manipulacion de la informacion de la empresa cliente y del cliente del convenio
    Public Class Cliente

        Public Function GetClientes(ByVal Nombre_Cliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Nombre_Cliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        Public Function GetClientesDS_MantenimientoCuotas(ByVal Nombre_Cliente As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            Try
                'Dim returnValue As Integer
                Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                    New Object() {Nombre_Cliente})

                oAdapter.SelectCommand = myCommand
                myConnection.Open()
                oAdapter.Fill(oDS, "Cliente")
                myConnection.Close()

                Return oDS
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        'obtenemos la informacion de un cliente usando un DS
        Public Function GetClientesDS(ByVal Nombre_Cliente As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Nombre_Cliente})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Cliente")
            myConnection.Close()
            Return oDS
        End Function

        'obtenemos la informacion de los clientes con los numeros de documentos
        Public Function GetDocumentoClientesRegistrados() As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Cliente")
            myConnection.Close()
            Return oDS
        End Function

        'Obtenemos la informaicon de un solo cliente
        Public Function GetCliente(ByVal Codigo_cliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_cliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtener la informacion del cliente utilizando el codigo del proceso
        Public Function GetInfoClienteProceso(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtenemos los correos electronicos de un cliente 
        Public Function GetEmails(ByVal Codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'obtenemos la informacion de notificacion de un usuario
        'Modificado por Christian Rivera 16-06-2014 EA273 Mejoras Convenios
        'Se añaden parametros para los nuevos datos del mantenimiento de empresas
        Public Function UpdateCliente(ByVal Codigo_Cliente As String, ByVal Nombre_Cliente As String, _
                                    ByVal TipoDocumento As String, ByVal NumeroDocumento As String, _
                                    ByVal CorreoElectronico As String, ByVal telefono_1 As String, _
                                    ByVal telefono_2 As String, ByVal telefono_3 As String, _
                                    ByVal telefono_4 As String, ByVal dia_envio_planilla As String, _
                                    ByVal dia_cierre_planilla As String, ByVal meses_anticipacion_envio_listado As String, _
                                    ByVal dia_corte As String, ByVal id_funcionario As Integer, _
                                    ByVal codigo_IBS As Integer, ByVal codigo_institucion As String, _
                                    ByVal ind_envio_automatico_listado As String, ByVal usuario_creacion As String, _
                                    ByVal fecha_creacion As Date, ByVal usuario_modificacion As String, _
                                    ByVal fecha_modificacion As Date) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, Nombre_Cliente, TipoDocumento, _
                            NumeroDocumento, CorreoElectronico, telefono_1, telefono_2, telefono_3, _
                            telefono_4, dia_envio_planilla, dia_cierre_planilla, meses_anticipacion_envio_listado, _
                            dia_corte, id_funcionario, codigo_IBS, codigo_institucion, _
                            ind_envio_automatico_listado, usuario_creacion, fecha_creacion, _
                            usuario_modificacion, fecha_modificacion})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        Public Function UpdateClienteModalidad(ByVal Codigo_Cliente As String, ByVal NumeroPagare As String, ByVal Modalidad As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, NumeroPagare, Modalidad})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function



        'obtenemos la informacion de notificacion de un usuario
        Public Function DeleteCliente(ByVal Codigo_Cliente As String, ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function


        'Obtener el CUSCUN (CUSTOMER NUMBER) desde el servidor AS/400
        Public Function GetCustomerNumber(ByVal tipoDocumento As String, ByVal NumeroDocumento As String) As String
            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String = ""
            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
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

#Region "Listado de importes por vencer"

        'Obtenemos la informacion del Contacto para el Credito Convenio
        'Obser: Una linea por moneda y cuenta
        'TODO: Cabecera para el Listado de Cuitas por Vencer
        Public Function GetInfoContactoConvenio(ByVal tipoDocumento As String, _
                                        ByVal NumeroDocumento As String, _
                                        ByVal Anhio As String, _
                                        ByVal Mes As String) As DataSet

            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim strSQL As String = ""

            Dim returnValue As String = ""

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            strSQL = "SELECT DISTINCT CUSCUN, CUSNA1, CUSNA2 CONCAT CUSNA3 AS CUSNA2, CUSCTY,"
            strSQL &= "  CASE WHEN NOMCNT IS NULL THEN '' ELSE NOMCNT END AS NOMCNT "
            strSQL &= " , ST.CUSZPC"
            strSQL &= " FROM CUMST ST LEFT JOIN DLCON DC ON "
            strSQL &= " ( ST.CUSCUN = DC.CONCUN  AND DC.CONSEC = 1 )"
            strSQL &= " INNER JOIN DLCRE CRE on ( CRE.DLCCC = CUSCUN )"
            strSQL &= " INNER JOIN DLCCR CCR "
            strSQL &= " 	ON ( CCR.DLACC = CRE.DLACC"
            strSQL &= " 		AND CCR.DLVCA +2000 = " & Anhio
            strSQL &= " 		AND CCR.DLVCM = " & Mes
            strSQL &= "  	)"
            strSQL &= " WHERE CUSTID  ='" & tipoDocumento & "' AND CUSIDN ='" & NumeroDocumento & "'"

            result = myConnection.Execute(strSQL)

            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "DLinfo")
            'oDs.WriteXmlSchema("c:/InfoContacto.txt")

            Return oDs
        End Function

        'Obtenemos los correos electronicos de un cliente 
        Public Function GetListadoCuotasPorVencerCliente(ByVal Codigo_proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            'myConnection.Open()
            myCommand.CommandTimeout = 100000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS)
            myConnection.Close()

            Return oDS
        End Function

        'Obtenemos los correos electronicos de un cliente 
        Public Function GetListadoCuotasPorVencerCliente2(ByVal Codigo_proceso As String, ByVal modalidad As String, ByVal situacionTrabajador As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, modalidad, situacionTrabajador})

            'myConnection.Open()
            myCommand.CommandTimeout = 100000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS)
            myConnection.Close()

            Return oDS
        End Function

        'ADD 04/07/2013 NCA REQ: EA2013-273 OPTIMIZACION PROCESOS CONVENIOS.
        Public Function GetDatosArchivoCuotasPorVencerCliente(ByVal Codigo_proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

            'myConnection.Open()
            myCommand.CommandTimeout = 100000
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS)
            myConnection.Close()

            Return oDS
        End Function


        'Public Function getProcesoNumeroPrestamos(ByVal Codigo_proceso As String) As DataSet
        '    Dim myConnection As New SqlConnection(GetDBConnectionString)
        '    Dim oDS As New DataSet()
        '    Dim oAdapter As New SqlDataAdapter()
        '    Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
        '        CType(MethodBase.GetCurrentMethod(), MethodInfo), _
        '        New Object() {Codigo_proceso})

        '    'myConnection.Open()
        '    myCommand.CommandTimeout = 100000
        '    oAdapter.SelectCommand = myCommand
        '    myConnection.Open()
        '    oAdapter.Fill(oDS)
        '    myConnection.Close()

        '    Return oDS
        'End Function


        'Obtenemos la lista de importes de un cliente
        Public Function GetListadoCuotasPorVencer(ByVal Codigo_proceso As String, ByVal DLST As String, ByVal DLMO As String, ByVal DLAG As Decimal) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, DLST, DLMO, DLAG})

            'myConnection.Open()
            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Cliente")
            myConnection.Close()
            'oDS.WriteXmlSchema("c:/cuotas.txt")

            Return oDS
            ' Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            '' Return result
        End Function

        Public Function GetListadoCuotasPorVencerResumen(ByVal Codigo_proceso As String, ByVal DLST As String, ByVal DLMO As String, ByVal DLAG As Decimal) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, DLST, DLMO, DLAG})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Cliente")
            myConnection.Close()
            'oDS.WriteXmlSchema("c:/resumen.txt")
            Return oDS
            'myConnection.Open()
            'Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            'Return result
        End Function
#End Region

        'Obtenemos el codigo de cliente si existe o -1 si no existe
        Public Function ExisteCliente(ByVal TipoDocumento As String, ByVal NumeroDocumento As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {TipoDocumento, NumeroDocumento})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, String)
            myConnection.Close()
            Return returnValue
        End Function




#Region "Maneja del cliente del convenio"

        'Obtenemos la informaicon de un solo REGISTRO de cliente con error
        Public Function GetDetalleErrorRegistro(ByVal codigo As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la informaicon de un solo REGISTRO de cliente con error
        Protected Function GetEstadoErrorRegistro(ByVal CodigoError As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {CodigoError})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos el nombre del error 
        Public Function GetError(ByVal CodigoError As String) As String
            Dim returnValue As String = ""
            Dim dr As SqlDataReader = Me.GetEstadoErrorRegistro(CodigoError)
            If dr.Read Then
                returnValue = CType(dr("CodigoNombre"), String)
            End If
            Return returnValue
        End Function

#End Region

#Region "Actualización de registros del cliente del convenio"
        'obtenemos la informacion de notificacion de un usuario
        Public Function ActualizaRegistroErroneo(ByVal Moneda As String, ByVal NumeroPagare As String, _
                            ByVal CodigoModular As String, ByVal CodigoReferencia As String, ByVal Anio As String, _
                            ByVal Mes As String, ByVal Cuota As String, ByVal SituacionLaboral As String, _
                            ByVal MontoDescuento As String, ByVal codigo As String, ByVal forceInsert As Boolean, ByVal usuario As String) As Integer

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Moneda, NumeroPagare, _
                                CodigoModular, CodigoReferencia, Anio, _
                                Mes, Cuota, SituacionLaboral, _
                                MontoDescuento, codigo, forceInsert, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function


        'Elimina un registro de la tabla de carga
        Public Function DeleteRegistroError(ByVal codigo As String, ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

#End Region


#Region "Actualización de la información de cliente del convenio en la tabla de envio"
        'obtenemos la informacion de notificacion de un usuario
        Public Function GetRegistroErrorEnvio(ByVal codigo_proceso As String, ByVal DLNP As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, DLNP})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'obtenemos la informacion de notificacion de un usuario
        Public Function EliminarRegistroErrorEnvio(ByVal DLNP As String, ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DLNP, usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        'obtenemos la informacion de notificacion de un usuario
        Public Function ActualizaDescuentoRegistroErrorEnvioA(ByVal myConnection As SqlConnection, ByVal codigo_proceso As String, ByVal DLNP As String, ByVal DLID As String, ByVal usuario As String) As Integer
            'Dim myConnection As New SqlConnection(GetDBConnectionString)
            pmyConnection = myConnection
            ActualizaDescuentoRegistroErrorEnvioA(codigo_proceso, DLNP, DLID, usuario)
        End Function


        Public Function ActualizaDescuentoRegistroErrorEnvioA(ByVal codigo_proceso As String, ByVal DLNP As String, ByVal DLID As String, ByVal usuario As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(pmyConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso, DLNP, DLID, usuario})

            'myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            'myConnection.Close()
            Return returnValue
        End Function


        'obtenemos la informacion de notificacion de un usuario
        Public Function ActualizaDescuentoRegistroErrorEnvio(ByVal myConnection As SqlConnection, ByVal DLNP As String, ByVal DLID As String, ByVal usuario As String, ByVal codigo_proceso As String, ByVal dateCode As String, ByVal ORDEN As String) As Integer
            'Dim myConnection As New SqlConnection(GetDBConnectionString)
            pmyConnection = myConnection
            ActualizaDescuentoRegistroErrorEnvio(DLNP, DLID, usuario, codigo_proceso, dateCode, ORDEN)
        End Function

        Protected pmyConnection As SqlConnection

        Public Function ActualizaDescuentoRegistroErrorEnvio(ByVal DLNP As String, ByVal DLID As String, ByVal usuario As String, _
        ByVal codigo_proceso As String, ByVal dateCode As String, ByVal ORDEN As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(pmyConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {DLNP, DLID, usuario, codigo_proceso, dateCode, ORDEN})

            'myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            'myConnection.Close()
            Return returnValue
        End Function

#End Region



#Region "Consulta de envio por trabajador"
        Public Function GetClientesProcesoEnviado() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtenemos la infomracion de los procesos que han sido 
        Public Function GetClienteCuotaEnvio(ByVal Codigo_Cliente As String, _
                                                    ByVal DLAP As String, ByVal DLMP As String, _
                                                    ByVal DLNE As String) As SqlDataReader

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, _
                               DLAP, DLMP, DLNE})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


#End Region



        'Dim myConnection As New ADODB.Connection()
        'Dim oAdapter As New OleDbDataAdapter()
        'Dim oDs As New DataSet()
        'Dim result As New ADODB.Recordset()
        'Dim returnValue As String = ""

        '    myConnection.CursorLocation = CursorLocationEnum.adUseClient
        '    myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

        '' result = myConnection.Execute("SELECT DLECUN, DLEDSC, DLEAEN, DLEMEN, DLEDEN FROM DLEMP order by 3, 4, 5")
        '    result = myConnection.Execute("SELECT DLECUN, DLEDSC, DLEAEN, DLEMEN, DLEDEN, C.CUSIDN, C.CUSTID FROM DLEMP D INNER JOIN CUMST C ON (C.CUSCUN = D.DLECUN) order by 3, 4, 5")
        '    result.ActiveConnection = Nothing
        '    myConnection.Close()
        '    myConnection = Nothing

        '    oAdapter.Fill(oDs, result, "DLEMP")


        'Obtener el cronograma de envio para los clientes
        Public Function GetCronogramaEnvioCliente() As DataSet
            Dim myConnection As New  ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String = ""

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute("SELECT DLECUN, DLEDSC, DLEAEN, DLEMEN, DLEDEN, C.CUSIDN, C.CUSTID FROM DLEMP D INNER JOIN CUMST C ON (C.CUSCUN = D.DLECUN) order by 2, 3, 4, 5")
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "DLEMP")


            'Copiar la estructura de el DS
            Dim dsReturn As New DataSet()
            dsReturn = oDs.Clone


            'Tenemos los datos de los clientes ahora, tenemos que buscar los clientes registrados
            'dentro del sistema.
            Dim dsJoin As New DataSet()
            dsJoin = Me.GetClientesDS("")

            'Adicionamos la tabla cliente a nuestro DataSet
            Dim dt As DataTable = dsJoin.Tables("Cliente").Copy()
            oDs.Tables.Add(dt)

            'Adicionamos las relaciones entre ambas tablas
            Dim Parent As DataColumn = oDs.Tables("Cliente").Columns("NumeroDocumento")    ' Clientes registrados
            Dim Child As DataColumn = oDs.Tables("DLEMP").Columns("CUSIDN")     ' clientes del cronograma en AS/400
            Dim CustomerRelation1 As New DataRelation("Division1", Parent, Child, False)
            oDs.Relations.Add(CustomerRelation1)


            Dim dr As DataRow
            Dim dr1 As DataRow
            For Each dr In oDs.Tables("Cliente").Rows
                For Each dr1 In dr.GetChildRows(CustomerRelation1)
                    dsReturn.Tables(0).ImportRow(dr1)
                Next
            Next

            '            dsReturn.Tables(0).DefaultView.Sort = "DLEAEN, DLEMEN, DLEDEN desc"
            Return dsReturn

            'Return oDs
        End Function

        Public Function getNuevosDLCCR(ByVal NumeroPagare As String, ByVal codigo_clienteIBS As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String = ""
            Dim sql As String = " SELECT 'N' AS MODALIDAD FROM DLCCR D INNER JOIN DLEMP F " & _
                                " ON D.DLVCA=F.DLEAEN AND D.DLVCM =F.DLEMEN AND D.DLNCT=1 AND D.DLACC='" & NumeroPagare & "' " & _
                                " AND F.DLECUN=" & codigo_clienteIBS

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(System.Configuration.ConfigurationManager.AppSettings("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(sql)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "DLCCR")

            Return oDs

        End Function

        Public Function getReembolsosPLPAD(ByVal NumeroPagare As String, ByVal codigo_clienteIBS As String) As DataSet
            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String = ""
            Dim sql As String = " SELECT 'S' AS MODALIDAD FROM DLCCR D INNER JOIN DLEMP F " & _
                                " ON D.DLVCA=F.DLEAEN AND D.DLVCM =F.DLEMEN INNER JOIN PLPAD G ON " & _
                                " G.PLDAPV=D.DLVCA AND G.PLDMPV=D.DLVCM AND G.PLCSEQ>0 AND G.PLCNPG = '" & NumeroPagare & "' " & _
                                " AND F.DLECUN=" & codigo_clienteIBS

            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(System.Configuration.ConfigurationManager.AppSettings("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(sql)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "PLPAD")

            Return oDs

        End Function

        Public Function getModificadosMNTOR(ByVal NumeroPagare As String) As DataSet

            Dim myConnection As New ADODB.Connection()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim result As New ADODB.Recordset()
            Dim returnValue As String = ""
            'Dim sql As String = "select 'M' AS MODALIDAD from (" & _
            '                    "SELECT MNTACC" & _
            '                    ",row_number() over(partition by mntacc) as rn FROM(" & _
            '                    "SELECT MNTACC FROM MNTOR " & _
            '                    "WHERE MNTACC = " & "'" & NumeroPagare & "'" & " AND mntmod ='UPDATE' " & _
            '                    "UNION " & _
            '                    "SELECT MNTACC FROM MNTORH  " & _
            '                    "WHERE MNTACC = " & "'" & NumeroPagare & "'" & " AND mntmod ='UPDATE' " & _
            '                    ")T )TT WHERE RN = 1   "

            Dim sql As String = "SELECT 'M' AS MODALIDAD FROM DLPMT WHERE DLPACC= " & NumeroPagare & " and DLPPNU=999 " & _
                                "AND  DLPPDY*10000 + DLPPDM*100 + DLPPDD >= RIGHT(TRIM(CHAR(YEAR(CURRENT DATE - 1 MONTH))),2)*10000 + MONTH(CURRENT DATE - 1 MONTH)*100 + DAY(CURRENT DATE - 1 MONTH) " & _
                                "AND  DLPPDY*10000 + DLPPDM*100 + DLPPDD <= RIGHT(TRIM(CHAR(YEAR(CURRENT DATE))),2)*10000 + MONTH(CURRENT DATE)*100 + DAY(CURRENT DATE) "


            myConnection.CursorLocation = CursorLocationEnum.adUseClient
            'myConnection.Open(System.Configuration.ConfigurationManager.AppSettings("AS400-ConnectionString-Convenios"))
            myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
            result = myConnection.Execute(sql)
            result.ActiveConnection = Nothing
            myConnection.Close()
            myConnection = Nothing

            oAdapter.Fill(oDs, result, "MNTOR")

            Return oDs

        End Function

        'Public Sub InsertaReemGanchesProceso(ByVal dsplpad As DataSet, ByVal Codigo_proceso As String)

        '    Dim myConnection As New SqlConnection(GetDBConnectionString)
        '    myConnection.Open()
        '    Dim command As SqlCommand = myConnection.CreateCommand()
        '    Dim transaction As SqlTransaction
        '    transaction = myConnection.BeginTransaction("InsertReembolsosTransaction")
        '    command.Connection = myConnection
        '    command.Transaction = transaction
        '    Dim indice As Integer = 0
        '    Try
        '        For indice = 0 To dsplpad.Tables("PLPAD").Rows.Count - 1
        '            command.CommandText = "INSERT INTO REENGANCHES (CODIGO_PROCESO,PLCNPG,PLDDPV,PLDMPV,PLDAPV) VALUES(@proceso,@nprestamo,@diavcto,@mesvcto,@aniovcto)"
        '            command.Parameters.Add(New SqlParameter("@proceso", Codigo_proceso))
        '            command.Parameters.Add(New SqlParameter("@nprestamo", dsplpad.Tables("PLPAD").Rows(indice).Item("PLCNPG").ToString()))
        '            command.Parameters.Add(New SqlParameter("@diavcto", dsplpad.Tables("PLPAD").Rows(indice).Item("PLDDPV").ToString()))
        '            command.Parameters.Add(New SqlParameter("@mesvcto", dsplpad.Tables("PLPAD").Rows(indice).Item("PLDMPV").ToString()))
        '            command.Parameters.Add(New SqlParameter("@aniovcto", dsplpad.Tables("PLPAD").Rows(indice).Item("PLDAPV").ToString()))
        '            command.ExecuteNonQuery()
        '            command.Parameters.Clear()
        '        Next
        '        transaction.Commit()
        '    Catch ex As Exception
        '        transaction.Rollback()
        '    End Try

        'End Sub

        'Public Sub InsertaModificadosProceso(ByVal dsmntor As DataSet, ByVal Codigo_proceso As String)

        '    Dim myConnection As New SqlConnection(GetDBConnectionString)
        '    myConnection.Open()
        '    Dim command As SqlCommand = myConnection.CreateCommand()
        '    Dim transaction As SqlTransaction
        '    transaction = myConnection.BeginTransaction("InsertModificadosTransaction")
        '    command.Connection = myConnection
        '    command.Transaction = transaction
        '    Dim indice As Integer = 0
        '    Try
        '        For indice = 0 To dsmntor.Tables("MNTOR").Rows.Count - 1
        '            command.CommandText = "INSERT INTO MODIFICADOS (CODIGO_PROCESO,MNTACC,MNTSYD,MNTSYM,MNTSYY) VALUES(@proceso,@nprestamo,@diavcto,@mesvcto,@aniovcto)"
        '            command.Parameters.Add(New SqlParameter("@proceso", Codigo_proceso))
        '            command.Parameters.Add(New SqlParameter("@nprestamo", dsmntor.Tables("MNTOR").Rows(indice).Item("MNTACC").ToString()))
        '            command.Parameters.Add(New SqlParameter("@diavcto", dsmntor.Tables("MNTOR").Rows(indice).Item("MNTSYD").ToString()))
        '            command.Parameters.Add(New SqlParameter("@mesvcto", dsmntor.Tables("MNTOR").Rows(indice).Item("MNTSYM").ToString()))
        '            command.Parameters.Add(New SqlParameter("@aniovcto", dsmntor.Tables("MNTOR").Rows(indice).Item("MNTSYY").ToString()))
        '            command.ExecuteNonQuery()
        '            command.Parameters.Clear()
        '        Next
        '        transaction.Commit()
        '    Catch ex As Exception
        '        transaction.Rollback()
        '    End Try

        'End Sub

        Public Function spObtenerEMailsEnviosClientes(ByVal iCodigoCliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {iCodigoCliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        Public Function spObtenerEMailsFuncionarioCliente(ByVal iCodigoCliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {iCodigoCliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtiene los datos de los coordinadores de las empresas. Christian Rivera 16-06-2014 EA 273-Mejoras convenios
        Public Function ObtieneCoordinadorCliente(ByVal Codigo_cliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_cliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        Public Function GetFuncionarioCliente(ByVal Codigo_cliente As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_cliente})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Actualiza los datos de los coordinadores de las empresas
        Public Function ActualizaCoordinadorCliente(ByVal Codigo_Cliente As String, ByVal id_coordinador As Integer, _
                                    ByVal nombre_coordinador As String, ByVal email_coordinador As String, _
                                    ByVal usuario_modificacion As String, ByVal fecha_modificacion As Date) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, id_coordinador, nombre_coordinador, email_coordinador, _
                                usuario_modificacion, fecha_modificacion})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        'Inserta los datos de los coordinadores de las empresas
        Public Function InsertaCoordinadorCliente(ByVal Codigo_Cliente As String, ByVal nombre_coordinador As String, _
                                                    ByVal email_coordinador As String, ByVal usuario_creacion As String, _
                                                    ByVal fecha_creacion As Date) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, nombre_coordinador, email_coordinador, _
                                usuario_creacion, fecha_creacion})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function

        'Elimina los datos de los coordinadores de las empresas
        Public Function EliminaCoordinadorCliente(ByVal Codigo_Cliente As String, ByVal id_coordinador As Integer, _
                                                    ByVal usuario_modificacion As String, ByVal fecha_modificacion As Date) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_Cliente, id_coordinador, usuario_modificacion, fecha_modificacion})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function
    End Class
End Namespace

Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB
Imports BIFConvenios.Container

Namespace BIFConvenios

    'Clase para contener las funciones de manipulacion de los archivos de descuentos
    Public Class ArchivosDescuento

        'Obtener información con los errores de proceso del archivo de descuentoss
        Public Shared Function getErroresArchivo(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function


        'Obtener la informacion de un archivo de texto con la informacion no procesada
        Public Shared Function getDatosArchivoTexto(ByVal codigo_proceso As String) As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Realizar una busqueda entre los errores para determinar cuales de los registros coinciden con el primer nombre del pagare 
        Public Shared Function getDatosArchivoTextoBusqueda(ByVal codigo_Proceso As String, ByVal numeropagare As String)
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_Proceso, numeropagare})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function

        'Obtener la informacion de un registro que no ha sido procesado en la carga con la finalidad de adicionarlo
        Public Shared Function getDatosRegistroNoProcesado(ByVal Codigo_proceso As String, ByVal dateCode As String, ByVal orden As String) As RegistroArchivoNoProcesado
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            ' Dim returnValue As Integer
            Dim oRegistroArchivoNoProcesado As RegistroArchivoNoProcesado

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_Proceso, dateCode, orden})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            oRegistroArchivoNoProcesado = New RegistroArchivoNoProcesado()

            If result.Read Then
                'oRegistroArchivoNoProcesado = New RegistroArchivoNoProcesado()
                oRegistroArchivoNoProcesado.IdProceso = CType(result("Id Proceso"), String)
                oRegistroArchivoNoProcesado.Linea = result("Linea")
                oRegistroArchivoNoProcesado.NumeroPagare = result("Numero Pagare")
                oRegistroArchivoNoProcesado.Nombre = result("Nombre")
                oRegistroArchivoNoProcesado.MontoDescuento = result("Monto Descuento")
                oRegistroArchivoNoProcesado.Anio_periodo = result("Anio_periodo")
                oRegistroArchivoNoProcesado.Mes_Periodo = result("Mes_Periodo")

                Dim i As Integer = 0
                For i = 0 To result.FieldCount - 1
                    If result.GetName(i).Trim = "Codigo Modular" Then
                        oRegistroArchivoNoProcesado.CodigoModular = result(result.GetName(i).Trim)
                    End If
                Next

            End If

            Return oRegistroArchivoNoProcesado

        End Function



        'obtenemos la informacion de notificacion de un usuario
        Public Shared Function AdicionaRegistroErroneoArchivoTexto(ByVal Codigo_proceso As String, ByVal dateCode As String, _
                                    ByVal ORDEN As String, ByVal Moneda As String, ByVal NumeroPagare As String, _
                                    ByVal CodigoModular As String, ByVal CodigoReferencia As String, ByVal Anio As String, _
                                    ByVal Mes As String, ByVal Cuota As String, ByVal SituacionLaboral As String, _
                                    ByVal MontoDescuento As String, ByVal DLNE As String, _
                                    ByVal usuario As String) As Integer
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, dateCode, _
                                    ORDEN, Moneda, NumeroPagare, _
                                    CodigoModular, CodigoReferencia, Anio, _
                                    Mes, Cuota, SituacionLaboral, _
                                    MontoDescuento, DLNE, _
                                    usuario})

            myConnection.Open()
            returnValue = myCommand.ExecuteNonQuery()
            myConnection.Close()
            Return returnValue
        End Function


        'Obtener informacion de las cuotas
        Public Shared Function getDataSetListadoCuotasFin(ByVal id As String, ByVal modalidad As String, ByVal situacionTrabajador As String) As DataSetListadoCuotasFin
            Dim ds As New DataSetListadoCuotasFin()
            Dim ds3 As DataSet
            'Dim dt As DataTable
            Dim Codigo_proceso As String
            Dim oCliente As New Cliente()

            Dim TipoDocumento As String = ""
            Dim NumeroDocumento As String = ""
            Dim Anio_periodo As String = ""
            Dim Mes_Periodo As String = ""


            ds.Reset()
            Codigo_proceso = id
            Dim dr As SqlDataReader = oCliente.GetInfoClienteProceso(Codigo_proceso)
            If dr.Read Then
                TipoDocumento = CType(dr("TipoDocumento"), String)
                NumeroDocumento = CType(dr("NumeroDocumento"), String)
                Anio_periodo = CType(dr("Anio_periodo"), String)
                Mes_Periodo = CType(dr("Mes_Periodo"), String)
            End If

            Dim ds2 As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)
            ds.Tables.Add(ds2.Tables(0).Copy)
            'ds3 = oCliente.GetListadoCuotasPorVencerCliente(Codigo_proceso)
            ds3 = oCliente.GetListadoCuotasPorVencerCliente2(Codigo_proceso, modalidad, situacionTrabajador)

            'Copiamos la lista de cuotas
            ds3.Tables(0).TableName = "Cliente"
            ds.Tables.Add(ds3.Tables(0).Copy)
            Return ds
        End Function

        
        'ADD NCA 08/07/2014 NCA EA2013-273 OPT PROCESO CONVENIOS
        Public Sub GenerarArchivExcelDescuentos(ByVal id As String, ByVal nombreArchivo As String, ByVal situacionTrabajador As String, ByRef cantidadRegistros As Integer, ByRef sumaRegistrosSoles As Double, ByRef sumaRegistrosDolares As Double, ByVal modalidad As String)
            Dim oCliente As New Cliente
            Dim dr As DataRow()
            Dim ds As DataSet = oCliente.GetDatosArchivoCuotasPorVencerCliente(id)
            Dim FILTRO As String = ""

            If (modalidad.Trim = "S") Then
                modalidad = "AMPLIACION"
            ElseIf (modalidad.Trim = "N") Then
                modalidad = "NUEVO"
            ElseIf (modalidad.Trim = "M") Then
                modalidad = "MODIFICADO"
            End If

            If situacionTrabajador <> "-" And situacionTrabajador <> "" Then
                FILTRO = "DLST = '" + situacionTrabajador + "'"
                If modalidad.Trim <> "-" And modalidad.Trim <> "" Then
                    FILTRO = FILTRO & " AND  " 'ADD JCHAVEZH 21/10/2014 REQ: FILTRO MODALIDAD
                End If
            End If
            If modalidad.Trim <> "-" And modalidad.Trim <> "" Then
                FILTRO = FILTRO & " MODALIDAD = '" + modalidad + "'  " 'ADD JCHAVEZH 21/10/2014 REQ: FILTRO MODALIDAD
            End If

            dr = ds.Tables(0).Select(FILTRO)

            cantidadRegistros = dr.Length
            Dim i As Integer = 0
            For i = 0 To dr.Length - 1
                If (dr(i).Item("DLNP").ToString() <> Nothing) Then
                    If (dr(i).Item("DLMO").ToString() = "SOL") Then
                        sumaRegistrosSoles = sumaRegistrosSoles + CType(dr(i).Item("DLIC").ToString(), Double)
                    End If
                    If (dr(i).Item("DLMO").ToString() = "DOL") Then
                        sumaRegistrosDolares = sumaRegistrosDolares + CType(dr(i).Item("DLIC").ToString(), Double)
                    End If
                End If
            Next

            Dim dt As New DataTable
            dt = ds.Tables(0).Clone()
            i = 0
            For i = 0 To dr.Length - 1
                dt.ImportRow(dr(i))
            Next

            Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("NombrePlantilla"))
            archivExcel.GenerarArchivoExcel(dt, ConfigurationManager.AppSettings("GenFolder") & nombreArchivo)
        End Sub

        Public Sub GenerarArchivExcelDescuentos(ByVal id As String, ByVal nombreArchivo As String)
            Dim oCliente As New Cliente
            Dim ds As DataSet = oCliente.GetDatosArchivoCuotasPorVencerCliente(id)
            Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("NombrePlantilla"))
            archivExcel.GenerarArchivoExcel(ds.Tables(0), ConfigurationManager.AppSettings("GenFolder") & nombreArchivo)
        End Sub


        
        '**********************************************************************************************************************************************************************
        '************************************************TI-EA2019-11648*******************************************************************************************************
        'Fecha Creacion: 24/06/2019
        'Creado Por: Magno Sanchez
        'Descripcion: funcion que genera el dataset y realiza la carga de datos.
        '**********************************************************************************************************************************************************************
        Public Shared Function getDataSetListadoCuotasFinCierre(ByVal id As String, ByVal modalidad As String, ByVal situacionTrabajador As String) As DsListadoCuotasFinCierre
            Dim ds As New DsListadoCuotasFinCierre()
            Dim ds3 As DataSet
            'Dim dt As DataTable
            Dim Codigo_proceso As String
            Dim oCliente As New Cliente()

            Dim TipoDocumento As String = ""
            Dim NumeroDocumento As String = ""
            Dim Anio_periodo As String = ""
            Dim Mes_Periodo As String = ""

            ds.Reset()
            Codigo_proceso = id
            Dim dr As SqlDataReader = oCliente.GetInfoClienteProceso(Codigo_proceso)
            If dr.Read Then
                TipoDocumento = CType(dr("TipoDocumento"), String)
                NumeroDocumento = CType(dr("NumeroDocumento"), String)
                Anio_periodo = CType(dr("Anio_periodo"), String)
                Mes_Periodo = CType(dr("Mes_Periodo"), String)
            End If

            Dim ds2 As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)
            ds.Tables.Add(ds2.Tables(0).Copy)
            'ds3 = oCliente.GetListadoCuotasPorVencerCliente(Codigo_proceso)
            ds3 = oCliente.GetListadoCuotasPorVencerCliente2(Codigo_proceso, modalidad, situacionTrabajador)

            'Copiamos la lista de cuotas
            ds3.Tables(0).TableName = "Cliente"
            ds.Tables.Add(ds3.Tables(0).Copy)
            Return ds
        End Function
        '*****************************************************************************************************************************************************************************

        'END
    End Class
End Namespace

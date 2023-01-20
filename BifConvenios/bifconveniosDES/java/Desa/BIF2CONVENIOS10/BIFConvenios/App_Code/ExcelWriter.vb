Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.POIFS.FileSystem

Public Class ExcelWriter
    Private Shared hssfworkbook As HSSFWorkbook
    Private strRutaPlantilla As String = String.Empty

    Sub New(ByVal strRutaPlantilla As String)
        Me.strRutaPlantilla = strRutaPlantilla
        InicializaWorkBook()
    End Sub

    Public Sub GenerarArchivoExcel(ByVal oDatos As DataTable, ByVal strRutaArchivo As String)

        Dim sheet1 As HSSFSheet = hssfworkbook.GetSheet("Datos")

        Dim iFila As Integer = 1

        For Each Item As DataRow In oDatos.Rows

            sheet1.CreateRow(iFila)

            sheet1.GetRow(iFila).CreateCell(0).SetCellValue(Long.Parse(Item("CODIGO_CLIENTE").ToString))
            sheet1.GetRow(iFila).CreateCell(1).SetCellValue(Integer.Parse(Item("ANIO_PERIODO").ToString))
            sheet1.GetRow(iFila).CreateCell(2).SetCellValue(Integer.Parse(Item("MES_PERIODO").ToString))

            sheet1.GetRow(iFila).CreateCell(3).SetCellValue(Item("BANDES").ToString)
            sheet1.GetRow(iFila).CreateCell(4).SetCellValue(Item("DLCTA").ToString)
            sheet1.GetRow(iFila).CreateCell(5).SetCellValue(Item("FECGEN").ToString)

            sheet1.GetRow(iFila).CreateCell(6).SetCellValue(Item("CODINS").ToString)
            sheet1.GetRow(iFila).CreateCell(7).SetCellValue(Item("PLDNPR").ToString)
            sheet1.GetRow(iFila).CreateCell(8).SetCellValue(Long.Parse(Item("DLNP").ToString))
            sheet1.GetRow(iFila).CreateCell(9).SetCellValue(Item("FINICRE").ToString)
            sheet1.GetRow(iFila).CreateCell(10).SetCellValue(Item("DLCUN").ToString)
            sheet1.GetRow(iFila).CreateCell(11).SetCellValue(Item("DLST").ToString)
            sheet1.GetRow(iFila).CreateCell(12).SetCellValue(Item("DLDNI").ToString)
            sheet1.GetRow(iFila).CreateCell(13).SetCellValue(Item("PLDFDE").ToString)
            sheet1.GetRow(iFila).CreateCell(14).SetCellValue(Item("MODALIDAD").ToString)
            sheet1.GetRow(iFila).CreateCell(15).SetCellValue(Item("DLCEM").ToString)
            sheet1.GetRow(iFila).CreateCell(16).SetCellValue(Item("DLNE").ToString)
            sheet1.GetRow(iFila).CreateCell(17).SetCellValue(Item("DLMO").ToString)
            sheet1.GetRow(iFila).CreateCell(18).SetCellValue(Double.Parse(Item("DLOAM").ToString))
            sheet1.GetRow(iFila).CreateCell(19).SetCellValue(Item("DLPRI").ToString)
            sheet1.GetRow(iFila).CreateCell(20).SetCellValue(Item("DLCNC1").ToString)
            sheet1.GetRow(iFila).CreateCell(21).SetCellValue(Item("DLNCT").ToString)
            sheet1.GetRow(iFila).CreateCell(22).SetCellValue(Item("CPENDIENTES").ToString)
            sheet1.GetRow(iFila).CreateCell(23).SetCellValue(Item("PLCCD").ToString)
            sheet1.GetRow(iFila).CreateCell(24).SetCellValue(Item("FECHACARGOCUENTA").ToString)
            sheet1.GetRow(iFila).CreateCell(25).SetCellValue(Item("DLITF").ToString)
            sheet1.GetRow(iFila).CreateCell(26).SetCellValue(Double.Parse(Item("DLIC").ToString))
            sheet1.GetRow(iFila).CreateCell(27).SetCellValue(Double.Parse(Item("CUODES").ToString))

            iFila += 1
        Next

        WriteToFile(strRutaArchivo)

    End Sub

    Public Sub GenerarExcelConsultarEstadoEmpresa(ByVal oDatos As DataTable, ByVal strRutaArchivo As String)

        Dim sheet1 As HSSFSheet = hssfworkbook.GetSheet("Datos")

        Dim iFila As Integer = 1

        For Each Item As DataRow In oDatos.Rows

            sheet1.CreateRow(iFila)

            sheet1.GetRow(iFila).CreateCell(0).SetCellValue(Item("codigo_IBS").ToString)
            sheet1.GetRow(iFila).CreateCell(1).SetCellValue(Item("Nombre_cliente").ToString)
            sheet1.GetRow(iFila).CreateCell(2).SetCellValue(Item("Fecha_Envio_Planilla").ToString)
            sheet1.GetRow(iFila).CreateCell(3).SetCellValue(Item("Fecha_Pago").ToString)
            sheet1.GetRow(iFila).CreateCell(4).SetCellValue(Item("Meses_Anticipacion").ToString)
            sheet1.GetRow(iFila).CreateCell(5).SetCellValue(Item("Fecha_Enviado").ToString)
            sheet1.GetRow(iFila).CreateCell(6).SetCellValue(Item("Fecha_Recibido").ToString)
            sheet1.GetRow(iFila).CreateCell(7).SetCellValue(Item("Fecha_Procesado").ToString)
            sheet1.GetRow(iFila).CreateCell(8).SetCellValue(Item("Fecha_PostConciliacion").ToString)

            iFila += 1
        Next

        WriteToFile(strRutaArchivo)

    End Sub

    Public Sub GenerarExcelEfectividadEmpresa(ByVal oDatos As DataTable, ByVal strRutaArchivo As String)
        Dim sheet1 As HSSFSheet = hssfworkbook.GetSheet("Datos")
        sheet1.GetRow(0).GetCell(1).SetCellValue(oDatos.Rows(0).Item("Nombre_empresa").ToString())
        sheet1.GetRow(1).GetCell(0).SetCellValue(oDatos.Rows(0).Item("codigo_clienteIBS").ToString())

        Dim iFila As Integer = 3

        For Each Item As DataRow In oDatos.Rows

            sheet1.CreateRow(iFila)

            sheet1.GetRow(iFila).CreateCell(0).SetCellValue(Item("AnioMes").ToString)
            sheet1.GetRow(iFila).CreateCell(1).SetCellValue(Item("nro_clientes_enviado").ToString)
            sheet1.GetRow(iFila).CreateCell(2).SetCellValue(Item("nro_monto_enviado_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(3).SetCellValue(Item("nro_monto_enviado_dolares").ToString)
            sheet1.GetRow(iFila).CreateCell(4).SetCellValue(Item("nro_clientes_retornado").ToString)
            sheet1.GetRow(iFila).CreateCell(5).SetCellValue(Item("nro_monto_retornado_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(6).SetCellValue(Item("nro_monto_retornado_dolares").ToString)
            sheet1.GetRow(iFila).CreateCell(7).SetCellValue(Item("efectividad_nro_clientes").ToString)
            sheet1.GetRow(iFila).CreateCell(8).SetCellValue(Item("efectividad_monto_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(9).SetCellValue(Item("efectividad_monto_dolares").ToString)
            iFila += 1
        Next

        WriteToFile(strRutaArchivo)
    End Sub

    Public Sub GenerarExcelEfectividadMes(ByVal oDatos As DataTable, ByVal strRutaArchivo As String _
    , ByVal suma_clientes_envio As Double, ByVal suma_soles_envio As Double, ByVal suma_dolares_envio As Double _
    , ByVal suma_clientes_retorno As Double, ByVal suma_soles_retorno As Double, ByVal suma_dolares_retorno As Double)

        Dim sheet1 As HSSFSheet = hssfworkbook.GetSheet("Datos")
        sheet1.GetRow(0).GetCell(2).SetCellValue(oDatos.Rows(0).Item("AnioMes").ToString())

        Dim iFila As Integer = 3

        For Each Item As DataRow In oDatos.Rows

            sheet1.CreateRow(iFila)

            sheet1.GetRow(iFila).CreateCell(0).SetCellValue(Item("codigo_clienteIBS").ToString)
            sheet1.GetRow(iFila).CreateCell(1).SetCellValue(Item("Nombre_empresa").ToString)
            sheet1.GetRow(iFila).CreateCell(2).SetCellValue(Item("nro_clientes_enviado").ToString)
            sheet1.GetRow(iFila).CreateCell(3).SetCellValue(Item("nro_monto_enviado_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(4).SetCellValue(Item("nro_monto_enviado_dolares").ToString)
            sheet1.GetRow(iFila).CreateCell(5).SetCellValue(Item("nro_clientes_retornado").ToString)
            sheet1.GetRow(iFila).CreateCell(6).SetCellValue(Item("nro_monto_retornado_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(7).SetCellValue(Item("nro_monto_retornado_dolares").ToString)
            sheet1.GetRow(iFila).CreateCell(8).SetCellValue(Item("efectividad_nro_clientes").ToString)
            sheet1.GetRow(iFila).CreateCell(9).SetCellValue(Item("efectividad_monto_soles").ToString)
            sheet1.GetRow(iFila).CreateCell(10).SetCellValue(Item("efectividad_monto_dolares").ToString)
            iFila += 1
        Next

        sheet1.CreateRow(iFila)
        sheet1.GetRow(iFila).CreateCell(0).SetCellValue("")
        sheet1.GetRow(iFila).CreateCell(1).SetCellValue("TOTAL")
        sheet1.GetRow(iFila).CreateCell(2).SetCellValue(suma_clientes_envio)
        sheet1.GetRow(iFila).CreateCell(3).SetCellValue(suma_soles_envio)
        sheet1.GetRow(iFila).CreateCell(4).SetCellValue(suma_dolares_envio)
        sheet1.GetRow(iFila).CreateCell(5).SetCellValue(suma_clientes_retorno)
        sheet1.GetRow(iFila).CreateCell(6).SetCellValue(suma_soles_retorno)
        sheet1.GetRow(iFila).CreateCell(7).SetCellValue(suma_dolares_retorno)
        sheet1.GetRow(iFila).CreateCell(8).SetCellValue("")
        sheet1.GetRow(iFila).CreateCell(9).SetCellValue("")
        sheet1.GetRow(iFila).CreateCell(10).SetCellValue("")

        WriteToFile(strRutaArchivo)
    End Sub

    Public Sub GenerarExcelConsultaEstadoEnvio(ByVal oDatos As DataTable, ByVal strRutaArchivo As String, ByVal Empresa As String)

        Dim sheet1 As HSSFSheet = hssfworkbook.GetSheet("Datos")

        Dim iFila As Integer = 1

        For Each Item As DataRow In oDatos.Rows

            sheet1.CreateRow(iFila)
            sheet1.GetRow(iFila).CreateCell(0).SetCellValue(Item("CodigoBanco").ToString)
            sheet1.GetRow(iFila).CreateCell(1).SetCellValue(Empresa)
            sheet1.GetRow(iFila).CreateCell(2).SetCellValue(Item("NombreTrabajador").ToString)
            sheet1.GetRow(iFila).CreateCell(3).SetCellValue(Item("NumeroPagare").ToString)
            sheet1.GetRow(iFila).CreateCell(4).SetCellValue(Item("MontoDescuento").ToString)
            sheet1.GetRow(iFila).CreateCell(5).SetCellValue(Item("CodigoNombre").ToString)
            iFila += 1
        Next

        WriteToFileExport(strRutaArchivo)

    End Sub

    Shared Sub WriteToFile(ByVal strRutaArchivo As String)
        Dim oArchivo As New System.IO.FileStream(strRutaArchivo, IO.FileMode.Create)
        hssfworkbook.Write(oArchivo)
        oArchivo.Flush()
        oArchivo.Dispose()
        oArchivo.Close()
    End Sub
    Shared Sub WriteToFileExport(ByVal strRutaArchivo As String)
        Dim oArchivo As New System.IO.FileStream(strRutaArchivo, IO.FileMode.Create)
        hssfworkbook.Write(oArchivo)
        oArchivo.Flush()
        oArchivo.Close()
    End Sub

    Protected Sub InicializaWorkBook()
        Dim oArchivo As New System.IO.FileStream(strRutaPlantilla, IO.FileMode.Open, IO.FileAccess.Read)
        hssfworkbook = New HSSFWorkbook(oArchivo)

        '//create a entry of DocumentSummaryInformation
        Dim dsi As DocumentSummaryInformation = PropertySetFactory.CreateDocumentSummaryInformation()
        dsi.Company = "BANBIF TI"
        hssfworkbook.DocumentSummaryInformation = dsi

        '//create a entry of SummaryInformation
        Dim si As SummaryInformation = PropertySetFactory.CreateSummaryInformation()
        si.Subject = "BANBIF"
        hssfworkbook.SummaryInformation = si
    End Sub
End Class

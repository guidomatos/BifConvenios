Imports System.IO
Imports System.Text
'ADD NCA 20/06/2014 REQ: EA2013-273
Imports System.Data.OleDb
Imports System.Configuration
'END

Public Class ArchivoXLS

    Public Sub ExportaXLS(ByVal myDS As DataSet, ByVal ruta As String, ByVal strFile As String)
        'BIFUtils.xlsRes
        'Dim m_objRes As System.Resources.ResourceManager = New System.Resources.ResourceManager("BIFUtils.resx", Me.GetType.Assembly)
        Dim sb As StringBuilder = New StringBuilder() '   para contener el archivo XLS
        Dim j As Integer
        Dim k As Integer
        Dim l As Integer

        Dim replVal As String = ""
        WS.Utils.RemoveFiles(ruta, New TimeSpan(0, 0, 60, 0, 0))
        'Crear el encabezado y la hoja...
        'String quoter  = "\"";
        'Iniciamos el encabezado del xls
        Dim BeginXls As String = "&lt;html xmlns:o=""urn:schemas-microsoft-com:office:office""xmlns:  x = ""urn:schemas-microsoft-com:office:excel""xmlns=""http://www.w3.org/TR/REC-html40""&gt;"
        BeginXls = BeginXls.Replace("&lt;", "<")
        BeginXls = BeginXls.Replace("&gt;", ">")
        sb.Append(BeginXls)
        'sb.Append(m_objRes.GetString("BeginXls"))
        Dim HeaderXls As String = "&lt;head&gt;" & vbCrLf & _
        "&lt;meta http-equiv=Content-Type content=""text/html; charset=windows-1252""&gt;" & vbCrLf & _
        "&lt;meta name=ProgId content=Excel.Sheet&gt;" & vbCrLf & _
        "&lt;meta name=Generator content=""Microsoft Excel 10""&gt;" & vbCrLf & _
        "&lt;!--[if gte mso 9]&gt;&lt;xml&gt;" & vbCrLf & _
        "&lt;o:DocumentProperties&gt;" & vbCrLf & _
        "&lt;o:Author&gt;Administrator&lt;/o:Author&gt;" & vbCrLf & _
        "&lt;o:LastAuthor&gt;Administrator&lt;/o:LastAuthor&gt;" & vbCrLf & _
        "&lt;o:Created&gt;2006-10-21T19:50:04Z&lt;/o:Created&gt;" & vbCrLf & _
        "&lt;o:LastSaved&gt;2006-10-21T20:18:54Z&lt;/o:LastSaved&gt;" & vbCrLf & _
        "&lt;o:Company&gt;Microsoft Corporation&lt;/o:Company&gt;" & vbCrLf & _
        "&lt;o:Version&gt;10.3501&lt;/o:Version&gt;!" & vbCrLf & _
        "&lt;/o:DocumentProperties&gt;" & vbCrLf & _
        "&lt;o:OfficeDocumentSettings&gt;" & vbCrLf & _
        "&lt;o:DownloadComponents/&gt;" & vbCrLf & _
        "&lt;o:LocationOfComponents HRef="" / ""/&gt;" & vbCrLf & _
        "&lt;/o:OfficeDocumentSettings&gt;" & vbCrLf & _
        "&lt;/xml&gt;&lt;![endif]--&gt;" & vbCrLf & _
        "&lt;style&gt;" & vbCrLf & _
        "&lt;!--table" & vbCrLf & _
        "{mso-displayed-decimal-separator:""\."";" & vbCrLf & _
        "mso-displayed-thousand-separator:""\,"";}" & vbCrLf & _
        "@page" & vbCrLf & _
        "{margin:1.0in .75in 1.0in .75in;" & vbCrLf & _
        "mso-header-margin:.5in;" & vbCrLf & _
        "mso-footer-margin:.5in;}" & vbCrLf & _
        "tr" & vbCrLf & _
        "{mso-height-source:auto;}" & vbCrLf & _
        "col" & vbCrLf & _
        "{mso-width-source:auto;}" & vbCrLf & _
        "br" & vbCrLf & _
        "{mso-data-placement:same-cell;}" & vbCrLf & _
        ".style0" & vbCrLf & _
 "{mso-number-format:General;" & vbCrLf & _
 "text-align:general;" & vbCrLf & _
 "vertical-align:bottom;" & vbCrLf & _
 "white-space:nowrap;" & vbCrLf & _
 "mso-rotate:0;" & vbCrLf & _
 "mso-background-source:auto;" & vbCrLf & _
 "mso-pattern:auto;" & vbCrLf & _
 "color:windowtext;" & vbCrLf & _
 "font-size:10.0pt;" & vbCrLf & _
 "font-weight:400;" & vbCrLf & _
 "font-style:normal;" & vbCrLf & _
 "text-decoration:none;" & vbCrLf & _
 "font-family:Arial;" & vbCrLf & _
 "mso-generic-font-family:auto;" & vbCrLf & _
 "mso-font-charset:0;" & vbCrLf & _
 "border:none;" & vbCrLf & _
 "mso-protection:locked visible;" & vbCrLf & _
 "mso-style-name:Normal;" & vbCrLf & _
 "mso-style-id:0;}" & vbCrLf & _
        "td" & vbCrLf & _
 "{mso-style-parent:style0;" & vbCrLf & _
 "padding-top:1px;" & vbCrLf & _
 "padding-right:1px;" & vbCrLf & _
 "padding-left:1px;" & vbCrLf & _
 "mso-ignore:padding;" & vbCrLf & _
 "color:windowtext;" & vbCrLf & _
 "font-size:10.0pt;" & vbCrLf & _
 "font-weight:400;" & vbCrLf & _
 "font-style:normal;" & vbCrLf & _
 "text-decoration:none;" & vbCrLf & _
 "font-family:Arial;" & vbCrLf & _
 "mso-generic-font-family:auto;" & vbCrLf & _
 "mso-font-charset:0;" & vbCrLf & _
 "mso-number-format:""\@"";" & vbCrLf & _
 "text-align:general;" & vbCrLf & _
 "vertical-align:bottom;" & vbCrLf & _
 "border:none;" & vbCrLf & _
 "mso-background-source:auto;" & vbCrLf & _
 "mso-pattern:auto;" & vbCrLf & _
 "mso-protection:locked visible;" & vbCrLf & _
 "white-space:nowrap;" & vbCrLf & _
 "mso-rotate:0;}" & vbCrLf & _
        ".xl24()" & vbCrLf & _
 "{mso-style-parent:style0;" & vbCrLf & _
 "mso-number-format:0;}" & vbCrLf & _
"--&gt;" & vbCrLf & _
"&lt;/style&gt;" & vbCrLf & _
"&lt;!--[if gte mso 9]&gt;&lt;xml&gt;" & vbCrLf & _
 "&lt;x:ExcelWorkbook&gt;" & vbCrLf & _
  "&lt;x:ExcelWorksheets&gt;" & vbCrLf & _
   "&lt;x:ExcelWorksheet&gt;" & vbCrLf & _
    "&lt;x:Name&gt;Sheet1&lt;/x:Name&gt;" & vbCrLf & _
    "&lt;x:WorksheetOptions&gt;" & vbCrLf & _
     "&lt;x:Print&gt;" & vbCrLf & _
      "&lt;x:ValidPrinterInfo/&gt;" & vbCrLf & _
      "&lt;x:HorizontalResolution&gt;1200&lt;/x:HorizontalResolution&gt;" & vbCrLf & _
      "&lt;x:VerticalResolution&gt;1200&lt;/x:VerticalResolution&gt;" & vbCrLf & _
     "&lt;/x:Print&gt;" & vbCrLf & _
     "&lt;x:Selected/&gt;" & vbCrLf & _
     "&lt;x:Panes&gt;" & vbCrLf & _
      "&lt;x:Pane&gt;" & vbCrLf & _
       "&lt;x:Number&gt;3&lt;/x:Number&gt;" & vbCrLf & _
       "&lt;x:ActiveCol&gt;1&lt;/x:ActiveCol&gt;" & vbCrLf & _
      "&lt;/x:Pane&gt;" & vbCrLf & _
     "&lt;/x:Panes&gt;" & vbCrLf & _
     "&lt;x:ProtectContents&gt;False&lt;/x:ProtectContents&gt;" & vbCrLf & _
     "&lt;x:ProtectObjects&gt;False&lt;/x:ProtectObjects&gt;" & vbCrLf & _
     "&lt;x:ProtectScenarios&gt;False&lt;/x:ProtectScenarios&gt;" & vbCrLf & _
    "&lt;/x:WorksheetOptions&gt;" & vbCrLf & _
   "&lt;/x:ExcelWorksheet&gt;" & vbCrLf & _
  "&lt;/x:ExcelWorksheets&gt;" & vbCrLf & _
  "&lt;x:WindowHeight&gt;8385&lt;/x:WindowHeight&gt;" & vbCrLf & _
  "&lt;x:WindowWidth&gt;14940&lt;/x:WindowWidth&gt;" & vbCrLf & _
  "&lt;x:WindowTopX&gt;360&lt;/x:WindowTopX&gt;" & vbCrLf & _
  "&lt;x:WindowTopY&gt;240&lt;/x:WindowTopY&gt;" & vbCrLf & _
  "&lt;x:ProtectStructure&gt;False&lt;/x:ProtectStructure&gt;" & vbCrLf & _
  "&lt;x:ProtectWindows&gt;False&lt;/x:ProtectWindows&gt;" & vbCrLf & _
 "&lt;/x:ExcelWorkbook&gt;" & vbCrLf & _
"&lt;/xml&gt;&lt;![endif]--&gt;" & vbCrLf & _
"&lt;/head&gt;"
        HeaderXls = HeaderXls.Replace("&lt;", "<")
        HeaderXls = HeaderXls.Replace("&gt;", ">")

        sb.Append(HeaderXls)
        'sb.Append(m_objRes.GetString("HeaderXls"))
        Dim BeginBodyXls As String = "&lt;body link=blue vlink=purple&gt;"
        BeginBodyXls = BeginBodyXls.Replace("&lt;", "<")
        BeginBodyXls = BeginBodyXls.Replace("&gt;", ">")

        sb.Append(BeginBodyXls)
        'sb.Append(m_objRes.GetString("BeginBodyXls"))
        Dim BeginTable As String = "&lt;table x:str border=0 cellpadding=0 cellspacing=0 width=177 style='border-collapse:" & vbCrLf & _
                                    "collapse;table-layout:fixed;width:133pt'&gt;"
        BeginTable = BeginTable.Replace("&lt;", "<")
        BeginTable = BeginTable.Replace("&gt;", ">")
        sb.Append(BeginTable)
        'sb.Append(m_objRes.GetString("BeginTable"))


        Dim BeginNormalTD As String = "&lt;td&gt;"
        BeginNormalTD = BeginNormalTD.Replace("&lt;", "<")
        BeginNormalTD = BeginNormalTD.Replace("&gt;", ">")

        Dim EndNormalTD As String = "&lt;/td&gt;"
        EndNormalTD = EndNormalTD.Replace("&lt;", "<")
        EndNormalTD = EndNormalTD.Replace("&gt;", ">")

        Dim BeginNormalTR As String = "&lt;tr&gt;"
        BeginNormalTR = BeginNormalTR.Replace("&lt;", "<")
        BeginNormalTR = BeginNormalTR.Replace("&gt;", ">")

        Dim EndNormalTR As String = "&lt;/tr&gt;"
        EndNormalTR = EndNormalTR.Replace("&lt;", "<")
        EndNormalTR = EndNormalTR.Replace("&gt;", ">")

        Dim EndTable As String = "&lt;/table&gt;"
        EndTable = EndTable.Replace("&lt;", "<")
        EndTable = EndTable.Replace("&gt;", ">")

        Dim EndBodyXls As String = "&lt;/body&gt;"
        EndBodyXls = EndBodyXls.Replace("&lt;", "<")
        EndBodyXls = EndBodyXls.Replace("&gt;", ">")

        Dim EndXls As String = "&lt;/html&gt;"
        EndXls = EndXls.Replace("&lt;", "<")
        EndXls = EndXls.Replace("&gt;", ">")

        'Generación de los headers 
        For j = 0 To myDS.Tables(0).Columns.Count - 1
            'sb.Append(m_objRes.GetString("BeginNormalTD"))
            sb.Append(BeginNormalTD)
            sb.Append(myDS.Tables(0).Columns(j).ColumnName.ToString())   '//headings
            'sb.Append(m_objRes.GetString("EndNormalTD"))
            sb.Append(EndNormalTD)
        Next

        sb.Append(Environment.NewLine)

        For l = 0 To myDS.Tables(0).Rows.Count - 1
            'sb.Append(m_objRes.GetString("BeginNormalTR"))
            sb.Append(BeginNormalTR)
            '//for (k = 0 ;k<= myReader.FieldCount - 1; k++)
            For k = 0 To myDS.Tables(0).Columns.Count - 1
                'sb.Append(m_objRes.GetString("BeginNormalTD"))
                sb.Append(BeginNormalTD)
                '//if( myReader.GetValue(k).ToString() == null )
                If (myDS.Tables(0).Rows(l)(k) Is Nothing) Then
                    '//sb.Append(myReader.GetValue(k).ToString());   
                    sb.Append(myDS.Tables(0).Rows(l)(k).ToString())
                Else
                    '//replVal= myReader.GetValue(k).ToString();
                    replVal = myDS.Tables(0).Rows(l)(k).ToString()
                    sb.Append(replVal)
                End If
                'sb.Append(m_objRes.GetString("EndNormalTD"))
                sb.Append(EndNormalTD)
            Next
            'sb.Append(m_objRes.GetString("EndNormalTR"))
            sb.Append(EndNormalTR)
            '//sb.Append("\r\n");
            sb.Append(Environment.NewLine)
        Next
        'sb.Append(m_objRes.GetString("EndTable"))
        sb.Append(EndTable)
        'sb.Append(m_objRes.GetString("EndBodyXls"))
        sb.Append(EndBodyXls)
        'sb.Append(m_objRes.GetString("EndXls"))
        sb.Append(EndXls)

        '//myReader.Close();
        '//myReader = null;

        Dim strFileContent As String = sb.ToString()
        Dim fi As FileInfo = New FileInfo(ruta + strFile) 'System.Web.HttpContext.Current.Server.MapPath(ruta + strFile))
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing
    End Sub

    'ADD NCA 20/06/2014
    'REQ: EA2013-273 CREACION ARCHIVOS EXCEL PARA DESCUENTOS / IMPORTACION
    Public Function ImportaXLS(ByVal archivo As String) As DataTable
        Dim cs As String = TraerCadenaConexion(archivo)
        Dim oConn As New OleDbConnection
        Dim oCmd As New OleDbCommand
        Dim oDa As New OleDbDataAdapter
        Dim dsExcel As New DataSet("Resultado")
        oConn.ConnectionString = cs
        Try
            oConn.Open()
            'oCmd.CommandText = "SELECT * FROM [Sheet1$]"
            oCmd.CommandText = ConfigurationManager.AppSettings("SqlExcel")
            oCmd.Connection = oConn
            oDa.SelectCommand = oCmd
            oDa.Fill(dsExcel)

        Catch ex As Exception
            Throw ex
        Finally
            oConn.Close()
        End Try

        Dim i As Integer = 0
        For i = 0 To dsExcel.Tables(0).Columns.Count - 1
            dsExcel.Tables(0).Columns(i).ColumnName = dsExcel.Tables(0).Rows(0).Item(i).ToString
        Next
        dsExcel.Tables(0).Rows.RemoveAt(0)
        Return dsExcel.Tables(0)
    End Function

    Public Function FiltraSoloRegistrosXLS(ByVal dtExcel As DataTable) As DataTable
        Dim _dtResult As New DataTable("Resultado")
        Dim _drFila As DataRow = Nothing
        Dim intColumns As Integer = 0

        Try
            '1. Conseguiendo numero de columnas
            intColumns = dtExcel.Columns.Count

            '2. Inicializando Datatable de Retorno
            _dtResult.Columns.Add("CodigoBanco", GetType(String))
            _dtResult.Columns.Add("Moneda", GetType(String))
            _dtResult.Columns.Add("NumeroPagare", GetType(String))
            _dtResult.Columns.Add("CodigoModular", GetType(String))
            _dtResult.Columns.Add("NombreTrabajador", GetType(String))
            _dtResult.Columns.Add("CodigoReferencia", GetType(String))
            _dtResult.Columns.Add("Anio", GetType(String))
            _dtResult.Columns.Add("Mes", GetType(String))
            _dtResult.Columns.Add("Cuota", GetType(String))
            _dtResult.Columns.Add("SituacionLaboral", GetType(String))
            _dtResult.Columns.Add("MontoDescuento", GetType(String))

            '3. Recorriendo Datatable Excel
            For Each _drFila In dtExcel.Rows
                Dim _dr As DataRow = _dtResult.NewRow()
                Dim strMontoDescuento As String = "0"
                Dim strCodigoModular As String = String.Empty
                Dim lstrCodigoModular As String = ""
                Try
                    If _drFila("CuotaDescontada").ToString().Trim <> String.Empty Then
                        strMontoDescuento = _drFila("CuotaDescontada").ToString().Trim
                    End If

                    If _drFila("CodigoTrabajador").ToString().Trim <> String.Empty Then

                        strCodigoModular = _drFila("CodigoTrabajador").ToString().Trim

                    End If

                    Dim strAnio As String = _drFila("Anio").ToString().Trim
                    Dim strMes As String = _drFila("Mes").ToString().Trim

                    'Seteo de valores
                    _dr("CodigoBanco") = "BIF"
                    _dr("Moneda") = _drFila("Moneda").ToString.Trim
                    _dr("NumeroPagare") = _drFila("NumeroPrestamo").ToString.Trim
                    _dr("CodigoModular") = strCodigoModular '""
                    _dr("NombreTrabajador") = _drFila("NombreTrabajador").ToString.Trim
                    _dr("CodigoReferencia") = ""
                    _dr("Anio") = strAnio
                    _dr("Mes") = strMes
                    _dr("Cuota") = _drFila("ImporteCuota").ToString.Trim
                    _dr("SituacionLaboral") = ""
                    _dr("MontoDescuento") = strMontoDescuento

                    'Agrega fila
                    _dtResult.Rows.Add(_dr)
                Catch ex As Exception

                End Try
            Next

            Return _dtResult

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function FiltraSoloRegistrosXLS(ByVal dtExcel As DataTable, ByVal strNombreCampos() As String) As DataTable
        Dim dtRet As New DataTable("Resultado")
        Dim drFila As DataRow = Nothing
        Dim intColumnas As Int16 = 0

        Try
            '1. Conseguiendo numero de columnas
            intColumnas = dtExcel.Columns.Count

            '2. Inicializando Datatable de Retorno
            dtRet.Columns.Add("CodigoBanco", GetType(String))
            dtRet.Columns.Add("Moneda", GetType(String))
            dtRet.Columns.Add("NumeroPagare", GetType(String))
            dtRet.Columns.Add("CodigoModular", GetType(String))
            dtRet.Columns.Add("NombreTrabajador", GetType(String))
            dtRet.Columns.Add("CodigoReferencia", GetType(String))
            dtRet.Columns.Add("Anio", GetType(String))
            dtRet.Columns.Add("Mes", GetType(String))
            dtRet.Columns.Add("Cuota", GetType(String))
            dtRet.Columns.Add("SituacionLaboral", GetType(String))
            dtRet.Columns.Add("MontoDescuento", GetType(String))

            '3. Recorriendo Datatable Excel
            For Each drFila In dtExcel.Rows

                If drFila(strNombreCampos(3)).ToString.Trim <> String.Empty AndAlso _
                drFila(strNombreCampos(4)).ToString.Trim <> String.Empty AndAlso _
                drFila(strNombreCampos(7)).ToString.Trim <> String.Empty AndAlso _
                drFila(strNombreCampos(1)).ToString.Trim <> String.Empty AndAlso _
                drFila(strNombreCampos(0)).ToString.Trim <> String.Empty Then
                    Dim dr As DataRow = dtRet.NewRow()
                    Dim strMontoDescuento As String = "0"
                    Dim strCodigoModular = String.Empty

                    Try

                        'MontoDescuento
                        If (drFila(strNombreCampos(0)).ToString.Trim <> String.Empty) Then strMontoDescuento = drFila(strNombreCampos(0)).ToString.Trim

                        'Fecha y Año
                        Dim strAnio As String = String.Empty
                        Dim strMes As String = String.Empty
                        'Dim strFecha() As String

                        'If drFila(strNombreCampos(1)).ToString <> String.Empty Then
                        '    Try
                        '        strFecha = drFila(strNombreCampos(1)).ToString.Split("/"c)
                        '        strAnio = strFecha(2)
                        '        strMes = strFecha(1)
                        '    Catch ex As Exception

                        '    End Try
                        'End If

                        strAnio = drFila(strNombreCampos(9)).ToString.Trim
                        strMes = drFila(strNombreCampos(10)).ToString.Trim

                        'Codigo Modular
                        If (drFila(strNombreCampos(2)).ToString.Trim <> String.Empty) Then strCodigoModular = drFila(strNombreCampos(2)).ToString.Trim

                        'Seteo de valores
                        dr("CodigoBanco") = "BIF"
                        dr("Moneda") = drFila(strNombreCampos(3)).ToString.Trim
                        dr("NumeroPagare") = drFila(strNombreCampos(4)).ToString.Trim
                        dr("CodigoModular") = drFila(strNombreCampos(2)).ToString.Trim
                        dr("NombreTrabajador") = drFila(strNombreCampos(5)).ToString.Trim
                        dr("CodigoReferencia") = drFila(strNombreCampos(6)).ToString.Trim
                        dr("Anio") = strAnio
                        dr("Mes") = strMes
                        dr("Cuota") = drFila(strNombreCampos(7)).ToString.Trim
                        dr("SituacionLaboral") = drFila(strNombreCampos(8)).ToString.Trim
                        dr("MontoDescuento") = strMontoDescuento

                        'Agrega fila
                        dtRet.Rows.Add(dr)

                    Catch ex As Exception

                    End Try

                End If

            Next

            Return dtRet

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function TraerCadenaConexion(ByVal archivo As String) As String
        Dim cadena As String
        'cadena = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0", archivo)
        'cadena = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text'", archivo)
        cadena = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1;HDR=Yes;'", archivo)
        Return cadena
    End Function
    'END ADD
End Class

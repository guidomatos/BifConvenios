Imports System.Data
Imports System.IO
Imports System.Text

Public Class ArchivoTXT

    Public Sub ExportaTXT(ByRef pData As DataSet, ByVal ruta As String, ByVal strFile As String) ', ByVal virtualPath As String)  'As String
        Dim ldr As DataRow
        Dim lNroColumnas As Integer

        Dim sb As New StringBuilder()   ' para contener el archivo CSV
        'Dim j As Integer
        Dim k As Integer
        Dim replVal As String = String.Empty

        WS.Utils.RemoveFiles(ruta, New TimeSpan(0, 0, 60, 0, 0))

        lNroColumnas = pData.Tables(0).Columns.Count
        For Each ldr In pData.Tables(0).Rows
            For k = 0 To lNroColumnas - 1
                sb.Append(ldr(k).ToString())
            Next
            sb.Append(vbCrLf)
        Next

        Dim strFileContent As String = sb.ToString()

        Dim fi As New FileInfo(ruta + strFile)
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        'sWriter.Write(System.Text.Encoding.ASCII.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing

    End Sub

    Public Function ImportaTXTEncolumnado(ByVal pRutaArchivo As String) As DataTable
        Dim ldt As DataTable = New DataTable("Result")
        Dim ldr As DataRow
        Dim ldata As String = ""

        Dim lStreamReader As IO.StreamReader
        lStreamReader = New IO.StreamReader(pRutaArchivo, System.Text.Encoding.Default)
        Try

            'Aqui añadimos las columnas
            ldt.Columns.Add(New DataColumn("CodigoBanco"))
            ldt.Columns.Add(New DataColumn("Moneda"))
            ldt.Columns.Add(New DataColumn("NumeroPagare"))
            ldt.Columns.Add(New DataColumn("CodigoModular"))
            ldt.Columns.Add(New DataColumn("NombreTrabajador"))
            ldt.Columns.Add(New DataColumn("CodigoReferencia"))
            ldt.Columns.Add(New DataColumn("Anio"))
            ldt.Columns.Add(New DataColumn("Mes"))
            ldt.Columns.Add(New DataColumn("Cuota"))
            ldt.Columns.Add(New DataColumn("SituacionLaboral"))
            ldt.Columns.Add(New DataColumn("MontoDescuento"))

            ldata = lStreamReader.ReadLine
            Do While (Not ldata Is Nothing)
                ldr = ldt.NewRow
                ldr("CodigoBanco") = ldata.Substring(0, 12)
                ldr("Moneda") = ldata.Substring(12, 3)
                ldr("NumeroPagare") = ldata.Substring(15, 12)
                ldr("CodigoModular") = ldata.Substring(27, 20)
                ldr("NombreTrabajador") = ldata.Substring(47, 75)
                ldr("CodigoReferencia") = ldata.Substring(122, 20)
                ldr("Anio") = ldata.Substring(142, 4)
                ldr("Mes") = ldata.Substring(146, 2)
                ldr("Cuota") = CStr(CInt(ldata.Substring(148, 12))) & "." & ldata.Substring(160, 2)
                ldr("SituacionLaboral") = ldata.Substring(162, 2)
                ldr("MontoDescuento") = CStr(CInt(ldata.Substring(164, 12))) & "." & ldata.Substring(176, 2)
                ldt.Rows.Add(ldr)
                ldata = lStreamReader.ReadLine
            Loop

        Catch ex As Exception
            Return New DataTable("Empty")
        Finally
            'lStreamReader.Close()
        End Try
        Return ldt

    End Function

    Public Function ImportaTXTGenerico(ByVal pRutaArchivo As String) As DataTable
        Dim ldt As DataTable = New DataTable("Result")
        Dim ldr As DataRow
        Dim ldata As String = ""

        Dim lStreamReader As IO.StreamReader
        lStreamReader = New IO.StreamReader(pRutaArchivo)
        Try

            'Aqui añadimos las columnas
            ldt.Columns.Add(New DataColumn("Data"))

            ldata = lStreamReader.ReadLine
            Do While (Not ldata Is Nothing)
                ldr = ldt.NewRow
                ldr("Data") = ldata
                ldt.Rows.Add(ldr)
                ldata = lStreamReader.ReadLine
            Loop
        Catch ex As Exception
            Return New DataTable("Empty")
        Finally
            lStreamReader.Close()
        End Try
        Return ldt

    End Function

End Class

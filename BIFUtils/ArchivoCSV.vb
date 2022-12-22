Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Data.OleDb

Public Class ArchivoCSV

    Public Sub ExportaCSV(ByRef pData As DataSet, ByVal ruta As String, ByVal strFile As String)
        Dim ldr As DataRow
        Dim lNroColumnas As Integer

        Dim sb As New StringBuilder()   ' para contener el archivo CSV
        Dim j As Integer
        Dim k As Integer
        Dim replVal As String = String.Empty

        WS.Utils.RemoveFiles(ruta, New TimeSpan(0, 0, 60, 0, 0))
        'Crear el encabezado y la hoja...
        Dim quoter As String = """"""

        lNroColumnas = pData.Tables(0).Columns.Count

        For j = 0 To lNroColumnas - 1
            sb.Append(pData.Tables(0).Columns(j).ColumnName)   'headings
            sb.Append(",") ' delimiter
        Next

        sb.Append(vbCrLf)
        For Each ldr In pData.Tables(0).Rows

            For k = 0 To lNroColumnas - 1
                If ldr(k).ToString() = Nothing Then
                    sb.Append("""""" + ldr(k).ToString() + " " + ",")
                Else
                    replVal = ldr(k).ToString().Replace("""", quoter)
                    replVal += " ,"
                    sb.Append(replVal)
                End If
            Next
            sb.Append(vbCrLf)
        Next

        Dim strFileContent As String = sb.ToString()

        Dim fi As New FileInfo(ruta + strFile)
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing
    End Sub

    Public Function ImportaCSV(ByVal pRutaArchivo As String) As DataTable
        Dim ldt As DataTable = New DataTable("Result")
        Dim ldr As DataRow
        Dim ldata As String = ""
        Dim ldataSplit As String()
        Dim lCol As Integer
        Dim lNroCol As Integer = 0

        Dim lStreamReader As IO.StreamReader
        lStreamReader = New IO.StreamReader(pRutaArchivo)
        Try

            'Aqui obtenemos los nombres de las columnas
            ldata = lStreamReader.ReadLine
            ldataSplit = ldata.Split(",")

            'Aqui añadimos las columnas
            For lCol = 0 To ldataSplit.Length() - 1
                If ldataSplit(lCol).Trim <> "" Then
                    ldt.Columns.Add(New DataColumn(ldataSplit(lCol)))
                    lNroCol = lNroCol + 1
                End If
            Next

            'Aqui leemos la primera linea
            ldata = lStreamReader.ReadLine
            Do While (Not ldata Is Nothing)
                ldataSplit = ldata.Split(",")
                ldr = ldt.NewRow
                For lCol = 0 To lNroCol - 1
                    ldr.Item(lCol) = ldataSplit(lCol).ToString
                Next
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

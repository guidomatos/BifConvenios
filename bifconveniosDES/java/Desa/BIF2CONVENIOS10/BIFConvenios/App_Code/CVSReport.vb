Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Web

Public Class CSVWebReport

    Public Function GenerateCSVReport(ByRef myReader As SqlDataReader, ByVal ruta As String, ByVal virtualPath As String, _
                        Optional ByVal headings As Boolean = True, Optional ByVal commaFinal As Boolean = True, _
                        Optional ByVal TextoEnlace As String = "Descargar reporte", Optional ByVal fileName As String = "", Optional ByVal blnOnlyPath As Boolean = False) As String
        Dim sb As New StringBuilder()   ' para contener el archivo CSV
        Dim j As Integer
        Dim k As Integer
        Dim replVal As String = String.Empty

        RemoveFiles(ruta)
        'Crear el encabezado y la hoja...
        Dim quoter As String = """"""


        If headings Then
            For j = 0 To myReader.FieldCount - 1
                sb.Append(myReader.GetName(j).ToString())   'headings
                sb.Append(",") ' delimiter
            Next
            sb.Append(vbCrLf)
        End If

        While myReader.Read
            For k = 0 To myReader.FieldCount - 1
                If myReader.GetValue(k).ToString() = Nothing Then
                    sb.Append("""""" + myReader.GetValue(k).ToString() + " " + ",")
                Else
                    replVal = myReader.GetValue(k).ToString().Replace("""", quoter)
                    replVal += " ,"
                    sb.Append(replVal)
                End If
            Next

            If Not commaFinal Then
                sb.Remove(sb.Length - 1, 1)
            End If

            sb.Append(vbCrLf)
        End While

        myReader.Close()
        myReader = Nothing

        Dim strFile As String = "report" + System.DateTime.Now.Ticks.ToString() + ".csv"
        Dim strFileContent As String = sb.ToString()

        If fileName.Trim <> "" Then
            strFile = fileName
        End If

        Dim fi As New FileInfo(ruta + strFile) 'System.Web.HttpContext.Current.Server.MapPath(ruta + strFile))
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        sWriter.Write(System.Text.Encoding.ASCII.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing
        Dim strMachineName As String = System.Web.HttpContext.Current.Request.ServerVariables("SERVER_NAME")

        'Devolvemos solo la ruta o la ruta con un enlace
        If Not blnOnlyPath Then
            Return "<A href=http://" + strMachineName + virtualPath + strFile + " target=_blank>" + TextoEnlace + "</a>"
        Else
            Return virtualPath + strFile
        End If
    End Function

    Protected Sub RemoveFiles(ByVal strPath As String)
        Dim di As New DirectoryInfo(strPath)
        Dim fiArr As FileInfo() = di.GetFiles()
        Dim fri As FileInfo

        For Each fri In fiArr
            If fri.Extension.ToString() = ".xls" Or fri.Extension.ToString() = ".csv" Then
                Dim min As New TimeSpan(0, 0, 60, 0, 0)
                If (fri.CreationTime < DateTime.Now.Subtract(min)) Then
                    fri.Delete()
                End If
            End If
        Next
    End Sub
End Class

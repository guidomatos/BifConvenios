Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration.ConfigurationSettings
'Imports System.Web

Public Class CSVReport

    Public Sub GenerateCSVReport(ByRef myReader As SqlDataReader, ByVal ruta As String, ByVal strFile As String) ', ByVal virtualPath As String)  'As String
        Dim sb As New StringBuilder()   ' para contener el archivo CSV
        Dim j As Integer
        Dim k As Integer
        Dim replVal As String = String.Empty

        Utils.RemoveFiles(ruta, New TimeSpan(0, 0, 60, 0, 0))
        'Crear el encabezado y la hoja...
        Dim quoter As String = """"""



        For j = 0 To myReader.FieldCount - 1
            sb.Append(myReader.GetName(j).ToString())   'headings
            sb.Append(",") ' delimiter

        Next

        sb.Append(vbCrLf)

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
            sb.Append(vbCrLf)
        End While

        myReader.Close()
        myReader = Nothing

        Dim strFileContent As String = sb.ToString()

        Dim fi As New FileInfo(ruta + strFile) 'System.Web.HttpContext.Current.Server.MapPath(ruta + strFile))
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing

        'Dim strMachineName As String = System.Web.HttpContext.Current.Request.ServerVariables("SERVER_NAME")

        'Return "<A href=http://" + strMachineName + virtualPath + strFile + " target=_blank>Descargar reporte</a>"
    End Sub

End Class

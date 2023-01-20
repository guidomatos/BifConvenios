Imports System.Text
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration.ConfigurationSettings


Public Class TXTReport


    Public Sub GenerateTXTReport(ByRef myReader As SqlDataReader, ByVal ruta As String, ByVal strFile As String) ', ByVal virtualPath As String)  'As String
        Dim sb As New StringBuilder()   ' para contener el archivo CSV
        Dim j As Integer
        Dim k As Integer
        Dim replVal As String = String.Empty

        Utils.RemoveFiles(ruta, New TimeSpan(0, 0, 60, 0, 0))

        'El formato es sin encabezado y en formato fijo, el reader debe tener los registros
        'listos para ser enviados al archivo
        While myReader.Read
            For k = 0 To myReader.FieldCount - 1
                sb.Append(myReader.GetValue(k).ToString())
            Next
            sb.Append(vbCrLf)
        End While

        myReader.Close()
        myReader = Nothing

        Dim strFileContent As String = sb.ToString()

        Dim fi As New FileInfo(ruta + strFile)
        Dim sWriter As FileStream = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
        sWriter.Write(System.Text.Encoding.ASCII.GetBytes(strFileContent), 0, strFileContent.Length)
        sWriter.Flush()
        sWriter.Close()
        fi = Nothing
        sWriter = Nothing

    End Sub

End Class
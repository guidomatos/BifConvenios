Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
'Contiene las clases para manipular la creacion de archivos DBF

Public Class DBFReport

    Public Shared Sub generateDBFUNICAReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-UNICA"), AppSettings("Generacion") + fileName)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' (documento, monto, tipo)" + _
            " VALUES ( ?, ?, ?)", _
            connection)
            command.Parameters.Add("documento", myReader("Documento"))
            command.Parameters.Add("monto", myReader("Monto"))
            command.Parameters.Add("tipo", myReader("Tipo"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing
    End Sub


    'Permite generar informacion en un archivo DBF basandose en un DataReader
    'para el sistema SIGA
    Public Shared Sub generateDBFSIGAReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-SIGA"), AppSettings("Generacion") + fileName)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            'INSERT INTO " +  filename + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' (depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.Add("depe_id", myReader("depe_id"))
            command.Parameters.Add("mes", myReader("mes"))
            command.Parameters.Add("anno", myReader("anno"))
            command.Parameters.Add("dni", myReader("dni"))
            command.Parameters.Add("sitlaboral", myReader("sitlaboral"))
            command.Parameters.Add("apellidos", myReader("apellidos"))
            command.Parameters.Add("nombres", myReader("nombres"))
            command.Parameters.Add("monto", myReader("monto"))
            command.Parameters.Add("ncuota", myReader("ncuota"))
            command.Parameters.Add("maxcuota", myReader("maxcuota"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing
    End Sub

    'Generacion de Archivo DBF para la Direccion Regional de Educacion Amazonas Chachapoyas 
    Public Shared Sub generateDBFJAENReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-JAEN"), AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            'INSERT INTO " +  filename + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' ( periodo, instit, codmod, cargo, carben, t_plla, coddes, montodes, apepater, amater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.Add("periodo", myReader("periodo"))
            command.Parameters.Add("instit", myReader("instit"))
            command.Parameters.Add("codmod", myReader("codmod"))
            command.Parameters.Add("cargo", myReader("cargo"))
            command.Parameters.Add("carben", myReader("carben"))
            command.Parameters.Add("t_plla", myReader("T-PLLA"))
            command.Parameters.Add("coddes", myReader("coddes"))
            command.Parameters.Add("montodes", myReader("montodes"))
            command.Parameters.Add("apepater", myReader("apepater"))
            command.Parameters.Add("amater", myReader("amater"))
            command.Parameters.Add("nombre", myReader("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF", AppSettings("Generacion") + fileName)

    End Sub


    Public Shared Sub generateDBFSanIgnacioReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-sanignacio"), AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            'INSERT INTO " +  filename + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plani, coddes, montodes, apepater, apemater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.Add("periodo", myReader("periodo"))
            command.Parameters.Add("empresa", myReader("empresa"))
            command.Parameters.Add("codmod", myReader("codmod"))
            command.Parameters.Add("cargo", myReader("cargo"))
            command.Parameters.Add("carben", myReader("carben"))
            command.Parameters.Add("t_plani", myReader("T-Plani"))
            command.Parameters.Add("coddes", myReader("coddes"))
            command.Parameters.Add("montodes", myReader("montodes"))
            command.Parameters.Add("apepater", myReader("apepater"))
            command.Parameters.Add("apemater", myReader("apemater"))
            command.Parameters.Add("nombre", myReader("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF", AppSettings("Generacion") + fileName)

    End Sub



    Public Shared Sub generateDBFChulucanasReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-CHULUCANAS"), AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            'INSERT INTO " +  filename + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plla, coddes, montodes, apepater, amater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.Add("periodo", myReader("periodo"))
            command.Parameters.Add("empresa", myReader("empresa"))
            command.Parameters.Add("codmod", myReader("codmod"))
            command.Parameters.Add("cargo", myReader("cargo"))
            command.Parameters.Add("carben", myReader("carben"))
            command.Parameters.Add("t_plla", myReader("T-PLLA"))
            command.Parameters.Add("coddes", myReader("coddes"))
            command.Parameters.Add("montodes", myReader("montodes"))
            command.Parameters.Add("apepater", myReader("apepater"))
            command.Parameters.Add("amater", myReader("amater"))
            command.Parameters.Add("nombre", myReader("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF", AppSettings("Generacion") + fileName)

    End Sub

    'Generacion de Archivo DBF para la Direccion Regional de Educacion Amazonas Chachapoyas 
    Public Shared Sub generateDBFAMAZONASReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-AMAZONAS"), AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            'INSERT INTO " +  filename + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plani, coddes, montodes, apepater, apemater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.Add("periodo", myReader("periodo"))
            command.Parameters.Add("empresa", myReader("empresa"))
            command.Parameters.Add("codmod", myReader("codmod"))
            command.Parameters.Add("cargo", myReader("cargo"))
            command.Parameters.Add("carben", myReader("carben"))
            command.Parameters.Add("t_plani", myReader("t_plani"))
            command.Parameters.Add("coddes", myReader("coddes"))
            command.Parameters.Add("montodes", myReader("montodes"))
            command.Parameters.Add("apepater", myReader("apepater"))
            command.Parameters.Add("apemater", myReader("apemater"))
            command.Parameters.Add("nombre", myReader("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(AppSettings("Generacion") + fileName.Split(".")(0) + ".DBF", AppSettings("Generacion") + fileName)

    End Sub

    'Generacion de Archivo DBF para la UNFV 
    Public Shared Sub generateDBFUNFVReport(ByRef myReader As SqlDataReader, ByVal fileName As String)
        'TODO: REVISAR SI fileName va a incluir la extension DBF
        File.Delete(AppSettings("Generacion") + fileName)
        File.Copy(AppSettings("DBFTemplates") & AppSettings("DBFTemplate-UNFV"), AppSettings("Generacion") + fileName)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + AppSettings("Generacion") + ";")
        connection.Open()

        While myReader.Read
            Dim command As New OleDbCommand("INSERT INTO '" + fileName.Split(".")(0) + _
            "' (CODVAR, CODPER, CLASE, APENOM, MONTO, CUOTA)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?)", _
            connection)
            command.Parameters.Add("CODVAR", myReader("CODIGO"))
            command.Parameters.Add("CODPER", myReader("NRO DE DNI"))
            command.Parameters.Add("CLASE", myReader("CLASE"))
            command.Parameters.Add("APENOM", myReader("NOMBRE"))
            command.Parameters.Add("MONTO", myReader("CUOTA"))
            command.Parameters.Add("CUOTA", myReader("NRO DE CUOTA"))

            command.ExecuteNonQuery()
            command.Dispose()
        End While

        connection.Close()
        connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing
    End Sub

End Class

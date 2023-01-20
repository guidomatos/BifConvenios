Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Reflection
Imports System.IO
Imports System.Runtime.InteropServices

Public Class ArchivoDBF
    Dim lRutaGeneracionArchivos As String = ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim()
    Dim lRutaDBFBase As String = ConfigurationManager.AppSettings("RutaDBFBase").Trim()

#Region " Exportar DBF "

    Public Sub ExportaDBFUNICA(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        'ELLANOS-1504-2013 INICIO
        IIF(pNombreArchivoProceso.Substring(0, 1) = "-", pNombreArchivoProceso.Substring(1), pNombreArchivoProceso)
        'msgbbox("ok", pNombreArchivoProceso)
        'ELLANOS-1504-2013 FIN
        Dim ldr As DataRow

        'Eliminamos el archivo original
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-UNICA"), lRutaGeneracionArchivos + pNombreArchivoProceso)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' (documento, monto, tipo)" + _
            " VALUES ( ?, ?, ?)", _
            connection)
            command.Parameters.AddWithValue("documento", ldr("Documento"))
            command.Parameters.AddWithValue("monto", ldr("Monto"))
            command.Parameters.AddWithValue("tipo", ldr("Tipo"))

            command.ExecuteNonQuery()
            command.Dispose()

        Next

        connection.Close()
        connection.Dispose()
        connection = Nothing
    End Sub

    Public Sub ExportaDBFSIGA(ByRef pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow
        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-SIGA"), lRutaGeneracionArchivos + pNombreArchivoProceso)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            'INSERT INTO " +  pNombreArchivoProceso + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' (depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.AddWithValue("depe_id", ldr("depe_id"))
            command.Parameters.AddWithValue("mes", ldr("mes"))
            command.Parameters.AddWithValue("anno", ldr("anno"))
            command.Parameters.AddWithValue("dni", ldr("dni"))
            command.Parameters.AddWithValue("sitlaboral", ldr("sitlaboral"))
            command.Parameters.AddWithValue("apellidos", ldr("apellidos"))
            command.Parameters.AddWithValue("nombres", ldr("nombres"))
            command.Parameters.AddWithValue("monto", ldr("monto"))
            command.Parameters.AddWithValue("ncuota", ldr("ncuota"))
            command.Parameters.AddWithValue("maxcuota", ldr("maxcuota"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        connection.Dispose()
        connection = Nothing
    End Sub
    ''' <summary>
    ''' Exporta informacion en formato DBF para la UNMSM.
    ''' </summary>
    ''' <param name="pData">Informacion a exportar</param>
    ''' <param name="pNombreArchivoProceso">Nombre del archivo a generar.</param>
    ''' <remarks>ADD 02/09/2013 NCA: Generar archivo de cobranzas en formato DBF para la UNMSM
    '''          MOD 05/09/2013 NCA: Generar dos formatos de archivos para la UNMSM</remarks>
    Public Sub ExportaDBFUNMSM(ByRef pData As DataSet, ByVal pNombreArchivoProceso As String, ByVal pSituacionTrabajador As String)
        Dim ldr As DataRow
        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)

        'MOD 05/09/2013 NCA: GENERAR dos formatos de archivos para la UNMSM
        Dim strTagPlantilla As String = "ArchivoDBF-UNMSM6"         'por defecto longitud columna CPERSON = 6
        If pSituacionTrabajador.ToUpper.Trim <> "A" Then strTagPlantilla = "ArchivoDBF-UNMSM8" 'por defecto longitud columna CPERSON = 8
        'Anterior: File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-UNMSM"), lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings(strTagPlantilla), lRutaGeneracionArchivos + pNombreArchivoProceso)
        'END MOD

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' (cperso, importe, cdeduc, person)" + _
            " VALUES ( ?, ?, ?, ?)", _
            connection)
            command.Parameters.AddWithValue("cperso", ldr("cperso"))
            command.Parameters.AddWithValue("importe", ldr("importe"))
            command.Parameters.AddWithValue("cdeduc", ldr("cdeduc"))
            command.Parameters.AddWithValue("person", ldr("person"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        connection.Dispose()
        connection = Nothing
    End Sub

    Public Sub ExportaDBFJAEN(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow

        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-JAEN"), lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            'INSERT INTO " +  pNombreArchivoProceso + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' ( PERIODO, EMPRESA, CODMOD, CARGO, CARBEN, T_PLANI, CODDES, MONTODES, APEPATER, APEMATER, NOMBRE)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.AddWithValue("PERIODO", ldr("PERIODO"))
            command.Parameters.AddWithValue("EMPRESA", ldr("EMPRESA"))
            command.Parameters.AddWithValue("CODMOD", ldr("CODMOD"))
            command.Parameters.AddWithValue("CARGO", ldr("CARGO"))
            command.Parameters.AddWithValue("CARBEN", ldr("CARBEN"))
            command.Parameters.AddWithValue("T_PLANI", ldr("T-PLANI"))
            command.Parameters.AddWithValue("CODDES", ldr("CODDES"))
            command.Parameters.AddWithValue("MONTODES", ldr("MONTODES"))
            command.Parameters.AddWithValue("APEPATER", ldr("APEPATER"))
            command.Parameters.AddWithValue("APEMATER", ldr("APEMATER"))
            command.Parameters.AddWithValue("NOMBRE", ldr("NOMBRE"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        'connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF", lRutaGeneracionArchivos + pNombreArchivoProceso)

    End Sub

    Public Sub ExportaDBFSanIgnacio(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow

        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-SANIGNACIO"), lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            'INSERT INTO " +  pNombreArchivoProceso + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plani, coddes, montodes, apepater, apemater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.AddWithValue("periodo", ldr("periodo"))
            command.Parameters.AddWithValue("empresa", ldr("empresa"))
            command.Parameters.AddWithValue("codmod", ldr("codmod"))
            command.Parameters.AddWithValue("cargo", ldr("cargo"))
            command.Parameters.AddWithValue("carben", ldr("carben"))
            command.Parameters.AddWithValue("t_plani", ldr("T-Plani"))
            command.Parameters.AddWithValue("coddes", ldr("coddes"))
            command.Parameters.AddWithValue("montodes", ldr("montodes"))
            command.Parameters.AddWithValue("apepater", ldr("apepater"))
            command.Parameters.AddWithValue("apemater", ldr("apemater"))
            command.Parameters.AddWithValue("nombre", ldr("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        'connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF", lRutaGeneracionArchivos + pNombreArchivoProceso)

    End Sub

    Public Sub ExportaDBFChulucanas(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow

        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-CHULUCANAS"), lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            'INSERT INTO " +  pNombreArchivoProceso + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plla, coddes, montodes, apepater, amater, nombre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.AddWithValue("periodo", ldr("periodo"))
            command.Parameters.AddWithValue("empresa", ldr("empresa"))
            command.Parameters.AddWithValue("codmod", ldr("codmod"))
            command.Parameters.AddWithValue("cargo", ldr("cargo"))
            command.Parameters.AddWithValue("carben", ldr("carben"))
            command.Parameters.AddWithValue("t_plla", ldr("T-PLLA"))
            command.Parameters.AddWithValue("coddes", ldr("coddes"))
            command.Parameters.AddWithValue("montodes", ldr("montodes"))
            command.Parameters.AddWithValue("apepater", ldr("apepater"))
            command.Parameters.AddWithValue("amater", ldr("amater"))
            command.Parameters.AddWithValue("nombre", ldr("nombre"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        'connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF", lRutaGeneracionArchivos + pNombreArchivoProceso)

    End Sub

    Public Sub ExportaDBFAMAZONAS(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow

        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-AMAZONAS"), lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF")

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            'INSERT INTO " +  pNombreArchivoProceso + "(depe_id, mes, anno, dni, sitlaboral, apellidos, nombres, monto, ncuota, maxcuota)
            'VALUES ( )
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' ( periodo, empresa, codmod, cargo, carben, t_plani, coddes, montodes, apepater, apemater, nombre, finicre)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", _
            connection)
            ' " VALUES ( @depe_id, @mes, @anno, @dni, @sitlaboral, @apellidos, @nombres, @monto, @ncuota, @maxcuota)", _
            command.Parameters.AddWithValue("periodo", ldr("periodo"))
            command.Parameters.AddWithValue("empresa", ldr("empresa"))
            command.Parameters.AddWithValue("codmod", ldr("codmod"))
            command.Parameters.AddWithValue("cargo", ldr("cargo"))
            command.Parameters.AddWithValue("carben", ldr("carben"))
            command.Parameters.AddWithValue("t_plani", ldr("t_plani"))
            command.Parameters.AddWithValue("coddes", ldr("coddes"))
            command.Parameters.AddWithValue("montodes", ldr("montodes"))
            command.Parameters.AddWithValue("apepater", ldr("apepater"))
            command.Parameters.AddWithValue("apemater", ldr("apemater"))
            command.Parameters.AddWithValue("nombre", ldr("nombre"))
            command.Parameters.AddWithValue("finicre", ldr("finicre"))
            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        'connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing

        File.Move(lRutaGeneracionArchivos + pNombreArchivoProceso.Split(".")(0) + ".DBF", lRutaGeneracionArchivos + pNombreArchivoProceso)

    End Sub

    Public Sub ExportaDBFUNFV(ByVal pData As DataSet, ByVal pNombreArchivoProceso As String)
        Dim ldr As DataRow

        'TODO: REVISAR SI pNombreArchivoProceso va a incluir la extension DBF
        File.Delete(lRutaGeneracionArchivos + pNombreArchivoProceso)
        File.Copy(lRutaDBFBase & ConfigurationManager.AppSettings("ArchivoDBF-UNFV"), lRutaGeneracionArchivos + pNombreArchivoProceso)

        Dim connection As New OleDbConnection("Provider=VFPOLEDB.1;Data Source=" + lRutaGeneracionArchivos + ";")
        connection.Open()

        For Each ldr In pData.Tables(0).Rows
            Dim command As New OleDbCommand("INSERT INTO '" + pNombreArchivoProceso.Split(".")(0) + _
            "' (CODVAR, CODPER, CLASE, APENOM, MONTO, CUOTA)" + _
            " VALUES ( ?, ?, ?, ?, ?, ?)", _
            connection)
            command.Parameters.AddWithValue("CODVAR", ldr("CODIGO"))
            command.Parameters.AddWithValue("CODPER", ldr("NRO DE DNI"))
            command.Parameters.AddWithValue("CLASE", ldr("CLASE"))
            command.Parameters.AddWithValue("APENOM", ldr("NOMBRE"))
            command.Parameters.AddWithValue("MONTO", ldr("CUOTA"))
            command.Parameters.AddWithValue("CUOTA", ldr("NRO DE CUOTA"))

            command.ExecuteNonQuery()
            command.Dispose()
        Next

        connection.Close()
        'connection.ReleaseObjectPool()
        connection.Dispose()
        connection = Nothing
    End Sub

#End Region

#Region " Lectura DBF "

    '// This is the file header for a DBF. We do this special layout with everything
    '// packed so we can read straight from disk into the structure to populate it
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
    Private Structure DBFHeader

        Public version As Byte
        Public updateYear As Byte
        Public updateMonth As Byte
        Public updateDay As Byte
        Public numRecords As Int32
        Public headerLen As Int16
        Public recordLen As Int16
        Public reserved1 As Int16
        Public incompleteTrans As Byte
        Public encryptionFlag As Byte
        Public reserved2 As Int32
        Public reserved3 As Int64
        Public MDX As Byte
        Public language As Byte
        Public reserved4 As Int16
    End Structure

    '// This is the field descriptor structure. There will be one of these for each column in the table.
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi, Pack:=1)> _
    Private Structure FieldDescriptor
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=11)> _
        Public fieldName As String
        Public fieldType As Char
        Public address As Int32
        Public fieldLen As Byte
        Public count As Byte
        Public reserved1 As Int16
        Public workArea As Byte
        Public reserved2 As Int16
        Public flag As Byte
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=7)> _
        Public reserved3 As Byte()
        Public indexFlag As Byte
    End Structure

    '// Read an entire standard DBF file into a DataTable
    Public Function LeerDBF(ByVal dbfFile As String) As DataTable

        Dim dt As DataTable = New DataTable()
        '// If there isn't even a file, just return an empty DataTable
        If ((False = File.Exists(dbfFile))) Then
            Return dt
        End If

        Dim br As BinaryReader = Nothing
        Try

            '// Read the header into a buffer
            br = New BinaryReader(File.OpenRead(dbfFile))
            Dim buffer As Byte() = br.ReadBytes(Marshal.SizeOf(GetType(DBFHeader)))

            '// Marshall the header into a DBFHeader structure
            Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
            Dim header As DBFHeader = CType(Marshal.PtrToStructure(handle.AddrOfPinnedObject(), GetType(DBFHeader)), DBFHeader)
            handle.Free()

            '// Read in all the field descriptors. Per the spec, 13 (0D) marks the end of the field descriptors
            Dim fields As ArrayList = New ArrayList()
            While ((13 <> br.PeekChar()))
                buffer = br.ReadBytes(Marshal.SizeOf(GetType(FieldDescriptor)))
                handle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
                fields.Add(CType(Marshal.PtrToStructure(handle.AddrOfPinnedObject(), GetType(FieldDescriptor)), FieldDescriptor))
                handle.Free()
            End While

            '// Create the columns in our new DataTable
            Dim col As DataColumn = Nothing
            Dim field As FieldDescriptor
            For Each field In fields

                Select Case field.fieldType
                    Case "N"
                        If (field.count > 0) Then
                            col = New DataColumn(field.fieldName, GetType(Double))
                        Else
                            col = New DataColumn(field.fieldName, GetType(Int32))
                        End If
                    Case "C"
                        col = New DataColumn(field.fieldName, GetType(String))
                    Case "D"
                        col = New DataColumn(field.fieldName, GetType(DateTime))
                    Case "L"
                        col = New DataColumn(field.fieldName, GetType(Boolean))
                End Select

                dt.Columns.Add(col)
            Next

            '// Skip past the end of the header. 
            CType(br.BaseStream, FileStream).Seek(header.headerLen + 1, SeekOrigin.Begin)

            '// Declare all our locals here outside the loops
            Dim number As String        '//Temporal del Dato Numerico
            Dim data As String          '//Temporal del Dato Texto
            Dim year As String          '//Temporal del Dato Año
            Dim month As String         '//Temporal del Dato Mes
            Dim day As String           '//Temporal del Dato Dia
            Dim bufferstr As String     '//Data Real sobre la que se trabaja
            Dim PosiIni As Integer = 0  '//Posicion Inicial para hacer el Substring
            Dim row As DataRow          '//Data a Insertar en el DataTable

            '// Leemos todos los Registros
            Dim counter As Integer
            For counter = 0 To header.numRecords - 1
                '// First we'll read the entire record into a buffer and then read each field from the buffer
                '// This helps account for any extra space at the end of each record and probably performs better
                buffer = br.ReadBytes(header.recordLen)                             '//Leemos como Bytes[]
                Dim enc As System.Text.Encoding = System.Text.Encoding.ASCII        '//Definimos un Encoding Ascii
                bufferstr = enc.GetString(buffer)                                   '//Convertimos a String el Tipo Bytes[]

                PosiIni = 0   '//Inicializamos el Indicador de Posicion

                '// Loop through each field in a record
                row = dt.NewRow()
                For Each field In fields
                    Select Case field.fieldType
                        Case "N" '// Number
                            '// We'll use a try/catch here in case it isn't a valid number
                            number = bufferstr.Substring(PosiIni, field.fieldLen)
                            PosiIni = PosiIni + field.fieldLen
                            Try
                                row(field.fieldName) = number
                            Catch
                                row(field.fieldName) = 0
                            End Try

                        Case "C" '// String
                            data = bufferstr.Substring(PosiIni, field.fieldLen)
                            data = Replace(data, vbTab, "")
                            row(field.fieldName) = data
                            PosiIni = PosiIni + field.fieldLen

                        Case "D" '// Date (YYYYMMDD)
                            '//year = Encoding.ASCII.GetString(recReader.ReadBytes(4));
                            '//month = Encoding.ASCII.GetString(recReader.ReadBytes(2));
                            '//day = Encoding.ASCII.GetString(recReader.ReadBytes(2));
                            year = bufferstr.Substring(PosiIni, 4)
                            PosiIni = PosiIni + 4
                            month = bufferstr.Substring(PosiIni, 2)
                            PosiIni = PosiIni + 2
                            day = bufferstr.Substring(PosiIni, 2)
                            PosiIni = PosiIni + 2
                            row(field.fieldName) = System.DBNull.Value
                            Try
                                If ((Int32.Parse(year) > 1900)) Then
                                    row(field.fieldName) = New DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day))
                                End If
                            Catch
                            End Try
                            '//case 'L': // Boolean (Y/N)
                            '// if ('Y' == recReader.ReadByte())
                            '// {
                            '//     row[field.fieldName] = true;
                            '// }
                            '//else
                            '// {
                            '//     row[field.fieldName] = false;
                            '// }
                            '//
                            '//break;
                    End Select
                Next
                '//recReader.Close();
                dt.Rows.Add(row)

            Next
        Catch Ex As Exception
            Throw Ex
        Finally
            If Not br Is Nothing Then
                br.Close()
            End If
        End Try

        Return dt
    End Function

#End Region

End Class

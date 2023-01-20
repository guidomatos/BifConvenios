Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports System.IO
Imports System.Runtime.InteropServices

Namespace BIFConvenios

    Public Class ProcesaArchivoDBF

#Region " Operaciones SQL Server "

        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim transaction As SqlTransaction

        Protected Function addInformacionArchivoTexto(ByVal Codigo_proceso As String, ByVal UserId As String, ByVal orden As Integer, ByVal lineainformacion As String, ByVal dateCode As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, UserId, orden, lineainformacion, dateCode})

            myCommand.Transaction = transaction

            returnValue = CType(myCommand.ExecuteScalar(), Integer)
            Return returnValue
        End Function

        'Envia al archivo a proceso 
        Protected Function procesaDatosArchivos(ByVal Codigo_proceso As String, ByVal dateCode As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, dateCode})

            myCommand.Transaction = transaction
            myCommand.CommandTimeout = 100000
            ''myCommand.ExecuteNonQuery()

            returnValue = CType(myCommand.ExecuteScalar(), Integer)
            Return returnValue
        End Function

        'Procesa informacion de archivo de descuentos por defecto
        Public Sub ProcesaArchivoDescuentoDefault(ByVal Codigo_proceso As String, ByVal usuario As String)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, usuario})

            myCommand.Transaction = transaction
            'returnValue = 
            myCommand.CommandTimeout = 100000
            myCommand.ExecuteNonQuery()
            'CType(myCommand.ExecuteScalar(), Integer)
            'Return returnValue
        End Sub


        'Procesar la informacion del archivo de creditos covenios
        Public Function procesaInformacionDBFConveniosSIGA(ByVal rutaArchivo As String, ByVal Codigo_Proceso As String, ByVal userid As String)
            Dim orden As Integer = 0
            Dim dateCode As String = Utils.getyyyyMMddhhmmss
            Try
                myConnection.Open()
                transaction = myConnection.BeginTransaction()
                '  Dim fs As New System.IO.FileStream(rutaArchivo, IO.FileMode.Open)
                'Dim SReadLine As Stream
                'SReadLine = File.OpenRead(rutaArchivo)
                'Dim SrReadLine As StreamReader = New StreamReader(SReadLine, _
                '    System.Text.Encoding.ASCII)
                'SrReadLine.BaseStream.Seek(0, SeekOrigin.Begin)
                'While (SrReadLine.Peek() > -1)
                '    'Console.Write(SrReadLine.ReadLine())
                '    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, SrReadLine.ReadLine, dateCode)
                '    orden += 1
                'End While
                'SrReadLine.Close()

                Dim ldt As DataTable = Me.ReadDBF(rutaArchivo)
                Dim ldr As DataRow
                Dim lstrData As String
                For Each ldr In ldt.Rows
                    lstrData = LSet(ldr("dni"), 15) & RSet(Format(ldr("process"), "##########0.00"), 15) & " " & Left(LSet(Trim(ldr("apellidos")) & " " & Trim(ldr("nombres")), 50), 50) & " " & RSet(Format(ldr("monto"), "##########0.00"), 15)
                    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, lstrData, dateCode)
                    orden += 1
                Next

                transaction.Save("ArchivoEscrito")

                Me.procesaDatosArchivos(Codigo_Proceso, dateCode)
                Me.ProcesaArchivoDescuentoDefault(Codigo_Proceso, "Server")

                transaction.Commit() 'Procesamos la transaccion en SQL 
            Catch e As Exception
                transaction.Rollback() 'Hacemos un rollback cuando hay error en la transaccion en AS/400
                Proceso.UpdErrorCargaArchivoDescuentos(Codigo_Proceso, "Server")
                'Throw e
            Finally
                myConnection.Close()
            End Try
        End Function

        'Procesar la informacion del archivo de creditos covenios UNFV 20080915
        Public Function procesaInformacionDBFConveniosUNFV(ByVal rutaArchivo As String, ByVal Codigo_Proceso As String, ByVal userid As String)
            Dim orden As Integer = 0
            Dim dateCode As String = Utils.getyyyyMMddhhmmss
            Try
                myConnection.Open()
                transaction = myConnection.BeginTransaction()
                '  Dim fs As New System.IO.FileStream(rutaArchivo, IO.FileMode.Open)
                'Dim SReadLine As Stream
                'SReadLine = File.OpenRead(rutaArchivo)
                'Dim SrReadLine As StreamReader = New StreamReader(SReadLine, _
                '    System.Text.Encoding.ASCII)
                'SrReadLine.BaseStream.Seek(0, SeekOrigin.Begin)
                'While (SrReadLine.Peek() > -1)
                '    'Console.Write(SrReadLine.ReadLine())
                '    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, SrReadLine.ReadLine, dateCode)
                '    orden += 1
                'End While
                'SrReadLine.Close()

                Dim ldt As DataTable = Me.ReadDBF(rutaArchivo)
                Dim ldr As DataRow
                Dim lstrData As String
                For Each ldr In ldt.Rows
                    lstrData = LSet(Right("0000000000" & Trim(ldr("CODIGO")), 10), 15) & RSet(Format(ldr("MONVAR"), "##########0.00"), 15) & " " & Left(LSet(Trim(ldr("NOMPER")), 50), 50) & " " & LSet(ldr("CLASE"), 15)
                    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, lstrData, dateCode)
                    orden += 1
                Next

                transaction.Save("ArchivoEscrito")

                Me.procesaDatosArchivos(Codigo_Proceso, dateCode)
                Me.ProcesaArchivoDescuentoDefault(Codigo_Proceso, "Server")

                transaction.Commit() 'Procesamos la transaccion en SQL 
            Catch e As Exception
                transaction.Rollback() 'Hacemos un rollback cuando hay error en la transaccion en AS/400
                Proceso.UpdErrorCargaArchivoDescuentos(Codigo_Proceso, "Server")
                'Throw e
            Finally
                myConnection.Close()
            End Try
        End Function

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
        Public Shared Function ReadDBF(ByVal dbfFile As String) As DataTable

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

End Namespace
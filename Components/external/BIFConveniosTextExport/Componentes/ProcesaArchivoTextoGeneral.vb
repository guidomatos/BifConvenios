Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports System.IO

Namespace BIFConvenios

    Public Class ProcesaArchivoTextoGeneral

#Region "Operaciones SQL Server"

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


        Protected Function deleteInformacionArchivoTexto(ByVal Codigo_proceso As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso})

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
            returnValue = CType(myCommand.ExecuteScalar(), Integer)
            Return returnValue
        End Function

        Public Sub ProcesaArchivoDescuentoDefaultUTES(ByVal Codigo_proceso As String, ByVal usuario As String)
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
        Public Function procesaInformacionArchivoConvenios_UTES(ByVal rutaArchivo As String, ByVal Codigo_Proceso As String, ByVal userid As String)
            Dim orden As Integer = 0
            Dim dateCode As String = Utils.getyyyyMMddhhmmss
            Dim contador_iguales As Integer = 0
            Try
                myConnection.Open()
                transaction = myConnection.BeginTransaction()
                '  Dim fs As New System.IO.FileStream(rutaArchivo, IO.FileMode.Open)
                Dim SReadLine As Stream
                SReadLine = File.OpenRead(rutaArchivo)
                Dim SrReadLine As StreamReader = New StreamReader(SReadLine, _
                    System.Text.Encoding.ASCII)
                SrReadLine.BaseStream.Seek(0, SeekOrigin.Begin)
                ''Me.deleteInformacionArchivoTexto(Codigo_Proceso)
                Dim line As String = String.Empty
                While (SrReadLine.Peek() > -1)
                    'Console.Write(SrReadLine.ReadLine())
                    'line = SrReadLine.ReadLine
                    'If (line.Substring(1, 2) = "==") Then

                    ' If contador_iguales = 2 Then
                    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, SrReadLine.ReadLine.Replace("", "0"), dateCode)
                    orden += 1
                    'contador_iguales = 0
                    ' Else
                    '    contador_iguales = contador_iguales + 1
                    '    orden += 1
                    'End If
                    ' Else

                    'End If
                End While
                SrReadLine.Close()

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

        Public Function procesaInformacionArchivoConvenios(ByVal rutaArchivo As String, ByVal Codigo_Proceso As String, ByVal userid As String)
            Dim orden As Integer = 0
            Dim dateCode As String = Utils.getyyyyMMddhhmmss
            Try
                myConnection.Open()
                transaction = myConnection.BeginTransaction()
                '  Dim fs As New System.IO.FileStream(rutaArchivo, IO.FileMode.Open)
                Dim SReadLine As Stream
                SReadLine = File.OpenRead(rutaArchivo)
                Dim SrReadLine As StreamReader = New StreamReader(SReadLine, _
                    System.Text.Encoding.ASCII)
                SrReadLine.BaseStream.Seek(0, SeekOrigin.Begin)
                While (SrReadLine.Peek() > -1)
                    'Console.Write(SrReadLine.ReadLine())
                    Me.addInformacionArchivoTexto(Codigo_Proceso, userid, orden, SrReadLine.ReadLine, dateCode)
                    orden += 1
                End While
                SrReadLine.Close()

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


    End Class

End Namespace

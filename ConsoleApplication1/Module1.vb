Module Module1

    Sub Main()

        'ServiceProcesarAlertaAutomatica()
        'ServiceProcesarEnvioAutomatico()
        ServiceProcesarReportesAutomaticos()

    End Sub


    Private Sub ServiceProcesarAlertaAutomatica()
        Dim str As String = ""
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        Dim num2 As Integer = 0
        Dim dataTable As System.Data.DataTable = New System.Data.DataTable()

        Dim wsAlertasAutomaticas As New wsAlertasAutomaticas.wsAlertasAutomaticas()

        'Me.objAlertasAutomaticas.Credentials = CredentialCache.DefaultCredentials
        Try
            'Me.evEnvioAlertas.WriteEntry("Iniciando las alertas automaticas")
            dataTable = wsAlertasAutomaticas.ObtenerListClienteUltimoProceso()


            wsAlertasAutomaticas.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarAlertaAutomatica", "Inicio del Método: - Parámetros: Ninguno", "", "OperadorDES")

            num = wsAlertasAutomaticas.RegistrarLogProcesosAutomaticos(0, 0, 0, "Iniciando Envio de Alertas Automáticas", 1, "OperadorDES")

            If (dataTable.Rows.Count <= 0) Then
                wsAlertasAutomaticas.ActualizarLogProcesosAutomaticos(num, dataTable.Rows.Count, num1, num2, "Alertas Automáticas: Lista de Alertas esta vacia", 2, "OperadorDES")
            Else
                For Each row As DataRow In dataTable.Rows
                    Dim str1 As String = row("Codigo_Cliente").ToString()
                    Dim num3 As Integer = Convert.ToInt32(row("Anio_Periodo").ToString())
                    Dim num4 As Integer = Convert.ToInt32(row("Mes_Periodo").ToString())
                    Dim num5 As Integer = 0
                    str = wsAlertasAutomaticas.ProcesarAlerta(num, str1, num3, num4, "operadordes", num5)

                    If (num5 <> 1) Then
                        Console.WriteLine(str)
                        num2 = num2 + 1
                    ElseIf (Not (str = "")) Then
                        Console.WriteLine(str)
                        num1 = num1 + 1
                    Else
                        Console.WriteLine(String.Concat("Envio de correo para el cliente:  ", str1, " terminado sin errores."))
                        'Me.evEnvioAlertas.WriteEntry(String.Concat("Envio de correo para el cliente:  ", str1, " terminado sin errores."))
                        num1 = num1 + 1
                    End If
                Next
                wsAlertasAutomaticas.ActualizarLogProcesosAutomaticos(num, dataTable.Rows.Count, num1, num2, "Alertas Automáticas: Proceso Terminado.", 2, "OperadorDES")
            End If
            wsAlertasAutomaticas.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarAlertaAutomatica", "Fin del Método: - Parámetros: Ninguno", "", "OperadorDES")
        Catch exception1 As System.Exception
            Dim exception As System.Exception = exception1
            wsAlertasAutomaticas.RegistrarLogEventoSistema("BifConvenio", 3, "ServiceProcesarAlertaAutomatica", "Fin del Método: - Parámetros: Ninguno", exception.ToString(), "OperadorDES")
            'Me.evEnvioAlertas.WriteEntry(exception.ToString())
            Console.WriteLine(exception.ToString())
            wsAlertasAutomaticas.ActualizarLogProcesosAutomaticos(num, 0, 0, 0, String.Concat("Alertas Automáticas: ", exception.ToString()), 3, "OperadorDES")
        End Try
    End Sub


    Private Sub ServiceProcesarEnvioAutomatico()
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        Dim num2 As Integer = 0
        Dim empty As String = String.Empty
        Dim str As String = String.Empty
        Dim empty1 As String = String.Empty
        Dim str1 As String = String.Empty
        Dim empty2 As String = String.Empty
        Dim str2 As String = String.Empty
        Dim empty3 As String = String.Empty
        Dim str3 As String = String.Empty
        Dim dataTable As System.Data.DataTable = New System.Data.DataTable()
        'Me.objEnvioAutomatico.Credentials = CredentialCache.DefaultCredentials

        Dim wsEnvioAutomatico As New wsEnvioAutomatico.wsEnvioAutomatico()

        Try
            'Me.evEnvioNominas.WriteEntry("Iniciando los envíos automáticos de nóminas")
            dataTable = wsEnvioAutomatico.ObtenerListaProcesosDisponiblesByFecha(DateTime.Now.Day)
            wsEnvioAutomatico.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarEnvioAutomatico", "Inicio del Método: - Parámetros: Ninguno", "", "OperadorDES")
            num = wsEnvioAutomatico.RegistrarLogProcesosAutomaticos(0, 0, 0, "Iniciando Envio de Nominas Automáticas", 1, "OperadorDES")
            For Each row As DataRow In dataTable.Rows
                Dim num3 As Integer = 0
                str1 = row("CUSNA1").ToString()
                empty1 = row("CUSCUN").ToString()
                empty = row("CUSTID").ToString()
                str = row("CUSIDN").ToString()
                empty2 = row("DLEMP").ToString()
                str2 = row("DLEAP").ToString()
                empty3 = row("DLEFP").ToString()
                str3 = wsEnvioAutomatico.ProcesarEnvioNominasByCliente(num, empty1, empty, str, empty2, str2, empty3, "OPERADORDES", num3)
                If (num3 <> 1) Then
                    Console.WriteLine(str3)
                    'Me.evEnvioNominas.WriteEntry(str3)
                    num2 = num2 + 1
                ElseIf (Not (str3 = "")) Then
                    Console.WriteLine(str3)
                    'Me.evEnvioNominas.WriteEntry(str3)
                    num1 = num1 + 1
                Else
                    Console.WriteLine(String.Concat("Envio de nómina para el cliente:  ", str1, " terminado sin errores."))
                    'Me.evEnvioNominas.WriteEnKtry(String.Concat("Envio de nómina para el cliente:  ", str1, " terminado sin errores."))
                    num1 = num1 + 1
                End If
            Next
            wsEnvioAutomatico.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarEnvioAutomatico", "Fin del Método: - Parámetros: Ninguno", "", "OperadorDES")
            wsEnvioAutomatico.ActualizarLogProcesosAutomaticos(num, dataTable.Rows.Count, num1, num2, "Envios Automáticos: Proceso Terminado.", 1, "OperadorDES")
        Catch exception1 As System.Exception
            Dim exception As System.Exception = exception1
            wsEnvioAutomatico.RegistrarLogEventoSistema("BifConvenio", 3, "ServiceProcesarEnvioAutomatico", "Fin del Método: - Parámetros: Ninguno", exception.ToString(), "OperadorDES")
            Console.WriteLine(exception.ToString())
            'Me.evEnvioNominas.WriteEntry(exception.ToString())
            wsEnvioAutomatico.ActualizarLogProcesosAutomaticos(num, 0, 0, 0, String.Concat("Envíos Automáticos: ", exception.ToString()), 3, "OperadorDES")
        End Try
    End Sub

    Private Sub ServiceProcesarReportesAutomaticos()
        Dim num As Integer = 0
        Dim num1 As Integer = 0
        Dim num2 As Integer = 0
        Dim num3 As Integer = 0
        Dim str As String = ""
        Dim str1 As String = ""
        Dim empty As String = String.Empty
        Dim dataTable As System.Data.DataTable = New System.Data.DataTable()
        'Me.objReportesAutomaticos.Credentials = CredentialCache.DefaultCredentials
        Dim wsReportesAutomatico As New wsReportesAutomatico.wsReportesAutomatico()

        Try
            'Me.evEnvioReportes.WriteEntry("Iniciando los Envíos automaticos de Reportes")
            dataTable = wsReportesAutomatico.GetFuncionarios()
            wsReportesAutomatico.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarReportesAutomaticos", "Inicio del Método: - Parámetros: Ninguno", "", "OperadorDES")
            num = wsReportesAutomatico.RegistrarLogProcesosAutomaticos(0, 0, 0, "Iniciando Envio de Reportes Automáticos", 1, "OperadorDES")
            For Each row As DataRow In dataTable.Rows
                Dim num4 As Integer = 0
                num3 = Convert.ToInt32(row("id_funcionario").ToString())
                str = row("nombre_funcionario").ToString()
                str1 = row("email_funcionario").ToString()
                empty = wsReportesAutomatico.EnviarReporteAutomaticoByFuncionario(num, num3, str, str1, "OPERADORDES", num4)
                If (num4 <> 1) Then
                    Console.WriteLine(empty)
                    'Me.evEnvioReportes.WriteEntry(empty)
                    num2 = num2 + 1
                ElseIf (Not (empty = "")) Then
                    Console.WriteLine(empty)
                    'Me.evEnvioReportes.WriteEntry(empty)
                    num1 = num1 + 1
                Else
                    Console.WriteLine(String.Concat("Envio del Reporte para el Funcionario:  ", str, " terminado sin errores."))
                    'Me.evEnvioReportes.WriteEntry(String.Concat("Envio del Reporte para el Funcionario:  ", str, " terminado sin errores."))
                    num1 = num1 + 1
                End If
            Next
            wsReportesAutomatico.RegistrarLogEventoSistema("BifConvenio", 1, "ServiceProcesarReportesAutomaticos", "Fin del Método: - Parámetros: Ninguno", "", "OperadorDES")
            wsReportesAutomatico.ActualizarLogProcesosAutomaticos(num, dataTable.Rows.Count, num1, num2, "Reportes Automáticos: Proceso Terminado.", 2, "OperadorDES")
        Catch exception1 As System.Exception
            Dim exception As System.Exception = exception1
            wsReportesAutomatico.RegistrarLogEventoSistema("BifConvenio", 3, "ServiceProcesarReportesAutomaticos", "Fin del Método: - Parámetros: Ninguno", exception.ToString(), "OperadorDES")
            Console.WriteLine(exception.ToString())
            'Me.evEnvioReportes.WriteEntry(exception.ToString())
            wsReportesAutomatico.ActualizarLogProcesosAutomaticos(num, 0, 0, 0, String.Concat("Reportes Automáticos: ", exception.ToString()), 3, "OperadorDES")
        End Try
    End Sub


    Sub metodo1()
        'Dim s As String = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")

        Dim servicio As New wsEnvioAutomatico.wsEnvioAutomatico()

        Dim resultado As String = servicio.ProcesarEnvioNominasByCliente(2, "31869", "8", "20100255325", "12", "2020", "20201116", "1", 0)

        Console.WriteLine(resultado)
        Console.ReadLine()


    End Sub



End Module

Option Strict Off
Option Explicit On 
Imports System.Configuration.ConfigurationSettings


'Operaciones de consulta y recuperación de cronograma futuro al servidor AS/400
Public Class ObtenerCronogramaFuturo

    '****************************************************************
    'Microsoft SQL Server 2000
    'Visual Basic file generated for DTS Package
    'File Name: c:\BIFConvenios - Importación de Cronograma futuro.bas
    'Package Name: BIFConvenios - Importación de Cronograma futuro
    'Package Description:
    'Generated Date: 05/02/2004
    'Generated Time: 10:29:34 a.m.
    '****************************************************************

    Private goPackageOld As New DTS.Package()
    Private goPackage As DTS.Package2

    Protected mCustomerNumber As String
    Protected mAnio As String
    Protected mMes As String
    Protected mPID As String
    Protected mfecha_ProcesoAS400 As String
    Protected mblnProcesoExitoso As Boolean = True


    '20040617 - Se adiciono un bool que indica si la importacion fue existosa
    Public Function ExecuteImport(ByVal PID As String, ByVal CustomerNumber As String, ByVal mes As String, ByVal anio As String, ByVal Fecha_ProcesoAS400 As String) As Boolean
        'Control de error en la ejecucion de un paquete de datos
        Try
            'On Error GoTo PackageError

            mCustomerNumber = CustomerNumber
            mMes = mes
            mAnio = anio
            mfecha_ProcesoAS400 = Fecha_ProcesoAS400
            mPID = PID

            goPackage = goPackageOld

            goPackage.Name = "BIFConvenios - Importación de Cronograma futuro V5"
            goPackage.WriteCompletionStatusToNTEventLog = False
            goPackage.FailOnError = False
            goPackage.PackagePriorityClass = 2
            goPackage.MaxConcurrentSteps = 4
            goPackage.LineageOptions = 0
            goPackage.UseTransaction = True
            goPackage.TransactionIsolationLevel = 4096
            goPackage.AutoCommitTransaction = True
            goPackage.RepositoryMetadataOptions = 0
            goPackage.UseOLEDBServiceComponents = True
            goPackage.LogToSQLServer = False
            goPackage.LogServerFlags = 0
            goPackage.FailPackageOnLogFailure = False
            goPackage.ExplicitGlobalVariables = False
            goPackage.PackageType = 0



            '---------------------------------------------------------------------------
            ' create package connection information
            '---------------------------------------------------------------------------

            Dim oConnection As DTS.Connection2

            '------------- a new connection defined below.
            'For security purposes, the password is never scripted

            oConnection = goPackage.Connections.New("IBMDA400")

            oConnection.ConnectionProperties.Item("Persist Security Info").Value = True
            oConnection.ConnectionProperties.Item("User ID").Value = AppSettings("AS400-Conv-UserId") '"PLAZO"
            oConnection.ConnectionProperties.Item("Data Source").Value = AppSettings("AS400-Conv-Server")  '"10.1.100.181"
            oConnection.ConnectionProperties.Item("Prompt").Value = 4
            oConnection.ConnectionProperties.Item("Protection Level").Value = 0
            oConnection.ConnectionProperties.Item("Connect Timeout").Value = 0
            oConnection.ConnectionProperties.Item("Initial Catalog").Value = AppSettings("AS400-Conv-Catalog") '"SISTEMA2"
            oConnection.ConnectionProperties.Item("OLE DB Services").Value = -1
            oConnection.ConnectionProperties.Item("Transport Product").Value = "Client Access"
            oConnection.ConnectionProperties.Item("SSL").Value = "DEFAULT"
            oConnection.ConnectionProperties.Item("Force Translate").Value = 65535
            oConnection.ConnectionProperties.Item("Default Collection").Value = AppSettings("AS400-Conv-Collection")  '"PLAZO"
            oConnection.ConnectionProperties.Item("Convert Date Time To Char").Value = "TRUE"
            oConnection.ConnectionProperties.Item("Cursor Sensitivity").Value = 3

            oConnection.Name = "Conexion a Convenios en AS/400"
            oConnection.ID = 1
            oConnection.Reusable = True
            oConnection.ConnectImmediate = False
            oConnection.DataSource = AppSettings("AS400-Conv-Server") '"10.1.100.181"
            oConnection.UserID = AppSettings("AS400-Conv-UserId") ' "PLAZO"
            oConnection.Password = AppSettings("AS400-Conv-Password")

            oConnection.ConnectionTimeout = 0
            oConnection.Catalog = AppSettings("AS400-Conv-Catalog") '  "SISTEMA2"
            oConnection.UseTrustedConnection = False
            oConnection.UseDSL = False

            'If you have a password for this connection, please uncomment and add your password below.

            'UPGRADE_WARNING: Couldn't resolve default property of object oConnection. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            goPackage.Connections.Add(oConnection)
            'UPGRADE_NOTE: Object oConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oConnection = Nothing

            '------------- a new connection defined below.
            'For security purposes, the password is never scripted

            oConnection = goPackage.Connections.New("SQLOLEDB")

            oConnection.ConnectionProperties.Item("Persist Security Info").Value = True
            oConnection.ConnectionProperties.Item("User ID").Value = AppSettings("DBUser") '"SA"
            oConnection.ConnectionProperties.Item("Initial Catalog").Value = AppSettings("DBName") '"BIFConveniosDesa"
            oConnection.ConnectionProperties.Item("Data Source").Value = AppSettings("ServerName")  '"TECNOLOGIA24"
            oConnection.ConnectionProperties.Item("Application Name").Value = "DTS Designer"

            oConnection.Name = "Microsoft OLE DB Provider for SQL Server"
            oConnection.ID = 2
            oConnection.Reusable = True
            oConnection.ConnectImmediate = False
            oConnection.DataSource = AppSettings("ServerName") '"TECNOLOGIA24"
            oConnection.UserID = AppSettings("DBUser") '"SA"
            oConnection.Password = AppSettings("DBUserPwd")
            oConnection.ConnectionTimeout = 60
            oConnection.Catalog = AppSettings("DBName") '"BIFConveniosDesa"
            oConnection.UseTrustedConnection = False
            oConnection.UseDSL = False

            'If you have a password for this connection, please uncomment and add your password below.
            'oConnection.Password = "<put the password here>"

            'UPGRADE_WARNING: Couldn't resolve default property of object oConnection. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            goPackage.Connections.Add(oConnection)
            'UPGRADE_NOTE: Object oConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oConnection = Nothing

            '---------------------------------------------------------------------------
            ' create package steps information
            '---------------------------------------------------------------------------

            Dim oStep As DTS.Step2
            Dim oPrecConstraint As DTS.PrecedenceConstraint

            '------------- a new step defined below

            oStep = goPackage.Steps.New

            oStep.Name = "DTSStep_DTSDataPumpTask_1"
            oStep.Description = "Obtener información de pago futuro para el cliente de convenio"
            oStep.ExecutionStatus = 4
            oStep.TaskName = "DTSTask_DTSDataPumpTask_1"
            oStep.CommitSuccess = False
            oStep.RollbackFailure = False
            oStep.ScriptLanguage = "VBScript"
            oStep.AddGlobalVariables = True
            oStep.RelativePriority = 3
            oStep.CloseConnection = False
            oStep.ExecuteInMainThread = True
            oStep.IsPackageDSORowset = False
            oStep.JoinTransactionIfPresent = False
            oStep.DisableStep = False
            oStep.FailPackageOnError = False

            'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            goPackage.Steps.Add(oStep)
            'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oStep = Nothing


            '--------- Informacion del nuevo step y el constraint agregado

            '------------- a new step defined below

            oStep = goPackage.Steps.New

            oStep.Name = "DTSStep_DTSExecuteSQLTask_1"
            oStep.Description = "Eliminar información de cronograma futuro importado"
            oStep.ExecutionStatus = 1
            oStep.TaskName = "DTSTask_DTSExecuteSQLTask_1"
            oStep.CommitSuccess = False
            oStep.RollbackFailure = False
            oStep.ScriptLanguage = "VBScript"
            oStep.AddGlobalVariables = True
            oStep.RelativePriority = 3
            oStep.CloseConnection = False
            oStep.ExecuteInMainThread = True
            oStep.IsPackageDSORowset = False
            oStep.JoinTransactionIfPresent = False
            oStep.DisableStep = False
            oStep.FailPackageOnError = False

            'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            goPackage.Steps.Add(oStep)
            'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oStep = Nothing

            '------------- a new step defined below

            oStep = goPackage.Steps.New

            oStep.Name = "DTSStep_DTSDataPumpTask_2"
            oStep.Description = "Obtener los registros del cronograma"
            oStep.ExecutionStatus = 4
            oStep.TaskName = "DTSTask_DTSDataPumpTask_2"
            oStep.CommitSuccess = False
            oStep.RollbackFailure = False
            oStep.ScriptLanguage = "VBScript"
            oStep.AddGlobalVariables = True
            oStep.RelativePriority = 3
            oStep.CloseConnection = False
            oStep.ExecuteInMainThread = True
            oStep.IsPackageDSORowset = False
            oStep.JoinTransactionIfPresent = False
            oStep.DisableStep = False
            oStep.FailPackageOnError = False

            'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            goPackage.Steps.Add(oStep)
            'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oStep = Nothing


            '------------- a precedence constraint for steps defined below

            oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_1")
            oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSDataPumpTask_1")
            oPrecConstraint.StepName = "DTSStep_DTSDataPumpTask_1"
            oPrecConstraint.PrecedenceBasis = 1
            oPrecConstraint.Value = 0

            oStep.PrecedenceConstraints.Add(oPrecConstraint)
            'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oPrecConstraint = Nothing
            '----------fin de nuevo step y constraint agregado
            '--------Nuevo constraint para precedencia con la importacion de datos del archivo DLCCR
            oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_1")
            oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSDataPumpTask_2")
            oPrecConstraint.StepName = "DTSStep_DTSDataPumpTask_2"
            oPrecConstraint.PrecedenceBasis = 1
            oPrecConstraint.Value = 0

            oStep.PrecedenceConstraints.Add(oPrecConstraint)
            'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            oPrecConstraint = Nothing
            '---------------Fin de constraint

            '---------------------------------------------------------------------------
            ' create package tasks information
            '---------------------------------------------------------------------------

            frmMonitor.Add2LogMessage("Preparando carga de información")

            '------------- call Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Obtener información de pago futuro para el cliente de convenio)
            Call Task_Sub1(goPackage)
            Call Task_Sub2(goPackage)
            Call Task_Sub3(goPackage)
            '---------------------------------------------------------------------------
            ' Save or execute package
            '---------------------------------------------------------------------------

            frmMonitor.Add2LogMessage("Ejecutando carga de información")
            'goPackage.SaveToSQLServer("(local)", "sa", "webdev")
            goPackage.FailOnError = True
            goPackage.Execute()
            'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            tracePackageError(goPackage)
            goPackage.UnInitialize()
            'to save a package instead of executing it, comment out the executing package lines above and uncomment the saving package line
            'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            goPackage = Nothing

            'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
            goPackageOld = Nothing

            Return mblnProcesoExitoso
            Exit Function
            'PackageError:
        Catch e As Exception

            mblnProcesoExitoso = False
            'If Err.Number = 0 Then Exit Function
            Dim sMsg As String
            'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
            'sMsg = "Ha ocurrido un error en la ejecución: " & sErrorNumConv(Err.Number) & vbCrLf & Err.Description & vbCrLf & sAccumStepErrors(goPackage)

            sMsg = "Ha ocurrido un error en la ejecución: " + vbCrLf + e.StackTrace.ToString

            '& sErrorNumConv(Err.Number) & vbCrLf & Err.Description & vbCrLf & sAccumStepErrors(goPackage)
            frmMonitor.Add2LogMessage(sMsg)
            'Throw New Exception("Error en la generacion del cronograma futuro")
        End Try
        Return mblnProcesoExitoso
    End Function


    '-------------------------------- Control de errores -------------------------
    Private Function sAccumStepErrors(ByVal objPackage As DTS.Package) As String
        'Acumula los pasos de error dentro de la información de mensaje de error
        Dim oStep As DTS.Step
        Dim sMessage As String
        Dim lErrNum As Integer
        Dim sDescr As String
        Dim sSource As String

        'Revisa los pasos que se han completado y fallado
        If Not objPackage Is Nothing Then
            For Each oStep In objPackage.Steps
                If oStep.ExecutionStatus = DTS.DTSStepExecStatus.DTSStepExecStat_Completed Then
                    If oStep.ExecutionResult = DTS.DTSStepExecResult.DTSStepExecResult_Failure Then

                        'Obtiene la informacion de error del paso y adiciona este al mensaje
                        oStep.GetExecutionErrorInfo(lErrNum, sSource, sDescr)
                        'MIGRAR INNOVA FALTA
                        'sMessage = sMessage & vbCrLf & "Step " & oStep.Name & " failed, error: " & sErrorNumConv(lErrNum) & vbCrLf & sDescr & vbCrLf
                    End If
                End If
            Next oStep
        End If
        sAccumStepErrors = sMessage
    End Function

    Private Function sErrorNumConv(ByVal lErrNum As Integer) As String
        'Convierte el numero de error en formas leibles, ambos hexadecimal y decimal para las palabras de bajo orden.
        'MIGRAR INNOVA FALTA
        If lErrNum < 65536 And lErrNum > -65536 Then
            sErrorNumConv = "x" & Hex(lErrNum) & ",  " & CStr(lErrNum)
        Else
            sErrorNumConv = "x" & Hex(lErrNum) & ",  x" & Hex(lErrNum And -65536) & " + " & CStr(lErrNum And 65535)
        End If
    End Function


    '-----------------------------------------------------------------------------
    ' error reporting using step.GetExecutionErrorInfo after execution
    '-----------------------------------------------------------------------------
    Private Sub tracePackageError(ByRef oPackage As DTS.Package)
        Dim ErrorCode As Integer
        Dim ErrorSource As String
        Dim ErrorDescription As String
        Dim ErrorHelpFile As String
        Dim ErrorHelpContext As Integer
        Dim ErrorIDofInterfaceWithError As String
        Dim i As Short

        For i = 1 To oPackage.Steps.Count
            If oPackage.Steps.Item(i).ExecutionResult = DTS.DTSStepExecResult.DTSStepExecResult_Failure Then
                oPackage.Steps.Item(i).GetExecutionErrorInfo(ErrorCode, ErrorSource, ErrorDescription, ErrorHelpFile, ErrorHelpContext, ErrorIDofInterfaceWithError)
                'MsgBox(oPackage.Steps.Item(i).Name & " failed" & vbCrLf & ErrorSource & vbCrLf & ErrorDescription)
                frmMonitor.Add2LogMessage(oPackage.Steps.Item(i).Name & " failed" & vbCrLf & ErrorSource & vbCrLf & ErrorDescription)
            End If
        Next i
    End Sub

    '------------- define Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Obtener información de pago futuro para el cliente de convenio)
    Public Sub Task_Sub1(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask1 As DTS.DataPumpTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSDataPumpTask")
        oTask.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1 = oTask.CustomTask

        oCustomTask1.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1.Description = "Obtener información de pago futuro para el cliente de convenio"
        oCustomTask1.SourceConnectionID = 1

        oCustomTask1.SourceSQLStatement = "SELECT '" & Trim(mPID) & "'  AS Codigo_proceso, e.DLCCC AS DLECC, e.DLAÑO AS DLEAN, e.DLAGC AS DLEAG, e.DLCOC AS DLECO, e.DLCCY AS DLEMO, e.DLACC AS DLENP, e.DLCEM AS DLECM, " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      e.DLNCL AS DLENE, e.DLAPP AS DLEPA, e.DLAPM AS DLEMA, trim(DLPRN) CONCAT ' ' CONCAT trim(DLSGN) AS DLEMN, " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      CASE WHEN TRIM(CAST(DLCCR AS CHARACTER(8)) CONCAT DLPLA CONCAT DLCUS) = '0' THEN '' ELSE CAST(SUBSTRING(TRIM(DLCUS) " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      CONCAT '0000', 1, 4)  CONCAT DLPLA CONCAT CAST(DLCCR AS CHARACTER(8))  AS CHARACTER(20)) END AS DLECR, 2000 + c.DLVCA AS DLEAP, " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      c.DLVCM AS DLEMP, SUM(r.DLIMC - r.DLIPC + r.DLITF) AS DLEIC, e.DLSTS AS DLEST, CAST(YEAR(CURRENT DATE) AS CHARACTER(4)) " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      CONCAT CASE LENGTH(TRIM(CAST(MONTH(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(MONTH(CURRENT DATE) " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(MONTH(CURRENT DATE) AS CHARACTER(2)), 3, 2) " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      END CONCAT CASE LENGTH(TRIM(CAST(DAY(CURRENT DATE) AS CHARACTER(2)))) WHEN 1 THEN '0' CONCAT TRIM(CAST(DAY(CURRENT DATE) " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      AS CHARACTER(2))) WHEN 2 THEN SUBSTRING('00' CONCAT CAST(DAY(CURRENT DATE) AS CHARACTER(2)), 3, 2) END AS DLEFP, " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      CASE WHEN COUNT(1) > 1 THEN 1 ELSE 0 END DLESD, e.DLFLI, COUNT(1) AS NUMCUOTAS" & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "FROM         DLCRE e INNER JOIN" & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & vbCrLf
        'REspinoza 20040809 - Modificacion para restringir cargas hasta la fecha del proceso
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      INNER JOIN DLEMP D ON (D.DLECUN = E.DLCCC AND D .DLEAEN = c.DLVCA AND D.DLEMEN >= c.DLVCM)" & vbCrLf
        'Anterior - oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "WHERE     (trim(r.DLSTS) = '')" & vbCrLf
        'Nuevo
        'oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "WHERE     (TRIM(r.DLSTS) = '') " AND R.DLVCM <= " & mMes & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " WHERE     (TRIM(r.DLSTS) = '') " 'AND R.DLVCM <= " & mMes & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " AND CAST (2000 + R.DLVCA AS CHARACTER (4)) "
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " CONCAT "
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " CASE WHEN LENGTH(TRIM(CAST(R.DLVCM AS CHARACTER (2) ))) = 1 THEN "
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " 	'0' CONCAT TRIM(CAST(R.DLVCM AS CHARACTER ( 2 )))"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " ELSE"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "	TRIM(CAST ( R.DLVCM AS CHARACTER ( 2 ) ))"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " END <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"

        'Fin de modificacion

        '    If CInt(mMes) <= Month(Now) Then
        '       oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " AND R.DLVCM <= " & mMes & vbCrLf
        '     Else
        '       oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " AND R.DLVCM > MONTH(CURRENT DATE) " & vbCrLf
        '    End If

        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "GROUP BY e.DLCCC, e.DLAÑO, e.DLAGC, e.DLCOC, e.DLCCY, e.DLACC, e.DLCEM, e.DLNCL, e.DLAPP, e.DLAPM, e.DLPRN, e.DLSGN, e.DLCCR, e.DLPLA, " & vbCrLf
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "                      e.DLCUS, c.DLNCT, c.DLVCA, c.DLVCM, c.DLVCD, e.DLSTS, e.DLFLI" & vbCrLf
        'oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & "HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = " + mCustomerNumber + " AND 2000 + c.DLVCA <= " + mAnio + " AND c.DLVCM <= " + mMes
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " HAVING      (c.DLNCT = MAX(r.DLNCT)) AND e.DLCCC = " + mCustomerNumber '+ " AND 2000 + c.DLVCA <= " + mAnio + " AND c.DLVCM <= " + mMes
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " AND CAST ( 2000+c.DLVCA  AS CHARACTER ( 4 ) ) "
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " CONCAT "
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " CASE WHEN LENGTH(TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " 	'0' CONCAT TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " ELSE"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " 	TRIM(CAST ( c.DLVCM AS CHARACTER ( 2 ) ))"
        oCustomTask1.SourceSQLStatement = oCustomTask1.SourceSQLStatement & " END <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"

        oCustomTask1.DestinationConnectionID = 2
        oCustomTask1.DestinationObjectName = "[DLENV]"
        oCustomTask1.ProgressRowCount = 1000
        oCustomTask1.MaximumErrorCount = 0
        oCustomTask1.FetchBufferSize = 1
        oCustomTask1.UseFastLoad = True
        oCustomTask1.InsertCommitSize = 0
        oCustomTask1.ExceptionFileColumnDelimiter = "|"
        oCustomTask1.ExceptionFileRowDelimiter = vbCrLf
        oCustomTask1.AllowIdentityInserts = False
        oCustomTask1.FirstRow = 0
        oCustomTask1.LastRow = 0
        oCustomTask1.FastLoadOptions = 2
        oCustomTask1.ExceptionFileOptions = 1
        oCustomTask1.DataPumpOptions = 0

        Call oCustomTask1_Trans_Sub1(oCustomTask1)
        Call oCustomTask1_Trans_Sub2(oCustomTask1)
        Call oCustomTask1_Trans_Sub3(oCustomTask1)
        Call oCustomTask1_Trans_Sub4(oCustomTask1)
        Call oCustomTask1_Trans_Sub5(oCustomTask1)
        Call oCustomTask1_Trans_Sub6(oCustomTask1)
        Call oCustomTask1_Trans_Sub7(oCustomTask1)
        Call oCustomTask1_Trans_Sub8(oCustomTask1)
        Call oCustomTask1_Trans_Sub9(oCustomTask1)
        Call oCustomTask1_Trans_Sub10(oCustomTask1)
        Call oCustomTask1_Trans_Sub11(oCustomTask1)
        Call oCustomTask1_Trans_Sub12(oCustomTask1)
        Call oCustomTask1_Trans_Sub13(oCustomTask1)
        Call oCustomTask1_Trans_Sub14(oCustomTask1)
        Call oCustomTask1_Trans_Sub15(oCustomTask1)
        Call oCustomTask1_Trans_Sub16(oCustomTask1)
        Call oCustomTask1_Trans_Sub17(oCustomTask1)
        Call oCustomTask1_Trans_Sub18(oCustomTask1)
        Call oCustomTask1_Trans_Sub19(oCustomTask1)
        Call oCustomTask1_Trans_Sub20(oCustomTask1)
        Call oCustomTask1_Trans_Sub21(oCustomTask1)


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask1 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub


    Private Sub oCustomTask1_Trans_Sub1(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__1"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLECC", 1)
        oColumn.Name = "DLECC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 9
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLECC", 1)
        oColumn.Name = "DLECC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 9
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub2(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__2"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEAN", 1)
        oColumn.Name = "DLEAN"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEAN", 1)
        oColumn.Name = "DLEAN"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub3(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__3"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEAG", 1)
        oColumn.Name = "DLEAG"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEAG", 1)
        oColumn.Name = "DLEAG"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub4(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__4"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLECO", 1)
        oColumn.Name = "DLECO"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLECO", 1)
        oColumn.Name = "DLECO"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub5(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__5"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEMO", 1)
        oColumn.Name = "DLEMO"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEMO", 1)
        oColumn.Name = "DLEMO"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub6(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__6"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLENP", 1)
        oColumn.Name = "DLENP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLENP", 1)
        oColumn.Name = "DLENP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub7(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__7"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLECM", 1)
        oColumn.Name = "DLECM"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLECM", 1)
        oColumn.Name = "DLECM"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub8(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__8"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLENE", 1)
        oColumn.Name = "DLENE"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 75
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLENE", 1)
        oColumn.Name = "DLENE"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 75
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub9(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__9"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLECR", 1)
        oColumn.Name = "DLECR"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLECR", 1)
        oColumn.Name = "DLECR"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub10(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__10"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEAP", 1)
        oColumn.Name = "DLEAP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEAP", 1)
        oColumn.Name = "DLEAP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub11(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__11"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEMP", 1)
        oColumn.Name = "DLEMP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEMP", 1)
        oColumn.Name = "DLEMP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub12(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__12"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEIC", 1)
        oColumn.Name = "DLEIC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEIC", 1)
        oColumn.Name = "DLEIC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub13(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__13"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEST", 1)
        oColumn.Name = "DLEST"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEST", 1)
        oColumn.Name = "DLEST"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub14(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__14"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEFP", 1)
        oColumn.Name = "DLEFP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 8
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEFP", 1)
        oColumn.Name = "DLEFP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 8
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub15(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__15"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEPA", 1)
        oColumn.Name = "DLEPA"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEPA", 1)
        oColumn.Name = "DLEPA"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub16(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__16"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEMA", 1)
        oColumn.Name = "DLEMA"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEMA", 1)
        oColumn.Name = "DLEMA"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub17(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__17"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLEMN", 1)
        oColumn.Name = "DLEMN"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLEMN", 1)
        oColumn.Name = "DLEMN"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 25
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub18(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__18"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLESD", 1)
        oColumn.Name = "DLESD"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLESD", 1)
        oColumn.Name = "DLESD"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub19(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__19"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("NUMCUOTAS", 1)
        oColumn.Name = "NUMCUOTAS"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 3
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("NUMCUOTAS", 1)
        oColumn.Name = "NUMCUOTAS"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 3
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        oTransProps = Nothing

        oCustomTask1.Transformations.Add(oTransformation)
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub20(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__20"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("CODIGO_PROCESO", 1)
        oColumn.Name = "CODIGO_PROCESO"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 36
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Codigo_proceso", 1)
        oColumn.Name = "Codigo_proceso"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 72
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        oTransProps = Nothing

        oCustomTask1.Transformations.Add(oTransformation)
        oTransformation = Nothing
        'Dim oTransformation As DTS.Transformation2
        'Dim oTransProps As DTS.Properties
        'Dim oColumn As DTS.Column
        ''UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        'oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        'oTransformation.Name = "DTSTransformation__20"
        'oTransformation.TransformFlags = 63
        'oTransformation.ForceSourceBlobsBuffered = 0
        'oTransformation.ForceBlobsInMemory = False
        'oTransformation.InMemoryBlobSize = 1048576
        'oTransformation.TransformPhases = 4

        'oColumn = oTransformation.SourceColumns.New("NUMCUOTAS", 1)
        'oColumn.Name = "NUMCUOTAS"
        'oColumn.Ordinal = 1
        'oColumn.Flags = 20
        'oColumn.Size = 0
        'oColumn.DataType = 3
        'oColumn.Precision = 0
        'oColumn.NumericScale = 0
        'oColumn.Nullable = False

        'oTransformation.SourceColumns.Add(oColumn)
        ''UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        'oColumn = Nothing

        'oColumn = oTransformation.DestinationColumns.New("NUMCUOTAS", 1)
        'oColumn.Name = "NUMCUOTAS"
        'oColumn.Ordinal = 1
        'oColumn.Flags = 24
        'oColumn.Size = 0
        'oColumn.DataType = 3
        'oColumn.Precision = 0
        'oColumn.NumericScale = 0
        'oColumn.Nullable = False

        'oTransformation.DestinationColumns.Add(oColumn)
        'oColumn = Nothing

        'oTransProps = oTransformation.TransformServerProperties


        'oTransProps = Nothing

        'oCustomTask1.Transformations.Add(oTransformation)
        'oTransformation = Nothing

    End Sub


    Private Sub oCustomTask1_Trans_Sub21(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__21"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLFLI", 1)
        oColumn.Name = "DLFLI"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLFLI", 1)
        oColumn.Name = "DLFLI"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub


    Private Sub Task_Sub2(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask2 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2 = oTask.CustomTask

        oCustomTask2.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2.Description = "Eliminar información de cronograma futuro importado"
        '        oCustomTask2.SQLStatement = "delete from DLenv where dlecc = " & mCustomerNumber
        'oCustomTask2.SQLStatement = "DELETE FROM DLCCR WHERE DLCCC = " & mCustomerNumber & " AND DLVCA = " & mAnio & " AND DLVCM = " & mMes
        oCustomTask2.SQLStatement = "DELETE FROM DLCCR WHERE DLVCA + 2000 = " & mAnio & " And DLVCM = 24" '& mMes
        oCustomTask2.SQLStatement = oCustomTask2.SQLStatement & " AND DLACC IN ( SELECT DLACC FROM DLCRE WHERE DLCCC =  -100000)" ' & mCustomerNumber & " )"
        oCustomTask2.ConnectionID = 1
        oCustomTask2.CommandTimeout = 0
        oCustomTask2.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask2 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing
    End Sub

    '------------- define Task_Sub3 for task DTSTask_DTSDataPumpTask_2 (Obtener los registros del cronograma)
    Public Sub Task_Sub3(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask3 As DTS.DataPumpTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSDataPumpTask")
        oTask.Name = "DTSTask_DTSDataPumpTask_2"
        oCustomTask3 = oTask.CustomTask

        oCustomTask3.Name = "DTSTask_DTSDataPumpTask_2"
        oCustomTask3.Description = "Obtener los registros del cronograma"
        oCustomTask3.SourceConnectionID = 1

        ' oCustomTask3.SourceSQLStatement = "SELECT  '" & Trim(mPID) & "'  AS Codigo_proceso,  C.*" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "FROM         DLCRE e INNER JOIN" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "                      DLCCR r ON e.DLACC = r.DLACC INNER JOIN" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "                      DLCCR c ON e.DLACC = c.DLACC AND c.DLSTS = ''" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "WHERE     (trim(r.DLSTS) = '')" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "AND" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "e.DLCCC = " & mCustomerNumber & " AND 2000 + c.DLVCA = " & mAnio & " AND c.DLVCM = " & mMes
        '----
        '    oCustomTask3.SourceSQLStatement = "SELECT '" & Trim(mPID) & "'  AS Codigo_proceso, DLCCR.*" & vbCrLf
        '   oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "FROM DLCCR   WHERE 2000+DLVCA  <= " & mAnio & "  AND DLVCM <= " & mMes & " AND DLACC" & vbCrLf
        '  oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "                      IN (SELECT DLACC FROM         DLCRE WHERE DLCCC  = " & mCustomerNumber & " )" & vbCrLf
        ' oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "AND DLSTS = ''"
        '---

        oCustomTask3.SourceSQLStatement = "SELECT '" & Trim(mPID) & "'  AS Codigo_proceso, DLCCR.*" & vbCrLf
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "FROM DLCCR "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "WHERE " '2000+DLVCA  <= " & mAnio & "  AND DLVCM <= " & mMes & " AND DLACC" & vbCrLf

        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " CAST ( 2000+DLVCA  AS CHARACTER ( 4 ) ) "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " CONCAT "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " CASE WHEN LENGTH(TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))) = 1 THEN "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " 	'0' CONCAT TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) )) "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " ELSE "
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " 	TRIM(CAST ( DLVCM AS CHARACTER ( 2 ) ))"
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " END"
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " <='" & mAnio & IIf(Len(Trim(mMes)) = 1, "0" + Trim(mMes), Trim(mMes)) & "'"

        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & " AND DLACC IN (SELECT DLACC FROM         DLCRE WHERE DLCCC  = " & mCustomerNumber & " )" & vbCrLf
        oCustomTask3.SourceSQLStatement = oCustomTask3.SourceSQLStatement & "AND DLSTS = ''"


        oCustomTask3.DestinationConnectionID = 2
        oCustomTask3.DestinationObjectName = "[DLCCR]"
        oCustomTask3.ProgressRowCount = 1000
        oCustomTask3.MaximumErrorCount = 0
        oCustomTask3.FetchBufferSize = 1
        oCustomTask3.UseFastLoad = True
        oCustomTask3.InsertCommitSize = 0
        oCustomTask3.ExceptionFileColumnDelimiter = "|"
        oCustomTask3.ExceptionFileRowDelimiter = vbCrLf
        oCustomTask3.AllowIdentityInserts = False
        oCustomTask3.FirstRow = 0
        oCustomTask3.LastRow = 0
        oCustomTask3.FastLoadOptions = 2
        oCustomTask3.ExceptionFileOptions = 1
        oCustomTask3.DataPumpOptions = 0

        Call oCustomTask3_Trans_Sub1(oCustomTask3)
        Call oCustomTask3_Trans_Sub2(oCustomTask3)
        Call oCustomTask3_Trans_Sub3(oCustomTask3)
        Call oCustomTask3_Trans_Sub4(oCustomTask3)
        Call oCustomTask3_Trans_Sub5(oCustomTask3)
        Call oCustomTask3_Trans_Sub6(oCustomTask3)
        Call oCustomTask3_Trans_Sub7(oCustomTask3)
        Call oCustomTask3_Trans_Sub8(oCustomTask3)
        Call oCustomTask3_Trans_Sub9(oCustomTask3)
        Call oCustomTask3_Trans_Sub10(oCustomTask3)
        Call oCustomTask3_Trans_Sub11(oCustomTask3)


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask3 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub1(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__1"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLACC", 1)
        oColumn.Name = "DLACC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLACC", 1)
        oColumn.Name = "DLACC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub2(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__2"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLCCY", 1)
        oColumn.Name = "DLCCY"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLCCY", 1)
        oColumn.Name = "DLCCY"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub3(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__3"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLVCA", 1)
        oColumn.Name = "DLVCA"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLVCA", 1)
        oColumn.Name = "DLVCA"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub4(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__4"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLVCM", 1)
        oColumn.Name = "DLVCM"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLVCM", 1)
        oColumn.Name = "DLVCM"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub5(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__5"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLVCD", 1)
        oColumn.Name = "DLVCD"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLVCD", 1)
        oColumn.Name = "DLVCD"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub6(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__6"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLNCT", 1)
        oColumn.Name = "DLNCT"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLNCT", 1)
        oColumn.Name = "DLNCT"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub7(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__7"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLIMC", 1)
        oColumn.Name = "DLIMC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLIMC", 1)
        oColumn.Name = "DLIMC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub8(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__8"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLIPC", 1)
        oColumn.Name = "DLIPC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLIPC", 1)
        oColumn.Name = "DLIPC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub9(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__9"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLSTS", 1)
        oColumn.Name = "DLSTS"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLSTS", 1)
        oColumn.Name = "DLSTS"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask3_Trans_Sub10(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__10"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("CODIGO_PROCESO", 1)
        oColumn.Name = "CODIGO_PROCESO"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 36
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Codigo_proceso", 1)
        oColumn.Name = "Codigo_proceso"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 72
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        oTransProps = Nothing

        oCustomTask3.Transformations.Add(oTransformation)
        oTransformation = Nothing

    End Sub

    Public Sub oCustomTask3_Trans_Sub11(ByVal oCustomTask3 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask3, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__11"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLITF", 1)
        oColumn.Name = "DLITF"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLITF", 1)
        oColumn.Name = "DLITF"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 15
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask3.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask3.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

End Class
Option Strict Off
Option Explicit On 
Imports System.Configuration.ConfigurationSettings

Public Class EliminacionErrorEnvio

    '****************************************************************
    'Microsoft SQL Server 2000
    'Visual Basic file generated for DTS Package
    'File Name: c:\BIFConvenios - Eliminación de información en error de envio.bas
    'Package Name: BIFConvenios - Eliminación de información en error de envio
    'Package Description:
    'Generated Date: 19/02/2004
    'Generated Time: 06:05:31 p.m.
    '****************************************************************
    'DLRCC =107476 AND DLRAP = 2004 AND DLRMP = 3 

    Private mCustomerNumber As String
    Private mAnio As String
    Private mMes As String
    Private mFecha_ProcesoAS400 As String
    Private goPackageOld As New DTS.Package()
    Private goPackage As DTS.Package2


    Public Sub Execute(ByVal CustomerNumber As String, _
                            ByVal Anio As String, ByVal Mes As String, _
                            ByVal Fecha_ProcesoAS400 As String)

        On Error GoTo PackageError

        goPackage = goPackageOld


        mCustomerNumber = CustomerNumber
        mAnio = Anio
        mMes = Mes
        mFecha_ProcesoAS400 = Fecha_ProcesoAS400

        goPackage.Name = "BIFConvenios - Eliminación de información en error de envio"
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

        oConnection.Name = "Conexion a AS/400"
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

        oStep.Name = "DTSStep_DTSExecuteSQLTask_1"
        oStep.Description = "Eliminación de información de AS/400"
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

        '---------------------------------------------------------------------------
        ' create package tasks information
        '---------------------------------------------------------------------------

        '------------- call Task_Sub1 for task DTSTask_DTSExecuteSQLTask_1 (Eliminación de información de AS/400)
        Call Task_Sub1(goPackage)

        '---------------------------------------------------------------------------
        ' Save or execute package
        '---------------------------------------------------------------------------

        'goPackage.SaveToSQLServer "(local)", "sa", ""
        goPackage.Execute()
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        tracePackageError(goPackage)
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package lines above and uncomment the saving package line
        'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackage = Nothing

        'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackageOld = Nothing

        Exit Sub
PackageError:
        If Err.Number = 0 Then Exit Sub
        Dim sMsg As String
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        sMsg = "Ha ocurrio un error en la ejecución: " & sErrorNumConv(Err.Number) & vbCrLf & Err.Description & vbCrLf & sAccumStepErrors(goPackage)
        frmMonitor.Add2LogMessage(sMsg)

        'RESPINOZA 20070821 - Se agrega esto para liberar los recursos del package y evitar reiniciar el app
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package lines above and uncomment the saving package line
        goPackage = Nothing

        goPackageOld = Nothing
    End Sub

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
                frmMonitor.Add2LogMessage(oPackage.Steps.Item(i).Name & " failed" & vbCrLf & ErrorSource & vbCrLf & ErrorDescription)
            End If
        Next i

    End Sub

    '------------- define Task_Sub1 for task DTSTask_DTSExecuteSQLTask_1 (Eliminación de información de AS/400)
    Private Sub Task_Sub1(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask1 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask1 = oTask.CustomTask

        oCustomTask1.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask1.Description = "Eliminación de información de AS/400"

        oCustomTask1.SQLStatement = "delete from DLREC where DLRCC =" & mCustomerNumber & " AND DLRAP = " & mAnio & " AND DLRMP = " & mMes & " AND DLRFP = " & mFecha_ProcesoAS400
        oCustomTask1.ConnectionID = 1
        oCustomTask1.CommandTimeout = 0
        oCustomTask1.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask1 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

End Class


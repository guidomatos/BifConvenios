Option Strict Off
Option Explicit On 
Imports System.Configuration.ConfigurationSettings

'Respinoza - 20040402

Public Class ProcesaArchivoDescuentosTexto
    Public goPackageOld As New DTS.Package()
    Public goPackage As DTS.Package2
    Private mruta As String
    Private mIdProceso As String


    Public Sub ExecuteProcesoArchivoDescuentosEstandardTexto(ByRef ruta As String, ByVal idProceso As String)
        On Error GoTo PackageError
        goPackage = goPackageOld

        mruta = ruta
        mIdProceso = idProceso

        goPackage.Name = "BIFConvenios - Proceso Carga Archivo Descuentos Texto"
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

        oConnection = goPackage.Connections.New("SQLOLEDB")

        oConnection.ConnectionProperties.Item("Persist Security Info").Value = True
        oConnection.ConnectionProperties.Item("User ID").Value = AppSettings("DBUser")
        oConnection.ConnectionProperties.Item("Initial Catalog").Value = AppSettings("DBName")  '
        oConnection.ConnectionProperties.Item("Data Source").Value = AppSettings("ServerName")
        oConnection.ConnectionProperties.Item("Connect Timeout").Value = 60
        oConnection.ConnectionProperties.Item("Application Name").Value = "DTS Designer"

        oConnection.Name = "Conexión a base de datos de Convenios"
        oConnection.ID = 2
        oConnection.Reusable = True
        oConnection.ConnectImmediate = False
        oConnection.DataSource = AppSettings("ServerName")
        oConnection.UserID = AppSettings("DBUser")
        oConnection.Password = AppSettings("DBUserPwd")
        oConnection.ConnectionTimeout = 60
        oConnection.Catalog = AppSettings("DBName")
        oConnection.UseTrustedConnection = False
        oConnection.UseDSL = False


        'UPGRADE_WARNING: Couldn't resolve default property of object oConnection. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Connections.Add(oConnection)
        'UPGRADE_NOTE: Object oConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oConnection = Nothing

        '------------- a new connection defined below.
        'For security purposes, the password is never scripted

        oConnection = goPackage.Connections.New("DTSFlatFile")

        oConnection.ConnectionProperties.Item("Data Source").Value = mruta
        oConnection.ConnectionProperties.Item("Mode").Value = 1
        oConnection.ConnectionProperties.Item("Row Delimiter").Value = vbCrLf
        oConnection.ConnectionProperties.Item("File Format").Value = 2
        oConnection.ConnectionProperties.Item("Column Lengths").Value = "12,3,12,20,75,20,4,2,14,2,14"
        oConnection.ConnectionProperties.Item("Column Delimiter").Value = ","
        oConnection.ConnectionProperties.Item("File Type").Value = 1
        oConnection.ConnectionProperties.Item("Skip Rows").Value = 0
        oConnection.ConnectionProperties.Item("Text Qualifier").Value = """"
        oConnection.ConnectionProperties.Item("First Row Column Name").Value = False
        oConnection.ConnectionProperties.Item("Number of Column").Value = 11
        oConnection.ConnectionProperties.Item("Max characters per delimited column").Value = 255

        oConnection.Name = "Archivo de descuentos "
        oConnection.ID = 1
        oConnection.Reusable = True
        oConnection.ConnectImmediate = False
        oConnection.DataSource = mruta
        oConnection.ConnectionTimeout = 60
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
        oStep.Description = "Transform Data Task: undefined"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSDataPumpTask_1"
        oStep.CommitSuccess = False
        oStep.RollbackFailure = False
        oStep.ScriptLanguage = "VBScript"
        oStep.AddGlobalVariables = True
        oStep.RelativePriority = 3
        oStep.CloseConnection = True
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

        oStep.Name = "DTSStep_DTSExecuteSQLTask_1"
        oStep.Description = "Proceso de validación y actualización de información"
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

        oStep.Name = "DTSStep_DTSExecuteSQLTask_2"
        oStep.Description = "Regulariza Montos Carga Texto"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSExecuteSQLTask_2"
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
        oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSExecuteSQLTask_2")
        oPrecConstraint.StepName = "DTSStep_DTSExecuteSQLTask_2"
        oPrecConstraint.PrecedenceBasis = 1
        oPrecConstraint.Value = 0

        oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oPrecConstraint = Nothing

        '------------- a precedence constraint for steps defined below

        oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_2")
        oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSDataPumpTask_1")
        oPrecConstraint.StepName = "DTSStep_DTSDataPumpTask_1"
        oPrecConstraint.PrecedenceBasis = 1
        oPrecConstraint.Value = 0

        oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oPrecConstraint = Nothing

        '---------------------------------------------------------------------------
        ' create package tasks information
        '---------------------------------------------------------------------------

        '------------- call Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Transform Data Task: undefined)
        Call Task_Sub1(goPackage)

        '------------- call Task_Sub2 for task DTSTask_DTSExecuteSQLTask_1 (Proceso de validación y actualización de información)
        Call Task_Sub2(goPackage)

        '------------- call Task_Sub3 for task DTSTask_DTSExecuteSQLTask_2 (Regulariza Montos Carga Texto)
        Call Task_Sub3(goPackage)

        '---------------------------------------------------------------------------
        ' Save or execute package
        '---------------------------------------------------------------------------

        'goPackage.SaveToSQLServer "(local)", "sa", ""
        frmMonitor.Add2LogMessage("Ejecutando")
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
        frmMonitor.Add2LogMessage("Ejecución finalizada")
PackageError:
        If Err.Number = 0 Then Exit Sub
        Dim sMsg As String
        'Throw New Exception("Ocurrio un error al procesar el archivo de descuentos estandard")
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        sMsg = "Ha ocurrio un error en la ejecución: " & sErrorNumConv(Err.Number) & vbCrLf & Err.Description & vbCrLf & sAccumStepErrors(goPackage)
        frmMonitor.Add2LogMessage(sMsg)

        Dim oProceso As New Proceso()
        oProceso.UpdErrorCargaArchivoDescuentos(idProceso, "Server")

        'RESPINOZA 20070821 - Se agrega esto para liberar los recursos del package y evitar reiniciar el app
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package lines above and uncomment the saving package line
        'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackage = Nothing

        'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
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
                        sMessage = sMessage & vbCrLf & "Step " & oStep.Name & " failed, error: " & sErrorNumConv(lErrNum) & vbCrLf & sDescr & vbCrLf
                    End If
                End If
            Next oStep
        End If
        sAccumStepErrors = sMessage
    End Function

    Private Function sErrorNumConv(ByVal lErrNum As Integer) As String
        'Convierte el numero de error en formas leibles, ambos hexadecimal y decimal para las palabras de bajo orden.
        If lErrNum < 65536 And lErrNum > -65536 Then
            sErrorNumConv = "x" & Hex(lErrNum) & ",  " & CStr(lErrNum)
        Else
            sErrorNumConv = "x" & Hex(lErrNum) & ",  x" & Hex(lErrNum And -65536) & " + " & CStr(lErrNum And 65535)
        End If
    End Function

    '-----------------------------------------------------------------------------



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



    '------------- define Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Transform Data Task: undefined)
    Private Sub Task_Sub1(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask1 As DTS.DataPumpTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSDataPumpTask")
        oTask.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1 = oTask.CustomTask

        oCustomTask1.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1.Description = "Transform Data Task: undefined"
        oCustomTask1.SourceConnectionID = 1
        oCustomTask1.SourceObjectName = mruta ' "C:\Inetpub\wwwroot\BIFConvenios\files\cargados\ad25377fdc-cde2-48d9-b4a5-d8a2de15e2d4.txt"
        oCustomTask1.DestinationConnectionID = 2
        oCustomTask1.DestinationObjectName = "[ARCHIVODESCUENTOS]"
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
        '------- A Lookup is defined here

        oLookup = oCustomTask1.Lookups.New("IDProceso")
        oLookup.Name = "IDProceso"
        oLookup.ConnectionID = 2
        oLookup.Query = "SELECT   '" & Trim(mIdProceso) & "' AS Codigo_Proceso"
        oLookup.MaxCacheRows = 0

        oCustomTask1.Lookups.Add(oLookup)
        oLookup = Nothing




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

        oColumn = oTransformation.SourceColumns.New("Col001", 1)
        oColumn.Name = "Col001"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("CodigoBanco", 1)
        oColumn.Name = "CodigoBanco"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col002", 1)
        oColumn.Name = "Col002"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Moneda", 1)
        oColumn.Name = "Moneda"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col003", 1)
        oColumn.Name = "Col003"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("NumeroPagare", 1)
        oColumn.Name = "NumeroPagare"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col004", 1)
        oColumn.Name = "Col004"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("CodigoModular", 1)
        oColumn.Name = "CodigoModular"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col005", 1)
        oColumn.Name = "Col005"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("NombreTrabajador", 1)
        oColumn.Name = "NombreTrabajador"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col006", 1)
        oColumn.Name = "Col006"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("CodigoReferencia", 1)
        oColumn.Name = "CodigoReferencia"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col007", 1)
        oColumn.Name = "Col007"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Anio", 1)
        oColumn.Name = "Anio"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col008", 1)
        oColumn.Name = "Col008"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Mes", 1)
        oColumn.Name = "Mes"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col009", 1)
        oColumn.Name = "Col009"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("Cuota", 1)
        oColumn.Name = "Cuota"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col010", 1)
        oColumn.Name = "Col010"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("SituacionLaboral", 1)
        oColumn.Name = "SituacionLaboral"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

        oColumn = oTransformation.SourceColumns.New("Col011", 1)
        oColumn.Name = "Col011"
        oColumn.Ordinal = 1
        oColumn.Flags = 32
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("MontoDescuento", 1)
        oColumn.Name = "MontoDescuento"
        oColumn.Ordinal = 1
        oColumn.Flags = 104
        oColumn.Size = 255
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = True

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

    Public Sub oCustomTask1_Trans_Sub12(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTSPump.DataPumpTransformScript")
        oTransformation.Name = "DTSTransformation__12"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oTransProps = oTransformation.TransformServerProperties

        oTransProps.Item("Text").Value = "'**********************************************************************" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "'  Visual Basic Transformation Script" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "'************************************************************************" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "'  Copy each source column to the destination column" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "Function Main()" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "	DTSDestination(""codigo_proceso"") = DTSLookups(""IDProceso"").Execute("""")" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "	Main = DTSTransformStat_OK" & vbCrLf
        oTransProps.Item("Text").Value = oTransProps.Item("Text").Value & "End Function"
        oTransProps.Item("Language").Value = "VBScript"
        oTransProps.Item("FunctionEntry").Value = "Main"

        oTransProps = Nothing

        oCustomTask1.Transformations.Add(oTransformation)
        oTransformation = Nothing

    End Sub


    '------------- define Task_Sub2 for task DTSTask_DTSExecuteSQLTask_1 (Proceso de validación y actualización de información)
    Private Sub Task_Sub2(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask2 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2 = oTask.CustomTask

        oCustomTask2.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2.Description = "Proceso de validación y actualización de información"
        oCustomTask2.SQLStatement = "execute ProcesaArchivoDescuentoDefault '" & mIdProceso.Trim() & "','Server'"
        oCustomTask2.ConnectionID = 2
        oCustomTask2.CommandTimeout = 0
        oCustomTask2.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask2 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

    '------------- define Task_Sub3 for task DTSTask_DTSExecuteSQLTask_2 (Regulariza Montos Carga Texto)
    Private Sub Task_Sub3(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask3 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_2"
        oCustomTask3 = oTask.CustomTask


        oCustomTask3.Name = "DTSTask_DTSExecuteSQLTask_2"
        oCustomTask3.Description = "Regulariza Montos Carga Texto"
        oCustomTask3.SQLStatement = "EXECUTE RegularizaMontosCargaTexto '" & mIdProceso & "'"
        oCustomTask3.ConnectionID = 2
        oCustomTask3.CommandTimeout = 0
        oCustomTask3.OutputAsRecordset = False

        goPackage.Tasks.Add(oTask)

        oCustomTask3 = Nothing
        oTask = Nothing
    End Sub
End Class

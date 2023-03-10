'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace wsAlertasAutomaticas
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="wsAlertasAutomaticasSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class wsAlertasAutomaticas
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private RegistrarLogEventoSistemaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RegistrarLogEnvioOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RegistrarLogProcesosAutomaticosOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ActualizarLogProcesosAutomaticosOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObtenerListClienteUltimoProcesoOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ProcesarAlertaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.ConsoleApplication1.My.MySettings.Default.ConsoleApplication1_wsAlertasAutomaticas_wsAlertasAutomaticas
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event RegistrarLogEventoSistemaCompleted As RegistrarLogEventoSistemaCompletedEventHandler
        
        '''<remarks/>
        Public Event RegistrarLogEnvioCompleted As RegistrarLogEnvioCompletedEventHandler
        
        '''<remarks/>
        Public Event RegistrarLogProcesosAutomaticosCompleted As RegistrarLogProcesosAutomaticosCompletedEventHandler
        
        '''<remarks/>
        Public Event ActualizarLogProcesosAutomaticosCompleted As ActualizarLogProcesosAutomaticosCompletedEventHandler
        
        '''<remarks/>
        Public Event ObtenerListClienteUltimoProcesoCompleted As ObtenerListClienteUltimoProcesoCompletedEventHandler
        
        '''<remarks/>
        Public Event ProcesarAlertaCompleted As ProcesarAlertaCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegistrarLogEventoSistema", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RegistrarLogEventoSistema(ByVal pstrHilo As String, ByVal penuNivel As Integer, ByVal pstrAccion As String, ByVal pstrMensaje As String, ByVal pstrExcepcion As String, ByVal pstrUsuario As String) As Integer
            Dim results() As Object = Me.Invoke("RegistrarLogEventoSistema", New Object() {pstrHilo, penuNivel, pstrAccion, pstrMensaje, pstrExcepcion, pstrUsuario})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogEventoSistemaAsync(ByVal pstrHilo As String, ByVal penuNivel As Integer, ByVal pstrAccion As String, ByVal pstrMensaje As String, ByVal pstrExcepcion As String, ByVal pstrUsuario As String)
            Me.RegistrarLogEventoSistemaAsync(pstrHilo, penuNivel, pstrAccion, pstrMensaje, pstrExcepcion, pstrUsuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogEventoSistemaAsync(ByVal pstrHilo As String, ByVal penuNivel As Integer, ByVal pstrAccion As String, ByVal pstrMensaje As String, ByVal pstrExcepcion As String, ByVal pstrUsuario As String, ByVal userState As Object)
            If (Me.RegistrarLogEventoSistemaOperationCompleted Is Nothing) Then
                Me.RegistrarLogEventoSistemaOperationCompleted = AddressOf Me.OnRegistrarLogEventoSistemaOperationCompleted
            End If
            Me.InvokeAsync("RegistrarLogEventoSistema", New Object() {pstrHilo, penuNivel, pstrAccion, pstrMensaje, pstrExcepcion, pstrUsuario}, Me.RegistrarLogEventoSistemaOperationCompleted, userState)
        End Sub
        
        Private Sub OnRegistrarLogEventoSistemaOperationCompleted(ByVal arg As Object)
            If (Not (Me.RegistrarLogEventoSistemaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RegistrarLogEventoSistemaCompleted(Me, New RegistrarLogEventoSistemaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegistrarLogEnvio", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RegistrarLogEnvio(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintCodigoCliente As Integer, ByVal pintCodigoIBS As Integer, ByVal penuTipoEnvio As Integer, ByVal pstrCodigoProceso As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrMensaje As String, ByVal penuEstado As enumLogEnvioCorreo, ByVal pstrUsuario As String) As Integer
            Dim results() As Object = Me.Invoke("RegistrarLogEnvio", New Object() {pintCodigoProcesoAutomatico, pintCodigoCliente, pintCodigoIBS, penuTipoEnvio, pstrCodigoProceso, pintAnioPeriodo, pintMesPeriodo, pstrMensaje, penuEstado, pstrUsuario})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogEnvioAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintCodigoCliente As Integer, ByVal pintCodigoIBS As Integer, ByVal penuTipoEnvio As Integer, ByVal pstrCodigoProceso As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrMensaje As String, ByVal penuEstado As enumLogEnvioCorreo, ByVal pstrUsuario As String)
            Me.RegistrarLogEnvioAsync(pintCodigoProcesoAutomatico, pintCodigoCliente, pintCodigoIBS, penuTipoEnvio, pstrCodigoProceso, pintAnioPeriodo, pintMesPeriodo, pstrMensaje, penuEstado, pstrUsuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogEnvioAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintCodigoCliente As Integer, ByVal pintCodigoIBS As Integer, ByVal penuTipoEnvio As Integer, ByVal pstrCodigoProceso As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrMensaje As String, ByVal penuEstado As enumLogEnvioCorreo, ByVal pstrUsuario As String, ByVal userState As Object)
            If (Me.RegistrarLogEnvioOperationCompleted Is Nothing) Then
                Me.RegistrarLogEnvioOperationCompleted = AddressOf Me.OnRegistrarLogEnvioOperationCompleted
            End If
            Me.InvokeAsync("RegistrarLogEnvio", New Object() {pintCodigoProcesoAutomatico, pintCodigoCliente, pintCodigoIBS, penuTipoEnvio, pstrCodigoProceso, pintAnioPeriodo, pintMesPeriodo, pstrMensaje, penuEstado, pstrUsuario}, Me.RegistrarLogEnvioOperationCompleted, userState)
        End Sub
        
        Private Sub OnRegistrarLogEnvioOperationCompleted(ByVal arg As Object)
            If (Not (Me.RegistrarLogEnvioCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RegistrarLogEnvioCompleted(Me, New RegistrarLogEnvioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/RegistrarLogProcesosAutomaticos", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function RegistrarLogProcesosAutomaticos(ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String) As Integer
            Dim results() As Object = Me.Invoke("RegistrarLogProcesosAutomaticos", New Object() {pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogProcesosAutomaticosAsync(ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String)
            Me.RegistrarLogProcesosAutomaticosAsync(pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub RegistrarLogProcesosAutomaticosAsync(ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String, ByVal userState As Object)
            If (Me.RegistrarLogProcesosAutomaticosOperationCompleted Is Nothing) Then
                Me.RegistrarLogProcesosAutomaticosOperationCompleted = AddressOf Me.OnRegistrarLogProcesosAutomaticosOperationCompleted
            End If
            Me.InvokeAsync("RegistrarLogProcesosAutomaticos", New Object() {pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario}, Me.RegistrarLogProcesosAutomaticosOperationCompleted, userState)
        End Sub
        
        Private Sub OnRegistrarLogProcesosAutomaticosOperationCompleted(ByVal arg As Object)
            If (Not (Me.RegistrarLogProcesosAutomaticosCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent RegistrarLogProcesosAutomaticosCompleted(Me, New RegistrarLogProcesosAutomaticosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ActualizarLogProcesosAutomaticos", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ActualizarLogProcesosAutomaticos(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String) As Integer
            Dim results() As Object = Me.Invoke("ActualizarLogProcesosAutomaticos", New Object() {pintCodigoProcesoAutomatico, pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ActualizarLogProcesosAutomaticosAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String)
            Me.ActualizarLogProcesosAutomaticosAsync(pintCodigoProcesoAutomatico, pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ActualizarLogProcesosAutomaticosAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String, ByVal userState As Object)
            If (Me.ActualizarLogProcesosAutomaticosOperationCompleted Is Nothing) Then
                Me.ActualizarLogProcesosAutomaticosOperationCompleted = AddressOf Me.OnActualizarLogProcesosAutomaticosOperationCompleted
            End If
            Me.InvokeAsync("ActualizarLogProcesosAutomaticos", New Object() {pintCodigoProcesoAutomatico, pintTotal, pintProcesados, pintError, pstrMensaje, pintEstado, pstrUsuario}, Me.ActualizarLogProcesosAutomaticosOperationCompleted, userState)
        End Sub
        
        Private Sub OnActualizarLogProcesosAutomaticosOperationCompleted(ByVal arg As Object)
            If (Not (Me.ActualizarLogProcesosAutomaticosCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ActualizarLogProcesosAutomaticosCompleted(Me, New ActualizarLogProcesosAutomaticosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerListClienteUltimoProceso", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObtenerListClienteUltimoProceso() As System.Data.DataTable
            Dim results() As Object = Me.Invoke("ObtenerListClienteUltimoProceso", New Object(-1) {})
            Return CType(results(0),System.Data.DataTable)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObtenerListClienteUltimoProcesoAsync()
            Me.ObtenerListClienteUltimoProcesoAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObtenerListClienteUltimoProcesoAsync(ByVal userState As Object)
            If (Me.ObtenerListClienteUltimoProcesoOperationCompleted Is Nothing) Then
                Me.ObtenerListClienteUltimoProcesoOperationCompleted = AddressOf Me.OnObtenerListClienteUltimoProcesoOperationCompleted
            End If
            Me.InvokeAsync("ObtenerListClienteUltimoProceso", New Object(-1) {}, Me.ObtenerListClienteUltimoProcesoOperationCompleted, userState)
        End Sub
        
        Private Sub OnObtenerListClienteUltimoProcesoOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObtenerListClienteUltimoProcesoCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerListClienteUltimoProcesoCompleted(Me, New ObtenerListClienteUltimoProcesoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ProcesarAlerta", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ProcesarAlerta(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoCliente As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrUsuario As String, ByRef pintEstado As Integer) As String
            Dim results() As Object = Me.Invoke("ProcesarAlerta", New Object() {pintCodigoProcesoAutomatico, pstrCodigoCliente, pintAnioPeriodo, pintMesPeriodo, pstrUsuario, pintEstado})
            pintEstado = CType(results(1),Integer)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ProcesarAlertaAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoCliente As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrUsuario As String, ByVal pintEstado As Integer)
            Me.ProcesarAlertaAsync(pintCodigoProcesoAutomatico, pstrCodigoCliente, pintAnioPeriodo, pintMesPeriodo, pstrUsuario, pintEstado, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ProcesarAlertaAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoCliente As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrUsuario As String, ByVal pintEstado As Integer, ByVal userState As Object)
            If (Me.ProcesarAlertaOperationCompleted Is Nothing) Then
                Me.ProcesarAlertaOperationCompleted = AddressOf Me.OnProcesarAlertaOperationCompleted
            End If
            Me.InvokeAsync("ProcesarAlerta", New Object() {pintCodigoProcesoAutomatico, pstrCodigoCliente, pintAnioPeriodo, pintMesPeriodo, pstrUsuario, pintEstado}, Me.ProcesarAlertaOperationCompleted, userState)
        End Sub
        
        Private Sub OnProcesarAlertaOperationCompleted(ByVal arg As Object)
            If (Not (Me.ProcesarAlertaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ProcesarAlertaCompleted(Me, New ProcesarAlertaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.9032.0"),  _
     System.SerializableAttribute(),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/")>  _
    Public Enum enumLogEnvioCorreo
        
        '''<remarks/>
        Enviado
        
        '''<remarks/>
        Cancelado
        
        '''<remarks/>
        [Error]
    End Enum
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub RegistrarLogEventoSistemaCompletedEventHandler(ByVal sender As Object, ByVal e As RegistrarLogEventoSistemaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RegistrarLogEventoSistemaCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub RegistrarLogEnvioCompletedEventHandler(ByVal sender As Object, ByVal e As RegistrarLogEnvioCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RegistrarLogEnvioCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub RegistrarLogProcesosAutomaticosCompletedEventHandler(ByVal sender As Object, ByVal e As RegistrarLogProcesosAutomaticosCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class RegistrarLogProcesosAutomaticosCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub ActualizarLogProcesosAutomaticosCompletedEventHandler(ByVal sender As Object, ByVal e As ActualizarLogProcesosAutomaticosCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ActualizarLogProcesosAutomaticosCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub ObtenerListClienteUltimoProcesoCompletedEventHandler(ByVal sender As Object, ByVal e As ObtenerListClienteUltimoProcesoCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObtenerListClienteUltimoProcesoCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As System.Data.DataTable
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),System.Data.DataTable)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub ProcesarAlertaCompletedEventHandler(ByVal sender As Object, ByVal e As ProcesarAlertaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ProcesarAlertaCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
        
        '''<remarks/>
        Public ReadOnly Property pintEstado() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(1),Integer)
            End Get
        End Property
    End Class
End Namespace

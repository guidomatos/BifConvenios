﻿'------------------------------------------------------------------------------
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
Namespace wsEnvioAutomatico
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="wsEnvioAutomaticoSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class wsEnvioAutomatico
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private ObtenerListaProcesosDisponiblesByFechaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ObtenerListaProcesosDisponiblesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ProcesarEnvioNominasByClienteOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ProcesarTodosClientesOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RegistrarLogEventoSistemaOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RegistrarLogEnvioOperationCompleted As System.Threading.SendOrPostCallback
        
        Private RegistrarLogProcesosAutomaticosOperationCompleted As System.Threading.SendOrPostCallback
        
        Private ActualizarLogProcesosAutomaticosOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.ConsoleApplication1.My.MySettings.Default.ConsoleApplication1_WSenvioAutomatico_wsEnvioAutomatico
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
        Public Event ObtenerListaProcesosDisponiblesByFechaCompleted As ObtenerListaProcesosDisponiblesByFechaCompletedEventHandler
        
        '''<remarks/>
        Public Event ObtenerListaProcesosDisponiblesCompleted As ObtenerListaProcesosDisponiblesCompletedEventHandler
        
        '''<remarks/>
        Public Event ProcesarEnvioNominasByClienteCompleted As ProcesarEnvioNominasByClienteCompletedEventHandler
        
        '''<remarks/>
        Public Event ProcesarTodosClientesCompleted As ProcesarTodosClientesCompletedEventHandler
        
        '''<remarks/>
        Public Event RegistrarLogEventoSistemaCompleted As RegistrarLogEventoSistemaCompletedEventHandler
        
        '''<remarks/>
        Public Event RegistrarLogEnvioCompleted As RegistrarLogEnvioCompletedEventHandler
        
        '''<remarks/>
        Public Event RegistrarLogProcesosAutomaticosCompleted As RegistrarLogProcesosAutomaticosCompletedEventHandler
        
        '''<remarks/>
        Public Event ActualizarLogProcesosAutomaticosCompleted As ActualizarLogProcesosAutomaticosCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerListaProcesosDisponiblesByFecha", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObtenerListaProcesosDisponiblesByFecha(ByVal pintDia As Integer) As System.Data.DataTable
            Dim results() As Object = Me.Invoke("ObtenerListaProcesosDisponiblesByFecha", New Object() {pintDia})
            Return CType(results(0),System.Data.DataTable)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObtenerListaProcesosDisponiblesByFechaAsync(ByVal pintDia As Integer)
            Me.ObtenerListaProcesosDisponiblesByFechaAsync(pintDia, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObtenerListaProcesosDisponiblesByFechaAsync(ByVal pintDia As Integer, ByVal userState As Object)
            If (Me.ObtenerListaProcesosDisponiblesByFechaOperationCompleted Is Nothing) Then
                Me.ObtenerListaProcesosDisponiblesByFechaOperationCompleted = AddressOf Me.OnObtenerListaProcesosDisponiblesByFechaOperationCompleted
            End If
            Me.InvokeAsync("ObtenerListaProcesosDisponiblesByFecha", New Object() {pintDia}, Me.ObtenerListaProcesosDisponiblesByFechaOperationCompleted, userState)
        End Sub
        
        Private Sub OnObtenerListaProcesosDisponiblesByFechaOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObtenerListaProcesosDisponiblesByFechaCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerListaProcesosDisponiblesByFechaCompleted(Me, New ObtenerListaProcesosDisponiblesByFechaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ObtenerListaProcesosDisponibles", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ObtenerListaProcesosDisponibles(ByVal pstrFiltro As String) As System.Data.DataTable
            Dim results() As Object = Me.Invoke("ObtenerListaProcesosDisponibles", New Object() {pstrFiltro})
            Return CType(results(0),System.Data.DataTable)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ObtenerListaProcesosDisponiblesAsync(ByVal pstrFiltro As String)
            Me.ObtenerListaProcesosDisponiblesAsync(pstrFiltro, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ObtenerListaProcesosDisponiblesAsync(ByVal pstrFiltro As String, ByVal userState As Object)
            If (Me.ObtenerListaProcesosDisponiblesOperationCompleted Is Nothing) Then
                Me.ObtenerListaProcesosDisponiblesOperationCompleted = AddressOf Me.OnObtenerListaProcesosDisponiblesOperationCompleted
            End If
            Me.InvokeAsync("ObtenerListaProcesosDisponibles", New Object() {pstrFiltro}, Me.ObtenerListaProcesosDisponiblesOperationCompleted, userState)
        End Sub
        
        Private Sub OnObtenerListaProcesosDisponiblesOperationCompleted(ByVal arg As Object)
            If (Not (Me.ObtenerListaProcesosDisponiblesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ObtenerListaProcesosDisponiblesCompleted(Me, New ObtenerListaProcesosDisponiblesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ProcesarEnvioNominasByCliente", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ProcesarEnvioNominasByCliente(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoIBS As String, ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String, ByVal pstrMesPeriodo As String, ByVal pstrAnioPeriodo As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrUsuario As String, ByRef pintEstado As Integer) As String
            Dim results() As Object = Me.Invoke("ProcesarEnvioNominasByCliente", New Object() {pintCodigoProcesoAutomatico, pstrCodigoIBS, pstrTipoDocumento, pstrNumeroDocumento, pstrMesPeriodo, pstrAnioPeriodo, pstrFechaProcesoAS400, pstrUsuario, pintEstado})
            pintEstado = CType(results(1),Integer)
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ProcesarEnvioNominasByClienteAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoIBS As String, ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String, ByVal pstrMesPeriodo As String, ByVal pstrAnioPeriodo As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrUsuario As String, ByVal pintEstado As Integer)
            Me.ProcesarEnvioNominasByClienteAsync(pintCodigoProcesoAutomatico, pstrCodigoIBS, pstrTipoDocumento, pstrNumeroDocumento, pstrMesPeriodo, pstrAnioPeriodo, pstrFechaProcesoAS400, pstrUsuario, pintEstado, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ProcesarEnvioNominasByClienteAsync(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoIBS As String, ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String, ByVal pstrMesPeriodo As String, ByVal pstrAnioPeriodo As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrUsuario As String, ByVal pintEstado As Integer, ByVal userState As Object)
            If (Me.ProcesarEnvioNominasByClienteOperationCompleted Is Nothing) Then
                Me.ProcesarEnvioNominasByClienteOperationCompleted = AddressOf Me.OnProcesarEnvioNominasByClienteOperationCompleted
            End If
            Me.InvokeAsync("ProcesarEnvioNominasByCliente", New Object() {pintCodigoProcesoAutomatico, pstrCodigoIBS, pstrTipoDocumento, pstrNumeroDocumento, pstrMesPeriodo, pstrAnioPeriodo, pstrFechaProcesoAS400, pstrUsuario, pintEstado}, Me.ProcesarEnvioNominasByClienteOperationCompleted, userState)
        End Sub
        
        Private Sub OnProcesarEnvioNominasByClienteOperationCompleted(ByVal arg As Object)
            If (Not (Me.ProcesarEnvioNominasByClienteCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ProcesarEnvioNominasByClienteCompleted(Me, New ProcesarEnvioNominasByClienteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ProcesarTodosClientes", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function ProcesarTodosClientes(ByVal penuTipoEnvio As Integer, ByVal pstrUsuario As String) As String
            Dim results() As Object = Me.Invoke("ProcesarTodosClientes", New Object() {penuTipoEnvio, pstrUsuario})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub ProcesarTodosClientesAsync(ByVal penuTipoEnvio As Integer, ByVal pstrUsuario As String)
            Me.ProcesarTodosClientesAsync(penuTipoEnvio, pstrUsuario, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub ProcesarTodosClientesAsync(ByVal penuTipoEnvio As Integer, ByVal pstrUsuario As String, ByVal userState As Object)
            If (Me.ProcesarTodosClientesOperationCompleted Is Nothing) Then
                Me.ProcesarTodosClientesOperationCompleted = AddressOf Me.OnProcesarTodosClientesOperationCompleted
            End If
            Me.InvokeAsync("ProcesarTodosClientes", New Object() {penuTipoEnvio, pstrUsuario}, Me.ProcesarTodosClientesOperationCompleted, userState)
        End Sub
        
        Private Sub OnProcesarTodosClientesOperationCompleted(ByVal arg As Object)
            If (Not (Me.ProcesarTodosClientesCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent ProcesarTodosClientesCompleted(Me, New ProcesarTodosClientesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
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
    Public Delegate Sub ObtenerListaProcesosDisponiblesByFechaCompletedEventHandler(ByVal sender As Object, ByVal e As ObtenerListaProcesosDisponiblesByFechaCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObtenerListaProcesosDisponiblesByFechaCompletedEventArgs
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
    Public Delegate Sub ObtenerListaProcesosDisponiblesCompletedEventHandler(ByVal sender As Object, ByVal e As ObtenerListaProcesosDisponiblesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ObtenerListaProcesosDisponiblesCompletedEventArgs
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
    Public Delegate Sub ProcesarEnvioNominasByClienteCompletedEventHandler(ByVal sender As Object, ByVal e As ProcesarEnvioNominasByClienteCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ProcesarEnvioNominasByClienteCompletedEventArgs
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
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")>  _
    Public Delegate Sub ProcesarTodosClientesCompletedEventHandler(ByVal sender As Object, ByVal e As ProcesarTodosClientesCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class ProcesarTodosClientesCompletedEventArgs
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
    End Class
    
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
End Namespace

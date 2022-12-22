Option Strict Off
Option Explicit On
Imports System.Configuration.ConfigurationSettings

Public Class DLREC

    Private DLCCFiled As Integer
    Property DLCC() As Integer
        Get
            Return DLCCFiled
        End Get
        Set(ByVal Value As Integer)
            DLCCFiled = Value
        End Set
    End Property


    Private DLANField As Integer
    Property DLAN() As Integer
        Get
            Return DLANField
        End Get
        Set(ByVal Value As Integer)
            DLANField = Value
        End Set
    End Property


    Private DLAGField As Integer
    Property DLAG() As Integer
        Get
            Return DLAGField
        End Get
        Set(ByVal Value As Integer)
            DLAGField = Value
        End Set
    End Property


    Private DLCOField As Integer
    Property DLCO() As Integer
        Get
            Return DLCOField
        End Get
        Set(ByVal Value As Integer)
            DLCOField = Value
        End Set
    End Property

    Private DLMOField As String
    Property DLMO() As String
        Get
            Return DLMOField
        End Get
        Set(ByVal Value As String)
            DLMOField = Value
        End Set
    End Property

    Private DLCMField As String
    Property DLCM() As String
        Get
            Return DLCMField
        End Get
        Set(ByVal Value As String)
            DLCMField = Value
        End Set
    End Property


    Private DLNPField As String
    Property DLNP() As String
        Get
            Return DLNPField
        End Get
        Set(ByVal Value As String)
            DLNPField = Value
        End Set
    End Property


    Private DLNEField As String
    Property DLNE() As String
        Get
            Return DLNEField
        End Get
        Set(ByVal Value As String)
            DLNEField = Value
        End Set
    End Property


    Private DLCRField As String
    Property DLCR() As String
        Get
            Return DLCRField
        End Get
        Set(ByVal Value As String)
            DLCRField = Value
        End Set
    End Property


    Private DLAPField As Integer
    Property DLAP() As Integer
        Get
            Return DLAPField
        End Get
        Set(ByVal Value As Integer)
            DLAPField = Value
        End Set
    End Property

    Private DLMPField As String
    Property DLMP() As String
        Get
            Return DLMPField
        End Get
        Set(ByVal Value As String)
            DLMPField = Value
        End Set
    End Property

    Private DLICField As Integer
    Property DLIC() As Integer
        Get
            Return DLICField
        End Get
        Set(ByVal Value As Integer)
            DLICField = Value
        End Set
    End Property


    Private DLSTField As String
    Property DLST() As String
        Get
            Return DLSTField
        End Get
        Set(ByVal Value As String)
            DLSTField = Value
        End Set
    End Property

    Private DLIDField As Integer
    Property DLID() As Integer
        Get
            Return DLIDField
        End Get
        Set(ByVal Value As Integer)
            DLIDField = Value
        End Set
    End Property


    Private DLFPField As String
    Property DLFP() As String
        Get
            Return DLFPField
        End Get
        Set(ByVal Value As String)
            DLFPField = Value
        End Set
    End Property


    Private DLERField As String
    Property DLER() As String
        Get
            Return DLERField
        End Get
        Set(ByVal Value As String)
            DLERField = Value
        End Set
    End Property


    Private EstadoField As String
    Property Estado() As String
        Get
            Return EstadoField
        End Get
        Set(ByVal Value As String)
            EstadoField = Value
        End Set
    End Property



End Class

Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsProceso
    ' Fields
    Private _CodigoProceso As String
    Private _Estado As String
    Private _AnioPeriodo As String
    Private _MesPeriodo As String
    Private _FechaProcesoAS400 As String
    Private _CodigoCliente As Integer
    Private _FechaCargaAS400 As DateTime
    Private _FechaGeneracionCF As DateTime
    Private _FechaDescargaArchivo As DateTime
    Private _FechaEnvioEmail As DateTime
    Private _TmpStatusGen As String
    Private _FechaProcesoAD As DateTime
    Private _FechaEnvioAS400 As DateTime
    Private _FechaCorteSeguimiento As DateTime
    Private _Usuario As String
    Private _FechaPostConciliacion As DateTime

    ' Properties
    Public Property CodigoProceso() As String
        Get
            Return Me._CodigoProceso
        End Get
        Set(ByVal value As String)
            Me._CodigoProceso = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return Me._Estado
        End Get
        Set(ByVal value As String)
            Me._Estado = value
        End Set
    End Property

    Public Property AnioPeriodo() As String
        Get
            Return Me._AnioPeriodo
        End Get
        Set(ByVal value As String)
            Me._AnioPeriodo = value
        End Set
    End Property

    Public Property MesPeriodo() As String
        Get
            Return Me._MesPeriodo
        End Get
        Set(ByVal value As String)
            Me._MesPeriodo = value
        End Set
    End Property

    Public Property FechaProcesoAS400() As String
        Get
            Return Me._FechaProcesoAS400
        End Get
        Set(ByVal value As String)
            Me._FechaProcesoAS400 = value
        End Set
    End Property

    Public Property CodigoCliente() As Integer
        Get
            Return Me._CodigoCliente
        End Get
        Set(ByVal value As Integer)
            Me._CodigoCliente = value
        End Set
    End Property

    Public Property FechaCargaAS400() As DateTime
        Get
            Return Me._FechaCargaAS400
        End Get
        Set(ByVal value As DateTime)
            Me._FechaCargaAS400 = value
        End Set
    End Property

    Public Property FechaGeneracionCF() As DateTime
        Get
            Return Me._FechaGeneracionCF
        End Get
        Set(ByVal value As DateTime)
            Me._FechaGeneracionCF = value
        End Set
    End Property

    Public Property FechaDescargaArchivo() As DateTime
        Get
            Return Me._FechaDescargaArchivo
        End Get
        Set(ByVal value As DateTime)
            Me._FechaDescargaArchivo = value
        End Set
    End Property

    Public Property FechaEnvioEmail() As DateTime
        Get
            Return Me._FechaEnvioEmail
        End Get
        Set(ByVal value As DateTime)
            Me._FechaEnvioEmail = value
        End Set
    End Property

    Public Property TmpStatusGen() As String
        Get
            Return Me._TmpStatusGen
        End Get
        Set(ByVal value As String)
            Me._TmpStatusGen = value
        End Set
    End Property

    Public Property FechaProcesoAD() As DateTime
        Get
            Return Me._FechaProcesoAD
        End Get
        Set(ByVal value As DateTime)
            Me._FechaProcesoAD = value
        End Set
    End Property

    Public Property FechaEnvioAS400() As DateTime
        Get
            Return Me._FechaEnvioAS400
        End Get
        Set(ByVal value As DateTime)
            Me._FechaEnvioAS400 = value
        End Set
    End Property

    Public Property FechaCorteSeguimiento() As DateTime
        Get
            Return Me._FechaCorteSeguimiento
        End Get
        Set(ByVal value As DateTime)
            Me._FechaCorteSeguimiento = value
        End Set
    End Property

    Public Property Usuario() As String
        Get
            Return Me._Usuario
        End Get
        Set(ByVal value As String)
            Me._Usuario = value
        End Set
    End Property

    Public Property FechaPostConciliacion() As DateTime
        Get
            Return Me._FechaPostConciliacion
        End Get
        Set(ByVal value As DateTime)
            Me._FechaPostConciliacion = value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._CodigoProceso = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Codigo_proceso"), dr.Table.Columns("Codigo_proceso").ToString, ""))
            Me._Estado = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Estado"), dr.Table.Columns("Estado").ToString, ""))
            Me._AnioPeriodo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Anio_periodo"), dr.Table.Columns("Anio_periodo").ToString, ""))
            Me._MesPeriodo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Mes_Periodo"), dr.Table.Columns("Mes_Periodo").ToString, ""))
            Me._FechaProcesoAS400 = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Fecha_ProcesoAS400"), dr.Table.Columns("Fecha_ProcesoAS400").ToString, ""))
            Me._CodigoCliente = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("Codigo_Cliente"), Convert.ToInt32(dr.Table.Columns("Codigo_Cliente").ToString), 0))
            Me._FechaCargaAS400 = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_CargaAS400"), Convert.ToDateTime(dr.Table.Columns("Fecha_CargaAS400").ToString), Convert.ToDateTime("")))
            Me._FechaGeneracionCF = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_GeneracionCF"), Convert.ToDateTime(dr.Table.Columns("Fecha_GeneracionCF").ToString), Convert.ToDateTime("")))
            Me._FechaDescargaArchivo = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_DescargaArchivo"), Convert.ToDateTime(dr.Table.Columns("Fecha_DescargaArchivo").ToString), Convert.ToDateTime("")))
            Me._FechaEnvioEmail = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_EnvioEmail"), Convert.ToDateTime(dr.Table.Columns("Fecha_EnvioEmail").ToString), Convert.ToDateTime("")))
            Me._TmpStatusGen = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("TmpStatusGen"), dr.Table.Columns("TmpStatusGen").ToString, ""))
            Me._FechaProcesoAD = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_ProcesoAD"), Convert.ToDateTime(dr.Table.Columns("Fecha_ProcesoAD").ToString), Convert.ToDateTime("")))
            Me._FechaEnvioAS400 = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_EnvioAS400"), Convert.ToDateTime(dr.Table.Columns("Fecha_EnvioAS400").ToString), Convert.ToDateTime("")))
            Me._FechaCorteSeguimiento = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_CorteSeguimiento"), Convert.ToDateTime("Fecha_CorteSeguimiento").ToString, Convert.ToDateTime("")))
            Me._Usuario = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("UsuarioCreacion"), dr.Table.Columns("UsuarioCreacion").ToString, ""))
            Me._FechaPostConciliacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("Fecha_PostConciliacion"), Convert.ToDateTime(dr.Table.Columns("Fecha_PostConciliacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub
End Class

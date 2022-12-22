Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Data
Public Class clsCliente
    ' Fields
    Private _CodigoCliente As Integer
    Private _NombreCliente As String
    Private _TipoArchivoEnviar As String
    Private _FormatoArchivo As String
    Private _TipoFormatoArchivo As String
    Private _CodigoReferencia As String
    Private _TipoDocumento As String
    Private _NumeroDocumento As String
    Private _CorreoElectronico As String
    Private _FormatoArchivoImportacion As String
    Private _Telefono1 As String
    Private _Telefono2 As String
    Private _Telefono3 As String
    Private _Telefono4 As String
    Private _DiaEnvioPlanilla As String
    Private _DiaCierrePlanilla As String
    Private _MesesAnticipacionEnvioListado As String
    Private _DiaCorte As String
    Private _IdFuncionario As Integer
    Private _CodigoIBS As Integer
    Private _CodigoInstitucion As String
    Private _CodigoInstitucionCAS As String
    Private _IndEnvioAutomaticoListado As String
    Private _BloquearCredito As Integer
    Private _Estado As Integer
    Private _CodigoOficina As Integer
    Private _NombreOficina As String
    Private _CodigoGestor As Integer
    Private _NombreGestor As String
    Private _UsuarioCreacion As String
    Private _FechaCreacion As DateTime
    Private _UsuarioModificacion As String
    Private _FechaModificacion As DateTime



    ' Properties
    Public Property CodigoCliente() As Integer
        Get
            Return Me._CodigoCliente
        End Get
        Set(ByVal value As Integer)
            Me._CodigoCliente = value
        End Set
    End Property

    Public Property NombreCliente() As String
        Get
            Return Me._NombreCliente
        End Get
        Set(ByVal value As String)
            Me._NombreCliente = value
        End Set
    End Property

    Public Property TipoArchivoEnviar() As String
        Get
            Return Me._TipoArchivoEnviar
        End Get
        Set(ByVal value As String)
            Me._TipoArchivoEnviar = value
        End Set
    End Property

    Public Property FormatoArchivo() As String
        Get
            Return Me._FormatoArchivo
        End Get
        Set(ByVal value As String)
            Me._FormatoArchivo = value
        End Set
    End Property

    Public Property TipoFormatoArchivo() As String
        Get
            Return Me._TipoFormatoArchivo
        End Get
        Set(ByVal value As String)
            Me._TipoFormatoArchivo = value
        End Set
    End Property

    Public Property CodigoReferencia() As String
        Get
            Return Me._CodigoReferencia
        End Get
        Set(ByVal value As String)
            Me._CodigoReferencia = value
        End Set
    End Property

    Public Property TipoDocumento() As String
        Get
            Return Me._TipoDocumento
        End Get
        Set(ByVal value As String)
            Me._TipoDocumento = value
        End Set
    End Property

    Public Property NumeroDocumento() As String
        Get
            Return Me._NumeroDocumento
        End Get
        Set(ByVal value As String)
            Me._NumeroDocumento = value
        End Set
    End Property

    Public Property CorreoElectronico() As String
        Get
            Return Me._CorreoElectronico
        End Get
        Set(ByVal value As String)
            Me._CorreoElectronico = value
        End Set
    End Property

    Public Property FormatoArchivoImportacion() As String
        Get
            Return Me._FormatoArchivoImportacion
        End Get
        Set(ByVal value As String)
            Me._FormatoArchivoImportacion = value
        End Set
    End Property

    Public Property Telefono1() As String
        Get
            Return Me._Telefono1
        End Get
        Set(ByVal value As String)
            Me._Telefono1 = value
        End Set
    End Property

    Public Property Telefono2() As String
        Get
            Return Me._Telefono2
        End Get
        Set(ByVal value As String)
            Me._Telefono2 = value
        End Set
    End Property

    Public Property Telefono3() As String
        Get
            Return Me._Telefono3
        End Get
        Set(ByVal value As String)
            Me._Telefono3 = value
        End Set
    End Property

    Public Property Telefono4() As String
        Get
            Return Me._Telefono4
        End Get
        Set(ByVal value As String)
            Me._Telefono4 = value
        End Set
    End Property

    Public Property DiaEnvioPlanilla() As String
        Get
            Return Me._DiaEnvioPlanilla
        End Get
        Set(ByVal value As String)
            Me._DiaEnvioPlanilla = value
        End Set
    End Property

    Public Property DiaCierrePlanilla() As String
        Get
            Return Me._DiaCierrePlanilla
        End Get
        Set(ByVal value As String)
            Me._DiaCierrePlanilla = value
        End Set
    End Property

    Public Property MesesAnticipacionEnvioListado() As String
        Get
            Return Me._MesesAnticipacionEnvioListado
        End Get
        Set(ByVal value As String)
            Me._MesesAnticipacionEnvioListado = value
        End Set
    End Property

    Public Property DiaCorte() As String
        Get
            Return Me._DiaCorte
        End Get
        Set(ByVal value As String)
            Me._DiaCorte = value
        End Set
    End Property

    Public Property IdFuncionario() As Integer
        Get
            Return Me._IdFuncionario
        End Get
        Set(ByVal value As Integer)
            Me._IdFuncionario = value
        End Set
    End Property

    Public Property CodigoIBS() As Integer
        Get
            Return Me._CodigoIBS
        End Get
        Set(ByVal value As Integer)
            Me._CodigoIBS = value
        End Set
    End Property

    Public Property CodigoInstitucion() As String
        Get
            Return Me._CodigoInstitucion
        End Get
        Set(ByVal value As String)
            Me._CodigoInstitucion = value
        End Set
    End Property

    Public Property CodigoInstitucionCAS() As String
        Get
            Return Me._CodigoInstitucionCAS
        End Get
        Set(ByVal value As String)
            Me._CodigoInstitucionCAS = value
        End Set
    End Property

    Public Property IndEnvioAutomaticoListado() As String
        Get
            Return Me._IndEnvioAutomaticoListado
        End Get
        Set(ByVal value As String)
            Me._IndEnvioAutomaticoListado = value
        End Set
    End Property

    Public Property BloquearCredito() As Integer
        Get
            Return Me._BloquearCredito
        End Get
        Set(ByVal value As Integer)
            Me._BloquearCredito = value
        End Set
    End Property

    Public Property Estado() As Integer
        Get
            Return Me._Estado
        End Get
        Set(ByVal value As Integer)
            Me._Estado = value
        End Set
    End Property

    Public Property CodigoOficina() As Integer
        Get
            Return Me._CodigoOficina
        End Get
        Set(ByVal value As Integer)
            Me._CodigoOficina = value
        End Set
    End Property

    Public Property NombreOficina() As String
        Get
            Return Me._NombreOficina
        End Get
        Set(ByVal value As String)
            Me._NombreOficina = value
        End Set
    End Property

    Public Property CodigoGestor() As Integer
        Get
            Return Me._CodigoGestor
        End Get
        Set(ByVal value As Integer)
            Me._CodigoGestor = value
        End Set
    End Property

    Public Property NombreGestor() As String
        Get
            Return Me._NombreGestor
        End Get
        Set(ByVal value As String)
            Me._NombreGestor = value
        End Set
    End Property

    Public Property UsuarioCreacion() As String
        Get
            Return Me._UsuarioCreacion
        End Get
        Set(ByVal value As String)
            Me._UsuarioCreacion = value
        End Set
    End Property

    Public ReadOnly Property FechaCreacion() As DateTime
        Get
            Return Me._FechaCreacion
        End Get
    End Property

    Public Property UsuarioModificacion() As String
        Get
            Return Me._UsuarioModificacion
        End Get
        Set(ByVal value As String)
            Me._UsuarioModificacion = value
        End Set
    End Property

    Public ReadOnly Property FechaModificacion() As DateTime
        Get
            Return Me._FechaModificacion
        End Get
    End Property


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal dr As DataRow)
        If Not Information.IsDBNull(dr) Then
            Me._CodigoCliente = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("Codigo_Cliente"), Convert.ToInt32(dr.Table.Columns("Codigo_Cliente").ToString), 0))
            Me._NombreCliente = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Nombre_Cliente"), dr.Table.Columns("Nombre_Cliente").ToString, ""))
            Me._TipoArchivoEnviar = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("TipoArchivoEnviar"), dr.Table.Columns("TipoArchivoEnviar").ToString, ""))
            Me._FormatoArchivo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("FormatoArchivo"), dr.Table.Columns("FormatoArchivo").ToString, ""))
            Me._TipoFormatoArchivo = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("TipoFormatoArchivo"), dr.Table.Columns("TipoFormatoArchivo").ToString, ""))
            Me._CodigoReferencia = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("Codigo_Referencia"), dr.Table.Columns("Codigo_Referencia").ToString, ""))
            Me._TipoDocumento = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("TipoDocumento"), dr.Table.Columns("TipoDocumento").ToString, ""))
            Me._NumeroDocumento = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("NumeroDocumento"), dr.Table.Columns("NumeroDocumento").ToString, ""))
            Me._CorreoElectronico = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("CorreoElectronico"), dr.Table.Columns("CorreoElectronico").ToString, ""))
            Me._FormatoArchivoImportacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("FormatoArchivoImportacion"), dr.Table.Columns("FormatoArchivoImportacion").ToString, ""))
            Me._Telefono1 = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("telefono_1"), dr.Table.Columns("Telefono_1").ToString, ""))
            Me._Telefono2 = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("telefono_2"), dr.Table.Columns("Telefono2").ToString, ""))
            Me._Telefono3 = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("telefono_3"), dr.Table.Columns("Telefono_3").ToString, ""))
            Me._Telefono4 = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("telefono_4"), dr.Table.Columns("Telefono_4").ToString, ""))
            Me._DiaEnvioPlanilla = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("dia_envio_planilla"), dr.Table.Columns("dia_envio_planilla").ToString, ""))
            Me._DiaCierrePlanilla = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("dia_cierre_planilla"), dr.Table.Columns.Contains("dia_cierre_planilla").ToString, ""))
            Me._MesesAnticipacionEnvioListado = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("meses_anticipacion_envio_listado"), dr.Table.Columns("meses_anticipacion_envio_listado").ToString, ""))
            Me._DiaCorte = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("dia_corte"), dr.Table.Columns("dia_corte").ToString, ""))
            Me._IdFuncionario = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("id_funcionario"), Convert.ToInt32(dr.Table.Columns("id_funcionario").ToString), 0))
            Me._CodigoIBS = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("codigo_IBS"), Convert.ToInt32(dr.Table.Columns("codigo_IBS").ToString), 0))
            Me._CodigoInstitucion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("codigo_institucion"), dr.Table.Columns("codigo_institucion").ToString, ""))
            Me._CodigoInstitucionCAS = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("codigo_institucion_cas"), dr.Table.Columns("codigo_institucion_cas").ToString, ""))
            Me._IndEnvioAutomaticoListado = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("ind_envio_automatico_listado"), dr.Table.Columns("ind_envio_automatico_listado").ToString, ""))
            Me._Estado = Conversions.ToInteger(Interaction.IIf(dr.Table.Columns.Contains("Estado"), Convert.ToInt32(dr.Table.Columns("Estado").ToString), 0))
            Me._UsuarioCreacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("usuario_creacion"), dr.Table.Columns("usuario_creacion").ToString, ""))
            Me._FechaCreacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("fecha_creacion"), Convert.ToDateTime(dr.Table.Columns("fecha_creacion").ToString), Convert.ToDateTime("")))
            Me._UsuarioModificacion = Conversions.ToString(Interaction.IIf(dr.Table.Columns.Contains("usuario_modificacion"), dr.Table.Columns("usuario_modificacion").ToString, ""))
            Me._FechaModificacion = Conversions.ToDate(Interaction.IIf(dr.Table.Columns.Contains("fecha_modificacion"), Convert.ToDateTime(dr.Table.Columns("fecha_modificacion").ToString), Convert.ToDateTime("")))
        End If
    End Sub
End Class


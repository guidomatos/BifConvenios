Namespace BIFConvenios.Container


    Public Class RegistroArchivoNoProcesado

        Dim mIdProceso As String
        Dim mLinea As String
        Dim mNumeroPagare As String
        Dim mNombre As String
        Dim mMontoDescuento As String

        Dim mAnio_periodo As String
        Dim mMes_Periodo As String
        Dim mCodigoModular As String = ""

        Property CodigoModular() As String
            Get
                Return mCodigoModular
            End Get
            Set(ByVal Value As String)
                mCodigoModular = Value
            End Set
        End Property

        Property Anio_periodo() As String
            Get
                Return mAnio_periodo
            End Get
            Set(ByVal Value As String)
                mAnio_periodo = Value
            End Set
        End Property

        Property Mes_Periodo() As String
            Get
                Return mMes_Periodo
            End Get
            Set(ByVal Value As String)
                mMes_Periodo = Value
            End Set
        End Property


        Property IdProceso() As String
            Get
                Return mIdProceso
            End Get
            Set(ByVal Value As String)
                mIdProceso = Value
            End Set
        End Property

        Property Linea() As String
            Get
                Return mLinea
            End Get
            Set(ByVal Value As String)
                mLinea = Value
            End Set
        End Property
        Property NumeroPagare() As String
            Get
                Return mNumeroPagare
            End Get
            Set(ByVal Value As String)
                mNumeroPagare = Value
            End Set
        End Property
        Property Nombre() As String
            Get
                Return mNombre
            End Get
            Set(ByVal Value As String)
                mNombre = Value
            End Set
        End Property
        Property MontoDescuento() As String
            Get
                Return mMontoDescuento
            End Get
            Set(ByVal Value As String)
                mMontoDescuento = Value
            End Set
        End Property

    End Class
End Namespace

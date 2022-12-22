Namespace BIFConvenios

    Class Anios
        Sub New(ByVal value As Integer)
            _value = value
        End Sub
        Private _value As Integer
        Public Property Year() As Integer
            Get
                Return _value
            End Get
            Set(ByVal Value As Integer)
                _value = Value
            End Set
        End Property
    End Class


    Class Meses
        Sub New(ByVal name As String, ByVal order As String)
            _monthName = name
            _monthOrder = order
        End Sub
        Sub New(ByVal name As String, ByVal order As String, ByVal wzero As Boolean)
            _monthName = name
            _monthOrder = order
            _wzero = wzero
        End Sub

        Private _monthName As String
        Private _monthOrder As String
        Private _wzero As Boolean
        Public Property MonthName() As String
            Get
                Return _monthName
            End Get
            Set(ByVal Value As String)
                _monthName = Value
            End Set
        End Property

        Public Property MonthOrder() As String
            Get
                If _wzero Then
                    Return IIf(_monthOrder.Length = 1, "0" + _monthOrder, _monthOrder)
                Else
                    Return IIf(_monthOrder.Length = 1, _monthOrder, _monthOrder)
                End If
            End Get
            Set(ByVal Value As String)
                _monthOrder = Value
            End Set
        End Property
    End Class


    Public Module Periodo

        Public Function Years() As ArrayList
            Dim i As Integer
            Dim hresult As New ArrayList()
            For i = Now.Year To Now.Year + 1
                hresult.Add(New Anios(i))
            Next
            Return hresult
        End Function

        Public Function Months() As ArrayList
            Dim hresult As New ArrayList()
            Dim i As Integer
            For i = 1 To 12
                hresult.Add(New Meses(MonthName((New Date(1979, i, 1)).Month), i))
            Next
            Return hresult
        End Function

        'Obtenemos el mes segun el numero 
        Public Function GetMonthByNumber(ByVal i As Integer) As String
            Try
                Return MonthName(i)
            Catch ex As System.ArgumentException
                Return ""
            Catch ex1 As Exception
                Return ""
            End Try

        End Function

        'Obtenemos la informacion de los meses utilizando un DataReader
        Public Function GetMonthsByReader(ByVal dr As System.Data.SqlClient.SqlDataReader) As ArrayList
            Dim hresult As New ArrayList()
            While dr.Read
                hresult.Add(New Meses(MonthName(CType(dr("Mes_Periodo"), Integer)), CType(dr("Mes_Periodo"), Integer)))
            End While
            Return hresult
        End Function

        'Obtenemos la informacion de los meses utilizando un DataReader
        Public Function GetMonthsByReaderWithOutZero(ByVal dr As System.Data.SqlClient.SqlDataReader) As ArrayList
            Dim hresult As New ArrayList()
            While dr.Read
                hresult.Add(New Meses(MonthName(CType(dr("Mes_Periodo"), Integer)), CType(dr("Mes_Periodo"), Integer), False))
            End While
            Return hresult
        End Function

    End Module

End Namespace

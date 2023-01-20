Imports Microsoft.VisualBasic

Public Class CustomPageChangeArgs
#Region "Variables"
    Dim _currentPageNumber As Integer
    Dim _totalPages As Integer
    Dim _totalRecordCount As Integer
    Dim _currentPageSize As Integer

#End Region

#Region "Propiedades"
    Public Property CurrentPageNumber() As Integer
        Get
            Return _currentPageNumber
        End Get
        Set(ByVal value As Integer)
            _currentPageNumber = value
        End Set
    End Property

    Public Property TotalPages() As Integer
        Get
            Return _totalPages
        End Get
        Set(ByVal value As Integer)
            _totalPages = value
        End Set
    End Property

    Public Property TotalRecordCount() As Integer
        Get
            Return _totalRecordCount
        End Get
        Set(ByVal value As Integer)
            _totalRecordCount = value
        End Set
    End Property

    Public Property CurrentPageSize() As Integer
        Get
            Return _currentPageSize
        End Get
        Set(ByVal value As Integer)
            _currentPageSize = value
        End Set
    End Property
#End Region

End Class

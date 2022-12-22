Imports System

Partial Class controls_Pager
    Inherits System.Web.UI.UserControl

    Public Event PageChanged As CustomDelegateClass.PageChangedEventHandler

#Region "Variables"
    Dim _currentPageNumber As Integer
    Dim _pageIsPostBack As Boolean
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

    Public Property PageIsPostBack() As Boolean
        Get
            Return _pageIsPostBack
        End Get
        Set(ByVal value As Boolean)
            _pageIsPostBack = value
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

#Region "Metodos"
    Public Sub LoadPager()

        ddlPageNumber.Items.Clear()
        lblTotalPages.Text = "0"
        lblRecordCount.Text = "0"

        If TotalPages > 0 Then
            For count As Integer = 1 To Me.TotalPages
                ddlPageNumber.Items.Add(count.ToString())
            Next

            ddlPageNumber.Items(0).Selected = True
            lblTotalPages.Text = String.Format(" {0} ", Me.TotalPages.ToString())
            lblRecordCount.Text = String.Format(" {0} ", Me.TotalRecordCount.ToString())
        End If
    End Sub

    Protected Sub Pager_PageChanged(ByVal sender As Object, ByVal e As CustomPageChangeArgs)
        RaiseEvent PageChanged(Me, e)
    End Sub

    Protected Sub ddlPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageSize.SelectedIndexChanged
        Dim args As New CustomPageChangeArgs()

        args.CurrentPageSize = Convert.ToInt32(Me.ddlPageSize.SelectedItem.Value)
        args.CurrentPageNumber = 1
        args.TotalPages = Convert.ToInt32(Me.lblTotalPages.Text)
        Pager_PageChanged(Me, args)

        ddlPageNumber.Items.Clear()

        For count As Integer = 1 To Me.TotalPages Step ++count
            ddlPageNumber.Items.Add(count.ToString())
        Next

        If ddlPageNumber.Items.Count > 0 Then
            ddlPageNumber.Items(0).Selected = True
        End If

        lblTotalPages.Text = String.Format(" {0} ", args.TotalPages.ToString())
        lblRecordCount.Text = String.Format(" {0} ", args.TotalRecordCount.ToString())
    End Sub

    Protected Sub ddlPageNumber_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageNumber.SelectedIndexChanged
        Dim args As New CustomPageChangeArgs()

        args.CurrentPageSize = Convert.ToInt32(Me.ddlPageSize.SelectedItem.Value)
        args.CurrentPageNumber = Convert.ToInt32(Me.ddlPageNumber.SelectedItem.Value)
        args.TotalPages = Convert.ToInt32(Me.lblTotalPages.Text)
        args.TotalRecordCount = Convert.ToInt32(Me.lblRecordCount.Text)

        Pager_PageChanged(Me, args)

        lblTotalPages.Text = String.Format(" {0} ", args.TotalPages.ToString())
        lblRecordCount.Text = String.Format(" {0} ", args.TotalRecordCount.ToString())
    End Sub
#End Region
End Class

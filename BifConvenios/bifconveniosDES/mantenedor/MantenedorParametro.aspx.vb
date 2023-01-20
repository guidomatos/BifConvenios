
Namespace BIFConvenios

    Partial Class mantenedor_MantenedorParametro
        Inherits System.Web.UI.Page

#Region "Propiedades"

        Public Property DATA_SP() As DataSet
            Get
                Return CType(ViewState("Data_SystemParameters"), DataSet)
            End Get
            Set(ByVal Value As DataSet)
                ViewState("Data_SystemParameters") = Value
            End Set
        End Property

        Public Property LISTADO_ESTADO() As DataTable
            Get
                Return CType(ViewState("Data_Estado"), DataTable)
            End Get
            Set(ByVal Value As DataTable)
                ViewState("Data_Estado") = Value
            End Set
        End Property

        Public Property DATA_SP_Hijo() As DataSet
            Get
                Return CType(ViewState("Data_SystemParameters_H"), DataSet)
            End Get
            Set(ByVal Value As DataSet)
                ViewState("Data_SystemParameters_H") = Value
            End Set
        End Property

        Public Property LISTADO_ESTADO_HIJO() As DataTable
            Get
                Return CType(ViewState("Data_Estado_H"), DataTable)
            End Get
            Set(ByVal Value As DataTable)
                ViewState("Data_Estado_H") = Value
            End Set
        End Property

        Public Property LISTADO_ESTADO_Visible() As DataTable
            Get
                Return CType(ViewState("Data_Visible"), DataTable)
            End Get
            Set(ByVal Value As DataTable)
                ViewState("Data_Visible") = Value
            End Set
        End Property


#End Region

#Region "Load"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

            JScript.RegistrarJScript(Page, "__RESALTAR_GRILLA", JScript.MUESTRAPANEL.__RESALTAR_GRILLA)
            If Not Page.IsPostBack Then
                muestraPadre(False)
                muestraHijo(False)
                Call CargaCombo_SystemParameter(1, 13)
                Call CargaCombo_SystemParameter_Hijo(1, 13)
                Call CargaCombo_SystemParameter_Visible(1, 12)
                Call CargaCombo_SystemParameter_VisibleM(1, 12)
                Call Consultar_SystemParameter()
            End If

        End Sub

#End Region

#Region "Eventos"

        Protected Sub btnNuevoHijo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            divSeccionHijo.Visible = True
        End Sub

        Protected Sub btnCancelarHijo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            divSeccionHijo.Visible = False
        End Sub

        Protected Sub btnGrabarHijo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Validar_Hijo() Then
                If Registrar_Hijo() Then
                    Call Consultar_GrillaHijo()
                    'limpiaPadre()
                    'muestraPadre(False)
                    divSeccionHijo.Visible = False
                    JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioInsert"))
                End If
            End If
        End Sub

        Protected Sub btnNuevoPadre_Click(ByVal sender As Object, ByVal e As System.EventArgs)

            muestraPadre(True)

        End Sub

        Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            lblMensaje.Text = String.Empty
            limpiaPadre()
            muestraPadre(False)
        End Sub

        Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Validar_Padre() Then
                If Registrar_Padre() Then
                    Call Consultar_SystemParameter()
                    limpiaPadre()
                    muestraPadre(False)
                    JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioInsert"))
                End If
            End If

        End Sub

#End Region

#Region "Eventos Grilla"

        Protected Sub gvSytemParameter_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
            gvSytemParameter.EditIndex = e.RowIndex
            actualizar_data_row(e)

        End Sub

        Protected Sub gvSytemParameter_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub gvSytemParameter_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Dim intID As String = String.Empty
            Dim lintIndice As Integer = 0
            Dim lstrUrl As String = String.Empty
            Dim lstrItemsUrl As String() = Nothing
            Dim lintIdBA As Integer = 0

            Select Case e.CommandName
                Case "delete"

                    Dim lstrCodigos() As String = e.CommandArgument.Split(",")
                    Eliminar_Padre(CInt(lstrCodigos(0)), CInt(lstrCodigos(1)))
                    Call Consultar_SystemParameter()
                    JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioDelete"))

                Case "Seleccionar"

                    muestraHijo(True)

                    Dim lgvw As GridViewRow = CType((CType(e.CommandSource, LinkButton).NamingContainer), GridViewRow)
                    Dim lstrCodigos() As String = CType(lgvw.FindControl("lnkNumero"), LinkButton).CommandArgument.Split(",")
                    Dim lstrDescripcion As String = CType(lgvw.FindControl("lnkDescripcion"), LinkButton).Text

                    hdnGrupo.value = lstrCodigos(0)
                    hdnParametro.value = lstrCodigos(1)

                    lblNombrePadre.Text = String.Concat(lstrDescripcion, " - Hijos")
                    divSeccionHijo.Visible = False

                    Call Consultar_GrillaHijo()

            End Select

        End Sub

        Protected Sub gvSytemParameter_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)

            gvSytemParameter.EditIndex = e.NewEditIndex
            gvSytemParameter.DataSource = Me.DATA_SP
            gvSytemParameter.DataBind()
            updMantenimiento.Update()
        End Sub

        Protected Sub gvSytemParameter_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
            gvSytemParameter.EditIndex = -1
            gvSytemParameter.DataSource = Me.DATA_SP
            gvSytemParameter.DataBind()
            updMantenimiento.Update()
        End Sub

        Protected Sub gvSytemParameter_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onmouseover", "javascript:ufd__CambiaColorFondo(this, true);")
                e.Row.Attributes.Add("onmouseout", "javascript:ufd__CambiaColorFondo(this,false);")

            End If

        End Sub

        Protected Sub gvSytemParameter_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
            gvSytemParameter.PageIndex = e.NewPageIndex
            gvSytemParameter.DataSource = Me.DATA_SP
            gvSytemParameter.DataBind()
            updMantenimiento.Update()

        End Sub

        Protected Sub gvSytemParameter_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem IsNot Nothing Then

                    Dim ddlEstado As DropDownList = CType(e.Row.FindControl("ddlEstado"), DropDownList)
                    Dim hdnEstado As HiddenField = CType(e.Row.FindControl("Hidd_Estado"), HiddenField)

                    If ddlEstado IsNot Nothing Then

                        For index As Integer = 0 To (Me.LISTADO_ESTADO.Rows.Count - 1)

                            Dim ldesc As String = Me.LISTADO_ESTADO.Rows(index).Item("vDescripcion")
                            Dim lval As String = Me.LISTADO_ESTADO.Rows(index).Item("vValor")
                            Dim elemento = New ListItem(ldesc, lval)
                            ddlEstado.Items.Add(elemento)

                            ddlEstado.SelectedValue = hdnEstado.Value

                        Next

                    End If
             

                End If

            End If


        End Sub

#Region "Hijo"

        Protected Sub gvHijos_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
            gvHijos.EditIndex = e.RowIndex
            actualizar_data_row_hijo(e)

        End Sub

        Protected Sub gvHijos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub gvHijos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Dim intID As String = String.Empty
            Dim lintIndice As Integer = 0
            Dim lstrUrl As String = String.Empty
            Dim lstrItemsUrl As String() = Nothing
            Dim lintIdBA As Integer = 0

            Select Case e.CommandName
                Case "delete"

                    Dim lstrCodigos() As String = e.CommandArgument.Split(",")
                    Eliminar_Padre(CInt(lstrCodigos(0)), CInt(lstrCodigos(1)))
                    Call Consultar_GrillaHijo()
                    JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioDelete"))

            End Select

        End Sub

        Protected Sub gvHijos_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)

            gvHijos.EditIndex = e.NewEditIndex
            gvHijos.DataSource = Me.DATA_SP_Hijo
            gvHijos.DataBind()

        End Sub

        Protected Sub gvHijos_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
            gvHijos.EditIndex = -1
            gvHijos.DataSource = Me.DATA_SP_Hijo
            gvHijos.DataBind()

        End Sub

        Protected Sub gvHijos_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("onmouseover", "javascript:ufd__CambiaColorFondo(this, true);")
                e.Row.Attributes.Add("onmouseout", "javascript:ufd__CambiaColorFondo(this,false);")
            End If

        End Sub

        Protected Sub gvHijos_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
            gvHijos.PageIndex = e.NewPageIndex
            gvHijos.DataSource = Me.DATA_SP_Hijo
            gvHijos.DataBind()

        End Sub

        Protected Sub gvHijos_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)

            If e.Row.RowType = DataControlRowType.DataRow Then

                If e.Row.DataItem IsNot Nothing Then

                    Dim ddlEstado As DropDownList = CType(e.Row.FindControl("ddlEstado"), DropDownList)
                    Dim hdnEstado As HiddenField = CType(e.Row.FindControl("Hidd_Estado"), HiddenField)

                    Dim ddlVisible As DropDownList = CType(e.Row.FindControl("ddlVisible"), DropDownList)
                    Dim hdnVisible As HiddenField = CType(e.Row.FindControl("Hidd_Visible"), HiddenField)

                    If ddlEstado IsNot Nothing Then

                        For index As Integer = 0 To (Me.LISTADO_ESTADO_HIJO.Rows.Count - 1)

                            Dim ldesc As String = Me.LISTADO_ESTADO_HIJO.Rows(index).Item("vDescripcion")
                            Dim lval As String = Me.LISTADO_ESTADO_HIJO.Rows(index).Item("vValor")
                            Dim elemento = New ListItem(ldesc, lval)
                            ddlEstado.Items.Add(elemento)

                            ddlEstado.SelectedValue = hdnEstado.Value

                        Next

                    End If

                    If ddlVisible IsNot Nothing Then

                        For index As Integer = 0 To (Me.LISTADO_ESTADO_Visible.Rows.Count - 1)

                            Dim ldesc As String = Me.LISTADO_ESTADO_Visible.Rows(index).Item("vDescripcion")
                            Dim lval As String = Me.LISTADO_ESTADO_Visible.Rows(index).Item("vValor")
                            Dim elemento = New ListItem(ldesc, lval)
                            ddlVisible.Items.Add(elemento)

                            ddlVisible.SelectedValue = hdnVisible.Value

                        Next

                    End If


                End If

            End If


        End Sub


#End Region

#End Region

#Region "Metodos"

        Private Sub limpiaPadre()

            txtDescripcionF.Text = String.Empty
            txtValorF.Text = String.Empty
            txtOrdenF.Text = String.Empty
        End Sub

        Private Sub muestraPadre(ByVal estado As Boolean)

            divAgregaPadre.Visible = estado

        End Sub

        Private Sub muestraHijo(ByVal estado As Boolean)

            divHijo.Visible = estado

        End Sub

        Protected Sub actualizar_data_row_hijo(ByVal e As GridViewUpdateEventArgs)

            Dim ds As DataSet

            Dim codigos As LinkButton = CType(gvHijos.Rows(e.RowIndex).FindControl("lnkNumero"), LinkButton)

            Dim descripcion As TextBox = CType(gvHijos.Rows(e.RowIndex).FindControl("txtDescripcion"), TextBox)

            Dim valor As TextBox = CType(gvHijos.Rows(e.RowIndex).FindControl("txtValor"), TextBox)
            Dim orden As TextBox = CType(gvHijos.Rows(e.RowIndex).FindControl("txtOrden"), TextBox)
            Dim estado As DropDownList = CType(gvHijos.Rows(e.RowIndex).FindControl("ddlEstado"), DropDownList)

            Dim finicio As TextBox = CType(gvHijos.Rows(e.RowIndex).FindControl("txtFInicio"), TextBox)
            Dim ffin As TextBox = CType(gvHijos.Rows(e.RowIndex).FindControl("txtFFin"), TextBox)
            Dim visible As DropDownList = CType(gvHijos.Rows(e.RowIndex).FindControl("ddlVisible"), DropDownList)

            Dim descOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_Descripcion"), HiddenField)
            Dim valOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_Valor"), HiddenField)
            Dim ordOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_Orden"), HiddenField)
            Dim estOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_Estado"), HiddenField)

            Dim finiOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_FInicio"), HiddenField)
            Dim ffinOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_FFin"), HiddenField)
            Dim visOriginal As HiddenField = CType(gvHijos.Rows(e.RowIndex).FindControl("Hidd_Visible"), HiddenField)

            Dim lboolEstado As Boolean = False

            lboolEstado = Actualizar_Mantenedor_Hijo(codigos.CommandArgument, _
            descripcion.Text, _
            valor.Text, _
            Convert.ToInt32(orden.Text), _
            Convert.ToInt32(estado.SelectedValue.Trim()), _
            Convert.ToInt32(visible.SelectedValue.Trim()), _
            descOriginal.Value, _
            valOriginal.Value, _
            Convert.ToInt32(ordOriginal.Value), _
            Convert.ToInt32(estOriginal.Value), _
            Convert.ToInt32(visOriginal.Value), _
            finicio.Text, _
            ffin.Text, _
            finiOriginal.Value, _
            ffinOriginal.Value)

            If lboolEstado Then
                gvHijos.EditIndex = -1
                Call Consultar_GrillaHijo()
                JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioUpdate"))
            End If

        End Sub

        Protected Sub actualizar_data_row(ByVal e As GridViewUpdateEventArgs)

            Dim ds As DataSet

            Dim codigos As LinkButton = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("lnkNumero"), LinkButton)

            Dim descripcion As TextBox = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("txtDescripcion"), TextBox)

            Dim valor As TextBox = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("txtValor"), TextBox)
            Dim orden As TextBox = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("txtOrden"), TextBox)
            Dim estado As DropDownList = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("ddlEstado"), DropDownList)

            Dim descOriginal As HiddenField = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("Hidd_Descripcion"), HiddenField)
            Dim valOriginal As HiddenField = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("Hidd_Valor"), HiddenField)
            Dim ordOriginal As HiddenField = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("Hidd_Orden"), HiddenField)
            Dim estOriginal As HiddenField = CType(gvSytemParameter.Rows(e.RowIndex).FindControl("Hidd_Estado"), HiddenField)

            Dim lboolEstado As Boolean = False

            lboolEstado = Actualizar_Mantenedor(codigos.CommandArgument, _
            descripcion.Text, _
            Convert.ToInt32(valor.Text), _
            Convert.ToInt32(orden.Text), _
            Convert.ToInt32(estado.SelectedValue.Trim()), _
            descOriginal.Value, _
            Convert.ToInt32(valOriginal.Value), _
            Convert.ToInt32(ordOriginal.Value), _
            Convert.ToInt32(estOriginal.Value))

            If lboolEstado Then
                gvSytemParameter.EditIndex = -1
                Call Consultar_SystemParameter()
                JScript.MensajeAlert(Me, System.Configuration.ConfigurationManager.AppSettings("mensajeSatisfactorioUpdate"))
            End If

        End Sub

        Private Sub Consultar_GrillaHijo()

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = CType(hdnParametro.Value, Integer)
            lobj_EntidadBE.ParametroId = 0
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = -1
            lobj_EntidadBE.Estado = -1

            Me.DATA_SP_Hijo = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)

            gvHijos.DataSource = Me.DATA_SP_Hijo
            gvHijos.DataBind()

        End Sub

        Private Sub Consultar_SystemParameter()

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = 0
            lobj_EntidadBE.ParametroId = 0
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = -1
            lobj_EntidadBE.Estado = -1

            Me.DATA_SP = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)

            gvSytemParameter.DataSource = Me.DATA_SP
            gvSytemParameter.DataBind()

        End Sub

        Private Sub CargaCombo_SystemParameter(ByVal lintGrupoId As Integer, ByVal lintParametroId As Integer)

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = lintParametroId
            lobj_EntidadBE.ParametroId = lintGrupoId
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = 1
            lobj_EntidadBE.Estado = 1

            Dim ldstLocal = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)
            Dim lintNumFilas = ldstLocal.Tables(0).Rows.Count

            Dim ldttLocal = Crear_DT_Listado()

            For index As Integer = 0 To (lintNumFilas - 1)
                Dim drG As DataRow = ldttLocal.NewRow
                drG.Item("vDescripcion") = ldstLocal.Tables(0).Rows(index).Item(3)
                drG.Item("vValor") = ldstLocal.Tables(0).Rows(index).Item(4)
                ldttLocal.Rows.Add(drG)
            Next

            Me.LISTADO_ESTADO = ldttLocal

        End Sub

        Private Sub CargaCombo_SystemParameter_Hijo(ByVal lintGrupoId As Integer, ByVal lintParametroId As Integer)

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = lintParametroId
            lobj_EntidadBE.ParametroId = lintGrupoId
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = 1
            lobj_EntidadBE.Estado = 1

            Dim ldstLocal = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)
            Dim lintNumFilas = ldstLocal.Tables(0).Rows.Count

            Dim ldttLocal = Crear_DT_Listado()

            For index As Integer = 0 To (lintNumFilas - 1)
                Dim drG As DataRow = ldttLocal.NewRow
                drG.Item("vDescripcion") = ldstLocal.Tables(0).Rows(index).Item(3)
                drG.Item("vValor") = ldstLocal.Tables(0).Rows(index).Item(4)
                ldttLocal.Rows.Add(drG)
            Next

            Me.LISTADO_ESTADO_HIJO = ldttLocal

        End Sub

        Private Sub CargaCombo_SystemParameter_Visible(ByVal lintGrupoId As Integer, ByVal lintParametroId As Integer)

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = lintParametroId
            lobj_EntidadBE.ParametroId = lintGrupoId
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = 1
            lobj_EntidadBE.Estado = 1

            Dim ldstLocal = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)
            Dim lintNumFilas = ldstLocal.Tables(0).Rows.Count

            Dim ldttLocal = Crear_DT_Listado()

            For index As Integer = 0 To (lintNumFilas - 1)
                Dim drG As DataRow = ldttLocal.NewRow
                drG.Item("vDescripcion") = ldstLocal.Tables(0).Rows(index).Item(3)
                drG.Item("vValor") = ldstLocal.Tables(0).Rows(index).Item(4)
                ldttLocal.Rows.Add(drG)
            Next

            Me.LISTADO_ESTADO_VISIBLE = ldttLocal

        End Sub

        Private Sub CargaCombo_SystemParameter_VisibleM(ByVal lintGrupoId As Integer, ByVal lintParametroId As Integer)

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = lintParametroId
            lobj_EntidadBE.ParametroId = lintGrupoId
            lobj_EntidadBE.Descripcion = ""
            lobj_EntidadBE.Visible = 1
            lobj_EntidadBE.Estado = 1

            Dim ldstLocal = lobj_Entidad.GetSystemParameterBL(lobj_EntidadBE)
            Dim lintNumFilas = ldstLocal.Tables(0).Rows.Count

            Dim ldttLocal = Crear_DT_Listado()
            Dim lstrdes As String
            Dim lstrval As String

            ddlVisibleH.Items.Clear()

            For index As Integer = 0 To (lintNumFilas - 1)
                Dim drG As DataRow = ldttLocal.NewRow
                lstrdes = ldstLocal.Tables(0).Rows(index).Item(3)
                lstrval = ldstLocal.Tables(0).Rows(index).Item(4)

                Dim elemento = New ListItem(lstrdes, lstrval)
                ddlVisibleH.Items.Add(elemento)

            Next


        End Sub

#End Region

#Region "Funciones"

        Private Function Eliminar_Padre(ByVal lintGrupoId As Integer, ByVal lintParametroId As Integer) As Boolean

            Dim resultado As Boolean

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = lintGrupoId
            lobj_EntidadBE.ParametroId = lintParametroId
            lobj_EntidadBE.Estado = 0
            lobj_EntidadBE.UsuarioModificacion = "Admin"

            resultado = lobj_Entidad.DeleteSystemParameterBL(lobj_EntidadBE)

            Return resultado

        End Function

        Private Function Registrar_Padre() As Boolean

            Dim result As Boolean = False

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = 0
            lobj_EntidadBE.Descripcion = txtDescripcionF.Text.Trim
            lobj_EntidadBE.Valor = txtValorF.Text.Trim
            lobj_EntidadBE.Orden = txtOrdenF.Text.Trim
            lobj_EntidadBE.Estado = 1
            lobj_EntidadBE.UsuarioCreacion = "Admin"

            result = lobj_Entidad.InsertSystemParameterBL(lobj_EntidadBE)

            Return result

        End Function

        Private Function Registrar_Hijo() As Boolean

            Dim result As Boolean = False

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()

            lobj_EntidadBE.GrupoId = CInt(hdnParametro.Value)
            lobj_EntidadBE.ParametroId = 0
            lobj_EntidadBE.Descripcion = txtDescripcionH.Text.Trim
            lobj_EntidadBE.Valor = txtValorH.Text.Trim
            lobj_EntidadBE.Orden = txtOrdenH.Text.Trim
            lobj_EntidadBE.Visible = ddlVisibleH.SelectedValue

            If txtFInicioH.Text = "" Then
                lobj_EntidadBE.FechaInicio = Nothing
            Else
                lobj_EntidadBE.FechaInicio = CDate(txtFInicioH.Text)
            End If

            If txtFFinH.Text = "" Then
                lobj_EntidadBE.FechaFin = Nothing
            Else
                lobj_EntidadBE.FechaFin = CDate(txtFFinH.Text)
            End If

            lobj_EntidadBE.Estado = 1
            lobj_EntidadBE.UsuarioCreacion = "Admin"

            result = lobj_Entidad.InsertSystemParameterBL(lobj_EntidadBE)

            Return result

        End Function

        Private Function Validar_Padre() As Boolean
            Dim lboolResultado As Boolean
            lblMensaje.Text = ""
            lboolResultado = True

            If txtDescripcionF.Text.Trim = "" Then
                lboolResultado = False
                lblMensaje.Text = "El Campo Descripción no debe estar vacío."
            End If

            If txtValorF.Text.Trim = "" Then
                lboolResultado = False
                lblMensaje.Text += "El Campo Valor no debe estar vacío."
            End If

            If txtOrdenF.Text.Trim = "" Then
                lboolResultado = False
                lblMensaje.Text += "El Campo Orden no debe estar vacío."
            End If

            Return lboolResultado
        End Function

        Private Function Validar_Hijo() As Boolean
            Dim lboolResultado As Boolean
            lblMensajeHijo.Text = ""
            lboolResultado = True

            If txtDescripcionH.Text.Trim = "" Then
                lboolResultado = False
                lblMensajeHijo.Text = "El Campo Descripción no debe estar vacío."
            End If

            If txtValorH.Text.Trim = "" Then
                lboolResultado = False
                lblMensajeHijo.Text += "El Campo Valor no debe estar vacío."
            End If

            If txtOrdenH.Text.Trim = "" Then
                lboolResultado = False
                lblMensajeHijo.Text += "El Campo Orden no debe estar vacío."
            End If

            Return lboolResultado
        End Function

        Private Function Crear_DT_Listado() As DataTable

            ' Creamos el objeto DataTable
            Dim dt As DataTable = New DataTable()

            ' Creamos una nueva columna
            Dim vDescripcion As New DataColumn("vDescripcion", Type.GetType("System.String"))
            Dim vValor As New DataColumn("vValor", Type.GetType("System.String"))

            ' Añadimos la columna al objeto DataTable
            dt.Columns.Add(vDescripcion)
            dt.Columns.Add(vValor)

            Return dt

        End Function

        Private Function Actualizar_Mantenedor(ByVal codigos As String, _
                ByVal descripcion As String, _
                ByVal valor As Integer, _
                ByVal orden As Integer, _
                ByVal estado As Integer, _
                ByVal descOriginal As String, _
                ByVal valOriginal As Integer, _
                ByVal ordOriginal As Integer, _
                ByVal estOriginal As Integer) As Boolean

            Dim result As Boolean = False

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()
            Dim lobj_EntidadBE_Org As New SystemParameterBE()

            Dim lstrCodigos() As String = codigos.Split(",")

            lobj_EntidadBE.GrupoId = lstrCodigos(0)
            lobj_EntidadBE.ParametroId = lstrCodigos(1)
            lobj_EntidadBE.Descripcion = descripcion
            lobj_EntidadBE.Valor = valor
            lobj_EntidadBE.Orden = orden
            lobj_EntidadBE.Estado = estado

            lobj_EntidadBE_Org.Descripcion = descOriginal
            lobj_EntidadBE_Org.Valor = valOriginal
            lobj_EntidadBE_Org.Orden = ordOriginal
            lobj_EntidadBE_Org.Estado = estOriginal

            lobj_EntidadBE.UsuarioModificacion = "Admin"

            result = lobj_Entidad.UpdateSystemParameterBL(lobj_EntidadBE, lobj_EntidadBE_Org)

            Return result

        End Function

        Private Function Actualizar_Mantenedor_Hijo(ByVal codigos As String, _
                ByVal descripcion As String, _
                ByVal valor As String, _
                ByVal orden As Integer, _
                ByVal estado As Integer, _
                ByVal visible As Integer, _
                ByVal descOriginal As String, _
                ByVal valOriginal As String, _
                ByVal ordOriginal As Integer, _
                ByVal estOriginal As Integer, _
                ByVal visOriginal As Integer, _
                Optional ByVal finicio As String = Nothing, _
                Optional ByVal ffin As String = Nothing, _
                Optional ByVal finiOriginal As String = Nothing, _
                Optional ByVal ffinOriginal As String = Nothing) As Boolean

            Dim result As Boolean = False

            Dim lobj_Entidad As New SystemParameterBL()
            Dim lobj_EntidadBE As New SystemParameterBE()
            Dim lobj_EntidadBE_Org As New SystemParameterBE()

            Dim lstrCodigos() As String = codigos.Split(",")

            lobj_EntidadBE.GrupoId = lstrCodigos(0)
            lobj_EntidadBE.ParametroId = lstrCodigos(1)
            lobj_EntidadBE.Descripcion = descripcion
            lobj_EntidadBE.Valor = valor
            lobj_EntidadBE.Orden = orden
            lobj_EntidadBE.Estado = estado

            If finicio <> "" Then
                lobj_EntidadBE.FechaInicio = finicio
            Else
                lobj_EntidadBE.FechaInicio = Nothing
            End If

            If ffin <> "" Then
                lobj_EntidadBE.FechaFin = ffin
            Else
                lobj_EntidadBE.FechaFin = Nothing
            End If

            lobj_EntidadBE.Visible = visible

            lobj_EntidadBE_Org.Descripcion = descOriginal
            lobj_EntidadBE_Org.Valor = valOriginal
            lobj_EntidadBE_Org.Orden = ordOriginal
            lobj_EntidadBE_Org.Estado = estOriginal

            If finiOriginal <> "" Then
                lobj_EntidadBE_Org.FechaInicio = finiOriginal
            Else
                lobj_EntidadBE_Org.FechaInicio = Nothing
            End If

            If ffinOriginal <> "" Then
                lobj_EntidadBE_Org.FechaFin = ffinOriginal
            Else
                lobj_EntidadBE_Org.FechaFin = Nothing
            End If

            lobj_EntidadBE_Org.Visible = visOriginal

            lobj_EntidadBE.UsuarioModificacion = "Admin"

            result = lobj_Entidad.UpdateSystemParameterBL(lobj_EntidadBE, lobj_EntidadBE_Org)

            Return result

        End Function

#End Region

    End Class

End Namespace


Imports Microsoft.VisualBasic

Public Class JScript

    Public Enum MUESTRAPANEL
        __MOSTRAR_PANEL = CType(1, Short)
        __OCULTAR_PANEL = CType(2, Short)
        __SOLO_MAYUSCULAS = CType(3, Short)
        __RESALTAR_GRILLA = CType(4, Short)
        __MOSTRAR_MODALPOPUP = CType(5, Short)
    End Enum

    Public Shared Sub RegistrarJScript(ByVal ifrmPagina As System.Web.UI.Page, ByVal istrNombre As String, ByVal enmTipoFuncion As MUESTRAPANEL)
        Dim s As New StringBuilder()
        s.Append("<script language='javascript' type='text/javascript'>")
        Select Case enmTipoFuncion
            Case MUESTRAPANEL.__RESALTAR_GRILLA
                s.Append(JScript_CambiaColorFondo())
                ' break;
        End Select

        s.Append("</script>")

        If Not ifrmPagina.ClientScript.IsClientScriptBlockRegistered(GetType(String), istrNombre) Then
            ifrmPagina.ClientScript.RegisterClientScriptBlock(GetType(String), istrNombre, s.ToString(), False)
        End If

    End Sub

    Public Shared Function JScript_CambiaColorFondo() As String
        Dim s As New StringBuilder()
        s.Append("var lastColorUsed;")
        s.Append("function ufd__CambiaColorFondo(row, highlight) {")
        s.Append("if (highlight){")
        s.Append("row.style.cursor = 'default';")
        s.Append("lastColorUsed = row.style.backgroundColor;")
        s.Append("row.style.backgroundColor = '#E0F3FF';}")
        s.Append("else ")
        s.Append("row.style.backgroundColor = lastColorUsed;}")
        Return s.ToString()

    End Function

    Public Shared Sub MensajeAlert(ByVal lobjPage As Page, ByVal Mensaje As String)

        ScriptManager.RegisterStartupScript(lobjPage, lobjPage.GetType(), Nothing, "alert('" + Mensaje + "');", True)

    End Sub

End Class

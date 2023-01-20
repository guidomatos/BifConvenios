Imports System.Web
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Web.Services
Imports BIFConvenios.BL
Imports System.Collections.Specialized.StringCollection


Partial Class Principal


    Inherits System.Web.UI.Page
    Protected oproc As New BIFConvenios.BL.clsAccesoBL() ' clsAcceso()
    Protected pstridUsuario As String
    Protected objAccesoConvenioBL As New BIFConvenios.BL.clsAccesoBL() ' clsAcceso()

    Protected usuario As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     
        Dim perfil As String
        ObtenerUsuario()
        perfil = objAccesoConvenioBL.GetBuscarPerfilUsuario(CType(usuario, String))


        If (perfil = "ADMIN") Then


            Session("usuarioIniciado") = usuario
            Response.Redirect("Default.aspx")

        Else


            lblValida.Text = "Usted no tiene acceso a esta página"
            Session.Clear()

        End If
    End Sub
    Sub ObtenerUsuario()
        Dim strDominio As String
        Dim user As String

        strDominio = ConfigurationManager.AppSettings("DOMINIO")
        user = HttpContext.Current.User.Identity.Name
        user = user.Substring((strDominio).Length + 1)

        usuario = user
    End Sub
End Class

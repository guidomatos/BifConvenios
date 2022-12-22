using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BusinessLogic;

public partial class Detalle_cuota_pagare : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            string pagare = Page.Request.QueryString["PAGARE"].Trim();
            string anio = Page.Request.QueryString["ANIO"].Trim();
            string mes = Page.Request.QueryString["MES"].Trim();
            string nombre_cliente = Page.Request.QueryString["nombre"].Trim();

            lbl_nombre.Text = nombre_cliente;  
            Reporte_Deuda_cliente_IBScs rep = new Reporte_Deuda_cliente_IBScs();
           DataTable dt = rep.detalle_pagare(pagare, anio, mes);
           gvw_deuda_total.DataSource = dt;
           gvw_deuda_total.DataBind();  


        
    }
}

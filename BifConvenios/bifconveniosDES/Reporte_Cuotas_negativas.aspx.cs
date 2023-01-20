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

public partial class Reporte_Cuotas_negativas : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Reporte_Deuda_cliente_IBScs rep = new Reporte_Deuda_cliente_IBScs();
        DataTable dt = rep.lista_cuotas_negativas(Convert.ToInt32(ddl_anio.SelectedValue), Convert.ToInt32(ddl_mes.SelectedValue));

        gvw_deuda_cliente.DataSource = dt;
        gvw_deuda_cliente.DataBind();

        DataTable dt_IBS = rep.lista_cuotas_negativas_IBS(Convert.ToInt32(ddl_anio.SelectedValue) - 2000, Convert.ToInt32(ddl_mes.SelectedValue));

        gvw_negativos_ibs.DataSource = dt_IBS;
        gvw_negativos_ibs.DataBind();


    }

    
}

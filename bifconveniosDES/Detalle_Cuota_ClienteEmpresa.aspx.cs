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

public partial class Detalle_Cuota_ClienteEmpresa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.Request.QueryString["EmpresaID"] != null)
        {
            int empresa = Convert.ToInt32(Page.Request.QueryString["EmpresaID"]); 
            string anio =  Page.Request.QueryString["anio"].Trim() ;
            string mes = Page.Request.QueryString["mes"].Trim();

            Reporte_Deuda_cliente_IBScs rep = new Reporte_Deuda_cliente_IBScs();

            #region Resumen deuda total cliente

            try
            {
                DataTable dt_resultado_total = rep.LISTAR_RESULTADO_DEUDA_TOTAL(empresa, mes, anio);

                if (dt_resultado_total != null)
                {
                    lbl_monto_total.Text = dt_resultado_total.Rows[0][1].ToString();
                    lbl_cantidad_registros_total.Text = dt_resultado_total.Rows[0][2].ToString();
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }

            #endregion


            #region Detalle deuda total cliente

            try
            {
                DataTable dt_detalle_total = rep.LISTA_DETALLE_CUOTA_EMPRESA_IBS(empresa, mes, anio);

                if (dt_detalle_total != null)
                {
                    gvw_deuda_total.DataSource = dt_detalle_total;
                    gvw_deuda_total.DataBind();
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }

            #endregion


             #region Resumen deuda mes cliente

            try
            {
                DataTable dt_resultado_mes = rep.LISTAR_RESULTADO_DEUDA_MES(empresa, mes, anio);

                if (dt_resultado_mes != null)
                {
                    lbl_monto_mes.Text = dt_resultado_mes.Rows[0][1].ToString();
                    lbl_nro_registro_mes.Text = dt_resultado_mes.Rows[0][2].ToString();
                }

            }
            catch (Exception ex)
            {

            }

             #endregion


            #region Detalle deuda mes cliente

                //DataTable dt_detalle_mes = rep.LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS(empresa, mes, anio);

                try
                {
                    DataTable dt_detalle_mes = rep.LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS(empresa, mes, anio);

                    if (dt_detalle_mes != null)
                    {
                        gvw_deuda_mes.DataSource = dt_detalle_mes;
                        gvw_deuda_mes.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }

            #endregion


                #region Detalle otros pagares
                
                try
                {
                    DataTable dt_detalle_otros = rep.LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS_otros(empresa, mes, anio);

                    if (dt_detalle_otros != null)
                    {
                     gvw_otros_pagares.DataSource = dt_detalle_otros;
                     gvw_otros_pagares.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    lbl_error.Text = ex.Message;
                }


                #endregion





            }


     
    }

 
    protected void gvw_deuda_total_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes.Add("onMouseOut", "grillaMouseOut(this,'ListadoCeldaMouseOut');");
                e.Row.Attributes.Add("onMouseOver", "grillaMouseOver(this,'ListadoCeldaContenidoSeleccionado');");
                e.Row.Attributes.Add("OnClick", String.Format("AbrirDialogoPeriodoModalResultados('Detalle_cuota_pagare.aspx?Pagare={0}&anio={1}&mes={2}&nombre={3}');", DataBinder.Eval(e.Row.DataItem, "PAGARE").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "ANIO").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "MES").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "nombre").ToString().Trim()));
                break;
        }

    }
    protected void gvw_otros_pagares_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                e.Row.Attributes.Add("onMouseOut", "grillaMouseOut(this,'ListadoCeldaMouseOut');");
                e.Row.Attributes.Add("onMouseOver", "grillaMouseOver(this,'ListadoCeldaContenidoSeleccionado');");
                e.Row.Attributes.Add("OnClick", String.Format("AbrirDialogoPeriodoModalResultados('Detalle_cuota_pagare.aspx?Pagare={0}&anio={1}&mes={2}&&nombre={3}');", DataBinder.Eval(e.Row.DataItem, "PAGARE").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "ANIO").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "MES").ToString().Trim(), DataBinder.Eval(e.Row.DataItem, "nombre").ToString().Trim()));
                break;
        }
    }
}

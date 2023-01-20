using System;
using System.Collections.Generic;
using System.Data;
using DataAccess;

namespace BusinessLogic
{
    public class Reporte_Deuda_cliente_IBScs
    {
        private Deuda_Cliente_IBS oDeuda;

        public Reporte_Deuda_cliente_IBScs()
        {
            this.oDeuda = new Deuda_Cliente_IBS();
        }

        public List<int> Listar_mes_DLCCR()
        {
            return oDeuda.lista_mes_DLCCR();
        }

        public List<int> Listar_anio_DLCCR()
        {
            return oDeuda.lista_anio_DLCCR();
        }

        public DataTable lista_deuda_empresa(string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.lista_deuda_empresa_DLCCR(mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable LISTAR_RESULTADO_DEUDA_TOTAL(int codigo_cliente, string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.LISTAR_RESULTADO_DEUDA_TOTAL(codigo_cliente, anio, mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_IBS(int codigo_cliente, string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.LISTA_DETALLE_CUOTA_EMPRESA_IBS(codigo_cliente, anio, mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable LISTAR_RESULTADO_DEUDA_MES(int codigo_cliente, string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.LISTAR_RESULTADO_DEUDA_MES(codigo_cliente, mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS(int codigo_cliente, string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS(codigo_cliente, mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS_otros(int codigo_cliente, string mes, string anio)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS_otros(codigo_cliente, mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable detalle_pagare(string pagare, string anio, string mes)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.detalle_pagare(pagare, mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable lista_cuotas_negativas(int anio, int mes)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.cLIENTES_CUOTAS_NEGATIVAS(mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable lista_cuotas_negativas_IBS(int anio, int mes)
        {
            DataTable dt;

            try
            {
                dt = oDeuda.cLIENTES_CUOTAS_NEGATIVAS_IBS(mes, anio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}
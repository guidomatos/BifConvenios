using System;

namespace DataAccess
{
    public static class Class_Formato
    {
        public static string formatodecimal(decimal numero)
        {
            decimal parte_entera = Math.Truncate(numero);
            decimal diferencia = numero - parte_entera;
            diferencia = Math.Round(diferencia, 2);
            string result_entero_formateado = parte_entera.ToString();

            for (int i = 0; i < Convert.ToInt32(Math.Floor(Convert.ToDecimal((result_entero_formateado.Length - (1 + i)) / 3))); i++)
                result_entero_formateado = result_entero_formateado.ToString().Substring(0, result_entero_formateado.Length - (4 * i + 3)) + ',' + result_entero_formateado.Substring(result_entero_formateado.Length - (4 * i + 3));

            result_entero_formateado = result_entero_formateado + diferencia.ToString().Substring(1, diferencia.ToString().Length - 1);

            if (result_entero_formateado.Length > 2)
            {
                if (result_entero_formateado.Substring(0, 2) == "-,")
                {
                    result_entero_formateado = "-" + result_entero_formateado.Substring(2);
                }
            }

            return result_entero_formateado;
        }
    }
}
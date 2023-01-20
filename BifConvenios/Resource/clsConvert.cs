using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Resource
{
    public static class clsConvert
    {
        public static string ConvertirBody(string pstrBody)
        {
            StringBuilder strHtmlBody = new StringBuilder();
            StringBuilder strBody = new StringBuilder();

            strBody.Replace("&E",".");

            string[] filas = pstrBody.Split(new char [] {'.'});

            foreach (string fila in filas)
            {
                if (fila == "")
                {
                    strBody.Append("<p style='margin-top:0; margin-bottom:0;'>&nbsp;</p>");                    
                }
                else
                {
                    string strCadena = string.Empty;
                    string strCaux = string.Empty;

                    strCaux = fila.Trim();

                    if (strCaux.Length > 200)
                    {
                        strCaux.Insert(200, "<br />");
                    }

                    for (int x = 0; x <= strCaux.Length - 1; x++)
                    {
                        if (strCaux[x].ToString() == " ")
                        {
                            strCadena = strCadena + "&nbsp;";
                        }
                        else
                        {
                            strCadena = strCadena + strCaux[x];
                        }
                    }

                    strBody.Append("<p style='margin-top;0; margin-bottom:0;'>" + strCadena + "</p>");                    
                }
            }

            strHtmlBody.Append("<html>");
            strHtmlBody.Append("<head>");
            strHtmlBody.Append("<meta name='ProgId' content='FrontPage.Editor.Document'>");
            strHtmlBody.Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>");
            strHtmlBody.Append("<title>Correo Electrónico Autogenerado </title>");
            strHtmlBody.Append("</head>");
            strHtmlBody.Append("<body>");
            strHtmlBody.Append(strBody.ToString());
            strHtmlBody.Append("</body>");
            strHtmlBody.Append("</html>");            

            return strHtmlBody.ToString();
        }

        public static string ReemplazarMetadataCorreoAutomatico(string pstrCadena, DataRow pdr)
        {
            string strResult = "";

            strResult = pstrCadena;
            strResult = strResult.Replace("[Nombre_Empresa]", pdr["vNombreEmpresa"].ToString());
            strResult = strResult.Replace("[Fecha_Pago]", pdr["iFechaPago"].ToString());
            strResult = strResult.Replace("[Cuenta_Abono]", pdr["iCuentaAbono"].ToString());
            strResult = strResult.Replace("[Nombre_Funcionario_Convenios]", pdr["vNombreFuncionario"].ToString());
            strResult = strResult.Replace("[Email_Funcionario_Convenios]", pdr["vEmailFuncionario"].ToString());
            strResult = strResult.Replace("[Anexo_Funcionario_Convenios]", pdr["vAnexoFuncionario"].ToString());
            strResult = strResult.Replace("[Mes]", clsPeriodo.NombreMes(Convert.ToInt32(pdr["iMesCuota"].ToString())));
            strResult = strResult.Replace("[Anio]", pdr["iAñoCuota"].ToString());
            strResult = strResult.Replace("[FECHA]", pdr["iFechaPago"].ToString() + "/" + pdr["iMesCuota"].ToString() + "/" + pdr["iAñoCuota"].ToString());
            strResult = strResult.Replace("&R1", "<strong>");
            strResult = strResult.Replace("&R2", "</strong>");

            return strResult;
        }

        public static Boolean IsNumeric(string pstrValor)
        {
            int intResult;

            return int.TryParse(pstrValor, out intResult);
        }
    }
}

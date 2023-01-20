using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Resource
{   
    public static class clsFiles
    {

        public static List<string> ObtenerListaArchivos(string pstrRoot, ref string pstrMensaje)
        {            
            List<string> listFiles = new List<string>();
            string[] listFilesTmp;

            try
            {
                if ((Directory.Exists(pstrRoot)))
                {
                    listFilesTmp = Directory.GetFiles(pstrRoot, "*.xls");

                    if (listFilesTmp.Length == 0)
                    {
                        pstrMensaje = clsMensajesGeneric.MensajeDirectoryEmpty.Replace("&1", pstrRoot).ToString();
                    }
                    else
                    {
                        foreach (string file in listFilesTmp)
                        {
                            listFiles.Add(file);
                        }
                    }
                }
                else
                {
                    pstrMensaje = clsMensajesGeneric.MensajeDirectoryNotExists.Replace("&1", pstrRoot);
                }
            }
            catch (IOException ex)
            {
                pstrMensaje = ex.ToString();
            }

            return listFiles;
        }


        public static Int32 VerifyPath(string pstrRoot, Int32 pintYear, Int32 pintMonth, ref string pstrPath, string pstrFuncionario)
        {
            Int32 intResult = 0;
            string userAccount = ConfigurationManager.AppSettings["UserArchivoConvenio"].ToString();

            string strMes = ConvertMes(pintMonth);
            
            try
            {
                pstrPath = Path.Combine(pstrRoot, Convert.ToString(pintYear));

                if (!(Directory.Exists(pstrPath)))
                {
                    pstrPath = Path.Combine(pstrPath, Convert.ToString(strMes));
                    pstrPath = Path.Combine(pstrPath, Convert.ToString(pstrFuncionario));

                    Directory.CreateDirectory(pstrPath);
                }
                else
                {                    
                    pstrPath = Path.Combine(pstrPath, Convert.ToString(strMes));

                    if (!(Directory.Exists(pstrPath)))
                    {
                        pstrPath = Path.Combine(pstrPath, Convert.ToString(pstrFuncionario));

                        Directory.CreateDirectory(pstrPath);
                    }
                    else
                    {
                        pstrPath = Path.Combine(pstrPath, Convert.ToString(pstrFuncionario));

                        if (!(Directory.Exists(pstrPath)))
                        {
                            Directory.CreateDirectory(pstrPath);
                        }
                    }
                }

                intResult = 1;
                return intResult;
            }
            catch (IOException ex)
            {
                pstrPath = ex.Message.ToString();
                intResult = 0;
                return intResult;
            }
        }

        public static Int32 VerifyPath(string pstrRoot, string pstrFactory, Int32 pintYear, string pintMonth, string pstrProcessType, ref string pstrPath)
        {
            Int32 intResult = 0;
            string userAccount = ConfigurationManager.AppSettings["UserArchivoConvenio"].ToString();

            //DirectorySecurity dsFolder = new DirectorySecurity();
            //dsFolder.AddAccessRule(new FileSystemAccessRule(userAccount, FileSystemRights.FullControl, AccessControlType.Allow));

            try
            {
                if (!(Directory.Exists(pstrRoot)))
                {
                    pstrPath = Path.Combine(pstrRoot, pstrFactory);
                    pstrPath = Path.Combine(pstrPath, Convert.ToString(pintYear));
                    pstrPath = Path.Combine(pstrPath, Convert.ToString(pintMonth));
                    pstrPath = Path.Combine(pstrPath, pstrProcessType);

                    //Directory.CreateDirectory(pstrPath, dsFolder);
                    Directory.CreateDirectory(pstrPath);
                }
                else
                {
                    pstrPath = Path.Combine(pstrRoot, pstrFactory);

                    if (!(Directory.Exists(pstrPath)))
                    {
                        pstrPath = Path.Combine(pstrPath, Convert.ToString(pintYear));
                        pstrPath = Path.Combine(pstrPath, Convert.ToString(pintMonth));
                        pstrPath = Path.Combine(pstrPath, pstrProcessType);

                        //Directory.CreateDirectory(pstrPath, dsFolder);
                        Directory.CreateDirectory(pstrPath);
                    }
                    else
                    {
                        pstrPath = Path.Combine(pstrPath, Convert.ToString(pintYear));

                        if (!(Directory.Exists(pstrPath)))
                        {
                            pstrPath = Path.Combine(pstrPath, Convert.ToString(pintMonth));
                            pstrPath = Path.Combine(pstrPath, pstrProcessType);

                            //Directory.CreateDirectory(pstrPath, dsFolder);
                            Directory.CreateDirectory(pstrPath);
                        }
                        else
                        {
                            pstrPath = Path.Combine(pstrPath, Convert.ToString(pintMonth));

                            if (!(Directory.Exists(pstrPath)))
                            {
                                pstrPath = Path.Combine(pstrPath, pstrProcessType);

                                //Directory.CreateDirectory(pstrPath, dsFolder);
                                Directory.CreateDirectory(pstrPath);
                            }
                            else
                            {
                                pstrPath = Path.Combine(pstrPath, pstrProcessType);

                                if (!(Directory.Exists(pstrPath)))
                                {
                                    //Directory.CreateDirectory(pstrPath, dsFolder);
                                    Directory.CreateDirectory(pstrPath);
                                }
                            }
                        }
                    }
                }
                
                intResult = 1;
                return intResult;
            }
            catch (IOException ex)
            {
                pstrPath = ex.Message.ToString();
                intResult = 0;
                return intResult;
            }
        }

        public static string ConvertMes(int iMes)
        {
            switch (iMes)
            {
                case 1:
                    return "ENE";
                    break;
                case 2:
                    return "FEB";
                    break;
                case 3:
                    return "MAR";
                    break;
                case 4:
                    return "ABR";
                    break;
                case 5:
                    return "MAY";
                    break;
                case 6:
                    return "JUN";
                    break;
                case 7:
                    return "JUL";
                    break;
                case 8:
                    return "AGO";
                    break;
                case 9:
                    return "SEP";
                    break;
                case 10:
                    return "OCT";
                    break;
                case 11:
                    return "NOV";
                    break;
                case 12:
                    return "DIC";
                    break;
                default:
                    return "";
                    break;
            }

        }

        private static string CleanCSVString(string input)
        {
            string output = "\"" + input.Replace("\"", "\"\"").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", "") + "\"";
            return output;
        }

        public static int ExportToCSV(System.Data.DataTable dt, string pstrType, string pstrRoot, string pstrFactory, Int32 pintYear, Int32 pintMonth, string pstrProcessType, ref string strNameFile, ref string strPathFile, ref string strMessage)
        {
            string strFieldSeparator = "\t";
            string strSeparator = "\\";
            
            strNameFile = pstrFactory + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ")" + "." + pstrType;

            Int32 intResult;
            string strFullName = string.Empty;

            try
            {
                if (dt == null || dt.Columns.Count == 0)
                {
                    strNameFile = "";
                    strPathFile = "";
                    strMessage = "";
                    return -1;
                }

                string strMes = ConvertMes(pintMonth);

                intResult = VerifyPath(pstrRoot, pstrFactory, pintYear, strMes, pstrProcessType, ref strPathFile);

                if (intResult == 1)
                {
                    strFullName = strPathFile + strSeparator + strNameFile;

                    StringBuilder csv = new StringBuilder(10 * dt.Rows.Count * dt.Columns.Count);

                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c > 0)
                            csv.Append(",");

                        DataColumn dc = dt.Columns[c];
                        string columnTitleCleaned = CleanCSVString(dc.ColumnName);
                        csv.Append(columnTitleCleaned);
                    }

                    csv.Append(Environment.NewLine);
                    foreach (DataRow dr in dt.Rows)
                    {
                        StringBuilder csvRow = new StringBuilder();
                        for (int c = 0; c < dt.Columns.Count; c++)
                        {
                            if (c != 0)
                                csvRow.Append(",");

                            object columnValue = dr[c];
                            if (columnValue == null)
                                csvRow.Append("");
                            else
                            {
                                string columnStringValue = columnValue.ToString();
                                string cleanedColumnValue = CleanCSVString(columnStringValue);

                                if (columnValue.GetType() == typeof(string) && !columnStringValue.Contains(","))
                                {
                                    cleanedColumnValue = "=" + cleanedColumnValue; // Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                                }

                                csvRow.Append(cleanedColumnValue);
                            }
                        }

                        csv.AppendLine(csvRow.ToString());
                    }

                    StreamWriter sw = new StreamWriter(strFullName, false, System.Text.Encoding.GetEncoding("iso-8859-1"));

                    sw.Write(csv.ToString());
                    sw.Close();

                    strMessage = "";
                    return 0;
                }
                else
                {
                    strMessage = strPathFile;
                    return -1;                
                }
            }
            catch (Exception ex)
            {
                strNameFile = "";
                strPathFile = "";
                strMessage = ex.Message;
                return -1;
            }
        }

        public static int ExportToExcel(System.Data.DataTable dt,string pstrType, string pstrRoot, string pstrFactory, Int32 pintYear, Int32 pintMonth, string pstrProcessType, ref string strNameFile, ref string strPathFile, ref string strMessage)
        {
            string strFieldSeparator = "\t";
            string strSeparator = "\\";
            string strRowSeparator = "\n";

            strNameFile = pstrFactory + "(" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ")" + "." + pstrType;

            Int32 intResult;
            string strFullName = string.Empty;
            
            try
            {
                if (dt == null || dt.Columns.Count == 0)
                {
                    strNameFile = "";
                    strPathFile = "";
                    strMessage = "";
                    return -1;
                }

                string strMes = ConvertMes(pintMonth);

                intResult = VerifyPath(pstrRoot, pstrFactory, pintYear, strMes, pstrProcessType, ref strPathFile);

                if (intResult == 1)
                {
                    strFullName = strPathFile + strSeparator + strNameFile;

                    StringBuilder outPut = new StringBuilder();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        outPut.Append(dc.ColumnName);
                        outPut.Append(strFieldSeparator);
                    }

                    outPut.Append(strRowSeparator);

                    foreach (DataRow item in dt.Rows)
                    {
                        foreach (object value in item.ItemArray)
                        {
                            outPut.Append(value.ToString().Replace('\n', ' ').Replace('\r', ' ').Replace(',', ' '));
                            outPut.Append(strFieldSeparator);
                        }

                        outPut.Append(strRowSeparator);
                    }

                    StreamWriter sw = new StreamWriter(strFullName, false, System.Text.Encoding.GetEncoding("iso-8859-1"));

                    sw.Write(outPut.ToString());
                    sw.Close();

                    strMessage = "";
                    return 0;
                }
                else
                {
                    strMessage = strPathFile;
                    return -1;
                }
                
            }
            catch (Exception ex)
            {
                strNameFile = "";
                strPathFile = "";
                strMessage = ex.Message;
                return -1;
            }
        }

        public static System.Data.DataTable ConvertToDateTable(string pstrFileName, ref string pstrMensaje)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            DataRow dr;
            int intCountRows;

            try
            {
                using (StreamReader sr = new StreamReader(pstrFileName))
                {
                    string strLine;
                    string[] strColumns = null;

                    int intColumns = 1;

                    strLine = sr.ReadLine();
                    strColumns = strLine.Split('\t');
                    intCountRows = strColumns.Length;

                    if (intCountRows == 16)
                    {
                        foreach (string column in strColumns)
                        {
                            dt.Columns.Add(column);
                        }

                        while ((strLine = sr.ReadLine()) != null)
                        {
                            strColumns = strLine.Split('\t');

                            dr = dt.NewRow();
                            int intRow = 0;

                            foreach (string column in strColumns)
                            {
                                dr[intRow] = column;
                                intRow += 1;
                            }

                            dt.Rows.Add(dr);
                        }

                        dt.AcceptChanges();
                    }
                    else
                    {
                        pstrMensaje = "El archivo no cumple con la cantidad de columnas necesarias, o existe un error en la lectura de las columnas.";
                    }
                }
            }
            catch (Exception ex1)
            {
                throw ex1;
            }

            return dt;
        }
        
    }
}

using System;
using System.Data.SqlClient;
using System.Text;
using  System.IO;
namespace LREM.Tools.Reporting.Excel
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class ReportGenerator
	{
		public ReportGenerator()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// General la informacion desde un SQLDataReader
		/// </summary>
		/// <param name="myReader"></param>
		/// <param name="ruta"></param>
		/// <param name="strFile"></param>
		public static void Generate(SqlDataReader myReader ,String ruta , String strFile )
		{
			System.Resources.ResourceManager m_objRes  = new System.Resources.ResourceManager( "LREM.Tools.Reporting.Excel.xlsRes", typeof(ReportGenerator).Assembly);
			StringBuilder sb =  new StringBuilder() ;//   para contener el archivo XLS
			int j ;
			int k ;
			//, ByVal virtualPath As String)  'As String
			string replVal = String.Empty;

			Utils.RemoveFiles(ruta, new TimeSpan(0, 0, 60, 0, 0));
			//Crear el encabezado y la hoja...
			//String quoter  = "\"";

			//Iniciamos el encabezado del xls
			sb.Append(m_objRes.GetString("BeginXls"));
			sb.Append(m_objRes.GetString("HeaderXls"));
			sb.Append(m_objRes.GetString("BeginBodyXls"));
			sb.Append(m_objRes.GetString("BeginTable"));

			for (j = 0 ;j<= myReader.FieldCount - 1; j++)
			{
				sb.Append(m_objRes.GetString("BeginNormalTD"));
				sb.Append(myReader.GetName(j).ToString());   //headings
				//sb.Append(","); // delimiter
				sb.Append(m_objRes.GetString("EndNormalTD"));
			}

			//sb.Append("\r\n");
			sb.Append(Environment.NewLine );

			while (myReader.Read())
			{
				sb.Append(m_objRes.GetString("BeginNormalTR"));
				for (k = 0 ;k<= myReader.FieldCount - 1; k++)
				{
					sb.Append(m_objRes.GetString("BeginNormalTD"));
					if( myReader.GetValue(k).ToString() == null )
					{
						sb.Append(myReader.GetValue(k).ToString());   

						//sb.Append("\"\"" + myReader.GetValue(k).ToString() + " " + ",");
					}
					else
					{
						//replVal = myReader.GetValue(k).ToString().Replace("\"", quoter);
						//replVal += " ,";
						replVal= myReader.GetValue(k).ToString();
						sb.Append(replVal);
					}
					sb.Append(m_objRes.GetString("EndNormalTD"));
				}
				sb.Append(m_objRes.GetString("EndNormalTR"));
				//sb.Append("\r\n");
				sb.Append(Environment.NewLine );
			}
			sb.Append(m_objRes.GetString("EndTable"));
			sb.Append(m_objRes.GetString("EndBodyXls"));
			sb.Append(m_objRes.GetString("EndXls"));

			myReader.Close();
			myReader = null;

			String strFileContent = sb.ToString();
			FileInfo fi = new FileInfo(ruta + strFile); //'System.Web.HttpContext.Current.Server.MapPath(ruta + strFile))
			FileStream sWriter  = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length);
			sWriter.Flush();
			sWriter.Close();
			sb = null;
			fi = null;
			sWriter = null;
		}


		//Reescribimos la clase para generar un excel esta vez usando un DataSet
		public static void Generate(System.Data.DataSet  myDS ,String ruta , String strFile )
		{
			System.Resources.ResourceManager m_objRes  = new System.Resources.ResourceManager( "LREM.Tools.Reporting.Excel.xlsRes", typeof(ReportGenerator).Assembly);
			StringBuilder sb =  new StringBuilder() ;//   para contener el archivo XLS
			int j ;
			int k ;
			int l;
			//, ByVal virtualPath As String)  'As String
			string replVal = String.Empty;

			Utils.RemoveFiles(ruta, new TimeSpan(0, 0, 60, 0, 0));
			//Crear el encabezado y la hoja...
			//String quoter  = "\"";

			//Iniciamos el encabezado del xls
			sb.Append(m_objRes.GetString("BeginXls"));
			sb.Append(m_objRes.GetString("HeaderXls"));
			sb.Append(m_objRes.GetString("BeginBodyXls"));
			sb.Append(m_objRes.GetString("BeginTable"));



			//Generación de los headers 
/*			for (j = 0 ;j<= myReader.FieldCount - 1; j++)
			{*/
			for (j = 0 ;j<= myDS.Tables[0].Columns.Count-1;j++){
				sb.Append(m_objRes.GetString("BeginNormalTD"));
				sb.Append(myDS.Tables[0].Columns[j].ColumnName.ToString());   //headings
				//sb.Append(","); // delimiter
				sb.Append(m_objRes.GetString("EndNormalTD"));
			}  
			//}

			//sb.Append("\r\n");
			sb.Append(Environment.NewLine );

			/*while (myReader.Read())
			{*/
			for (l=0;l<= myDS.Tables[0].Rows.Count - 1; l++){
				sb.Append(m_objRes.GetString("BeginNormalTR"));
				//for (k = 0 ;k<= myReader.FieldCount - 1; k++)
				for (k = 0 ;k<= myDS.Tables[0].Columns.Count-1;k++)
				{
					sb.Append(m_objRes.GetString("BeginNormalTD"));
					//if( myReader.GetValue(k).ToString() == null )
					if ( myDS.Tables[0].Rows[l][k] == null )  
					{
						//sb.Append(myReader.GetValue(k).ToString());   
						sb.Append(myDS.Tables[0].Rows[l][k].ToString());   

					}
					else
					{
						//replVal= myReader.GetValue(k).ToString();
						replVal= myDS.Tables[0].Rows[l][k].ToString();
						sb.Append(replVal);
					}
					sb.Append(m_objRes.GetString("EndNormalTD"));
				}
				sb.Append(m_objRes.GetString("EndNormalTR"));
				//sb.Append("\r\n");
				sb.Append(Environment.NewLine );
			}
			sb.Append(m_objRes.GetString("EndTable"));
			sb.Append(m_objRes.GetString("EndBodyXls"));
			sb.Append(m_objRes.GetString("EndXls"));

			//myReader.Close();
			//myReader = null;

			String strFileContent = sb.ToString();
			FileInfo fi = new FileInfo(ruta + strFile); //'System.Web.HttpContext.Current.Server.MapPath(ruta + strFile))
			FileStream sWriter  = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length);
			sWriter.Flush();
			sWriter.Close();
			fi = null;
			sWriter = null;
		}



		
		/// <summary>
		/// Reescribimos la clase para generar un excel esta vez usando un DataView
		/// </summary>
		/// <param name="myDV"></param>
		/// <param name="ruta"></param>
		/// <param name="strFile"></param>
		/// <param name="columns">Filtro a usarse en la generacion para obtener datos de solo ciertas columnas</param>
		public static void Generate(System.Data.DataView myDV, String ruta , String strFile, String columns, String headers )
		{
			System.Resources.ResourceManager m_objRes  = new System.Resources.ResourceManager( "LREM.Tools.Reporting.Excel.xlsRes", typeof(ReportGenerator).Assembly);
			StringBuilder sb =  new StringBuilder() ;//   para contener el archivo XLS
			int j ;
			int k ;
			bool filterColumns = (columns.Trim()!="");
			bool replaceHeader = (headers.Trim ()!="") ;
			string replVal = String.Empty;

			Utils.RemoveFiles(ruta, new TimeSpan(0, 0, 60, 0, 0));
			//Crear el encabezado y la hoja...
			//String quoter  = "\"";

			//Iniciamos el encabezado del xls
			sb.Append(m_objRes.GetString("BeginXls"));
			sb.Append(m_objRes.GetString("HeaderXls"));
			sb.Append(m_objRes.GetString("BeginBodyXls"));
			sb.Append(m_objRes.GetString("BeginTable"));


			//Generación de los headers 
			string [] s = columns.Split(',');
			Array.Sort ( s ) ;
			string [] headerArray = headers.Split (',');
			int headerCount = 0;

			for (j = 0 ;j<= myDV.Table.Columns.Count-1;j++)
			{
				//condicion para mostrar o no esta informacion
				bool show = false;
				if ( filterColumns )
				{
					if (Array.BinarySearch( s, myDV.Table.Columns[j].ColumnName )>=0) {
					show = true;
					}				
				}
				else{
					show = true;
				}
				// fin  condicion

				if ( show  ) 
				{
					sb.Append(m_objRes.GetString("BeginNormalTD"));

					if ( replaceHeader ) 
					{
						sb.Append( headerArray[ headerCount ]  );   //headings
						headerCount++;
					}
					else
					{
						sb.Append(myDV.Table.Columns[j].ColumnName.ToString());   //headings
					}
					sb.Append(m_objRes.GetString("EndNormalTD"));
				}
			}  

			sb.Append(Environment.NewLine );

			foreach (System.Data.DataRowView dr in myDV ) 
			{
				sb.Append(m_objRes.GetString("BeginNormalTR"));
				for (k = 0 ;k<= myDV.Table.Columns.Count-1;k++)
				{

					//condicion para mostrar o no esta informacion
					bool show = false;
					if ( filterColumns )
					{
						if (Array.BinarySearch( s, myDV.Table.Columns[k].ColumnName )>=0) 
						{
							show = true;
						}				
					}
					else
					{
						show = true;
					}
					// fin  condicion

					if ( show ) 
					{
						//myDV.Table.Columns[k].ColumnName 
						sb.Append(m_objRes.GetString("BeginNormalTD"));
						if ( dr[k] == null )  
						{
							sb.Append(dr[k].ToString());   
						}
						else
						{
							replVal= dr[k].ToString();
							sb.Append(replVal);
						}
						sb.Append(m_objRes.GetString("EndNormalTD"));
					}
				}
				sb.Append(m_objRes.GetString("EndNormalTR"));
				sb.Append(Environment.NewLine );
			}
			sb.Append(m_objRes.GetString("EndTable"));
			sb.Append(m_objRes.GetString("EndBodyXls"));
			sb.Append(m_objRes.GetString("EndXls"));


			String strFileContent = sb.ToString();
			FileInfo fi = new FileInfo(ruta + strFile); 
			FileStream sWriter  = fi.Open(FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			sWriter.Write(System.Text.Encoding.Default.GetBytes(strFileContent), 0, strFileContent.Length);
			sWriter.Flush();
			sWriter.Close();
			fi = null;
			sWriter = null;
		}
	}
}

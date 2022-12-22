using System;
using System.IO;
namespace LREM.Tools.Reporting.Excel
{
	/// <summary>
	/// Clase que contiene utilitarios
	/// </summary>
	 class Utils
	{
		public Utils()
		{

		}
		/// <summary>
		/// Removemos archivos que son demasiado antiguos
		/// </summary>
		/// <param name="strPath"></param>
		/// <param name="tmsp"></param>
		public static void RemoveFiles(String strPath , TimeSpan tmsp )
		{
			DirectoryInfo di = new DirectoryInfo(strPath);
			FileInfo[] fiArr  = di.GetFiles();
			foreach (FileInfo fri  in fiArr)
			{
				if (fri.Extension.ToString() == ".xls" || fri.Extension.ToString() == ".csv")
				{
					if (fri.CreationTime < DateTime.Now.Subtract(tmsp)) 
					{
						fri.Delete();
					}
				}
			}
		}
	}
}

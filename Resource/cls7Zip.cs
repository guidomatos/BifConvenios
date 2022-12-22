using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using SevenZip;

namespace Resource
{
    public class cls7Zip
    {
        protected string strPathdll7Zip = string.Empty;
        protected string strPathTemp = string.Empty;
        public string strPath = string.Empty;

        public cls7Zip()
        {
            int bits = IntPtr.Size * 8;
            string pathEXE = string.Empty;
            pathEXE = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            pathEXE = pathEXE.Replace(@"file:\", "");
            if (bits == 32)
            {
                strPathdll7Zip = pathEXE + @"\7z.dll";
            }
            else
            {
                strPathdll7Zip = pathEXE + @"\7z64.dll";
            }
            string strPathConfig = ConfigDA.ReadConfig("PathUpload");
            strPathTemp = System.IO.Path.GetTempPath();
            strPath = (strPathConfig == null || strPathConfig == "") ? strPathTemp : strPathConfig;
        }

        public Byte[] CompressFiles(string strFileName, object obj)
        {
            return CompressFiles(strFileName,obj,true);
        }

        public Byte[] CompressFiles(string strFileName, object obj, bool bRemoveTemporary7Zip)
        {
            Byte[] bReturn;
            try
            {
                XmlSerializer writer = new XmlSerializer(obj.GetType());
                string strpathFile = strPathTemp + strFileName + ".xml";
                string strpath7Zip = strpathFile + ".rar";

                System.IO.StreamWriter file = new System.IO.StreamWriter(strpathFile);
                writer.Serialize(file, obj);
                file.Close();

                SevenZipCompressor.SetLibraryPath(strPathdll7Zip);
                SevenZipCompressor cmp = new SevenZipCompressor();

                cmp.ArchiveFormat = OutArchiveFormat.SevenZip;
                cmp.CompressionLevel = CompressionLevel.Normal;
                cmp.CompressionMethod = CompressionMethod.Lzma2;
                cmp.VolumeSize = 0;// Convert.ToInt32(lblValCapacity.Text);

                cmp.CompressFiles(strpath7Zip, strpathFile);
                bReturn = FileHandler.FileToByteArray(strpath7Zip);
                File.Delete(strpathFile);
                if (bRemoveTemporary7Zip) File.Delete(strpath7Zip);
            }
            catch (Exception ex)
            {
                bReturn = null;
            }
            return bReturn;

        }

        public Object ExtractArchive(string strFileName, Byte[] pbaBinaryData, object obj)
        {
            return ExtractArchive(strFileName, pbaBinaryData,obj,true);
        }

        public Object ExtractArchive(string strFileName, Byte[] pbaBinaryData, object obj, bool bRemoveTemporary7Zip)
        {
            object oReturn;
            string strpathFile = strPathTemp + strFileName + ".xml";
            string strpath7Zip = strPath + strFileName + ".rar";

            FileHandler.ByteArrayToFile(strpath7Zip, pbaBinaryData);
            SevenZipExtractor.SetLibraryPath(strPathdll7Zip);
            SevenZipExtractor cmp = new SevenZipExtractor(strpath7Zip);
            cmp.ExtractArchive(strPathTemp);

            XmlSerializer reader = new XmlSerializer(obj.GetType());
            StreamReader file = new StreamReader(strpathFile);
            oReturn = reader.Deserialize(file);
            file.Close();

            File.Delete(strpathFile);
            if (bRemoveTemporary7Zip) File.Delete(strpath7Zip);

            return oReturn;
        }
    }
}

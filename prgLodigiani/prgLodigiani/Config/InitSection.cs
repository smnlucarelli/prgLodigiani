using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace prgLodigiani.Configurazioni
{
    //metodo lettura file xml di config
    public class InitSection
    {
        /// <summary>
        /// Lettura parametri di connessione database SYSDB
        /// </summary>
        /// <returns>Stringa di connessione SQL</returns>
        public string ReadXmlDBldg()
        {
            XmlTextReader xtr = new XmlTextReader("C:\\Users\\gianmarcolilli\\Desktop\\C#\\prgLodigiani\\prgLodigiani\\Data.xml");
            string sConnDb = "";

            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DB_Datasource")
                {
                    sConnDb = "Data source=" + xtr.ReadElementString() + ";";
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DB_Catalog")
                {
                    sConnDb = sConnDb + "Initial Catalog=" + xtr.ReadElementString() + ";";
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DB_User")
                {
                    sConnDb = sConnDb + "User id=" + xtr.ReadElementString() + ";";
                }
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "DB_Psw")
                {
                    sConnDb = sConnDb + "Password=" + xtr.ReadElementString() + ";";
                }
            }
            return sConnDb;
        }
    }
}


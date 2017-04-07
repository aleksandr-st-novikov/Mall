using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Helpers
{
    public static class IO
    {
        public static DataTable GetFileFields(string file)
        {
            try
            {
                // Строка подключения
                string ConnectionString = "";
                if (System.IO.Path.GetExtension(file).ToUpper() == ".XLS")
                {
                    ConnectionString = String.Format(
                        "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=\"Excel 8.0;HDR=Yes\";Data Source={0}", file);
                }
                //пока не работает
                else if (System.IO.Path.GetExtension(file).ToUpper() == ".XLSX")
                {
                    ConnectionString = String.Format(
                        "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties =\"Excel 12.0 Xml;HDR=YES\";Data Source={0}", file);
                }

                // Открываем соединение
                DataSet ds = new DataSet("EXCEL");
                OleDbConnection cn = new OleDbConnection(ConnectionString);
                cn.Open();

                DataTable schemaTable =
                        cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                                new object[] { null, null, null, "TABLE" });

                // Берем название первого листа
                string sheet1 = (string)schemaTable.Rows[0].ItemArray[2];
                // Выбираем все данные с листа
                string select = String.Format("SELECT * FROM [{0}]", sheet1);
                OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
                ad.Fill(ds);
                DataTable tb = ds.Tables[0];
                return tb;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}

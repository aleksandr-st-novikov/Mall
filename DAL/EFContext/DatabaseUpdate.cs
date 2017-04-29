using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace DAL.EFContext
{
    public class DatabaseUpdate : ApplicationEFContext
    {
        public void Update()
        {
            using (MySqlConnection con = (MySqlConnection)context.Database.Connection)
            {
                try
                {
                    con.Open();
                    SetVersion(con, "1.0.0",
                        @"
                            CREATE TABLE mall.setting (
                            Id INT NOT NULL AUTO_INCREMENT,
                            Name VARCHAR(150) NOT NULL,
                            Value VARCHAR(300) NULL,
                            PRIMARY KEY (Id));"
                    );
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private bool CheckSettingsTable(MySqlConnection con)
        {
            MySqlCommand com = new MySqlCommand(@"SELECT count(*)
                FROM information_schema.TABLES
                WHERE(TABLE_SCHEMA = 'mall') AND(TABLE_NAME = 'setting')", con);
            using (MySqlDataReader dr = com.ExecuteReader())
            {
                dr.Read();
                return dr.GetInt32(0) > 0 ? true : false;
            }
        }

        private void SetVersion(MySqlConnection con, string version, string script)
        {
            using (SettingEFContext settingEFContext = new SettingEFContext())
            {
                if (version == "1.0.0")
                {
                    //проверяем существование таблицы настроек и создаем
                    if (!(CheckSettingsTable(con)))
                    {
                        MySqlCommand com = new MySqlCommand(script, con);
                        com.ExecuteNonQuery();
                    }
                }
                else
                {
                    MySqlCommand com = new MySqlCommand(script, con);
                    com.ExecuteNonQuery();
                }
                settingEFContext.SetVersionDB(version);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartData
{
    public class DBProvider
    {
        public DataTableCollection DB { get; private set; }

        public void Initialize()
        {
            string connectionString = GetConnectionString();

            DataSet ds = new DataSet();

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();
                SQLiteDataAdapter adapt = new SQLiteDataAdapter($"SELECT Sunrise, Sunset, NumberDay, MonthId FROM TableTime", con);
                // Заполняем Dataset.
                adapt.Fill(ds);
                con.Close();
            }
            DB = ds.Tables;
        }
        // Метод возвращает строку места расположения БД
        private string GetConnectionString()
        {
            return "Data Source=" + Directory.GetCurrentDirectory() + "\\Tabletimesnew.db";
        }
    }
}

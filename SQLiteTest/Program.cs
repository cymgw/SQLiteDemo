using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace SQLiteTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string sql = "SELECT * FROM cost";
            string sqlcmd = "insert into cost values ('2017-08-08',20,'eat')";
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent;
            string connStr = @"Data Source=" + dir.FullName + @"\data\test.db;Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {

                conn.Open();
                insertTable(sqlcmd, conn);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    da.Dispose();
                    conn.Close();
                    conn.Dispose();
                    DataTable dt = ds.Tables[0];
                    Console.WriteLine("Date         money      comment");
                    foreach (DataRow item in dt.Rows)
                    {
                        string date1 = item[0].ToString();
                        Console.WriteLine(item[0].ToString() + "   " + item[1].ToString() + "         " + item[2].ToString());
                    }
                }
            }
            Console.Read();
        }

        public static void insertTable(string cmd,SQLiteConnection conn)
        {
            //string sqlcmd = "insert into cost values ('2017-08-08',20,'eat')";
            SQLiteCommand scmd = new SQLiteCommand(cmd, conn);
            scmd.ExecuteNonQuery();

        }


    }

    
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonRBOC.DAL
{
    class RbocDAL
    {

        private string connectionString = "server=SLIDER;database=RBOC;UID=sa;password=@cc3ss0n3";
        SqlConnection conn;


        public RbocDAL()
        {
            conn = new SqlConnection(connectionString);
        }


        public DateTime GetLastRbocRunDate()
        {

            DateTime dtRbocLastRun = new DateTime();

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 [Month] FROM tblRBOC_LastRun ORDER BY 1 DESC", conn);
            SqlDataReader reader;

            conn.Open();

            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var value = reader.GetDateTime(0);
                    dtRbocLastRun = Convert.ToDateTime(value).Date;
                }
            }

            conn.Close();

            return dtRbocLastRun;

        }
 
    }
}

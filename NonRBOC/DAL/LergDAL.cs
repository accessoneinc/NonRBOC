using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonRBOC.DAL
{
    class LergDAL
    {

        private string connectionString = "server=SLIDER;database=LERG;UID=sa;password=@cc3ss0n3";
        SqlConnection conn;


        public LergDAL()
        {
          conn = new SqlConnection(connectionString);
        }

        public DateTime GetLastLergRunDate()
        {
           
            DateTime dtLastRun = new DateTime();

            SqlCommand cmd = new SqlCommand("select top 1 DATE from LERGDATE", conn);
            SqlDataReader reader;

            conn.Open();


            reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var value = reader.GetString(0);
                    dtLastRun = Convert.ToDateTime(value).Date;
                }
            }
           
            conn.Close();

            return dtLastRun;


        }

    }
}

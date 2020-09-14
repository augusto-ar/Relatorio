using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicativoRelatorio.Conexao
{
   public class Conexao
    {
        SqlConnection con = new SqlConnection();
        public Conexao()
        {
            con.ConnectionString = @"Data Source=DESKTOP-T87R0PS\SQLEXPRESS;Initial Catalog=cpa;Integrated Security=True";
        }

        public SqlConnection Conectar()
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            return con;
        }

        public void FecharConexao()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }
    }
}

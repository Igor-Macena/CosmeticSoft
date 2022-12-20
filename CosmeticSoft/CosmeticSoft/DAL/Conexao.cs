using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CosmeticSoft.DAL
{
    class Conexao
    {

        SqlConnection con = new SqlConnection(@"Data Source = (local); Initial Catalog = bdcosmeticsoft; Integrated Security = True");

        public SqlConnection Conectar()
        {
            if (con.State == ConnectionState.Closed) con.Open();
            return con;
        }

        public void Desconectar()
        {
            if (con.State == ConnectionState.Open) con.Close();
        }
    }
}

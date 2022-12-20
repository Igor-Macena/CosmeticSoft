using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CosmeticSoft.DAL
{
    class ClienteDAL
    {
        Conexao con = new Conexao();

        public void Cadastrar(BLL.ClienteBLL clienteBLL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO TBCLIENTE (NOME, CPF, FONE, CEL) 
            VALUES (@NOME, @CPF, @FONE, @CEL)";

            cmd.Parameters.AddWithValue("@NOME", clienteBLL.Nome);
            cmd.Parameters.AddWithValue("@CPF", clienteBLL.Cpf);
            cmd.Parameters.AddWithValue("@FONE", clienteBLL.Fone);
            cmd.Parameters.AddWithValue("@CEL", clienteBLL.Cel);
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public DataTable ConsultarTodosCliente()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbcliente.codcli as 'Código', tbcliente.nome AS 'Nome', tbcliente.cpf AS 'CPF'," +
                " tbcliente.fone AS 'Telefone', tbcliente.cel AS 'Celular' FROM tbcliente", con.Conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        public DataTable FiltrarCliente(BLL.ClienteBLL clienteBLL)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbcliente.codcli as 'Código', tbcliente.nome AS 'Nome', tbcliente.cpf AS 'CPF'," +
                " tbcliente.fone AS 'Telefone', tbcliente.cel AS 'Celular' FROM tbcliente WHERE NOME LIKE @NOME", con.Conectar());// comando pra consulta;
            da.SelectCommand.Parameters.AddWithValue("@NOME", "%" + clienteBLL.Nome + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        public void Apagar(BLL.ClienteBLL clienteBLL)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM TBCLIENTE WHERE CODCLI = @CODCLI", con.Conectar());
            cmd.Parameters.AddWithValue("@CODCLI", clienteBLL.Codcli);  
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void Atualizar(BLL.ClienteBLL clienteBLL)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE TBCLIENTE SET
                           NOME = @NOME,
                           CPF = @CPF,
                           FONE = @FONE,
                           CEL = @CEL
                           WHERE CODCLI = @CODCLI");
            cmd.Parameters.AddWithValue("@CODCLI", clienteBLL.Codcli);
            cmd.Parameters.AddWithValue("@NOME", clienteBLL.Nome);
            cmd.Parameters.AddWithValue("@CPF", clienteBLL.Cpf);
            cmd.Parameters.AddWithValue("@FONE", clienteBLL.Fone);
            cmd.Parameters.AddWithValue("@CEL", clienteBLL.Cel);
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public BLL.ClienteBLL RetornaDados(BLL.ClienteBLL clienteBLL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"
            SELECT * FROM TBCLIENTE WHERE CODCLI = @CODCLI";
            cmd.Parameters.AddWithValue("@CODCLI", clienteBLL.Codcli);
            cmd.Connection = con.Conectar();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                clienteBLL.Codcli = Convert.ToInt16(dr["CODCLI"]);
                clienteBLL.Nome = dr["Nome"].ToString();
                clienteBLL.Cpf = dr["Cpf"].ToString();
                clienteBLL.Fone = dr["Fone"].ToString();
                clienteBLL.Cel = dr["Cel"].ToString();
            }
            dr.Close();
            con.Desconectar();
            return clienteBLL;
        }
    }
}

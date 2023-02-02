using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CosmeticSoft.DAL
{
    class RevendedorDAL
    {
        Conexao con = new Conexao();

        private static string dicaDaSenha;

        public static string DicaDaSenha { get => dicaDaSenha; set => dicaDaSenha = value; }

        public void Cadastrar(BLL.RevendedorBLL revendedorBLL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"INSERT INTO tbrevendedor ( nome, senha, dicasenha, pergunta, resposta, dicares) 
            VALUES ( @nome, @senha, @dicasenha, @pergunta, @resposta, @dicares)";

            cmd.Parameters.AddWithValue("@nome", revendedorBLL.Nome);
            cmd.Parameters.AddWithValue("@senha", revendedorBLL.Senha);
            cmd.Parameters.AddWithValue("@dicasenha", revendedorBLL.Dicasenha);
            cmd.Parameters.AddWithValue("@pergunta", revendedorBLL.Pergunta);
            cmd.Parameters.AddWithValue("@resposta", revendedorBLL.Resposta);
            cmd.Parameters.AddWithValue("@dicares", revendedorBLL.Dicares);

            DicaDaSenha = revendedorBLL.Dicasenha.ToString();

            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public BLL.RevendedorBLL Logar(BLL.RevendedorBLL revendedorBLL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT * FROM TBREVENDEDOR WHERE nome = @nome AND senha = @senha";

            cmd.Parameters.AddWithValue("nome", revendedorBLL.Nome);
            cmd.Parameters.AddWithValue("senha", revendedorBLL.Senha);

            cmd.Connection = con.Conectar();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                revendedorBLL.Codrevendedor = Convert.ToInt16(dr["CODREVENDE"]);
                revendedorBLL.Nome = dr["NOME"].ToString();
                revendedorBLL.Senha = dr["SENHA"].ToString();
            }
            else
            {
                revendedorBLL.Codrevendedor = 0;
            }
            dr.Close();
            con.Desconectar();
            return revendedorBLL;
        }
        public void Excluir(BLL.RevendedorBLL revendedorBLL)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM tbrevendedor WHERE codrevende = @codrevende", con.Conectar());
            cmd.Parameters.AddWithValue("@codrevende", revendedorBLL.Codrevendedor);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public DataTable Exibe()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBREVENDEDOR", con.Conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }
        public BLL.RevendedorBLL RetornarDados(BLL.RevendedorBLL revendedorBLL)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM TBREVENDEDOR WHERE codrevende = @codrevende", con.Conectar());
            cmd.Parameters.AddWithValue("@codrevende", revendedorBLL.Codrevendedor);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                revendedorBLL.Codrevendedor = Convert.ToInt16(dr["codrevende"]);
                revendedorBLL.Nome = dr["nome"].ToString();
                revendedorBLL.Senha = dr["senha"].ToString();
                revendedorBLL.Dicasenha = dr["dicasenha"].ToString();
                revendedorBLL.Pergunta = dr["pergunta"].ToString();
                revendedorBLL.Resposta = dr["resposta"].ToString();
                revendedorBLL.Dicares = dr["dicares"].ToString();
            }
            dr.Close();
            con.Desconectar();
            return revendedorBLL;
        }

        public BLL.RevendedorBLL verificaBD()
        {
            BLL.RevendedorBLL revendedor = new BLL.RevendedorBLL();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM TBREVENDEDOR", con.Conectar());
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                revendedor.Codrevendedor = Convert.ToInt16(dr["codrevende"]);
                revendedor.Nome = dr["nome"].ToString();
                revendedor.Senha = dr["senha"].ToString();
                revendedor.Dicasenha = dr["dicasenha"].ToString();
                revendedor.Pergunta = dr["pergunta"].ToString();
                revendedor.Resposta = dr["resposta"].ToString();
                revendedor.Dicares = dr["dicares"].ToString();
            }
            dr.Close();
            con.Desconectar();
            return revendedor;
        }
    }
}

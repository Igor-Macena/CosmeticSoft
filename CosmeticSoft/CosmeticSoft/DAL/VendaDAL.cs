using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CosmeticSoft.DAL
{
    class VendaDAL
    {
        Conexao con = new Conexao();
        public int Cadastrar(BLL.VendaBLL vendaBLL)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO tbvenda (datavenda, codcli, codprod, estatus, total, lucro) 
            VALUES (@datavenda, @codcli, @codprod, @estatus, @total, @lucro);
            SELECT SCOPE_IDENTITY() as CHAVE");

            cmd.Parameters.AddWithValue("@datavenda", vendaBLL.Datavenda);
            cmd.Parameters.AddWithValue("@codcli", vendaBLL.Codcli);
            cmd.Parameters.AddWithValue("@codprod", vendaBLL.Codprod);
            cmd.Parameters.AddWithValue("@estatus", vendaBLL.Estatus);
            cmd.Parameters.AddWithValue("@total", vendaBLL.Total);
            cmd.Parameters.AddWithValue("@lucro", vendaBLL.Lucro);

            cmd.Connection = con.Conectar();
        //  cmd.ExecuteNonQuery();
            int chave_gerada = Convert.ToInt16(cmd.ExecuteScalar());
            con.Desconectar();
            return chave_gerada;
        }
        public void Atualizar(BLL.VendaBLL vendaBLL)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE tbvenda SET datavenda = @datavenda, codcli = @codcli, codprod = @codprod, estatus = @estatus, total = @total,
            lucro = @lucro WHERE codvenda = @codvenda");

            cmd.Parameters.AddWithValue("@codvenda", vendaBLL.Codvenda);
            cmd.Parameters.AddWithValue("@datavenda", vendaBLL.Datavenda);
            cmd.Parameters.AddWithValue("@codcli", vendaBLL.Codcli);
            cmd.Parameters.AddWithValue("@codprod", vendaBLL.Codprod);
            cmd.Parameters.AddWithValue("@estatus", vendaBLL.Estatus);
            cmd.Parameters.AddWithValue("@total", vendaBLL.Total);
            cmd.Parameters.AddWithValue("@lucro", vendaBLL.Lucro);
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }
        public DataTable ConsultarTodos()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbvenda.codvenda AS 'Código', tbcliente.nome AS 'Nome do cliente', tbvenda.datavenda AS" +
                " 'Data', tbvenda.estatus AS 'Status da venda', tbvenda.total AS 'Valor da venda', tbvenda.lucro AS 'Lucro' FROM tbvenda JOIN tbcliente" +
                " ON tbvenda.codcli = tbcliente.codcli", con.Conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }
        public DataTable FiltrarNomeCli(BLL.ClienteBLL clienteBLL)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbvenda.codvenda AS 'Código', tbcliente.nome AS 'Nome do cliente', tbvenda.datavenda AS 'Data'," +
                " tbvenda.estatus AS 'Status da venda', tbvenda.total AS 'Valor da venda', tbvenda.lucro AS 'Lucro' FROM tbvenda JOIN tbcliente ON" +
                " tbvenda.codcli = tbcliente.codcli WHERE tbcliente.nome LIKE @nome", con.Conectar());// comando pra consulta;
            da.SelectCommand.Parameters.AddWithValue("@nome", "%" + clienteBLL.Nome + "%");
            DataTable dt = new DataTable();
            da.Fill(dt); // preenche a tabela
            con.Desconectar();// fecha a conexão
            return dt;// retorna pra tabela preenchida
        }

        public void Excluir(BLL.VendaBLL vendaBLL)
        {
            SqlCommand cmd = new SqlCommand(@"DELETE FROM tbvenda WHERE codvenda = @codvenda", con.Conectar());
            cmd.Parameters.AddWithValue("@codvenda", vendaBLL.Codvenda);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void QtdMenos(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand(@"update tbproduto set qtd = qtd-1 where codprod = @codprod", con.Conectar());
            cmd.Parameters.AddWithValue("@codprod", produtoBLL.Codprod);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void QtdMais(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand(@"update tbproduto set qtd = qtd+1 where codprod = @codprod", con.Conectar());
            cmd.Parameters.AddWithValue("@codprod", produtoBLL.Codprod);
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public BLL.VendaBLL RetornarDados(BLL.VendaBLL vendaBLL)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM tbvenda WHERE codvenda = @codvenda", con.Conectar());
            cmd.Parameters.AddWithValue("@codvenda", vendaBLL.Codvenda);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                vendaBLL.Codvenda = Convert.ToInt16(dr["codvenda"]);
                vendaBLL.Datavenda = Convert.ToDateTime(dr["datavenda"]);
                vendaBLL.Codcli = Convert.ToInt16(dr["codcli"]);
                vendaBLL.Codprod = Convert.ToInt16(dr["codprod"]);
                vendaBLL.Estatus = (dr["estatus"].ToString());
                vendaBLL.Total = Convert.ToDouble(dr["total"]);
                vendaBLL.Lucro = Convert.ToDouble(dr["lucro"]);
            }
            dr.Close();
            con.Desconectar();
            return vendaBLL;
        }
    }
}

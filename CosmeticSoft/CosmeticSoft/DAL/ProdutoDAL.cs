using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace CosmeticSoft.DAL
{
    class ProdutoDAL
    {
        Conexao con = new Conexao();
        public void Cadastrar(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tbproduto (nome, preco, qtd, custo, validade) VALUES " +
                "(@nome, @preco, @qtd, @custo, @validade)");

            cmd.Parameters.AddWithValue("@nome", produtoBLL.Nome);
            cmd.Parameters.AddWithValue("@preco", produtoBLL.Preco);
            cmd.Parameters.AddWithValue("@qtd", produtoBLL.Qtd);
            cmd.Parameters.AddWithValue("@custo", produtoBLL.Custo);
            if (produtoBLL.Validade != null)
                cmd.Parameters.AddWithValue("@validade", produtoBLL.Validade);
            else
                cmd.Parameters.AddWithValue("@validade", DBNull.Value);
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public DataTable ConsultarTodosProduto()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbproduto.codprod as 'Código', tbproduto.nome AS 'Nome do Produto'," +
                " tbproduto.preco AS 'Preço', tbproduto.qtd AS 'Quantidade', tbproduto.custo AS 'Custo', tbproduto.validade AS 'Validade'" +
                " FROM tbproduto", con.Conectar());
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }
        public DataTable FiltrarProd(BLL.ProdutoBLL produtoBLL)// new new new 
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbproduto.codprod as 'Código', tbproduto.nome AS 'Nome do Produto'," +
                " tbproduto.preco AS 'Preço', tbproduto.qtd AS 'Quantidade', tbproduto.custo AS 'Custo', tbproduto.validade AS 'Validade'" +
                " FROM tbproduto WHERE codprod LIKE @codprod", con.Conectar());// comando pra consulta;
            da.SelectCommand.Parameters.AddWithValue("@codprod", "%" + produtoBLL.Codprod + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }
        public DataTable FiltrarProduto(BLL.ProdutoBLL produtoBLL)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT tbproduto.codprod as 'Código', tbproduto.nome AS 'Nome do Produto'," +
                " tbproduto.preco AS 'Preço', tbproduto.qtd AS 'Quantidade', tbproduto.custo AS 'Custo', tbproduto.validade AS 'Validade'" +
                " FROM tbproduto WHERE NOME LIKE @nome", con.Conectar());// comando pra consulta;
            da.SelectCommand.Parameters.AddWithValue("@nome", "%" + produtoBLL.Nome + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Desconectar();
            return dt;
        }

        public void Apagar(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"DELETE FROM tbproduto WHERE codprod = @codprod";
            cmd.Parameters.AddWithValue("@codprod", produtoBLL.Codprod);
            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public void Atualizar(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand(@"UPDATE tbproduto SET
                           nome = @nome,
                           preco = @preco,
                           qtd = @qtd,
                           custo = @custo,
                           validade = @validade
                           WHERE codprod = @codprod");

            cmd.Parameters.AddWithValue("@codprod", produtoBLL.Codprod);
            cmd.Parameters.AddWithValue("@nome", produtoBLL.Nome);
            cmd.Parameters.AddWithValue("@preco", produtoBLL.Preco);
            cmd.Parameters.AddWithValue("@qtd", produtoBLL.Qtd);
            cmd.Parameters.AddWithValue("@custo", produtoBLL.Custo);
            if (produtoBLL.Validade != null) // se a validade não for nula
                cmd.Parameters.AddWithValue("@validade", produtoBLL.Validade);  // cadastra
            else // se não
                cmd.Parameters.AddWithValue("@validade", DBNull.Value); // ela recebe "DBnull"

            cmd.Connection = con.Conectar();
            cmd.ExecuteNonQuery();
            con.Desconectar();
        }

        public BLL.ProdutoBLL RetornarDados(BLL.ProdutoBLL produtoBLL)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM TBPRODUTO WHERE CODPROD = @CODPROD", con.Conectar());
            cmd.Parameters.AddWithValue("@CODPROD", produtoBLL.Codprod);
            cmd.Connection = con.Conectar();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                produtoBLL.Codprod = Convert.ToInt16(dr["CODPROD"]);
                produtoBLL.Nome = dr["NOME"].ToString();
                produtoBLL.Preco = Convert.ToDouble(dr["PRECO"]);
                produtoBLL.Qtd = Convert.ToInt16(dr["QTD"]);
                produtoBLL.Custo = Convert.ToDouble(dr["CUSTO"]);
                if (produtoBLL.Validade != null)
                    produtoBLL.Validade = Convert.ToDateTime(dr["VALIDADE"]);
                else
                    produtoBLL.Validade = null;

            }

            dr.Close();
            con.Desconectar();
            return produtoBLL;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CosmeticSoft.UI
{
    public partial class FrmProduto : Form
    {
        DAL.ProdutoDAL produtoDAL = new DAL.ProdutoDAL();
        BLL.ProdutoBLL produtoBLL = new BLL.ProdutoBLL();

        bool atualizar = false;
        public FrmProduto()
        {
            InitializeComponent();
        }

        public void limpaCampos()
        {
            txtNome.Clear();
            txtPreco.Clear();
            txtQtd.Clear();
            txtCusto.Clear();
            txtValidade.Clear();
            lblProduto.Text = "Cadastra Produto";
            btnCadastra.Text = "Cadastrar";
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }
        private void FrmProduto_Load(object sender, EventArgs e)
        {
            dgvConsulta.DataSource = produtoDAL.ConsultarTodosProduto();
        }
        private void BtnEditar_Click_1(object sender, EventArgs e)
        {
            if (dgvConsulta.RowCount > 0)
            {
                produtoBLL.Codprod = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                produtoBLL = produtoDAL.RetornarDados(produtoBLL);
                txtNome.Text = produtoBLL.Nome;
                txtPreco.Text = Convert.ToDouble(produtoBLL.Preco).ToString();
                txtQtd.Text = Convert.ToInt16(produtoBLL.Qtd).ToString();
                txtCusto.Text = Convert.ToDouble(produtoBLL.Custo).ToString();
                txtValidade.Text = produtoBLL.Validade.ToString();
                atualizar = true;
                btnCadastra.Text = "Atualizar";
                lblProduto.Text = "Atualizar Produto";
            }
        }
        private void BtnApagar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja apagar este produto?", "Apagar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                produtoBLL.Codprod = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value.ToString());
                produtoDAL.Apagar(produtoBLL);
                dgvConsulta.DataSource = produtoDAL.ConsultarTodosProduto();
            }
        }
        private void BtnCadastra_Click_1(object sender, EventArgs e)
        {
            try
            {
                produtoBLL.Nome = txtNome.Text;
                produtoBLL.Preco = Convert.ToDouble(txtPreco.Text);
                produtoBLL.Qtd = Convert.ToInt16(txtQtd.Text);
                produtoBLL.Custo = Convert.ToDouble(txtCusto.Text);
                try
                {
                    produtoBLL.Validade = Convert.ToDateTime(txtValidade.Text);
                }
                catch
                {
                    produtoBLL.Validade = null;
                }

                if (txtNome.Text == "" || txtCusto.Text == "" || txtQtd.Text == "" || txtPreco.Text == "")
                {

                }
                else
                {
                    if (atualizar == false)
                    {
                        lblProduto.Text = "Cadastra Produto";
                        btnCadastra.Text = "Cadastrar";
                        produtoDAL.Cadastrar(produtoBLL);
                        MessageBox.Show(" Cadastro efetuado com sucesso ");
                        limpaCampos();
                    }
                    else
                    {
                        produtoDAL.Atualizar(produtoBLL);
                        MessageBox.Show(" Alteração efetuada com sucesso ");
                        btnCadastra.Text = "Atualizar";
                        limpaCampos();
                        atualizar = false;
                    }
                    dgvConsulta.DataSource = produtoDAL.ConsultarTodosProduto();
                }
            }
            catch
            {
                MessageBox.Show("Verifique os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }

        private void BtnLimpa_Click_1(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtPreco.Clear();
            txtQtd.Clear();
            txtCusto.Clear();
            txtValidade.Clear();
        }

        private void BtnFecha_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtFiltro_TextChanged(object sender, EventArgs e)
        {

            produtoBLL.Nome = txtFiltro.Text;
            dgvConsulta.DataSource = produtoDAL.FiltrarProduto(produtoBLL);

        }

        private void btnFecha(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
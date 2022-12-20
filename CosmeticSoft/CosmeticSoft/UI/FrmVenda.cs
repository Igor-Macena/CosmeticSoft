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
    public partial class FrmVenda : Form
    {

        BLL.VendaBLL vendaBLL = new BLL.VendaBLL();
        DAL.VendaDAL vendaDAL = new DAL.VendaDAL();

        BLL.ClienteBLL clienteBLL = new BLL.ClienteBLL();
        DAL.ClienteDAL clienteDAL = new DAL.ClienteDAL();

        BLL.ProdutoBLL produtoBLL = new BLL.ProdutoBLL();
        DAL.ProdutoDAL produtoDAL = new DAL.ProdutoDAL();

        FrmConsulta consulta = new FrmConsulta();

        double total = 0, total2 = 0, cust = 0;
        bool atualizar = false;
        public FrmVenda()
        {
            InitializeComponent();
            dgvConsulta.DataSource = vendaDAL.ConsultarTodos();
            VerificaProduto();
            cmbStatus.Text = "Não Pago";
        }
        public void VerificaProduto()// verifica a quantidade de colunas no DataGridView
        {
            if (dvgProduto.RowCount >= 1)
                btnAdd.Enabled = false;
           else
                btnAdd.Enabled = true;
        }
        public void CalcularTotal() // calcular o total dos produtos
        {
            cust = 0;
            total2 = 0;
            total = 0;
            for (int i = 0; i < dvgProduto.RowCount; i++)
            {
                total += Convert.ToDouble(dvgProduto[2, i].Value);
                total2 += Convert.ToDouble(dvgProduto[4, i].Value);
            }
            cust = total - total2;
            lblTotal.Text = "Lucro total R$: " + cust.ToString("n");
            txtTotal.Text = total.ToString();
        }
    
        private void BtnBusca_Click(object sender, EventArgs e)
        {
            FrmConsulta consulta = new FrmConsulta();
            consulta.tipo_consulta = "cliente";
            consulta.ShowDialog();
            if (consulta.cod_cli > 0)
            {
                vendaBLL.Codcli = consulta.cod_cli;
                txtNome.Text = consulta.nome_cli;
            }
            else
            {
                txtNome.Clear();
            }
        }

        private void BtnCadastra_Click(object sender, EventArgs e) // cadastrar  e atualizar
        {
            try
            {
                // recebe os dados
            vendaBLL.Datavenda = Convert.ToDateTime(txtData.Text);
            vendaBLL.Codprod = Convert.ToInt16(dvgProduto.SelectedCells[0].Value);
            vendaBLL.Estatus = cmbStatus.Text;
            vendaBLL.Total = total;
            vendaBLL.Lucro = cust;

            if (atualizar == false)
                    {
                        vendaDAL.Cadastrar(vendaBLL);
                        MessageBox.Show("Cadastro efetuado com sucesso");
                        limpar();
                    }
                    else
                    {
                        vendaDAL.Atualizar(vendaBLL);
                        MessageBox.Show(" Alteração efetuada com sucesso ");
                        btnCadastra.Text = "Cadastrar";
                        lblVenda.Text = "Cadastrar venda";
                    limpar();
                    }
                    dgvConsulta.DataSource = clienteDAL.ConsultarTodosCliente();
        }
          catch
            {
                MessageBox.Show("Verifique os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); // trata o preenchimento
                // incorreto  de campos
            }
            dgvConsulta.DataSource = vendaDAL.ConsultarTodos();
        }

        private void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            clienteBLL.Nome = txtFiltro.Text;
            dgvConsulta.DataSource = vendaDAL.FiltrarNomeCli(clienteBLL);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmConsulta consulta = new FrmConsulta();
            consulta.tipo_consulta = "produto";
            consulta.ShowDialog();

            if (consulta.cod_primaria > 0)
            {
                produtoDAL.RetornarDados(produtoBLL);
                dvgProduto.Rows.Add(consulta.cod_primaria, consulta.descricao, consulta.valor_prod, consulta.qtd_prod,
                    consulta.custo_prod, consulta.vali_prod); // adiciona campos no DataGridView
                  produtoBLL.Codprod = consulta.cod_primaria; // recebe código da chave estrangeira
                vendaDAL.QtdMenos(produtoBLL);// remove a quantidade do produto no estoque conforme ele é adicionado a venda
            }
            CalcularTotal();
            VerificaProduto();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dvgProduto.RowCount > 0)
            {
                produtoDAL.RetornarDados(produtoBLL);  // retorna os dados que serão afetados pelo código abaixo
                dvgProduto.Rows.Remove(dvgProduto.CurrentRow); // remove o produto selecionado
                vendaDAL.QtdMais(produtoBLL); // aumenta a quantidade do produto no estoque conforme ele é removido da venda
            }
            CalcularTotal();
            VerificaProduto();
        }

        private void BtnLimpa_Click(object sender, EventArgs e)
        {
            limpar();
        }
        public void limpar()
        {
            txtData.Clear();
            txtNome.Clear();
            txtTotal.Clear();
            dvgProduto.Rows.Clear(); // limpa a DataGridView toda
            VerificaProduto();
            lblTotal.Text = "";
            atualizar = false;
            btnCadastra.Text = "Cadastrar";
            lblVenda.Text = "Cadastrar venda";
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close(); // fechar tela
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente excluir essa venda?", "Excluir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                vendaBLL.Codvenda = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                vendaDAL.Excluir(vendaBLL);
                dgvConsulta.DataSource = vendaDAL.ConsultarTodos();
            }
        }

        public void Editar() // método editar
        {
            txtData.Clear();
            txtNome.Clear();
            txtTotal.Clear();
            dvgProduto.Rows.Clear();
            VerificaProduto();
            lblTotal.Text = "";
            if (dgvConsulta.RowCount >= 0)
            {
                vendaBLL.Codvenda = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                vendaBLL = vendaDAL.RetornarDados(vendaBLL);
                txtData.Text = vendaBLL.Datavenda.ToString();
                txtNome.Text = Convert.ToString(dgvConsulta.SelectedCells[1].Value);
                produtoBLL.Codprod = vendaBLL.Codprod; // recebe o código
                if (produtoBLL.Codprod == vendaBLL.Codprod) // compara o código 
                {
                    produtoBLL = produtoDAL.RetornarDados(produtoBLL); // procura colunas relacionadas ao código
                    dvgProduto.Rows.Add(produtoBLL.Codprod, produtoBLL.Nome, produtoBLL.Preco, produtoBLL.Qtd,
                    produtoBLL.Custo, produtoBLL.Validade); // coloca as colunas no DataGridView
                }
                cmbStatus.Text = vendaBLL.Estatus;
                txtTotal.Text = vendaBLL.Total.ToString();
                cust = vendaBLL.Lucro;
                CalcularTotal();
                VerificaProduto();
                atualizar = true;
                btnCadastra.Text = "Atualizar";
                lblVenda.Text = "Atualizar venda";
            }
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            Editar(); 
        }
    }
}



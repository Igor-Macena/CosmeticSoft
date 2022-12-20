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
    public partial class FrmConsulta : Form
    {
        public string tipo_consulta { get; set; }
        public int tipo_primaria { get; set; }
        public string descricao { get; set; }
        public double valor_prod { get; set; }
        public int cod_primaria { get; set; }
        public int qtd_prod { get; set; }
        public DateTime? vali_prod { get; set; }
        public int cod_cli { get; set; }
        public string nome_cli { get; set; }
        public double custo_prod { get; set; }

        BLL.ClienteBLL clienteBLL = new BLL.ClienteBLL();
        DAL.ClienteDAL clienteDAL = new DAL.ClienteDAL();

        BLL.ProdutoBLL produtoBLL = new BLL.ProdutoBLL();
        DAL.ProdutoDAL produtoDAL = new DAL.ProdutoDAL();

        public FrmConsulta()
        {
            InitializeComponent();
        }

        private void BtnSeleciona_Click(object sender, EventArgs e)
        {
            selecionar();
        }

        private void FrmConsulta_Load(object sender, EventArgs e)
        {
            if (tipo_consulta == "cliente")
            {
                dgvConsulta.DataSource = clienteDAL.ConsultarTodosCliente();
            }
            else
            {
                dgvConsulta.DataSource = produtoDAL.ConsultarTodosProduto();
            }
        }

        private void TxtFiltro_TextChanged(object sender, EventArgs e)
        {
            if (tipo_consulta == "cliente")
            {
                clienteBLL.Nome = txtFiltro.Text;
                dgvConsulta.DataSource = clienteDAL.FiltrarCliente(clienteBLL);
            }
            else
            {
                produtoBLL.Nome = txtFiltro.Text;
                dgvConsulta.DataSource = produtoDAL.FiltrarProduto(produtoBLL);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void selecionar()
        {
       
            if (dgvConsulta.RowCount > 0)
            {
                if (tipo_consulta == "produto")
                {
                    cod_primaria = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                    descricao = dgvConsulta.SelectedCells[1].Value.ToString();
                    valor_prod = Convert.ToDouble(dgvConsulta.SelectedCells[2].Value);
                    qtd_prod = Convert.ToInt16(dgvConsulta.SelectedCells[3].Value);
                    custo_prod = Convert.ToDouble(dgvConsulta.SelectedCells[4].Value);
                    try
                    {
                        vali_prod = Convert.ToDateTime(dgvConsulta.SelectedCells[5].Value);
                        Close();
                    }
                    catch 
                    {
                        vali_prod = null;
                        Close();
                    }
                }
                else
                {
                    cod_cli = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                    nome_cli = dgvConsulta.SelectedCells[1].Value.ToString();
                    Close();
                }
                
            }
        }
        private void DgvConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selecionar();
        }
    }
}

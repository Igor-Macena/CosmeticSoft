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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void gerenciaClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }
        private void gerenciaProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void tsbRevendedor_Click(object sender, EventArgs e)
        {

        }

        private void BtnFecha_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void BtnCli_Click(object sender, EventArgs e)
        {
            FrmCliente cli = new FrmCliente();
            cli.ShowDialog();
        }

        private void BtnProd_Click(object sender, EventArgs e)
        {
            FrmProduto prod = new FrmProduto();
            prod.ShowDialog();
        }

        private void BtnVenda_Click(object sender, EventArgs e)
        {
            FrmVenda venda = new FrmVenda();
            venda.ShowDialog();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void PnlVisual_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

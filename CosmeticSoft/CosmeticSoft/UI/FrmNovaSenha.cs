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
    public partial class FrmNovaSenha : Form
    {
        BLL.RevendedorBLL revendedorBLL = new BLL.RevendedorBLL();
        DAL.RevendedorDAL revendedorDAL = new DAL.RevendedorDAL();

        public FrmNovaSenha()
        {
            InitializeComponent();

            revendedorBLL = revendedorDAL.verificaBD();
            txtPergunta.Text = revendedorBLL.Pergunta;
        }

        private void BtnMinimiza_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnVerificar_Click(object sender, EventArgs e)
        {
            revendedorBLL = revendedorDAL.verificaBD();

            if (txtResposta.Text == revendedorBLL.Resposta)
            {
                MessageBox.Show("Aqui está sua senha: " + revendedorBLL.Senha, "Recuperação da senha", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } // tá funcionando (°~°')
            else
            {
                MessageBox.Show("Resposta Incorreta", "Tentativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblDica.Text = revendedorBLL.Dicares;
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtResposta.Clear();
        }
    }
}

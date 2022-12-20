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
    public partial class FrmLogin : Form
    {
        DAL.RevendedorDAL revendedorDAL = new DAL.RevendedorDAL();
        BLL.RevendedorBLL revendedorBLL = new BLL.RevendedorBLL();

        FrmRevendedor rev = new FrmRevendedor();
        FrmPrincipal frm = new FrmPrincipal();
        int antRasquer = 3, cont = 2;

        public static string nomerevendedor;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            revendedorBLL.Nome = txtNome.Text;
            revendedorBLL.Senha = txtSenha.Text;

            revendedorBLL = revendedorDAL.Logar(revendedorBLL);

            if (revendedorBLL.Codrevendedor > 0)
            {
                nomerevendedor = revendedorBLL.Nome;

                this.Hide();
                frm.ShowDialog();

                Close();
            }
            else
            {
                if (cont >= 2)
                {
                    MessageBox.Show("O nome de usuário ou senha informado\nnão consta em nosso cadastro.\n\n" +
                    "           Você tem mais " + cont + " tentativas", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cont == 1)
                {
                    MessageBox.Show("O nome de usuário ou senha informado\nnão consta em nosso cadastro.\n\n" +
                    "           Você tem mais " + cont + " tentativa", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("O nome de usuário ou senha informado\nnão consta em nosso cadastro.\n\n" +
                    "           Você não tem mais tentativas", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                txtSenha.Clear();
                antRasquer--;
                cont--;
                lblTentativas.Text = antRasquer.ToString();
            }
            if (antRasquer == 2)
            {
                revendedorBLL = revendedorDAL.verificaBD();
                if (revendedorBLL.Codrevendedor != 0)
                    lblDicaSenha.Text = "Dica: " + revendedorBLL.Dicasenha; // lblDicaSenha recebe a dica de senha cadastrada
                
                else
                    lblDicaSenha.Text = "Você precisa criar uma conta para realizar o cadastro"; // lblDicaSenha exibe esse mensagem
                    // caso não tenha nada cadastrado
            }
            if (antRasquer == 0)
            {
                Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            FrmNovaSenha newS = new FrmNovaSenha();
            newS.ShowDialog();
        }

        private void LblConta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            revendedorBLL = revendedorDAL.verificaBD();
            if (revendedorBLL.Codrevendedor != 0)
            {
                MessageBox.Show("Você não pode criar mais de 1 conta", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                rev.ShowDialog();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnMinimiza_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}


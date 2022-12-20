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
    public partial class FrmRevendedor : Form
    {
        BLL.RevendedorBLL revendedorBLL = new BLL.RevendedorBLL();
        DAL.RevendedorDAL revendedorDAL = new DAL.RevendedorDAL();

        public FrmRevendedor()
        {
            InitializeComponent();
        }

        private void FrmRevendedor_Load(object sender, EventArgs e)
        {

        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {

        }

        private void BtnMinimiza_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LblEmail_Click(object sender, EventArgs e)
        {

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnCadastrar_Click_1(object sender, EventArgs e)
        {
            revendedorBLL.Nome = txtNome.Text;
            revendedorBLL.Senha = txtSenha.Text;
            revendedorBLL.Dicasenha = txtDicaSenha.Text;
            revendedorBLL.Pergunta = txtPergunta.Text;
            revendedorBLL.Resposta = txtResposta.Text;
            revendedorBLL.Dicares = txtDicaRes.Text;
            string conS = txtConSenha.Text;
            if (txtNome.Text == "" || txtSenha.Text == "" || txtPergunta.Text == "" || txtResposta.Text == "" || conS == "")
            {
                MessageBox.Show("Campos vazios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (conS != revendedorBLL.Senha)
            {
                MessageBox.Show("Verifique o campo Confirmar senha");
                txtSenha.Clear();
                txtConSenha.Clear();
            }
            else if (txtDicaRes.Text == "" || txtDicaSenha.Text == "")
            {
                if (MessageBox.Show("Deseja realmente se cadastrar sem inserir dica para sua resposta?", "Cadastrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    revendedorDAL.Cadastrar(revendedorBLL);
                    MessageBox.Show("Cadastro efetuado com sucesso");
                    this.Close();
                }
            }
            else
            {
                revendedorDAL.Cadastrar(revendedorBLL);
                MessageBox.Show("Cadastro efetuado com sucesso");
                this.Close();
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Clear();
            txtSenha.Clear();
            txtConSenha.Clear();
            txtDicaSenha.Clear();
            txtPergunta.Clear();
            txtResposta.Clear();
            txtDicaRes.Clear();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LinklblPergunta_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Essa pergunta será usada como meio de recuperação de senha caso você esqueça a mesma. \n" +
                            "Exemplo: Qual a data de nascimento do meu cachorro favorito?\n" +
                            "Resposta: 19/10/2018", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LinklblDica_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Os campos de dica são de preenchimento opcional e eles servem para auxiliar no preenchimento da senha. \n" +
                     "Exemplo: Senha: 123321159357\n" +
                     "Dica: começa com 123", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

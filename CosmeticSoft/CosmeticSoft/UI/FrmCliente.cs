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
    public partial class FrmCliente : Form
    {
        BLL.ClienteBLL clienteBLL = new BLL.ClienteBLL();
        DAL.ClienteDAL clienteDAL = new DAL.ClienteDAL();

        int[]
            numcpf = new int[11],
            vercpf = new int[10],
            vercpf2 = new int[11]
            ;
        int i;
        string local;

        bool cp;

        bool atualizar = false;

        string CpfMask;

        public FrmCliente()
        {
            InitializeComponent();
        }

        public void limpaCampos()
        {
            txtNome.Clear();
            txtCPF.Clear();
            txtFone.Clear();
            txtCel.Clear();
        }
   
        private void FrmCliente_Load(object sender, EventArgs e)
        {
            dgvConsulta.DataSource = clienteDAL.ConsultarTodosCliente();
        }

        private void BtnEditar_Click_1(object sender, EventArgs e)
        {

            if (dgvConsulta.RowCount > 0)
            {
                clienteBLL.Codcli = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value);
                clienteBLL = clienteDAL.RetornaDados(clienteBLL);
                txtNome.Text = clienteBLL.Nome;
                txtFone.Text = clienteBLL.Fone;
                txtCPF.Text = clienteBLL.Cpf;
                txtCel.Text = clienteBLL.Cel;

                atualizar = true;
                btnCadastrar.Text = "Atualizar";
                lblCliente.Text = "Atualiza cliente";
            }
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            limpaCampos();
        }

        private void BtnApagar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja apagar este cliente?", "Apagar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clienteBLL.Codcli = Convert.ToInt16(dgvConsulta.SelectedCells[0].Value.ToString());
                clienteDAL.Apagar(clienteBLL);
                dgvConsulta.DataSource = clienteDAL.ConsultarTodosCliente();
            }
        }

        private void BtnCadastrar_Click_1(object sender, EventArgs e)
        {
            string cpf = txtCPF.Text;
            cpf = cpf.Replace(".", null);
            cpf = cpf.Replace("-", null);
            CpfMask = cpf;


            clienteBLL.Nome = txtNome.Text;
            clienteBLL.Cpf = CpfMask;
            clienteBLL.Fone = txtFone.Text;
            clienteBLL.Cel = txtCel.Text;

            verificaCPF();

            if (txtNome.Text == "" || txtCPF.Text == "" || txtCel.Text == "")
            {
                MessageBox.Show("Verifique os campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    if (cp == true)
                    {
                        if (atualizar == false)
                        {
                            clienteDAL.Cadastrar(clienteBLL);
                            MessageBox.Show(" Cadastro efetuado com sucesso ");
                            limpaCampos();
                        }
                        else
                        {
                            clienteDAL.Atualizar(clienteBLL);
                            MessageBox.Show(" Alteração efetuada com sucesso ");
                            btnCadastrar.Text = "Cadastrar";
                            lblCliente.Text = "Cadastra cliente";
                            limpaCampos();
                            atualizar = false;
                        }
                        dgvConsulta.DataSource = clienteDAL.ConsultarTodosCliente();
                    }
                }
                catch
                {
                    MessageBox.Show("Verifique os campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void TxtFiltro_TextChanged_1(object sender, EventArgs e)
        {
            clienteBLL.Nome = txtFiltro.Text;
            dgvConsulta.DataSource = clienteDAL.FiltrarCliente(clienteBLL);
        }

        private void BtnFecha_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void verificaCPF()
        {
            string cpf;
            cpf = CpfMask;
            
            try
            {
                for (int i = 0; i < 11; i++)
                {
                    int pedacoCpf = int.Parse(cpf.Substring(i, 1));
                    numcpf[i] = pedacoCpf;
                }

                verificaCPF1();
                verificaCPF2();
                vocesabe();
            }
            catch
            {
                if (txtCPF.Text == "")
                {
                }
                else
                {
                }
            }
        }

        public void verificaCPF1()
        {
            vercpf[0] = numcpf[0] * 10;
            vercpf[1] = numcpf[1] * 9;
            vercpf[2] = numcpf[2] * 8;
            vercpf[3] = numcpf[3] * 7;
            vercpf[4] = numcpf[4] * 6;
            vercpf[5] = numcpf[5] * 5;
            vercpf[6] = numcpf[6] * 4;
            vercpf[7] = numcpf[7] * 3;
            vercpf[8] = numcpf[8] * 2;

            int digResA = 0;// = vercpf[0] + vercpf[];
            for (int i = -1; i < 8; i++)
            {
                digResA = digResA + vercpf[i + 1];
            }

            digResA = digResA % 11;

            if (digResA < 2)
            {
                vercpf[9] = 0;
            }

            else
            {
                vercpf[9] = 11 - digResA;
            }
        }

        private void TxtFone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TxtCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TxtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificaCPF2()
        {
            vercpf2[0] = numcpf[0] * 11;
            vercpf2[1] = numcpf[1] * 10;
            vercpf2[2] = numcpf[2] * 9;
            vercpf2[3] = numcpf[3] * 8;
            vercpf2[4] = numcpf[4] * 7;
            vercpf2[5] = numcpf[5] * 6;
            vercpf2[6] = numcpf[6] * 5;
            vercpf2[7] = numcpf[7] * 4;
            vercpf2[8] = numcpf[8] * 3;
            vercpf2[9] = vercpf[9] * 2;

            int digResB = 0;// = vercpf[0] + vercpf[];
            for (int i = -1; i < 9; i++)
            {
                digResB = digResB + vercpf2[i + 1];
            }

            digResB = digResB % 11;
            if (digResB < 2)
            {
                vercpf2[10] = 0;
            }
            else
            {
                vercpf2[10] = 11 - digResB;
            }
        }

        public void vocesabe()
        {
            if (numcpf[9] == vercpf[9] && numcpf[10] == vercpf2[10])
            {
                cp = true;
            }
            else
            {
                cp = false;
                MessageBox.Show(" CPF Incorreto ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPF.Focus();
            }
        }
    }
}

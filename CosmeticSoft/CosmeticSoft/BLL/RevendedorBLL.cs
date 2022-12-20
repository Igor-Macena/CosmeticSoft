using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticSoft.BLL
{
    class RevendedorBLL
    {
        int codrevendedor;
        string nome;
        string senha;
        string dicasenha;
        string pergunta;
        string resposta;
        string dicares;

        public int Codrevendedor { get => codrevendedor; set => codrevendedor = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Dicasenha { get => dicasenha; set => dicasenha = value; }
        public string Pergunta { get => pergunta; set => pergunta = value; }
        public string Resposta { get => resposta; set => resposta = value; }
        public string Dicares { get => dicares; set => dicares = value; }
    }
}

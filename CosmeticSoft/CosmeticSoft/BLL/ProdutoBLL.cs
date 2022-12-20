using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticSoft.BLL
{
    class ProdutoBLL
    {
        int codprod;
        string nome;
        double preco;
        int qtd;
        double custo;
        Nullable<DateTime> validade;

        public int Codprod { get => codprod; set => codprod = value; }
        public string Nome { get => nome; set => nome = value; }
        public double Preco { get => preco; set => preco = value; }
        public int Qtd { get => qtd; set => qtd = value; }
        public double Custo { get => custo; set => custo = value; }
        public DateTime? Validade { get => validade; set => validade = value; }
    }
}

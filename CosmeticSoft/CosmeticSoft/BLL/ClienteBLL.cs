using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticSoft.BLL
{
    class ClienteBLL
    {
        int codcli;
        string nome;
        string cpf;
        string fone;
        string cel;

        public int Codcli { get => codcli; set => codcli = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Fone { get => fone; set => fone = value; }
        public string Cel { get => cel; set => cel = value; }
    }
}

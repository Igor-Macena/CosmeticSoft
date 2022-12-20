using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticSoft.BLL
{
    class VendaBLL
    {
        int codvenda;
        DateTime datavenda;
        int codcli;
        int codprod;
        string estatus;
        double total;
        double lucro;

        public int Codvenda { get => codvenda; set => codvenda = value; }
        public DateTime Datavenda { get => datavenda; set => datavenda = value; }
        public int Codcli { get => codcli; set => codcli = value; }
        public double Total { get => total; set => total = value; }
        public string Estatus { get => estatus; set => estatus = value; }
        public double Lucro { get => lucro; set => lucro = value; }
        public int Codprod { get => codprod; set => codprod = value; }
    }
}

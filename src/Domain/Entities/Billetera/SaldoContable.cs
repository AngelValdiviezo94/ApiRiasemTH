using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Wallet
{
    public class SaldoContable
    {
        public decimal SaldoDisponible { get; set; }

        public decimal ValorContable { get; set; }

        public decimal AhorroProgramado { get; set; }

        public decimal SaldoTotal { get; set; }

        public decimal PorcentajeSaldo { get; set; }
    }
}

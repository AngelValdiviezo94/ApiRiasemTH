using EnrolApp.Application.Features.Billeteras.Interfaces;
using EnrolApp.Domain.Entities.Wallet;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Repository.Billetera;

public class SaldoContableService : ISaldoContable
{
    private readonly ILogger<SaldoContableService> _log;

    public SaldoContableService(ILogger<SaldoContableService> log)
    {
        _log = log;
    }

    public Task<SaldoContable> GetSaldoContableByIdentificacionAsync(string Identificacion)
    {
        var Obj = new SaldoContable()
        {
            SaldoDisponible = Convert.ToDecimal(175.25),
            AhorroProgramado = Convert.ToDecimal(50),
            SaldoTotal = Convert.ToDecimal(400.50),
            ValorContable = Convert.ToDecimal(225.25),
            PorcentajeSaldo = Convert.ToDecimal(0.55)
        };

        return Task.FromResult(Obj);
    }

}

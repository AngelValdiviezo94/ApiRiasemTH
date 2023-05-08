using EnrolApp.Domain.Entities.Wallet;

namespace EnrolApp.Application.Features.Billeteras.Interfaces;

public interface ISaldoContable
{
    Task<SaldoContable> GetSaldoContableByIdentificacionAsync(string identificacion);
}

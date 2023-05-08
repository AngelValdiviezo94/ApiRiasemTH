using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Domain.Entities.Wallet;

namespace EnrolApp.Application.Features.Wallets.Interfaces;

public interface ICupoCredito
{
    Task<ResponseType<CupoCredito>> GetCupoCreditoAsync(string AuthToken);
}

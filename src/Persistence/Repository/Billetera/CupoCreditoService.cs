using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Wallets.Interfaces;
using EnrolApp.Domain.Entities.Wallet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EnrolApp.Persistence.Repository.Billetera;
public class CupoCreditoService : ICupoCredito
{
    private readonly ILogger<CupoCreditoService> _log;
    private readonly string UrlBaseApiEcommerce = string.Empty;
    private readonly IConfiguration _config;

    public CupoCreditoService(ILogger<CupoCreditoService> log, IConfiguration config)
    {
        _log = log;
        _config = config;
        UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiEcommerce").Get<string>();
    }

    public async Task<ResponseType<CupoCredito>> GetCupoCreditoAsync(string AuthToken)
    {
        var nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiEcommerce:GetCliente").Get<string>();
        var uriEndPoint = UrlBaseApiEcommerce + nombreEnpoint;

        try
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", AuthToken ?? string.Empty);
            var response = await client.GetAsync(uriEndPoint);
            if (response.IsSuccessStatusCode)
            {

                var resulTask = response.Content.ReadFromJsonAsync<ResponseType<CupoCredito>>().Result;
                return resulTask;
            }

        }
        catch (Exception ex)
        {

            _log.LogError(ex, "Error en " + nombreEnpoint);
        }
        return null;
    }
}

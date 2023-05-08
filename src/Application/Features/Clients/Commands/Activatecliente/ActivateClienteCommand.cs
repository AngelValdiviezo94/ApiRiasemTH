using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace EnrolApp.Application.Features.Clients.Commands.ActivateCliente;

public record ActivateClienteCommand(string Identificacion) : IRequest<ResponseType<string>>;

public class ActivateClienteCommandHandler : IRequestHandler<ActivateClienteCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<Cliente> _repositoryAsyncCl;
    private readonly IRepositoryAsync<Prospecto> _repositoryAsyncPr;
    private readonly IRepositoryAsync<FamiliarColaborador> _repositoryAsyncFami;
    private readonly IConfiguration _config;
    private readonly IApisConsumoAsync _repositoryApis;
    private readonly string UrlBaseApiAuth = "";
    private  string nombreEnpoint = "";

    public ActivateClienteCommandHandler(IRepositoryAsync<FamiliarColaborador> repositoryFami, IRepositoryAsync<Cliente> repository, IRepositoryAsync<Prospecto> repoProspecto, 
        IApisConsumoAsync repositoryApis,IConfiguration configuration)
    {
        _repositoryAsyncCl = repository;
        _repositoryAsyncFami = repositoryFami;
        _repositoryAsyncPr = repoProspecto;
        _repositoryApis = repositoryApis;
        _config = configuration;
        UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
    }


    public async Task<ResponseType<string>> Handle(ActivateClienteCommand request, CancellationToken cancellationToken)
    {
        byte[] decbuff = Convert.FromBase64String(request.Identificacion);
        var identificacion = System.Text.Encoding.UTF8.GetString(decbuff);

        nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiAuth:ActivarUsuarioLdap").Get<string>();

        //Invocar endpoint de ldap que verifica si la fecha de activacion de servicio esta dentro de lo permitido para
        var uriActivateLdapUser = UrlBaseApiAuth + nombreEnpoint;
        var (success, objData) = await  _repositoryApis.PostEndPoint(new { identificacion }, uriActivateLdapUser, nombreEnpoint);

        if (success && ((ResponseType<string>)objData).StatusCode == "100" )
        {
            //Se actualiza el campo ServicioActivo em la tabla Cliente
            Specifications.ClienteByIdentificacionSpec cliente = new(identificacion);
            var objCliente = await _repositoryAsyncCl.FirstOrDefaultAsync(cliente, cancellationToken);
            if (objCliente != null)
            {
                objCliente.ServicioActivo = true;
                await _repositoryAsyncCl.UpdateAsync(objCliente, cancellationToken);
                return new ResponseType<string>() { Succeeded = true, Message = "Servicio activado satisfactoriamente", StatusCode = "000" };
            }
            else
            {
                var objFamiliar = await _repositoryAsyncFami.FirstOrDefaultAsync(new FamiliarByIdentificacionSpec("C", identificacion), cancellationToken);
                if (objFamiliar == null)
                {
                    return new ResponseType<string>() { Succeeded = false, Message = "Familiar no registrado", StatusCode = "002" };
                }
                objFamiliar.ServicioActivo = true;
                await _repositoryAsyncFami.UpdateAsync(objFamiliar, cancellationToken);

                return new ResponseType<string>() { Succeeded = true, Message = "Servicio activado satisfactoriamente", StatusCode = "000" };
            }

            return new ResponseType<string>() { Succeeded = false, Message = "Cliente no registrado", StatusCode = "002" };

        }

        return new ResponseType<string>() { Succeeded = false, Message = "Error en la activacion del servicio",StatusCode ="002" };
    }
}
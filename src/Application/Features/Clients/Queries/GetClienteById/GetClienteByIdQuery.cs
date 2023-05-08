using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Familiares;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace EnrolApp.Application.Features.Clients.Queries.GetClienteById;

public record GetProspectoByIdQuery(string Codigo, string identificacion) : IRequest<ResponseType<ClienteType>>;

public class GetClienteByIdQueryHandler : IRequestHandler<GetProspectoByIdQuery, ResponseType<ClienteType>>
{
    private readonly IRepositoryAsync<Cliente> _repositoryAsync;
    private readonly IRepositoryAsync<FamiliarColaborador> _repositoryAsyncFami;
    private readonly IMapper _mapper;
    private readonly ILogger<GetClienteByIdQueryHandler> _log;

    public GetClienteByIdQueryHandler(ILogger<GetClienteByIdQueryHandler> log, IRepositoryAsync<Cliente> repository, IRepositoryAsync<FamiliarColaborador> repositoryFami, IMapper mapper, IConfiguration config)
    {
        _log = log;
        _repositoryAsync = repository;
        _repositoryAsyncFami = repositoryFami;
        _mapper = mapper;
    }

    public async Task<ResponseType<ClienteType>> Handle(GetProspectoByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            //return new ResponseType<ClienteType>() { Data = null, Succeeded = true, StatusCode = "001", Message = "El número de cédula ingresado es incorrecto" };
            var objCliente = await _repositoryAsync.FirstOrDefaultAsync(new ClienteByCodigoSpec(request.Codigo, request.identificacion), cancellationToken);

            if (objCliente is not null)
            {
                if (objCliente.Estado == "I")
                {
                    return new ResponseType<ClienteType>() { Data = null, Succeeded = false, StatusCode = "001", Message = "Para iniciar sesión primero debes suscribirte en el Ecosistema Enrolapp" };
                }

                if (!objCliente.ServicioActivo)
                {
                    return new ResponseType<ClienteType>() { Data = _mapper.Map<ClienteType>(objCliente), Succeeded = false, StatusCode = "001", Message = "Para iniciar sesión debes activar el servicio" };
                }

                return new ResponseType<ClienteType>() { Data = _mapper.Map<ClienteType>(objCliente), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
            }
            else
            {
                var objFamiliar = await _repositoryAsyncFami.FirstOrDefaultAsync(new FamiliarByIdentificacionSpec("C" ,request.identificacion), cancellationToken);
                if (objFamiliar is not null)
                {
                    if (objFamiliar.Estado == "I")
                    {
                        return new ResponseType<ClienteType>() { Data = null, Succeeded = false, StatusCode = "001", Message = "Para iniciar sesión primero debes suscribirte en el Ecosistema Enrolapp" };
                    }

                    if (objFamiliar.ServicioActivo == false)
                    {
                        return new ResponseType<ClienteType>() { Data = _mapper.Map<ClienteType>(objCliente), Succeeded = false, StatusCode = "001", Message = "Para iniciar sesión debes activar el servicio" };
                    }
                }
                if (!string.IsNullOrEmpty(objFamiliar.SesionColaborador))
                {
                    objFamiliar.SesionColaborador = objFamiliar.SesionColaborador.Split(" ")[1];
                }

                return new ResponseType<ClienteType>() { Data = _mapper.Map<ClienteType>(objFamiliar), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
            }

        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<ClienteType>() { Data = null, Succeeded = true, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }
}
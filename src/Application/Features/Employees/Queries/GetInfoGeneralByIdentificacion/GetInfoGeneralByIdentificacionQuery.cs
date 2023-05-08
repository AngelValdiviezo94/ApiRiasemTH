using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Dto;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EnrolApp.Application.Features.Employees.Queries.GetInfoGeneralByIdentificacion;

public record GetInfoGeneralByIdentificacionQuery(string Identificacion) : IRequest<ResponseType<InformacionGeneralEmpleadoType>>;

public class GetInfoGeneralByIdentificacionQueryHandler : IRequestHandler<GetInfoGeneralByIdentificacionQuery, ResponseType<InformacionGeneralEmpleadoType>>
{
    private readonly IConfiguration _config;
    private readonly IEmpleado _repository;
    private readonly IMapper _mapper;
    private readonly IRepositoryAsync<Cliente> _repoCliAsync;
    private readonly ILogger<GetInfoGeneralByIdentificacionQueryHandler> _log;
    //private string uriEnpoint = string.Empty;
    //private string nombreEnpoint = string.Empty;
    //private readonly string UrlBaseApiWorkflow = string.Empty;

    public GetInfoGeneralByIdentificacionQueryHandler(IEmpleado repository, IMapper mapper, IRepositoryAsync<Cliente> repoCliAsync, IConfiguration config, ILogger<GetInfoGeneralByIdentificacionQueryHandler> log)
    {
        _config = config;
        _repository = repository;
        _mapper = mapper;
        _log = log;
        _repoCliAsync = repoCliAsync;
        //UrlBaseApiWorkflow = _config.GetSection("ConsumoApis:UrlBaseApiWorkflow").Get<string>();
    }

    public async Task<ResponseType<InformacionGeneralEmpleadoType>> Handle(GetInfoGeneralByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetInfoGeneralByIdentificacion(request.Identificacion);

            if (objResult is null)
                return new ResponseType<InformacionGeneralEmpleadoType>() { Data = null, Succeeded = false, StatusCode = "001", Message = "Error al obtener los datos del usuario" };

            var result = _mapper.Map<InformacionGeneralEmpleadoType>(objResult);
            var cliente = await _repoCliAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(objResult.Identificacion), cancellationToken);

            if (cliente is not null)
            {
                result.Correo = cliente.Correo;
                result.Id = cliente.Id;
                result.LatitudLocalidad = cliente.Latitud;  // -2.14064;
                result.LongitudLocalidad = cliente.Longitud; //-79.93191;
                result.DispositivoId = cliente.DispositivoId;
            }

            return new ResponseType<InformacionGeneralEmpleadoType>() { Data = result, Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<InformacionGeneralEmpleadoType>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }

    }

}

using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using EnrolApp.Domain.Entities.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Employees.Queries.GetCertifLaboralByIdentificacion;


public record GetCertifLaboralByIdentificacionQuery(string Identificacion) : IRequest<ResponseType<CertificadoLaboralType>>;

public class GetCertifLaboralByIdentificacionQueryHandler : IRequestHandler<GetCertifLaboralByIdentificacionQuery, ResponseType<CertificadoLaboralType>>
{

    private readonly IReportesEmpleado _repository;
    private readonly IRepositoryAsync<Cliente> _repoCliAsync;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCertifLaboralByIdentificacionQueryHandler> _log;

    public GetCertifLaboralByIdentificacionQueryHandler(IReportesEmpleado repository, 
        IMapper mapper, ILogger<GetCertifLaboralByIdentificacionQueryHandler> log,
        IRepositoryAsync<Cliente> repoCliAsync)
    {
        _log = log;
        _repository = repository;
        _mapper = mapper;
        _repoCliAsync = repoCliAsync;
    }

    public async Task<ResponseType<CertificadoLaboralType>> Handle(GetCertifLaboralByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetCertificadoLaboralByIdentificacionAsync(request.Identificacion);
            var objCliente = await _repoCliAsync.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(request.Identificacion));

            if (objResult is null) return new ResponseType<CertificadoLaboralType>() { Data = null, Succeeded = true, StatusCode = "001", Message = "No existe informaciòn para mostrar, en el corte seleccionado." };
            if(objCliente is not null) objResult.Correo = objCliente.Correo;
            return new ResponseType<CertificadoLaboralType>() { Data = _mapper.Map<CertificadoLaboralType>(objResult), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<CertificadoLaboralType>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }
}

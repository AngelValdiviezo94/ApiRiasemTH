using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Employees.Queries.GetAvisoEntradaByIdentificacion;


public record GetAvisoEntradaByIdentificacionQuery(string Identificacion) : IRequest<ResponseType<AvisoEntradaType>>;

public class GetAvisoEntradaByIdentificacionQueryHandler : IRequestHandler<GetAvisoEntradaByIdentificacionQuery, ResponseType<AvisoEntradaType>>
{
    private readonly IReportesEmpleado _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAvisoEntradaByIdentificacionQueryHandler> _log;
    public GetAvisoEntradaByIdentificacionQueryHandler(IReportesEmpleado repository, IMapper mapper, ILogger<GetAvisoEntradaByIdentificacionQueryHandler> log)
    {
        _log = log;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseType<AvisoEntradaType>> Handle(GetAvisoEntradaByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetAvisoEntradaByIdentificacionAsync(request.Identificacion);

            if (objResult is null)
                return new ResponseType<AvisoEntradaType>() { Data = null, Succeeded = true, StatusCode = "001", Message = "No existe informaciòn para mostrar, en el corte seleccionado." };

            return new ResponseType<AvisoEntradaType>() { Data = _mapper.Map<AvisoEntradaType>(objResult), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch(Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<AvisoEntradaType>() { Data = null, Succeeded = true, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }
}

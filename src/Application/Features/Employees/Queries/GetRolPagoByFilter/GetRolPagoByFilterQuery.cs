using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Employees.Dto;
using EnrolApp.Application.Features.Employees.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Employees.Queries.GetRolPagoByFilter;


public record GetRolPagoByFilterQuery(string Identificacion, string FechaCorte) : IRequest<ResponseType<RolPagoType>>;

public class GetRolPagoByFilterQueryHandler : IRequestHandler<GetRolPagoByFilterQuery, ResponseType<RolPagoType>>
{
    private readonly IReportesEmpleado _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRolPagoByFilterQueryHandler> _log;
    public GetRolPagoByFilterQueryHandler(IReportesEmpleado repository, IMapper mapper, ILogger<GetRolPagoByFilterQueryHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<RolPagoType>> Handle(GetRolPagoByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetRolPagoByFilterAsync(request.Identificacion, request.FechaCorte);

            if (!objResult.ListaEgresos.Any() && !objResult.ListaIngresos.Any())
            {
                return new ResponseType<RolPagoType>() { Data = null, Succeeded = true, StatusCode = "001", Message = "No existe información para mostrar, en el corte seleccionado." };
            }

            return new ResponseType<RolPagoType>() { Data = _mapper.Map<RolPagoType>(objResult), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<RolPagoType>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }

    }
}


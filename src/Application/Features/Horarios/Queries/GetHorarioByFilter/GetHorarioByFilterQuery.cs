using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Horarios.Interfaces;
using EnrolApp.Domain.Entities.Horario;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Horarios.Queries.GetHorarioByFilter;

public record GetHorarioByFilterQuery(string Identificacion, string FechaDesde, string FechaHasta, string Token) : IRequest<ResponseType<List<Horario>>>;

public class GetHorarioByFilterQueryHandler : IRequestHandler<GetHorarioByFilterQuery, ResponseType<List<Horario>>>
{
    private readonly IHorario _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetHorarioByFilterQueryHandler> _log;

    public GetHorarioByFilterQueryHandler(IHorario repository, IMapper mapper, ILogger<GetHorarioByFilterQueryHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<List<Horario>>> Handle(GetHorarioByFilterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetHorarioByFilterAsync(request.Identificacion, request.FechaDesde, request.FechaHasta, request.Token);

            if (!objResult.Any())
                return new ResponseType<List<Horario>>() { Data = null, Succeeded = true, StatusCode = "001", Message = "No existe información para mostrar, en el corte seleccionado." };

            return new ResponseType<List<Horario>>() { Data = objResult, Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        } 
        catch(Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<List<Horario>>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }
}


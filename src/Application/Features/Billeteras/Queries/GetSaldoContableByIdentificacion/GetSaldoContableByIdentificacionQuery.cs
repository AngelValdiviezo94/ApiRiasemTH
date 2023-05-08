using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Billeteras.Dto;
using EnrolApp.Application.Features.Billeteras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Billeteras.Queries.GetSaldoContableByIdentificacion;

public record GetSaldoContableByIdentificacionQuery(string Identificacion) : IRequest<ResponseType<SaldoContableResponseType>>;

public class GetSaldoContableByIdentificacionQueryHandler : IRequestHandler<GetSaldoContableByIdentificacionQuery, ResponseType<SaldoContableResponseType>>
{
    private readonly ISaldoContable _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetSaldoContableByIdentificacionQueryHandler> _log;

    public GetSaldoContableByIdentificacionQueryHandler(ISaldoContable repository, IMapper mapper, ILogger<GetSaldoContableByIdentificacionQueryHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<SaldoContableResponseType>> Handle(GetSaldoContableByIdentificacionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.GetSaldoContableByIdentificacionAsync(request.Identificacion);

            if (objResult is null)
                return new ResponseType<SaldoContableResponseType>() { Data = null, Succeeded = true, StatusCode = "001", Message = CodeMessageResponse.GetMessageByCode("001") };

            return new ResponseType<SaldoContableResponseType>() { Data = _mapper.Map<SaldoContableResponseType>(objResult), Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<SaldoContableResponseType>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }

}
using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Wallets.Dto;
using EnrolApp.Application.Features.Wallets.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Wallets.Queries.GetCupoCredito;

public record GetCupoCreditoQuery(string AuthToken) : IRequest<ResponseType<CupoCreditoResponseType>>;

public class GetCupoCreditoQueryHandler : IRequestHandler<GetCupoCreditoQuery, ResponseType<CupoCreditoResponseType>>
{
    private readonly ICupoCredito _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCupoCreditoQueryHandler> _log;

    public GetCupoCreditoQueryHandler(ILogger<GetCupoCreditoQueryHandler> log, ICupoCredito repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<CupoCreditoResponseType>> Handle(GetCupoCreditoQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var objResultEcom = await _repository.GetCupoCreditoAsync(request.AuthToken);

            if (!objResultEcom.Succeeded)
                return new ResponseType<CupoCreditoResponseType>() { Data = null, Succeeded = false, StatusCode = "002", Message = objResultEcom.Message ?? CodeMessageResponse.GetMessageByCode("002") };

            var objResult = _mapper.Map<CupoCreditoResponseType>(objResultEcom.Data);

            if (objResultEcom.Data.CreditoCupo > 0)
                objResult.PorcentajeSaldo = Math.Round(Convert.ToDecimal(objResultEcom.Data.CreditoDisponible) / Convert.ToDecimal(objResultEcom.Data.CreditoCupo), 2);
            else 
                objResult.PorcentajeSaldo = 0;

            objResult.SaldoUtilizado = Convert.ToDecimal(objResultEcom.Data.CreditoCupo) - Convert.ToDecimal(objResultEcom.Data.CreditoDisponible);
            objResult.ConsumoDebito = objResult.SaldoUtilizado;
            objResult.SaldoPagar = objResult.SaldoUtilizado;
            objResult.FechaCorte = objResultEcom.Data.FechaUltimoPago;

            return new ResponseType<CupoCreditoResponseType>() { Data = objResult, Succeeded = true, StatusCode = "000", Message = CodeMessageResponse.GetMessageByCode("000") };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<CupoCreditoResponseType>() { Data = null, Succeeded = false, StatusCode = "500", Message = CodeMessageResponse.GetMessageByCode("500") };
        }
    }

}

using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Dto;
using EnrolApp.Application.Features.Familiares.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Familiares.Commands.GetTipoRelacionFamiliar
{
    public record GetTipoRelacionFamiliarCommand() : IRequest<ResponseType<List<ResponseTipoRelacionFamiliarType>>>;

    public class GetTipoRelacionFamiliarCommandHandler : IRequestHandler<GetTipoRelacionFamiliarCommand, ResponseType<List<ResponseTipoRelacionFamiliarType>>>
    {
        private readonly IFamiliares _repoFamiliaresAsync;

        public GetTipoRelacionFamiliarCommandHandler(IFamiliares repoFamiliaresAsync)
        {
            _repoFamiliaresAsync = repoFamiliaresAsync;
        }

        public async Task<ResponseType<List<ResponseTipoRelacionFamiliarType>>> Handle(GetTipoRelacionFamiliarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoFamiliaresAsync.GetTipoRelacionFamiliarAsync();

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<List<ResponseTipoRelacionFamiliarType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}

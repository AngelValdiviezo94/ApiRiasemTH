using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador
{
    public record UpdateFamiliarColaboradorCommand(UpdateFamiliarColaboradorRequest Request, string TokenEcommerce) : IRequest<ResponseType<string>>;

    public class GetTipoRelacionFamiliarCommandHandler : IRequestHandler<UpdateFamiliarColaboradorCommand, ResponseType<string>>
    {
        private readonly IFamiliares _repoFamiliaresAsync;

        public GetTipoRelacionFamiliarCommandHandler(IFamiliares repoFamiliaresAsync)
        {
            _repoFamiliaresAsync = repoFamiliaresAsync;
        }
        public async Task<ResponseType<string>> Handle(UpdateFamiliarColaboradorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoFamiliaresAsync.UpdateFamiliarColaboradorAsync(request.Request, request.TokenEcommerce);

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}

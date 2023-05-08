using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Familiares.Commands.CreateFamiliarColaborador
{
    public record CreateFamiliarColaboradorCommand(CreateFamiliarColaboradorRequest Request, string TokenEcommerce) : IRequest<ResponseType<string>>;

    public class GetTipoRelacionFamiliarCommandHandler : IRequestHandler<CreateFamiliarColaboradorCommand, ResponseType<string>>
    {
        private readonly IFamiliares _repoFamiliaresAsync;

        public GetTipoRelacionFamiliarCommandHandler(IFamiliares repoFamiliaresAsync)
        {
            _repoFamiliaresAsync = repoFamiliaresAsync;
        }
        public async Task<ResponseType<string>> Handle(CreateFamiliarColaboradorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoFamiliaresAsync.CreateFamiliarColaboradorAsync(request.Request, request.TokenEcommerce);

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}

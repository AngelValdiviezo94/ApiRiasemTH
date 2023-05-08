using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Familiares.Commands.UpdateSesionFamilar
{
    public record UpdateSesionFamiliarCommand(UpdateSesionFamiliarRequest Request) : IRequest<ResponseType<string>>;

    public class GetTipoRelacionFamiliarCommandHandler : IRequestHandler<UpdateSesionFamiliarCommand, ResponseType<string>>
    {
        private readonly IFamiliares _repoFamiliaresAsync;

        public GetTipoRelacionFamiliarCommandHandler(IFamiliares repoFamiliaresAsync)
        {
            _repoFamiliaresAsync = repoFamiliaresAsync;
        }
        public async Task<ResponseType<string>> Handle(UpdateSesionFamiliarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoFamiliaresAsync.UpdateSesionFamiliarAsync(request.Request.Identificacion, request.Request.Token);

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}

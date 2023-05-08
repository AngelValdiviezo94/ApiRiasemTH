using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Dto;
using EnrolApp.Application.Features.Familiares.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Familiares.Commands.GetInfoFamiliarColaborador
{
    public record GetInfoFamiliarColaboradorCommand(string ColaboradorId) : IRequest<ResponseType<List<ResponseFamiliarColaboradorType>>>;

    public class GetInfoFamiliarColaboradorCommandHandler : IRequestHandler<GetInfoFamiliarColaboradorCommand, ResponseType<List<ResponseFamiliarColaboradorType>>>
    {
        private readonly IFamiliares _repoFamiliaresAsync;

        public GetInfoFamiliarColaboradorCommandHandler(IFamiliares repoFamiliaresAsync)
        {
            _repoFamiliaresAsync = repoFamiliaresAsync;
        }

        public async Task<ResponseType<List<ResponseFamiliarColaboradorType>>> Handle(GetInfoFamiliarColaboradorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoFamiliaresAsync.GetInfoFamiliarColaboradorAsync(request.ColaboradorId);

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<List<ResponseFamiliarColaboradorType>>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }
}

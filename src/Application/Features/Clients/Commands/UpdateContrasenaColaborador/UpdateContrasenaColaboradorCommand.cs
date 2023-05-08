using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Interfaces;
using MediatR;

namespace EnrolApp.Application.Features.Clients.Commands.UpdateContrasenaColaborador
{
    public record UpdateContrasenaColaboradorCommand(UpdateContrasenaColaboradorRequest Request) : IRequest<ResponseType<string>>;

    public class UpdateInfoColaboradorCommandHandler : IRequestHandler<UpdateContrasenaColaboradorCommand, ResponseType<string>>
    {
        private readonly IColaborador _repoColaboradorAsync;

        public UpdateInfoColaboradorCommandHandler(IColaborador repoColaboradorAsync)
        {
            _repoColaboradorAsync = repoColaboradorAsync;
        }

        public async Task<ResponseType<string>> Handle(UpdateContrasenaColaboradorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _repoColaboradorAsync.UpdateContrasenaColaboradorAsync(request.Request);

                return response;
            }
            catch (Exception)
            {
                return new ResponseType<string>() { Data = null, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500", Succeeded = false };
            }
        }
    }


}

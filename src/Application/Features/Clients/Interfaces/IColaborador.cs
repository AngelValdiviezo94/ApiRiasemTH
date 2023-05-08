using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Commands.UpdateContrasenaColaborador;

namespace EnrolApp.Application.Features.Clients.Interfaces
{
    public interface IColaborador
    {
        Task<ResponseType<string>> UpdateContrasenaColaboradorAsync(UpdateContrasenaColaboradorRequest request);
    }
}

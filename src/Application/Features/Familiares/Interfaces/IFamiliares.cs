using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Familiares.Commands.CreateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador;
using EnrolApp.Application.Features.Familiares.Dto;

namespace EnrolApp.Application.Features.Familiares.Interfaces
{
    public interface IFamiliares
    {
        Task<ResponseType<List<ResponseTipoRelacionFamiliarType>>> GetTipoRelacionFamiliarAsync();
        Task<ResponseType<string>> CreateFamiliarColaboradorAsync(CreateFamiliarColaboradorRequest request, string tokenEcommerce);
        Task<ResponseType<List<ResponseFamiliarColaboradorType>>> GetListadoFamiliarColaboradorAsync(string ColaboradorId);
        Task<ResponseType<List<ResponseFamiliarColaboradorType>>> GetInfoFamiliarColaboradorAsync(string identificacionFamiliar);
        Task<ResponseType<string>> UpdateFamiliarColaboradorAsync(UpdateFamiliarColaboradorRequest request, string tokenEcommerce);
        Task<ResponseType<string>> UpdateSesionFamiliarAsync(string identificacion, string token);
    }
}

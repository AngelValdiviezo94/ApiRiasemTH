using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Notificacion;
using EnrolApp.Domain.Entities.Suscripcion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EnrolApp.Application.Features.Notifications.Commands.UpdateNotificacion;

public record UpdateNotificacionCommand(string[] listaIdNotif) : IRequest<ResponseType<string>>;

public class UpdateNotificacionCommandHandler : IRequestHandler<UpdateNotificacionCommand, ResponseType<string>>
{
    private readonly IRepositoryAsync<BitacoraNotificacion> _repositoryAsync;
    private readonly ILogger<UpdateNotificacionCommandHandler> _log;

    public UpdateNotificacionCommandHandler(IRepositoryAsync<BitacoraNotificacion> repository, ILogger<UpdateNotificacionCommandHandler> log)
    {
        _log = log;
        _repositoryAsync = repository;
    }

    public async Task<ResponseType<string>> Handle(UpdateNotificacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.listaIdNotif.Length > 0)
            {
                foreach (var item in request.listaIdNotif)
                {
                    var objNotif = await _repositoryAsync.GetByIdAsync(Guid.Parse(item.ToString()), cancellationToken);
                    objNotif.EstadoLeido = "S";
                    objNotif.FechaActualizacion = DateTime.Now;

                    await _repositoryAsync.UpdateAsync(objNotif, cancellationToken);
                }
                return new ResponseType<string>() { Succeeded = true, Message = "Estado de notificacion actualizado con exito", StatusCode = "200" };
            }
            else
            {
                return new ResponseType<string>() { Succeeded = false, Message = "Por favor ingrese código de notificación", StatusCode = "201" };
            }
        }
        catch(Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<string>() { Succeeded = false, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
        }
    }
}
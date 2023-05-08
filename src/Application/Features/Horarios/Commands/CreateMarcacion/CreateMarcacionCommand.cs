using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Horarios.Interfaces;
using EnrolApp.Application.Features.Horarios.Specifications;
using EnrolApp.Domain.Entities.Horario;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Horarios.Commands.CreateMarcacion;

public record CreateMarcacionCommand(CreateMarcacionRequest CreateMarcacion) : IRequest<ResponseType<string>>;

public class CreateMarcacionCommandHandler : IRequestHandler<CreateMarcacionCommand, ResponseType<string>>
{

    private readonly IHorario _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateMarcacionCommandHandler> _log;

    public CreateMarcacionCommandHandler(IHorario repository, IMapper mapper, ILogger<CreateMarcacionCommandHandler> log)
    {
        _repository = repository;
        _mapper = mapper;
        _log = log;
    }

    public async Task<ResponseType<string>> Handle(CreateMarcacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var objResult = await _repository.CreateMarcacion(request.CreateMarcacion, cancellationToken);

            if (objResult != "1")
                return new ResponseType<string>() { Data = null, Succeeded = false, StatusCode = "102", Message = objResult };

            return new ResponseType<string>() { Data = null, Succeeded = true, Message = "Marcación registrada correctamente.", StatusCode = "100" };
        }
        catch (Exception e)
        {
            _log.LogError(e, string.Empty);
            return new ResponseType<string>() { Data = null, Succeeded = false, Message = CodeMessageResponse.GetMessageByCode("500"), StatusCode = "500" };
        }
    }
}

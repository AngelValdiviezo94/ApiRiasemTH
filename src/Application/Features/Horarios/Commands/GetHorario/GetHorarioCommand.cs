using AutoMapper;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Horarios.Dto;
using EnrolApp.Application.Features.Prospectos.Specifications;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.Horario;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.Json;

namespace EnrolApp.Application.Features.Horarios.Commands.GetHorario;

public record GetHorarioCommand(GetHorarioRequest Identificacion) : IRequest<ResponseType<List<HorarioType>>>;


public class GetHorarioCommandHandler : IRequestHandler<GetHorarioCommand, ResponseType<List<HorarioType>>>
{
    private readonly IRepositoryAsync<Horario> _repositoryAsyncCl;
    private readonly IMapper _mapper;
    //private List<HorarioType> results;

    public GetHorarioCommandHandler(IRepositoryAsync<Horario> repository, IMapper mapper)
    {
        _repositoryAsyncCl = repository;
        _mapper = mapper;
        //_config = configuration;
        //UrlBaseApiAuth = _config.GetSection("ConsumoApis:UrlBaseApiAuth").Get<string>();
    }


    public Task<ResponseType<List<HorarioType>>> Handle(GetHorarioCommand request, CancellationToken cancellationToken)
    {
        #region Arreglo de Horarios

        //results = new List<HorarioType>()
        //    {
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-22"),
        //                MarcacionEntrada = TimeSpan.Parse("8:08"),
        //                MarcacionSalida = TimeSpan.Parse("5:10"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-23"),
        //                MarcacionEntrada = TimeSpan.Parse("8:06"),
        //                MarcacionSalida = TimeSpan.Parse("5:00"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-24"),
        //                MarcacionEntrada = TimeSpan.Parse("7:50"),
        //                MarcacionSalida = TimeSpan.Parse("5:50"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-25"),
        //                MarcacionEntrada = TimeSpan.Parse("8:01"),
        //                MarcacionSalida = TimeSpan.Parse("5:20"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-26"),
        //                MarcacionEntrada = TimeSpan.Parse("8:40"),
        //                MarcacionSalida = TimeSpan.Parse("5:20"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-27"),
        //                MarcacionEntrada = TimeSpan.Parse("7:48"),
        //                MarcacionSalida = TimeSpan.Parse("6:10"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            },
        //            new HorarioType
        //            {
        //                Fecha = DateTime.Parse("2022-08-28"),
        //                MarcacionEntrada = TimeSpan.Parse("7:48"),
        //                MarcacionSalida = TimeSpan.Parse("6:10"),
        //                TurnoEntrada = TimeSpan.Parse("8:00"),
        //                TurnoSalida = TimeSpan.Parse("5:00"),
        //                CodigoResultadoEvaluacion = "CN",
        //                DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            }
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-07"),
        //            //    MarcacionEntrada = TimeSpan.Parse("8:18"),
        //            //    MarcacionSalida = TimeSpan.Parse("7:12"),
        //            //    TurnoEntrada = TimeSpan.Parse("8:00"),
        //            //    TurnoSalida = TimeSpan.Parse("5:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //}
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-08"),
        //            //    MarcacionEntrada = TimeSpan.Parse("8:08"),
        //            //    MarcacionSalida = TimeSpan.Parse("5:10"),
        //            //    TurnoEntrada = TimeSpan.Parse("8:00"),
        //            //    TurnoSalida = TimeSpan.Parse("5:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-09"),
        //            //    MarcacionEntrada = TimeSpan.Parse("8:08"),
        //            //    MarcacionSalida = TimeSpan.Parse("5:10"),
        //            //    TurnoEntrada = TimeSpan.Parse("8:00"),
        //            //    TurnoSalida = TimeSpan.Parse("5:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-10"),
        //            //    MarcacionEntrada = TimeSpan.Parse("8:40"),
        //            //    MarcacionSalida = TimeSpan.Parse("6:10"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-11"),
        //            //    MarcacionEntrada = TimeSpan.Parse("9:08"),
        //            //    MarcacionSalida = TimeSpan.Parse("6:30"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-12"),
        //            //    MarcacionEntrada = TimeSpan.Parse("9:08"),
        //            //    MarcacionSalida = TimeSpan.Parse("6:30"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-13"),
        //            //    MarcacionEntrada = TimeSpan.Parse("9:14"),
        //            //    MarcacionSalida = TimeSpan.Parse("6:10"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-14"),
        //            //    MarcacionEntrada = TimeSpan.Parse("9:00"),
        //            //    MarcacionSalida = TimeSpan.Parse("5:50"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //},
        //            //new HorarioType
        //            //{
        //            //    Fecha = DateTime.Parse("2022-08-15"),
        //            //    MarcacionEntrada = TimeSpan.Parse("8:58"),
        //            //    MarcacionSalida = TimeSpan.Parse("6:30"),
        //            //    TurnoEntrada = TimeSpan.Parse("9:00"),
        //            //    TurnoSalida = TimeSpan.Parse("6:00"),
        //            //    CodigoResultadoEvaluacion = "CN",
        //            //    DescripcionResultadoEvaluacion = "Unsp intracranial injury w/o loss of consciousness, sequela"
        //            //}

        //    };
        #endregion


        return  Task.FromResult(new ResponseType<List<HorarioType>> () { Succeeded = true, Message = "Consulta generada correctamente", StatusCode = "000" /*Data = results  */});
    }
}
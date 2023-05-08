using AutoMapper;
using EnrolApp.Application.Common.Exceptions;
using EnrolApp.Application.Common.Interfaces;
using EnrolApp.Application.Common.Wrappers;
using EnrolApp.Application.Features.Clients.Specifications;
using EnrolApp.Application.Features.MenuAlimentacion.Interfaces;
using EnrolApp.Domain.Dto;
using EnrolApp.Domain.Entities.Common;
using EnrolApp.Domain.Entities.MenuSemana;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Persistence.Repository.MenuSemana;

public class MenuSemanalService : IMenuSemanal
{
    private readonly ILogger<MenuSemanal> _log;
    private readonly string UrlBaseApiEcommerce = string.Empty;
    private readonly IConfiguration _config;
    private readonly IRepositoryAsync<Cliente> _repoClient;
    private readonly IMapper _mapper;

    public MenuSemanalService(ILogger<MenuSemanal> log, IConfiguration config, IRepositoryAsync<Cliente> repoClient, IMapper mapper)
    {
        _log = log;
        _config = config;
        _repoClient = repoClient;
        _mapper = mapper;
        UrlBaseApiEcommerce = _config.GetSection("ConsumoApis:UrlBaseApiBuenDia").Get<string>();
    }

    public async Task<ResponseType<MenuSemanal>> GetMenuSemanaAsync(string identificacion, string codOrganizacion,string token, CancellationToken cancellationToken)
    {
        var nombreEnpoint = _config.GetSection("EndPointConsumoApis:ApiBuenDia:GetMenuSemanal").Get<string>();
        var uriEndPoint = UrlBaseApiEcommerce + nombreEnpoint;
        try
        {
            var objCliente = await _repoClient.FirstOrDefaultAsync(new ClienteByIdentificacionSpec(identificacion), cancellationToken);
            var dateNow = new DateTime(2022,06,05);
            //System.Globalization.CultureInfo norwCulture =
            //System.Globalization.CultureInfo.CreateSpecificCulture("es");
            //System.Globalization.Calendar cal = norwCulture.Calendar;
            //int diaSemana = cal.GetWeekOfYear(dateNow,
            //norwCulture.DateTimeFormat.CalendarWeekRule,
            //norwCulture.DateTimeFormat.FirstDayOfWeek);

            System.Globalization.CultureInfo myCI = new System.Globalization.CultureInfo("es-EC");
            System.Globalization.Calendar myCal = myCI.Calendar;
            var diaSemana = myCal.GetWeekOfYear(dateNow, myCI.DateTimeFormat.CalendarWeekRule, myCI.DateTimeFormat.FirstDayOfWeek);


            var menudia = "CodEmpleado=16042&esColaboradorInterno=0&fecIni="+ dateNow.AddDays(-30).Date.ToString("yyyy-MM-dd") + "&fecFin="+ dateNow.AddDays(7).Date.ToString("yyyy-MM-dd") + "&tipoServicio=2";

            using var client = new HttpClient();
            
            var builder = new UriBuilder(uriEndPoint);
            builder.Query = menudia;
            var url = builder.ToString();

            client.DefaultRequestHeaders.Add("AuthToken", token ?? string.Empty);
            AgregarRequestHeaders(client);
            var response= await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                List<Alimentos> alimentos = new();
                List<MenuAlimentos> menuAlimentos = new();
                List<MenuAlimentos> menuAlimentosAnterior = new();
                var local = response.Content.ReadAsStringAsync();
                var resulTask = response.Content.ReadFromJsonAsync<List<ResponseMenuSemana>>().Result;
                if (resulTask.Any())
                {
                    var objMenuHoy = resulTask.Where(x => x.menu_fecha.Date.Date == dateNow.Date).ToList();
                    var objSemana = resulTask.Where(x => x.menu_semana == diaSemana).OrderByDescending(x => x.menu_fecha.Date).ToList();
                    var objSemanaAnterior = resulTask.Where(x => x.menu_semana == (diaSemana-2)).OrderByDescending(x => x.menu_fecha.Date).ToList();
                    foreach (var item in objMenuHoy)
                    {
                        alimentos.Add(new Alimentos()
                        {
                            ReceCodigo = item.rece_codigo,
                            ReceNombre = item.rece_nombre,
                            TireCodigo = item.tire_tipo,
                            TireDescripcion = item.TipoReceta,
                            TiseCodigo = item.tise_codigo,
                            TiseDescripcion = item.TipoServicio
                        });
                    }
                    foreach (var item in objSemana)
                    {
                        List<Alimentos> alimentosSemana = new();
                        var alimentosSem = objSemana.Where(x => x.menu_fecha.Date.Date == item.menu_fecha.Date).ToList();
                        foreach (var itemAlimentoSem in alimentosSem)
                        {
                            alimentosSemana.Add(new Alimentos()
                            {
                                ReceCodigo = itemAlimentoSem.rece_codigo,
                                ReceNombre = itemAlimentoSem.rece_nombre,
                                TireCodigo = itemAlimentoSem.tire_tipo,
                                TireDescripcion = itemAlimentoSem.TipoReceta,
                                TiseCodigo = itemAlimentoSem.tise_codigo,
                                TiseDescripcion = itemAlimentoSem.TipoServicio
                            });
                        }
                        var existFecha = menuAlimentos.Where(x => x.MenuFecha == item.menu_fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList();
                        if (!existFecha.Any())
                        {
                            menuAlimentos.Add(new MenuAlimentos()
                            {
                                MenuCodigo = item.menu_codigo,
                                MenuFecha = item.menu_fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                                MenuAnio = item.menu_año,
                                MenuSemana = item.menu_semana,
                                NumeroDiaSemana = item.NumDia,
                                Menu = alimentosSemana

                            });
                        }
                    }


                    foreach (var item in objSemanaAnterior)
                    {
                        List<Alimentos> alimentosSemanaAnt = new();
                        var alimentosSem = objSemanaAnterior.Where(x => x.menu_fecha.Date.Date == item.menu_fecha.Date).ToList();
                        foreach (var itemAlimentoSem in alimentosSem)
                        {
                            alimentosSemanaAnt.Add(new Alimentos()
                            {
                                ReceCodigo = itemAlimentoSem.rece_codigo,
                                ReceNombre = itemAlimentoSem.rece_nombre,
                                TireCodigo = itemAlimentoSem.tire_tipo,
                                TireDescripcion = itemAlimentoSem.TipoReceta,
                                TiseCodigo = itemAlimentoSem.tise_codigo,
                                TiseDescripcion = itemAlimentoSem.TipoServicio
                            });
                        }
                        var existFecha = menuAlimentosAnterior.Where(x => x.MenuFecha == item.menu_fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList();
                        if (!existFecha.Any())
                        {
                            menuAlimentosAnterior.Add(new MenuAlimentos()
                            {
                                MenuCodigo = item.menu_codigo,
                                MenuFecha = item.menu_fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                                MenuAnio = item.menu_año,
                                MenuSemana = item.menu_semana,
                                NumeroDiaSemana = item.NumDia,
                                Menu = alimentosSemanaAnt

                            });
                        }
                    }

                    var hSInicio = objSemana.OrderBy(x => x.menu_fecha).FirstOrDefault() == null ? null : objSemana.OrderByDescending(x => x.menu_fecha).FirstOrDefault().menu_fecha.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    var hSFin = objSemana.OrderByDescending(x => x.menu_fecha).FirstOrDefault() == null ? null : objSemana.OrderBy(x => x.menu_fecha).FirstOrDefault().menu_fecha.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    var hgSAInicio = objSemanaAnterior.OrderBy(x => x.menu_fecha).FirstOrDefault() == null ? null : objSemanaAnterior.OrderByDescending(x => x.menu_fecha).FirstOrDefault().menu_fecha.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    var hgSAFin = objSemanaAnterior.OrderByDescending(x => x.menu_fecha).FirstOrDefault() == null ? null : objSemanaAnterior.OrderBy(x => x.menu_fecha).FirstOrDefault().menu_fecha.Date.ToString("yyyy-MM-dd HH:mm:ss");

                    MenuSemanal objmenuSemanal = new()
                    {
                        OrgaCodigo = objMenuHoy.ElementAt(0).orga_codigo,
                        PlanCodigo = objMenuHoy.ElementAt(0).plan_codigo,
                        MenuHoy = new MenuAlimentos
                        {
                            MenuCodigo = objMenuHoy.ElementAt(0).menu_codigo,
                            MenuFecha = objMenuHoy.ElementAt(0).menu_fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                            MenuAnio = objMenuHoy.ElementAt(0).menu_año,
                            MenuSemana = objMenuHoy.ElementAt(0).menu_semana,
                            NumeroDiaSemana = objMenuHoy.ElementAt(0).NumDia,
                            Menu = alimentos,
                            
                        },
                        MenuSemana = new SemanaAlimentos
                        {
                            MenuFechaIni = hSInicio,
                            MenuFechaFin = hSFin,
                            Dias = menuAlimentos
                        },
                        MenuSemanaAnterior = new SemanaAlimentos
                        {
                            MenuFechaIni = hgSAInicio,
                            MenuFechaFin = hgSAFin,
                            Dias = menuAlimentosAnterior
                        }
                    };

                    return new ResponseType<MenuSemanal>() { Data = objmenuSemanal, Message = CodeMessageResponse.GetMessageByCode("000"), StatusCode = "000", Succeeded = true };

                }
                return new ResponseType<MenuSemanal>() { Message = CodeMessageResponse.GetMessageByCode("001"), StatusCode = "001", Succeeded = true };

            }
            return new ResponseType<MenuSemanal>() { Message = "No se pudo conectar al servidor Sebeli", StatusCode = "002", Succeeded = false};
        }
        catch (Exception ex)
        {
            _log.LogError(ex, string.Empty);
            return new ResponseType<MenuSemanal>() { Succeeded = false, StatusCode = "002", Message = CodeMessageResponse.GetMessageByCode("002") };
        }
    }

    private void AgregarRequestHeaders(HttpClient client)
    {
        if (client is not null)
        {
            client.DefaultRequestHeaders.Add("CurrentOrganizationId", "1");
            client.DefaultRequestHeaders.Add("AuthTypeToken", "XAMARIN");
            client.DefaultRequestHeaders.Add("ClientId", "inventory-app");
            client.DefaultRequestHeaders.Add("ClientSecret", "sic-inventory");
        }
    }

}

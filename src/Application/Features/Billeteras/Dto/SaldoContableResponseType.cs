using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Billeteras.Dto;

public class SaldoContableResponseType
{
    [JsonPropertyName("saldoDisponible")]
    public decimal SaldoDisponible { get; set; }

    [JsonPropertyName("valorContable")]
    public decimal ValorContable { get; set; }

    [JsonPropertyName("ahorroProgramado")]
    public decimal AhorroProgramado { get; set; }

    [JsonPropertyName("saldoTotal")]
    public decimal SaldoTotal { get; set; }

    [JsonPropertyName("porcentajeSaldo")]
    public decimal PorcentajeSaldo { get; set; }
}

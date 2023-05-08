using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Wallets.Dto;

public class CupoCreditoResponseType
{
    [JsonIgnore]
    public int IdEmpresa { get; set; }

    [JsonIgnore]
    public int IdCliente { get; set; }

    [JsonIgnore]
    public int IdTipoIdentificacion { get; set; }

    [JsonIgnore]
    public string Identificacion { get; set; }

    [JsonIgnore]
    public string Descripcion { get; set; }

    [JsonIgnore]
    public string RazonSocial { get; set; }

    [JsonIgnore]
    public string CorreoElectronico { get; set; }

    [JsonIgnore]
    public object IdListaPrecio { get; set; }

    [JsonPropertyName("cupoAutorizado")]
    public decimal CreditoCupo { get; set; }

    [JsonPropertyName("saldoDisponible")]
    public decimal CreditoDisponible { get; set; }

    [JsonIgnore]
    public object ListaPrecio { get; set; }

    [JsonIgnore]
    public bool EsMayorista { get; set; }

    [JsonIgnore]
    public decimal Deuda { get; set; }

    [JsonIgnore]
    public decimal ExtraCupo { get; set; }

    [JsonIgnore]
    public int FacturasPendiente { get; set; }

    [JsonIgnore]
    public decimal? DeudaCorriente { get; set; }

    [JsonIgnore]
    public decimal? DeudaVencida { get; set; }

    [JsonIgnore]
    public string EstadoCrediticio { get; set; }

    [JsonIgnore]
    public bool PoseeAutorizacion { get; set; }

    [JsonIgnore]
    public DateTime? FechaUltimoPago { get; set; }

    [JsonIgnore]
    public int PlazoPermitido { get; set; }

    [JsonIgnore]
    public int DividendosPermitidos { get; set; }

    [JsonIgnore]
    public string Direccion { get; set; }

    [JsonIgnore]
    public string Empresa { get; set; }

    [JsonIgnore]
    public string Telefono { get; set; }

    [JsonIgnore]
    public string Password { get; set; }

    [JsonIgnore]
    public string ConfirmarPassword { get; set; }

    [JsonIgnore]
    public string IdUsuario { get; set; }

    [JsonIgnore]
    public int IdAlianza { get; set; }

    [JsonIgnore]
    public string Alianza { get; set; }

    [JsonIgnore]
    public object GeneroTabla { get; set; }

    [JsonIgnore]
    public string Genero { get; set; }

    [JsonIgnore]
    public string GeneroDescripcion { get; set; }

    [JsonIgnore]
    public DateTime FechaNacimiento { get; set; }

    [JsonIgnore]
    public string TipoIdentificacion { get; set; }

    [JsonIgnore]
    public object ClienteDireccionDescripcion { get; set; }

    [JsonIgnore]
    public object ClienteDireccionDireccion { get; set; }

    [JsonIgnore]
    public object ClienteDireccionLatitud { get; set; }

    [JsonIgnore]
    public object ClienteDireccionLongitud { get; set; }

    [JsonIgnore]
    public DateTime? FechaUltimaTransaccion { get; set; }

    [JsonIgnore]
    public bool Activo { get; set; }

    [JsonIgnore]
    public int IdArea { get; set; }

    [JsonIgnore]
    public string Area { get; set; }

    [JsonIgnore]
    public double Latitud { get; set; }

    [JsonIgnore]
    public double Longitud { get; set; }

    [JsonIgnore]
    public string EtiquetaMarker { get; set; }

    [JsonIgnore]
    public object Direcciones { get; set; }

    [JsonIgnore]
    public object ListaPrecios { get; set; }

    [JsonIgnore]
    public int Color { get; set; }

    [JsonIgnore]
    public int MarkerIndex { get; set; }

    [JsonIgnore]
    public int MarkerZIndex { get; set; }

    [JsonIgnore]
    public int MarkerStart { get; set; }

    [JsonIgnore]
    public int IdUbicacion { get; set; }

    [JsonIgnore]
    public string Titulo { get; set; }

    [JsonIgnore]
    public long FechaNacimientoTicks { get; set; }

    [JsonPropertyName("porcentajeSaldo")]
    public decimal PorcentajeSaldo { get; set; }

    [JsonPropertyName("saldoUtilizado")]
    public decimal SaldoUtilizado { get; set; }

    [JsonPropertyName("fechaCorte")]
    public DateTime? FechaCorte { get; set; } = null;

    [JsonPropertyName("fechaPago")]
    public DateTime? FechaPago { get; set; } = null;

    [JsonPropertyName("saldoAnterior")]
    public decimal SaldoAnterior { get; set; } = 0;

    [JsonPropertyName("consumoDebito")]
    public decimal ConsumoDebito { get; set; }

    [JsonPropertyName("creditoPago")]
    public decimal CreditoPago { get; set; } = 0;

    [JsonPropertyName("saldoPagar")]
    public decimal SaldoPagar { get; set; }




}

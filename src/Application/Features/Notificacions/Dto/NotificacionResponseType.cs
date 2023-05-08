
using System.Text.Json.Serialization;
namespace EnrolApp.Application.Features.Notifications.Dto;

public class NotificacionResponseType
{

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("featureId")]
    public Guid FeatureId { get; set; }

    [JsonPropertyName("solicitudId")]
    public Guid? SolicitudId { get; set; }

    [JsonPropertyName("tipoSolicitud")]
    public string TipoSolicitud { get; set; }

    [JsonPropertyName("mensajeNotificacion")]
    public string MensajeNotificacion { get; set; }

    [JsonPropertyName("mensajeNotificacionHtml")]
    public string MensajeNotificacionHtml { get; set; }

    [JsonPropertyName("resumen")]
    public string Resumen { get; set; }

    [JsonPropertyName("uriImageNotifacion")]
    public string UriImageNotifacion { get; set; }

    [JsonPropertyName("fechaCreacion")]
    public DateTime FechaCreacion { get; set; }

    [JsonPropertyName("estadoLeido")]
    public string EstadoLeido { get; set; }

    [JsonPropertyName("descripClasificacion")]
    public string DescripClasificacion { get; set; }

    [JsonPropertyName("codigoClasificacion")]
    public string CodigoClasificacion { get; set; }

    [JsonPropertyName("uriImageClasificacion")]
    public string UriImageClasificacion { get; set; } = string.Empty;

    [JsonPropertyName("requiereAccion")]
    public bool RequiereAccion { get; set; } = false;

    [JsonPropertyName("relevante")]
    public string Relevante { get; set; } = "N";

    [JsonPropertyName("requiereNivelDetalle")]
    public bool RequiereNivelDetalle { get; set; } = false;

    [JsonPropertyName("ordenClasificacion")]
    public int OrdenClasificacion { get; set; }

    [JsonPropertyName("idTransaccion")]
    public string IdTransaccion { get; set; }

    [JsonPropertyName("idPuntoOperacion")]
    public string IdPuntoOperacion { get; set; }
}

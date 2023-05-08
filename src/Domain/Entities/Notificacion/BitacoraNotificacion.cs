
using EnrolApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Notificacion;

[Table("NT_BitacoraNotificacion", Schema = "dbo")]
public class BitacoraNotificacion
{
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; }

    [Column("eventoDifusionId", Order = 1, TypeName = "uniqueidentifier")]
    public Guid EventoDifusionId { get; set; }
    public virtual EventoDifusion EventoDifusion { get; set; }

    [Column("clienteId", Order = 2, TypeName = "uniqueidentifier")]
    public Guid ClienteId { get; set; }
    public virtual Cliente Cliente { get; set; }

    [Column("solicitudId", Order = 3, TypeName = "uniqueidentifier")]
    public Guid? SolicitudId { get; set; }
    //public virtual Solicitud Solicitud { get; set; }

    [Column("referenciaClienteId", Order = 4, TypeName = "uniqueidentifier")]
    public Guid? ReferenciaClienteId { get; set; }

    [Column("mensaje", Order = 5, TypeName = "varchar")]
    [StringLength(800)]
    public string Mensaje { get; set; }

    [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; }

    [Column("fechaActualizacion", Order = 7, TypeName = "datetime2")]
    public DateTime? FechaActualizacion { get; set; }

    [Column("estadoLeido", Order = 8, TypeName = "varchar")]
    [MaxLength(1)]
    public string EstadoLeido { get; set; }

    [Column("estado", Order = 9, TypeName = "varchar")]
    [MaxLength(1)]
    public string Estado { get; set; }

    [Column("tipoSolicitud", Order = 10, TypeName = "varchar")]
    public string TipoSolicitud { get; set; }

    [Column("mensajeHtml", Order = 11, TypeName = "varchar")]
    [StringLength(800)]
    public string MensajeHtml { get; set; }

    [Column("identificacion", Order = 12, TypeName = "varchar")]
    [StringLength(20)]
    public string Identificacion { get; set; }

    [Column("resumen", Order = 13, TypeName = "varchar")]
    [StringLength(500)]
    public string Resumen { get; set; }

    [Column("idTransaccion", Order = 14, TypeName = "varchar")]
    public string IdTransaccion { get; set; }

    [Column("idPuntoOperacion", Order = 15, TypeName = "varchar")]
    public string IdPuntoOperacion { get; set; }





}


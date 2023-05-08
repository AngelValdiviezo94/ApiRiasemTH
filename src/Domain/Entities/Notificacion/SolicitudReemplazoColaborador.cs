using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Notificacion
{
    [Table("AS_SolicitudReemplazoColaborador", Schema = "dbo")]
    public class SolicitudReemplazoColaborador
    {

        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("featureId", Order = 1, TypeName = "uniqueidentifier")]
        public Guid FeatureId { get; set; }

        [Column("solicitudId", Order = 2, TypeName = "uniqueidentifier")]
        public Guid SolicitudId { get; set; }

        [Column("identificacionColaborador", Order = 3, TypeName = "varchar")]
        public string IdentificacionColaborador { get; set; }

        [Column("identificacionReemplazo", Order = 4, TypeName = "varchar")]
        public string IdentificacionReemplazo { get; set; }

        [Column("fechaDesde", Order = 5, TypeName = "datetime2")]
        public DateTime FechaDesde { get; set; }

        [Column("fechaHasta", Order = 6, TypeName = "datetime2")]
        public DateTime FechaHasta { get; set; }

        [Column("estado", Order = 7, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 8, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [Column("fechaCreacion", Order = 9, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("usuarioModificacion", Order = 10, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; } = string.Empty;

        [Column("fechaModificacion", Order = 11, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }
    }
}

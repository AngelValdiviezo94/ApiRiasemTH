using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Marketing
{
    [Table("MK_RolContenido", Schema = "dbo")]
    public class RolContenidoMK
    {
        [Key]
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("rolId", Order = 1, TypeName = "uniqueidentifier")]
        public Guid RolId { get; set; }

        [Column("contenidoId", Order = 2, TypeName = "uniqueidentifier")]
        public Guid ContenidoMKId { get; set; }
        public virtual ContenidoMK ContenidoMK { get; set; }

        [Column("estado", Order = 3, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 4, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 5, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 6, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 7, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }
    }
}
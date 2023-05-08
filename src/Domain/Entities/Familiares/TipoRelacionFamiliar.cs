using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Familiares
{
    [Table("CL_TipoRelacionFamiliar", Schema = "dbo")]

    public class TipoRelacionFamiliar
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("nombre", Order = 1, TypeName = "varchar")]
        public string Nombre { get; set; }

        [Column("estado", Order = 2, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 3, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 4, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 5, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 6, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }

    }
}

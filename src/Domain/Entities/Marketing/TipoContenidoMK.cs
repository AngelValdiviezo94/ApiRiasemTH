using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Marketing
{
    [Table("MK_TipoContenido", Schema = "dbo")]
    public class TipoContenidoMK
    {
        [Key]
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("codigo", Order = 1, TypeName = "varchar")]
        public string Codigo { get; set; }

        [Column("descripcion", Order = 2, TypeName = "varchar")]
        public string Descripcion { get; set; }

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
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Marketing
{
    [Table("MK_ContenidoCategoria", Schema = "dbo")]
    public class ContenidoCategoriaMK
    {
        [Key]
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("contenidoId", Order = 1, TypeName = "uniqueidentifier")]
        public Guid ContenidoId { get; set; }
        public virtual ContenidoMK Contenido { get; set; }

        [Column("categoriaId", Order = 2, TypeName = "uniqueidentifier")]
        public Guid CategoriaId { get; set; }
        public virtual CategoriaMK Categoria { get; set; }

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
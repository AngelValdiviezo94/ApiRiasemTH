using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Marketing
{
    [Table("MK_Contenido", Schema = "dbo")]
    public class ContenidoMK
    {
        [Key]
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("tipoContenidoId", Order = 1, TypeName = "uniqueidentifier")]
        public Guid TipoContenidoId { get; set; }
        public virtual TipoContenidoMK TipoContenido { get; set; }

        [Column("portadaUrl", Order = 2, TypeName = "nvarchar")]
        public string PortadaUrl { get; set; }

        [Column("posterUrl", Order = 3, TypeName = "nvarchar")]
        public string PosterUrl { get; set; }

        [Column("contenidoUrl", Order = 4, TypeName = "nvarchar")]
        public string ContenidoUrl { get; set; }

        [Column("nombreCorto", Order = 5, TypeName = "varchar")]
        public string NombreCorto { get; set; }

        [Column("nombreLargo", Order = 6, TypeName = "varchar")]
        public string NombreLargo { get; set; }

        [Column("orden", Order = 7, TypeName = "int")]
        public int Orden { get; set; }

        [Column("fechaPublicacion", Order = 8, TypeName = "datetime2")]
        public DateTime FechaPublicacion { get; set; }

        [Column("fechaCaducidad", Order = 9, TypeName = "datetime2")]
        public DateTime FechaCaducidad { get; set; }

        [Column("fechaVigenciaPortada", Order = 10, TypeName = "datetime2")]
        public DateTime FechaVigenciaPortada { get; set; }

        [Column("descripcion", Order = 11, TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Column("comentario", Order = 12, TypeName = "varchar")]
        public string Comentario { get; set; }

        [Column("estado", Order = 13, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 14, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 15, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 16, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 17, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }
    }
}
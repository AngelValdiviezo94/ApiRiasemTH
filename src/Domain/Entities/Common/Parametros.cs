using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Common
{
    [Table("GN_Parametros", Schema = "dbo")]
    public class Parametros
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("codigo", Order = 1, TypeName = "varchar")]
        public string Codigo { get; set; }

        [Column("secuencia", Order = 2, TypeName = "int")]
        public int Secuencia { get; set; }

        [Column("nombre", Order = 3, TypeName = "varchar")]
        public string Nombre { get; set; }

        [Column("valor", Order = 4, TypeName = "nvarchar")]
        public string Valor { get; set; }

        [Column("descripcion", Order = 5, TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Column("estado", Order = 6, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 7, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; } = string.Empty;

        [Column("fechaCreacion", Order = 8, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Column("usuarioModificacion", Order = 9, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; } = string.Empty;

        [Column("fechaModificacion", Order = 10, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }
    }
}

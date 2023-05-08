using EnrolApp.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Familiares
{
    [Table("CL_FamiliarColaborador", Schema = "dbo")]
    public class FamiliarColaborador
    {
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

		[Column("tipoRelacionFamiliarId", Order = 1, TypeName = "uniqueidentifier")]
		public Guid TipoRelacionFamiliarId { get; set; }
		public virtual TipoRelacionFamiliar TipoRelacionFamiliar { get; set; }

		[Column("colaboradorId", Order = 2, TypeName = "uniqueidentifier")]
		public Guid ColaboradorId { get; set; }
		public virtual Cliente Colaborador { get; set; }

		[Column("tipoIdentificacion", Order = 3, TypeName = "varchar")]
		public string TipoIdentificacion { get; set; }

		[Column("identificacion", Order = 4, TypeName = "varchar")]
		public string Identificacion { get; set; }

		[Column("nombres", Order = 5, TypeName = "varchar")]
		public string Nombres { get; set; }

		[Column("apellidos", Order = 6, TypeName = "varchar")] 
		public string Apellidos { get; set; }

		[Column("alias", Order = 7, TypeName = "varchar")]
		public string Alias { get; set; }

		[Column("latitud", Order = 8, TypeName = "float")]
		public float? Latitud { get; set; }

		[Column("longitud", Order = 9, TypeName = "float")]
		public double? Longitud { get; set; }

		[Column("direccion", Order = 10, TypeName = "varchar")]
		public string Direccion { get; set; }

		[Column("celular", Order = 11, TypeName = "varchar")]
		public string Celular { get; set; }

		[Column("correo", Order = 12, TypeName = "varchar")]
		public string Correo { get; set; }

		[Column("fechaNacimiento", Order = 13, TypeName = "datetime")]
		public DateTime? FechaNacimiento { get; set; }

		[Column("genero", Order = 14, TypeName = "varchar")]
		public string Genero { get; set; }

		[Column("servicioActivo", Order = 15, TypeName = "bit")]
		public bool ServicioActivo { get; set; }

		[Column("estado", Order = 16, TypeName = "varchar")]
		public string Estado { get; set; }

		[Column("fechaDesde", Order = 17, TypeName = "datetime")]
		public DateTime FechaDesde { get; set; }

		[Column("fechaHasta", Order = 18, TypeName = "datetime")]
		public DateTime? FechaHasta { get; set; }

		[Column("habilitado", Order = 19, TypeName = "bit")]
		public bool Habilitado { get; set; }

		[Column("eliminado", Order = 20, TypeName = "bit")]
		public bool Eliminado { get; set; }

		[Column("cupo", Order = 21, TypeName = "float")]
		public double Cupo { get; set; }

		[Column("dispositivoId", Order = 22, TypeName = "varchar")]
		public string DispositivoId { get; set; }

		[Column("imagenPerfilId", Order = 23, TypeName = "uniqueidentifier")]
		public Guid? ImagenPerfilId { get; set; }
		public virtual Adjunto ImagenPerfil { get; set; }

		[Column("usuarioCreacion", Order = 24, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 25, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 26, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 27, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }

        [Column("sesionColaborador", Order = 28, TypeName = "varchar")]
        public string SesionColaborador { get; set; }
    }
}

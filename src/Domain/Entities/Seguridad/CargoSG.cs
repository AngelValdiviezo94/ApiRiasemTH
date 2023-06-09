﻿using EnrolApp.Domain.Entities.Organizacion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Seguridad
{
    [Table("SG_Cargo", Schema = "dbo")]
    public class CargoSG
    {
        [Key]
        [Column("id", Order = 0, TypeName = "uniqueidentifier")]
        public Guid Id { get; set; }

        [Column("departamentoId", Order = 1, TypeName = "uniqueidentifier")]
        public Guid DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }

        [Column("nombre", Order = 2, TypeName = "varchar")]
        public string Nombre { get; set; }

        [Column("descripcion", Order = 3, TypeName = "varchar")]
        public string Descripcion { get; set; }

        [Column("codigoHomologacion", Order = 4, TypeName = "varchar")]
        public string CodigoHomologacion { get; set; }

        [Column("estado", Order = 5, TypeName = "varchar")]
        public string Estado { get; set; }

        [Column("usuarioCreacion", Order = 6, TypeName = "varchar")]
        public string UsuarioCreacion { get; set; }

        [Column("fechaCreacion", Order = 7, TypeName = "datetime2")]
        public DateTime FechaCreacion { get; set; }

        [Column("usuarioModificacion", Order = 8, TypeName = "varchar")]
        public string UsuarioModificacion { get; set; }

        [Column("fechaModificacion", Order = 9, TypeName = "datetime2")]
        public DateTime? FechaModificacion { get; set; }

        public virtual ICollection<CargoEje> CargoEje { get; set; }
    }
}

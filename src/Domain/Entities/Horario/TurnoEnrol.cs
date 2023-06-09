﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EnrolApp.Domain.Entities.Horario;

[Table("AS_TurnoColaborador", Schema = "dbo")]
public class TurnoEnrol
{

    [Key]
    [Column("id", Order = 0, TypeName = "uniqueidentifier")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("idTurno", Order = 1, TypeName = "uniqueidentifier")]
    public Guid IdTurno { get; set; }
    public virtual Turno Turno { get; set; }

    [Column("idColaborador", Order = 2, TypeName = "uniqueidentifier")]
    public Guid IdColaborador { get; set; }

    [Column("fechaAsginacion", Order = 3, TypeName = "datetime")]
    public DateTime FechaAsignacion { get; set; }

    [Column("estado", Order = 4, TypeName = "varchar")]
    public string Estado { get; set; } = "A";



    //AUDITORIA
    [Column("usuarioCreacion", Order = 5, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioCreacion { get; set; } = string.Empty;

    [Column("fechaCreacion", Order = 6, TypeName = "datetime2")]
    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Column("usuarioModificacion", Order = 7, TypeName = "varchar")]
    [StringLength(20)] public string UsuarioModificacion { get; set; } = string.Empty;

    [Column("fechaModificacion", Order = 8, TypeName = "datetime2")]
    public DateTime? FechaModificacion { get; set; }
}

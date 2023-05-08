using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Vacaciones;

[Table("AS_SolicitudVacacion", Schema = "dbo")]
public class SolicitudVacacion
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("codOrganizacion")]
    public int? CodOrganizacion { get; set; }

    [NotMapped]
    //public List<PeriodoVacacion> PeriodoAfectacion { get; set; }

    [Column("idTipoSolicitud")]
    public Guid IdTipoSolicitud { get; set; }
    //public virtual TipoPermiso TipoPermiso { get; set; }

    [Column("idEstadoSolicitud")]
    public Guid IdEstadoSolicitud { get; set; }
    //public virtual EstadoTarea EstadoTarea { get; set; }

    [Column("numeroSolicitud")]
    public int NumeroSolicitud { get; set; }

    [Column("idSolicitante")]
    public int IdSolicitante { get; set; }

    [Column("idBeneficiario")]
    public int IdBeneficiario { get; set; }

    [Column("identificacionEmpleado")]
    public string IdentificacionEmpleado { get; set; }

    [Column("fechaDesde")]
    public DateTime FechaDesde { get; set; }

    [Column("fechaHasta")]
    public DateTime FechaHasta { get; set; }

    [Column("cantidadDias")]
    public int CantidadDias { get; set; }

    [Column("codigoEmpleadoReemplazo")]
    public string CodigoEmpleadoReemplazo { get; set; }

    [Column("observacion")]
    public string Observacion { get; set; }

    [Column("fechaCreacion")]
    public DateTime FechaCreacion { get; set; }

    [Column("aplicaDescuento")]
    public bool AplicaDescuento { get; set; }

    [Column("periodoOrigen")]
    public int? PeriodoOrigen { get; set; }

    [Column("diasVacacionesOrigen")]
    public int? DiasVacacionesOrigen { get; set; }

    [Column("diasTomadosOrigen")]
    public int? DiasTomadosOrigen { get; set; }



}

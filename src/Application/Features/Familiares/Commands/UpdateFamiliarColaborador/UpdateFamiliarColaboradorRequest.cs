namespace EnrolApp.Application.Features.Familiares.Commands.UpdateFamiliarColaborador
{
    public class UpdateFamiliarColaboradorRequest
    {
        public Guid Id { get; set; }
        public Guid ColaboradorId { get; set; }
        public string IdentificacionColaborador { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Alias { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public bool Habilitado { get; set; }
        public bool Eliminado { get; set; }
        public double Cupo { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string TipoRelacionFamiliarId { get; set; }
        public string SesionColaborador { get; set; }
    }
}

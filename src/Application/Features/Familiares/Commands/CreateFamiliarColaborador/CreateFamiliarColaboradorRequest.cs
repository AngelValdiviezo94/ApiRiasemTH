namespace EnrolApp.Application.Features.Familiares.Commands.CreateFamiliarColaborador
{
    public class CreateFamiliarColaboradorRequest
    {
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
        public double Cupo { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public Guid TipoRelacionFamiliarId { get; set; }
    }
}

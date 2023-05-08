namespace EnrolApp.Application.Features.Clients.Commands.UpdateInfoColaborador
{
    public class UpdateInfoColaboradorRequest
    {
        public Guid IdColaborador { get; set; }
        public string Identificacion { get; set; }
        public string IdJefe { get; set; }
        public List<Guid> LstLocalidad { get; set; }
        public Guid LocalidadPrincipal { get; set; }
        public string IdColaboradorReemplazo { get; set; }
        public string IdentificacionModifica { get; set; }
    }
}

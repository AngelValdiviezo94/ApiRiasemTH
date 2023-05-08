namespace EnrolApp.Application.Features.Clients.Commands.UpdateContrasenaColaborador
{
    public class UpdateContrasenaColaboradorRequest
    {
        public string Identificacion { get; set; }
        public string ContrasenaAnterior { get; set; }
        public string ContrasenaNueva { get; set; }
        public string TipoColaborador { get; set; }
    }
}
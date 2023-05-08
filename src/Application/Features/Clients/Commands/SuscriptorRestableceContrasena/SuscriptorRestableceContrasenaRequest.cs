namespace EnrolApp.Application.Features.Clients.Commands.SuscriptorRestableceContrasena
{
    public class SuscriptorRestableceContrasenaRequest
    {
        public string Identificacion { get; set; }
        public string Password { get; set; }
        public string TipoColaborador { get; set; }
        //public string Otp { get; set; }
    }
}

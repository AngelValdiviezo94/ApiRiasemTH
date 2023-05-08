using EnrolApp.Domain.Entities.Horario;

namespace EnrolApp.Application.Features.Clients.Dto
{
    public class ListadoColaboradoresType
    {
        public Guid Id { get; set; }
        public string CodUdn { get; set; }
        public string Udn { get; set; }
        public string CodArea { get; set; }
        public string Area { get; set; }
        public string CodScc { get; set; }
        public string Scc { get; set; }
        public string Colaborador { get; set; }
        public string Cedula { get; set; }
        public string Codigo { get; set; }
        public string Cargo { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public List<Localidad> LstLocalidad { get; set; }
        public  Guid? IdJefe { get; set; }
        public string Jefe { get; set; }
        public Guid? IdReemplazo { get; set; }
        public string Reemplazo { get; set; }
        public string FotoPerfil { get; set; }
        public Guid? FacialPersonId { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }
        public ImagenPerfilAdjunto Adjunto { get; set; }
    }

    public class ImagenPerfilAdjunto
    {
        public string Base64 { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Extension { get; set; } = string.Empty;
    }

}
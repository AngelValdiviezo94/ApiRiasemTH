namespace EnrolApp.Application.Features.Banner.Dto
{
    public class ListadoBannerType
    {
        public Guid Id { get; set; }
        public Guid TipoContenidoId { get; set; }
        public string TipoContenido { get; set; }
        public string PortadaUrl { get; set; }
        public string PosterUrl { get; set; }
        public string ContenidoUrl { get; set; }
        public string NombreCorto { get; set; }
        public string NombreLargo { get; set; }
        public int? Orden { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public DateTime? FechaVigenciaPortada { get; set; }
        public string Descripcion { get; set; }
        public string Comentario { get; set; }
        public List<ListadoCategoria> ListadoCategoria { get; set; } = new();
    }

    public class ListadoCategoria
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
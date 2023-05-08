using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Application.Features.Clients.Dto
{
    public partial class ClienteSebeli
    {
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        //public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        //public string Descripcion { get; set; }
        //public string RazonSocial { get; set; }
        //public string CorreoElectronico { get; set; }
        //public string IdListaPrecio { get; set; }
        //public float CreditoCupo { get; set; }
        //public float CreditoDisponible { get; set; }
        //public string ListaPrecio { get; set; }
        //public bool EsMayorista { get; set; }
        //public float Deuda { get; set; }
        //public float ExtraCupo { get; set; }
        //public int FacturasPendiente { get; set; }
        //public string DeudaCorriente { get; set; }
        //public string DeudaVencida { get; set; }
        //public string EstadoCrediticio { get; set; }
        //public bool PoseeAutorizacion { get; set; }
        //public string FechaUltimoPago { get; set; }
        //public int PlazoPermitido { get; set; }
        //public int DividendosPermitidos { get; set; }
        public string Direccion { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string ConfirmarPassword { get; set; }
        public string IdUsuario { get; set; }
        public string IdAlianza { get; set; }
        public string Alianza { get; set; }
        public string GeneroTabla { get; set; }
        public string Genero { get; set; }
        public string GeneroDescripcion { get; set; }
        //public DateTime? FechaNacimiento { get; set; }
        public string TipoIdentificacion { get; set; }
        public string ClienteDireccionDescripcion { get; set; }
        public string ClienteDireccionDireccion { get; set; }
        public string ClienteDireccionLatitud { get; set; }
        public string ClienteDireccionLongitud { get; set; }
        public string FechaUltimaTransaccion { get; set; }
        public bool Activo { get; set; }
        //public int IdArea { get; set; }
        public string Area { get; set; }
        //public int IdCanal { get; set; }
        public string Canal { get; set; }
        //public float Latitud { get; set; }
        //public float Longitud { get; set; }
        public string EtiquetaMarker { get; set; }
        //public object Direcciones { get; set; }
        //public object ListaPrecios { get; set; }
        //public int Color { get; set; }
        //public int MarkerIndex { get; set; }
        //public int MarkerZIndex { get; set; }
        //public int MarkerStart { get; set; }
        //public int IdUbicacion { get; set; }
        public string Titulo { get; set; }
        //public long FechaNacimientoTicks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolApp.Domain.Entities.Wallet
{
    public class CupoCredito
    {
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string Descripcion { get; set; }
        public string RazonSocial { get; set; }
        public string CorreoElectronico { get; set; }
        public object IdListaPrecio { get; set; }
        public decimal CreditoCupo { get; set; }
        public decimal CreditoDisponible { get; set; }
        public object ListaPrecio { get; set; }
        public bool EsMayorista { get; set; }
        public decimal Deuda { get; set; }
        public decimal ExtraCupo { get; set; }
        public int FacturasPendiente { get; set; }
        public decimal? DeudaCorriente { get; set; }
        public decimal? DeudaVencida { get; set; }
        public string EstadoCrediticio { get; set; }
        public bool PoseeAutorizacion { get; set; }
        public DateTime? FechaUltimoPago { get; set; }
        public int PlazoPermitido { get; set; }
        public int DividendosPermitidos { get; set; }
        public string Direccion { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string ConfirmarPassword { get; set; }
        public string IdUsuario { get; set; }
        public int IdAlianza { get; set; }
        public string Alianza { get; set; }
        public object GeneroTabla { get; set; }
        public string Genero { get; set; }
        public string GeneroDescripcion { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string TipoIdentificacion { get; set; }
        public object ClienteDireccionDescripcion { get; set; }
        public object ClienteDireccionDireccion { get; set; }
        public object ClienteDireccionLatitud { get; set; }
        public object ClienteDireccionLongitud { get; set; }
        public DateTime? FechaUltimaTransaccion { get; set; }
        public bool Activo { get; set; }
        public int IdArea { get; set; }
        public string Area { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string EtiquetaMarker { get; set; }
        public object Direcciones { get; set; }
        public object ListaPrecios { get; set; }
        public int Color { get; set; }
        public int MarkerIndex { get; set; }
        public int MarkerZIndex { get; set; }
        public int MarkerStart { get; set; }
        public int IdUbicacion { get; set; }
        public string Titulo { get; set; }
        public long FechaNacimientoTicks { get; set; }
        //public decimal SaldoDisponible { get; set; }

        //public decimal CupoAutorizado { get; set; }

        //public DateTime FechaCorte { get; set; }

        //public DateTime FechaPago { get; set; }

        //public decimal SaldoUtilizado { get; set; }

        //public decimal SaldoAnterior { get; set; }

        //public decimal ConsumoDebito { get; set; }

        //public decimal CreditoPago { get; set; }

        //public decimal SaldoPagar { get; set; }

        ////public decimal SaldoContable { get; set; }

        ////public decimal AhorroProgramado { get; set; }

        //public decimal PorcentajeSaldo { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolApp.Domain.Entities.Common
{

    [Table("V_COLABORADORES_CONVIVENCIA", Schema = "dbo")]
    public class ColaboradorConvivencia
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("DFiIngreso", Order = 0, TypeName = "datetime")]
        public DateTime DFiIngreso { get; set; }

        [Column("codUdn", Order = 1, TypeName = "varchar")]
        public string CodUdn { get; set; }

        [Column("desUdn", Order = 2, TypeName = "varchar")]
        public string DesUdn { get; set; }

        [Column("codArea", Order = 3, TypeName = "varchar")]
        public string CodArea { get; set; }

        [Column("desArea", Order = 4, TypeName = "varchar")]
        public string DesArea { get; set; }

        [Column("codCentroCosto", Order = 5, TypeName = "varchar")]
        public string CodCentroCosto { get; set; }

        [Column("desCentroCosto", Order = 6, TypeName = "varchar")]
        public string DesCentroCosto { get; set; }

        [Column("codSubcentroCosto", Order = 7, TypeName = "varchar")]
        public string CodSubcentroCosto { get; set; }

        [Column("desSubcentroCosto", Order = 8, TypeName = "varchar")]
        public string DesSubcentroCosto { get; set; }

        [Column("identificacion", Order = 9, TypeName = "varchar")]
        public string Identificacion { get; set; }

        [Column("Empleado", Order = 10, TypeName = "varchar")]
        public string Empleado { get; set; }

        [Column("codigoBiometrico", Order = 11, TypeName = "varchar")]
        public string CodigoBiometrico { get; set; }

        [Column("codCargo", Order = 12, TypeName = "varchar")]
        public string CodCargo { get; set; }

        [Column("desCargo", Order = 13, TypeName = "varchar")]
        public string DesCargo { get; set; }
    }
}
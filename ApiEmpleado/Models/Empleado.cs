using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEmpleado.Models
{
    [Table("Empleado")]
    public class Empleado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [MaxLength(50)]
        public string PApellido{ get; set; } = null!;

        [MaxLength(50)]
        public string SApellido{ get; set; } = null!;
        public int Edad { get; set; }
        public int Telefono { get; set; }

        [ForeignKey("Departamento")]
        public int IdDepartamento { get; set; }

        public Departamento Departamento { get; set; } 

    }
}

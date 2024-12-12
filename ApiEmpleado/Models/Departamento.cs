using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEmpleado.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; } = null!;
        public List<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}

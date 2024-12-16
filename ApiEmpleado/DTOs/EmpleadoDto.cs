namespace ApiEmpleado.DTOs
{
    public class EmpleadoDto
    {
        public string Nombre { get; set; } = null!;
        public string PApellido { get; set; } = null!;
        public string SApellido { get; set; } = null!;
        public int Edad { get; set; }
        public int Telefono { get; set; }
        public int IdDepartamento { get; set; }
    }
}

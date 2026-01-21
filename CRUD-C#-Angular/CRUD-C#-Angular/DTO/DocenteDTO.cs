namespace CRUD_C__Angular.DTO
{
    public class DocenteDTO
    {
        public int IdDocente { get; set; }

        public string NombrePrimer { get; set; } = null!;

        public string NombreSegundo { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string? Correo { get; set; }

        public string Dni { get; set; } = null!;

        public int? Edad { get; set; }

        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
    }
}

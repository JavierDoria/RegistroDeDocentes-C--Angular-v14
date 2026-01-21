using System;
using System.Collections.Generic;

namespace CRUD_C__Angular.Models;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Docente> Docentes { get; } = new List<Docente>();
}

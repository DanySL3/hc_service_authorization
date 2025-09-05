using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class SistemaUsuario
{
    public int Id { get; set; }

    public int SistemaId { get; set; }

    public int UsuarioId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public bool? Isactive { get; set; }

    public int? Usercreatedid { get; set; }

    public int? Usermodifiedid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }
}

using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class AgenciaUsuario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int AgenciaId { get; set; }

    public int? SistemaId { get; set; }

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

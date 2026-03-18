using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class MenuPerfil
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public int PerfilId { get; set; }

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

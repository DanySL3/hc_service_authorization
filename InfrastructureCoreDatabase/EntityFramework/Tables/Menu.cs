using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Menu
{
    public int Id { get; set; }

    public int? MenuPadreId { get; set; }

    public int MenuTipoId { get; set; }

    public int SistemaId { get; set; }

    public int? Orden { get; set; }

    public string Menu1 { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

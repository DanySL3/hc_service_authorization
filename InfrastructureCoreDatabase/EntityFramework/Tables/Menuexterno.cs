using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class MenuExterno
{
    public int Id { get; set; }

    public int? MenuPadreId { get; set; }

    public string Menu { get; set; } = null!;

    public string? Descripcion { get; set; }

    public double Orden { get; set; }

    public string Url { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

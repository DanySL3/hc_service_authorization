using System;
using System.Collections.Generic;

namespace InfrastructureCoreDatabase.EntityFramework.Tables;

public partial class Sistema
{
    public Guid Uuid { get; set; }

    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? IsActive { get; set; }

    public int UserCreatedId { get; set; }

    public int? UserModifiedId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
